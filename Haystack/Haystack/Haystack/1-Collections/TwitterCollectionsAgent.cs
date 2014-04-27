using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;
using Newtonsoft.Json;

namespace Haystack
{
    abstract class TwitterCollectionsAgent : AbstractCollectionsAgent
    {
        protected int _sleepTime = 900000;
        protected TwitterCommandEnum _twitterCommand;
        protected string _target;
        protected string _parameters;

        protected TwitterCollectionsAgent(string station, string collector) : base (station, collector)
        {
            _station = station;
            _collector = collector;
        }

        public override string RunCommand(string target, string parameters)
        {
            _target = target;
            _parameters = parameters;
            var obj = ExecuteCommand(target, parameters);
            string resultJson = JsonConvert.SerializeObject(obj);

            return resultJson;
        }

        protected abstract object ExecuteCommand(string target, string parameters);

        //public abstract long TargetId();

        //Used for Logging
        protected string GetContext()
        {
            string logPrefix = string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t", _station, _collector, _twitterCommand.ToString(), _target, _parameters);
            return logPrefix;
        }
    }
}
