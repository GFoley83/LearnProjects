using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HaystackLibrary;
using Microsoft.WindowsAzure.StorageClient;

namespace TapLibarary
{
    public class TargetingManager
    {
        public int IssueCollectionTasks()
        {
            int tasksIssued = 0;

            //Query Collection Tasks
            LinqToSqlDataContext context = new LinqToSqlDataContext();
            IQueryable<HaystackLibrary.CollectionTask> results =
                        from r in context.CollectionTasks
                        where r.State == CollectionsTaskState.Approved.ToString()
                        select r;

            //Write Queue Message, Update State to "Issued"
            foreach (HaystackLibrary.CollectionTask r in results)
            {
                r.State = CollectionsTaskState.Issued.ToString();
                r.Issued = DateTime.UtcNow;

                string message = CollectionsTaskHelper.DatabaseTaskToMessage(r);

                StorageClientHelper.AddQueueMessage(QueueType.Collections, SourceType.Twitter, message);
                tasksIssued++;

                string task = string.Format("{0} {1} {2} {3} {4} {5} {6} ",
                    r.Id.ToString(), r.State, r.Created.ToString(), r.Project, r.Source, r.Command, r.Target);

                Console.WriteLine(task);
            }

            //Submit Changes
            context.SubmitChanges();

            return tasksIssued;
        }
    }
}