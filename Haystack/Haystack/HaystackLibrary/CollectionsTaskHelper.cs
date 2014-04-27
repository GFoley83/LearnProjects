using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TweetSharp;

namespace HaystackLibrary
{
    public static class CollectionsTaskHelper
    {
        internal static CollectionsTaskLocal MessageToLocalTask(string message)
        {
            string[] fields = message.Split('\t');

            CollectionsTaskLocal task = new CollectionsTaskLocal();
            task.Id = fields[0];
            task.Created = fields[1];
            task.Project = fields[2];
            task.Source = fields[3];
            task.State = fields[4];

            task.Issued = fields[5];
            task.Command = fields[6];
            task.Target = fields[7];

            task.Collected = fields[8];
            task.CollectionsStation = fields[9];
            task.CollectorInstance = fields[10];

            task.BlobName = fields[11];
            task.Processed = fields[12];

            return task;
        }

        internal static string LocalTaskToMessage(CollectionsTaskLocal task)
        {
            string message = string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}",
                task.Id,
                task.Created,
                task.Project,
                task.Source,
                task.State,

                task.Issued,
                task.Command,
                task.Target,

                task.Collected,
                task.CollectionsStation,
                task.CollectorInstance,

                task.BlobName,
                task.Processed);

            return message;
        }

        public static string DatabaseTaskToMessage(CollectionTask task)
        {
            string message = string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}",
                task.Id,
                task.Created,
                task.Project,
                task.Source,
                task.State,

                task.Issued,
                task.Command,
                task.Target,

                task.Collected,
                task.Station,
                task.Collector,

                task.BlobName,
                task.Processed);

            return message;
        }

        public static CollectionTask MessageToDatabaseTask(string message)
        {
            string[] fields = message.Split('\t');

            CollectionTask task = new CollectionTask();

            task.Id = int.Parse(fields[0]);
            task.Created = DateTime.Parse(fields[1]);
            task.Project = fields[2];
            task.Source = fields[3];
            task.State = fields[4];

            task.Issued = DateTime.Parse(fields[5]);
            task.Command = fields[6];
            task.Target = fields[7];

            task.Collected = DateTime.Parse(fields[8]);
            task.Station = fields[9];
            task.Collector = fields[10];

            task.BlobName = fields[11];
            task.Processed = null;

            return task;
        }

        public static string DatabaseTaskString(CollectionTask task)
        {
            string taskString = string.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10} {11}",
                            task.Id.ToString(), task.State, task.Created.ToString(), task.Project, task.Source, task.Command, task.Target,
                            task.Collected.ToString() ?? "", task.Station ?? "", task.BlobName ?? "", task.BlobHash ?? "", task.Processed.ToString() ?? "");

            return taskString;
        }

        public static string CalculateMD5Hash(string content)
        {
            content = content ?? "";
            MD5 md5 = MD5.Create();
            byte[] b = System.Text.Encoding.UTF8.GetBytes(content);
            Byte[] md5Hash = md5.ComputeHash(b);
            String base64EncodedMD5Hash =
                Convert.ToBase64String(md5Hash);
            return base64EncodedMD5Hash;
        }

        public static string Now()
        {
            return DateTime.UtcNow.ToString();
        }


        #region Json Helper
        public static string ObjectToJson(object obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            return json;
        }

        //private static object DeseralizeObject(string json)
        //{
        //    var obj = JsonConvert.DeserializeObject<object>(json);
        //    return obj;
        //}

        //Profile
        public static TwitterUser JstonToTwitterUser(string json)
        {
            return JsonConvert.DeserializeObject<TwitterUser>(json);
        }


        //Twitter Status
        public static IEnumerable<TwitterStatus> JstonToTwitterStatus(string json)
        {
            return JsonConvert.DeserializeObject<IEnumerable<TwitterStatus>>(json);
        }

        //Followers
        public static IEnumerable<int> JsonToFriends(string json)
        {
            return JsonConvert.DeserializeObject<IEnumerable<int>>(json);
        }

        //Following
        public static IEnumerable<int> JsonToFollowing(string json)
        {
            return JsonConvert.DeserializeObject<IEnumerable<int>>(json);
        }
        #endregion
    }
}
