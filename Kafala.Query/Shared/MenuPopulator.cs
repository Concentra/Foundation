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
                    new MenuItem() {Active = false, Text = "Donors", URL = "Donor"},
                    new MenuItem() {Active = false, Text = "Donation Cases", URL = "DonationCase"},
                    new MenuItem()
                    {
                        Active = false, Text = "Payments",
                        Children = new List<MenuItem>
                        {
                            new MenuItem() {Active = false, Text = "Find Payments" , URL = "Payment"},
                            new MenuItem() {Active = false, Text = "Create Payments" , URL = "Payment/Create"},
                        }
                    },
                    new MenuItem() {Active = false, Text = "Alerts", URL = "Alerts"},
                    new MenuItem() {
                        Active = false, Text = "Reports" ,
                        Children = new List<MenuItem>
                        {
                            new MenuItem() {Active = false, Text = "Over Due Payments", URL = "Reports/OverDue"},
                            new MenuItem() {Active = false, Text = "Collection Summary", URL = "Reports/Collection"},
                        }
                    },
                    new MenuItem() {
                        Active = false, Text = "Settings" ,
                        Children = new List<MenuItem>
                        {
                            new MenuItem() {Active = false, Text = "Payment Periods", URL = "PaymentPeriod"},
                            new MenuItem() {Divider = true},
                            new MenuItem() {Active = false, Text = "Users", URL = "User"}
                        }
                    }
                }
            };

            return menu;
        }
    }
}
