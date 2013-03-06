#region

using System;
using System.Windows.Input;
using Sample.Core;
using Tumbaga.Commands;
using Tumbaga.MVVM;

#endregion

namespace Sample.ViewModels
{
    public class MainPageViewModel : ViewModel
    {
        private string _test;

        public string Test
        {
            get { return _test; }
            set { SetProperty(ref _test, value); }
        }

        public ICommand TestCommand
        {
            get { return new RelayCommand<object>(x => { Test = DateTime.Now.ToString(); }); }
        }

        protected override void OnLoad()
        {
            MetroGridHelper.CreateGrid();
        }
    }
}
