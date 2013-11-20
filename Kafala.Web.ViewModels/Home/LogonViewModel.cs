using System;
using System.Collections.Generic;
using System.Text;

namespace Kafala.Web.ViewModels.Home
{
    public class LogOnViewModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string ReturnURL { get; set; }
        
        public bool RememberMe { get; set; }
    }
}
