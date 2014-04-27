using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.WindowsAzure.StorageClient;
using System.ComponentModel;
using System.Diagnostics;
namespace Haystack
{
    public class CollectionsOfficer
    {
        internal string _station;
        internal string _collector;
        internal QueueTypeEnum _collectionsQueue;

        internal bool _loopOfficer;
        internal int _sleepOfficer;

        internal CloudQueueMessage _currentQueueMessage;
        internal CollectionsTask _currentTask;
        internal AbstractCollectionsAgent _currentAgent;

        internal BackgroundWorker worker;

        public CollectionsOfficer(string station, string collector, QueueTypeEnum collectionsQueue)
        {
            _station = station;
            _collector = collector;
            _collectionsQueue = collectionsQueue;
            _loopOfficer = true;
            _currentTask = new CollectionsTask();
            _currentTask.Id = "NO ID"; //placeholder for no new task
#if DEBUG
            //Sleep for 1 Minute
            _sleepOfficer = 60000;
#else
            //Sleep for 20 Minutes
            _sleepOfficer = 1200000;
#endif
            worker = new BackgroundWorker();
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.WorkerSupportsCancellation = true;
        }

        public void Run()
        {
            Logger.Log(GetContext()+": RunWorkerAsync()");
            worker.RunWorkerAsync();
        }

        //Loop
        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (_loopOfficer)
            {
                //1-PreCollection
                PreCollection();

                //2-GetCollectionTask
                #region GetCollectionTask
                bool collectionTaskRecieved = false;
                try
                {
                    collectionTaskRecieved = GetCollectionTask();
                }
                catch (Exception ex)
                {
                    Logger.Log(GetContext() + "Error @ GetCollectionTask(): " + ex.Message);
#if DEBUG
                    //throw;
#endif
                }
                #endregion

                if (collectionTaskRecieved)
                {
                    //3-SelectAgent
                    #region SelectAgent
                    try
                    {
                        SelectAgent();
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(GetContext() + "Error @ SelectAgent(): " + ex.Message);
#if DEBUG
                        //throw;
#endif
                    }
                    #endregion

                    //4-RunAgent
                    #region RunAgent
                    try
                    {
                        RunAgent();
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(GetContext() + "Error @ RunAgent(): " + ex.Message);
#if DEBUG
                        //throw;
#endif
                    }
                    #endregion

                    //5-ReportCollectionTask
                    #region ReportCollectionTask
                    try
                    {
                        ReportCollectionTask();
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(GetContext() + "Error @ ReportCollectionTask(): " + ex.Message);
#if DEBUG
                        //throw;
#endif
                    }
                    #endregion

                    //6-PostCollection
                    PostCollection();
                }
                else
                {
                    //Sleep
                    Logger.Log(GetContext() +": No Queue Message, Sleeping Thread...");
                    Thread.Sleep(_sleepOfficer);
                }
            }
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Logger.Log(GetContext() + "worker_RunWorkerCompleted");
        }

        //1-PreCollection
        protected void PreCollection()
        {
        }

        //2-GetCollectionTask
        protected bool GetCollectionTask()
        {
            Logger.Log(GetContext() + "GetCollectionTask()");
            
            
            CloudQueueMessage queueMessage = AzureClientService.GetQueueMessage(_collectionsQueue);
            
            if (queueMessage != null)
            {
                _currentQueueMessage = queueMessage;
                _currentTask = CollectionsTaskHelper.MessageToCollectionsTask(_currentQueueMessage.AsString);
                _currentTask.QueueMessage = _currentQueueMessage;
                return true;
            }
            else
                return false;
        }

        //3-SelectAgent
        protected void SelectAgent()
        {
            if (_currentTask.Source == SourceTypeEnum.Twitter.ToString())
            {
                if (_currentTask.Command == TwitterCommandEnum.Followers.ToString())
                    _currentAgent = new TwitterCollectionsFollowersAgent(_station, _collector);
                else if (_currentTask.Command == TwitterCommandEnum.Friends.ToString())
                    _currentAgent = new TwitterCollectionsFriendsAgent(_station, _collector);
                else if (_currentTask.Command == TwitterCommandEnum.Profile.ToString())
                    _currentAgent = new TwitterCollectionsProfileAgent(_station, _collector);
                else if (_currentTask.Command == TwitterCommandEnum.Tweets.ToString())
                    _currentAgent = new TwitterCollectionsTweetsAgent(_station, _collector);
                else if (_currentTask.Command == TwitterCommandEnum.Search.ToString())
                    _currentAgent = new TwitterCollectionsSearchAgent(_station, _collector);
                else
                    throw new Exception(GetContext() + "No Command Found");
            }
            else
                throw new Exception(GetContext() + "No Source Found");
        }

        //4-RunAgent
        protected void RunAgent()
        {
            _currentTask.ResultJson = _currentAgent.RunCommand(_currentTask.Target, _currentTask.Parameters);
        }

        //5-ReportCollectionTask
        protected void ReportCollectionTask()
        {
            Logger.Log(GetContext() + "ReportCollectionTask(): #" + _currentTask.Id);

            //MetaData
            _currentTask.Station = _station;
            _currentTask.Collector = _collector;
            _currentTask.Collected = CollectionsTaskHelper.Now();

            //SaveBlob
            CloudBlob blob = AzureClientService.GetBlobReference(_currentTask.Project, _currentTask.BlobName);
            blob.Attributes.Properties.ContentMD5 = _currentTask.ResultHash;
            blob.Attributes.Metadata["Id"] = _currentTask.Id.ToString();
            blob.Attributes.Metadata["State"] = StateTypeEnum.Collected.ToString();
            blob.Attributes.Metadata["Project"] = _currentTask.Project;
            blob.Attributes.Metadata["Collected"] = _currentTask.Collected;
            blob.Attributes.Metadata["Command"] = _currentTask.Command;
            //blob.Attributes.Metadata["TargetId"] = _currentTask.
            blob.UploadText(_currentTask.ResultJson);

            //AddQueue
            string message = CollectionsTaskHelper.CollectionsTaskToMessage(_currentTask);
            AzureClientService.AddQueueMessage(QueueTypeEnum.ProcessingTwitter, message);
            AzureClientService.DeleteQueueMessage(_collectionsQueue, _currentQueueMessage);
        }

        //6-PostCollection
        protected void PostCollection()
        {
        }

        public void Stop()
        {
            Logger.Log(GetContext() + "Stop Worker");
            worker.CancelAsync();
            worker.Dispose();
        }

        //Used for Logging
        private string GetContext()
        {
            string logPrefix = string.Format("{0}\t{1}\t{2}\t{3}\t", _station, _collector, _collectionsQueue, _currentTask.Id);
            return logPrefix;
        }
    }
}