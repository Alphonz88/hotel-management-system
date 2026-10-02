
			  CREATE OR REPLACE FUNCTION  "getById_sp_all_guest"
              (
			  pvar_guestid Varchar
			  )
              RETURNS TABLE(
                tenantid uuid
,_tenantname Varchar
,"guestid" uuid
,guestidno Varchar
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

                
                
                
				
                )

              AS $BODY$
                BEGIN
			   
			  /*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/30/2026 6:54:10 AM*/
			  		 
              RETURN QUERY
			  SELECT  
				 guest.tenantid
,tenant.businessname as _tenantname
,guest.guestid
,guest.guestidno
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
                 
                 
				 
			  FROM  guest 
 LEFT OUTER JOIN tenant ON guest.tenantid=tenant.tenantid

			  WHERE CAST(guest.guestid AS Varchar)=pvar_guestid ;
			  
					 	
			  END
              $BODY$
              LANGUAGE plpgsql;

