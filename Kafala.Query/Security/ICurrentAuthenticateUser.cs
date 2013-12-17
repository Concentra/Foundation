namespace Kafala.Query.Security
{
    public interface ICurrentAuthenticateUser
    {
        string UniqueUserToken { get; }

        string UserDisplayName { get; }
    }
}