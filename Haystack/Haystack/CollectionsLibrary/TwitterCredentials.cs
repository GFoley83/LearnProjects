using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsLibrary
{
    class TwitterCredentials
    {
        private string _CollectionStation;

        public TwitterCredentials(string CollectionStation)
        {
            _CollectionStation = CollectionStation;

            //TODO: Load Twitter Credentionals
        }

        string _consumerKey = "tCb0reKFcZuPQxlhF11Ww";
        string _consumerSecret = "3TUwcmJFSxgPr3jaeeLUih1Q9q89AkHXmBVOU630yCY";
        string _accessTokenKey = "1232505830-e3U6MwC2rz98IfQzsvla6sjbm4ockqBYj5LrT2B";
        string _accessTokenSecret = "E4b89w0nSf3QhDgcJDfM9d3JcpGAX1IK7L5DstPIc";

        public string AccessTokenKey
        {
            get
            {
                return _accessTokenKey;
            }

            private set { }
        }

        public string AccessTokenSecret
        {
            get
            {
                return _accessTokenSecret;
            }

            private set { }
        }
    }
}
