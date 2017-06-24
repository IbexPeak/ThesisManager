namespace ThesisManager.Core.Persistence.Core.FluentHibernate {
    using System;
    using System.Linq.Expressions;

    /// <summary>
    ///     Hilfsmethoden für Member von Klassen
    /// </summary>
    public static class Member {
        /// <summary>
        ///     Liefert den Namen des übergebenen Member als String.
        /// </summary>
        /// <param name="expression"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TU"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string ToString<T, TU>(Expression<Func<T, TU>> expression) {
            MemberExpression member = expression.Body as MemberExpression;
            if (member != null)
                return member.Member.Name;

            throw new ArgumentException("Expression is not a member access", nameof(expression));
        }
    }
}