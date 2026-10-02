
			  CREATE OR REPLACE FUNCTION  "getById_sp_all_Housekeeping"
              (
			  pvar_Housekeepingid Varchar
			  )
              RETURNS TABLE(
                tenantid uuid
,_tenantname Varchar
,"Housekeepingid" uuid
,housekeepingno Varchar
,cleaningdate Varchar
,roomno Varchar
,cleaningstatus Varchar
,supervisor Varchar
,remark text
,createduser uuid
,createddate Timestamp(5)
,modifieduser uuid
,modifieddate Timestamp(5)

                
                
                
				
                )

              AS $BODY$
                BEGIN
			   
			  /*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 6:08:31 AM*/
			  		 
              RETURN QUERY
			  SELECT  
				 Housekeeping.tenantid
,tenant.businessname as _tenantname
,Housekeeping.Housekeepingid
,Housekeeping.housekeepingno
,CAST(COALESCE(to_char(Housekeeping.cleaningdate,'dd/MM/yyyy'),'') AS Varchar) as cleaningdate
,Housekeeping.roomno
,Housekeeping.cleaningstatus
,Housekeeping.supervisor
,Housekeeping.remark

				 ,Housekeeping.createduser,Housekeeping.createddate,Housekeeping.modifieduser,Housekeeping.modifieddate
                 
                 
				 
			  FROM  Housekeeping 
 LEFT OUTER JOIN tenant ON Housekeeping.tenantid=tenant.tenantid

			  WHERE CAST(Housekeeping.Housekeepingid AS Varchar)=pvar_Housekeepingid ;
			  
					 	
			  END
              $BODY$
              LANGUAGE plpgsql;

