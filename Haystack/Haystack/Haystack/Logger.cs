using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haystack
{
    internal static class Logger
    {
        internal static void Log(string log)
        {
#if DEBUG
            Debug.WriteLine(DateTime.UtcNow.ToString() + ": " + log);
#else
#endif
            AzureClientService.WriteBlobLog(log);
        }
    }
}