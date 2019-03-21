using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using MyRewards.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace MyRewards
{
    public class Bootstrapper
    {
        public static void Run()
        {
            SetAutofacContainer();
            SetAutofacWebApiContainer(GlobalConfiguration.Configuration);
        }

        private static void SetAutofacContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            // Services
            builder.RegisterAssemblyTypes(typeof(VoucherService).Assembly)
               .Where(t => t.Name.EndsWith("Service"))
               .AsImplementedInterfaces().InstancePerRequest();

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }


        private static void SetAutofacWebApiContainer(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Services
            builder.RegisterAssemblyTypes(typeof(VoucherService).Assembly)
               .Where(t => t.Name.EndsWith("Service"))
               .AsImplementedInterfaces().InstancePerRequest();

            IContainer container = builder.Build();
            config.DependencyResolver = (new AutofacWebApiDependencyResolver(container));
        }

    }
}