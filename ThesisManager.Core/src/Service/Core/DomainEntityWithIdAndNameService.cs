namespace ThesisManager.Core.Service.Core {
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using Spring.Transaction.Interceptor;

    using ThesisManager.Core.Domain.Core;
    using ThesisManager.Core.Exceptions;
    using ThesisManager.Core.Persistence.Core;

    /// <summary>
    /// Klasse für einen Service, wo die Entität eine Id und einen Namen hat.
    /// </summary>
    /// <typeparam name="T">Der Typ der Entität</typeparam>
    public abstract class DomainEntityWithIdAndNameService<T> : DomainEntityWithIdService<T>, IDomainEntityWithIdAndNameService<T>
            where T : DomainEntityWithIdAndName {
        /// <summary>
        ///   Ruft das Data-Access-Object ab, welches für den Zugriff auf die Persistence-Schicht verwendet wird ab.
        /// </summary>
        protected abstract IDomainEntityWithIdAndNameDao<T> IdAndNameDao { get; set; }

        /// <summary>
        ///   Ruft das Data-Access-Object ab, welches für den Zugriff auf die Persistence-Schicht verwendet wird ab.
        /// </summary>
        protected override IDomainEntityWithIdDao<T> IdDao {
            get { return IdAndNameDao; }
            set { IdAndNameDao = (IDomainEntityWithIdAndNameDao<T>)value; }
        }

        /// <summary>
        ///   Ruft das Data-Access-Object ab, welches für den Zugriff auf die Persistence-Schicht verwendet wird ab.
        /// </summary>
        protected override IDomainEntityDao<T> Dao {
            get { return IdAndNameDao; }
            set { IdAndNameDao = (IDomainEntityWithIdAndNameDao<T>)value; }
        }

        /// <summary>
        ///   Ruft ab, ob der Name des Objektes eindeutig sein muss. Das Property entscheidet u.a., ob beim erstellen oder ändern von Objekten eine Überprufung auf eindeutigkeit des Namens durchgeführt wird oder nicht.
        /// </summary>
        protected abstract bool IsNameUnique { get; }

        /// <summary>
        ///   Sucht nach Objekten mit dem übergebenen Namen
        /// </summary>
        /// <param name="name"> Der Name nach dem gesucht werden soll </param>
        /// <returns> Liste mit gefundenen Objekten </returns>
        [Transaction]
        public virtual IList<T> FindByName(string name) {
            return IdAndNameDao.FindByName(name);
        }

        /// <summary>
        ///   Ändert den Namen eines Domainobjektes
        /// </summary>
        /// <param name="objToUpdate"> Das zu ändernde Objekt </param>
        /// <param name="name"> Der neue Name </param>
        [Transaction]
        public virtual void Update(ref T objToUpdate, string name) {
            if (objToUpdate == null) {
                throw new ArgumentNullException("objToUpdate");
            }
            if (name == null) {
                throw new ArgumentNullException("name");
            }
            objToUpdate = GetById(objToUpdate.Id);


            ValidateName(name, objToUpdate);

            if (objToUpdate.Name == name) {
                return;
            }
            
            objToUpdate.Update(name);
            IdAndNameDao.Save(objToUpdate);
        }

        /// <summary>
        /// Überprüft ob ein Name für ein Objekt verwendet werden kann oder nicht.
        /// Der Name kann nicht verwendet werden, wenn er eindeutig sein muss (<see cref="IsNameUnique"/>), es aber nicht ist.
        /// </summary>
        /// <param name="name">Der zu überprüfende Name.</param>
        /// <param name="validationFor">Für welches Objekt soll der Name überprüft werden? Ist es eine generelle Überprüfung (z.B. für Create) dann null.</param>
        /// <exception cref="DuplicateNameException">Wird geworfen, wenn der Name eindeutig sein muss, dies aber nicht ist.</exception>
        public virtual void ValidateName(string name, T validationFor = default(T)) {

            if (IsNameUnique) {
                if (IdAndNameDao.FindByNameExcept(name, validationFor).Any()) {
                    throw new DuplicateEntityNameException("name", name);
                }
            }

        }
    }
}