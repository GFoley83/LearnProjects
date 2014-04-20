using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using EvalServiceLibrary;
using System.ServiceModel.Description;

namespace EvalServiceLibrary
{
    public class EvalServiceHost : ServiceHost
    {
        public EvalServiceHost(params Uri[] baseAddresses) : base(typeof(EvalService), baseAddresses) { }

        protected override void OnOpening()
        {
            base.OnOpening();

            this.AddServiceEndpoint(typeof(IEvalService),
                new BasicHttpBinding(),
                "always-here");

            ServiceMetadataBehavior meta =
                this.Description.Behaviors.Find<ServiceMetadataBehavior>();
            if (meta != null)
            {
                meta.HttpGetEnabled = true;
            }
            else
            {
                meta = new ServiceMetadataBehavior();
                meta.HttpGetEnabled = true;
                this.Description.Behaviors.Add(meta);
            }
        }
    }
}
