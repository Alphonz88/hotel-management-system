 
			  
			  CREATE OR REPLACE FUNCTION  "getById_sp_Housekeeping"
			  (
				  pvar_Housekeepingid Varchar
			  )
			  RETURNS TABLE(
                housekeepingno Varchar
,cleaningdate date
,roomno Varchar
,cleaningstatus Varchar
,supervisor Varchar
,remark text
,createduser uuid
,createddate Timestamp(5)
,modifieduser uuid
,modifieddate Timestamp(5)
,tenantid uuid

                ,Housekeepingid uuid
                
            )
            AS $BODY$
            BEGIN
			  /*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 6:08:31 AM*/
               
              RETURN QUERY
			  SELECT 
				 Housekeeping.housekeepingno
,Housekeeping.cleaningdate
,Housekeeping.roomno
,Housekeeping.cleaningstatus
,Housekeeping.supervisor
,Housekeeping.remark

				 ,Housekeeping.createduser,Housekeeping.createddate,Housekeeping.modifieduser,Housekeeping.modifieddate
				 ,Housekeeping.tenantid
                 ,Housekeeping.Housekeepingid
                    
			  FROM Housekeeping
			  WHERE CAST(Housekeeping.Housekeepingid AS Varchar)=pvar_Housekeepingid
                       ;

					 	
			  END
              $BODY$
              LANGUAGE plpgsql;

