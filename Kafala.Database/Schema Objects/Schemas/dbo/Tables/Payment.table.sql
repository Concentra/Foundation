create table [Payment] (
        Id UNIQUEIDENTIFIER not null,
       Comments NVARCHAR(255) null,
       PaymentDate DATETIME null,
       Amount DECIMAL(19,5) null,
       PaymentPeriod_id UNIQUEIDENTIFIER null,
       Commitment_id UNIQUEIDENTIFIER null,
       primary key (Id)
    )








