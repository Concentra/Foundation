using System.Configuration;

namespace Foundation.Persistence
{
    public class ConnectionString : IConnectionString
    {
        public ConnectionString(string connectionStringName)
        {
            this.Name = connectionStringName;
        }

        public string Name { get; private set; }

        public string Value
        {
            get { return ConfigurationManager.ConnectionStrings[this.Name].ConnectionString; }
        }
    }
}