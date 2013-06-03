#region

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

#endregion

namespace Tumbaga.MVVM
{
    public abstract class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        internal void Create(UserControl rootElement, object state)
        {
            RootElement = rootElement;
            State = state;
            OnCreate();
        }

        internal void Unload()
        {
            OnUnload();
        }

        public void GoToState(string stateName, bool useTransitions = true)
        {
            VisualStateManager.GoToState(RootElement, stateName, useTransitions);
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] String propertyName = null)
        {
            if (Equals(storage, value))
            {
                return false;
            }
            storage = value;
// ReSharper disable ExplicitCallerInfoArgument
            OnPropertyChanged(propertyName);
// ReSharper restore ExplicitCallerInfoArgument
            return true;
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var eventHandler = PropertyChanged;
            if (eventHandler != null)
            {
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region Virtual members

        protected internal Control RootElement { get; private set; }

        protected object State { get; private set; }

        protected virtual void OnCreate()
        {
        }

        protected virtual void OnLoad()
        {
        }

        protected virtual void OnUnload()
        {
        }

        #endregion

        #region Internal members

        internal void RootElementLoaded(object sender, RoutedEventArgs e)
        {
            OnLoad();
        }

        #endregion
    }
}
