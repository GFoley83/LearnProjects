using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Client.EvalServiceReference;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press <Enter> to run the client...");
            Console.ReadLine();

            EvalServiceClient client = new EvalServiceClient("WSHttpBinding_IEvalService");

            Eval eval = new Eval();
            eval.Comments = "This eval came from code!";
            eval.Submitter = "Aaron";
            eval.TimeSubmitted = DateTime.Now;

            client.SubmitEval(eval);

            client.GetEvalsCompleted += new EventHandler<GetEvalsCompletedEventArgs>(client_GetEvalsCompleted);
            Console.WriteLine("Calling GetEvals...");
            client.GetEvalsAsync();
            Console.WriteLine("GetEvals called.\n");

            Console.ReadLine();
        }

        static void client_GetEvalsCompleted(object sender, GetEvalsCompletedEventArgs e)
        {
            foreach (Eval ev in e.Result)
                Console.WriteLine(ev.Comments);
        }
    }
}
