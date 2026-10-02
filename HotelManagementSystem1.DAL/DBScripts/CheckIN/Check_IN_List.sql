
			  CREATE OR REPLACE FUNCTION  "Check_IN_List"
              (pvar_tenantid Varchar
)
			  RETURNS TABLE(tenantid uuid
,_tenantName Varchar(128)
,CheckINid uuid
,checkinno Varchar,guestno Varchar,checkindate Varchar,depositamount decimal,status Varchar,roomno Varchar,checkintime Varchar,createduser uuid
,createddate Timestamp(5)
,modifieduser uuid
,modifieddate Timestamp(5)
)
			  AS $BODY$
              declare lvar_tenantid varchar[];declare lstr_usersid varchar;
              
			  BEGIN
			  /*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 6:08:23 AM*/
			  		
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
				CheckIN.tenantid
,tenant.businessname as _tenantName
,CheckIN.CheckINid
,CheckIN.checkinno
,CheckIN.guestno
,CAST(COALESCE(to_char(CheckIN.checkindate,'dd/MM/yyyy'),'') AS Varchar) as checkindate
,CheckIN.depositamount
,CheckIN.status
,CheckIN.roomno
,CheckIN.checkintime

				
				,CheckIN.createduser,CheckIN.createddate,CheckIN.modifieduser,CheckIN.modifieddate
				FROM  CheckIN 
 LEFT OUTER JOIN tenant ON CheckIN.tenantid=tenant.tenantid

				WHERE (lvar_tenantid is null or COALESCE(cast(CheckIN.tenantid as varchar), '') = Any(lvar_tenantid)) AND CheckIN.isdeleted=false

				 ORDER BY CheckIN.createddate DESC;
			  
					 	
			  END
              $BODY$
              LANGUAGE plpgsql;

