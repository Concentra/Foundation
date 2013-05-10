namespace Foundation.Web.Extensions
{
    public class MvcPanelTab
    {
        public MvcPanelTab(string text, string action, bool isActive = false, object routeValues = null)
        {
            this.Text = text;
            this.Action = action;
            this.IsActive = isActive;
            this.RouteValues = routeValues;
        }

        public string Text { get; private set; }
        
        public string Action { get; private set; }

        public object RouteValues { get; private set; }

        public bool IsActive { get; private set; }
    }
}
