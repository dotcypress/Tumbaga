#region

using System;
using System.Collections.Generic;
using Tumbaga.Common;
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
        private readonly Grid _root;

        public NavigationManager(Window window, AppBootstrapper bootstrapper)
        {
            _window = window;
            _root = new Grid();
            _root.Children.Add(new MetroGrid());
            _bootstrapper = bootstrapper;
        }

        public void Navigate<TPage>(object state = null)
        {
            Navigate(typeof (TPage), state);
        }

        public void Navigate(Type pageType, object state = null)
        {
            if (_window.Content == null)
            {
                _window.Content = _root;
            }
            var currentPage = _root.Children.Count > 1 ? _root.Children[0] as UserControl: null;
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
            if (_root.Children.Count > 1)
            {
                _root.Children[0] = page;
            }
            else
            {
                _root.Children.Insert(0, page);
            }

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
            _root.Children[0] = page;
            _window.Activate();
            _bootstrapper.InvalidateVisualState();
        }

        public bool CanNavigateBack()
        {
            return _history.Count > 0;
        }
    }
}
