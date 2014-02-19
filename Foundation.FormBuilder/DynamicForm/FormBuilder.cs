using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Foundation.FormBuilder.BootStrapSet;
using Foundation.FormBuilder.CustomAttribute;
using Foundation.Web.Extensions;

namespace Foundation.FormBuilder.DynamicForm
{
    public class FormBuilder
    {
        private readonly IElementGenerator elementGenerator;
        private readonly ILayoutBuilder layoutBuilder;


        public FormBuilder(IElementGenerator elementGenerator, ILayoutBuilder layoutBuilder)
        {
            this.elementGenerator = elementGenerator;
            this.layoutBuilder = layoutBuilder;
        }

        public string BuildForm(List<FormElement> formElements, IFormBuilderParameters formBuilderParameters)
        {
            var sb = new StringBuilder();
            sb.Append(RenderHiddenFields(formElements));
               
            var groupsofElements = formElements.OrderBy(x => x.ControlSpecs.GroupName).GroupBy(x => x.ControlSpecs.GroupName);
            var useLegend = (formElements.Select(x => x.ControlSpecs.GroupName).Distinct().Count() > 1);

            foreach (var groupedElements in groupsofElements)
            {
                var groupName = (!String.IsNullOrEmpty(groupedElements.Key)) ? groupedElements.Key : "General";

                sb.Append(this.layoutBuilder.BeginFieldCollection(groupName, useLegend, formBuilderParameters));
                {
                    var elementsToRender = groupedElements.Where(x => x.ControlSpecs.ElementType != ElementType.Hidden)
                                                          .OrderBy(x => x.ControlSpecs.Order);

                    // loop over the attributes (ordered)..
                    foreach (var formElement in elementsToRender)
                    {
                       sb.Append(this.RenderElement(formElement, formBuilderParameters));
                    }
                }
                sb.Append(this.layoutBuilder.EndFieldCollection(formBuilderParameters));
               
            }

            return sb.ToString();
        }

        public string RenderElement(FormElement formElement, IFormBuilderParameters formBuilderParameters)
        {
            var sb = new StringBuilder();
            
            sb.Append(this.layoutBuilder.BeginElementContainer(formElement, formBuilderParameters));
            {
                sb.Append(this.layoutBuilder.RenderLabel(formElement, formBuilderParameters));
                sb.Append(this.layoutBuilder.BeginControlContainer(formElement, formBuilderParameters));
                {
                    var elementBlock = this.elementGenerator.RenderElement(formElement);

                    elementBlock.MergeAttributes(formElement.ValidationAttributes);

                    sb.Append(elementBlock);
                }
                sb.Append(this.layoutBuilder.EndControlContainer(formElement, formBuilderParameters));
            }
            sb.Append(this.layoutBuilder.EndElementContainer(formElement, formBuilderParameters));

            return sb.ToString();
        }

        private string RenderHiddenFields(IEnumerable<FormElement> formElements)
        {
            var hiddenBlock = new StringBuilder();
            foreach (var formElement in formElements)
            {
                var hiddenAttribute = formElement.ControlSpecs;

                if (hiddenAttribute != null && hiddenAttribute.ElementType == ElementType.Hidden)
                {
                    hiddenBlock.Append(elementGenerator.RenderElement(formElement));
                }
            }

            return hiddenBlock.ToString();
        }
    }
}