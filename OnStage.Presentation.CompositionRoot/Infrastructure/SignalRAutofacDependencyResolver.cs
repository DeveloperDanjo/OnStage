using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Autofac;

namespace OnStage.Presentation.CompositionRoot.Infrastructure
{
    public class SignalRAutofacDependencyResolver : DefaultDependencyResolver
    {

        private ILifetimeScope lifetimeScope;

        public SignalRAutofacDependencyResolver(ILifetimeScope lifetimeScope)
        {
            this.lifetimeScope = lifetimeScope;
        }

        public override object GetService(Type serviceType)
        {
            return (ResolutionExtensions.ResolveOptional(this.lifetimeScope, serviceType) ??  base.GetService(serviceType));
        }

        public override IEnumerable<object> GetServices(Type serviceType)
        {
            var enumServiceType = typeof(IEnumerable<>).MakeGenericType(new Type[] { serviceType });
            var instance = ResolutionExtensions.Resolve(this.lifetimeScope, enumServiceType) as IEnumerable<object>;
            if (!instance.Any())
            {
                return base.GetServices(serviceType);
            }
            return instance;
        }

    }
}