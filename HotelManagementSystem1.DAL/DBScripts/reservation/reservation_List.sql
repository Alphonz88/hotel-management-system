
			  CREATE OR REPLACE FUNCTION  "reservation_List"
              (pvar_tenantid Varchar
)
			  RETURNS TABLE(tenantid uuid
,_tenantName Varchar(128)
,reservationid uuid
,reservationidno Varchar,guestidno Varchar,bookinggate Varchar,checkindate Varchar,checkoutdate Varchar,roomtype Varchar,noofguests Varchar,reservationstatus Varchar,createduser uuid
,createddate Timestamp(5)
,modifieduser uuid
,modifieddate Timestamp(5)
)
			  AS $BODY$
              declare lvar_tenantid varchar[];declare lstr_usersid varchar;
              
			  BEGIN
			  /*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/30/2026 7:06:48 AM*/
			  		
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
				reservation.tenantid
,tenant.businessname as _tenantName
,reservation.reservationid
,reservation.reservationidno
,reservation.guestidno
,CAST(COALESCE(to_char(reservation.bookinggate,'dd/MM/yyyy'),'') AS Varchar) as bookinggate
,CAST(COALESCE(to_char(reservation.checkindate,'dd/MM/yyyy'),'') AS Varchar) as checkindate
,CAST(COALESCE(to_char(reservation.checkoutdate,'dd/MM/yyyy'),'') AS Varchar) as checkoutdate
,reservation.roomtype
,reservation.noofguests
,reservation.reservationstatus

				
				,reservation.createduser,reservation.createddate,reservation.modifieduser,reservation.modifieddate
				FROM  reservation 
 LEFT OUTER JOIN tenant ON reservation.tenantid=tenant.tenantid

				WHERE (lvar_tenantid is null or COALESCE(cast(reservation.tenantid as varchar), '') = Any(lvar_tenantid)) AND reservation.isdeleted=false

				 ORDER BY reservation.createddate DESC;
			  
					 	
			  END
              $BODY$
              LANGUAGE plpgsql;

