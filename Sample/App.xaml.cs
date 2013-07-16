#region

using Sample.Core;
using Sample.Views;
using Tumbaga;

#endregion

namespace Sample
{
    sealed partial class App : AppBootstrapper
    {
        public App() : base(typeof (MainPage))
        {
            InitializeComponent();
            InitBootstrapper();
            EnableGrid = true;
        }

        private void InitBootstrapper()
        {
            Container.RegisterInstance(new Calculator(0));
            Container.RegisterInstance(new Calculator(10), "hacked");
        }
    }
}
