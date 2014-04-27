using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using System;
using System.Configuration;
using System.Diagnostics;

namespace AzureConsole
{
    public static class StorageClientManager
    {
        /*
          CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["DataConnectionString"]);
          
          <appSettings>
            <add key="DataConnectionString" value="DefaultEndpointsProtocol=https;AccountName={Account Name};AccountKey={Account Key}"/>
            <add key="AccountName" value="{Account Name}"/>
            <add key="AccountKey" value="{Account Key}"/>
          </appSettings>
         */

        private static CloudStorageAccount GetStorageAccount()
        {
            //Get Storage Credentials
            //StorageCredentialsAccountAndKey storageCredentials = new StorageCredentialsAccountAndKey(
            //    ConfigurationManager.AppSettings["AccountName"],
            //    ConfigurationManager.AppSettings["AccountKey"]);

            //Return Storage Account
            CloudStorageAccount storageAccount;
#if DEBUG
            Debug.WriteLine("Mode=Debug");
            storageAccount = CloudStorageAccount.DevelopmentStorageAccount;
#else
            Debug.WriteLine("Mode=Release");
            StorageCredentialsAccountAndKey storageCredentials = new StorageCredentialsAccountAndKey(
                ConfigurationManager.AppSettings["AccountName"],
                ConfigurationManager.AppSettings["AccountKey"]);
            storageAccount = new CloudStorageAccount(storageCredentials, true);
#endif

            return storageAccount;
        }

        public static CloudBlobClient GetBlobClient()
        {
            CloudBlobClient blobClient = GetStorageAccount().CreateCloudBlobClient();
            return blobClient;
        }

        public static CloudTableClient GetTableClient()
        {
            CloudTableClient tableClient = GetStorageAccount().CreateCloudTableClient();
            return tableClient;
        }

        public static CloudQueueClient GetQueueClient()
        {
            CloudQueueClient queueClient = GetStorageAccount().CreateCloudQueueClient();
            return queueClient;
        }
    }
}
