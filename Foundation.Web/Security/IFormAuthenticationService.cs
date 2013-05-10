namespace Foundation.Web.Security
{
    public interface IFormAuthenticationService
    {
        SignInResult SignIn(string userName, string password, bool rememberMe = false);
        void SignOut();
    }
}