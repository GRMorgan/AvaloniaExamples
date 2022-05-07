using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AvaloniaMicrosoftExtensionsDependencyInjection.ViewModels;
using AvaloniaMicrosoftExtensionsDependencyInjection.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace AvaloniaMicrosoftExtensionsDependencyInjection
{
    public partial class App : Application
    {

        private IServiceProvider _container;

        public App()
        {
            _container = InitialiseContainer();
        }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }
        private IServiceProvider InitialiseContainer()
        {
            var serviceCollection = new ServiceCollection();

            //Add any services here

            serviceCollection.AddSingleton<ViewLocator>();

            //Add all view models and views
            GetType().Assembly.GetTypes()
                     .Where(type => type.IsClass)
                     .Where(type => type.Name.EndsWith("ViewModel"))
                     .ToList()
                     .ForEach(viewModelType =>
                     {
                         serviceCollection.AddTransient(viewModelType, viewModelType);
                         var viewName = viewModelType.FullName!.Replace("ViewModel", "View");
                         var viewType = Type.GetType(viewName);
                         if (viewType != null)
                         {
                             serviceCollection.AddTransient(viewType, viewType);
                         }
                     });

            return serviceCollection.BuildServiceProvider();
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                DataTemplates.Add(_container.GetService<ViewLocator>());

                desktop.MainWindow = new MainWindow
                {
                    DataContext = _container.GetService<MainWindowViewModel>(),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
