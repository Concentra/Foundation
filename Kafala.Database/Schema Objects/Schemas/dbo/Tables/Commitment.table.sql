create table [Commitment] (
        Id UNIQUEIDENTIFIER not null,
       Amount DECIMAL(19,5) null,
       Deleted BIT null,
       StartDate DATETIME null,
       EndDate DATETIME null,
       DonationCase_id UNIQUEIDENTIFIER null,
       Donor_id UNIQUEIDENTIFIER null,
       primary key (Id)
    )








