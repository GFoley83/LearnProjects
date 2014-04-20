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
            EvalServiceClient client =
                new EvalServiceClient("NetTcpBinding_IEvalService");

            Eval eval = new Eval();
            eval.Submitter = "Bob";
            eval.Timesent = DateTime.Now;
            eval.Comments = "Hope this works!";

            client.SubmitEval(eval);

            Console.WriteLine("Number of evals: {0}",
                client.GetEvals().Length);
        }
    }
}
