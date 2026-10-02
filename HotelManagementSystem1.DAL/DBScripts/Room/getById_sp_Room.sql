 
			  
			  CREATE OR REPLACE FUNCTION  "getById_sp_Room"
			  (
				  pvar_Roomid Varchar
			  )
			  RETURNS TABLE(
                roomno int
,floornumber int
,capacitymaxnoofpersons Varchar
,bedtype Varchar
,pricepernight Varchar
,availablestauts Varchar
,roomtype uuid
,noofguests Varchar
,createduser uuid
,createddate Timestamp(5)
,modifieduser uuid
,modifieddate Timestamp(5)
,tenantid uuid

                ,Roomid uuid
                
            )
            AS $BODY$
            BEGIN
			  /*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 8:30:56 AM*/
               
              RETURN QUERY
			  SELECT 
				 Room.roomno
,Room.floornumber
,Room.capacitymaxnoofpersons
,Room.bedtype
,Room.pricepernight
,Room.availablestauts
,Room.roomtype
,Room.noofguests

				 ,Room.createduser,Room.createddate,Room.modifieduser,Room.modifieddate
				 ,Room.tenantid
                 ,Room.Roomid
                    
			  FROM Room
			  WHERE CAST(Room.Roomid AS Varchar)=pvar_Roomid
                       ;

					 	
			  END
              $BODY$
              LANGUAGE plpgsql;

