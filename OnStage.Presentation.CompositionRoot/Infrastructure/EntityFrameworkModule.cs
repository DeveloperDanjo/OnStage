using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using OnStage.Entities;

namespace OnStage.Presentation.CompositionRoot.Infrastructure
{
    public class EntityFrameworkModule : Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException("builder");

            builder.Register(x => new OnStageEntitiesContainer()).SingleInstance();
        }

    }
}