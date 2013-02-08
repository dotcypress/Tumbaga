#region

using System;
using System.Linq.Expressions;

#endregion

namespace Tumbaga.Settings
{
    public interface ISettingsManager
    {
        void SetValue(Expression<Func<object>> property, Object value);
        void SetValue(string key, Object value);
        T GetValue<T>(Expression<Func<object>> property, T defaultValue);
        T GetValue<T>(string key, T defaultValue);
    }
}