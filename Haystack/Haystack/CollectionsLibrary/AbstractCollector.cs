using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

using Microsoft.WindowsAzure.StorageClient;
using System.Collections;

namespace Haystack
{
    public abstract class AbstractCollector
    {
        protected string _collectionsStation = "DefaultStation";
        protected string _collectorInstance = "DefaultInstance";
        protected double _collectionLoopInterval = 10000;               //overwritten by subclass
        protected Timer _collectionLoopTimer;

        public void Startup(string collectionStation, string collectorInstance)
        {
            _collectionsStation = collectionStation;
            _collectorInstance = collectorInstance;

            //Starting  -> Running  True
            //Running   -> Running  True
            //Waiting   -> Waiting  True
            //Stopping  -> Stopped  False
            //Stopped   -> Stopped  False
            //Failure or Stopped??

            _collectionLoopTimer = new Timer();
            _collectionLoopTimer.Elapsed += _collectionLoopTimer_Elapsed;
            _collectionLoopTimer.Interval = 100;                       //fire immediately
            _collectionLoopTimer.Start();
        }

        public void Shutdown(string message)
        {
            Logger.Log("Shutdown");
            _collectionLoopTimer.Stop();
        }
                
        int _collectionCapacity = 0;                                    //used to track API limit
        bool _queueContainsMessages = true;                             //used to determine loop status
        protected CollectionsTaskLocal _currentTask;                         //used in ExecuteTask and ReportTask

        //Step 0, Timer Loop, Outer Loop, Inner Loop
        void _collectionLoopTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Logger.Log("");
            Logger.Log("");
            Logger.Log("----------Timer Loop ----------");

            PreCollection();

            while ((_collectionCapacity > 0) && _queueContainsMessages)
            {
                //RequestCollectionTasks                    Math.Min(_collectionCapacity, 32)
                List<CollectionsTaskLocal> collectionsTaskList = RequestCollectionTasks(_collectionCapacity);

                if (collectionsTaskList.Count<CollectionsTaskLocal>() > 0)
                {
                    foreach (CollectionsTaskLocal taskIn in collectionsTaskList)
                    {
                        _collectionCapacity--;
                        _currentTask = taskIn;

                        ExecuteCollectionTask();
                        ReportCollectionTask();

                        StorageClientHelper.DeleteQueueMessage(QueueType.Collections, SourceType.Twitter, _currentTask.QueueMessage);
                    }
                }
                else
                {
                    Logger.Log("Received zero messages");                    
                    _queueContainsMessages = false;
                }
            }

            PostCollection();
        }

        //Step 1, Timer Loop
        private void PreCollection()
        {
            Logger.Log("PreCollection");

            //Timmer Delay
            _collectionLoopTimer.Interval = _collectionLoopInterval;

            //GetCollectionCapaicty
            _collectionCapacity = GetCollectionCapacity();

            //Log
        }

        //Step 2, Timer Loop
        protected abstract int GetCollectionCapacity();

        //Step 3, Outer Loop
        private List<CollectionsTaskLocal> RequestCollectionTasks(int collectionCapacity)
        {
            Logger.Log("RequestCollectionTasks");
            //Hard Limit by Azure
            int messageCount = Math.Min(collectionCapacity, 32);

            //Get Q Messages
            IEnumerable<CloudQueueMessage> queueMessages = StorageClientHelper.GetQueueMessages(QueueType.Collections, _sourceType, messageCount);
            
            //Convert to Tasks
            List<CollectionsTaskLocal> collectionsTaskList = new List<CollectionsTaskLocal>();
            if (queueMessages == null)
            {
                return collectionsTaskList;
            }
            else
                Logger.Log("Received #" + queueMessages.Count<CloudQueueMessage>().ToString() + " messages");

            foreach (CloudQueueMessage qm in queueMessages)
            {
                string messag = qm.AsString;
                CollectionsTaskLocal task = CollectionsTaskHelper.MessageToLocalTask(messag);
                task.QueueMessage = qm;
                collectionsTaskList.Add(task);
            }
            
            return collectionsTaskList;
        }

        //Step 4, Inner Loop
        protected abstract void ExecuteCollectionTask();

        //Step 5, Inner Loop
        private void ReportCollectionTask()
        {
            Logger.Log("ReportCollectionTask: #" + _currentTask.Command);
            Logger.Log("");

            //MetaData
            _currentTask.CollectionsStation = _collectionsStation;
            _currentTask.CollectorInstance = _collectorInstance;
            _currentTask.Collected = CollectionsTaskHelper.Now();
            
            //SaveBlob
            CloudBlob blob = StorageClientHelper.GetBlobReference(_currentTask.Project, _currentTask.BlobName);
            blob.Attributes.Properties.ContentMD5 = _currentTask.ResultsHash;
            blob.Attributes.Metadata["Id"] = _currentTask.Id.ToString();
            blob.Attributes.Metadata["Project"] = _currentTask.Project;
            blob.Attributes.Metadata["State"] = CollectionsTaskState.Collected.ToString();
            blob.Attributes.Metadata["Collected"] = _currentTask.Collected;
            blob.UploadText(_currentTask.ResultsXml);

            //AddQueue
            string message = CollectionsTaskHelper.LocalTaskToMessage(_currentTask);
            StorageClientHelper.AddQueueMessage(QueueType.Processing, SourceType.Twitter, message);
        }

        //Step 6, Timer Loop
        private void PostCollection()
        {
            Logger.Log("PostCollection");
            //TODO: PostCollection() Reporting
        }
    }
}