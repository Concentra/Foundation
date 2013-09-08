using System;
using System.Collections.Generic;
using System.Linq;
using Foundation.FormBuilder.Blocks;
using Foundation.FormBuilder.CustomAttribute;
using Foundation.FormBuilder.ElementGenerators;

namespace Foundation.FormBuilder.DynamicForm
{
    public class UiBuilderBase
    {
        protected IElementGenerator ElementGenerator;

        public void BuildLayout(BootstrapFormType formType, List<FormElement> formElements, NavHtmlTextWritter textWriter)
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

                            using (new ControlContainer(textWriter))
                            {
                                // ElementType
                                var elementBlock = ElementGenerator.RenderElement(formElement);
                                textWriter.Write(elementBlock);
                            }
                        }
                    }
                }
            }
        }
    }
}