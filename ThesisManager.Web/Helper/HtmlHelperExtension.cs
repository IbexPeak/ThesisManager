namespace ThesisManager.Web.Helper {
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;

    /// <summary>
    /// Klasse mit Erweiterungsmethoden für den HTML-Helper.
    /// </summary>
    public static class HtmlHelperExtension {


        /// <summary>
        /// Liefert das HTML-Markup für eine DropDownBox.
        /// 
        /// Die Methode ist notwendig, da die Helper-Methode von Microsoft Probleme mit dem vorauswählen von Einträgen hat.
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="name"></param>
        /// <param name="selectList">Die Liste mit auswählbaren Einträgen. Ist bei einem Item in der Liste die Eigenschaft <see cref="SelectListItem.Selected"/> true, enthält es das entsprechende HTML-Markup (selected=selected).</param>
        /// <param name="optionLabel">Optionaler Eintrag, der bedeuted, dass kein Element ausgewählt ist. Dient zum Beispiel dazu, im initialen Zustand "Auswählen" oder "Kein Element ausgewählt" anzuzeigen.</param>
        /// <param name="class">Klasse(n) die der eigentlichen Dropdownbox (&lt;select&gt;) zugewiesen werden.</param>
        /// <returns>HTML-Markup für die DropDownBox</returns>
        public static MvcHtmlString DropDownList(this HtmlHelper htmlHelper, string name, IList<SelectListItem> selectList, string optionLabel, string @class = "") {

            TagBuilder selectTagBuilder = new TagBuilder("select");
            if (!string.IsNullOrEmpty(@class)) {
                selectTagBuilder.AddCssClass(@class);
            }

            selectTagBuilder.Attributes.Add("name", htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldName(name));
            selectTagBuilder.Attributes.Add("id", htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldId(name));

            foreach (KeyValuePair<string, object> validationAttribute in htmlHelper.GetUnobtrusiveValidationAttributes(name)) {
                selectTagBuilder.Attributes.Add(validationAttribute.Key, validationAttribute.Value.ToString());
            }


            if (!string.IsNullOrEmpty(optionLabel)) {
                TagBuilder optionTagBuilder = new TagBuilder("option");
                optionTagBuilder.Attributes.Add("value", null);
                optionTagBuilder.InnerHtml = optionLabel;
                selectTagBuilder.InnerHtml += optionTagBuilder.ToString();
            }
            foreach (SelectListItem listItem in selectList) {
                TagBuilder optionTagBuilder = new TagBuilder("option");
                if (listItem.Selected) {
                    optionTagBuilder.Attributes.Add("selected", "selected");
                }
                optionTagBuilder.Attributes.Add("value", listItem.Value);
                optionTagBuilder.InnerHtml = listItem.Text;
                selectTagBuilder.InnerHtml += optionTagBuilder.ToString();
            }

            return new MvcHtmlString(selectTagBuilder.ToString());

        }

        public static MvcHtmlString TextBoxWithFormatFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes) {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return htmlHelper.TextBox(htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldName(ExpressionHelper.GetExpressionText(expression)), string.Format(metadata.DisplayFormatString, metadata.Model), htmlAttributes);
        }
    }
}