using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Haystack
{
    public class CollectionsStation
    {
        List<CollectionsOfficer> officers;
        CollectionsOfficer followersOfficer;
        CollectionsOfficer friendsOfficer;
        CollectionsOfficer profileOfficer;
        CollectionsOfficer tweetsOfficer;

        string _station;
        string _collector;

        public CollectionsStation(string station)
        {
            _station = station;
            _collector = "Chief";

            officers = new List<CollectionsOfficer>();

            followersOfficer = new CollectionsOfficer(_station, _collector, QueueTypeEnum.CollectionsTwitterFollowers);
            officers.Add(followersOfficer);

            friendsOfficer = new CollectionsOfficer(_station, _collector, QueueTypeEnum.CollectionsTwitterFriends);
            officers.Add(friendsOfficer);

            profileOfficer = new CollectionsOfficer(_station, _collector, QueueTypeEnum.CollectionsTwitterProfile);
            officers.Add(profileOfficer);

            tweetsOfficer = new CollectionsOfficer(_station, _collector, QueueTypeEnum.CollectionsTwitterTweets);
            officers.Add(tweetsOfficer);
        }
        
        public void StartUp()
        {
            foreach (CollectionsOfficer officer in officers)
            {
                officer.Run();
            }
        }

        public void ShutDown()
        {
            foreach (CollectionsOfficer officer in officers)
            {
                officer.Stop();
            }
        }

        public void ExternalError()
        {
            Logger.Log("ExternalError");
            ShutDown();
        }
    }
}
