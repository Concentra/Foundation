using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace Kafala.Entities.Mapping
{
    public class UserMapper : IAutoMappingOverride<User> 
    {
        public void Override(AutoMapping<User> mapping)
        {
           mapping.IgnoreProperty(m => m.UserName);
        }
    }
}
