﻿create table [Commitment] (
        Id UNIQUEIDENTIFIER not null,
       StartDate DATETIME null,
       EndDate DATETIME null,
       DonationCase_id UNIQUEIDENTIFIER null,
       Donor_id UNIQUEIDENTIFIER null,
       primary key (Id)
    )







