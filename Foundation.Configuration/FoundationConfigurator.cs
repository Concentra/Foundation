namespace Foundation.Configuration
{
    public class FoundationConfigurator : IFoundationConfigurator
    {
        public FoundationConfigurator()
        {
            this.Business = new BusinessConfigurations();

            this.Persistence = new PersistenceConfigurations();

            this.Mongo = new MongoConfigurations();

            this.Web = new WebConfigurations();

            this.UseBuseinssManagers = true;
            this.UseEmailing = false;
            this.UsePresistence = false;
            this.UseQueryContainer = true;
            this.UseSecurity = true;
            this.UseWeb = true;
            this.UseMongo = false;
        }

        public WebConfigurations Web { get; set; }
        public BusinessConfigurations Business { get; set; }
        public PersistenceConfigurations Persistence { get; set; }
        public MongoConfigurations Mongo { get; set; }

        public bool UsePresistence { get; set; }
        
        public bool UseQueryContainer { get; set; }
        
        public bool UseBuseinssManagers { get; set; }
        
        public bool UseWeb { get; set; }
        
        public bool UseSecurity { get; set; }
        
        public bool UseEmailing { get; set; }
        public bool UseMongo { get; set; }
    }
}