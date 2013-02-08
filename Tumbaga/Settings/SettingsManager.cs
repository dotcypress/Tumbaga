#region

using System;
using System.Linq.Expressions;
using Tumbaga.Common;
using Windows.Foundation.Collections;
using Windows.Storage;

#endregion

namespace Tumbaga.Settings
{
    internal class SettingsManager : ISettingsManager
    {
        private readonly IPropertySet _properties;

        public SettingsManager()
        {
            _properties = ApplicationData.Current.LocalSettings.Values;
        }

        public void SetValue(Expression<Func<object>> property, object value)
        {
            SetValue(property.GetPropertyName(), value);
        }

        public void SetValue(string key, object value)
        {
            if (_properties.ContainsKey(key))
            {
                _properties.Remove(key);
            }
            _properties.Add(key, value);
        }

        public T GetValue<T>(Expression<Func<object>> property, T defaultValue)
        {
            return GetValue(property.GetPropertyName(), defaultValue);
        }

        public T GetValue<T>(string key, T defaultValue)
        {
            if (_properties.ContainsKey(key))
            {
                return (T) _properties[key];
            }
            return defaultValue;
        }
    }
}