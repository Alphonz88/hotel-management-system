
			  CREATE OR REPLACE FUNCTION  "getById_sp_all_Payment"
              (
			  pvar_Paymentid Varchar
			  )
              RETURNS TABLE(
                tenantid uuid
,_tenantname Varchar
,"Paymentid" uuid
,paymentno Varchar
,roomrent decimal
,noofdays int
,totalamount Varchar
,invoiceno Varchar
,guestno Varchar
,transactionno Varchar
,paymentmethod Varchar
,paymentdate Varchar
,paymentstatus Varchar
,createduser uuid
,createddate Timestamp(5)
,modifieduser uuid
,modifieddate Timestamp(5)

                
                
                
				
                )

              AS $BODY$
                BEGIN
			   
			  /*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 6:46:37 AM*/
			  		 
              RETURN QUERY
			  SELECT  
				 Payment.tenantid
,tenant.businessname as _tenantname
,Payment.Paymentid
,Payment.paymentno
,Payment.roomrent
,Payment.noofdays
,Payment.totalamount
,Payment.invoiceno
,Payment.guestno
,Payment.transactionno
,Payment.paymentmethod
,CAST(COALESCE(to_char(Payment.paymentdate,'dd/MM/yyyy'),'') AS Varchar) as paymentdate
,Payment.paymentstatus

				 ,Payment.createduser,Payment.createddate,Payment.modifieduser,Payment.modifieddate
                 
                 
				 
			  FROM  Payment 
 LEFT OUTER JOIN tenant ON Payment.tenantid=tenant.tenantid

			  WHERE CAST(Payment.Paymentid AS Varchar)=pvar_Paymentid ;
			  
					 	
			  END
              $BODY$
              LANGUAGE plpgsql;

