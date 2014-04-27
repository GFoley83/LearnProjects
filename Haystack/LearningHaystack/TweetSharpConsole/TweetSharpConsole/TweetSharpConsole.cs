using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TweetSharp;
using TweetSharpConsole;
using Newtonsoft.Json;

namespace TweetSharpConsole
{
    class TweetSharpConsole
    {
        string _requestUrl = "https://api.twitter.com/1/";
        string _consumerKey = "tCb0reKFcZuPQxlhF11Ww";
        string _consumerSecret = "3TUwcmJFSxgPr3jaeeLUih1Q9q89AkHXmBVOU630yCY";
        string _accessToken = "1232505830-e3U6MwC2rz98IfQzsvla6sjbm4ockqBYj5LrT2B";
        string _accessTokenSecret = "E4b89w0nSf3QhDgcJDfM9d3JcpGAX1IK7L5DstPIc";
        //string _username = "HSDevPrototype";
        //string _username = "codenameduchess";
        //string _username = "aishatyler";
        //string _username = "Archer_Malory ";
        //string _username = "distastee";
        string _username = "KriegerSignals";

        string _password = "FWRpSAAlBXg2LesZ";

        //private int GetApiCapacity()
        //{
        //    //Authenication
        //    var service = new TwitterService(_consumerKey, _consumerSecret);
        //    service.AuthenticateWith(_accessToken, _accessTokenSecret);

        //    //Get Rate Limit
        //    TwitterRateLimitStatus rate = service.GetRateLimitStatus();
        //    Console.WriteLine("You have used " + rate.RemainingHits + " out of your " + rate.HourlyLimit);
            
        //    return rate.RemainingHits;
        //}

        private List<int> GetTwitterFriends()
        {
            //Authenication
            var service = new TwitterService(_consumerKey, _consumerSecret);
            service.AuthenticateWith(_accessToken, _accessTokenSecret);
            
            //TwitterCursorList<int> friends = service.ListFriendIdsOf(_username, -1);

            return null;
        }

        private List<long> GetTwitterFollowers()
        {
            var service = new TwitterService(_consumerKey, _consumerSecret);
            service.AuthenticateWith(_accessToken, _accessTokenSecret);
            
            //Set Rate Options
            GetRateLimitStatusOptions rateOptions = new GetRateLimitStatusOptions();
            List<string> checkResources = new List<string>();
            checkResources.Add("followers/ids");
            rateOptions.Resources = checkResources;

            //GetRateLimitStatus
            TwitterRateLimitStatusSummary summary = service.GetRateLimitStatus(rateOptions);
            
            List<long> followers = new List<long>();
            long cursor = -1;
            bool loop = true;

            do
            {
                //Base.CheckApiLimit()
                ListFollowerIdsOfOptions followersOptions = new ListFollowerIdsOfOptions();
                followersOptions.ScreenName = _username;
                followersOptions.Cursor = cursor;
                followersOptions.Count = 5000;

                TwitterCursorList<long> batchFollowers = service.ListFollowerIdsOf(followersOptions);
                
                Debug.WriteLine("Reset in " + ""+ " seconds.");

                if (batchFollowers.Count == 0)
                    return null;

                foreach (long l in batchFollowers)
                {
                    followers.Add(l);
                }

                cursor = batchFollowers.NextCursor ?? 0;

                if (cursor == 0)
                    loop = false;
                
            } while (loop);
            
            return followers;
        }

        //Tweets
        //Followers




        public void Run()
        {
            GetTwitterFollowers();

            ////Authenication
            //var service = new TwitterService(_consumerKey, _consumerSecret);
            //service.AuthenticateWith(_accessToken, _accessTokenSecret);

            ////Get Rate Limit
            //TwitterRateLimitStatus rate = service.GetRateLimitStatus();
            //Console.WriteLine("You have used " + rate.RemainingHits + " out of your " + rate.HourlyLimit);

            ////Profile
            //TwitterUser user = service.GetUserProfileFor(_username);
            
            ////Tweets
            //IEnumerable<TwitterStatus> tweets = service.ListTweetsOnSpecifiedUserTimeline(_username, 3200);

            ////Friends
            //IEnumerable<int> friends = service.ListFriendIdsOf(_username, -1);

            ////Followers
            //IEnumerable<int> followers = service.ListFollowerIdsOf(_username, -1);
            

            
            //Console.WriteLine("");

            //int friendsCount = friends.Count<int>();
            //Console.WriteLine("friendsCount = " + friendsCount);
            //int friendCurrent = 0;
            //int geoCount = 0;


            //foreach (int friend in friends)
            //{
            //    friendCurrent++;
            //    Console.WriteLine("friendCurrent = " + friendCurrent);
            //    IEnumerable<TwitterStatus> tweets = service.ListTweetsOnSpecifiedUserTimeline(friend);
            //    if (tweets != null)
            //    {
            //        foreach (TwitterStatus tweet in tweets)
            //        {
            //            if (tweet.Location != null)
            //            {
            //                geoCount++;
            //                Console.WriteLine(tweet.User.Name + ": " + tweet.Text);
            //                Console.WriteLine(tweet.Location.Coordinates.Latitude + " x " + tweet.Location.Coordinates.Longitude);
            //            }
            //        }
            //    }

            //    if (friendCurrent == 100)
            //        break;
            //}

            //Console.WriteLine(geoCount + " of " + friendCurrent);


            //Account
            //TwitterAccount a = new TwitterAccount();
            //TwitterResponse r = new TwitterResponse();
            //Action<TwitterAccount, TwitterResponse> ac= new Action<TwitterAccount, TwitterResponse>(new TwitterAccount(), new TwitterResponse());



            //IEnumerable<TwitterStatus> tweets = service.ListTweetsOnSpecifiedUserTimeline(_username);
            //foreach (TwitterStatus tweet in tweets)
            //{
            //    if (tweet.Location != null)
            //        Console.WriteLine(tweet.Location.Coordinates.Latitude + " x " + tweet.Location.Coordinates.Longitude);
            //}


            //string json = JsonConvert.SerializeObject(tweets);
            //IEnumerable<TwitterStatus> tweets2 = JsonConvert.DeserializeObject<IEnumerable<TwitterStatus>>(json);

            //foreach (TwitterStatus ts in tweets2)
            //{
            //    Console.WriteLine(ts.Text);
            //}
            
            //Console.WriteLine(json);
            //Console.ReadKey();



            //TwitterAccount account = service.GetAccountSettings();
            ////Console.WriteLine(account.AlwaysUseHttps.ToString());
            ////Console.WriteLine(account.DiscoverableByEmail.ToString());
            //Console.WriteLine("Geo: " + account.GeoEnabled.ToString());
            //Console.WriteLine(account.IsProtected.ToString());
            //Console.WriteLine("Language: " + account.Language.ToString());
            ////Console.WriteLine(account.RawSource.ToString());
            //Console.WriteLine("Username: " + account.ScreenName.ToString());
            ////Console.WriteLine(account.ShowAllInlineMedia.ToString());
            //if (account.TrendLocations != null )
            //    Console.WriteLine("TrendLocations: " + account.TrendLocations.First().CountryCode);


            //Console.WriteLine("" + account.TimeZone.UtcOffset);
            //Console.WriteLine(account.TrendLocations.First().CountryCode.ToString());




            //Friends
            //IEnumerable<int> friends = service.ListFriendIdsOf(_username, -1);
            //foreach (int friend in friends)
            //{
            //    Console.WriteLine(friend.ToString());
            //}


            //Followers
            //IEnumerable<int> followers = service.ListFollowerIdsOf(_username, -1);
            //foreach (int id in followers)
            //{
            //    Console.WriteLine(id.ToString());
            //}


            //Console.WriteLine(user.FollowersCount.ToString());

            //Profile


            //Tweets


           

        }
    }
}
