using System;
using Faker.Extensions;
using Foundation.Infrastructure;
using Foundation.Infrastructure.BL;
using Foundation.Infrastructure.Query;
using Foundation.Persistence;
using Foundation.Web;
using Kafala.BusinessManagers;
using Kafala.BusinessManagers.Commitment;
using Kafala.BusinessManagers.DonationCase;
using Kafala.BusinessManagers.Donor;
using Kafala.Entities.DoNotMap;
using Kafala.Entities.Enums;
using Moq;
using NHibernate;
using NUnit.Framework;
using StructureMap;

namespace Kafala.Test
{
    [TestFixture]
    public class Donors : BaseTextFixture
    {
        [SetUp]
        public void ClearDonors()
        {
         
            Session.CreateQuery("delete from Donor").ExecuteUpdate();
        }

        [Test(Description = "Populates the system with 3 donors , 3 cases each , 5 payments each")]

        public void CreateDonors()
        {
            for (var d = 0; d < 50; ++d)
            {
                var donorMoq = MockDonor();
                var donorId = DonorBusinessManager.Add(donorMoq.Object);
                for (var dc = 0; dc < 2; dc++)
                {
                    var donationCaseMock = MockDonationCase();
                    var donationCaseId = DonationCaseBusinessManager.Add(donationCaseMock.Object);
                    var commitmentMock = MockCommitment(donorId, donationCaseId);
                    var commitmentId = CommitmentBusinessManager.Add(commitmentMock.Object);
                    for (var payment = 0; payment < 2; payment++)
                    {
                        var paymentMock = MockPayment(commitmentId, PaymentPeriodList[payment].Id, (int) commitmentMock.Object.Amount);
                        PaymentBusinessManager.Register(paymentMock.Object);
                    }
                }

            }
        }
    }
}
