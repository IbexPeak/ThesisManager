namespace ThesisManager.Web.Helper {
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    ///     Bietet Erweiterungsmethoden für object an.
    /// </summary>
    public static class ObjectHelper {
        /// <summary>
        ///     Ruft den Namen eines Properties per Lambdaausdruck ab. Bsp this.GetPropertyName(m=>m.Property)
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <typeparam name="TProp"> </typeparam>
        /// <param name="obj"> </param>
        /// <param name="expression"> </param>
        /// <returns> </returns>
        public static string GetPropertyName<T, TProp>(this T obj, Expression<Func<T, TProp>> expression) {
            if (expression == null) {
                throw new ArgumentNullException("expression");
            }

            MemberExpression body = expression.Body as MemberExpression;
            if (body == null) {
                throw new ArgumentException("'expression' should be a member expression");
            }

            return body.Member.Name;
        }

        /// <summary>
        ///     Liefert den Pfad zu einem Property.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProp"></typeparam>
        /// <param name="obj"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string GetPropertyPath<T, TProp>(this T obj, Expression<Func<T, TProp>> expression) {
            if (expression == null) {
                throw new ArgumentNullException("expression");
            }
            MemberExpression memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null) {
                throw new ArgumentException("'expression' should be a member expression");
            } else {
                return string.Join(".", expression.ToString().Split('.').Skip(1));
            }
        }
    }
}