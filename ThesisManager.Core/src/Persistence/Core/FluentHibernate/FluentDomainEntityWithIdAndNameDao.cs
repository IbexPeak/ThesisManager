namespace ThesisManager.Core.Persistence.Core.FluentHibernate {
    using System;
    using System.Collections.Generic;

    using NHibernate;
    using NHibernate.Criterion;

    using Spring.Data.NHibernate.Generic;

    using ThesisManager.Core.Domain.Core;

    /// <summary>
    ///     Ein Dao für über Fluent-Hibernate gemappte Domainobjekte mit Id und einem Namen.
    /// </summary>
    public class FluentDomainEntityWithIdAndNameDao<T> : DomainEntityWithIdAndNameDao<T>, IFluentDomainEntityWithIdAndNameDao<T>
            where T : DomainEntityWithIdAndName {
        /// <summary>
        ///     Sucht in der Persistenzschicht nach Objekten vom Typ T mit diesem Namen.
        /// </summary>
        /// <param name="name"> zu suchender Name </param>
        /// <returns> </returns>
        public override IList<T> FindByName(string name) {
            if (name == null) {
                throw new ArgumentNullException("name");
            }
            FindHibernateDelegate<T> finder = delegate(ISession session) {
                ICriteria criteria = session.CreateCriteria(typeof(T));
                if (IsNameCaseSensitive) {
                    criteria.Add(Restrictions.Eq(Member.ToString((T p) => p.Name), name));
                } else {
                    criteria.Add(Restrictions.Like(Member.ToString((T p) => p.Name), name));
                }

                return criteria.List<T>();
            };

            return HibernateTemplate.ExecuteFind(finder);
        }

        /// <summary>
        ///   Sucht in der Persistenzschicht nach Objekten vom Typ T mit diesem Namen außer einem Objekt, das nicht berücksichtigt werden soll.
        /// </summary>
        /// <param name="name"> zu suchender Name </param>
        /// <param name="except">Objekt, dass nicht berücksichtigt werden soll. Bei Null werden alle vorhandenen Objekte berücksichtigt.</param>
        /// <returns> </returns>
        public override IList<T> FindByNameExcept(string name, T except)
        {
            if (except == null)
            {
                return FindByName(name);
            }

            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            FindHibernateDelegate<T> finder = delegate(ISession session)
            {
                ICriteria criteria = session.CreateCriteria(typeof(T));
                if (IsNameCaseSensitive)
                {
                    criteria.Add(Restrictions.Eq(Member.ToString((T p) => p.Name), name));
                }
                else
                {
                    criteria.Add(Restrictions.Like(Member.ToString((T p) => p.Name), name));
                }
                criteria.Add(Restrictions.Not(Restrictions.Eq(Member.ToString((T p) => p.BusinessId), except.BusinessId)));

                return criteria.List<T>();
            };

            return HibernateTemplate.ExecuteFind(finder);
        }

        /// <summary>
        ///     Ruft ab, ob ein Objekt mit diesem Namen existiert.
        /// </summary>
        /// <param name="name"> </param>
        /// <returns> </returns>
        public override bool IsNameExisting(string name) {
            if (name == null) {
                throw new ArgumentNullException("name");
            }

            HibernateDelegate<bool> finder = delegate(ISession session) {
                ICriteria criteria = session.CreateCriteria(typeof(T)).
                        SetProjection(new[] { Projections.RowCount() });
                if (IsNameCaseSensitive) {
                    criteria.Add(Restrictions.Eq(Member.ToString((T p) => p.Name), name));
                } else {
                    criteria.Add(Restrictions.Like(Member.ToString((T p) => p.Name), name));
                }

                return ((int)criteria.UniqueResult() > 0);
            };

            return HibernateTemplate.Execute(finder);
        }
    }
}