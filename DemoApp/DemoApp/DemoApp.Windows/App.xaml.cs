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
using DemoApp.Common.Interfaces;
using DemoApp.Common.Services;
using System.Diagnostics;

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
            ViewLocator.AddNamespaceMapping("DemoApp.Common.ViewModels", "DemoApp.Windows.Views");

            LogManager.GetLog = type => new DebugLogger(type);
            
            _container = new WinRTContainer();

            _container.RegisterWinRTServices();

            _container.PerRequest<MainViewModel>();
            _container.PerRequest<JobDetailViewModel>();

            PrepareViewFirst();
        }

        protected override void PrepareViewFirst(Frame rootFrame)
        {
            _container.RegisterNavigationService(rootFrame);
            _container.RegisterSingleton(typeof(INavigationService), null, typeof(WindowsNavigationService));
            _container.RegisterSingleton(typeof(IMyJobsService), null, typeof(MyJobsService));
            _container.RegisterSingleton(typeof(ILocalDataService), null, typeof(WindowsLocalDataService));
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

    class DebugLogger : ILog
    {
        #region Fields
        private readonly Type _type;
        #endregion

        #region Constructors
        public DebugLogger(Type type)
        {
            _type = type;
        }
        #endregion

        #region Helper Methods
        private string CreateLogMessage(string format, params object[] args)
        {
            return string.Format("[{0}] {1}",
            DateTime.Now.ToString("o"),
            string.Format(format, args));
        }
        #endregion

        #region ILog Members
        public void Error(Exception exception)
        {
            Debug.WriteLine(CreateLogMessage(exception.ToString()), "ERROR");
        }

        public void Info(string format, params object[] args)
        {
            Debug.WriteLine(CreateLogMessage(format, args), "INFO");
        }

        public void Warn(string format, params object[] args)
        {
            Debug.WriteLine(CreateLogMessage(format, args), "WARN");
        }
        #endregion
    }
}