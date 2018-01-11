using System.Windows;
using GameDb.Base.Authorization;
using GameDb.Base.Interfaces;
using GameDb.Desktop.Base.Interfaces;
using GameDb.Desktop.Base.Services;
using GameDb.Desktop.Modules.Navigation;
using GameDb.Desktop.Shell.Views;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Unity;

namespace GameDb.Desktop.Shell
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            var view = Container.Resolve<ShellView>();
            return view;
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            var moduleCatalog = new ModuleCatalog();

            moduleCatalog.AddModule(typeof(NavigationModule));

            return moduleCatalog;
        }

        protected override void InitializeShell()
        {
            if (Application.Current.MainWindow != null)
            {
                Application.Current.MainWindow.Show();
            }
        }

        protected override void ConfigureContainer()
        {
            Container.RegisterType<INavigationModule, NavigationModule>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IRegistrationService, RegistrationService>();
            Container.RegisterType<IAuthorizationService, AuthorizationService>();
            Container.RegisterType<ISimpleMessageBoxService, SimpleMessageBoxService>();
            Container.RegisterType<IIdentityService, IdentityService>(new ContainerControlledLifetimeManager());

            base.ConfigureContainer();
        }
    }
}