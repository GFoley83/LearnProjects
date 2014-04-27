using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using System.Configuration;
using System.Diagnostics;
using System.Threading;

//Sleep
//--Hit API Rate Limit
//--No Queues, 162

namespace AzureConsole
{
    class WorkerClass
    {
        private ManualResetEvent _doneEvent;
        public WorkerClass(ManualResetEvent doneEvent)
        {
            _doneEvent = doneEvent;
        }

        public void ThreadPoolCallback(Object threadContext)
        {
            int threadIndex = (int)threadContext;
            Console.WriteLine("thread {0} started...", threadIndex);
            Calculate();
            Console.WriteLine("thread {0} result calculated...", threadIndex);
            _doneEvent.Set();
        }

        private void Calculate()
        {
            //CheckLimit
            //Poll 32
            //Execute
            //
            
        }

    }


    class LearnQueues
    {
        string _queueCollections = "collections";
        string _queueProcessing = "processing";
        string _queuePoison = "poison";

        const int _threadCount = 10;

        public void Run()
        {
            //CreateJobs();
            CreateWorkers();
            
        }

        //Create 1,000 Queues
        private void CreateJobs()
        {
            CloudQueueClient queueClient = StorageClientManager.GetQueueClient();
            queueClient.GetQueueReference(_queueCollections);
            CloudQueue queue = queueClient.GetQueueReference(_queueCollections);

            for (int i = 0; i < 10000; i++)
            {
                CloudQueueMessage message = new CloudQueueMessage(string.Format("collection job #{0}", i));
                queue.AddMessage(message);
            }
        }

        private void CreateWorkers()
        {
            ManualResetEvent[] doneEvents = new ManualResetEvent[_threadCount];
            WorkerClass[] workers = new WorkerClass[_threadCount];

            for (int i = 0; i < _threadCount; i++)
            {
                doneEvents[i] = new ManualResetEvent(false);
                WorkerClass worker = new WorkerClass(doneEvents[i]);
                workers[i] = worker;
                ThreadPool.QueueUserWorkItem(worker.ThreadPoolCallback, i);
            }

            WaitHandle.WaitAll(doneEvents);
            Console.WriteLine("All calculations are complete.");
        }

        private void ProcessProcessingQueue()
        {

        }

        //Poison Message
        public void ProcessMessage()
        {
            CloudQueueClient queueClient = StorageClientManager.GetQueueClient();
            CloudQueue collectionsQueue = queueClient.GetQueueReference(_queueCollections);
            collectionsQueue.CreateIfNotExist();

            CloudQueueMessage message = collectionsQueue.GetMessage();

            if (message == null)
            {
                //Stop Polling
                return;
            }

            try
            {
                //Do Work
                collectionsQueue.DeleteMessage(message);
            }
            catch //Handle Poisned Messages
            {
                if (message.DequeueCount > 3)
                {
                    CloudQueue poisonQueue = queueClient.GetQueueReference(_queuePoison);
                    poisonQueue.CreateIfNotExist();
                    poisonQueue.AddMessage(message);
                    collectionsQueue.DeleteMessage(message);
                }
            }
        }
    }
}
