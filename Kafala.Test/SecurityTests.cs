using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foundation.Web.Security;
using Kafala.BusinessManagers.Donor;
using Kafala.BusinessManagers.User;
using Kafala.Entities.Enums;
using Moq;
using NUnit.Framework;
using StructureMap;

namespace Kafala.Test
{
    [TestFixture]
    class SecurityTests : BaseTextFixture
    {
        [Test]
        public void CreateUserAndSignIn()
        {
            var email = Faker.Internet.Email();
            var userMock = new Mock<IUserContract>();
            userMock.SetupProperty(x => x.FirstName, Faker.Name.First());
            userMock.SetupProperty(x => x.LastName, Faker.Name.Last());
            userMock.SetupProperty(x => x.EmailAddress, email);
            userMock.SetupProperty(x => x.Telephone, Faker.Phone.Number());
            userMock.SetupProperty(x => x.Password, email);
            var ubm = bmc.Get<UserManager>();
            ubm.RegisterUser(userMock.Object);

            var authenticationService = ObjectFactory.GetInstance<IAuthenticationService>();
            var result = authenticationService.SignIn(email, email);
            Assert.AreEqual(SignInResult.Success, result);
        }
    }
}
