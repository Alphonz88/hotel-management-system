 
			  
			  CREATE OR REPLACE FUNCTION  "getById_sp_Invoice"
			  (
				  pvar_Invoiceid Varchar
			  )
			  RETURNS TABLE(
                invoicenumber Varchar
,guestno Varchar
,bookingnumber Varchar
,invoicedate date
,roomcharges decimal
,taxdiscount decimal
,totalamount Varchar
,verifiedstatus Varchar
,createduser uuid
,createddate Timestamp(5)
,modifieduser uuid
,modifieddate Timestamp(5)

                ,Invoiceid uuid
                
            )
            AS $BODY$
            BEGIN
			  /*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 8:57:49 AM*/
               
              RETURN QUERY
			  SELECT 
				 Invoice.invoicenumber
,Invoice.guestno
,Invoice.bookingnumber
,Invoice.invoicedate
,Invoice.roomcharges
,Invoice.taxdiscount
,Invoice.totalamount
,Invoice.verifiedstatus

				 ,Invoice.createduser,Invoice.createddate,Invoice.modifieduser,Invoice.modifieddate
				 
                 ,Invoice.Invoiceid
                    
			  FROM Invoice
			  WHERE CAST(Invoice.Invoiceid AS Varchar)=pvar_Invoiceid
                       ;

					 	
			  END
              $BODY$
              LANGUAGE plpgsql;

