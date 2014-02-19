using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Foundation.FormBuilder.CustomAttribute;

namespace Foundation.FormBuilder.DynamicForm
{
    public class FormElementsProvider<TModel> 
    {
        public List<FormElement> ExtractElementsFromModel(TModel model, HtmlHelper<TModel> htmlHelper)
        {
            var properties = typeof(TModel).GetCachedProperties(BindingFlags.Public | BindingFlags.Instance)
                                    .ToDictionary(p => p.Name, p => p);

            var formElements = properties.Select(p =>
                                            new FormElement
                                            {
                                                PropertyInfo = p.Value,
                                                ControlSpecs =
                                                    p.Value.GetCustomAttributes(typeof(EditControl), false)
                                                     .Cast<EditControl>()
                                                     .FirstOrDefault(),
                                                CollectionInfo = p.Value.GetCustomAttributes(typeof(CollectionInfo), false)
                                                     .Cast<CollectionInfo>()
                                                     .FirstOrDefault(),
                                                FieldValue = FieldValue(model, p)
                                            })
                                    .Where(p => p.ControlSpecs != null)
                                    .ToList();

            foreach (var formElement in formElements)
            {
                var displayAttribute = formElement.PropertyInfo.GetCustomAttributes(typeof (DisplayAttribute), false).Cast<DisplayAttribute>().FirstOrDefault();
                
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
            }

            foreach (var collectionItems in formElements.Where(x => x.CollectionInfo != null))
            {
                collectionItems.CollectionInfo.CollectionObject =
                    properties[collectionItems.CollectionInfo.ListSourceMember]
                        .GetValue(model, null) as IEnumerable<SelectListItem>;
            }

            return formElements;
        }

        private object FieldValue(TModel model, KeyValuePair<string, PropertyInfo> p)
        {
            return (model != null) ? p.Value.GetValue(model, null) : null;
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