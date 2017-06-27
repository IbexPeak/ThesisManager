namespace ThesisManager.Web.Controllers {
    using System;

    using Common.Logging;

    using ThesisManager.Web.Models.Shared;

    /// <summary>
    /// Basisklasse für alle Controller, die CRUD-Funktionalität bieten.
    /// </summary>
    public class CrudController:BaseController {
        /// <summary>
        ///     Ruft das Element ab, das zum Schreiben von Log-Einträgen verwendet wird.
        /// </summary>
        protected ILog Logger
        {
            get;
        }

        /// <summary>
        ///     Methode die beim Überschreiben das Verhalten für den Fall definiert, dass kein Dialog zum Löschen eines Nutzers
        ///     angezeigt werden darf.
        /// </summary>
        /// <param name="exception">Die Exception die geworfen wurde.</param>
        /// <returns></returns>
        protected virtual ErrorViewModel OnDeleteException(Exception exception)
        {
            Logger.Error("Beim Löschen des Objektes ist ein Fehler aufgetreten.", exception);
            return new ErrorViewModel("Fehler", "Unbekannter Fehler");
        }

        /// <summary>
        ///     Methode die beim Überschreiben das Verhalten für den Fall festlegt, dass beim Anzeigen des Dialog zum Bearbeiten
        ///     eines Objektes eine Exception auftritt.
        /// </summary>
        /// <param name="exception">Die aufgetretene Exception</param>
        /// <returns></returns>
        protected virtual ErrorViewModel OnEditException(Exception exception)
        {
            Logger.Error("Beim Öffnen des Dialogs zum Bearbeiten des Objektes ist ein Fehler aufgetreten.", exception);
            return new ErrorViewModel("Fehler", "Unbekannter Fehler");
        }

        /// <summary>
        ///     Methode die beim Überschreiben das Verhalten für den Fall festlegt, dass beim Anzeigen des Dialog zum Erstellen
        ///     eines neuen Objektes eine Exception auftritt.
        /// </summary>
        /// <param name="exception">Die aufgetretene Exception</param>
        protected virtual ErrorViewModel OnCreateException(Exception exception)
        {
            Logger.Error("Beim Aufruf des Dialogs zum erstellen eines neuen Objektes ist ein Fehler aufgetreten.", exception);
            return new ErrorViewModel("Fehler", "Unbekannter Fehler");
        }
    }
}