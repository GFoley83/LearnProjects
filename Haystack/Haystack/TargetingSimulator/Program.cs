using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.StorageClient;
using System.Data.Linq;
using Haystack;

namespace TargetingSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            //Haystack.TargetingSimulator.ClearQueues();
            Haystack.TargetingSimulator.IssueCollectionTasks();
            //Haystack.TargetingSimulator.CreateCollectionTasks();
            Haystack.TargetingSimulator.PrintQueues();
            Console.ReadKey();
        }
    }
}
