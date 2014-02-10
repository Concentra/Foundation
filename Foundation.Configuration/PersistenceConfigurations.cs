using System;

namespace Foundation.Configuration
{
    public class PersistenceConfigurations
    {
        /// <summary>
        /// a type of an element which implements  IDataModelLocator. This type describes where the Data Model exists and the name space that should be mapped by Nhibernate.
        /// </summary>
        public Type EntityTypeHolder { get; set; }

        /// <summary>
        /// Name of the Connection string key.
        /// </summary>
        public string ConnectionStringKeyName { get; set; }
    }
}