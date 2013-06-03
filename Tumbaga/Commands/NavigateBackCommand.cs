#region

using System;
using System.Windows.Input;
using Tumbaga.Navigation;
using Windows.UI.Xaml.Controls;

#endregion

namespace Tumbaga.Commands
{
    public class NavigateBackCommand : ICommand
    {
        private readonly INavigationManager _navigationManager;

        public NavigateBackCommand(INavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }

        public bool CanExecute(object parameter)
        {
            return _navigationManager.CanNavigateBack();
        }

        public void Execute(object parameter)
        {
            _navigationManager.NavigateBack();
        }

        public event EventHandler CanExecuteChanged;

        public void OnCanExecutedChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, new EventArgs());
            }
        }
    }
}
