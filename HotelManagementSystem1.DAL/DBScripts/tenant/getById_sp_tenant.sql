 
			  
			  CREATE OR REPLACE FUNCTION  "getById_sp_tenant"
			  (
				  pvar_tenantid Varchar
			  )
			  RETURNS TABLE(
                businessname Varchar
,natureofbusiness Varchar
,businessemail Varchar
,businessphone Varchar
,businesswebsite Varchar
,organizationlogo Varchar
,numberofemployees int
,addressline1 Varchar
,addressline2 Varchar
,city Varchar
,statename Varchar
,zip Varchar
,country Varchar
,parentid uuid
,username Varchar
,password Varchar
,userrole Varchar
,createduser uuid
,createddate Timestamp(5)
,modifieduser uuid
,modifieddate Timestamp(5)

                ,tenantid uuid
                
            )
            AS $BODY$
            BEGIN
			  /*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 6:08:09 AM*/
               
              RETURN QUERY
			  SELECT 
				 tenant.businessname
,tenant.natureofbusiness
,tenant.businessemail
,tenant.businessphone
,tenant.businesswebsite
,tenant.organizationlogo
,tenant.numberofemployees
,tenant.addressline1
,tenant.addressline2
,tenant.city
,tenant.statename
,tenant.zip
,tenant.country
,tenant.parentid
,tenant.username
,tenant.password
,tenant.userrole

				 ,tenant.createduser,tenant.createddate,tenant.modifieduser,tenant.modifieddate
				 
                 ,tenant.tenantid
                    
			  FROM tenant
			  WHERE CAST(tenant.tenantid AS Varchar)=pvar_tenantid
                       ;

					 	
			  END
              $BODY$
              LANGUAGE plpgsql;

