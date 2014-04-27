using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haystack
{
    public class TargetingService
    {
        public int IssueCollectionTasks()
        {
            int tasksIssued = 0;

            //Query Collection Tasks
            LinqToSqlAzureHaystackDataContext context = new LinqToSqlAzureHaystackDataContext();
            IQueryable<T_CollectionTask> results =
                        from r in context.T_CollectionTasks
                        where r.State == StateTypeEnum.Approved.ToString()
                        select r;

            //Write Queue Message, Update State to "Issued"
            foreach (T_CollectionTask r in results)
            {
                r.State = StateTypeEnum.Issued.ToString();
                r.Issued = DateTime.UtcNow;

                string message = CollectionsTaskHelper.T_CollectionsTaskToMessage(r);

                QueueTypeEnum queueName;

                if (r.Command == TwitterCommandEnum.Followers.ToString())
                    queueName = QueueTypeEnum.CollectionsTwitterFollowers;
                else if (r.Command == TwitterCommandEnum.Friends.ToString())
                    queueName = QueueTypeEnum.CollectionsTwitterFriends;
                else if (r.Command == TwitterCommandEnum.Profile.ToString())
                    queueName = QueueTypeEnum.CollectionsTwitterProfile;
                else if (r.Command == TwitterCommandEnum.Search.ToString())
                    queueName = QueueTypeEnum.CollectionsTwitterSearch;
                else if (r.Command == TwitterCommandEnum.Tweets.ToString())
                    queueName = QueueTypeEnum.CollectionsTwitterTweets;
                else
                    throw new Exception("Command Not Found");

                AzureClientService.AddQueueMessage(queueName, message);
                tasksIssued++;

                string task = string.Format("{0} {1} {2} {3} {4} {5} {6} ",
                    r.Id.ToString(), r.State, r.Created.ToString(), r.Project, r.Source, r.Command, r.Target);

                Console.WriteLine(task);
            }

            //Submit Changes
            context.SubmitChanges();

            return tasksIssued;
        }

        List<string> targets;
        public void CreateCollectionTasks()
        {
            targets = new List<string>();
            PopulateTarget();

            LinqToSqlAzureHaystackDataContext context = new LinqToSqlAzureHaystackDataContext();
            foreach (string target in targets)
            {
                //Followers
                T_CollectionTask followersTask = new T_CollectionTask();
                followersTask.State = StateTypeEnum.Approved.ToString();
                followersTask.Created = DateTime.UtcNow;
                followersTask.Project = "Haystack";
                followersTask.Source = SourceTypeEnum.Twitter.ToString();
                followersTask.Command = TwitterCommandEnum.Followers.ToString();
                followersTask.Target = target;
                context.T_CollectionTasks.InsertOnSubmit(followersTask);

                //Friends
                T_CollectionTask Friends = new T_CollectionTask();
                Friends.State = StateTypeEnum.Approved.ToString();
                Friends.Created = DateTime.UtcNow;
                Friends.Project = "Haystack";
                Friends.Source = SourceTypeEnum.Twitter.ToString();
                Friends.Command = TwitterCommandEnum.Friends.ToString();
                Friends.Target = target;
                context.T_CollectionTasks.InsertOnSubmit(Friends);


                //Profile
                T_CollectionTask Profile = new T_CollectionTask();
                Profile.State = StateTypeEnum.Approved.ToString();
                Profile.Created = DateTime.UtcNow;
                Profile.Project = "Haystack";
                Profile.Source = SourceTypeEnum.Twitter.ToString();
                Profile.Command = TwitterCommandEnum.Profile.ToString();
                Profile.Target = target;
                context.T_CollectionTasks.InsertOnSubmit(Profile);


                //Followers
                T_CollectionTask Tweets = new T_CollectionTask();
                Tweets.State = StateTypeEnum.Approved.ToString();
                Tweets.Created = DateTime.UtcNow;
                Tweets.Project = "Haystack";
                Tweets.Source = SourceTypeEnum.Twitter.ToString();
                Tweets.Command = TwitterCommandEnum.Tweets.ToString();
                Tweets.Target = target;
                context.T_CollectionTasks.InsertOnSubmit(Tweets);

            }
            context.SubmitChanges();
        }

        private void PopulateTarget()
        {
            targets.Add("hhassan140");
            targets.Add("Mohammad_syria");
            targets.Add("revolutionsyrian");
            targets.Add("malathaumran");
            targets.Add("shahed_3ayan");
            targets.Add("redman4u");
            targets.Add("nmoon5555");
            targets.Add("syrian_thinker");
            targets.Add("sweetsyrialeena");
            targets.Add("barazi_7urr");
            targets.Add("Mondassy");
            targets.Add("gdoumany");
            targets.Add("alaa_syr");
            targets.Add("mohammad_hallak");
            targets.Add("suhairatassi");
            targets.Add("obeidanahas");
            targets.Add("sa_council");
            targets.Add("hishammarwah");
            targets.Add("alaabday");
            targets.Add("syria_horra");
            targets.Add("fares_alhurrya");
            targets.Add("bintalsham");
            targets.Add("fsa_dam");
            targets.Add("yathalema");
            targets.Add("al_forqaan");
            targets.Add("alrifai1");
            targets.Add("alarabiya_sy");
            targets.Add("mgsa2006");
            targets.Add("hazemalarour");
            targets.Add("riyadhalasaad");
            targets.Add("kuwwithsyria");
            targets.Add("yaser0502");
            targets.Add("loveliberty");
            targets.Add("Da3m_mubasher");
            targets.Add("FSAunited");
            targets.Add("taimhawi");
            targets.Add("hadialabdallah");
            targets.Add("awadalqarni");
            targets.Add("abokazi");
            targets.Add("tareqalsuwaidan");
            targets.Add("salman_alodah");
            targets.Add("topsecret_ku");
            targets.Add("wikileaks_gcc");
            targets.Add("radwanziadeh");
            targets.Add("kasimf");
            targets.Add("katauibalwaleed");
            targets.Add("syrianSS");
            targets.Add("omawii");
            targets.Add("hossam_alghali1");
            targets.Add("syrianmediac");
            targets.Add("syrianfalcon11");
            targets.Add("turkialdakhil");
            targets.Add("sultanalqassemi");
            targets.Add("free_programmer");
            targets.Add("kasimf");
            targets.Add("abuhatem");
            targets.Add("mar15syria");
            targets.Add("AnonymousSyria");
            targets.Add("HamaEcho");
            targets.Add("Anasyria");
            targets.Add("ikhwanSyria");
            targets.Add("MohanaAlhubail");
            targets.Add("ZainSyr");
            targets.Add("syria_omar");
            targets.Add("LeShaque");
            targets.Add("edwardedark");
            targets.Add("LccSy");
            targets.Add("Samsomhoms");
            targets.Add("Sub7ei");
            targets.Add("RanaKabbani54");
            targets.Add("revolutionsyria");
            targets.Add("mjsamman");
            targets.Add("Syrianfortress");
            targets.Add("hussamov11");
            targets.Add("syrian_heart");
            targets.Add("ikwansyria");
            targets.Add("Sana_english");
            targets.Add("mohamad_alassad");
            targets.Add("ana_cherine");
            targets.Add("Bsyria");
            targets.Add("way2wonderland");
            targets.Add("Partisangirl");
            targets.Add("kim_tastiic");
            targets.Add("resistanceAxis");
            targets.Add("tha3pro_sea");
            targets.Add("fsa_war_crimes");
            targets.Add("lindasyria");
            targets.Add("syrian_media");
            targets.Add("marwaa410");
            targets.Add("mrdayvie");
            targets.Add("angryarabnews");
            targets.Add("nolibya4syria");
            targets.Add("jesuisseeba");
            targets.Add("sea_leaks");
            targets.Add("syrmukhabarat");
            targets.Add("ahmed_alasad91");
            targets.Add("myalterego1984");
            targets.Add("syria_newz");
            targets.Add("efwta");
            targets.Add("salamalasaad");
            targets.Add("syriancommando");
        }
    }
}
