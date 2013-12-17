using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;

namespace Foundation.Web.Extensions
{
    public class NavHtmlTextWritter : HtmlTextWriter
    {
        private Dictionary<HtmlTextWriterAttribute, List<string>> attrValues = new Dictionary<HtmlTextWriterAttribute, List<string>>();
        private readonly HtmlTextWriterAttribute[] multiValueAttrs = new[] { HtmlTextWriterAttribute.Class };

        public NavHtmlTextWritter(TextWriter writer) : base(writer) { }

        public override void AddAttribute(HtmlTextWriterAttribute key, string value)
        {
            if (multiValueAttrs != null && multiValueAttrs.Contains(key))
            {
                if (!this.attrValues.ContainsKey(key))
                    this.attrValues.Add(key, new List<string>());

                this.attrValues[key].Add(value);
            }
            else
            {
                base.AddAttribute(key, value);
            }
        }

        public override void RenderBeginTag(HtmlTextWriterTag tagKey)
        {
            this.AddMultiValuesAttrs();
            base.RenderBeginTag(tagKey);
            this.ClearAttributes();
        }

        public override void RenderBeginTag(string tagName)
        {
            this.AddMultiValuesAttrs();
            base.RenderBeginTag(tagName);
            this.ClearAttributes();
        }

        private void AddMultiValuesAttrs()
        {
            foreach (var key in this.attrValues.Keys)
                this.AddAttribute(key.ToString(), string.Join(" ", this.attrValues[key].ToArray()));

            this.attrValues = new Dictionary<HtmlTextWriterAttribute, List<string>>();
        }

        public void ClearAttributes()
        {
            this.attrValues.Clear();
        }
    }
}