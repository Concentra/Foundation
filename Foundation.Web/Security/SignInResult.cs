namespace Foundation.Web.Security
{
    public enum SignInResult
    {
        NoSuchUser,
        UserDisabledByAdmin,
        UserAlreadyLocked,
        WrongPassword,
        LockedForExcessiveLoginAttempts,
        Success
    }
}