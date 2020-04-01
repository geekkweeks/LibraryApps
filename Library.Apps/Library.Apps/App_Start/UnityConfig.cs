using Library.Domain.Infrastructure;
using Autofac;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using Autofac.Integration.Mvc;
using System.Reflection;

namespace Library.Apps
{
    public static class UnityConfig
    {
        public static void Run()
        {
            SetAutofactContainer();
        }
        public static IContainer RegisterComponents()
        {
			var builder = new ContainerBuilder();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();
            builder.RegisterType<DbFactory>()
                .As<IDbFactory>()
                .InstancePerLifetimeScope();                  

            // Repositories
            builder.RegisterAssemblyTypes(Assembly.Load("Library.Domain"))
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();

            // Services
            builder.RegisterAssemblyTypes(Assembly.Load("Library.Services"))
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();

            IContainer result = builder.Build();

            return result;
        }

        public static void SetAutofactContainer()
        {
            IContainer container = RegisterComponents();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}