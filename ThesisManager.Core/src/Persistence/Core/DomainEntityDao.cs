namespace ThesisManager.Core.Persistence.Core {
    using System;
    using System.Collections.Generic;

    using NHibernate;
    using NHibernate.Criterion;
    using NHibernate.Transform;

    using Spring.Data.NHibernate.Generic;
    using Spring.Data.NHibernate.Generic.Support;

    /// <summary>
    /// Generische Basisklasse für alle DAOs, die sich um DomainEntities kümmern.
    /// T muss vom Typ <see cref="DomainEntity"/> sein.
    /// </summary>
    /// <typeparam name="T">Typ der durch die Klasse behandelt wird.</typeparam>
    public abstract class DomainEntityDao<T> : HibernateDaoSupport, IDomainEntityDao<T> where T : class {

        /// <summary>
        /// Liefert eine Liste mit allen in der Datenbank gespeicherten Objekten vom Typ T.
        /// </summary>
        /// <returns></returns>
        public virtual IList<T> GetAll() {
            return GetAll(null);
        }

        /// <summary>
        /// Liefert eine Liste mit allen in der Datenbank gespeicherten Objekten vom Typ T.
        /// </summary>
        /// <param name="orders">Sortierkriterien oder null wenn keine Sortierung gewünscht ist.</param>
        /// <returns></returns>
        public virtual IList<T> GetAll(IEnumerable<Order> orders) {
            return Find(null, orders);
        }


        /// <summary>
        /// Get entities of this type by the given criterions.
        /// </summary>
        /// <param name="criterions">the criterions</param>
        /// <param name="orders">the orders or null if no order is wanted</param>
        /// <returns>the matching entities</returns>
        /// <example>
        /// In diesem Beispiel wird die Verwendung der Find-Methode dargestellt.
        /// Es werden nur gelockte Objekte nach Namen sortiert zurückgegeben.
        /// <code>
        /// public class SampleService : ISampleService {
        /// 
        ///     ISampleDao _sampleDao = new SampleService();
        ///  
        ///     public IList&lt;SampleObjects&gt; FindLockedSampleObjectsOrderedByName(){ 
        ///         return _sampleDao.Find(new[]{Restrictions.IsTrue("_isLocked"), new[]{Order.Asc("_name")});
        ///     }
        ///  
        /// }
        /// </code>
        /// </example>
        public virtual IList<T> Find(IEnumerable<ICriterion> criterions, IEnumerable<Order> orders) {
            return HibernateTemplate.ExecuteFind(FinderForCriteriaQuery(criterions, orders));

        }

        /// <summary>
        /// Get the one entity of this type by the given criterions.
        /// </summary>
        /// <param name="criterions">the criterions</param>
        /// <returns>the matching entitiy or an exception</returns>
        protected T GetUniqueByCriterions(params ICriterion[] criterions) {
            T obj = FindUniqueByCriterions(criterions);

            if (obj == null) {
                throw new ObjectNotFoundException("Es wurde kein Objekt mit den geforderten Kriterien gefunden.", typeof(T));
            }

            return obj;
        }

        /// <summary>
        /// Find the one entity of this type by the given criterions.
        /// </summary>
        /// <param name="criterions">the criterions</param>
        /// <returns>the matching entitiy or null</returns>
        protected T FindUniqueByCriterions(params ICriterion[] criterions) {
            return HibernateTemplate.Execute(HibernateDelegateForUniqueCriteriaQuery(criterions));
        }


        /// <summary>
        /// Build a <see cref="FindHibernateDelegate{T}"/> for the given criterions and add a <see cref="DistinctRootEntityResultTransformer"/>
        /// </summary>
        /// <param name="criterions">the criterions</param>
        /// <param name="orders">the order</param>
        /// <returns>a finder for the criterions</returns>
        private static FindHibernateDelegate<T> FinderForCriteriaQuery(IEnumerable<ICriterion> criterions, IEnumerable<Order> orders) {
            return delegate(ISession session) {
                ICriteria criteria = session.CreateCriteria(typeof(T)).SetResultTransformer(new DistinctRootEntityResultTransformer());
                BuildCriteria(criteria, criterions);
                AddOrder(criteria, orders);
                return criteria.List<T>();
            };
        }

        /// <summary>
        /// Build a unique result <see cref="HibernateDelegate{T}"/> for the given criterions and add a <see cref="DistinctRootEntityResultTransformer"/>
        /// </summary>
        /// <param name="criterions">the criterions</param>
        /// <returns>a hibernate delegate for the criterions</returns>
        private static HibernateDelegate<T> HibernateDelegateForUniqueCriteriaQuery(ICriterion[] criterions) {
            return delegate(ISession session) {
                ICriteria criteria = session.CreateCriteria(typeof(T)).SetResultTransformer(new DistinctRootEntityResultTransformer());
                BuildCriteria(criteria, criterions);
                return (T)criteria.UniqueResult();
            };
        }

        /// <summary>
        /// Build a hibernate critera query fo the given criterions.
        /// </summary>
        /// <param name="criteria">the criteria</param>
        /// <param name="criterions">the criterions</param>
        /// <returns>the citeria query</returns>
        private static void BuildCriteria(ICriteria criteria, IEnumerable<ICriterion> criterions) {
            if (criterions != null) {
                foreach (ICriterion criterion in criterions) {
                    criteria.Add(criterion);
                }
            }
        }

        /// <summary>
        /// Add the orders for the query to criteria
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="orders"></param>
        private static void AddOrder(ICriteria criteria, IEnumerable<Order> orders) {
            if (orders != null) {
                foreach (Order order in orders) {
                    criteria.AddOrder(order);
                }
            }
        }


        /// <summary>
        /// Speichert ein Objekt vom Typ T in der Datenbank.
        /// </summary>
        /// <param name="entity">Das zu speichernde Objekt</param>
        public virtual void Save(T entity) {
            if (entity == null) {
                throw new ArgumentNullException("entity");
            }
            HibernateTemplate.SaveOrUpdate(entity);
        }

        /// <summary>
        /// Löscht einen Objekt vom Typ T aus der Datenbank.
        /// </summary>
        /// <param name="entity">Das zu löschende Objekt.</param>
        public virtual void Delete(T entity) {
            if (entity == null) {
                throw new ArgumentNullException("entity");
            }
            HibernateTemplate.Delete(entity);
        }

        /// <summary>
        /// Ruft die Anzahl aller Objekte ab.
        /// </summary>
        /// <returns></returns>
        public virtual int GetCount() {
            HibernateDelegate<int> finder = delegate(ISession session) {
                ICriteria criteria =
                    session.CreateCriteria(typeof(T)).SetProjection(
                        new[] { Projections.RowCount() });

                return (int)(criteria.UniqueResult());
            };

            return HibernateTemplate.Execute(finder);
        }

        /// <summary>
        /// Schreibt die aktuelle Session in die Datenbank und leert sie anschließend.
        /// </summary>          
        public void FlushAndClear() {
            HibernateTemplate.Flush();
            HibernateTemplate.Clear();
        }

        /// <summary>
        /// Überprüft ob ein Objekt in der Datenbank referenziert ist.
        /// </summary>
        /// <param name="objectToTest">Das zu testende Objekt.</param>
        /// <returns>true - wenn das Objekt referenziert ist
        /// false - wenn keine Referenz auf das Objekt zeigt</returns>
        public virtual bool IsReferenced(T objectToTest) {
            return false;
        }

        /// <summary>
        /// Ruft das Objekt mit der übergebenen BusinessId aus der Datenbank ab.
        /// Existiert kein Objekt mit der entsprechenden BusinessId wird eine Exception geworfen.
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public virtual T GetByBusinessId(Guid guid) {
            return GetUniqueByCriterions(Restrictions.Eq("_businessId", guid));
        }
    }
}