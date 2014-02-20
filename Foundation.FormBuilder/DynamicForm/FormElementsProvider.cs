using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using Foundation.FormBuilder.CustomAttribute;

namespace Foundation.FormBuilder.DynamicForm
{
    public class FormElementsProvider<TModel> 
    {
        readonly Dictionary<string, PropertyInfo> properties;
            
        public FormElementsProvider()
        {
            this.properties = typeof(TModel).GetCachedProperties(BindingFlags.Public | BindingFlags.Instance)
                                       .ToDictionary(p => p.Name, p => p);
        }

        public List<FormElement> ExtractElementsFromModel(TModel model, HtmlHelper<TModel> htmlHelper)
        {
            var formElements = properties.Select(p =>
                                            FormElement(model, p.Value, htmlHelper))
                                            .Where(p => p!= null && p.ControlSpecs != null)
                                    .ToList();
            return formElements;
        }

        public FormElement ExtractSingleElementFromModel(TModel model, HtmlHelper<TModel> htmlHelper, string propertyName)
        {
            var formElement = properties
                .Where(p => p.Key == propertyName)
                .Select(p =>
                        FormElement(model, p.Value, htmlHelper)).FirstOrDefault(p => p.ControlSpecs != null);

            return formElement;
        }


        private FormElement FormElement(TModel model, PropertyInfo propertyInfo, HtmlHelper<TModel> htmlHelper)
        {
            var formElement = new FormElement
                {
                    PropertyInfo = propertyInfo,
                    ControlSpecs =
                        propertyInfo.GetCustomAttributes(typeof(EditControl), false)
                         .Cast<EditControl>()
                         .FirstOrDefault(),
                    CollectionInfo = propertyInfo.GetCustomAttributes(typeof(CollectionInfo), false)
                                      .Cast<CollectionInfo>()
                                      .FirstOrDefault(),
                    FieldValue = FieldValue(model, propertyInfo)
                };

            if (formElement.ControlSpecs == null)
            {
                return null;
            }

            var displayAttribute = formElement.PropertyInfo.GetCustomAttributes(typeof(DisplayAttribute), false).Cast<DisplayAttribute>().FirstOrDefault();

            formElement.ControlSpecs.ControlName = formElement.ControlSpecs.ControlName ?? formElement.PropertyInfo.Name;
            formElement.ControlSpecs.ClientId = formElement.ControlSpecs.ClientId ?? formElement.PropertyInfo.Name;
            formElement.MappedDataType = formElement.PropertyInfo.PropertyType;


            ModelState modelState;
            if (htmlHelper.ViewData.ModelState.TryGetValue(formElement.ControlSpecs.ControlName, out modelState) && modelState.Errors.Count > 0)
            {
                formElement.HasErrors = true;
            }

            formElement.ValidationAttributes = this.GetValidationAttributes(htmlHelper, formElement.PropertyInfo.Name);

            if (displayAttribute != null)
            {
                formElement.ControlSpecs.GroupName = displayAttribute.GetGroupName();
                formElement.ControlSpecs.Name = displayAttribute.GetName();
                formElement.ControlSpecs.Prompt = displayAttribute.GetPrompt();
                formElement.ControlSpecs.Order = displayAttribute.GetOrder();
                formElement.ControlSpecs.ShortName = displayAttribute.GetShortName();

                if (string.IsNullOrEmpty(formElement.ControlSpecs.ControlName))
                {
                    formElement.ControlSpecs.ControlName = formElement.PropertyInfo.Name;
                }

                if (string.IsNullOrEmpty(formElement.ControlSpecs.ClientId))
                {
                    formElement.ControlSpecs.ClientId = formElement.PropertyInfo.Name;
                }
            }

            if (formElement.CollectionInfo != null)
            {
                formElement.CollectionInfo.CollectionObject =
                   properties[formElement.CollectionInfo.ListSourceMember]
                       .GetValue(model, null) as IEnumerable<SelectListItem>;
            }

            return formElement;
        }

        private object FieldValue(TModel model, PropertyInfo p)
        {
            return (model != null) ? p.GetValue(model, null) : null;
        }

        private IDictionary<string, object> GetValidationAttributes(HtmlHelper<TModel> htmlHelper, string memberName)
        {
            var propertyExpression = ExpressionHelper.GetExpressionText(memberName);

            var metadata = ModelMetadata.FromStringExpression(propertyExpression, htmlHelper.ViewData);

            var attributes = htmlHelper.GetUnobtrusiveValidationAttributes(propertyExpression, metadata);
            return attributes;
        }
    }
}