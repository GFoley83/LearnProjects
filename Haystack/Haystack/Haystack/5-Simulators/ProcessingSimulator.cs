using Microsoft.WindowsAzure.StorageClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haystack
{
    public static class ProcessingSimulator
    {
        public static void PrintQueues()
        {
            Console.WriteLine(AzureClientService.GetQueuesStatus(QueueTypeEnum.CollectionsTwitterFollowers));
            Console.WriteLine(AzureClientService.GetQueuesStatus(QueueTypeEnum.CollectionsTwitterFriends));
            Console.WriteLine(AzureClientService.GetQueuesStatus(QueueTypeEnum.CollectionsTwitterProfile));
            Console.WriteLine(AzureClientService.GetQueuesStatus(QueueTypeEnum.CollectionsTwitterSearch));
            Console.WriteLine(AzureClientService.GetQueuesStatus(QueueTypeEnum.CollectionsTwitterTweets));

            Console.WriteLine(AzureClientService.GetQueuesStatus(QueueTypeEnum.ProcessingTwitter));
            Console.WriteLine(AzureClientService.GetQueuesStatus(QueueTypeEnum.FailuresTwitter));
        }

        public static void Display()
        {
            CloudQueue queue = AzureClientService.GetCloudQueue(QueueTypeEnum.ProcessingTwitter);
            int messageCount = queue.RetrieveApproximateMessageCount();
            for (int i = 0; i < messageCount; i++)
            {
                CloudQueueMessage message = queue.GetMessage();
                CollectionsTask task = CollectionsTaskHelper.MessageToCollectionsTask(message.AsString);
                Console.WriteLine(string.Format("ID={0}, Command{1}, Target={2}",
                    task.Id, task.Command, task.Target));

                //GetBlob
                CloudBlob blob = AzureClientService.GetBlobReference(task.Project, task.BlobName);
                string resultsXml = blob.DownloadText();

                Console.WriteLine(string.Format("BLOB: {0}, {1}, {2}, {3}, {4},{5}",
                    blob.Attributes.Metadata["Id"].ToString(),
                    blob.Attributes.Metadata["Project"].ToString(),
                    blob.Attributes.Metadata["State"].ToString(),
                    blob.Attributes.Metadata["Collected"].ToString(),
                    blob.Name,
                    blob.Attributes.Properties.ContentMD5.ToString()));
                Console.WriteLine(string.Format("BLOB XML: {0}", resultsXml));
            }
        }

        public static void Delete()
        {
            CloudQueue queue = AzureClientService.GetCloudQueue(QueueTypeEnum.ProcessingTwitter);
            int messageCount = queue.RetrieveApproximateMessageCount();
            for (int i = 0; i < messageCount; i++)
            {
                CloudQueueMessage message = queue.GetMessage();
                CollectionsTask task = CollectionsTaskHelper.MessageToCollectionsTask(message.AsString);

                //GetBlob
                CloudBlob blob = AzureClientService.GetBlobReference(task.Project, task.BlobName);
#if DEBUG
                blob.DeleteIfExists();
#endif
                queue.DeleteMessage(message);
            }
        }
    }
}
