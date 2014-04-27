using Microsoft.WindowsAzure.StorageClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TweetSharp;


namespace Haystack
{
    public class ProcessingService
    {
        public void ProccesCollectionTasks()
        {
            bool loop = true;

            while (loop)
            {
                IEnumerable<CloudQueueMessage> queueMessages = AzureClientService.GetQueueMessages(QueueTypeEnum.ProcessingTwitter, 32);

                int messageCount = queueMessages.Count<CloudQueueMessage>();

                if (messageCount < 1)
                {
                    Console.WriteLine("No Messages");
                    loop = false;
                }
                else
                {
                    Console.WriteLine("GetQueueMessage = " + queueMessages.Count<CloudQueueMessage>());

                    foreach (CloudQueueMessage queueMessage in queueMessages)
                    {
                        //1. Get Queue
                        T_CollectionTask t_newTask = CollectionsTaskHelper.MessageToT_CollectionTask(queueMessage.AsString);
                        //string taskString = CollectionsTaskHelper.T_CollectionsTaskToMessage(newTask);
                        //Console.WriteLine(taskString);

                        //2. Get Blob
                        CloudBlob blob = AzureClientService.GetBlobReference(t_newTask.Project, t_newTask.BlobName);
                        string blobText = blob.DownloadText();
                        //TODO: Blob Validate Hash
                        //Console.WriteLine(blobText);

                        //3. Update Database: Results
                        if (t_newTask.Command == TwitterCommandEnum.Followers.ToString())
                            ProcessTwitterFollowers(blobText, t_newTask);
                        else if (t_newTask.Command == TwitterCommandEnum.Friends.ToString())
                            ProcessTwitterFriends(blobText, t_newTask);
                        else if (t_newTask.Command == TwitterCommandEnum.Profile.ToString())
                            ProcessTwitterProfile(blobText, t_newTask);
                        else if (t_newTask.Command == TwitterCommandEnum.Tweets.ToString())
                            ProcessTwitterTweets(blobText, t_newTask);
                        else if (t_newTask.Command == TwitterCommandEnum.Search.ToString())
                            ProcessTwitterSearch(blobText, t_newTask);
                        else
                            throw new NotImplementedException();

                        //4. Update Database: Tasks
                        LinqToSqlAzureHaystackDataContext context = new LinqToSqlAzureHaystackDataContext();
                        T_CollectionTask t_oldTask = (from t in context.T_CollectionTasks
                                                  where t.Id == t_newTask.Id
                                                  orderby t.Id descending
                                                      select t).First<T_CollectionTask>();

                        t_oldTask.State = StateTypeEnum.Processed.ToString();
                        t_oldTask.Collected = t_newTask.Collected;
                        t_oldTask.Station = t_newTask.Station;
                        t_oldTask.Collector = t_newTask.Collector;
                        t_oldTask.BlobName = t_newTask.BlobName;
                        t_oldTask.ResultHash = blob.Attributes.Properties.ContentMD5;
                        t_oldTask.Processed = DateTime.UtcNow;
                        context.SubmitChanges();

                        //5. Update Blob Metadata
                        blob.Attributes.Metadata["State"] = StateTypeEnum.Processed.ToString();

                        //6. Delete Queue
                        AzureClientService.DeleteQueueMessage(QueueTypeEnum.ProcessingTwitter, queueMessage);

                        //7. Print Success
                        string output = CollectionsTaskHelper.T_CollectionsTaskToMessage(t_newTask);
                        Console.WriteLine(output);
                    }
                }
            }
        }

        private void ProcessTwitterFollowers(string blobText, T_CollectionTask t_newTask)
        {
            //1-Deserialize
            List<long> followers = JsonConvert.DeserializeObject<List<long>>(blobText);
            List<T_TwitterFollower> t_newFollowers = new List<T_TwitterFollower>();
            foreach (long follower in followers)
            {
                T_TwitterFollower t_newFollower = new T_TwitterFollower();
                t_newFollower.CollectionTaskId = t_newTask.Id;
                t_newFollower.TargetUserName = t_newTask.Target;
                t_newFollower.SourceId = follower;
                t_newFollower.Date = DateTime.UtcNow;
                t_newFollowers.Add(t_newFollower);
            }
            
            //2-Validate
            //TODO

            //3-Update
            LinqToSqlAzureHaystackDataContext context = new LinqToSqlAzureHaystackDataContext();
            int counter = 0;
            foreach (T_TwitterFollower t_NewFollower in t_newFollowers)
            {
                context.T_TwitterFollowers.InsertOnSubmit(t_NewFollower);
                counter++;
                if (counter > 1000)
                    context.SubmitChanges();
            }
            context.SubmitChanges();
        }

        private void ProcessTwitterFriends(string blobText, T_CollectionTask t_newTask)
        {
            //1-Deserialize
            List<long> friends = JsonConvert.DeserializeObject<List<long>>(blobText);
            List<T_TwitterFriend> t_newFriends = new List<T_TwitterFriend>();
            foreach (long friend in friends)
            {
                T_TwitterFriend t_newFriend = new T_TwitterFriend();
                t_newFriend.CollectionTaskId = t_newTask.Id;
                t_newFriend.SourceUserName = t_newTask.Target;
                t_newFriend.TargetId = friend;
                t_newFriend.Date = DateTime.UtcNow;
                t_newFriends.Add(t_newFriend);
            }

            //2-Validate
            //TODO

            //3-Update
            LinqToSqlAzureHaystackDataContext context = new LinqToSqlAzureHaystackDataContext();
            foreach (T_TwitterFriend t_NewFriend in t_newFriends)
            {
                context.T_TwitterFriends.InsertOnSubmit(t_NewFriend);
            }
            context.SubmitChanges();
        }

        private void ProcessTwitterProfile(string blobText, T_CollectionTask t_newTask)
        {
            //1-Deserialize
            TwitterUser user = JsonConvert.DeserializeObject<TwitterUser>(blobText);

            T_TwitterUser t_user = new T_TwitterUser();
            t_user.CollectionTaskId = t_newTask.Id;
            t_user.CreatedDate = user.CreatedDate;
            t_user.Description = user.Description;
            t_user.FavoritesCount = user.FavouritesCount;
            t_user.FollowersCount = user.FollowersCount;
            t_user.FriendsCount = user.FriendsCount;
            t_user.IsContributorsEnabled = user.ContributorsEnabled;
            t_user.IsGeoEnabled = user.IsGeoEnabled;
            t_user.IsProtected = user.IsProtected;
            t_user.IsVerified = user.IsVerified;
            t_user.Language = user.Language;
            t_user.ListedCount = user.ListedCount;
            t_user.Location = user.Location;
            t_user.Name = user.Name;
            t_user.ProfileBackgroundImageUrl = user.ProfileBackgroundImageUrl;
            t_user.ProfileImageUrl = user.ProfileImageUrl;
            t_user.ScreenName = user.ScreenName;
            t_user.StatusesCount = user.StatusesCount;
            t_user.TimeZone = user.TimeZone;
            t_user.Url = user.Url;
            t_user.UserId = user.Id;
            t_user.UtcOffset = user.UtcOffset;

            //2-Validate


            //3-Update
            LinqToSqlAzureHaystackDataContext context = new LinqToSqlAzureHaystackDataContext();
            context.T_TwitterUsers.InsertOnSubmit(t_user);
            context.SubmitChanges();
        }

        private void ProcessTwitterTweets(string blobText, T_CollectionTask t_newTask)
        {
            //1-Deserialize
            List<T_TwitterTweet> t_tweets = new List<T_TwitterTweet>();
            List<T_TwitterGeoLocation> t_geoLocations = new List<T_TwitterGeoLocation>();
            List<T_TwitterPlace> t_places = new List<T_TwitterPlace>();
            List<T_TwitterHashTag> t_hashTags = new List<T_TwitterHashTag>();
            List<T_TwitterMedia> t_medias = new List<T_TwitterMedia>();
            List<T_TwitterMention> t_mentions = new List<T_TwitterMention>();
            List<T_TwitterUrl> t_urls = new List<T_TwitterUrl>();
            
            List<TwitterStatus> tweets = JsonConvert.DeserializeObject<List<TwitterStatus>>(blobText);

            #region Tweets
            foreach (TwitterStatus tweet in tweets)
            {
                T_TwitterTweet t_tweet = new T_TwitterTweet();
                t_tweet.CollectionTaskId = t_newTask.Id;
                t_tweet.CreatedDate = tweet.CreatedDate;
                t_tweet.InReplayToTweetId = tweet.InReplyToStatusId;
                t_tweet.InReplayToUserId = tweet.InReplyToUserId;
                t_tweet.InReplyToScreenName = tweet.InReplyToScreenName;
                t_tweet.IsFavorited = tweet.IsFavorited;
                t_tweet.IsPossiblySensitive = tweet.IsPossiblySensitive;
                t_tweet.IsTruncated = tweet.IsTruncated;
                t_tweet.RetweetCount = tweet.RetweetCount;
                t_tweet.Source = tweet.Source;
                t_tweet.Text = tweet.Text;
                t_tweet.TweeterId = tweet.User.Id;
                t_tweet.TweeterScreenName = tweet.User.ScreenName;
                t_tweet.TweetId = tweet.Id;
                
                t_tweets.Add(t_tweet);

                if (tweet.Location != null)
                {
                    T_TwitterGeoLocation t_geoLocation = new T_TwitterGeoLocation();
                    t_geoLocation.CollectionTaskId = t_newTask.Id;
                    t_geoLocation.TweetId = t_tweet.TweetId;
                    t_geoLocation.TweeterUserId = t_tweet.TweeterId;
                   
                    t_geoLocation.Longitude = Math.Round(Convert.ToDecimal(tweet.Location.Coordinates.Longitude), 6);
                    t_geoLocation.Latitude = Math.Round(Convert.ToDecimal(tweet.Location.Coordinates.Latitude), 6);

                    t_geoLocations.Add(t_geoLocation);
                }

                if (tweet.Place != null)
                {
                    T_TwitterPlace t_place = new T_TwitterPlace();
                    t_place.CollectionTaskId = t_newTask.Id;
                    t_place.TweetId = t_tweet.TweetId;
                    t_place.TweeterUserId = t_tweet.TweeterId;

                    t_place.Country = tweet.Place.Country;
                    t_place.CountryCode = tweet.Place.CountryCode;
                    t_place.FullName = tweet.Place.FullName;
                    t_place.Name = tweet.Place.Name;
                    t_place.PlaceId = tweet.Place.Id;
                    t_place.PlaceType = tweet.Place.PlaceType.ToString();
                    t_place.Url = tweet.Place.Url;

                    t_places.Add(t_place);
                }

                if (tweet.Entities != null)
                {
                    if (tweet.Entities.Count<TwitterEntity>() > 0)
                    {
                        foreach (TwitterEntity entity in tweet.Entities)
                        {
                            switch (entity.EntityType)
                            {
                                case TwitterEntityType.HashTag:
                                    TwitterHashTag hashTag = (TwitterHashTag)entity;
                                    T_TwitterHashTag t_hashTag = new T_TwitterHashTag();
                                    t_hashTag.CollectionTaskId = t_newTask.Id;
                                    t_hashTag.TweetId = t_tweet.TweetId;
                                    t_hashTag.TweeterUserId = t_tweet.TweeterId;
                                    
                                    t_hashTag.Text = hashTag.Text;
                                    
                                    t_hashTags.Add(t_hashTag);
                                    break;
                                case TwitterEntityType.Media:
                                    TwitterMedia media = (TwitterMedia)entity;
                                    T_TwitterMedia t_media = new T_TwitterMedia();
                                    t_media.CollectionTaskId = t_newTask.Id;
                                    t_media.TweetId = t_tweet.TweetId;
                                    t_media.TweeterUserId = t_tweet.TweeterId;

                                    t_media.DisplayUrl = media.DisplayUrl;
                                    t_media.ExpandedUrl = media.ExpandedUrl;
                                    t_media.MediaId = media.Id;
                                    t_media.MediaType = media.MediaType.ToString();
                                    t_media.MediaUrl = media.MediaUrl;
                                    t_media.Url = media.Url;
                                    
                                    t_medias.Add(t_media);
                                    break;
                                case TwitterEntityType.Mention:
                                    TwitterMention mention = (TwitterMention)entity;
                                    T_TwitterMention t_mention = new T_TwitterMention();
                                    t_mention.CollectionTaskId = t_newTask.Id;
                                    t_mention.TweetId = t_tweet.TweetId;
                                    t_mention.TweeterUserId = t_tweet.TweeterId;

                                    t_mention.MentionId = mention.Id;
                                    t_mention.Name = mention.Name;
                                    t_mention.ScreenName = mention.ScreenName;

                                    t_mentions.Add(t_mention);
                                    break;
                                case TwitterEntityType.Url:
                                    TwitterUrl url = (TwitterUrl)entity;
                                    T_TwitterUrl t_url = new T_TwitterUrl();
                                    t_url.CollectionTaskId = t_newTask.Id;
                                    t_url.TweetId = t_tweet.TweetId;
                                    t_url.TweeterUserId = t_tweet.TweeterId;

                                    t_url.ExpandedValue = url.ExpandedValue;
                                    t_url.Value = url.Value;

                                    t_urls.Add(t_url);
                                    break;
                                default:
                                    throw new NotImplementedException();
                            }
                        }
                    }

                }
            }
            #endregion

            //2-Validate

            //3-Update
            LinqToSqlAzureHaystackDataContext context = new LinqToSqlAzureHaystackDataContext();
            context.T_TwitterTweets.InsertAllOnSubmit<T_TwitterTweet>(t_tweets);

            context.T_TwitterGeoLocations.InsertAllOnSubmit<T_TwitterGeoLocation>(t_geoLocations);

            context.T_TwitterPlaces.InsertAllOnSubmit<T_TwitterPlace>(t_places);

            context.T_TwitterHashTags.InsertAllOnSubmit<T_TwitterHashTag>(t_hashTags);

            context.T_TwitterMedias.InsertAllOnSubmit<T_TwitterMedia>(t_medias);

            context.T_TwitterMentions.InsertAllOnSubmit<T_TwitterMention>(t_mentions);

            context.T_TwitterUrls.InsertAllOnSubmit<T_TwitterUrl>(t_urls);
            context.SubmitChanges();
        }

        private void ProcessTwitterSearch(string blobText, T_CollectionTask t_newTask)
        {
            throw new NotImplementedException();
        }

        public void PopulateTwitterFollowers()
        {
            LinqToSqlAzureHaystackDataContext context = new LinqToSqlAzureHaystackDataContext();

            IQueryable<T_TwitterUser> users =
                        from r in context.T_TwitterUsers
                        select r;

            IQueryable<T_TwitterFollower> followers =
                        from r in context.T_TwitterFollowers
                        where r.Complete == null
                        select r;

            int counter = 0;
            foreach (T_TwitterFollower follower in followers)
            {
                T_TwitterUser targetUser = users.FirstOrDefault<T_TwitterUser>(user => user.ScreenName == follower.TargetUserName);
                if (targetUser != null)
                {
                    if (follower.TargetId != targetUser.UserId)
                        follower.TargetId = targetUser.UserId;
                }
                T_TwitterUser sourceUser = users.FirstOrDefault<T_TwitterUser>(user => user.UserId == follower.SourceId);
                if (sourceUser != null)
                {
                    follower.SourceUserName = sourceUser.ScreenName;
                    follower.Complete = true;
                }

                counter++;
                if (counter > 100)
                {
                    context.SubmitChanges();
                    counter = 0;
                }
            }
            context.SubmitChanges();


            IQueryable<T_TwitterFriend> friends =
                        from r in context.T_TwitterFriends
                        where r.Complete == null
                        select r;

            counter = 0;
            foreach (T_TwitterFriend friend in friends)
            {
                T_TwitterUser sourceUser = users.FirstOrDefault<T_TwitterUser>(user => user.ScreenName == friend.SourceUserName);
                if (sourceUser != null)
                {
                    if (friend.SourceId != sourceUser.UserId)
                        friend.SourceId = sourceUser.UserId;
                }

                T_TwitterUser targetUser = users.FirstOrDefault<T_TwitterUser>(user => user.UserId == friend.TargetId);
                if (targetUser != null)
                {
                    friend.TargetUserName = targetUser.ScreenName;
                    friend.Complete = true;
                }

                counter++;
                if (counter > 100)
                {
                    context.SubmitChanges();
                    counter = 0;
                }
            }
            context.SubmitChanges();
        }
   
    }
}
