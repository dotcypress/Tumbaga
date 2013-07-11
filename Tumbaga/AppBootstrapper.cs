#region

using System;
using Tumbaga.IoC;
using Tumbaga.Logging;
using Tumbaga.MVVM;
using Tumbaga.Messaging;
using Tumbaga.Navigation;
using Tumbaga.Settings;
using Windows.ApplicationModel.Activation;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

#endregion

namespace Tumbaga
{
    public abstract class AppBootstrapper : Application
    {
        private readonly Type _startupPageType;

        protected AppBootstrapper(Type startupPageType)
        {
            _startupPageType = startupPageType;
            Container = new IocContainer();
            ViewModelMap = new ViewModelMap();
            InitContainer();
            InitCore();
        }

        #region Init

        protected bool EnableGrid { get; set; }

        private void InitContainer()
        {
            Container.RegisterInstance<ILogger, NullLogger>();
            Container.RegisterInstance<IMessagePublisher, MessagePublisher>();
            Container.RegisterInstance<INavigationManager>(new NavigationManager(Window.Current, this));
            Container.RegisterInstance<ISettingsManager, SettingsManager>();
        }

        private void InitCore()
        {
            UnhandledException += AppUnhandledException;
        }

        private void WindowSizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            InvalidateVisualState();
        }

        internal void InvalidateVisualState()
        {
            var root = Window.Current.Content as Grid;
            if (root == null)
            {
                return;
            }
            var currentPage = root.Children.Count > 1 ? root.Children[0] as UserControl : null;
            if (currentPage == null)
            {
                return;
            }

            var grid = root.Children.Count > 1 ? root.Children[1] as Grid : null;
            if (grid != null)
            {
                grid.Visibility = EnableGrid ? Visibility.Visible : Visibility.Collapsed;
            }


            var currentViewModel = currentPage.DataContext as ViewModel;
            if (currentViewModel != null)
            {
                currentViewModel.GoToState(ApplicationView.Value.ToString());
            }
        }

        #endregion

        #region Application events

        private void AppUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var logger = Container.TryResolve<ILogger>();
            if (logger != null)
            {
                logger.Fatal(e.Message, e.Exception);
            }
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            Window.Current.Activated += CurrentActivated;
            Window.Current.Closed += CurrentClosed;
            Container.Resolve<INavigationManager>().Navigate(_startupPageType);
        }

        private void CurrentClosed(object sender, CoreWindowEventArgs e)
        {
            Window.Current.SizeChanged -= WindowSizeChanged;
        }

        private void CurrentActivated(object sender, WindowActivatedEventArgs e)
        {
            Window.Current.SizeChanged += WindowSizeChanged;
        }

        #endregion

        #region Properties

        public IocContainer Container { get; private set; }

        public ViewModelMap ViewModelMap { get; private set; }

        #endregion
    }
}
