CREATE VIEW [dbo].[PaymentStatus] AS
	
	SELECT 
	Commitment.DonationCase_id as DonationCaseId,
	Commitment.DonationCase_id ,
	NEWID() as Id,
	Payment.Amount as PaidAmount,
	Commitment.Amount as CommittedAmount,
			Commitment.Id as Commitment_id,
			PaymentPeriod.id as PaymentPeriod_id,
		  Payment.id as Payment_Id,
           CASE When (Payment.Id is null ) THEN 0 ELSE 1 END Paid
FROM   Commitment cross join 
                      PaymentPeriod
                       Left outer  JOIN
                      Payment ON 
                      Payment.PaymentPeriod_id = PaymentPeriod.Id 
                      
                       AND
                      Commitment.Id = Payment.Commitment_id
