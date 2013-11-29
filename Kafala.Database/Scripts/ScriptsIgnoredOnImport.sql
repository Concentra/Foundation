
if exists (select * from dbo.sysobjects where id = object_id(N'[Donor]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [Donor]

GO

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK3965DDCF1E052D55]') AND parent_object_id = OBJECT_ID('[Commitment]'))
alter table [Commitment]  drop constraint FK3965DDCF1E052D55
GO
if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK3965DDCF4D8A2D2]') AND parent_object_id = OBJECT_ID('[Commitment]'))
alter table [Commitment]  drop constraint FK3965DDCF4D8A2D2
GO
if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKF4FA0208EEF170BE]') AND parent_object_id = OBJECT_ID('[Payment]'))
alter table [Payment]  drop constraint FKF4FA0208EEF170BE
GO
if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKF4FA0208A5F31EA6]') AND parent_object_id = OBJECT_ID('[Payment]'))
alter table [Payment]  drop constraint FKF4FA0208A5F31EA6
GO
if exists (select * from dbo.sysobjects where id = object_id(N'[Commitment]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [Commitment]
GO
if exists (select * from dbo.sysobjects where id = object_id(N'[DonationCase]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [DonationCase]
GO
if exists (select * from dbo.sysobjects where id = object_id(N'[Donor]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [Donor]
GO
if exists (select * from dbo.sysobjects where id = object_id(N'[Payment]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [Payment]
GO
if exists (select * from dbo.sysobjects where id = object_id(N'[PaymentPeriod]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [PaymentPeriod]

GO

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK3965DDCF1E052D55]') AND parent_object_id = OBJECT_ID('[Commitment]'))
alter table [Commitment]  drop constraint FK3965DDCF1E052D55
GO
if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK3965DDCF4D8A2D2]') AND parent_object_id = OBJECT_ID('[Commitment]'))
alter table [Commitment]  drop constraint FK3965DDCF4D8A2D2
GO
if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKA3DB4D981D3823D7]') AND parent_object_id = OBJECT_ID('[Donor]'))
alter table [Donor]  drop constraint FKA3DB4D981D3823D7
GO
if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKF4FA0208EEF170BE]') AND parent_object_id = OBJECT_ID('[Payment]'))
alter table [Payment]  drop constraint FKF4FA0208EEF170BE
GO
if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKF4FA0208A5F31EA6]') AND parent_object_id = OBJECT_ID('[Payment]'))
alter table [Payment]  drop constraint FKF4FA0208A5F31EA6
GO
if exists (select * from dbo.sysobjects where id = object_id(N'[Commitment]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [Commitment]
GO
if exists (select * from dbo.sysobjects where id = object_id(N'[DonationCase]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [DonationCase]
GO
if exists (select * from dbo.sysobjects where id = object_id(N'[Donor]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [Donor]
GO
if exists (select * from dbo.sysobjects where id = object_id(N'[Payment]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [Payment]
GO
if exists (select * from dbo.sysobjects where id = object_id(N'[PaymentPeriod]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [PaymentPeriod]

GO

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK3965DDCF1E052D55]') AND parent_object_id = OBJECT_ID('[Commitment]'))
alter table [Commitment]  drop constraint FK3965DDCF1E052D55
GO
if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK3965DDCF4D8A2D2]') AND parent_object_id = OBJECT_ID('[Commitment]'))
alter table [Commitment]  drop constraint FK3965DDCF4D8A2D2
GO
if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKA3DB4D981D3823D7]') AND parent_object_id = OBJECT_ID('[Donor]'))
alter table [Donor]  drop constraint FKA3DB4D981D3823D7
GO
if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKF4FA0208EEF170BE]') AND parent_object_id = OBJECT_ID('[Payment]'))
alter table [Payment]  drop constraint FKF4FA0208EEF170BE
GO
if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKF4FA0208A5F31EA6]') AND parent_object_id = OBJECT_ID('[Payment]'))
alter table [Payment]  drop constraint FKF4FA0208A5F31EA6
GO
if exists (select * from dbo.sysobjects where id = object_id(N'[Commitment]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [Commitment]
GO
if exists (select * from dbo.sysobjects where id = object_id(N'[DonationCase]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [DonationCase]
GO
if exists (select * from dbo.sysobjects where id = object_id(N'[Donor]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [Donor]
GO
if exists (select * from dbo.sysobjects where id = object_id(N'[Payment]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [Payment]
GO
if exists (select * from dbo.sysobjects where id = object_id(N'[PaymentPeriod]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [PaymentPeriod]
GO
if exists (select * from dbo.sysobjects where id = object_id(N'[User]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [User]

GO

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK3965DDCF1E052D55]') AND parent_object_id = OBJECT_ID('[Commitment]'))
alter table [Commitment]  drop constraint FK3965DDCF1E052D55
GO

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK3965DDCF4D8A2D2]') AND parent_object_id = OBJECT_ID('[Commitment]'))
alter table [Commitment]  drop constraint FK3965DDCF4D8A2D2
GO

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKA3DB4D981D3823D7]') AND parent_object_id = OBJECT_ID('[Donor]'))
alter table [Donor]  drop constraint FKA3DB4D981D3823D7
GO

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKF4FA0208EEF170BE]') AND parent_object_id = OBJECT_ID('[Payment]'))
alter table [Payment]  drop constraint FKF4FA0208EEF170BE
GO

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKF4FA0208A5F31EA6]') AND parent_object_id = OBJECT_ID('[Payment]'))
alter table [Payment]  drop constraint FKF4FA0208A5F31EA6
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[Commitment]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [Commitment]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[DonationCase]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [DonationCase]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[Donor]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [Donor]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[Payment]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [Payment]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[PaymentPeriod]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [PaymentPeriod]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[User]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [User]
GO

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK3965DDCF1E052D55]') AND parent_object_id = OBJECT_ID('[Commitment]'))
alter table [Commitment]  drop constraint FK3965DDCF1E052D55
GO

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK3965DDCF4D8A2D2]') AND parent_object_id = OBJECT_ID('[Commitment]'))
alter table [Commitment]  drop constraint FK3965DDCF4D8A2D2
GO

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKA3DB4D981D3823D7]') AND parent_object_id = OBJECT_ID('[Donor]'))
alter table [Donor]  drop constraint FKA3DB4D981D3823D7
GO

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKF4FA0208EEF170BE]') AND parent_object_id = OBJECT_ID('[Payment]'))
alter table [Payment]  drop constraint FKF4FA0208EEF170BE
GO

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKF4FA0208A5F31EA6]') AND parent_object_id = OBJECT_ID('[Payment]'))
alter table [Payment]  drop constraint FKF4FA0208A5F31EA6
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[Commitment]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [Commitment]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[DonationCase]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [DonationCase]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[Donor]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [Donor]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[Payment]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [Payment]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[PaymentPeriod]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [PaymentPeriod]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[User]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [User]
GO

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK3965DDCF1E052D55]') AND parent_object_id = OBJECT_ID('[Commitment]'))
alter table [Commitment]  drop constraint FK3965DDCF1E052D55
GO

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK3965DDCF4D8A2D2]') AND parent_object_id = OBJECT_ID('[Commitment]'))
alter table [Commitment]  drop constraint FK3965DDCF4D8A2D2
GO

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKA3DB4D981D3823D7]') AND parent_object_id = OBJECT_ID('[Donor]'))
alter table [Donor]  drop constraint FKA3DB4D981D3823D7
GO

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKF4FA0208EEF170BE]') AND parent_object_id = OBJECT_ID('[Payment]'))
alter table [Payment]  drop constraint FKF4FA0208EEF170BE
GO

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKF4FA0208A5F31EA6]') AND parent_object_id = OBJECT_ID('[Payment]'))
alter table [Payment]  drop constraint FKF4FA0208A5F31EA6
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[Commitment]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [Commitment]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[DonationCase]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [DonationCase]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[Donor]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [Donor]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[Payment]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [Payment]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[PaymentPeriod]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [PaymentPeriod]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[User]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [User]
GO

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK3965DDCF1E052D55]') AND parent_object_id = OBJECT_ID('[Commitment]'))
alter table [Commitment]  drop constraint FK3965DDCF1E052D55
GO

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK3965DDCF4D8A2D2]') AND parent_object_id = OBJECT_ID('[Commitment]'))
alter table [Commitment]  drop constraint FK3965DDCF4D8A2D2
GO

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKA3DB4D981D3823D7]') AND parent_object_id = OBJECT_ID('[Donor]'))
alter table [Donor]  drop constraint FKA3DB4D981D3823D7
GO

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKF4FA0208EEF170BE]') AND parent_object_id = OBJECT_ID('[Payment]'))
alter table [Payment]  drop constraint FKF4FA0208EEF170BE
GO

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKF4FA0208A5F31EA6]') AND parent_object_id = OBJECT_ID('[Payment]'))
alter table [Payment]  drop constraint FKF4FA0208A5F31EA6
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[Commitment]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [Commitment]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[DonationCase]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [DonationCase]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[Donor]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [Donor]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[Payment]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [Payment]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[PaymentPeriod]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [PaymentPeriod]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[User]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [User]
GO

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK3965DDCF1E052D55]') AND parent_object_id = OBJECT_ID('[Commitment]'))
alter table [Commitment]  drop constraint FK3965DDCF1E052D55
GO

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK3965DDCF4D8A2D2]') AND parent_object_id = OBJECT_ID('[Commitment]'))
alter table [Commitment]  drop constraint FK3965DDCF4D8A2D2
GO

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKA3DB4D981D3823D7]') AND parent_object_id = OBJECT_ID('[Donor]'))
alter table [Donor]  drop constraint FKA3DB4D981D3823D7
GO

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKF4FA0208EEF170BE]') AND parent_object_id = OBJECT_ID('[Payment]'))
alter table [Payment]  drop constraint FKF4FA0208EEF170BE
GO

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKF4FA0208A5F31EA6]') AND parent_object_id = OBJECT_ID('[Payment]'))
alter table [Payment]  drop constraint FKF4FA0208A5F31EA6
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[Commitment]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [Commitment]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[DonationCase]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [DonationCase]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[Donor]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [Donor]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[Payment]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [Payment]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[PaymentPeriod]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [PaymentPeriod]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[User]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [User]
GO
