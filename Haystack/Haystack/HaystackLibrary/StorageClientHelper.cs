using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaystackLibrary
{
    public enum QueueType
    {
        Collections,
        Processing,
        Failures
    }

    public enum SourceType
    {
        Twitter,
    }

    public static class StorageClientHelper
    {
        private static string _AccountName = "portalvhdsmd337rk46w2xn";
        private static string _AccountKey = "48y8rG5s1nVI7HhYxiumg/hfNIvyHyn7uQ9QzZgVu8D142kperkPeYhXUC694WPVgrdBWNi1o0fbyYMt1PZacg==";

        private static string _SqlAzureServer = "";
        private static string _SqlAzureUsername = "";
        private static string _SqlAzurePassword = "";

        private static CloudStorageAccount GetStorageAccount()
        {
            //Return Storage Account
            CloudStorageAccount storageAccount;
#if DEBUG
            //Debug.WriteLine("Mode=Debug");
            storageAccount = CloudStorageAccount.DevelopmentStorageAccount;
#else
            StorageCredentialsAccountAndKey storageCredentials = new StorageCredentialsAccountAndKey(_AccountName, _AccountKey);
            storageAccount = new CloudStorageAccount(storageCredentials, true);
#endif
            return storageAccount;
        }

        #region Queues
        public static CloudQueue GetCloudQueue(QueueType type, SourceType source)
        {
            //Queue eference Format
            string queueReference = source.ToString().ToLower() + "-" + type.ToString().ToLower();

            CloudQueueClient queueClient = GetStorageAccount().CreateCloudQueueClient();
            CloudQueue queue = queueClient.GetQueueReference(queueReference);
            queue.CreateIfNotExist();
            return queue;
        }

        public static void AddQueueMessage(QueueType type, SourceType source, string message)
        {
            CloudQueue queue = GetCloudQueue(type, source);
            queue.AddMessage(new CloudQueueMessage(message));
        }

        public static List<CloudQueueMessage> GetQueueMessages(QueueType type, SourceType source, int messageCount)
        {
            messageCount = Math.Min(messageCount, 32); //32 is hard limit
            CloudQueue queue = GetCloudQueue(type, source);

            List<CloudQueueMessage> queueMessageList = new List<CloudQueueMessage>();
            
            IEnumerable<CloudQueueMessage> queueMessages = queue.GetMessages(messageCount);

            if ((queueMessages != null) && (queueMessages.Count<CloudQueueMessage>() > 0))
            {
                foreach (CloudQueueMessage message in queueMessages)
                {
                    //Check Poisen Message
                    if (message.DequeueCount < 3)
                        queueMessageList.Add(message);
                    else
                    {
                        AddQueueMessage(QueueType.Failures, SourceType.Twitter, message.AsString);
                        queue.DeleteMessage(message);
                    }
                }
            }

            return queueMessageList;
        }

        public static void DeleteQueueMessage(QueueType type, SourceType source, CloudQueueMessage message)
        {
            CloudQueue queue = GetCloudQueue(type, source);
            queue.DeleteMessage(message);
        }

        public static string GetQueuesStatus(QueueType type, SourceType source)
        {
            CloudQueue queue = GetCloudQueue(type, source);
            return string.Format("{0} queue of {1} source has #{2} messages.", queue.Name, source, queue.RetrieveApproximateMessageCount());
        }

        //public static string[] GetQueueMessages(QueueType type, SourceType source, int messageCount)
        //{
        //    messageCount = Math.Min(messageCount, 32); //32 is hard limit
            
        //    CloudQueue queue = GetCloudQueue(type, source);
        //    List<string> messages = new List<string>();

        //    if (queue.RetrieveApproximateMessageCount() > 0)
        //    {
        //        IEnumerable<CloudQueueMessage> queueMessages = queue.GetMessages(messageCount);
        //        foreach (CloudQueueMessage queueMessage in queueMessages)
        //        {
        //            messages.Add(queueMessage.AsString);
        //        }
        //        return messages.ToArray<string>();
        //    }
        //    else
        //        return null;
        //}

        //public static string GetQueueMessage(QueueType type, SourceType source)
        //{
        //    string[] messages = GetQueueMessages(type, source, 1);

        //    if (messages.Count<string>() == 1)
        //        return messages.ElementAt<string>(0);
        //    else
        //        return null;
        //}
        #endregion

        #region Blobs
        public static CloudBlobContainer GetBlobContainer(string projectName)
        {
            CloudBlobClient blobClient = GetStorageAccount().CreateCloudBlobClient();
            CloudBlobContainer blobContainer = blobClient.GetContainerReference(projectName.ToLower());
            blobContainer.CreateIfNotExist();
            return blobContainer;
        }

        public static CloudBlob GetBlobReference(string projectName, string blobName)
        {
            CloudBlobContainer blobContainer = GetBlobContainer(projectName);
            CloudBlob blob = blobContainer.GetBlobReference(blobName.ToLower());
            return blob;
        }

        //public static string ListContainerBlobs(string projectName)
        //{
        //    CloudBlobClient blobClient = GetStorageAccount().CreateCloudBlobClient();
        //    CloudBlobContainer blobContainer = blobClient.GetContainerReference(projectName.ToLower());
        //    IEnumerable<IListBlobItem> blobItems = blobContainer.ListBlobs();
        //    StringBuilder sb = new StringBuilder();
        //    foreach (IListBlobItem blobItem in blobItems)
        //    {
        //        sb.AppendLine(blobItem.Uri.AbsoluteUri);
        //    }
        //    return sb.ToString();
        //}
        #endregion

        #region SqlAzure
        public static string GetSqlConnectionString()
        {
            return "";
        }

        #endregion

    }
}