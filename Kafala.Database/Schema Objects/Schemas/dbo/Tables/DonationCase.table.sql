create table [DonationCase] (
        Id UNIQUEIDENTIFIER not null,
       Name NVARCHAR(255) null,
       StartDate DATETIME null,
       EndDate DATETIME null,
       DonationCaseStatus NVARCHAR(255) null,
       primary key (Id)
    )








