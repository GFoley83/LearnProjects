using Microsoft.WindowsAzure.StorageClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProcessingSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Haystack.ProcessingSimulator.PrintQueues();
            //Haystack.ProcessingSimulator.Display();
            //Haystack.ProcessingSimulator.Delete();

            Haystack.ProcessingService pservice = new Haystack.ProcessingService();
            pservice.ProccesCollectionTasks();
            pservice.PopulateTwitterFollowers();

            Console.ReadKey();
        }
    }
}
