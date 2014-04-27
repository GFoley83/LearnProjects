using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TweetSharp;

namespace Haystack
{
    class TwitterCollectionsFriendsAgent : TwitterCollectionsAgent
    {
        public TwitterCollectionsFriendsAgent(string station, string collector)
            : base(station, collector)
        {
            _station = station;
            _collector = collector;
            base._twitterCommand = TwitterCommandEnum.Friends;
        }

        protected override object ExecuteCommand(string target, string parameters)
        {
            Logger.Log(GetContext() + "ExecuteCommand");

            List<long> friends = new List<long>(); //friends
            TwitterCollectionsService service = new TwitterCollectionsService(_station, _collector);

            long cursor;
            if (String.IsNullOrEmpty(parameters))
                cursor = -1;
            else
                cursor = long.Parse(parameters);

            bool loop = true;

            do
            {
                TwitterCursorList<long> cursorList = service.GetTwitterFriends(target, cursor); //friends
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
                {
                    //Sync Result
                    foreach (long friend in cursorList)
                        friends.Add(friend);

                    //Done?
                    cursor = cursorList.NextCursor ?? 0;
                    if (cursor <= 0)
                        loop = false;

                    if (service.twitterService.Response.RateLimitStatus.RemainingHits == 0)
                    {
                        Logger.Log(GetContext() + "Sleep Thread, RemainingHits = 0");
                        Thread.Sleep(_sleepTime); //Sleep Thread
                    }
                }
            } while (loop);

            return friends;
        }
    }
}
