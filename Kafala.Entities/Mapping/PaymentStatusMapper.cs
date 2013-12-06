using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using NHibernate.Mapping;

namespace Kafala.Entities.Mapping
{
    public class PaymentStatusMapper : IAutoMappingOverride<PaymentStatus> 
    {
        public void Override(AutoMapping<PaymentStatus> mapping)
        {
            mapping.Table("PaymentStatus");
            mapping.ReadOnly();
        }
    }
}
