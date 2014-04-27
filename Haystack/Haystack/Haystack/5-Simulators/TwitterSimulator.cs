using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haystack
{
    public class TwitterSimulator
    {
        //string _screenName = "KriegerSignals";
        string _screenName = "distastee";
        //string _screenName = "codenameduchess";
        //string _screenName = "aishatyler";

        public void Run()
        {
            TwitterCollectionsFollowersAgent followersAgent = new TwitterCollectionsFollowersAgent("Debug", "Chief");
            TwitterCollectionsFriendsAgent friendsAgent = new TwitterCollectionsFriendsAgent("Debug", "Chief");
            TwitterCollectionsProfileAgent profileAgent = new TwitterCollectionsProfileAgent("Debug", "Chief");
            TwitterCollectionsTweetsAgent tweetsAgent = new TwitterCollectionsTweetsAgent("Debug", "Chief");

            string output = tweetsAgent.RunCommand(_screenName, "");
            Console.WriteLine(output);
        }

        public void ApproveNonFollowers()
        {
            LinqToSqlAzureHaystackDataContext context = new LinqToSqlAzureHaystackDataContext();

        }
    }
}
