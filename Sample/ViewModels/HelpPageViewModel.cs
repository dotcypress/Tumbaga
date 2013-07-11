#region

using Sample.Core;
using Sample.Views;
using Tumbaga.Commands;
using Tumbaga.IoC;
using Tumbaga.MVVM;

#endregion

namespace Sample.ViewModels
{
    public class HelpPageViewModel : ViewModel
    {
        [Inject]
        public NavigateBackCommand NavigateBackCommand { get; set; }
    }
}
