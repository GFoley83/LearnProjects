using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haystack
{
    class TwitterCollectionsSearchAgent : TwitterCollectionsAgent
    {
        public TwitterCollectionsSearchAgent(string station, string collector)
            : base(station, collector)
        {
            _station = station;
            _collector = collector;
            base._twitterCommand = TwitterCommandEnum.Search;
        }

        protected override object ExecuteCommand(string target, string parameters)
        {
            return null;
        }
    }
}

