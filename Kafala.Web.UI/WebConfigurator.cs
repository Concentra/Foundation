using System;
using System.Configuration;
using Foundation.Web;

namespace Kafala.Web.UI
{
    public class WebConfigurator : IWebConfigurator
    {
        public int PageSize
        {
            get
            {
                var configuredPageSize = ConfigurationManager.AppSettings["Foundation_PageSize"];
                return string.IsNullOrEmpty(configuredPageSize) ? 15 : Convert.ToInt32(configuredPageSize);
            }
        }

        public string PageTitle
        {
            get
            {
                var configuredPageTitle = ConfigurationManager.AppSettings["Foundation_PageTitle"];
                return string.IsNullOrEmpty(configuredPageTitle) ? "Kafala System 1.0" : configuredPageTitle;
            }
        }
    }
}
