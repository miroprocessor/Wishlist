using Autofac;
using Autofac.Integration.Mvc;
using Wishlist.DB;
using Wishlist.Services;
using System.Reflection;
using System.Web.Mvc;

namespace Wishlist.Web
{
    public class AutofacConfig
    {
        public static IContainer RegisterAll()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new AutofacWebTypesModule());
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<DataContextFactory>().As<IDataContextFactory>();
            builder.RegisterType<ItemsService>();

            return Container(builder);
        }

        private static IContainer Container(ContainerBuilder builder)
        {
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            return container;
        }
    }
}