using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TweetSharp;

namespace Haystack
{
    class TwitterCollectionsProfileAgent : TwitterCollectionsAgent
    {
        public TwitterCollectionsProfileAgent(string station, string collector)
            : base(station, collector)
        {
            _station = station;
            _collector = collector;
            base._twitterCommand = TwitterCommandEnum.Profile;
        }

        protected override object ExecuteCommand(string target, string parameters)
        {
            Logger.Log(GetContext() + "ExecuteCommand");

            TwitterUser user;
            TwitterCollectionsService service = new TwitterCollectionsService(_station, _collector);

            bool loop = true;

            do
            {
                user = service.GetTwitterProfile(target); //friends
                if (service.twitterService.Response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    if (service.twitterService.Response.Error.Message == "Rate limit exceeded")
                    {
                        Logger.Log(GetContext() + "Sleep Thread, First Hit Exception");
                        Thread.Sleep(_sleepTime); //Sleep Thread, First Hit Exception
                    }
                    else
                    {
                        string error = GetContext() + "RESPONSE ERROR: " + service.twitterService.Response.StatusCode.ToString();
                        Logger.Log(error);
                        throw new Exception(error);
                    }
                }
                else
                    loop = false;

            } while (loop);

            return user;
        }
    }
}
