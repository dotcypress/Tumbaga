#region

using System;
using Tumbaga.MVVM;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

#endregion

namespace Tumbaga.Navigation
{
    internal class NavigationManager : INavigationManager
    {
        private readonly Window _window;
        private readonly Bootstrapper _bootstrapper;
        private object _state;

        public NavigationManager(Window window, Bootstrapper bootstrapper)
        {
            _window = window;
            _bootstrapper = bootstrapper;
        }

        public T GetState<T>()
        {
            return (T) _state;
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
            }
            _state = state;
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
                    viewModel.Create(page);
                }
            }
            _window.Activate();
            _bootstrapper.InvalidateVisualState();
        }
    }
}