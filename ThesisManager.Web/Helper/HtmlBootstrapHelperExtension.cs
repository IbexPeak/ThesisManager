namespace ThesisManager.Web.Helper {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;

    public static class HtmlBootstrapHelperExtension {

        /// <summary>
        /// Erzeugt Html-Markup für eine Checkbox.
        /// 
        /// <example>
        /// Beispiel für das erzeugte HTML-Markup:
        /// 
        /// <code>
        /// &lt;div&gt;
        ///     &lt;div class="checkbox @class"&gt;
        ///         &lt;label&gt;
        ///             &lt;input type="checkbox" id="[Propertyname]" name="[Propertyname]" class="@inputClass"/&gt; [Propertyname]             
        ///         &lt;/label&gt;
        ///     &lt;/div&gt;
        /// &lt;/div&gt;
        /// </code>
        /// </example>
        /// 
        /// </summary>
        /// <typeparam name="TModel">Typ des Models für dessen boolsche Eigenschaft eine Checkbox erstellt werden soll.</typeparam>
        /// <param name="htmlHelper">Instanz des HTML-Helpers.</param>
        /// <param name="expression">Memberexpression, die auf die boolsche Eigenschaft des Models zeigt.</param>
        /// <param name="class">CSS-Klassen, welche der Formgroup zugewiesen werden sollen.</param>
        /// <param name="inputClass">CSS-Klassen, welche dem Input (eigentliche Checkbox) zugewiesen werden sollen.</param>
        /// <returns></returns>
        public static MvcHtmlString BootstrapCheckBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, bool>> expression, string @class = "", string inputClass = "") {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            TagBuilder labelTagBuilder = new TagBuilder("label");
            var checkBoxHtml = htmlHelper.CheckBoxFor(expression, new { @class = inputClass });
            labelTagBuilder.InnerHtml = checkBoxHtml + metadata.DisplayName;

            TagBuilder checkBoxDivTagBuilder = new TagBuilder("div");
            checkBoxDivTagBuilder.Attributes.Add("class", "checkbox");
            checkBoxDivTagBuilder.InnerHtml = labelTagBuilder.ToString();

            TagBuilder divTagBuilder = new TagBuilder("div");
            divTagBuilder.Attributes.Add("class", @class);
            divTagBuilder.InnerHtml = checkBoxDivTagBuilder.ToString();
            return new MvcHtmlString(divTagBuilder.ToString());
        }

        public static MvcHtmlString BootstrapCheckBox(this HtmlHelper htmlHelper, string name, bool isChecked, string label, object value, string @class = "", string inputClass = "") {

            TagBuilder labelTagBuilder = new TagBuilder("label");
            var checkBoxHtml = htmlHelper.CheckBox(htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldName(name), isChecked, new { @class = inputClass, value, id = htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldId(name + "_" + value) });
            labelTagBuilder.InnerHtml = checkBoxHtml + label;

            TagBuilder checkBoxDivTagBuilder = new TagBuilder("div");
            checkBoxDivTagBuilder.Attributes.Add("class", "checkbox");
            checkBoxDivTagBuilder.InnerHtml = labelTagBuilder.ToString();

            TagBuilder divTagBuilder = new TagBuilder("div");
            divTagBuilder.Attributes.Add("class", @class);
            divTagBuilder.InnerHtml = checkBoxDivTagBuilder.ToString();
            return new MvcHtmlString(divTagBuilder.ToString());
        }

        public static MvcHtmlString BootstrapDateTimePicker<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, DateTime?>> expression, bool isTimePickingEnabled = false, string valueClass = "", string inputClass = "") {
            //string valueFieldId = htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldId(ExpressionHelper.GetExpressionText(expression));
            //string valueFieldName = htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldName(ExpressionHelper.GetExpressionText(expression));
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            // Äußeres Div
            TagBuilder divTagBuilder = new TagBuilder("div");
            divTagBuilder.Attributes.Add("class", valueClass);

            // InputGroup div
            TagBuilder inputGroupTagBuilder = new TagBuilder("div");
            string isTimePickingDisabledClass = "";
            if (isTimePickingEnabled) {
                isTimePickingDisabledClass = "time";
            } else {
                isTimePickingDisabledClass = "notime";
            }
            inputGroupTagBuilder.Attributes.Add("class", "input-group date " + isTimePickingDisabledClass + " " + inputClass);

            object value;
            if (!string.IsNullOrEmpty(metadata.DisplayFormatString)) {
                value = string.Format(metadata.DisplayFormatString, metadata.Model);
            } else {
                value = metadata.Model;
            }


            // Input Textbox und Kalender-Button.
            MvcHtmlString inputString = htmlHelper.TextBox(ExpressionHelper.GetExpressionText(expression), value, new { @class = "form-control" });
            const string CALENDER_BUTTON_STRING = "<span class=\"input-group-addon\"><span class=\"glyphicon glyphicon-calendar\"></span></span>";

            inputGroupTagBuilder.InnerHtml = inputString + CALENDER_BUTTON_STRING;
            divTagBuilder.InnerHtml = inputGroupTagBuilder.ToString();
            return new MvcHtmlString(divTagBuilder.ToString());
        }

        public static MvcHtmlString BootstrapDisplayBoolFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, bool?>> expression, string labelClass = "", string valueClass = "") {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            bool? value = metadata.Model as bool?;

            TagBuilder labelTagBuilder = new TagBuilder("div");
            labelTagBuilder.AddCssClass(labelClass);
            labelTagBuilder.InnerHtml = metadata.DisplayName;
            labelTagBuilder.AddCssClass(valueClass);

            TagBuilder valueContainerTagBuilder = new TagBuilder("div");

            TagBuilder valueSpanTagBuilder = new TagBuilder("span");
            valueSpanTagBuilder.AddCssClass("label");

            TagBuilder iconSpanTagBuilder = new TagBuilder("span");
            iconSpanTagBuilder.AddCssClass("glyphicon");
            switch (value) {
                case true: {
                        valueSpanTagBuilder.AddCssClass("label-success");
                        iconSpanTagBuilder.AddCssClass("glyphicon-ok");
                        break;
                    }
                case false: {
                        valueSpanTagBuilder.AddCssClass("label-danger");
                        iconSpanTagBuilder.AddCssClass("glyphicon-remove");
                        break;
                    }
                default: {
                        valueSpanTagBuilder.AddCssClass("label-info");
                        iconSpanTagBuilder.AddCssClass("glyphicon-question-sign");
                        break;
                    }
            }

            valueSpanTagBuilder.InnerHtml = iconSpanTagBuilder.ToString();
            valueContainerTagBuilder.InnerHtml = valueSpanTagBuilder.ToString();

            return new MvcHtmlString(labelTagBuilder + Environment.NewLine + valueContainerTagBuilder);
        }

        public static MvcHtmlString BootstrapDisplayBool(this HtmlHelper htmlHelper, bool? valueToDisplay, string valueClass = "") {

            TagBuilder valueContainerTagBuilder = new TagBuilder("div");
            // valueContainerTagBuilder.AddCssClass(valueClass);

            TagBuilder valueSpanTagBuilder = new TagBuilder("span");
            valueSpanTagBuilder.AddCssClass("label");

            TagBuilder iconSpanTagBuilder = new TagBuilder("span");
            iconSpanTagBuilder.AddCssClass("glyphicon");
            switch (valueToDisplay) {
                case true: {
                        valueSpanTagBuilder.AddCssClass("label-success");
                        iconSpanTagBuilder.AddCssClass("glyphicon-ok");
                        break;
                    }
                case false: {
                        valueSpanTagBuilder.AddCssClass("label-danger");
                        iconSpanTagBuilder.AddCssClass("glyphicon-remove");
                        break;
                    }
                default: {
                        valueSpanTagBuilder.AddCssClass("label-info");
                        iconSpanTagBuilder.AddCssClass("glyphicon-question-sign");
                        break;
                    }
            }

            valueSpanTagBuilder.InnerHtml = iconSpanTagBuilder.ToString();
            valueContainerTagBuilder.InnerHtml = valueSpanTagBuilder.ToString();

            return new MvcHtmlString(valueContainerTagBuilder.ToString());
        }

        public static MvcHtmlString BootstrapDisplayFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string @class = "", string inputClass = "") {
            TagBuilder divTagBuilder = new TagBuilder("div");
            divTagBuilder.Attributes.Add("class", @class);

            var inputHtml = htmlHelper.DisplayFor(expression, new { @class = "form-control " + inputClass });
            divTagBuilder.InnerHtml = inputHtml.ToString();

            return new MvcHtmlString(divTagBuilder.ToString());
        }

        public static MvcHtmlString BootstrapDisplayText<TModel>(this HtmlHelper<TModel> htmlHelper, string text, string containerClass = "") {
            TagBuilder divTagBuilder = new TagBuilder("div");
            divTagBuilder.Attributes.Add("class", containerClass);
            divTagBuilder.InnerHtml = text;
            return new MvcHtmlString(divTagBuilder.ToString());
        }

        public static MvcHtmlString BootstrapDropDownFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, SelectList selectList, string optionLabel, string @class = "", string inputClass = "", bool validation = true) {

            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            object selectedValue = metadata.Model;

            MvcHtmlString validationControl = new MvcHtmlString("");
            if (validation) {
                validationControl = new MvcHtmlString(Environment.NewLine + htmlHelper.ValidationMessageFor(expression));
            }

            // Neue Select List erstellen um den ausgewählten Wert korrekt zu selektieren
            IList<SelectListItem> selectListItems = new List<SelectListItem>();
            foreach (SelectListItem listItem in selectList) {
                bool isSelected = false;
                if (selectedValue != null) {
                    isSelected = Equals(selectedValue, listItem.Value) || selectedValue.ToString() == listItem.Value;
                }
                selectListItems.Add(new SelectListItem { Selected = isSelected, Value = listItem.Value, Text = listItem.Text });
            }

            TagBuilder divTagBuilder = new TagBuilder("div");
            divTagBuilder.Attributes.Add("class", @class);
            MvcHtmlString dropDownHtml = htmlHelper.DropDownList(ExpressionHelper.GetExpressionText(expression), selectListItems, optionLabel, "form-control");
            divTagBuilder.InnerHtml = dropDownHtml.ToString() + validationControl;
            return new MvcHtmlString(divTagBuilder.ToString());
        }

        public static MvcHtmlString BootstrapListBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, bool multiple, string placeholder, string @class = "", string inputClass = "") {

            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            TagBuilder divTagBuilder = new TagBuilder("div");
            divTagBuilder.Attributes.Add("class", @class);

            TagBuilder listboxTagBuilder = new TagBuilder("select");
            listboxTagBuilder.AddCssClass("form-control " + inputClass);
            if (!string.IsNullOrEmpty(placeholder)) {
                listboxTagBuilder.Attributes.Add("data-placeholder", placeholder);
            }
            listboxTagBuilder.Attributes.Add("id", htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldId(metadata.PropertyName));
            listboxTagBuilder.Attributes.Add("name", htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldName(metadata.PropertyName));
            if (multiple) {
                listboxTagBuilder.Attributes.Add("multiple", "multiple");
            }
            foreach (SelectListItem listItem in selectList) {
                TagBuilder itemTagBuilder = new TagBuilder("option");
                itemTagBuilder.Attributes.Add("value", listItem.Value);
                itemTagBuilder.InnerHtml = listItem.Text;
                if (listItem.Selected) {
                    itemTagBuilder.Attributes.Add("selected", "selected");
                }

                listboxTagBuilder.InnerHtml += itemTagBuilder.ToString();
            }

            divTagBuilder.InnerHtml = listboxTagBuilder.ToString();
            return new MvcHtmlString(divTagBuilder.ToString());
        }


        public static MvcHtmlString BootstrapDynamicListFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, IEnumerable<string>>> expression, string listClass = "", string inputClass = "", string addButtonClass = "") {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            IList<string> values = metadata.Model as IList<string>;

            string dynamicListHtmlString = "";

            if (values.Any()) {
                // Liste mit bisherigen Einträgen
                foreach (string existingEntry in values) {
                    TagBuilder inputDivTagBuilder = new TagBuilder("div");
                    inputDivTagBuilder.Attributes.Add("class", "input-group");
                    inputDivTagBuilder.InnerHtml = htmlHelper.TextBox(htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldName(metadata.PropertyName), existingEntry, new { @class = "form-control " + inputClass }) + Environment.NewLine + "<span class=\"input-group-addon remove-input\">" + Environment.NewLine + "<span class=\"glyphicon glyphicon-minus\"></span></span>";

                    TagBuilder divTagBuilder = new TagBuilder("div");
                    divTagBuilder.Attributes.Add("class", listClass);
                    divTagBuilder.InnerHtml = inputDivTagBuilder.ToString();

                    TagBuilder formGroupDivTagBuilder = new TagBuilder("div");
                    formGroupDivTagBuilder.Attributes.Add("class", "form-group");
                    formGroupDivTagBuilder.InnerHtml = divTagBuilder.ToString();

                    dynamicListHtmlString += "<!-- Dynamischer Eintrag -->" + Environment.NewLine + formGroupDivTagBuilder;
                }
            } else {
                // Leeres Feld für ersten Eintrag.
                TagBuilder inputDivTagBuilder = new TagBuilder("div");
                inputDivTagBuilder.Attributes.Add("class", "input-group");
                inputDivTagBuilder.InnerHtml = htmlHelper.TextBox(htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldName(metadata.PropertyName), "", new { @class = "form-control " + inputClass }) + Environment.NewLine + "<span class=\"input-group-addon remove-input\">" + Environment.NewLine + "<span class=\"glyphicon glyphicon-minus\"></span></span>";

                TagBuilder divTagBuilder = new TagBuilder("div");
                divTagBuilder.Attributes.Add("class", listClass);
                divTagBuilder.InnerHtml = inputDivTagBuilder.ToString();

                TagBuilder formGroupDivTagBuilder = new TagBuilder("div");
                formGroupDivTagBuilder.Attributes.Add("class", "form-group");
                formGroupDivTagBuilder.InnerHtml = divTagBuilder.ToString();

                dynamicListHtmlString += Environment.NewLine + formGroupDivTagBuilder;
            }

            TagBuilder javaScriptAddAndDeleteTagBuilder = new TagBuilder("script");

            javaScriptAddAndDeleteTagBuilder.InnerHtml = "$(function () {" + Environment.NewLine + "   /* add input element field group */" + Environment.NewLine + "   $(document).on('click', '.add-input', function (e) {" + Environment.NewLine + "       e.preventDefault();" + Environment.NewLine + "       var inputElement = '<div class=\"form-group\">'+" + Environment.NewLine + "           '<div class=\"col-sm-offset-4 col-sm-8 removable\">'+" + Environment.NewLine + "           '<div class=\"input-group\">'+" + Environment.NewLine + "           '<input type=\"text\" class=\"form-control\" name=\"" + htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldName(metadata.PropertyName) + "\" />'+" + Environment.NewLine + "           '<span class=\"input-group-addon remove-input\"><span class=\"glyphicon glyphicon-minus\"></span></span>'+" + Environment.NewLine + "           '</div>'+" + Environment.NewLine + "           '</div>'+" + Environment.NewLine + "           '</div>';" + Environment.NewLine + "       $(this).parents('.form-group').before(inputElement);" + Environment.NewLine + "   });" + Environment.NewLine + Environment.NewLine + "   /* remove input element field group */" + Environment.NewLine + "   $(document).on('click', '.remove-input', function (e) {" + Environment.NewLine + "       e.preventDefault();" + Environment.NewLine + "       $(this).parents('.form-group').remove();" + Environment.NewLine + "   });" + Environment.NewLine + "}); " + Environment.NewLine;

            TagBuilder addButtonTagBuilder = new TagBuilder("div");
            addButtonTagBuilder.Attributes.Add("class", addButtonClass);
            addButtonTagBuilder.InnerHtml = "<button class=\"btn btn-default btn-sm add-input\" type=\"button\">" + Environment.NewLine + "  <span class=\"glyphicon glyphicon-plus\"></span>" + Environment.NewLine + "</button>";

            TagBuilder addButtonFormGroupTagBuilder = new TagBuilder("div");
            addButtonFormGroupTagBuilder.Attributes.Add("class", "form-group");
            addButtonFormGroupTagBuilder.InnerHtml = addButtonTagBuilder.ToString();

            dynamicListHtmlString += Environment.NewLine + javaScriptAddAndDeleteTagBuilder + Environment.NewLine + addButtonFormGroupTagBuilder;

            return new MvcHtmlString(dynamicListHtmlString);
        }

        public static MvcHtmlString BootstrapLabel<TModel>(this HtmlHelper<TModel> htmlHelper, string label, string @class = "") {
            TagBuilder tagBuilder = new TagBuilder("label");
            tagBuilder.Attributes.Add("class", @class + " control-label");

            tagBuilder.InnerHtml = label;
            return new MvcHtmlString(tagBuilder.ToString());
        }

        public static MvcHtmlString BootstrapLabelAndDatePickerFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, DateTime?>> expression, string formGroupClass = "", string labelClass = "", string valueClass = "", string inputClass = "", bool isTimePicking = false, bool validation = true) {
            TagBuilder formGroupTag = new TagBuilder("div");
            formGroupTag.Attributes.Add("class", "form-group date " + formGroupClass);

            MvcHtmlString validationControl = new MvcHtmlString("");
            if (validation) {
                validationControl = new MvcHtmlString(Environment.NewLine + htmlHelper.ValidationMessageFor(expression));
            }

            formGroupTag.InnerHtml = BootstrapLabelFor(htmlHelper, expression, labelClass) + Environment.NewLine + BootstrapDateTimePicker(htmlHelper, expression, isTimePicking, valueClass, inputClass) + validationControl;
            return new MvcHtmlString(formGroupTag.ToString());
        }

        public static MvcHtmlString BootstrapLabelAndDateRangePickerFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, DateTime?>> expressionFrom, Expression<Func<TModel, DateTime?>> expressionTo, string formGroupClass = "", string labelClass = "", string valueClass = "", string inputClass = "", bool isTimePicking = false, bool validation = true) {
            TagBuilder formGroupTagFrom = new TagBuilder("div");
            formGroupTagFrom.Attributes.Add("class", "form-group date" + formGroupClass);

            TagBuilder formGroupTagTo = new TagBuilder("div");
            formGroupTagTo.Attributes.Add("class", "form-group date" + formGroupClass);

            // Id der DatePicker-Felder ermitteln
            string idInputFrom = htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldId(ExpressionHelper.GetExpressionText(expressionFrom));
            ;
            string idInputTo = htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldId(ExpressionHelper.GetExpressionText(expressionTo));

            // Wenn Validation aktiv, dann Controls erstellen
            MvcHtmlString validationControlTo = new MvcHtmlString("");
            MvcHtmlString validationControlFrom = new MvcHtmlString("");
            if (validation) {
                validationControlTo = new MvcHtmlString(Environment.NewLine + htmlHelper.ValidationMessageFor(expressionFrom));
                validationControlFrom = new MvcHtmlString(Environment.NewLine + htmlHelper.ValidationMessageFor(expressionTo));
            }

            formGroupTagFrom.InnerHtml = BootstrapLabelFor(htmlHelper, expressionFrom, labelClass) + Environment.NewLine + BootstrapDateTimePicker(htmlHelper, expressionFrom, isTimePicking, valueClass, inputClass) + validationControlFrom;

            formGroupTagTo.InnerHtml = BootstrapLabelFor(htmlHelper, expressionTo, labelClass) + Environment.NewLine + BootstrapDateTimePicker(htmlHelper, expressionTo, isTimePicking, valueClass, inputClass) + validationControlTo;

            TagBuilder javaScriptMinMaxDateTagBuilder = new TagBuilder("script");
            javaScriptMinMaxDateTagBuilder.Attributes.Add("type", "text/javascript");
            javaScriptMinMaxDateTagBuilder.InnerHtml = "// set first and last possible dates on the date picker fields" + Environment.NewLine + "$(function () {" + Environment.NewLine + "   $('#" + idInputFrom + "').parent().on('dp.change',function (e) {" + Environment.NewLine + "       $('#" + idInputTo + "').parent().data('DateTimePicker').setMinDate(new Date(e.date.year(), e.date.month(), e.date.date()));" + Environment.NewLine + "   });" + Environment.NewLine + "   $('#" + idInputTo + "').parent().on('dp.change',function (e) {" + Environment.NewLine + "       $('#" + idInputFrom + "').parent().data('DateTimePicker').setMaxDate(new Date(e.date.year(), e.date.month(), e.date.date()));" + Environment.NewLine + "   });" + Environment.NewLine + "});" + Environment.NewLine;

            return new MvcHtmlString(formGroupTagFrom + Environment.NewLine + formGroupTagTo + Environment.NewLine + javaScriptMinMaxDateTagBuilder);
        }

        public static MvcHtmlString BootstrapLabelAndDisplayFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string formGroupClass = "", string labelClass = "", string valueClass = "", string inputClass = "") {
            TagBuilder formGroupTag = new TagBuilder("div");
            formGroupTag.Attributes.Add("class", "form-group " + formGroupClass);

            formGroupTag.InnerHtml = BootstrapLabelFor(htmlHelper, expression, labelClass) + Environment.NewLine + BootstrapDisplayFor(htmlHelper, expression, valueClass, inputClass);
            return new MvcHtmlString(formGroupTag.ToString());
        }

        public static MvcHtmlString BootstrapLabelAndDisplayText<TModel>(this HtmlHelper<TModel> htmlHelper, string label, string text, string formGroupClass = "", string labelClass = "", string textContainerClass = "") {
            TagBuilder formGroupTag = new TagBuilder("div");
            formGroupTag.Attributes.Add("class", "form-group " + formGroupClass);

            formGroupTag.InnerHtml = BootstrapLabel(htmlHelper, label, labelClass) + Environment.NewLine + BootstrapDisplayText(htmlHelper, text, textContainerClass);
            return new MvcHtmlString(formGroupTag.ToString());
        }

        public static MvcHtmlString BootstrapLabelAndDropDownFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, SelectList selectList, string optionLabel = "", string formGroupClass = "", string labelClass = "", string valueClass = "", string inputClass = "", bool validation = true) {
            TagBuilder formGroupTag = new TagBuilder("div");
            formGroupTag.Attributes.Add("class", "form-group " + formGroupClass);

            formGroupTag.InnerHtml = BootstrapLabelFor(htmlHelper, expression, labelClass) + Environment.NewLine + BootstrapDropDownFor(htmlHelper, expression, selectList, optionLabel, valueClass, inputClass, validation);
            return new MvcHtmlString(formGroupTag.ToString());
        }

        public static MvcHtmlString BootstrapLabelAndDynamicListFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, IEnumerable<string>>> expression, string labelClass = "", string listClass = "", string inputClass = "", string addButtonClass = "") {
            MvcHtmlString bootstrapLabelFor = BootstrapLabelFor(htmlHelper, expression, labelClass);
            MvcHtmlString bootstrapDynamicListFor = BootstrapDynamicListFor(htmlHelper, expression, listClass, inputClass, addButtonClass);

            return new MvcHtmlString(bootstrapLabelFor + Environment.NewLine + bootstrapDynamicListFor);
        }

        /// <summary>
        ///     Erzeugt den Html-Code für eine TextBox mit Label nach Bootstrap-Konventionen.
        /// </summary>
        /// <typeparam name="TModel">Der Typ des Models</typeparam>
        /// <typeparam name="TProperty">Der Typ des Members.</typeparam>
        /// <param name="htmlHelper">Instanz des HtmlHelpers</param>
        /// <param name="expression">Für welchen Member des Models soll das Control erstellt werden?</param>
        /// <param name="formGroupClass">Zusätzliche Klassen für das Form-Group div</param>
        /// <param name="labelClass">Zusätzliche Klassen für das Label</param>
        /// <param name="valueClass">Zusätzliche Klassen für das div, welches um die Textbox gebaut wird.</param>
        /// <param name="inputClass">Zusätzliche Klassen für die Textbox</param>
        /// <returns></returns>
        public static MvcHtmlString BootstrapLabelAndTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string formGroupClass = "", string labelClass = "", string valueClass = "", string inputClass = "", bool validation = true) {
            TagBuilder formGroupTag = new TagBuilder("div");
            formGroupTag.Attributes.Add("class", "form-group " + formGroupClass);

            MvcHtmlString validationControl = new MvcHtmlString("");
            if (validation) {
                validationControl = new MvcHtmlString(Environment.NewLine + htmlHelper.ValidationMessageFor(expression));
            }

            formGroupTag.InnerHtml = BootstrapLabelFor(htmlHelper, expression, labelClass) + Environment.NewLine + BootstrapTextBoxFor(htmlHelper, expression, valueClass, inputClass) + validationControl;
            return new MvcHtmlString(formGroupTag.ToString());
        }


        /// <summary>
        ///     Erzeugt den Html-Code für eine PasswordBox mit Label nach Bootstrap-Konventionen.
        /// </summary>
        /// <typeparam name="TModel">Der Typ des Models</typeparam>
        /// <typeparam name="TProperty">Der Typ des Members.</typeparam>
        /// <param name="htmlHelper">Instanz des HtmlHelpers</param>
        /// <param name="expression">Für welchen Member des Models soll das Control erstellt werden?</param>
        /// <param name="formGroupClass">Zusätzliche Klassen für das Form-Group div</param>
        /// <param name="labelClass">Zusätzliche Klassen für das Label</param>
        /// <param name="valueClass">Zusätzliche Klassen für das div, welches um die PasswordBox gebaut wird.</param>
        /// <param name="inputClass">Zusätzliche Klassen für die PasswordBox</param>
        /// <returns></returns>
        public static MvcHtmlString BootstrapLabelAndPasswordFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string formGroupClass = "", string labelClass = "", string valueClass = "", string inputClass = "", bool validation = true)
        {
            TagBuilder formGroupTag = new TagBuilder("div");
            formGroupTag.Attributes.Add("class", "form-group " + formGroupClass);

            MvcHtmlString validationControl = new MvcHtmlString("");
            if (validation)
            {
                validationControl = new MvcHtmlString(Environment.NewLine + htmlHelper.ValidationMessageFor(expression));
            }

            formGroupTag.InnerHtml = BootstrapLabelFor(htmlHelper, expression, labelClass) + Environment.NewLine + BootstrapPasswordFor(htmlHelper, expression, valueClass, inputClass) + validationControl;
            return new MvcHtmlString(formGroupTag.ToString());
        }

        public static MvcHtmlString BootstrapLabelFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string @class = "") {
            TagBuilder tagBuilder = new TagBuilder("label");
            tagBuilder.Attributes.Add("class", @class + " control-label");

            string valueFieldId = htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldId(ExpressionHelper.GetExpressionText(expression));
            string valueFieldName = htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldName(ExpressionHelper.GetExpressionText(expression));

            tagBuilder.Attributes.Add("for", valueFieldId);

            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            tagBuilder.InnerHtml = metadata.DisplayName;
            return new MvcHtmlString(tagBuilder.ToString());
        }

        public static MvcHtmlString BootstrapRadioButtonFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, TProperty accordingValue, string labelText, string @class = "", string inputClass = "", IDictionary<string, string> inputAttributes = null) {
            TagBuilder labelTagBuilder = new TagBuilder("label");
            labelTagBuilder.Attributes.Add("class", "radio-inline");

            Dictionary<string, object> htmlAttributes = new Dictionary<string, object>();
            if (inputAttributes != null) {
                if (!string.IsNullOrEmpty(inputClass)) {
                    htmlAttributes.Add("class", inputClass);
                }
                foreach (KeyValuePair<string, string> inputAttribute in inputAttributes) {
                    htmlAttributes.Add(inputAttribute.Key, inputAttribute.Value);
                }
            }

            labelTagBuilder.InnerHtml = htmlHelper.RadioButtonFor(expression, accordingValue, htmlAttributes) + labelText;
            return new MvcHtmlString(labelTagBuilder.ToString());
        }

        public static MvcHtmlString BootstrapPasswordFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string @class = "", string inputClass = "", string placeHolder = "")
        {
            TagBuilder divTagBuilder = new TagBuilder("div");
            divTagBuilder.Attributes.Add("class", @class);

            IDictionary<string, object> htmlAttributesDictionary = new Dictionary<string, object> {
                {"class", "form-control " + inputClass}
            };
            if (!string.IsNullOrWhiteSpace(placeHolder))
            {
                htmlAttributesDictionary.Add("placeholder", placeHolder);
            }

            var inputHtml = htmlHelper.PasswordFor(expression, htmlAttributesDictionary);
            divTagBuilder.InnerHtml = inputHtml.ToString();

            return new MvcHtmlString(divTagBuilder.ToString());
        }

        public static MvcHtmlString BootstrapTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string @class = "", string inputClass = "", string placeHolder = "") {
            TagBuilder divTagBuilder = new TagBuilder("div");
            divTagBuilder.Attributes.Add("class", @class);

            IDictionary<string, object> htmlAttributesDictionary = new Dictionary<string, object> {
                {"class", "form-control " + inputClass}
            };
            if (!string.IsNullOrWhiteSpace(placeHolder)) {
                htmlAttributesDictionary.Add("placeholder", placeHolder);
            }

            var inputHtml = htmlHelper.TextBoxFor(expression, htmlAttributesDictionary);
            divTagBuilder.InnerHtml = inputHtml.ToString();

            return new MvcHtmlString(divTagBuilder.ToString());
        }

        public static MvcHtmlString TextBoxWithFormatFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes) {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return htmlHelper.TextBox(htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldName(ExpressionHelper.GetExpressionText(expression)), string.Format(metadata.DisplayFormatString, metadata.Model), htmlAttributes);
        }
    }

}