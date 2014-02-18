using System;
using System.Collections.Generic;
using System.Linq;
using Foundation.FormBuilder.BootStrapSet;
using Foundation.FormBuilder.CustomAttribute;
using Foundation.Web.Extensions;

namespace Foundation.FormBuilder.DynamicForm
{
    public class FormBuilderBase
    {
        protected IElementGenerator ElementGenerator;

        protected void BuildForm(BootstrapFormType formType, List<FormElement> formElements, NavHtmlTextWritter textWriter)
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

                        RenderElement(formType, textWriter, formElement);
                    }
                }
            }
        }

        protected void RenderElement(BootstrapFormType formType, NavHtmlTextWritter textWriter, FormElement formElement)
        {
            using (new ControlGroup(textWriter, formElement))
            {
                ControlGroup.RenderLabel(formType, formElement, textWriter);

                using (new ControlContainer(textWriter, formType))
                {
                    var elementBlock = ElementGenerator.RenderElement(formElement);

                    elementBlock.MergeAttributes(formElement.ValidationAttributes);

                    textWriter.Write(elementBlock);
                }
            }
        }
    }
}