namespace Foundation.Configuration
{
    public interface IFoundationConfigurator 
    {
        WebConfigurations  Web { get; set; }
        BusinessConfigurations Business { get; set; }
        PersistenceConfigurations Persistence { get; set; }
        bool UsePresistence { get; set; }
        bool UseQueryContainer { get; set; }
        bool UseBuseinssManagers { get; set; }
        bool UseWeb { get; set; }
        bool UseSecurity { get; set; }
        bool UseEmailing { get; set; }
    }
}