using System;
using System.Collections.Generic;
using System.Reflection;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml.Controls;
using Caliburn.Micro;
using DemoApp.Common.ViewModels;
using DemoApp.Windows.Services;
using DemoApp.Windows.Views;
using INavigationService = DemoApp.Common.INavigationService;

namespace DemoApp.Windows
{
    public sealed partial class App
    {
        private WinRTContainer _container;

        public App()
        {
            InitializeComponent();
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {

            return new[]
                {
                    GetType().GetTypeInfo().Assembly,
                    typeof (MainViewModel).GetTypeInfo().Assembly
                };
        }

        protected override void Configure()
        {
            MessageBinder.SpecialValues.Add("$clickeditem", c => ((ItemClickEventArgs)c.EventArgs).ClickedItem);

            ViewModelLocator.AddNamespaceMapping("DemoApp.Windows.Views", "DemoApp.Common.ViewModels");
           
            
            _container = new WinRTContainer();

            _container.RegisterWinRTServices();

            _container.PerRequest<MainViewModel>();

            PrepareViewFirst();
        }

        protected override void PrepareViewFirst(Frame rootFrame)
        {
            _container.RegisterNavigationService(rootFrame);
            _container.RegisterSingleton(typeof(INavigationService), null, typeof(WindowsNavigationService));
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            Initialize();

            if (args.PreviousExecutionState == ApplicationExecutionState.Running)
                return;

            if (RootFrame.Content == null)
                DisplayRootView<MainView>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}