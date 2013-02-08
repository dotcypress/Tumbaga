#region

using System;

#endregion

namespace Tumbaga.Navigation
{
    public interface INavigationManager
    {
        void Navigate<TPage>(object state = null);

        void Navigate(Type pageType, object state = null);

        TState GetState<TState>();
    }
}