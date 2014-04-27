using Microsoft.WindowsAzure.StorageClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AzureConsole
{
    class LearnAzureStorage
    {
        #region Blob
        //Account -> Container(s) -> Blob(s)
        //http://<storage account>.blob.core.windows.net/<container>/<blob>

        string _blobContainerName = "haystackcontainer";
        string _blobName = "myblob";
        string _blobAbsoutlteUri = "";
        long _8MB = 8388608;

        public void RunBlobs()
        {
            Console.WriteLine("RunBlobs()");
            
            //Blob Client
            CloudBlobClient blobClient = StorageClientManager.GetBlobClient();

            //Get Container
            CloudBlobContainer blobContainer = blobClient.GetContainerReference(_blobContainerName);
            blobContainer.CreateIfNotExist();

            //Create Text
            StringBuilder sb = new StringBuilder("Howdy World!");
            CloudBlob blob = blobContainer.GetBlobReference(_blobName);
            blob.Attributes.Metadata["Status"] = "Unprocessed";
            blob.UploadText(sb.ToString());

            blob.FetchAttributes();
            Console.WriteLine("MD5 = " + blob.Attributes.Properties.ContentMD5);
            foreach (string key in blob.Attributes.Metadata)
            {
                Console.WriteLine(string.Format("Key={0}, Value={1}", key, blob.Attributes.Metadata[key]));
            }

            //List Blobs
            foreach (var blobItem in blobContainer.ListBlobs())
            {
                Console.WriteLine(blobItem.Uri.ToString());
            }

            //Print Blob
            CloudBlob blob2 = blobContainer.GetBlobReference(_blobName);
            string output = blob2.DownloadText();

            //string output = blockBlob.DownloadText();
            Console.WriteLine(String.Format("{0} says, {1}", _blobName, output));

            //Delete Blob
            blob2.DeleteIfExists();

            //List Blobs
            Console.WriteLine("List Blobs:");
            foreach (var blobItem in blobContainer.ListBlobs())
            {
                Console.WriteLine(blobItem.Uri.ToString());
            }
        }

        private string CalculateMD5Hash(string s)
        {
            MD5 md5 = MD5.Create();
            byte[] b = System.Text.Encoding.UTF8.GetBytes(s);
            Byte[] md5Hash = md5.ComputeHash(b);
            String base64EncodedMD5Hash =
                Convert.ToBase64String(md5Hash);
            return base64EncodedMD5Hash;
        }
        #endregion

        #region Queues
        string _queueName = "myqueue";
        CloudQueueClient _queueClient;
        public void RunQueues()
        {
            Console.WriteLine("RunQueues()");
            _queueClient = StorageClientManager.GetQueueClient();

            CreateQueue(_queueName);
            GetQueueInfomration(_queueName);
            DeleteQueue(_queueName);
        }

        private void CreateQueue(string queueName)
        {
            CloudQueue queue = _queueClient.GetQueueReference(queueName);
            queue.CreateIfNotExist();
            CloudQueueMessage message1 = new CloudQueueMessage("MyMessage1");
            CloudQueueMessage message2 = new CloudQueueMessage("MyMessage2");
            queue.AddMessage(message1);
            queue.AddMessage(message2);
            queue.Metadata["Metadata1"] = "MyMetadata1";
            queue.CreateIfNotExist();
        }

        private void GetQueueInfomration(string queueName)
        {
            CloudQueue queue = _queueClient.GetQueueReference(queueName);
            //Enumerate Metadata
            queue.FetchAttributes();
            foreach (string key in queue.Metadata.Keys)
            {
                Console.WriteLine(string.Format("Key={0}, Value={1}", key, queue.Metadata[key]));
            }

            //Message Count, Enumerate Messages
            int messageCount = queue.RetrieveApproximateMessageCount();
            Console.WriteLine(string.Format("RetrieveApproximateMessageCount() = {0}", messageCount));
            IEnumerable<CloudQueueMessage> messages = queue.GetMessages(messageCount);
            foreach (CloudQueueMessage message in messages)
            {
                Console.WriteLine(string.Format("Queue {0}, Message: {1}", queueName, message.AsString));
            }
        }

        private void DeleteQueue(string queueName)
        {
            CloudQueue queue = _queueClient.GetQueueReference(queueName);
            queue.Delete();
        }

        #endregion

        //PartitionKey
        //RowKey

        string _tableName = "mytable";
        public void RunTables()
        {
            Console.WriteLine("RunTables()");
            CloudTableClient tableClient = StorageClientManager.GetTableClient();

            //Create Table
            tableClient.CreateTableIfNotExist(_tableName);

            //List Tables
            IEnumerable<string> tables = tableClient.ListTables();
            foreach (string table in tables)
            {
                Console.WriteLine(table);
            }

            //DataModel, DataContext

            

            tableClient.DeleteTableIfExist(_tableName);

        }
    }
}
