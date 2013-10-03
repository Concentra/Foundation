using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafala.BusinessManagers.User
{
    public interface IUserContract
    {
       Guid Id { get; set; }

       string FirstName { get; set; }

       string LastName { get; set; }

       string EmailAddress { get; set; }

       string Telephone { get; set; }

       bool Disabled { get; set; }

       bool AccountLocked { get; set; }

       string Password { get; set; }

       string PasswordSalt { get; set; }

       DateTime PasswordExpirtyDate { get; set; }

       int FailedLoginAttempts { get; set; }
    }
}
