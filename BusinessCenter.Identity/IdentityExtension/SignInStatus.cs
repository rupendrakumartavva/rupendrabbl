namespace BusinessCenter.Identity.IdentityExtension
{
    public enum UserSignInStatus
    {
        Success,
        Nodata,
        In_Active,
        Delete,
        LockedOut,
        RequiresTwoFactorAuthentication,
        Expire,
        Failure,
        AlreadyLoggedIn

    }

    public enum RegistartionStatus
    {
        Success,
        Exists,
        In_Active,
        Delete,
        Failure,
        EmailExists
    }
}