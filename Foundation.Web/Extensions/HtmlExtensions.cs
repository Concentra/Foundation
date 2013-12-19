using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using StructureMap;

namespace Foundation.Web.Extensions
{
    public static class HtmlExtensions
    {
        private static readonly ResourceManager WebPageTitlesResourceManager;

        private static readonly ResourceManager HelpMessagesResourceManager;
        private static readonly string DefaultPageTitle;

        private static IResourcesLocator resourcesLocator = ObjectFactory.GetInstance<IResourcesLocator>();

        static HtmlExtensions()
        {
            if (resourcesLocator.PageTitleResourceManager != null)
                WebPageTitlesResourceManager = resourcesLocator.PageTitleResourceManager;

            if (resourcesLocator.HelpResourceManager != null)
                HelpMessagesResourceManager = resourcesLocator.HelpResourceManager;

            DefaultPageTitle = resourcesLocator.DefaultPageTitle;
        }


        public static MvcHtmlString RenderFlashMessages(this HtmlHelper htmlHelper)
        {
            var flashMessenger = htmlHelper.ViewContext.TempData["FlashMessenger"] as WebFlashMessenger;

            if (flashMessenger != null)
            {
                return flashMessenger.RenderFlashMessages();
            }
            else
            {
                return null;
            }
        }

        public static MvcHtmlString CssInclude(this HtmlHelper htmlHelper, string filename, CssMediaType mediaType = CssMediaType.All)
        {
            var helper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var filepath = filename.StartsWith("~") ? filename : "~/content/css/" + filename;
            var url = helper.Content(filepath);

            return MvcHtmlString.Create(string.Format("<link href=\"{0}\" rel=\"stylesheet\" type=\"text/css\" media=\"{1}\" />", url.ToLower(), mediaType.ToString().ToLower()));
        }

        public static MvcHtmlString ScriptInclude(this HtmlHelper htmlHelper, string filename)
        {
            var filepath = "~/Scripts/";
            filepath += htmlHelper.ViewContext.HttpContext.IsDebuggingEnabled ? "Debug" : "Release";
            filepath += "/" + filename;

            var helper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var url = helper.Content(filepath);

            return MvcHtmlString.Create(string.Format("<script type=\"text/javascript\" src=\"{0}\"></script>", url.ToLower()));
        }

        public static MvcHtmlString ScriptIncludeThirdParty(this HtmlHelper htmlHelper, string filename)
        {
            // read a subfolder from config, or empty
            var thirdPartyScriptsFolder = ConfigurationManager.AppSettings["Foundation_ThirdPartyScripts"] ?? string.Empty;
            var filepath = "~/Scripts/";
            // filepath += htmlHelper.ViewContext.HttpContext.IsDebuggingEnabled ? "Debug" : "Release" + "/";
            filepath += thirdPartyScriptsFolder + filename;

            var helper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var url = helper.Content(filepath);

            return MvcHtmlString.Create(string.Format("<script type=\"text/javascript\" src=\"{0}\"></script>", url.ToLower()));
        }

        public static MvcHtmlString Image(
            this HtmlHelper htmlHelper,
            string filename,
            string altText = null,
            string title = null,
            string @class = null)
        {
            return MvcHtmlString.Create(ImageMaker(htmlHelper, filename, altText, title, @class));
        }

        public static string ImageMaker(
            this HtmlHelper htmlHelper,
            string filename,
            string altText = null,
            string title = null,
            string @class = null)
        {
            var filepath = "~/Content/Images/" + filename;
            var helper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var url = helper.Content(filepath);

            var tagBuilder = new TagBuilder("img");
            tagBuilder.Attributes.Add("src", url.ToLower());

            if (!string.IsNullOrWhiteSpace(altText))
            {
                tagBuilder.Attributes.Add("alt", htmlHelper.Encode(altText));
            }

            if (!string.IsNullOrWhiteSpace(title))
            {
                tagBuilder.Attributes.Add("title", htmlHelper.Encode(title));
            }

            if (!string.IsNullOrWhiteSpace(@class))
            {
                tagBuilder.Attributes.Add("class", @class);
            }

            return tagBuilder.ToString(TagRenderMode.SelfClosing);
        }

        public static string If(this HtmlHelper htmlHelper, bool test, string text)
        {
            return test ? text : string.Empty;
        }

        public static MvcHtmlString ModalConfirmDialogContainer(this HtmlHelper htmlHelper, string message, string dialogId)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<div style='display:none'>");
            sb.AppendLine("<div id='" + dialogId + "' style='padding:10px; background:#fff;'><div style='margin-top:30px'>");
            sb.AppendLine("<span class='confirmationMessage'>" + message + "</span>");
            sb.AppendLine("<div class='controls'>");
            sb.AppendLine("<button class='blue' id='yesBtn'>Yes</button>");
            sb.AppendLine("<button id='noBtn'>No</button>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div></div></div>");
            return new MvcHtmlString(sb.ToString());
        }

        public static MvcHtmlString ModalInformationDialogContainer(this HtmlHelper htmlHelper, string message, string dialogId)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<div style='display:none'>");
            sb.AppendLine("<div id='" + dialogId + "' style='padding:10px; background:#fff;'><div style='margin-top:30px'>");
            sb.AppendLine("<span class='confirmationMessage'>" + message + "</span>");
            sb.AppendLine("<div class='controls'>");
            sb.AppendLine("<button class='blue' id='okBtn'>Ok</button>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div></div></div>");
            return new MvcHtmlString(sb.ToString());
        }

        public static MvcPanel BeginPanel(this HtmlHelper htmlHelper, string title, string blockClass = "purple")
        {
            var writer = htmlHelper.ViewContext.Writer;
            writer.WriteLine("<div class=\"block " + blockClass + "\">");
            if (!string.IsNullOrWhiteSpace(title))
            {
                writer.WriteLine("<h3 class=\"header\">" + title + "</h3>");
            }

            writer.WriteLine("<div class=\"main\">");
            return new MvcPanel(htmlHelper);
        }

        
        public static MvcPanel BeginTabbedPanel(this HtmlHelper htmlHelper, IEnumerable<MvcPanelTab> tabs)
        {
            var writer = htmlHelper.ViewContext.Writer;
            writer.WriteLine("<div id=\"tabs\">");
            writer.WriteLine("<ul>");

            foreach (var tab in tabs)
            {
                writer.WriteLine("<li class=\"" + (tab.IsActive ? " active" : string.Empty) + "\">");
                writer.WriteLine(htmlHelper.ActionLink(tab.Text, tab.Action, tab.RouteValues));
                writer.WriteLine("</li>");
            }

            writer.WriteLine("</ul>");
            writer.WriteLine("<div class=\"tabPanel\">");

            return new MvcPanel(htmlHelper);
        }

        public static void EndPanel(this HtmlHelper htmlHelper)
        {
            MvcPanel.EndPanel(htmlHelper);
        }

        public static void EndTabbedPanel(this HtmlHelper htmlHelper)
        {
            MvcPanel.EndPanel(htmlHelper);
        }

        public static MvcHtmlString PageTitle(this HtmlHelper htmlHelper)
        {
            var model = htmlHelper.ViewContext.ViewData.Model;
            string title = null;

            if (model != null)
            {
                var modelType = model.GetType();
                var @namespace = modelType.Namespace ?? string.Empty;
                @namespace = @namespace.Substring(@namespace.LastIndexOf('.') + 1);
                var pageName = @namespace + "_" + modelType.Name.Replace("ViewModel", string.Empty);

                if (WebPageTitlesResourceManager != null)
                {
                    title = WebPageTitlesResourceManager.GetString(pageName);
                }

            }

            return MvcHtmlString.Create(title ?? DefaultPageTitle);
        }

     

        public static MvcHtmlString YesNo(this HtmlHelper htmlHelper, bool? value)
        {
            return MvcHtmlString.Create((value ?? false) ? "Yes" : "No");
        }

        public static MvcHtmlString YesNo(this HtmlHelper htmlHelper, bool? value, string nullValueText)
        {
            string result = nullValueText;
            if (value.HasValue)
            {
                result = value.Value ? "Yes" : "No";
            }

            return MvcHtmlString.Create(result);
        }

        public static MvcHtmlString HelpText(this HtmlHelper htmlHelper, string text)
        {
            if (HelpMessagesResourceManager != null)
            {
                var helpText = HelpMessagesResourceManager.GetString(text);

                return string.IsNullOrWhiteSpace(helpText) ? MvcHtmlString.Empty : htmlHelper.Image("help-icon.gif", "help", helpText, "helpText");
            }
            else
            {
                return MvcHtmlString.Empty;
            }
        }

        public static string HelpTextHtml(this HtmlHelper htmlHelper, string text)
        {
            if (HelpMessagesResourceManager != null)
            {
                var helpText = HelpMessagesResourceManager.GetString(text);

                return string.IsNullOrWhiteSpace(helpText) ? string.Empty : HelpMessagesResourceManager.GetString(text);
            }
            else
            {
                return String.Empty;
            } 
        }

        public static MvcHtmlString HelpTextFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            var propertyName = ExpressionHelper.GetExpressionText(expression);

            return htmlHelper.HelpText(propertyName);
        }

        public static MvcHtmlString ValueOrText(this decimal value, string text)
        {
            string result = text;

            if (value > 0)
            {
                result = Math.Round(value, 2, MidpointRounding.AwayFromZero).ToString();
            }

            return MvcHtmlString.Create(result);
        }

        public static MvcHtmlString RoundItOrText(this object value, string text, int decimalPlaces)
        {
            string result = text;

            if (value != null)
            {
                if (value.GetType() == typeof(decimal))
                {
                    result = Math.Round((decimal)value, decimalPlaces, MidpointRounding.AwayFromZero).ToString();
                }
                else if (value.GetType() == typeof(double))
                {
                    result = Math.Round((double)value, decimalPlaces, MidpointRounding.AwayFromZero).ToString();
                }
                else if (value.GetType() == typeof(float))
                {
                    result = Math.Round((float)value, decimalPlaces, MidpointRounding.AwayFromZero).ToString();
                }
                else
                {
                    result = value.ToString();
                }
            }

            return MvcHtmlString.Create(result);
        }

        public static MvcHtmlString RenderDate(this HtmlHelper html, DateTime? date, string dateFormat)
        {
            string result = string.Empty;
            if (date.HasValue)
            {
                result = RenderDate(date.Value, dateFormat);
            }

            return MvcHtmlString.Create(result);
        }

        public static MvcHtmlString RenderDate(this DateTime? date, string dateFormat)
        {
            string result = string.Empty;
            if (date.HasValue && date > DateTime.MinValue)
            {
                result = RenderDate(date.Value, dateFormat);
            }

            return MvcHtmlString.Create(result);
        }

        public static MvcHtmlString RenderDate(this DateTime? date)
        {
            string result = string.Empty;
            if (date.HasValue && date > DateTime.MinValue)
            {
                return date.Value.RenderDate();
            }

            return MvcHtmlString.Create(result);
        }

        public static MvcHtmlString RenderDate(this DateTime date)
        {
            return MvcHtmlString.Create(RenderDate(date, "dd/MM/yyyy"));
        }

        public static MvcHtmlString RenderTime(this DateTime? date)
        {
            string result = string.Empty;
            if (date.HasValue && date > DateTime.MinValue)
            {
                result = date.Value.ToShortTimeString();
            }

            return MvcHtmlString.Create(result);
        }

        private static string RenderDate(DateTime date, string dateFormat)
        {
            return date.ToString(dateFormat);
        }

        /// <summary>
        /// Multi drop down control
        /// </summary>
        /// <param name="h">htmlHelper auxillary object.</param>
        /// <param name="name">Name and Id for the drop down control.</param>
        /// <param name="items">The list of all the available items.</param>
        /// <param name="selectedItems">The list of the the currently selected items.</param>
        /// <param name="otherOption">The string of the "other" value, if present. Otherwise null or empty string.</param>
        /// <param name="hasOther">Whether control has an option for "Other" or not.</param>
        /// <param name="htmlAttributes">Optional, additional html attributes.</param>
        /// <returns>The html string with the control.</returns>
        public static MvcHtmlString MultiDropDown(
            this HtmlHelper h,
            string name,
            IEnumerable<SelectListItem> items,
            IEnumerable<SelectListItem> selectedItems,
            string otherOption,
            bool hasOther = true,
            object htmlAttributes = null)
        {
            var container = new TagBuilder("table");
            container.MergeAttribute("class", "multiSelectContainer");
            var firstRow = new TagBuilder("tr");
            var firstCell = new TagBuilder("td");
            firstCell.MergeAttribute("style", "text-align: left !important");
            
            var secondCell = new TagBuilder("td");
            secondCell.MergeAttribute("style", "text-align: left !important");
            
            var select = new TagBuilder("select");

            var options = string.Empty;
            TagBuilder option;
            IDictionary<string, object> attributes = new RouteValueDictionary(htmlAttributes);
            foreach (var attr in attributes)
            {
                select.Attributes.Add(attr.Key, attr.Value.ToString());
            }

            if (selectedItems != null)
            {
                items = from it in items
                        where !selectedItems.Any(sit => sit.Value == it.Value)
                        select it;
            }

            option = new TagBuilder("option");
            option.MergeAttribute("value", string.Empty);
            option.SetInnerText(string.Empty);
            options += option.ToString(TagRenderMode.Normal) + "\n";

            foreach (var item in items)
            {
                option = new TagBuilder("option");
                option.MergeAttribute("value", item.Value.ToString());
                option.SetInnerText(item.Text);
                options += option.ToString(TagRenderMode.Normal) + "\n";
            }

            if (hasOther && string.IsNullOrEmpty(otherOption))
            {
                option = new TagBuilder("option");
                option.MergeAttribute("value", Guid.Empty.ToString());
                option.SetInnerText("Other...");
                options += option.ToString(TagRenderMode.Normal) + "\n";
            }

            var deleteButton = new TagBuilder("input");
            deleteButton.MergeAttribute("value", "Delete");
            deleteButton.MergeAttribute("class", "deleteValue");
            deleteButton.MergeAttribute("type", "image");
            deleteButton.MergeAttribute("src", "/Content/Images/icons/minus-icon.png");
            deleteButton.MergeAttribute("title", "Delete");
            deleteButton.MergeAttribute("width", "16");
            deleteButton.MergeAttribute("height", "16");

            var addButton = new TagBuilder("input");
            addButton.MergeAttribute("value", "Add");
            addButton.MergeAttribute("class", "addValue");
            addButton.MergeAttribute("type", "image");
            addButton.MergeAttribute("src", "/Content/Images/icons/plus-icon.png");
            addButton.MergeAttribute("width", "16");
            addButton.MergeAttribute("height", "16");
            addButton.MergeAttribute("title", "Add");

            var selectedRows = string.Empty;
            if (selectedItems != null)
            {
                foreach (var item in selectedItems)
                {
                    var row = new TagBuilder("tr");
                    var cell1 = new TagBuilder("td");
                    cell1.MergeAttribute("style", "text-align: left !important");
                    var cell2 = new TagBuilder("td");
                    cell2.MergeAttribute("style", "text-align: left !important");
                    
                    var hiddenInput = new TagBuilder("input");
                    hiddenInput.MergeAttribute("class", "hiddenValue");
                    hiddenInput.MergeAttribute("type", "hidden");
                    hiddenInput.MergeAttribute("name", name + "_Guid");
                    hiddenInput.MergeAttribute("value", item.Value.ToString());

                    var textLabel = new TagBuilder("label");
                    textLabel.InnerHtml = item.Text;
                    cell1.InnerHtml = textLabel.ToString(TagRenderMode.Normal);
                    cell1.InnerHtml += hiddenInput.ToString(TagRenderMode.Normal);
                    cell2.InnerHtml = deleteButton.ToString(TagRenderMode.Normal) + "<span class=\"removelink\"> Remove </span> \n";
                    cell2.Attributes.Add("width", "60px");
                    row.InnerHtml = cell1.ToString(TagRenderMode.Normal) + "\n";
                    row.InnerHtml += cell2.ToString(TagRenderMode.Normal) + "\n";
                    selectedRows += row.ToString(TagRenderMode.Normal) + "\n";
                }
            }

            select.MergeAttribute("id", name);
            select.MergeAttribute("name", name);
            select.MergeAttribute("class", "multiselect");
            select.InnerHtml = options;

            var lastRow = new TagBuilder("tr");
            var lastCell1 = new TagBuilder("td");
            lastCell1.MergeAttribute("style", "text-align: left !important");
            
            var lastCell2 = new TagBuilder("td");
            lastCell2.MergeAttribute("style", "text-align: left !important");
            
            var otherInput = new TagBuilder("input");
            otherInput.MergeAttribute("class", "otherValue");
            otherInput.MergeAttribute("type", "text");

            firstCell.InnerHtml = select.ToString(TagRenderMode.Normal) + "\n";
            firstRow.InnerHtml = firstCell.ToString(TagRenderMode.Normal) + "\n";
            firstRow.InnerHtml += secondCell.ToString(TagRenderMode.Normal) + "\n";

            if (hasOther && !string.IsNullOrEmpty(otherOption))
            {
                var otherRow = new TagBuilder("tr");
                otherRow.MergeAttribute("class", "externalValue");
                var otherCell1 = new TagBuilder("td");
                otherCell1.MergeAttribute("style", "text-align: left !important");
            
                var otherCell2 = new TagBuilder("td");
                otherCell2.MergeAttribute("style", "text-align: left !important");
            
                var otherLabel = new TagBuilder("label");
                otherLabel.InnerHtml = otherOption;
                otherCell1.InnerHtml = otherLabel.ToString(TagRenderMode.Normal);

                var otherHiddenInput = new TagBuilder("input");
                otherHiddenInput.MergeAttribute("class", "hiddenValue");
                otherHiddenInput.MergeAttribute("type", "hidden");
                otherHiddenInput.MergeAttribute("name", name + "Other");
                otherHiddenInput.MergeAttribute("value", otherOption);

                otherCell1.InnerHtml += otherHiddenInput.ToString(TagRenderMode.Normal);
                otherCell2.InnerHtml = deleteButton.ToString(TagRenderMode.Normal) + "\n";
                otherRow.InnerHtml = otherCell1.ToString(TagRenderMode.Normal) + "\n";
                otherRow.InnerHtml += otherCell2.ToString(TagRenderMode.Normal) + "\n";
                selectedRows += otherRow.ToString(TagRenderMode.Normal) + "\n";
            }

            if (hasOther)
            {
                var otherMessage = new TagBuilder("label") { InnerHtml = "Please give the details below" };
                lastCell1.InnerHtml = otherMessage.ToString(TagRenderMode.Normal) + "<br/>\n";
                lastCell1.InnerHtml += otherInput.ToString(TagRenderMode.Normal) + "\n";

                addButton.Attributes["class"] = "addOtherValue";
                lastCell2.InnerHtml = addButton.ToString(TagRenderMode.Normal) + "\n";
                lastRow.InnerHtml = lastCell1.ToString(TagRenderMode.Normal) + "\n";
                lastRow.InnerHtml += lastCell2.ToString(TagRenderMode.Normal) + "\n";
            }

            container.InnerHtml = firstRow.ToString(TagRenderMode.Normal) + "\n";
            container.InnerHtml += selectedRows + "\n";

            if (hasOther)
            {
                container.InnerHtml += lastRow.ToString(TagRenderMode.Normal) + "\n";
            }

            return MvcHtmlString.Create(container.ToString(TagRenderMode.Normal));
        }

        /// <summary>
        /// Single drop down control
        /// </summary>
        /// <param name="h">htmlHelper auxillary object.</param>
        /// <param name="name">Name and Id for the drop down control.</param>
        /// <param name="items">The list of all the available items.</param>
        /// <param name="selectedItem">The guid of the currently selected item, otherwise null</param>
        /// <param name="otherOption">The string of the "other" value, if present. Otherwise null or empty string.</param>
        /// <param name="htmlAttributes">Optional, additional html attributes for the list.</param>
        /// <param name="textBoxHtmlAttributes">Optional, additional html attributes for the textbox.</param>
        /// <returns>The html string with the control.</returns>
        public static MvcHtmlString SingleDropDown(
             this HtmlHelper h,
             string name,
             IEnumerable<SelectListItem> items,
             Guid? selectedItem,
             string otherOption,
             object htmlAttributes = null,
             object textBoxHtmlAttributes = null)
        {
            var container = new TagBuilder("table");
            container.MergeAttribute("class", "singleSelectContainer");
            var firstRow = new TagBuilder("tr");
            var firstCell = new TagBuilder("td");
            firstCell.MergeAttribute("style", "text-align: left !important");
            firstCell.MergeAttribute("colspand", "2");

            var select = new TagBuilder("select");

            var options = string.Empty;
            TagBuilder option;
            IDictionary<string, object> attributes = new RouteValueDictionary(htmlAttributes);
            foreach (var attr in attributes)
            {
                select.Attributes.Add(attr.Key, attr.Value.ToString());
            }

            option = new TagBuilder("option");
            option.MergeAttribute("value", string.Empty);
            option.SetInnerText(string.Empty);
            options += option.ToString(TagRenderMode.Normal) + "\n";

            foreach (var item in items)
            {
                option = new TagBuilder("option");
                option.MergeAttribute("value", item.Value.ToString());
                if (selectedItem.HasValue && selectedItem.Value.ToString() == item.Value && string.IsNullOrEmpty(otherOption))
                {
                    option.MergeAttribute("selected", "selected");
                }

                option.SetInnerText(item.Text);
                options += option.ToString(TagRenderMode.Normal) + "\n";
            }

            option = new TagBuilder("option");
            option.MergeAttribute("value", Guid.Empty.ToString());
            option.SetInnerText("Other...");

            if (!string.IsNullOrEmpty(otherOption))
            {
                option.MergeAttribute("selected", "selected");
            }

            options += option.ToString(TagRenderMode.Normal) + "\n";

            select.MergeAttribute("id", name);
            select.MergeAttribute("name", name);
            select.MergeAttribute("class", "singleselect");
            if (!string.IsNullOrEmpty(otherOption))
            {
                select.MergeAttribute("disabled", "disabled");
            }

            select.InnerHtml = options;

            var cancelButton = new TagBuilder("input");
            cancelButton.MergeAttribute("value", "Cancel");
            cancelButton.MergeAttribute("class", "cancelValue");
            cancelButton.MergeAttribute("type", "image");
            cancelButton.MergeAttribute("src", "/Content/Images/icons/minus-icon.png");
            cancelButton.MergeAttribute("title", "Cancel");
            cancelButton.MergeAttribute("width", "16");
            cancelButton.MergeAttribute("height", "16");

            var lastRow = new TagBuilder("tr");
            var lastCell1 = new TagBuilder("td");
            lastCell1.MergeAttribute("style", "text-align: left !important");
            
            var lastCell2 = new TagBuilder("td");
            lastCell2.MergeAttribute("style", "text-align: left !important");
            
            var otherInput = new TagBuilder("input");

            IDictionary<string, object> txtBoxattributes = new RouteValueDictionary(textBoxHtmlAttributes);
            foreach (var attr in txtBoxattributes)
            {
                otherInput.Attributes.Add(attr.Key, attr.Value.ToString());
            }

            otherInput.MergeAttribute("class", "otherValue");
            otherInput.MergeAttribute("type", "text");
            otherInput.MergeAttribute("name", name + "Other");

            firstCell.InnerHtml = select.ToString(TagRenderMode.Normal) + "\n";
            firstRow.InnerHtml = firstCell.ToString(TagRenderMode.Normal) + "\n";

            if (!string.IsNullOrEmpty(otherOption))
            {
                otherInput.MergeAttribute("value", otherOption);
            }

            var otherMessage = new TagBuilder("label") { InnerHtml = "Please give the details below" };
            lastCell1.InnerHtml = otherMessage.ToString(TagRenderMode.Normal) + "<br/>\n";

            lastCell1.InnerHtml += otherInput.ToString(TagRenderMode.Normal) + "\n";
            lastCell2.InnerHtml = cancelButton.ToString(TagRenderMode.Normal) + "<span class=\"removelink\"> Remove </span> \n";
            lastRow.InnerHtml = lastCell1.ToString(TagRenderMode.Normal) + "\n";
            lastRow.InnerHtml += lastCell2.ToString(TagRenderMode.Normal) + "\n";

            container.InnerHtml = firstRow.ToString(TagRenderMode.Normal) + "\n";
            container.InnerHtml += lastRow.ToString(TagRenderMode.Normal) + "\n";

            return MvcHtmlString.Create(container.ToString(TagRenderMode.Normal));
        }

        
        
    }
}
