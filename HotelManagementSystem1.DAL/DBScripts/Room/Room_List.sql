
			  CREATE OR REPLACE FUNCTION  "Room_List"
              (pvar_tenantid Varchar
)
			  RETURNS TABLE(tenantid uuid
,_tenantName Varchar(128)
,Roomid uuid
,roomno int,floornumber int,capacitymaxnoofpersons Varchar,bedtype Varchar,pricepernight Varchar,availablestauts Varchar,roomtype uuid,roomtype_master Varchar,noofguests Varchar,createduser uuid
,createddate Timestamp(5)
,modifieduser uuid
,modifieddate Timestamp(5)
)
			  AS $BODY$
              declare lvar_tenantid varchar[];declare lstr_usersid varchar;
              
			  BEGIN
			  /*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 8:30:56 AM*/
			  		
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
				Room.tenantid
,tenant.businessname as _tenantName
,Room.Roomid
,Room.roomno
,Room.floornumber
,Room.capacitymaxnoofpersons
,Room.bedtype
,Room.pricepernight
,Room.availablestauts
,Room.roomtype
,CAST(_Reservation.roomtype AS VARCHAR) as roomtype_master
,Room.noofguests

				
				,Room.createduser,Room.createddate,Room.modifieduser,Room.modifieddate
				FROM  Room 
 LEFT OUTER JOIN tenant ON Room.tenantid=tenant.tenantid
LEFT OUTER JOIN Reservation _Reservation ON Room.roomtype=_Reservation.Reservationid

				WHERE (lvar_tenantid is null or COALESCE(cast(Room.tenantid as varchar), '') = Any(lvar_tenantid)) AND Room.isdeleted=false

				 ORDER BY Room.createddate DESC;
			  
					 	
			  END
              $BODY$
              LANGUAGE plpgsql;

