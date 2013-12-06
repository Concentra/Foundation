CREATE VIEW [dbo].[PaymentStatus] AS
	SELECT Commitment.DonationCase_Id,
			Commitment.Id as Commitment_Id,
			PaymentPeriod.id as PaymentPeriod_Id,
		  Payment.id as Payment_Id,
           CASE When (Payment.Id is null ) THEN 0 ELSE 1 END Paid
FROM   Commitment cross join 
                      PaymentPeriod
                       Left outer  JOIN
                      Payment ON 
                      Payment.PaymentPeriod_id = PaymentPeriod.Id 
                      
                       AND
                      Commitment.Id = Payment.Commitment_id
