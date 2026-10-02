 
			  
			  CREATE OR REPLACE FUNCTION  "getById_sp_Checkout"
			  (
				  pvar_Checkoutid Varchar
			  )
			  RETURNS TABLE(
                checkoutno Varchar
,guestno Varchar
,roomno Varchar
,checkoutdate date
,checkouttime Varchar
,finalbill decimal
,status Varchar
,createduser uuid
,createddate Timestamp(5)
,modifieduser uuid
,modifieddate Timestamp(5)
,tenantid uuid

                ,Checkoutid uuid
                
            )
            AS $BODY$
            BEGIN
			  /*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 6:08:29 AM*/
               
              RETURN QUERY
			  SELECT 
				 Checkout.checkoutno
,Checkout.guestno
,Checkout.roomno
,Checkout.checkoutdate
,Checkout.checkouttime
,Checkout.finalbill
,Checkout.status

				 ,Checkout.createduser,Checkout.createddate,Checkout.modifieduser,Checkout.modifieddate
				 ,Checkout.tenantid
                 ,Checkout.Checkoutid
                    
			  FROM Checkout
			  WHERE CAST(Checkout.Checkoutid AS Varchar)=pvar_Checkoutid
                       ;

					 	
			  END
              $BODY$
              LANGUAGE plpgsql;

