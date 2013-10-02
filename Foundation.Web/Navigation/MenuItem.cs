using System.Collections.Generic;

namespace Foundation.Web.Navigation
{
    public class MenuItem
    {
        public string Text {get; set;}

        public string URL { get; set; }

        public bool Disabled { get; set; }

        public bool Active { get; set; }

        public bool Divider { get; set; }

        public object HtmlAttributes { get; set; }

        public object RouteValues { get; set; }

        public IList<MenuItem> Children { get; set; }
    }
}
