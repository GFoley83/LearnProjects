using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CollectionsLibrary
{
    public static class Logger
    {
        public static void Log(string message)
        {
#if DEBUG
            //Mode=Debug
            Debug.WriteLine(message);
#else
            //Mode=Release
#endif
        }
    }
}
