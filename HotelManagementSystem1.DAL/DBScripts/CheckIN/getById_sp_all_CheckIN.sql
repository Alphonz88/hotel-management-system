
			  CREATE OR REPLACE FUNCTION  "getById_sp_all_CheckIN"
              (
			  pvar_CheckINid Varchar
			  )
              RETURNS TABLE(
                tenantid uuid
,_tenantname Varchar
,"CheckINid" uuid
,checkinno Varchar
,guestno Varchar
,checkindate Varchar
,depositamount decimal
,status Varchar
,roomno Varchar
,checkintime Varchar
,createduser uuid
,createddate Timestamp(5)
,modifieduser uuid
,modifieddate Timestamp(5)

                
                
                
				
                )

              AS $BODY$
                BEGIN
			   
			  /*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 6:08:23 AM*/
			  		 
              RETURN QUERY
			  SELECT  
				 CheckIN.tenantid
,tenant.businessname as _tenantname
,CheckIN.CheckINid
,CheckIN.checkinno
,CheckIN.guestno
,CAST(COALESCE(to_char(CheckIN.checkindate,'dd/MM/yyyy'),'') AS Varchar) as checkindate
,CheckIN.depositamount
,CheckIN.status
,CheckIN.roomno
,CheckIN.checkintime

				 ,CheckIN.createduser,CheckIN.createddate,CheckIN.modifieduser,CheckIN.modifieddate
                 
                 
				 
			  FROM  CheckIN 
 LEFT OUTER JOIN tenant ON CheckIN.tenantid=tenant.tenantid

			  WHERE CAST(CheckIN.CheckINid AS Varchar)=pvar_CheckINid ;
			  
					 	
			  END
              $BODY$
              LANGUAGE plpgsql;

