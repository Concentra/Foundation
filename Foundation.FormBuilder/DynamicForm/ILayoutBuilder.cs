using System.Web.Mvc;
using System.Web.UI;
using Foundation.FormBuilder.BootStrapSet;
using Foundation.Web.Extensions;

namespace Foundation.FormBuilder.DynamicForm
{
    public interface ILayoutBuilder
    {
        string BeginFieldCollection(string groupName, bool useLegend, IFormBuilderParameters formBuilderParameters);
        string EndFieldCollection(IFormBuilderParameters formBuilderParameters);

        string RenderLabel(FormElement formElement, IFormBuilderParameters formBuilderParameters);

        string BeginElementContainer(FormElement formElement, IFormBuilderParameters formBuilderParameters);
        string EndElementContainer(FormElement formElement, IFormBuilderParameters formBuilderParameters);

        string BeginControlContainer(FormElement formElement, IFormBuilderParameters bootStrapBuilderParameters);
        string EndControlContainer(FormElement formElement, IFormBuilderParameters bootStrapBuilderParameters);
    }
}