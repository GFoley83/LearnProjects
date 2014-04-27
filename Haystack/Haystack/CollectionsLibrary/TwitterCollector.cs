using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HaystackLibrary;
using CollectionsLibrary;
using TweetSharp;

namespace Haystack
{
    public class TwitterCollector : AbstractCollector
    {

        string _consumerKey;
        string _consumerSecret;
        string _accessToken;
        string _accessTokenSecret;

        string _command;
        string _targe;
        string _parameters;

        public TwitterCollector()
        {
            Logger.Log("TwitterCollector.TwitterCollector");
            base._sourceType = SourceType.Twitter; //Overwire SourceType

#if DEBUG
            base._collectionLoopInterval = 20000;
            Logger.Log("Use Debug Account");

            //Load Debug Twitter Credentionals
            _consumerKey = "tCb0reKFcZuPQxlhF11Ww";
            _consumerSecret = "3TUwcmJFSxgPr3jaeeLUih1Q9q89AkHXmBVOU630yCY";
            _accessToken = "1232505830-e3U6MwC2rz98IfQzsvla6sjbm4ockqBYj5LrT2B";
            _accessTokenSecret = "E4b89w0nSf3QhDgcJDfM9d3JcpGAX1IK7L5DstPIc";
#else
            base._collectionLoopInterval = 3600000;
            Logger.Log("Get Production Account");
            //TODO Load Production Twitter Credentionals by Context
#endif
            var service = new TwitterService(_consumerKey, _consumerSecret);
            service.AuthenticateWith(_accessToken, _accessTokenSecret);

        }

        protected override int GetCollectionCapacity()
        {
            Logger.Log("GetCollectionCapacity");
            
            //TwitterRateLimitStatus rate = service.GetRateLimitStatus();
            //todo

            return 62;
        }

        protected override void ExecuteCollectionTask()
        {
            Logger.Log("TwitterCollector.ExecuteCollectionTask");
            Logger.Log("Execute: " + base._currentTask.Id);

            string command = base._currentTask.Command;
            string target = base._currentTask.Target;





            //TODO: Twitter ExecuteCollectionTask
            _currentTask.ResultsXml = "Results of " + _currentTask.Id;
        }

        
        //Profile


        //public string GetUserProfile(string target)
        //{
        //    var service = new TwitterService(_consumerKey, _consumerSecret);
        //    service.AuthenticateWith(_accessToken, _accessTokenSecret);
        //    TwitterUser user = service.GetUserProfileFor(target);
        //    return CollectionsTaskHelper.ObjectToJson(user);
        //}


            ////Profile
            //TwitterUser user = service.GetUserProfileFor(_username);
            
            ////Tweets
            //IEnumerable<TwitterStatus> tweets = service.ListTweetsOnSpecifiedUserTimeline(_username);

            ////Friends
            //IEnumerable<int> friends = service.ListFriendIdsOf(_username, -1);

            ////Followers
            //IEnumerable<int> followers = service.ListFollowerIdsOf(_username, -1);




    }
}
