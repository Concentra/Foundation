using System;
using System.Linq;
using Foundation.Infrastructure.BL;
using Kafala.BusinessManagers.Commitment;
using Kafala.BusinessManagers.DonationCase;
using Kafala.BusinessManagers.Donor;
using Kafala.BusinessManagers.Payment;
using Kafala.BusinessManagers.PaymentPeriod;
using Kafala.Entities;
using Kafala.Entities.Enums;
using Moq;
using NHibernate;
using NHibernate.Linq;
using NUnit.Framework;
using StructureMap;

namespace Kafala.Test
{
    public class BaseTextFixture
    {
        protected IBusinessManagerContainer bmc;

        protected DonorBusinessManager DonorBusinessManager;
        protected CommitmentBusinessManager CommitmentBusinessManager;
        protected DonationCaseBusinessManager DonationCaseBusinessManager;
        protected PaymentBusinessManager PaymentBusinessManager;
        protected PaymentPeriodBusinessManager PaymentPeriodBusinessManager;
        protected ISession Session;
        protected PaymentPeriod[] PaymentPeriodList;

        private void FillPaymentPeriods(int year)
        {
            for (int i = 1; i < 13; i++)
            {
                var periodSample = new DateTime(year, month: i, day: 1);
                
                PaymentPeriodBusinessManager.Add(periodSample.Year, periodSample.Month,
                    periodSample.ToString("yyyy MMMM"));

            }
        }

        [TestFixtureSetUp]
        public void InitializatFixture()
        {
            ObjectFactory.Configure(Configurations.ConfigureDependencies);
            bmc = ObjectFactory.GetInstance<BusinessManagerContainer>();
            Session = ObjectFactory.GetInstance<ISession>();
            CommitmentBusinessManager = bmc.Get<CommitmentBusinessManager>();
            DonorBusinessManager = bmc.Get<DonorBusinessManager>();
            DonationCaseBusinessManager = bmc.Get<DonationCaseBusinessManager>();
            PaymentBusinessManager = bmc.Get<PaymentBusinessManager>();
            PaymentPeriodBusinessManager = bmc.Get<PaymentPeriodBusinessManager>();
            FillPaymentPeriods(2013); PaymentPeriodList = Session.Query<Entities.PaymentPeriod>().ToArray();
        }

        public Mock<IDonorContract> MockDonor()
        {
            var donorName = Faker.Name.FullName();
            var mock = new Mock<IDonorContract>();
            mock.SetupProperty(x => x.Name, donorName);
            mock.SetupProperty(x => x.Email, Faker.Internet.Email(donorName));
            mock.SetupProperty(x => x.Mobile, Faker.Phone.Number());
            mock.SetupProperty(x => x.Telephone, Faker.Phone.Number());
            mock.SetupProperty(x => x.DonorStatus, DonorStatus.Active);
            mock.SetupProperty(x => x.JoinDate, DateTime.Now);
            return mock;
        }

        public Mock<IDonationCaseContract> MockDonationCase()
        {
            var caseName = Faker.Name.FullName();
            var mock = new Mock<IDonationCaseContract>();
            mock.SetupProperty(x => x.Name, caseName);
            mock.SetupProperty(x => x.StartDate, DateTime.Now.AddDays(-20));
            mock.SetupProperty(x => x.DonationCaseStatus, DonationCaseStatus.Active);
            mock.SetupProperty(x => x.EndDate, DateTime.Now.AddDays(+20));
            return mock;
        }

        public Mock<ICommitmentContract> MockCommitment(Guid donorId, Guid donationCaseId)
        {
            var mock = new Mock<ICommitmentContract>();
            mock.SetupProperty(x => x.DonorId, donorId);
            mock.SetupProperty(x => x.StartDate, DateTime.Now.AddDays(-20));
            mock.SetupProperty(x => x.DonationCaseId, donationCaseId);
            mock.SetupProperty(x => x.EndDate, DateTime.Now.AddDays(+20));
            return mock;
        }

        public Mock<IPaymentContract> MockPayment(Guid commitmentId, Guid periodId)
        {
            var mock = new Mock<IPaymentContract>();
            mock.SetupProperty(x => x.CommitmentId, commitmentId);
            mock.SetupProperty(x => x.Amount, 2121);
            mock.SetupProperty(x => x.Comments, Faker.Lorem.Sentence());
            mock.SetupProperty(x => x.PaymentDate, DateTime.Now.AddDays(3));
            mock.SetupProperty(x => x.PaymentPeriodId, periodId);
            return mock;
        }
    }
}