﻿create table [Donor] (
        Id UNIQUEIDENTIFIER not null,
       Name NVARCHAR(255) null,
       Telephone NVARCHAR(255) null,
       JoinDate DATETIME null,
       Referral_id UNIQUEIDENTIFIER null,
       primary key (Id)
    )










