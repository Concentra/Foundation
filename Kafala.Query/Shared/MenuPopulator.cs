using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foundation.Infrastructure.Query;
using Foundation.Web.Navigation;

namespace Kafala.Query.Shared
{
    public class MenuPopulator : IQuery<string, MenuItem>
    {
        public MenuItem Execute(string id)
        {
            var menu = new MenuItem
            {
                Children = new List<MenuItem>
                {
                    new MenuItem() {Active = false, Text = "Donors"},
                    new MenuItem() {Active = false, Text = "Donation Cases"},
                    new MenuItem() {Active = false, Text = "Payments"}
                }
            };

            return menu;
        }
    }
}
