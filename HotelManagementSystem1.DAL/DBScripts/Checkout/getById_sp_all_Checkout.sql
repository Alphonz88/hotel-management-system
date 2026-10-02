
			  CREATE OR REPLACE FUNCTION  "getById_sp_all_Checkout"
              (
			  pvar_Checkoutid Varchar
			  )
              RETURNS TABLE(
                tenantid uuid
,_tenantname Varchar
,"Checkoutid" uuid
,checkoutno Varchar
,guestno Varchar
,roomno Varchar
,checkoutdate Varchar
,checkouttime Varchar
,finalbill decimal
,status Varchar
,createduser uuid
,createddate Timestamp(5)
,modifieduser uuid
,modifieddate Timestamp(5)

                
                
                
				
                )

              AS $BODY$
                BEGIN
			   
			  /*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 6:08:29 AM*/
			  		 
              RETURN QUERY
			  SELECT  
				 Checkout.tenantid
,tenant.businessname as _tenantname
,Checkout.Checkoutid
,Checkout.checkoutno
,Checkout.guestno
,Checkout.roomno
,CAST(COALESCE(to_char(Checkout.checkoutdate,'dd/MM/yyyy'),'') AS Varchar) as checkoutdate
,Checkout.checkouttime
,Checkout.finalbill
,Checkout.status

				 ,Checkout.createduser,Checkout.createddate,Checkout.modifieduser,Checkout.modifieddate
                 
                 
				 
			  FROM  Checkout 
 LEFT OUTER JOIN tenant ON Checkout.tenantid=tenant.tenantid

			  WHERE CAST(Checkout.Checkoutid AS Varchar)=pvar_Checkoutid ;
			  
					 	
			  END
              $BODY$
              LANGUAGE plpgsql;

