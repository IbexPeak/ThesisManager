namespace ThesisManager.Core.Domain.Core {
    using System;

    /// <summary>
    ///   Basisklasse f�r alle Klassen der Domaine die zus�tzlich zur Id auch �ber einen Namen verf�gen.
    /// </summary>
    public abstract class DomainEntityWithIdAndName : DomainEntityWithId, IComparable {
        
        private string _name;

        /// <summary>
        ///   Standardkonstruktor mit ID und Name
        /// </summary>
        /// <param name="id"> </param>
        /// <param name="name"> </param>
        protected DomainEntityWithIdAndName(int id, string name)
                : base(id) {
            Update(name);
        }

        /// <summary>
        ///   Standardkonstruktor mit Name
        /// </summary>
        /// <param name="name"> </param>
        protected DomainEntityWithIdAndName(string name)
                : this(0, name) {
        }

        /// <summary>
        ///   Parameterloser Standardkonstruktor
        /// </summary>
        protected DomainEntityWithIdAndName()
                : base(0) {
        }

        /// <summary>
        ///   Ruft den Text ab, der verwendet werden kann wenn das Objekt angezeigt werden soll. Die Standardimplemntierung liefert den Wert der ToString-Methode.
        /// </summary>
        public virtual string DisplayText {
            get { return ToString(); }
        }
        /// <summary>
        ///   Ruft den Namen des Objektes ab.
        /// </summary>
        public virtual string Name {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Vergleicht die aktuelle Instanz mit einem anderen Objekt vom selben Typ und gibt eine ganze Zahl zur�ck, die angibt, ob die aktuelle Instanz in der Sortierreihenfolge vor oder nach dem anderen Objekt oder an derselben Position auftritt.
        /// </summary>
        /// <returns>
        /// Ein Wert, der die relative Reihenfolge der verglichenen Objekte angibt.Der R�ckgabewert hat folgende Bedeutung:Wert Bedeutung Kleiner als�0 Diese Instanz ist kleiner als <paramref name="obj"/>. 0 Diese Instanz ist gleich <paramref name="obj"/>. Gr��er als�0 Diese Instanz ist gr��er als <paramref name="obj"/>. 
        /// </returns>
        /// <param name="obj">Ein Objekt, das mit dieser Instanz verglichen werden soll. </param><exception cref="T:System.ArgumentException"><paramref name="obj"/> hat nicht denselben Typ wie diese Instanz. </exception><filterpriority>2</filterpriority>
        public virtual int CompareTo(object obj) {
            if (obj == null) {
                return 1;
            }

            if (obj.GetType() != GetType()) {
                return 1;
            }
            return String.Compare(Name, ((DomainEntityWithIdAndName)obj).Name, StringComparison.Ordinal);
        }

        /// <summary>
        ///   �berschriebt die ToString-Methode und liefert jeweils den Namen des Objektes.
        /// </summary>
        /// <returns> </returns>
        public override string ToString() {
            return _name;
        }

        /// <summary>
        ///   Aktualisiert/�ndert den Namen
        /// </summary>
        /// <param name="name"> </param>
        public virtual void Update(string name) {
            if (string.IsNullOrEmpty(name)) {
                throw new ArgumentNullException("name");
            }
            if (_name == null || !_name.Equals(name)) {
                _name = name;
            }
        }
       
    }
}