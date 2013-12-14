using Foundation.Web;

namespace Kafala.Web.UI
{
    public class CurrentAuthenticateUser : ICurrentAuthenticateUser
    {
        public string UniqueUserToken { get; private set; }
        public string UserDisplayName { get; private set; }
    }
}