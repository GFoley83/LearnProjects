using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haystack
{
    public abstract class AbstractCollectionsAgent
    {
        protected string _station;
        protected string _collector;

        public AbstractCollectionsAgent(string station, string collector)
        {
            _station = station;
            _collector = collector;
        }

        public abstract string RunCommand(string target, string parameters);
    }
}
