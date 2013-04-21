using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using Microsoft.AspNet.SignalR.Hubs;

namespace OnStage.Presentation.CompositionRoot.Infrastructure
{
    public class SignalRHubModule : Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException("builder");

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .Where(t => typeof(IHub).IsAssignableFrom(t))
                .ExternallyOwned();
        }

    }
}