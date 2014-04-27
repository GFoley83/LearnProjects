using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HaystackLibrary
{
    internal class CollectionsTaskLocal
    {
        internal CollectionsTaskLocal()
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

        //Issued
        internal string Issued;

        //Collections
        internal string Collected;
        internal string CollectionsStation;
        internal string CollectorInstance;

        internal string ResultsXml;
        internal string ResultsHash
        {
            get { return CollectionsTaskHelper.CalculateMD5Hash(ResultsXml); }
            set { }
        }
        internal string BlobName
        {
            get { return string.Format("{0}_{1}_{2}", Project, Collected, Id); }
            set { }
        }

        //Processing
        internal string Processed;

        internal Microsoft.WindowsAzure.StorageClient.CloudQueueMessage QueueMessage;
    }

    public enum CollectionsTaskState
    {
        Proposed,
        Approved,
        Issued,
        Collected,
        Processed,
        Actioned,
        Failed
    }
}
