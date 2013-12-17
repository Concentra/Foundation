using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Foundation.FormBuilder.Blocks;
using Foundation.FormBuilder.CustomAttribute;
using Foundation.FormBuilder.ElementGenerators;
using Foundation.FormBuilder.Extensions;
using Foundation.FormBuilder.Validation;
using Foundation.Web;
using Foundation.Web.Extensions;

namespace Foundation.FormBuilder.DynamicForm
{
    public class UiBuilderBase<TModel>
    {
        protected IElementGenerator ElementGenerator;

        protected Dictionary<string, PropertyInfo> Properties;



        protected List<FormElement> ExtractElementsToRender(TModel model)
        {
            var formElements = Properties.Select(p =>
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
                                                FieldValue = FieldValue(model, p),
                                                ValidationInfo = null
                                            })
                                    .Where(p => p.ControlSpecs != null)
                                    .ToList();

            foreach (var collectionItems in formElements.Where(x => x.CollectionInfo != null))
            {
                collectionItems.CollectionInfo.CollectionObject =
                    Properties[collectionItems.CollectionInfo.ListSourceMember]
                        .GetValue(model, null) as IEnumerable<SelectListItem>;
            }

            return formElements;
        }

        private static object FieldValue(TModel model, KeyValuePair<string, PropertyInfo> p)
        {
            if (model != null)
            {
                return p.Value.GetValue(model, null);
            }
            else
            {
                return null;
            }
        }



        public void BuildLayout(HtmlHelper<TModel> htmlHelper, BootstrapFormType formType, List<FormElement> formElements, NavHtmlTextWritter textWriter)
        {
            var groupsofElements = formElements.OrderBy(x => x.ControlSpecs.GroupName).GroupBy(x => x.ControlSpecs.GroupName);
            var useLegend = (formElements.Select(x => x.ControlSpecs.GroupName).Distinct().Count() > 1);

            foreach (var groupedElements in groupsofElements)
            {
                var groupName = (!String.IsNullOrEmpty(groupedElements.Key)) ? groupedElements.Key : "General";

                using (new ElementGroup(textWriter, groupName, useLegend))
                {
                    var elementsToRender = groupedElements.Where(x => x.ControlSpecs.ElementType != ElementType.Hidden)
                                                          .OrderBy(x => x.ControlSpecs.Order);

                    // loop over the attributes (ordered)..
                    foreach (var formElement in elementsToRender)
                    {
                        using (new ControlGroup(textWriter))
                        {
                            ControlGroup.RenderLabel(formType, formElement, textWriter);

                            using (new ControlContainer(textWriter, formType))
                            {
                                var validationAttributes = htmlHelper.GetValidationAttributes(formElement.PropertyInfo.Name);
                            
                                // ElementType
                                var elementBlock = ElementGenerator.RenderElement(formElement);

                                elementBlock.MergeAttributes(validationAttributes);
                                
                                textWriter.Write(elementBlock);
                            }
                        }
                    }
                }
            }
        }
    }
}