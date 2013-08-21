using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation.Persistence;

namespace Kafala.Entities
{
    public class User : IAuthenticatableUser
    {
        public virtual Guid Id { get; set; }

        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual string EmailAddress { get; set; }

        public virtual string Telephone { get; set; }

        public virtual bool Disabled { get; set; }

        public virtual bool AccountLocked { get; set; }

        public virtual string Password { get; set; }

        public virtual string PasswordSalt { get; set; }

        public virtual DateTime PasswordExpirtyDate { get; set; }

        public virtual int FailedLoginAttempts { get; set; }
    }
}
