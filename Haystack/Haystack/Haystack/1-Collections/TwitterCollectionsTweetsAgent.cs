using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TweetSharp;

namespace Haystack
{
    class TwitterCollectionsTweetsAgent : TwitterCollectionsAgent
    {
        public TwitterCollectionsTweetsAgent(string station, string collector)
            : base(station, collector)
        {
            _station = station;
            _collector = collector;
            base._twitterCommand = TwitterCommandEnum.Tweets;
        }

        protected override object ExecuteCommand(string target, string parameters)
        {
            Logger.Log(GetContext() + "ExecuteCommand");

            List<TwitterStatus> tweets = new List<TwitterStatus>(); //tweets
            TwitterCollectionsService service = new TwitterCollectionsService(_station, _collector);

            long maxId;
            if (String.IsNullOrEmpty(parameters))
                maxId = 0;
            else
                maxId = long.Parse(parameters);

            bool loop = true;

            do
            {
                IEnumerable<TwitterStatus> cursorList = service.GetTwitterTweets(target, maxId); //tweets
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
                    //Done?
                    if (cursorList.Count<TwitterStatus>() > 0)
                    {
                        //Sync Result
                        foreach (TwitterStatus tweet in cursorList)
                            tweets.Add(tweet);
                        Logger.Log(GetContext() + "tweets.Count = " + tweets.Count.ToString());

                        maxId = tweets.Last<TwitterStatus>().Id - 1;
                    }
                    else
                        loop = false;

                    if (service.twitterService.Response.RateLimitStatus.RemainingHits == 0)
                    {
                        Logger.Log(GetContext() + "Sleep Thread, RemainingHits = 0");
                        Thread.Sleep(_sleepTime); //Sleep Thread
                    }
                }
            } while (loop);

            return tweets;
        }
    }
}