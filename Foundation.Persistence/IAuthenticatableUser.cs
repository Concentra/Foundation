using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Foundation.Persistence
{
    public interface IAuthenticatableUser
    {
        string EmailAddress { get; set; }
        string Password { get; set; }
        string PasswordSalt { get; set; }
        bool Disabled { get; set; }
        bool AccountLocked { get; set; }
        int FailedLoginAttempts { get; set; }
    }
}
