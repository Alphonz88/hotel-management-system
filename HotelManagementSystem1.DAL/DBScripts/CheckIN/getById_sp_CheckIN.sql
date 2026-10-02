 
			  
			  CREATE OR REPLACE FUNCTION  "getById_sp_CheckIN"
			  (
				  pvar_CheckINid Varchar
			  )
			  RETURNS TABLE(
                checkinno Varchar
,guestno Varchar
,checkindate date
,depositamount decimal
,status Varchar
,roomno Varchar
,checkintime Varchar
,createduser uuid
,createddate Timestamp(5)
,modifieduser uuid
,modifieddate Timestamp(5)
,tenantid uuid

                ,CheckINid uuid
                
            )
            AS $BODY$
            BEGIN
			  /*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 6:08:23 AM*/
               
              RETURN QUERY
			  SELECT 
				 CheckIN.checkinno
,CheckIN.guestno
,CheckIN.checkindate
,CheckIN.depositamount
,CheckIN.status
,CheckIN.roomno
,CheckIN.checkintime

				 ,CheckIN.createduser,CheckIN.createddate,CheckIN.modifieduser,CheckIN.modifieddate
				 ,CheckIN.tenantid
                 ,CheckIN.CheckINid
                    
			  FROM CheckIN
			  WHERE CAST(CheckIN.CheckINid AS Varchar)=pvar_CheckINid
                       ;

					 	
			  END
              $BODY$
              LANGUAGE plpgsql;

