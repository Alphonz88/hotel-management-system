
			  CREATE OR REPLACE FUNCTION  "Invoice_List"
              ()
			  RETURNS TABLE(Invoiceid uuid
,invoicenumber Varchar,guestno Varchar,bookingnumber Varchar,invoicedate Varchar,roomcharges decimal,taxdiscount decimal,totalamount Varchar,verifiedstatus Varchar,createduser uuid
,createddate Timestamp(5)
,modifieduser uuid
,modifieddate Timestamp(5)
)
			  AS $BODY$
              
              
			  BEGIN
			  /*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 8:57:49 AM*/
			  		
              
                RETURN QUERY
				SELECT  
				Invoice.Invoiceid
,Invoice.invoicenumber
,Invoice.guestno
,Invoice.bookingnumber
,CAST(COALESCE(to_char(Invoice.invoicedate,'dd/MM/yyyy'),'') AS Varchar) as invoicedate
,Invoice.roomcharges
,Invoice.taxdiscount
,Invoice.totalamount
,Invoice.verifiedstatus

				
				,Invoice.createduser,Invoice.createddate,Invoice.modifieduser,Invoice.modifieddate
				FROM  Invoice 

				WHERE Invoice.isdeleted=false 
 AND Invoice.verifiedstatus='Approved'

				 ORDER BY Invoice.createddate DESC;
			  
					 	
			  END
              $BODY$
              LANGUAGE plpgsql;

