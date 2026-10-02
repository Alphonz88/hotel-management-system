 
			  
			  CREATE OR REPLACE FUNCTION  "getById_sp_guest"
			  (
				  pvar_guestid Varchar
			  )
			  RETURNS TABLE(
                guestidno Varchar
,guestname Varchar
,gender Varchar
,phonenumber Varchar
,email Varchar
,addressline1 Varchar
,addressline2 Varchar
,city Varchar
,statename Varchar
,country Varchar
,idproof Varchar
,nationality Varchar
,createduser uuid
,createddate Timestamp(5)
,modifieduser uuid
,modifieddate Timestamp(5)
,tenantid uuid

                ,guestid uuid
                
            )
            AS $BODY$
            BEGIN
			  /*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/30/2026 6:54:10 AM*/
               
              RETURN QUERY
			  SELECT 
				 guest.guestidno
,guest.guestname
,guest.gender
,guest.phonenumber
,guest.email
,guest.addressline1
,guest.addressline2
,guest.city
,guest.statename
,guest.country
,guest.idproof
,guest.nationality

				 ,guest.createduser,guest.createddate,guest.modifieduser,guest.modifieddate
				 ,guest.tenantid
                 ,guest.guestid
                    
			  FROM guest
			  WHERE CAST(guest.guestid AS Varchar)=pvar_guestid
                       ;

					 	
			  END
              $BODY$
              LANGUAGE plpgsql;

