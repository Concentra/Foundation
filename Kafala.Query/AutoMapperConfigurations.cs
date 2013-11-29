using AutoMapper;
using Foundation.Web.Paging;
using Kafala.BusinessManagers.DonationCase;
using Kafala.Web.ViewModels.Commitment;
using Kafala.Web.ViewModels.DonationCase;
using Kafala.Web.ViewModels.Donor;
using Kafala.Web.ViewModels.Payment;
using Kafala.Web.ViewModels.PaymentPeriod;

namespace Kafala.Query
{
    public class AutoMapperConfigurations
    {
        public static void Configure(IConfiguration cfg)
        {
            cfg.CreateMap<Entities.DonationCase, EditDonationCaseViewModel>();
            cfg.CreateMap<Entities.Commitment, EditCommitmentViewModel>();
            cfg.CreateMap<Entities.DonationCase, ViewDonationCaseViewModel>();
            cfg.CreateMap<Entities.Donor, ViewDonorViewModel>();
            cfg.CreateMap<Entities.Donor, DonorUpdateViewModel>();
            cfg.CreateMap<Entities.Commitment, ViewCommitmentViewModel>();
            cfg.CreateMap<Entities.Payment, ViewPaymentViewModel>();
            cfg.CreateMap<Entities.Payment, EditPaymentViewModel>();
            cfg.CreateMap<Entities.PaymentPeriod, ViewPaymentPeriodViewModel>();
            cfg.CreateMap<Entities.PaymentPeriod, EditPaymentPeriodViewModel>();
            cfg.CreateMap<IDonationCaseContract, Entities.DonationCase>();


            cfg.CreateMap<PagingInfo, PagingInfoViewModel>();
        }
    }
}