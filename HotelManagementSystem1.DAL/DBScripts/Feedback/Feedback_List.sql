
			  CREATE OR REPLACE FUNCTION  "Feedback_List"
              (pvar_tenantid Varchar
)
			  RETURNS TABLE(tenantid uuid
,_tenantName Varchar(128)
,Feedbackid uuid
,feedbackno Varchar,guestno Varchar,staffrating Varchar,roomrating Varchar,createduser uuid
,createddate Timestamp(5)
,modifieduser uuid
,modifieddate Timestamp(5)
)
			  AS $BODY$
              declare lvar_tenantid varchar[];declare lstr_usersid varchar;
              
			  BEGIN
			  /*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 7:00:03 AM*/
			  		
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
				Feedback.tenantid
,tenant.businessname as _tenantName
,Feedback.Feedbackid
,Feedback.feedbackno
,Feedback.guestno
,Feedback.staffrating
,Feedback.roomrating

				
				,Feedback.createduser,Feedback.createddate,Feedback.modifieduser,Feedback.modifieddate
				FROM  Feedback 
 LEFT OUTER JOIN tenant ON Feedback.tenantid=tenant.tenantid

				WHERE (lvar_tenantid is null or COALESCE(cast(Feedback.tenantid as varchar), '') = Any(lvar_tenantid)) AND Feedback.isdeleted=false

				 ORDER BY Feedback.createddate DESC;
			  
					 	
			  END
              $BODY$
              LANGUAGE plpgsql;

