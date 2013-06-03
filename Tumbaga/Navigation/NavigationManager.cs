#region

using System;
using System.Collections.Generic;
using Tumbaga.MVVM;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

#endregion

namespace Tumbaga.Navigation
{
    internal class NavigationManager : INavigationManager
    {
        private readonly AppBootstrapper _bootstrapper;
        private readonly Stack<UserControl> _history = new Stack<UserControl>();
        private readonly Window _window;

        public NavigationManager(Window window, AppBootstrapper bootstrapper)
        {
            _window = window;
            _bootstrapper = bootstrapper;
        }

        public void Navigate<TPage>(object state = null)
        {
            Navigate(typeof (TPage), state);
        }

        public void Navigate(Type pageType, object state = null)
        {
            var currentPage = _window.Content as UserControl;
            if (currentPage != null)
            {
                var currentViewModel = currentPage.DataContext as ViewModel;
                if (currentViewModel != null)
                {
                    currentViewModel.Unload();
                    currentPage.Loaded -= currentViewModel.RootElementLoaded;
                }
                _history.Push(currentPage);
            }
            var page = (UserControl) _bootstrapper.Container.Resolve(pageType);
            _window.Content = page;

            var viewModelType = _bootstrapper.ViewModelMap.Resolve(pageType);
            if (viewModelType != null)
            {
                var viewModel = (ViewModel) _bootstrapper.Container.Resolve(viewModelType);
                page.DataContext = viewModel;
                page.Loaded += viewModel.RootElementLoaded;
                if (viewModel.RootElement == null)
                {
                    viewModel.Create(page, state);
                }
            }
            _window.Activate();
            _bootstrapper.InvalidateVisualState();
        }

        public void NavigateBack()
        {
            var page = _history.Pop();
            _window.Content = page;
            _window.Activate();
            _bootstrapper.InvalidateVisualState();
        }

        public bool CanNavigateBack()
        {
            return _history.Count > 0;
        }
    }
}
