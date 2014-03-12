using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Foundation.FormBuilder.DynamicForm;
using Foundation.FormBuilder.Form;

namespace Foundation.FormBuilder.BootStrapSet
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString DynamicForm<TModel>
            (this HtmlHelper<TModel> htmlHelper, BootstrapFormType formType = BootstrapFormType.Horizontal)
        {
            TModel model = htmlHelper.ViewData.Model;
            var elements = new FormElementsProvider<TModel>().ExtractElementsFromModel(model, htmlHelper);

            return new BootStrapUIBuilder(Mode.Edit).BuildForm(formType, elements);
        }

        public static MvcHtmlString DynamicForm<TModel>(this HtmlHelper<TModel> htmlHelper, List<FormElement> elements, BootstrapFormType formType = BootstrapFormType.Horizontal)
        {
            return new BootStrapUIBuilder(Mode.Edit).BuildForm(formType, elements);
        }


        public static MvcHtmlString DynamicView<TModel>(this HtmlHelper<TModel> htmlHelper, BootstrapFormType formType = BootstrapFormType.Horizontal, bool renderButtons = false)
        {
            TModel model = htmlHelper.ViewData.Model;
            var elements = new FormElementsProvider<TModel>().ExtractElementsFromModel(model, htmlHelper);

            return new BootStrapUIBuilder(Mode.View).BuildForm(formType, elements);
        }

        public static MvcHtmlString DynamicView<TModel>(this HtmlHelper<TModel> htmlHelper, List<FormElement> elements, BootstrapFormType formType = BootstrapFormType.Horizontal)
        {
            return new BootStrapUIBuilder(Mode.View).BuildForm(formType, elements);
        }

        public static MvcHtmlString DynamicFormElement<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
                                                                       Expression<Func<TModel, TProperty>> expression, BootstrapFormType formType = BootstrapFormType.Horizontal)
        {
            TModel model = htmlHelper.ViewData.Model;

            var expressionText = ExpressionHelper.GetExpressionText(expression);
            
            var element = new FormElementsProvider<TModel>().ExtractSingleElementFromModel(model, htmlHelper, expressionText);

            return new BootStrapUIBuilder(Mode.Edit).BuildElement(formType, element);
        }


        public static MvcHtmlString DynamicViewElement<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
                                                                  Expression<Func<TModel, TProperty>> expression, BootstrapFormType formType = BootstrapFormType.Horizontal)
        {
            TModel model = htmlHelper.ViewData.Model;

            var expressionText = ExpressionHelper.GetExpressionText(expression);

            var element = new FormElementsProvider<TModel>().ExtractSingleElementFromModel(model, htmlHelper, expressionText);

            return new BootStrapUIBuilder(Mode.View).BuildElement(formType, element);
        }

        public static Bootstrap<TModel> Bootstrap<TModel>(this HtmlHelper<TModel> htmlHelper)
        {
            return new Bootstrap<TModel>(htmlHelper);
        }

        public static MvcHtmlString SubmitButton(
         this HtmlHelper htmlHelper,
         string text = "Save",
         string id = null,
         string name = null,
         string @class = null,
         string value = null,
         bool disabled = false)
        {
            return Button(htmlHelper, text, id, name, @class, value: value, disabled: disabled, isSubmit: true);
        }

        public static MvcHtmlString Button(
            this HtmlHelper htmlHelper,
            string text,
            string id = null,
            string name = null,
            string @class = null,
            string icon = null,
            string href = null,
            string value = null,
            string title = null,
            bool disabled = false,
            bool isSubmit = false)
        {
            var tagBuilder = new TagBuilder("button");
            tagBuilder.Attributes.Add("type", isSubmit ? "submit" : "button");

            if (!string.IsNullOrWhiteSpace(id))
            {
                tagBuilder.Attributes.Add("id", id);
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                tagBuilder.Attributes.Add("name", name);
            }

            if (!string.IsNullOrWhiteSpace(@class))
            {
                foreach (var className in @class.Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)))
                {
                    tagBuilder.AddCssClass(className);
                }

                
            }

            tagBuilder.AddCssClass("btn btn-primary");


            if (!string.IsNullOrWhiteSpace(href))
            {
                tagBuilder.Attributes.Add("href", href);
            }

            if (!string.IsNullOrWhiteSpace(title))
            {
                tagBuilder.Attributes.Add("title", title);
            }

            tagBuilder.Attributes.Add("value", string.IsNullOrWhiteSpace(value) ? text : value);

            if (!string.IsNullOrWhiteSpace(icon))
            {
                // tagBuilder.InnerHtml += Image(htmlHelper, icon) + "&nbsp;";
            }

            tagBuilder.InnerHtml += text;

            if (disabled)
            {
                tagBuilder.Attributes.Add("disabled", "disabled");
            }

            return MvcHtmlString.Create(tagBuilder.ToString());
        }
    }
}