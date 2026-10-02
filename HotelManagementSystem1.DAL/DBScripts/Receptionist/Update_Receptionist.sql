
			  CREATE OR REPLACE FUNCTION  "Update_Receptionist"
			  (
				  pvar_Receptionistid uuid
,pvar_tenantid uuid
,
pvar_employeeno Varchar(128)
,
pvar_employeename Varchar(128)
,
pvar_shift Varchar(128)
,
pvar_experience Varchar(128)
,
pvar_salary Varchar(128)
,
pvar_phonenumber Varchar(10)
,
pvar_email Varchar(128)

				  ,pvar_modifieduser  uuid  

				  ,OUT pvar_returnMessage Varchar(4000)
			  )
			  RETURNS Varchar(4000) 
              AS $BODY$  
              DECLARE lv_viewactionroles Varchar(128);
              BEGIN

			  /*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 8:38:57 AM*/
			  IF "Check_Authorization"(pvar_modifieduser, 'Receptionist', 'edit') THEN


			  pvar_returnMessage:='';

			  
                
			  IF(pvar_returnMessage='')
			  THEN
               
                    INSERT INTO history
VALUES('Receptionist', NOW(),
(SELECT query_to_xml('SELECT * FROM Receptionist WHERE Receptionist.Receptionistid= '''||pvar_Receptionistid||'''', true, false, '')));

                    
                    UPDATE Receptionist SET
                    employeeno=pvar_employeeno
,employeename=pvar_employeename
,shift=pvar_shift
,experience=pvar_experience
,salary=pvar_salary
,phonenumber=pvar_phonenumber
,email=pvar_email

                    
                    ,modifieduser=pvar_modifieduser,modifieddate=NOW()
                    WHERE Receptionistid=pvar_Receptionistid;

                    

                    


					
							
					pvar_returnMessage :='201.1';
			
			  END IF;

			  
																ELSE
																

															
																INSERT INTO system_logging
																(
																Log_code
																,system_logging_guid
																,log_application
																,log_date
																,log_level
																,log_logger
																,log_message
																,log_user_name
																)
																VALUES
																('401.1'
																,gen_random_uuid()
																,'Store Proc Authorization Check'
																,NOW()
																,'Critical'
																,'Update_Receptionist'
																,'Authorization Failed Update_Receptionist'
																,pvar_modifieduser
																);
																pvar_returnMessage = '401.1';
																
																END IF;

			  			 /* EXCEPTION WHEN OTHERS THEN
			 
						INSERT INTO system_logging
						(
						Log_code
						,system_logging_guid
						,log_application
						,log_date
						,log_level
						,log_logger
						,log_message
						)
						VALUES
						('16'
						,gen_random_uuid()
						,'Postgre Function Exception'
						,NOW()
						,'16'
						,'Update_Receptionist'
						,'update failed'
						);
                        pvar_returnMessage := 'Update_Receptionist - update failed';*/
			  	
			  END
              $BODY$
              LANGUAGE plpgsql;

