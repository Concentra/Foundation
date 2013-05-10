create table [User] (
        Id UNIQUEIDENTIFIER not null,
       FirstName NVARCHAR(255) null,
       LastName NVARCHAR(255) null,
       EmailAddress NVARCHAR(255) null,
       Telephone NVARCHAR(255) null,
       Disabled BIT null,
       AccountLocked BIT null,
       Password NVARCHAR(255) null,
       PasswordSalt NVARCHAR(255) null,
       PasswordExpirtyDate DATETIME null,
       FailedLoginAttempts INT null,
       primary key (Id)
    )


