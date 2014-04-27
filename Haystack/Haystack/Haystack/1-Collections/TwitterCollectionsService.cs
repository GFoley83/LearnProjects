using System;
using System.Collections.Generic;
using TweetSharp;

namespace Haystack
{
    public class TwitterSettings
    {
        public string ConsumerKey;
        public string ConsumerSecret;
        public string AccessToken;
        public string AccessTokenSecret;
    }

    public class TamuResearch1 : TwitterSettings
    {
        public TamuResearch1()
        {
            ConsumerKey = "kiZDPIogaFTod4ZjE3GHYg";
            ConsumerSecret = "btjJVW1gVMa9m3nvlwvMW9aSHCd57NKEVPON9CeuVI";
            AccessToken = "1005154800-irzAcP4lNOH098FHFdKbtPd9vD58Cbt7nOH454h";
            AccessTokenSecret = "07OZKfji6kqgJwkDZIprwi19Q12lATKUYaMTtrDEE";
        }
    }

    public class TwitterCollectionsService
    {
        string _consumerKey;
        string _consumerSecret;
        string _accessToken;
        string _accessTokenSecret;
        
        public TwitterService twitterService;

        public TwitterCollectionsService(string station, string officer)
        {

            TwitterSettings settings = new TwitterSettings();

            _consumerKey = settings.ConsumerKey;
            _consumerSecret = settings.ConsumerSecret;
            _accessToken = settings.AccessToken;
            _accessTokenSecret = settings.AccessTokenSecret;


//#if DEBUG
//            _consumerKey = "tCb0reKFcZuPQxlhF11Ww";
//            _consumerSecret = "3TUwcmJFSxgPr3jaeeLUih1Q9q89AkHXmBVOU630yCY";
//            _accessToken = "1232505830-e3U6MwC2rz98IfQzsvla6sjbm4ockqBYj5LrT2B";
//            _accessTokenSecret = "E4b89w0nSf3QhDgcJDfM9d3JcpGAX1IK7L5DstPIc";
//#else
//            _consumerKey = "6l2m5ZfcL0pfZSG0p9XYIA";
//            _consumerSecret = "Mck0q007Wiu1Ntw1iYL8Z9TbFlGpdOOv64j3zgmqhM4";
//            _accessToken = "1232505830-voy7IOYzu6zcUuNCS1Kb1BgJUam5UMN5xG0UzKj";
//            _accessTokenSecret = "6CtZNojkOzOroQiELxs9l9yrqwsPTTKL8WKRW5Vp5c";
//#endif


            twitterService = new TwitterService(_consumerKey, _consumerSecret);
            twitterService.AuthenticateWith(_accessToken, _accessTokenSecret);
        }

        //1-Friends, 1-Sites\CollectionsStation
        public TwitterCursorList<long> GetTwitterFriends(string screenName, long cursor)
        {
            ListFriendIdsOfOptions options = new ListFriendIdsOfOptions();
            options.Count = 5000;               //Twitter Max
            options.Cursor = cursor;
            options.ScreenName = screenName;

            TwitterCursorList<long> friends ;
            try
            {
                friends = twitterService.ListFriendIdsOf(options);
            }
            catch (Exception ex)
            {
                return null;                
            }

            return friends;
        }

        //2-Followers, 5000 Per Batch
        public TwitterCursorList<long> GetTwitterFollowers(string screenName, long cursor)
        {
            ListFollowerIdsOfOptions options = new ListFollowerIdsOfOptions();
            options.Count = 5000;               //Twitter Max
            options.Cursor = cursor;
            options.ScreenName = screenName;

            TwitterCursorList<long> followers;

            try
            {
                followers = twitterService.ListFollowerIdsOf(options);
            }
            catch (Exception)
            {
                return null;
            }

            return followers;
        }

        //3-Tweets, 3200 Hard History Limit, 200 Per Batch
        public IEnumerable<TwitterStatus> GetTwitterTweets(string screenName, long maxId)
        {
            ListTweetsOnUserTimelineOptions options = new ListTweetsOnUserTimelineOptions();
            options.Count = 200;              //Twitter Max
            if (maxId > 0)
                options.MaxId = maxId;
            options.ScreenName = screenName;
            options.IncludeRts = true;

            IEnumerable<TwitterStatus> tweets = twitterService.ListTweetsOnUserTimeline(options);
            
            return tweets;
        }

        //4-Profile
        public TwitterUser GetTwitterProfile(string screenName)
        {
            GetUserProfileForOptions options = new GetUserProfileForOptions();
            options.ScreenName = screenName;

            TwitterUser user = twitterService.GetUserProfileFor(options);

            return user;
        }

        //5-Search
        public void GetTwitterSearch()
        {
            new NotImplementedException();
        }
    }
}
