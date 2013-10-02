using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using Foundation.FormBuilder.DynamicForm;

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

        private void Render(MenuItem menuItem, NavHtmlTextWritter textWriter, bool isRoot = false)
        {
            if (menuItem.Children != null && menuItem.Children.Any())
            {
                textWriter.AddAttribute(HtmlTextWriterAttribute.Class, "dropdown");
            }

            if (menuItem.Divider)
            {
                textWriter.AddAttribute(HtmlTextWriterAttribute.Class, "divider");
            }

            textWriter.RenderBeginTag(HtmlTextWriterTag.Li);
           
            if (menuItem.Active)
            {
                textWriter.AddAttribute(HtmlTextWriterAttribute.Class, "active");
            }

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

        private void RenderLeafElement(MenuItem menuItem, NavHtmlTextWritter textWriter, bool dropDownToggle = false)
        {
           
             
                var link = new TagBuilder("a");
                menuItem.URL = string.IsNullOrEmpty(menuItem.URL) ? "#" : menuItem.URL;
                link.Attributes.Add("href", menuItem.URL);
                if (dropDownToggle)
                {
                    link.Attributes.Add("class", "dropdown-toggle");
                    link.Attributes.Add("data-toggle", "dropdown");
                }
                link.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(menuItem.HtmlAttributes));
                link.InnerHtml = menuItem.Text;
                textWriter.Write(link.ToString());
        }
    }
}
