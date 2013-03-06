#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

#endregion

namespace Tumbaga.Common
{
    public static class Extensions
    {
        #region Expressions

        public static string GetPropertyName<T>(this Expression<Func<T>> property)
        {
            var lambda = (LambdaExpression) property;
            MemberExpression memberExpression;
            if (lambda.Body is UnaryExpression)
            {
                var unaryExpression = (UnaryExpression) lambda.Body;
                memberExpression = (MemberExpression) unaryExpression.Operand;
            }
            else
            {
                memberExpression = (MemberExpression) lambda.Body;
            }
            return memberExpression.Member.Name;
        }

        public static string GetPropertyName<T, T1>(this Expression<Func<T, T1>> property)
        {
            var lambda = (LambdaExpression) property;
            MemberExpression memberExpression;
            if (lambda.Body is UnaryExpression)
            {
                var unaryExpression = (UnaryExpression) lambda.Body;
                memberExpression = (MemberExpression) unaryExpression.Operand;
            }
            else
            {
                memberExpression = (MemberExpression) lambda.Body;
            }
            return memberExpression.Member.Name;
        }

        #endregion

        #region DateTime

        public static DateTime ConvertFromUnixTimestamp(this double timestamp)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return origin.AddSeconds(timestamp).ToLocalTime();
        }

        public static double ConvertToUnixTimestamp(this DateTime date)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return Math.Ceiling((date.ToUniversalTime() - origin).TotalSeconds);
        }

        #endregion

        #region Visual tree

        public static IEnumerable<DependencyObject> GetAncestors(this DependencyObject node)
        {
            var parent = VisualTreeHelper.GetParent(node);
            while (parent != null)
            {
                yield return parent;
                parent = VisualTreeHelper.GetParent(parent);
            }
        }

        public static DependencyObject FindAncestor(this DependencyObject target, Type ancestorType)
        {
            return target.GetAncestors().FirstOrDefault(x => ancestorType.GetTypeInfo().IsAssignableFrom(x.GetType().GetTypeInfo()));
        }

        #endregion

        internal static int CombineHashCodes(int hash, int anotherHash)
        {
            return (hash << 5) + hash ^ anotherHash;
        }
    }
}
