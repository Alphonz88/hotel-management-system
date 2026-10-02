
			  CREATE OR REPLACE FUNCTION  "guest_List"
              (pvar_tenantid Varchar
)
			  RETURNS TABLE(tenantid uuid
,_tenantName Varchar(128)
,guestid uuid
,guestidno Varchar,guestname Varchar,gender Varchar,phonenumber Varchar,email Varchar,addressline1 Varchar,addressline2 Varchar,city Varchar,statename Varchar,country Varchar,idproof Varchar,nationality Varchar,createduser uuid
,createddate Timestamp(5)
,modifieduser uuid
,modifieddate Timestamp(5)
)
			  AS $BODY$
              declare lvar_tenantid varchar[];declare lstr_usersid varchar;
              
			  BEGIN
			  /*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/30/2026 6:54:10 AM*/
			  		
                SELECT  SPLIT_PART(pvar_tenantid, '|', 1),SPLIT_PART(pvar_tenantid, '|', 2) into lstr_usersid,pvar_tenantid;
		        
                if(pvar_tenantid is null or pvar_tenantid='' or pvar_tenantid='00000000-0000-0000-0000-000000000000')	
				then
                    SELECT STRING_TO_ARRAY(viewertenantids, ',') into lvar_tenantid
				    FROM users where users.usersid::varchar=lstr_usersid;	
                    if(lvar_tenantid is NULL)
					then 
						SELECT array_agg(tenant.tenantid) INTO lvar_tenantid FROM tenant;
               
					end if;
                else 
				  lvar_tenantid=ARRAY[pvar_tenantid];
                end if;
                lvar_tenantid := lvar_tenantid || ARRAY[''::character varying] || ARRAY['00000000-0000-0000-0000-000000000000'::character varying];



              
                RETURN QUERY
				SELECT  
				guest.tenantid
,tenant.businessname as _tenantName
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

				WHERE (lvar_tenantid is null or COALESCE(cast(guest.tenantid as varchar), '') = Any(lvar_tenantid)) AND guest.isdeleted=false

				 ORDER BY guest.createddate DESC;
			  
					 	
			  END
              $BODY$
              LANGUAGE plpgsql;

