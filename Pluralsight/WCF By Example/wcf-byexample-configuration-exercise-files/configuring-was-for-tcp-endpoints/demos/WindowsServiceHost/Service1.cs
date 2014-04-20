using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.ServiceModel;
using EvalServiceLibrary;

namespace WindowsServiceHost
{
    public partial class EvalServiceHost : ServiceBase
    {
        ServiceHost host = new ServiceHost(typeof(EvalService));

        public EvalServiceHost()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            host.Open();
        }

        protected override void OnStop()
        {
            host.Close();
        }
    }
}
