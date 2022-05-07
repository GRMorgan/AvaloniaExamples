using Avalonia.Controls;
using Avalonia.Controls.Templates;
using AvaloniaMicrosoftExtensionsDependencyInjection.ViewModels;
using System;

namespace AvaloniaMicrosoftExtensionsDependencyInjection
{
    public class ViewLocator : IDataTemplate
    {
        private IServiceProvider _serviceProvider;

        public ViewLocator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IControl Build(object data)
        {
            var name = data.GetType().FullName!.Replace("ViewModel", "View");
            var type = Type.GetType(name);

            if (type != null)
            {
                return (Control)_serviceProvider.GetService(type)!;
            }
            else
            {
                return new TextBlock { Text = "Not Found: " + name };
            }
        }

        public bool Match(object data)
        {
            return data is ViewModelBase;
        }
    }
}
