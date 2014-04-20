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
            eval.Comments = "This came from code!";
            eval.Submitter = "Aaron";
            eval.TimeSubmitted = DateTime.Now;

            client.SubmitEval(eval);

            List<Eval> evals = client.GetEvals();
            foreach (Eval ev in evals)
                Console.WriteLine(ev.Comments);

            Console.ReadLine();
        }
    }
}
