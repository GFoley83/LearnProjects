using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Haystack
{
    internal static class CollectionsTaskHelper
    {
        #region Table, Q, Task
        //Table to Q
        internal static string T_CollectionsTaskToMessage(T_CollectionTask t_task)
        {
            CollectionsTask task = new CollectionsTask();
            task.Id = t_task.Id.ToString();
            task.State = t_task.State;
            task.Created = t_task.Created.ToUniversalTime().ToString();
            task.Project = t_task.Project;
            task.Source = t_task.Source;

            task.Command = t_task.Command;
            task.Target = t_task.Target;
            task.Parameters = t_task.Parameters;

            task.Issued = t_task.Issued.GetValueOrDefault().ToString() ?? "";

            task.Collected = t_task.Collected.GetValueOrDefault().ToString() ?? "";
            task.Station = t_task.Station;
            task.Collector = t_task.Collector;

            task.ResultHash = t_task.ResultHash;
            task.BlobName = t_task.BlobName;
            task.Processed = t_task.Processed.GetValueOrDefault().ToString() ?? "";

            string message = CollectionsTaskToMessage(task);

            return message;
        }

        //Q to Task
        internal static CollectionsTask MessageToCollectionsTask(string message)
        {
            CollectionsTask task = new CollectionsTask();
            string[] fields = message.Split('\t');

            task.Id = fields[0];
            task.State = fields[1];
            task.Created = fields[2];
            task.Project = fields[3];
            task.Source = fields[4];
            
            task.Command = fields[5];
            task.Target = fields[6];
            task.Parameters = fields[7];
            
            task.Issued = fields[8];

            task.Collected = fields[9];
            task.Station = fields[10];
            task.Collector = fields[11];
            
            task.ResultHash = fields[12];
            task.BlobName = fields[13];
            task.Processed = fields[14];

            return task;
        }

        //Task to Q
        internal static string CollectionsTaskToMessage(CollectionsTask task)
        {
            string message = string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t{13}\t{14}",
                task.Id,
                task.State,
                task.Created,
                task.Project,
                task.Source,

                task.Command,
                task.Target,
                task.Parameters,

                task.Issued,

                task.Collected,
                task.Station,
                task.Collector,

                task.ResultHash,
                task.BlobName,
                task.Processed);

            return message;
        }

        //Q to Table
        internal static T_CollectionTask MessageToT_CollectionTask(string message)
        {
            CollectionsTask task = MessageToCollectionsTask(message);

            T_CollectionTask t_task = new T_CollectionTask();
            t_task.Id = int.Parse(task.Id);
            t_task.State = task.State;
            t_task.Created = DateTime.Parse(task.Created);
            t_task.Project = task.Project;
            t_task.Source = task.Source;

            t_task.Command = task.Command;
            t_task.Target = task.Target;
            t_task.Parameters = task.Parameters;

            t_task.Issued = DateTime.Parse(task.Issued);

            t_task.Collected = DateTime.Parse(task.Collected);
            t_task.Station = task.Station;
            t_task.Collector = task.Collector;

            t_task.ResultHash = task.ResultHash;
            t_task.BlobName = task.BlobName;
            t_task.Processed = DateTime.Parse(task.Processed);
            
            return t_task;
        }
        #endregion

        #region Misc
        internal static string CalculateMD5Hash(string content)
        {
            if (String.IsNullOrEmpty(content))
                return "";

            MD5 md5 = MD5.Create();
            byte[] b = System.Text.Encoding.UTF8.GetBytes(content);
            Byte[] md5Hash = md5.ComputeHash(b);
            String base64EncodedMD5Hash =
                Convert.ToBase64String(md5Hash);
            return base64EncodedMD5Hash;
        }

        internal static string Now()
        {
            return DateTime.UtcNow.ToString();
        }
        #endregion
    }
}
