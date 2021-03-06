﻿namespace ThesisManager.Web.Models.Shared {
    using System;

    /// <summary>
    ///     Bildet das VM für eine Bestätigung ab.
    /// </summary>
    public class ConfirmDialogViewModel {
        /// <summary>
        ///     Erstellt ein neues VM für die Bestätigung
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="confirmationText"></param>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        public ConfirmDialogViewModel(Guid id, string title, string confirmationText, string controller, string action) {
            Action = action;
            Controller = controller;
            Id = id;
            Title = title;
            ConfirmationText = confirmationText;
        }

        /// <summary>
        ///     Ruft den Namen der ActionMethode ab, die beim Bestätigen aufgerufen wird.
        /// </summary>
        public string Action { get; private set; }

        /// <summary>
        ///     Ruft den Text ab, den der Nutzer bestätigen muss/soll.
        /// </summary>
        public string ConfirmationText { get; private set; }

        /// <summary>
        ///     Ruft den Namen des Controllers ab, auf dem die Action(-Methode) beim Bestätigen aufgerufen wird.
        /// </summary>
        public string Controller { get; private set; }

        /// <summary>
        ///     Ruft die Id des Objektes ab, für welches die Bestätigung erfolgen soll.
        /// </summary>
        public object Id { get; private set; }

        /// <summary>
        ///     Ruft den Titel des bestätigen Dialoges ab.
        /// </summary>
        public string Title { get; private set; }
    }
}