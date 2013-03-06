#region

using System;
using Sample.Core;
using Sample.ViewModels;
using Sample.Views;
using Tumbaga;

#endregion

namespace Sample
{
    sealed partial class App : Bootstrapper
    {
        public App()
            : base(typeof (MainPage))
        {
            InitializeComponent();
            InitBootstrapper();
        }

        private void InitBootstrapper()
        {
            ViewModelMap.Register<MainPage, MainPageViewModel>();
        }
    }
}
