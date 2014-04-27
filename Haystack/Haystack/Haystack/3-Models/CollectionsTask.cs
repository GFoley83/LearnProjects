using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haystack
{
    internal class CollectionsTask
    {
        internal CollectionsTask()
        {

        }

        //Task
        internal string Id;
        internal string State;
        internal string Created;

        //Proposed
        internal string Project;
        internal string Source;
        internal string Command;
        internal string Target;
        internal string Parameters;

        //Issued
        internal string Issued;

        //Collections
        internal string Collected;
        internal string Station;
        internal string Collector;

        //Processing
        internal string ResultHash
        {
            get { return CollectionsTaskHelper.CalculateMD5Hash(ResultJson); }
            set { }
        }
        internal string BlobName
        {
            get { return string.Format("{0}_{1}_{2}", Project, Collected, Id); }
            set { }
        }
        internal string Processed;

        //Extra
        internal Microsoft.WindowsAzure.StorageClient.CloudQueueMessage QueueMessage;
        internal string ResultJson;
    }

    public enum QueueTypeEnum
    {
        CollectionsTwitterProfile,
        CollectionsTwitterFriends,
        CollectionsTwitterFollowers,
        CollectionsTwitterTweets,
        CollectionsTwitterSearch,
        ProcessingTwitter,
        FailuresTwitter
    }

    public enum StateTypeEnum
    {
        Proposed,
        Approved,
        Issued,
        Collected,
        Processed,
        Actioned,
        Failed
    }

    public enum SourceTypeEnum
    {
        Twitter
    }

    public enum TwitterCommandEnum
    {
        Profile,
        Friends,
        Followers,
        Tweets,
        Search
    }
}