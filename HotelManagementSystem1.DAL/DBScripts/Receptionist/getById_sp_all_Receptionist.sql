
			  CREATE OR REPLACE FUNCTION  "getById_sp_all_Receptionist"
              (
			  pvar_Receptionistid Varchar
			  )
              RETURNS TABLE(
                tenantid uuid
,_tenantname Varchar
,"Receptionistid" uuid
,employeeno Varchar
,employeename Varchar
,shift Varchar
,experience Varchar
,salary Varchar
,phonenumber Varchar
,email Varchar
,createduser uuid
,createddate Timestamp(5)
,modifieduser uuid
,modifieddate Timestamp(5)

                
                
                
				
                )

              AS $BODY$
                BEGIN
			   
			  /*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 8:38:57 AM*/
			  		 
              RETURN QUERY
			  SELECT  
				 Receptionist.tenantid
,tenant.businessname as _tenantname
,Receptionist.Receptionistid
,Receptionist.employeeno
,Receptionist.employeename
,Receptionist.shift
,Receptionist.experience
,Receptionist.salary
,Receptionist.phonenumber
,Receptionist.email

				 ,Receptionist.createduser,Receptionist.createddate,Receptionist.modifieduser,Receptionist.modifieddate
                 
                 
				 
			  FROM  Receptionist 
 LEFT OUTER JOIN tenant ON Receptionist.tenantid=tenant.tenantid

			  WHERE CAST(Receptionist.Receptionistid AS Varchar)=pvar_Receptionistid ;
			  
					 	
			  END
              $BODY$
              LANGUAGE plpgsql;

