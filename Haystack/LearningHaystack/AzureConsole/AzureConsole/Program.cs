using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            LearnAzureStorage las = new LearnAzureStorage();
            las.RunBlobs();
            las.RunQueues();
            las.RunTables();

            //LearnQueues lq = new LearnQueues();
            //lq.Run();
            Console.ReadKey();
        }
    }
}
