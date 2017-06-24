namespace ThesisManager.Web.Models.Menu {
    using System;

    public class ActionUrl {
        /// <summary>
        ///     Ruft den Namen der RootArea ab.
        /// </summary>
        public const string ROOT_AREA_NAME = "NoArea";

        public ActionUrl(string area, string controller, string action) {
            Area = area;
            Controller = controller;
            Action = action;
        }

        /// <summary>
        ///     Ruft den Namen der ActionMethode ab, die der Link aufruft.
        /// </summary>
        public string Action { get; private set; }

        /// <summary>
        ///     Ruft den Namen der Area ab, auf die der Link verweisen soll.
        /// </summary>
        public string Area { get; private set; }

        /// <summary>
        ///     Ruft den Namen des Controllers ab, auf den gezeigt wird.
        /// </summary>
        public string Controller { get; private set; }

        /// <summary>
        /// Überprüft, ob der Link auf die entsprechende Area und den entsprechenden Controller zeigt.
        /// </summary>
        /// <param name="area"></param>
        /// <param name="controller"></param>
        /// <returns></returns>
        public bool Equals(string area, string controller) {
            if (String.IsNullOrWhiteSpace(area)) {
                area = ROOT_AREA_NAME;
            }

            return Area == area && Controller == controller;
        }

        /// <summary>
        /// Überprüft ob der Link auf die entsprechende Action-Methode am entsprechenden Controller in der entsprechenden Area zeigt.
        /// </summary>
        /// <param name="area"></param>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool Equals(string area, string controller, string action) {
            return Action == action && Object.Equals(area, controller);
        }
    }
}