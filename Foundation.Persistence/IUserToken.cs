using System;
using System.Collections.Generic;
using System.Text;

namespace Foundation.Persistence
{
    public interface IUserToken
    {
        string EmailAddress { get;  }
        string Password { get; set; }
        string PasswordSalt { get; set; }
        bool Disabled { get; set; }
        bool AccountLocked { get; set; }
        int FailedLoginAttempts { get; set; }
        string UserName { get;}
    }
}
