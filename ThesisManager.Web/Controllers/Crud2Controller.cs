//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web.Mvc;

//using Com.QueoFlow.Mid360.Web.Areas.Admin.Models.Input;
//using Com.QueoFlow.Mid360.Web.Areas.Admin.Models.View.Shared.Crud;
//using Com.QueoFlow.Mid360.Web.Models.View.Shared;
//using Com.QueoFlow.Mid360.Web.Resources;
//using Com.QueoFlow.Persistence.NHibernate.Domain;
//using Com.QueoFlow.Persistence.NHibernate.Service;

//using Common.Logging;

//namespace Com.QueoFlow.Mid360.Web.Areas.Admin.Controllers {
//    using ThesisManager.Core.Domain.Core;
//    using ThesisManager.Core.Service.Core;
//    using ThesisManager.Web.Controllers;
//    using ThesisManager.Web.Models.Shared;

//    /// <summary>
//    ///     Controller, der Methoden für das CRUD von Domain-Objekten anbietet. In der Basis-Klasse sind alle Methoden
//    ///     implementiert, die in der Regel für alle Controller für das Crud von Entitäten benötigt werden.
//    ///     Sie können jedoch überschrieben werden.
//    /// </summary>
//    /// <typeparam name="TEntity">Die Domain-Klasse.</typeparam>
//    /// <typeparam name="TCreateModel">Der Typ des Models, dass zum Übertragen der Informationen verwendet wird, die für das Erstellen eines neuen Domain-Objektes benötigt werden.</typeparam>
//    /// <typeparam name="TEditModel">Der Typ des Models, dass zum Übertragen der Informationen verwendet wird, die zum Bearbeiten eines vorhandenen Domain-Objektes benötigt werden.</typeparam>
//    /// <typeparam name="TListModel">Typ der für die Anzeige eines Eintrages in einer Liste verwendet wird.</typeparam>
//    public abstract class CrudController<TEntity, TCreateModel, TEditModel, TListModel> : BaseController
//        where TEntity : DomainEntityWithId
//        where TCreateModel : IInputModel, new() 
//        where TEditModel : IInputModel {

//        /// <summary>
//        ///     Legt den Service zur Verwaltung Objekten des Typs <see cref="TEntity"/> fest.
//        /// </summary>
//        protected abstract IDomainEntityWithIdService<TEntity> EntityService {
//            get;
//        }



//        /// <summary>
//        ///     Ruft das Element ab, das zum Schreiben von Log-Einträgen verwendet wird.
//        /// </summary>
//        protected abstract ILog Logger {
//            get;
//        }

//        /// <summary>
//        ///     Ruft den Text ab, der angezeigt wird, wenn das anzuzeigende Objekt nicht gefunden wird.
//        ///     Zum Beispiel beim Aufrufen des Bearbeiten-Dialogs, wenn eine Id übergeben wird, zu der kein Objekt gefunden wird.
//        /// </summary>
//        protected abstract string NotFoundText {
//            get;
//        }

//        /// <summary>
//        ///     Zeigt einen Modalen Dialog zum Erstellen eines neuen Domain-Objektes an.
//        /// </summary>
//        /// <returns>
//        ///     PartialView auf der Felder zum Eintragen der notwendigen Informationen für ein neues Domain-Objekt angezeigt
//        ///     werden.
//        /// 
//        ///     Die angezeigte PartialView ist Crud/Create.
//        ///     Das Formular wird per Reflection anhand des Models erzeugt.
//        /// </returns>
//        [HttpGet]
//        public virtual ActionResult Create() {
//            try {
//                AssertCanCreate();
//                TCreateModel createInputModel = GetCrudCreateModel();
//                return TypedPartialView(CreateView, GetCreateViewModel(), createInputModel);
//            }
//            catch (Exception ex) {
//                return ModalError(OnCreateException(ex));
//            }
//        }

//        /// <summary>
//        /// Definiert beim Überschreiben, ob ein neues Objekt vom Typ <see cref="TEntity"/> erstellt werden kann.
//        /// 
//        /// Ist kein Erstellen möglich muss eine Exception geworfen werden.
//        /// Die Fehlermeldung muss dann in den Methoden <see cref="OnCreateException"/> bzw. <see cref="OnSubmitCreateException"/> definiert werden.
//        /// </summary>
//        protected virtual void AssertCanCreate() {

//        }



//        /// <summary>
//        /// Methode die das Initiale CreateInputModel liefert.
//        /// Wird die Methode nicht überschrieben, wird der default-Initializer (default(TCreateModel)) aufgerufen.
//        /// </summary>
//        /// <returns></returns>
//        protected virtual TCreateModel GetCrudCreateModel() {
//            TCreateModel createInputModel = new TCreateModel();
//            return createInputModel;
//        }

//        /// <summary>
//        ///     Legt ein neues Domain-Objekt vom Typ <see cref="TEntity"/> an, und liefert im Erfolgsfall eine PartialView auf der alle vorhandenen Domain-Objekte des Typs <see cref="TEntity"/> angezeigt werden.
//        /// Tritt ein Fehler auf wird das Erstellen-Formular mit den eingetragenen Werten erneut angezeigt.
//        /// </summary>
//        /// <param name="createModel">Model mit den Eigenschaften, die dem neuen Domain-Objekt zugewiesen werden sollen.</param>
//        /// <returns></returns>
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public virtual ActionResult Create(TCreateModel createModel) {

//            if (ModelState.IsValid) {
//                try {
//                    AssertCanCreate();
//                    OnCreate(createModel);
//                    return RedirectToAction("Grid");
//                }
//                catch (Exception ex) {
//                    OnSubmitCreateException(ex, createModel);
//                }
//            }

//            SetInternalServerError();

//            return TypedPartialView(CreateView, GetCreateViewModel(createModel), createModel);
//        }

//        /// <summary>
//        /// Methoden zum Erstellen des CreateViewModels, wenn beim Erstellen ein Fehler aufgetreten ist und bereits ein CreateModel existiert.
//        /// 
//        /// Das ist u.a. dann notwendig, wenn das ViewModel abhängig von den Eigenschaften des CreateModels initialisiert werden muss (z.B. Abhängige DropDown-Boxen).
//        /// </summary>
//        /// <param name="createModel">Das CreateModel, bei dem das Erstellen zu einem Fehler geführt hat. Kann bzw. darf NICHT null sein.</param>
//        /// <returns></returns>
//        protected virtual ICrudCreateViewModel GetCreateViewModel(TCreateModel createModel) {
//            return GetCreateViewModel();
//        }

//        /// <summary>
//        ///     Zeigt eine Seite an, auf der das Löschen des Objektes bestätigt werden muss.
//        /// </summary>
//        /// <param name="id">Die id des Objektes welches gelöscht werden soll.</param>
//        /// <returns></returns>
//        [HttpGet]
//        public virtual ActionResult Delete(Guid id) {
//            TEntity objectToDelete;
//            try {
//                objectToDelete = EntityService.GetByBusinessId(id);
//            }
//            catch (Exception ex) {
//                Logger.Warn("Das zu löschende Objekt vom Typ <" + typeof(TEntity) + "> mit der Id [" + id + "] wurde nicht gefunden bzw. konnte nicht geladen werden.", ex);
//                return ModalError(new ErrorViewModel(Resources_Web.Errors_Common_Title, NotFoundText));
//            }

//            try {
//                AssertCanDelete(objectToDelete);
//                return PartialView("Crud/Delete", GetCrudDeleteModel(objectToDelete));
//            }
//            catch (Exception ex) {
//                return ModalError(OnDeleteException(ex, objectToDelete));
//            }
//        }

//        /// <summary>
//        ///     Löscht das Objekt mit der übergebenen Id endgültig.
//        ///     Ist das Löschen erfolgreich, wird eine Liste mit den jetzt noch vorhandenen Objekten als PartialView geliefert.
//        ///     Andernfalls wird ein Fehlerdialog (ebenfalls als PartialView) geliefert.
//        /// </summary>
//        /// <param name="id">Die Id des entgültig zu löschenden Objektes.</param>
//        /// <param name="formCollection">Parameter zur Unterscheidung der Überladungen.</param>
//        /// <returns></returns>
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public virtual ActionResult Delete(Guid id, FormCollection formCollection) {
//            TEntity objectToDelete;
//            try {
//                objectToDelete = EntityService.GetByBusinessId(id);
//            }
//            catch (Exception ex) {
//                Logger.Warn("Das endgültig zu löschende Objekt vom Typ <" + typeof(TEntity) + "> mit der Id [" + id + "] wurde nicht gefunden bzw. konnte nicht geladen werden.", ex);
//                return ModalInternalServerError(new ErrorViewModel(Resources_Web.Errors_Common_Title, NotFoundText));
//            }

//            try {
//                AssertCanDelete(objectToDelete);
//                EntityService.Delete(objectToDelete);
//                return GetDeleteResult();
//            }
//            catch (Exception ex) {
//                ErrorViewModel errorViewModel = OnSubmitDeleteException(ex, objectToDelete);
//                return ModalInternalServerError(errorViewModel);
//            }
//        }

//        /// <summary>
//        /// Ruft das Ergebnis ab, das nach dem ERFOLGREICHEN Löschen zurückgegeben wird. Standardmäßig wird die Methode <see cref="Grid"/> geliefert.
//        /// </summary>
//        /// <returns></returns>
//        public virtual ActionResult GetDeleteResult() {
//            return RedirectToAction("Grid");
//        }

//        /// <summary>
//        ///     Liefert das Partial zum Bearbeiten des Domain-Objektes.
//        ///     Wird das Domain-Objekt anhand seiner ID nicht gefunden, wird ein Fehlerpartial angezeigt.
//        /// </summary>
//        /// <param name="id">Die <see cref="DomainEntity.BusinessId">Id</see> des Domain-Objektes, welches bearbeitet werden soll.</param>
//        /// <returns></returns>
//        [HttpGet]
//        public virtual ActionResult Edit(Guid id) {
//            TEntity objectToEdit;
//            try {
//                objectToEdit = EntityService.GetByBusinessId(id);
//            }
//            catch (Exception ex) {
//                Logger.Error("Es sollte ein Nutzer mit der nicht existierenden Id [" + id + "] bearbeitet werden.", ex);
//                return ModalError(new ErrorViewModel(Resources_Web.Errors_Common_Title, NotFoundText));
//            }

//            try {
//                AssertCanEdit(objectToEdit);
//                return TypedPartialView(EditView, GetCrudEditViewModel(objectToEdit), GetCrudEditModel(objectToEdit));
//            }
//            catch (Exception ex) {
//                return ModalError(OnEditException(ex, objectToEdit));
//            }
//        }

//        /// <summary>
//        /// Ruft den Namen der View ab, die zum Bearbeiten eines Eintrages verwendet wird.
//        /// </summary>
//        protected virtual string EditView {
//            get {
//                return "Crud/Edit";
//            }
//        }

//        /// <summary>
//        /// Ruft den Namen der View ab, die zum Erstellen eines neuen Eintrages verwendet wird.
//        /// </summary>
//        protected virtual string CreateView {
//            get {
//                return "Crud/Create";
//            }
//        }

//        /// <summary>
//        /// Erwartet beim überschreiben ein Model, welches die Daten enthält, die vom Nutzer angepasst werden können.
//        /// </summary>
//        /// <param name="objectToEdit"></param>
//        /// <returns></returns>
//        protected abstract IInputModel GetCrudEditModel(TEntity objectToEdit);

//        /// <summary>
//        ///     Übernimmt die Änderungen am Domain-Objekt und liefert eine PartialView, auf der alle vorhandenen Domain-Objekte des Typs <see cref="TEntity"/> angezeigt werden.
//        /// </summary>
//        /// <param name="id">Die Id des Domain-Objektes, das bearbeitet werden soll.</param>
//        /// <param name="editModel">Model, dass die neuen Eigenscahften des Domain-Objektes enthält.</param>
//        /// <returns></returns>
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public virtual ActionResult Edit(Guid id, TEditModel editModel) {

//            TEntity objectToEdit;
//            try {
//                objectToEdit = EntityService.GetByBusinessId(id);
//            }
//            catch (Exception ex) {
//                Logger.Warn(string.Format("Es sollten Änderungen an einem Element vom Typ [{0}] mit der nicht existierenden Id [{1}] übernommen werden.", typeof(TEntity), id), ex);
//                return ModalInternalServerError(new ErrorViewModel(Resources_Web.Errors_Common_Title, NotFoundText));
//            }

//            if (ModelState.IsValid) {

//                try {
//                    AssertCanEdit(objectToEdit);
//                    OnUpdate(objectToEdit, editModel);
//                    return RedirectToAction("Grid");
//                }
//                catch (Exception ex) {
//                    OnSubmitEditException(ex, objectToEdit, editModel);
//                }
//            }

//            SetInternalServerError();
//            return TypedPartialView(EditView, GetCrudEditViewModel(objectToEdit), editModel);
//        }

//        /// <summary>
//        ///     Liefert eine PartialView, auf der alle Domain-Objekte des Typs <see cref="TEntity"/> angezeigt werden.
//        /// </summary>
//        /// <returns></returns>
//        [HttpGet]
//        public virtual ActionResult Grid() {
//            try {
//                IList<TListModel> items = GetGridItems().Select(u => GetListViewModel(u)).ToList();
//                ICrudGridViewModel model = GetCrudGridModel(items);
//                return PartialView("Crud/Grid", model);
//            }
//            catch (Exception ex) {
//                Logger.Error(string.Format("Fehler beim Anzeigen aller Elemente vom Typ {0} in der Grid-Ansicht.", typeof(TEntity)), ex);
//                throw;
//            }

//        }

//        /// <summary>
//        /// Ruft eine Liste der Items <see cref="TEntity"/>, die im Grid angezeigt werden sollen.
//        /// </summary>
//        /// <returns></returns>
//        protected virtual IList<TEntity> GetGridItems() {
//            IList<TEntity> items = EntityService.GetAll();
//            return items;
//        }

//        /// <summary>
//        ///     Liefert eine Übersichtsseite, auf der weitere Optionen angezeigt werden.
//        /// </summary>
//        /// <returns></returns>
//        public virtual ActionResult Index() {
//            return View("Crud/Index", GetCrudIndexViewModel());
//        }

//        /// <summary>
//        ///     Legt beim Überschreiben fest, ob das Objekt gelöscht werden darf/kann.
//        /// 
//        /// Ist kein Löschen möglich muss eine Exception geworfen werden.
//        /// Die Fehlermeldung muss dann in den Methoden <see cref="OnDeleteException"/> bzw. <see cref="OnSubmitDeleteException"/> definiert werden.
//        /// </summary>
//        /// <param name="objectToDelete">Das zu löschende Objekt.</param>
//        protected virtual void AssertCanDelete(TEntity objectToDelete) {
//        }

//        /// <summary>
//        ///     Methode die beim Überschreiben festlegt, ob das Objekt bearbeitet werden darf.
//        /// 
//        /// Ist kein Bearbeiten möglich muss eine Exception geworfen werden.
//        /// Die Fehlermeldung muss dann in den Methoden <see cref="OnEditException"/> bzw. <see cref="OnSubmitEditException"/> definiert werden.
//        /// </summary>
//        /// <param name="objectToEdit">Das Objekt, das bearbeitet werden soll.</param>
//        protected virtual void AssertCanEdit(TEntity objectToEdit) {
//        }

//        /// <summary>
//        /// Erwartet beim Überschreiben, dass ein ViewModel erzeugt wird, das zum Rendern der Create-Ansicht verwendet wird.
//        /// Das ViewModel enthält dabei nur die anzuzeigenden Daten der View. Daten die vom Nutzer eingegeben/geändert werden können, sind in diesem ViewModel nicht enthalten, sondern müssen am Input-Model definiert werden.
//        /// </summary>
//        /// <returns></returns>
//        protected abstract ICrudCreateViewModel GetCreateViewModel();

//        /// <summary>
//        /// Erwartet beim Überschreiben, dass ein ViewModel erzeugt wird, das zum Rendern der Delete-Ansicht verwendet werden kann.
//        /// </summary>
//        /// <param name="objectToDelete"></param>
//        /// <returns></returns>
//        protected abstract ICrudDeleteViewModel GetCrudDeleteModel(TEntity objectToDelete);

//        /// <summary>
//        ///     Methode die beim Überschreiben das CrudEditViewModel erzeugt.
//        /// </summary>
//        /// <param name="entityToEdit">Das Original-Domain-Objekt, das bearbeitet werden soll.</param>
//        protected abstract ICrudEditViewModel GetCrudEditViewModel(TEntity entityToEdit);

//        /// <summary>
//        /// Methode die beim Überschreiben dafür verantwortlich ist, dass ein ViewModel erzeugt wird, das zur Anzeige der Einträge als Liste verwendet werden kann.
//        /// </summary>
//        /// <param name="list"></param>
//        /// <returns></returns>
//        protected abstract ICrudGridViewModel GetCrudGridModel(IList<TListModel> list);

//        /// <summary>
//        ///     Soll beim Überschreiben, das ViewModel für die Index-Ansicht des Controllers liefern.
//        /// </summary>
//        /// <returns></returns>
//        protected abstract CrudIndexViewModel GetCrudIndexViewModel();

//        /// <summary>
//        /// Methode die definiert wie bzw. dafür verantwortlich, dass aus einem <see cref="TEntity">Domain-Objekt</see> ein <see cref="TListModel">ListModel</see> wird.
//        /// </summary>
//        /// <param name="entity">Das Domain-Objekt aus dem das Model erzeugt werden soll.</param>
//        /// <returns></returns>
//        protected abstract TListModel GetListViewModel(TEntity entity);

//        /// <summary>
//        /// Legt beim Überschreiben die Aktionen fest, die durchgeführt werden, wenn das neue Domain-Objekt angelegt wird.
//        /// </summary>
//        /// <param name="createModel">Das Create-Model, dass alle Informationen enthält, die für das erzeugen des Domain-Objektes verwendet werden.</param>
//        protected abstract void OnCreate(TCreateModel createModel);

//        /// <summary>
//        ///     Methode die beim Überschreiben das Verhalten für den Fall definiert, dass kein Dialog zum Löschen eines Nutzers
//        ///     angezeigt werden darf.
//        /// </summary>
//        /// <param name="exception">Die Exception die geworfen wurde.</param>
//        /// <param name="objectToDelete">Das Objekt, für welches die Löschabfrage angezeigt werden soll.</param>
//        /// <returns></returns>
//        protected virtual ErrorViewModel OnDeleteException(Exception exception, TEntity objectToDelete) {
//            Logger.Error("Beim Löschen des Objektes [" + objectToDelete + "] ist ein Fehler aufgetreten.", exception);
//            return new ErrorViewModel(Resources_Web.Errors_Common_Title, Resources_Web.Errors_Common_UnknownException);
//        }

//        /// <summary>
//        ///     Methode die beim Überschreiben das Verhalten für den Fall festlegt, dass beim Anzeigen des Dialog zum Bearbeiten
//        ///     eines Objektes eine Exception auftritt.
//        /// </summary>
//        /// <param name="exception">Die aufgetretene Exception</param>
//        /// <param name="objectToEdit">Das Objekt, welches Bearbeitet werden soll.</param>
//        /// <returns></returns>
//        protected virtual ErrorViewModel OnEditException(Exception exception, TEntity objectToEdit) {
//            Logger.Error("Beim Öffnen des Dialogs zum Bearbeiten des Objektes [" + objectToEdit + " + ID: " + objectToEdit.BusinessId + "] ist ein Fehler aufgetreten.", exception);
//            return new ErrorViewModel(Resources_Web.Errors_Common_Title, Resources_Web.Errors_Common_UnknownException);
//        }

//        /// <summary>
//        ///     Methode die beim Überschreiben das Verhalten für den Fall festlegt, dass beim Anzeigen des Dialog zum Erstellen
//        ///     eines neuen Objektes eine Exception auftritt.
//        /// </summary>
//        /// <param name="exception">Die aufgetretene Exception</param>
//        protected virtual ErrorViewModel OnCreateException(Exception exception) {
//            Logger.Error("Beim Aufruf des Dialogs zum erstellen eines neuen Objektes vom Typ [" + typeof(TEntity) + "] ist ein Fehler aufgetreten.", exception);
//            return new ErrorViewModel(Resources_Web.Errors_Common_Title, Resources_Web.Errors_Common_UnknownException);
//        }

//        /// <summary>
//        /// Definiert beim Überschreiben, das Fehlerhandling für den Fall, das beim Erstellen des neuen Domain-Objektes eine Exception geworfen wurde.
//        /// 
//        ///     Wird die Methode nicht überschrieben, bzw. die Basis-Methode aufgerufen, wird ein ModelState-Error ohne Property-Bezug mit einem allgemeinen Fehlertext hinzugefügt.
//        /// </summary>
//        /// <param name="exception">Die aufgetretene Exception.</param>
//        /// <param name="createModel">Die eingetragenen Daten, die zur Exception führten.</param>
//        protected virtual void OnSubmitCreateException(Exception exception, TCreateModel createModel) {
//            Logger.Error("Beim Erstellen eines Objektes vom Typ [" + typeof(TEntity) + "] ist ein Fehler aufgetreten.", exception);
//            ModelState.AddModelError("", Resources_Web.Errors_Common_UnknownException);
//        }

//        /// <summary>
//        ///     Methode die beim Überschreiben das Verhalten für den Fall festlegt, dass beim Löschen
//        ///     eines Domain-Objektes eine Exception auftritt.
//        /// 
//        ///     Wird die Basis-Methode aufgerufen, wird eine allgemeine Fehlermeldung angezeigt.
//        /// </summary>
//        /// <param name="exception">Die aufgetretene Exception</param>
//        /// <param name="objectToDelete">Das Objekt, welches gelöscht werden soll.</param>
//        /// <returns></returns>
//        protected virtual ErrorViewModel OnSubmitDeleteException(Exception exception, TEntity objectToDelete) {
//            Logger.Error("Beim Löschen des Objektes [" + objectToDelete + ", ID: " + objectToDelete.BusinessId + "] ist ein Fehler aufgetreten.", exception);
//            return new ErrorViewModel(Resources_Web.Errors_Common_Title, Resources_Web.Errors_Common_UnknownException);
//        }

//        /// <summary>
//        ///     Methode die beim Überschreiben das Verhalten für den Fall festlegt, dass beim Übernehmen der Änderungen an
//        ///     einem Domain-Objekt eine Exception auftritt.
//        /// 
//        ///     Wird die Methode nicht überschrieben, bzw. die Basis-Methode aufgerufen, wird ein ModelState-Error ohne Property-Bezug mit einem allgemeinen Fehlertext hinzugefügt.
//        /// </summary>
//        /// <param name="exception">Die aufgetretene Exception</param>
//        /// <param name="objectToEdit">Das Objekt, welches gelöscht werden soll.</param>
//        /// <param name="editModel">Die geänderten Daten, die dazu führten, dass die Exception aufgetreten ist.</param>
//        /// <returns></returns>
//        protected virtual void OnSubmitEditException(Exception exception, TEntity objectToEdit, TEditModel editModel) {
//            Logger.Error("Beim Bearbeiten des Objektes [" + objectToEdit + ", ID: " + objectToEdit.BusinessId + "] ist ein Fehler aufgetreten.", exception);
//            ModelState.AddModelError("", Resources_Web.Errors_Common_UnknownException);
//        }

//        /// <summary>
//        /// Legt beim Überschreiben die Aktionen fest, die durchgeführt werden müssen, um die Änderungen an einem Domain-Objekt zu übernehmen.
//        /// </summary>
//        /// <param name="entity"></param>
//        /// <param name="editModel"></param>
//        protected abstract void OnUpdate(TEntity entity, TEditModel editModel);
//    }
//}