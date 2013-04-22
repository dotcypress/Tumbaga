#region

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
            //Use autoresolve
            //ViewModelMap.Register<MainPage, MainPageViewModel>();
        }
    }
}
