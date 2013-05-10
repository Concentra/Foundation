namespace Foundation.Web
{
    public interface ICurrentAuthenticateUser
    {
        string UniqueUserToken { get; }

        string UserDisplayName { get; }
    }
}