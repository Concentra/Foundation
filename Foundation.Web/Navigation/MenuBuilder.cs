using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.UI;
using Foundation.Web.Extensions;

namespace Foundation.Web.Navigation
{
    public class MenuBuilder
    {
        public MvcHtmlString Build(MenuItem menu)
        {
            var sb = new StringBuilder();
            var stringWriter = new StringWriter(sb);
            using (var textWriter = new NavHtmlTextWritter(stringWriter))
            {
                using (new MenuContainer(textWriter)) foreach (var menuItem in menu.Children)
                {
                    Render(menuItem, textWriter);
                }

                return new MvcHtmlString(sb.ToString());
            }
        }

        private void Render(MenuItem menuItem, HtmlTextWriter textWriter, bool isRoot = false)
        {
            if (menuItem.Children != null && menuItem.Children.Any())
            {
                textWriter.AddAttribute(HtmlTextWriterAttribute.Class, "dropdown");
            }

            if (menuItem.Divider)
            {
                textWriter.AddAttribute(HtmlTextWriterAttribute.Class, "divider");
            }
            
            if (menuItem.Active)
            {
                textWriter.AddAttribute(HtmlTextWriterAttribute.Class, "active");
            }

            textWriter.RenderBeginTag(HtmlTextWriterTag.Li);
           
            if (menuItem.Children != null && menuItem.Children.Any())
            {
                RenderLeafElement(menuItem, textWriter, true); 
                using (new MenuList(textWriter, false))
                {
                    foreach (var childMenuItem in menuItem.Children)
                    {
                        Render(childMenuItem, textWriter);
                        
                    }
                }
            }
            else
            {
                RenderLeafElement(menuItem, textWriter);
            }

            textWriter.RenderEndTag();

        }

        private void RenderLeafElement(MenuItem menuItem, HtmlTextWriter textWriter, bool dropDownToggle = false)
        {
           
             
                var link = new TagBuilder("a");
                menuItem.URL = string.IsNullOrEmpty(menuItem.URL) ? "#" : "/" + menuItem.URL;
                link.Attributes.Add("href", menuItem.URL);
                if (dropDownToggle)
                {
                    link.MergeAttribute("class", "dropdown-toggle");
                    link.MergeAttribute("data-toggle", "dropdown");
                }
                link.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(menuItem.HtmlAttributes));
                link.InnerHtml = menuItem.Text;
                textWriter.Write(link.ToString());
        }
    }
}
