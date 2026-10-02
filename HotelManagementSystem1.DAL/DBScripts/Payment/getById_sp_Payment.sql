 
			  
			  CREATE OR REPLACE FUNCTION  "getById_sp_Payment"
			  (
				  pvar_Paymentid Varchar
			  )
			  RETURNS TABLE(
                paymentno Varchar
,roomrent decimal
,noofdays int
,totalamount Varchar
,invoiceno Varchar
,guestno Varchar
,transactionno Varchar
,paymentmethod Varchar
,paymentdate date
,paymentstatus Varchar
,createduser uuid
,createddate Timestamp(5)
,modifieduser uuid
,modifieddate Timestamp(5)
,tenantid uuid

                ,Paymentid uuid
                
            )
            AS $BODY$
            BEGIN
			  /*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 6:46:37 AM*/
               
              RETURN QUERY
			  SELECT 
				 Payment.paymentno
,Payment.roomrent
,Payment.noofdays
,Payment.totalamount
,Payment.invoiceno
,Payment.guestno
,Payment.transactionno
,Payment.paymentmethod
,Payment.paymentdate
,Payment.paymentstatus

				 ,Payment.createduser,Payment.createddate,Payment.modifieduser,Payment.modifieddate
				 ,Payment.tenantid
                 ,Payment.Paymentid
                    
			  FROM Payment
			  WHERE CAST(Payment.Paymentid AS Varchar)=pvar_Paymentid
                       ;

					 	
			  END
              $BODY$
              LANGUAGE plpgsql;

