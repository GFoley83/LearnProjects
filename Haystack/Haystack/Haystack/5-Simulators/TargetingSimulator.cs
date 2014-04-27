using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haystack
{
    public static class TargetingSimulator
    {
        public static void ClearQueues()
        {
            AzureClientService.GetCloudQueue(QueueTypeEnum.CollectionsTwitterFollowers).Clear();
            AzureClientService.GetCloudQueue(QueueTypeEnum.CollectionsTwitterFriends).Clear();
            AzureClientService.GetCloudQueue(QueueTypeEnum.CollectionsTwitterProfile).Clear();
            AzureClientService.GetCloudQueue(QueueTypeEnum.CollectionsTwitterProfile).Clear();
            AzureClientService.GetCloudQueue(QueueTypeEnum.CollectionsTwitterSearch).Clear();

            AzureClientService.GetCloudQueue(QueueTypeEnum.ProcessingTwitter).Clear();
            AzureClientService.GetCloudQueue(QueueTypeEnum.FailuresTwitter).Clear();
            Console.WriteLine("All Queues Cleared");
        }

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

        public static void IssueCollectionTasks()
        {
            TargetingService ts = new TargetingService();
            ts.IssueCollectionTasks();
        }

        public static void CreateCollectionTasks()
        {
            TargetingService ts = new TargetingService();
            ts.CreateCollectionTasks();
        }
    }
}
