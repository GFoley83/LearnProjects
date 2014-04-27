using HaystackLibrary;
using Microsoft.WindowsAzure.StorageClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TapLibarary
{
    public class ProccessingManager
    {
        public void ProccesCollectionTasks()
        {
            bool loop = true;

            while (loop)
            {
                IEnumerable<CloudQueueMessage> queueMessages = StorageClientHelper.GetQueueMessages(QueueType.Processing, SourceType.Twitter, 32);

                int messageCount = queueMessages.Count<CloudQueueMessage>();

                if ((messageCount < 1) || (messageCount == null))
                {
                    Console.WriteLine("No Messages");
                    loop = false;
                }
                else
                {
                    Console.WriteLine("GetQueueMessage = " + queueMessages.Count<CloudQueueMessage>());

                    foreach (CloudQueueMessage queueMessage in queueMessages)
                    {
                        //1. Get Queue
                        HaystackLibrary.CollectionTask newTask = CollectionsTaskHelper.MessageToDatabaseTask(queueMessage.AsString);
                        string taskString = CollectionsTaskHelper.DatabaseTaskString(newTask);
                        Console.WriteLine(taskString);

                        //2. Get Blob
                        CloudBlob blob = StorageClientHelper.GetBlobReference(newTask.Project, newTask.BlobName);
                        string blobText = blob.DownloadText();
                        //TODO: Blob Validate Hash
                        Console.WriteLine(blobText);

                        //3. Update Database: Results
                        //TODO: Update Database: Results







                        //4. Update Database: Tasks
                        LinqToSqlDataContext context = new LinqToSqlDataContext();
                        CollectionTask oldTask = (from t in context.CollectionTasks
                                                  where t.Id == newTask.Id
                                                  orderby t.Id descending
                                                  select t).First<CollectionTask>();

                        oldTask.State = CollectionsTaskState.Processed.ToString();
                        oldTask.Collected = newTask.Collected;
                        oldTask.Station = newTask.Station;
                        oldTask.Collector = newTask.Collector;
                        oldTask.BlobName = newTask.BlobName;
                        oldTask.BlobHash = blob.Attributes.Properties.ContentMD5;
                        oldTask.Processed = DateTime.UtcNow;
                        context.SubmitChanges();

                        //5. Update Blob Metadata
                        blob.Attributes.Metadata["State"] = CollectionsTaskState.Processed.ToString();

                        //6. Delete Queue
                        StorageClientHelper.DeleteQueueMessage(QueueType.Processing, SourceType.Twitter, queueMessage);

                        //7. Print Success
                        string output = CollectionsTaskHelper.DatabaseTaskString(newTask);
                        Console.WriteLine(output);
                    }
                }
            }
        }
    }
}
