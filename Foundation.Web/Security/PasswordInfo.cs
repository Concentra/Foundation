using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Foundation.Web.Security
{
    public class PasswordInfo
    {
        public string EncryptedPassword { get; set; }
        public string Salt { get; set; }
    }
}
