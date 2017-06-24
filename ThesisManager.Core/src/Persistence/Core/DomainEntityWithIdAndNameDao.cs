namespace ThesisManager.Core.Persistence.Core {
    using System;
    using System.Collections.Generic;

    using NHibernate;
    using NHibernate.Criterion;

    using Spring.Data.NHibernate.Generic;

    using ThesisManager.Core.Domain.Core;

    /// <summary>
    ///   Generische Basisklasse für alle DAOs die sich um DomainEntitiesWithIdAndName kümmern. T muss vom Typ <see
    ///    cref="DomainEntityWithIdAndName" /> sein.
    /// </summary>
    /// <typeparam name="T"> Typ der durch die Klasse behandelt wird. </typeparam>
    public class DomainEntityWithIdAndNameDao<T> : DomainEntityWithIdDao<T>, IDomainEntityWithIdAndNameDao<T>
            where T : DomainEntityWithIdAndName {
        /// <summary>
        ///   Ruft ab, ob die Groß- und Kleinschreibung beim Namen der Entität eine Rolle spielt oder nicht.
        /// </summary>
        protected virtual bool IsNameCaseSensitive {
            get { return true; }
        }

        /// <summary>
        ///   Sucht in der Persistenzschicht nach Objekten vom Typ T mit diesem Namen.
        /// </summary>
        /// <param name="name"> zu suchender Name </param>
        /// <returns> </returns>
        public virtual IList<T> FindByName(string name) {
            if (name == null) {
                throw new ArgumentNullException("name");
            }
            FindHibernateDelegate<T> finder = delegate(ISession session) {
                ICriteria criteria = session.CreateCriteria(typeof(T));
                if (IsNameCaseSensitive) {
                    criteria.Add(Restrictions.Eq("_name", name));
                } else {
                    criteria.Add(Restrictions.Like("_name", name));
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
        public virtual IList<T> FindByNameExcept(string name, T except) {
            if (except == null) {
                return FindByName(name);
            }

            if (name == null) {
                throw new ArgumentNullException("name");
            }

            FindHibernateDelegate<T> finder = delegate(ISession session) {
                ICriteria criteria = session.CreateCriteria(typeof(T));
                if (IsNameCaseSensitive) {
                    criteria.Add(Restrictions.Eq("_name", name));
                } else {
                    criteria.Add(Restrictions.Like("_name", name));
                }
                criteria.Add(Restrictions.Not(Restrictions.Eq("_businessId", except.BusinessId)));
                
                return criteria.List<T>();
            };

            return HibernateTemplate.ExecuteFind(finder);
        }

        /// <summary>
        ///   Ruft ab, ob ein Objekt mit diesem Namen existiert.
        /// </summary>
        /// <param name="name"> </param>
        /// <returns> </returns>
        public virtual bool IsNameExisting(string name) {
            if (name == null) {
                throw new ArgumentNullException("name");
            }

            HibernateDelegate<bool> finder = delegate(ISession session) {
                ICriteria criteria = session.CreateCriteria(typeof(T)).
                    SetProjection(new[] { Projections.RowCount() });
                if (IsNameCaseSensitive) {
                    criteria.Add(Restrictions.Eq("_name", name));
                } else {
                    criteria.Add(Restrictions.Like("_name", name));
                }

                return ((int)criteria.UniqueResult() > 0);
            };

            return HibernateTemplate.Execute(finder);
        }

        
    }
}