This example shows how to setup dependency injection in Avalonia. The full set of changes to
implement this are below

### 1. Add an IServiceProvider field to App.axaml.cs code behind

This is the actual dependency injection container. All our views, view models, services and our
ViewLocator will be retrieved from this object.

```csharp
        private IServiceProvider _container;
```

### 2. Create an `InitialiseContainer` method in App.axaml.cs to setup your dependencies

This creates a ServiceCollection to which we add our ViewLocator, views and view model. The code
`serviceCollection.BuildServiceProvider()` creates an `IServiceProvider` capable of providing the
services defined here with the specified life cycles. The returned `IServiceProvider` will return
a provider with itself as the root if you ask it for an `IServiceProvider`.

Any application services, dataaccess, etc should be added where the comment suggests. The order
doesn't matter but my preference has always been to put items in the order you'd create them.

```csharp
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
```

### 3. Add a default constructor to App.axaml.cs that initialises _container

This just populates `_container` by calling `InitialiseContainer()`

```csharp
        public App()
        {
            _container = InitialiseContainer();
        }
```

### 4. Update `OnFrameworkInitializationCompleted()` in App.axaml.cs to use the container

There's two changes we want to make here. We want to use the container to create the
`MainWindowViewModel` DataContext for the `MainWindow`. This will autowire up any dependencies
for the central view model in the project.

The second change we make is to add a `ViewLocator` from our container to the applications
DataTemplates. This will allow us to pass our container into the `ViewLocator` and so have the
container manage the creation of views.

```csharp
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
```

### 5. Remove the default `ViewLocator` from App.axaml

Delete the three lines shown below from the App.axaml file. This code will create a duplicate
`ViewLocator` that overrides the one created in point 4 that will not return your created views.

```xml
    <Application.DataTemplates>
        <local:ViewLocator/>
    </Application.DataTemplates>
```

### 6. Add an `IServiceProvider` dependency and a constructor to `ViewLocator`

We need to pass in the container to our `ViewLocator` if we are to use it to create our views. The
constructor parameter will be autowired by the container when created. If the XAML loader is
allowed to create this it will wire in its own `IServiceProvider` that does not contain the views.
For this reason it is vital that step 5 is completed.

```csharp
        private IServiceProvider _serviceProvider;

        public ViewLocator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
```

### 7. Update the `Build(object data)` method in `ViewLocator` to use the container

The only change here is to `Activator.CreateInstance(type)` call with a call to our
`IServiceProvider` instance.

```csharp
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
```