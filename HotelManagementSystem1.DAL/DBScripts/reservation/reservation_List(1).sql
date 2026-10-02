
			  CREATE OR REPLACE FUNCTION  "reservation_List"
              (pvar_tenantid Varchar
)
			  RETURNS TABLE(tenantid uuid
,_tenantName Varchar(128)
,Reservationid uuid
,reservationno Varchar,guestno Varchar,bookingdate Varchar,checkindate Varchar,checkoutdate Varchar,reservationstatus Varchar,roomtype Varchar,noofguests Varchar,createduser uuid
,createddate Timestamp(5)
,modifieduser uuid
,modifieddate Timestamp(5)
)
			  AS $BODY$
              declare lvar_tenantid varchar[];declare lstr_usersid varchar;
              
			  BEGIN
			  /*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 6:56:03 AM*/
			  		
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
				Reservation.tenantid
,tenant.businessname as _tenantName
,Reservation.Reservationid
,Reservation.reservationno
,Reservation.guestno
,CAST(COALESCE(to_char(Reservation.bookingdate,'dd/MM/yyyy'),'') AS Varchar) as bookingdate
,CAST(COALESCE(to_char(Reservation.checkindate,'dd/MM/yyyy'),'') AS Varchar) as checkindate
,CAST(COALESCE(to_char(Reservation.checkoutdate,'dd/MM/yyyy'),'') AS Varchar) as checkoutdate
,Reservation.reservationstatus
,Reservation.roomtype
,Reservation.noofguests

				
				,Reservation.createduser,Reservation.createddate,Reservation.modifieduser,Reservation.modifieddate
				FROM  Reservation 
 LEFT OUTER JOIN tenant ON Reservation.tenantid=tenant.tenantid

				WHERE (lvar_tenantid is null or COALESCE(cast(Reservation.tenantid as varchar), '') = Any(lvar_tenantid)) AND Reservation.isdeleted=false

				 ORDER BY Reservation.createddate DESC;
			  
					 	
			  END
              $BODY$
              LANGUAGE plpgsql;

