using System;
using Foundation.Persistence;

// ReSharper disable CheckNamespace
namespace Kafala.Entities.DoNotMap
// ReSharper restore CheckNamespace
{
    public class TypeHolder : ITypeHolder
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