using System;
using Foundation.Persistence;

namespace Kafala.Entities.DoNotMap

{
    public class DataModelLocator : IDataModelLocator
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