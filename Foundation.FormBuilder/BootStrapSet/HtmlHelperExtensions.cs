using System;
using System.Collections.Generic;
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
    }
}