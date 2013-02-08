#region

using System;
using System.Windows.Input;
using Tumbaga.Navigation;
using Windows.UI.Xaml.Controls;

#endregion

namespace Tumbaga.Commands
{
    public class NavigateCommand<T> : ICommand where T : Page
    {
        private readonly INavigationManager _navigationManager;

        public NavigateCommand(INavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _navigationManager.Navigate<T>(parameter);
        }

        public void OnCanExecutedChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, new EventArgs());
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}