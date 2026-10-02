
			  CREATE OR REPLACE FUNCTION  "getById_sp_all_Room"
              (
			  pvar_Roomid Varchar
			  )
              RETURNS TABLE(
                tenantid uuid
,_tenantname Varchar
,"Roomid" uuid
,roomno int
,floornumber int
,capacitymaxnoofpersons Varchar
,bedtype Varchar
,pricepernight Varchar
,availablestauts Varchar
,roomtype Varchar
,noofguests Varchar
,createduser uuid
,createddate Timestamp(5)
,modifieduser uuid
,modifieddate Timestamp(5)

                
                
                
				
                )

              AS $BODY$
                BEGIN
			   
			  /*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 8:30:56 AM*/
			  		 
              RETURN QUERY
			  SELECT  
				 Room.tenantid
,tenant.businessname as _tenantname
,Room.Roomid
,Room.roomno
,Room.floornumber
,Room.capacitymaxnoofpersons
,Room.bedtype
,Room.pricepernight
,Room.availablestauts
,CAST(_Reservation.roomtype AS VARCHAR) as roomtype
,Room.noofguests

				 ,Room.createduser,Room.createddate,Room.modifieduser,Room.modifieddate
                 
                 
				 
			  FROM  Room 
 LEFT OUTER JOIN tenant ON Room.tenantid=tenant.tenantid
LEFT OUTER JOIN Reservation _Reservation ON Room.roomtype=_Reservation.Reservationid

			  WHERE CAST(Room.Roomid AS Varchar)=pvar_Roomid ;
			  
					 	
			  END
              $BODY$
              LANGUAGE plpgsql;

