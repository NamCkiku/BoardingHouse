using Autofac;
using Autofac.Integration.Mvc;
using BoardingHouse.Administrator.Infrastructure.Core;
using BoardingHouse.Entities.Models;
using BoardingHouse.Repositoty.Infrastructure;
using BoardingHouse.Repositoty.Repositories;
using BoardingHouse.Service.IService;
using BoardingHouse.Service.Service;
using Microsoft.Owin;
using Owin;
using System.Reflection;
using System.Web.Mvc;

[assembly: OwinStartupAttribute(typeof(BoardingHouse.Administrator.Startup))]
namespace BoardingHouse.Administrator
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            ConfigAutofac(app);
            var service = ServiceFactory.Get<ISystemSettingService>();
            if (service != null)
            {
                service.LoadDefaultValuesSetings();
                service.LoadSystemSettingConfiguration();
            }
        }
        private void ConfigAutofac(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            // Register your Web API controllers.

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();

            builder.RegisterType<DbContexEntities>().AsSelf().InstancePerRequest();



            // Repositories
            builder.RegisterAssemblyTypes(typeof(RoomRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerRequest();

            // Services
            builder.RegisterAssemblyTypes(typeof(RoomService).Assembly)
               .Where(t => t.Name.EndsWith("Service"))
               .AsImplementedInterfaces().InstancePerRequest();

            Autofac.IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }
    }
}
