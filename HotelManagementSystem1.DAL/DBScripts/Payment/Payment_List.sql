
			  CREATE OR REPLACE FUNCTION  "Payment_List"
              (pvar_tenantid Varchar
)
			  RETURNS TABLE(tenantid uuid
,_tenantName Varchar(128)
,Paymentid uuid
,paymentno Varchar,roomrent decimal,noofdays int,totalamount Varchar,invoiceno Varchar,guestno Varchar,transactionno Varchar,paymentmethod Varchar,paymentdate Varchar,paymentstatus Varchar,createduser uuid
,createddate Timestamp(5)
,modifieduser uuid
,modifieddate Timestamp(5)
)
			  AS $BODY$
              declare lvar_tenantid varchar[];declare lstr_usersid varchar;
              
			  BEGIN
			  /*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 6:46:37 AM*/
			  		
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
				Payment.tenantid
,tenant.businessname as _tenantName
,Payment.Paymentid
,Payment.paymentno
,Payment.roomrent
,Payment.noofdays
,Payment.totalamount
,Payment.invoiceno
,Payment.guestno
,Payment.transactionno
,Payment.paymentmethod
,CAST(COALESCE(to_char(Payment.paymentdate,'dd/MM/yyyy'),'') AS Varchar) as paymentdate
,Payment.paymentstatus

				
				,Payment.createduser,Payment.createddate,Payment.modifieduser,Payment.modifieddate
				FROM  Payment 
 LEFT OUTER JOIN tenant ON Payment.tenantid=tenant.tenantid

				WHERE (lvar_tenantid is null or COALESCE(cast(Payment.tenantid as varchar), '') = Any(lvar_tenantid)) AND Payment.isdeleted=false

				 ORDER BY Payment.createddate DESC;
			  
					 	
			  END
              $BODY$
              LANGUAGE plpgsql;

