using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Threading;
using Haystack;

namespace CollectionsStation
{
    public class Global : System.Web.HttpApplication
    {
        private bool enable = true; //TODO - Link to Configuration

        private Haystack.CollectionsStation collectionsStation;

        protected void Application_Start(object sender, EventArgs e)
        {
#if DEBUG
            string station = "Deubg";
#else
            string station = "Production";
#endif
            collectionsStation = new Haystack.CollectionsStation(station);
            
            if (enable)
            {
                collectionsStation.StartUp();
            }
            else
            {
                collectionsStation.ShutDown();
            }
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            if (!enable && (collectionsStation != null))
                collectionsStation.ShutDown();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (!enable && (collectionsStation != null))
                collectionsStation.ShutDown();
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (!enable && (collectionsStation != null))
                collectionsStation.ShutDown();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            if (collectionsStation != null)
            collectionsStation.ExternalError();
        }

        protected void Session_End(object sender, EventArgs e)
        {
            if (!enable && (collectionsStation != null))
                collectionsStation.ShutDown();
        }

        protected void Application_End(object sender, EventArgs e)
        {
            if (collectionsStation != null)
                collectionsStation.ShutDown();
        }
    }
}