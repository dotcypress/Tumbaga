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

        internal void Create(UserControl rootElement)
        {
            RootElement = rootElement;
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
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var eventHandler = PropertyChanged;
            if (eventHandler != null)
            {
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region Virtual members

        protected virtual void OnCreate()
        {
        }

        protected virtual void OnLoad()
        {
        }

        protected virtual void OnUnload()
        {
        }

        protected internal Control RootElement { get; private set; }

        #endregion

        #region Internal members

        internal void RootElementLoaded(object sender, RoutedEventArgs e)
        {
            OnLoad();
        }

        #endregion
    }
}