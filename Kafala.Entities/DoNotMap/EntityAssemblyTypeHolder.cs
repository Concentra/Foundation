using System;
using Foundation.Persistence;

namespace Kafala.Entities.DoNotMap

{
    public class EntityAssemblyTypeHolder : ITypeHolder
    {
        public Type HookType
        {
            get { return typeof(Donor); }
        }

        public string NameSpace
        {
            get
            {
                const string kafalaEntities = "Kafala.Entities";
                return kafalaEntities;
            }
        }
    }
}