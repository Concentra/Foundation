using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Foundation.Web.Security
{
    public interface IPasswordEncoder
    {
        string EncodePassword(string Password, string Salt);
        string GenerateSalt();
    }
}
