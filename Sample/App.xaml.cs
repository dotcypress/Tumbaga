#region

using Sample.ViewModels;
using Sample.Views;
using Tumbaga;

#endregion

namespace Sample
{
    sealed partial class App : AppBootstrapper
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
