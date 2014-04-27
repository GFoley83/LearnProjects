using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Haystack;

namespace Haystack
{
    public static class AzureClientService
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
            //Logger.Log("Mode=Debug");
            //storageAccount = CloudStorageAccount.DevelopmentStorageAccount;
            StorageCredentialsAccountAndKey storageCredentials = new StorageCredentialsAccountAndKey(_AccountName, _AccountKey);
            storageAccount = new CloudStorageAccount(storageCredentials, true);
#else
            StorageCredentialsAccountAndKey storageCredentials = new StorageCredentialsAccountAndKey(_AccountName, _AccountKey);
            storageAccount = new CloudStorageAccount(storageCredentials, true);
#endif
            return storageAccount;
        }

        #region Queues
        public static CloudQueue GetCloudQueue(QueueTypeEnum type)
        {
            //Queue eference Format
            string queueReference = type.ToString().ToLower();

            CloudQueueClient queueClient = GetStorageAccount().CreateCloudQueueClient();
            CloudQueue queue = queueClient.GetQueueReference(queueReference);
            queue.CreateIfNotExist();
            return queue;
        }

        public static void AddQueueMessage(QueueTypeEnum type, string message)
        {
            CloudQueue queue = GetCloudQueue(type);
            queue.AddMessage(new CloudQueueMessage(message));
        }

        public static CloudQueueMessage GetQueueMessage(QueueTypeEnum type)
        {

            CloudQueue queue = GetCloudQueue(type);
            CloudQueueMessage message;

            bool loop = true;
            do
            {
                message = queue.GetMessage();
                if (message == null)
                    loop = false; //No Message
                else if (message.DequeueCount < 3)
                    loop = false; //Good Message
                else
                {
                    //Poisen Message
                    AddQueueMessage(QueueTypeEnum.FailuresTwitter, message.AsString);
                    queue.DeleteMessage(message);
                }
            } while (loop);

            return message;
        }

        public static List<CloudQueueMessage> GetQueueMessages(QueueTypeEnum type, int messageCount)
        {
            messageCount = Math.Min(messageCount, 32); //32 is hard limit
            CloudQueue queue = GetCloudQueue(type);

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
                        AddQueueMessage(QueueTypeEnum.FailuresTwitter, message.AsString);
                        queue.DeleteMessage(message);
                    }
                }
            }

            return queueMessageList;
        }

        public static void DeleteQueueMessage(QueueTypeEnum type, CloudQueueMessage message)
        {
            CloudQueue queue = GetCloudQueue(type);
            queue.DeleteMessage(message);
        }

        public static string GetQueuesStatus(QueueTypeEnum type)
        {
            CloudQueue queue = GetCloudQueue(type);
            return string.Format("{0} queue #{1} messages.", queue.Name, queue.RetrieveApproximateMessageCount());
        }

        //public static string[] GetQueueMessages(CollectionsTaskQueue type, CollectionsTaskSource source, int messageCount)
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

        //public static string GetQueueMessage(CollectionsTaskQueue type, CollectionsTaskSource source)
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

        public static void WriteBlobLog(string log)
        {
            CloudBlob blob = GetBlobReference("Log", "Log - " + DateTime.Now.Date.ToString());
            
            string text = "";

            if (Exists(blob))
            {
                text = blob.DownloadText();
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(String.Format("{0}: {1}", DateTime.Now.ToString(), log));
            sb.AppendLine(text);

            blob.UploadText(sb.ToString());
        }

        public static bool Exists(this CloudBlob blob)
        {
            try
            {
                blob.FetchAttributes();
                return true;
            }
            catch (StorageClientException e)
            {
                if (e.ErrorCode == StorageErrorCode.ResourceNotFound)
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
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
        //private static string _SqlAzureServer = "";
        //private static string _SqlAzureUsername = "";
        //private static string _SqlAzurePassword = "";

        //internal static string GetSqlConnectionString()
        //{
        //    return "";
        //}
        #endregion
    }
}
