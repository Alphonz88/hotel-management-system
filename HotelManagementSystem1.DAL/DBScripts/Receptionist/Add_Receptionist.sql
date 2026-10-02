
			  CREATE OR REPLACE FUNCTION  "Add_Receptionist"
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
 
				  ,pvar_createduser  uuid 

				  ,OUT pvar_returnMessage Varchar(4000)
			  )
			  RETURNS Varchar(4000) 
              AS $BODY$  
              DECLARE lv_viewactionroles Varchar(128);
              BEGIN

				/*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 8:38:57 AM*/
		

			  
                                                                                    if pvar_Receptionistid is null then
                                                                                    pvar_Receptionistid:=gen_random_uuid();
                                                                                    end if;	
                                                                                    
			  
			  IF "Check_Authorization"(pvar_createduser, 'Receptionist', 'create') THEN
			  pvar_returnMessage:='';
			  
                
			  if(pvar_returnMessage='')
			  THEN

			  

			  INSERT INTO Receptionist(
				 employeeno
,employeename
,shift
,experience
,salary
,phonenumber
,email

				 ,createduser
				 ,Receptionistid
				 ,tenantid
                
			  )
			  VALUES (
 				 pvar_employeeno
,pvar_employeename
,pvar_shift
,pvar_experience
,pvar_salary
,pvar_phonenumber
,pvar_email

				 ,pvar_createduser
				 ,pvar_Receptionistid
				 ,pvar_tenantid
                   
			  );
			   
               

			  


			  
					 
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
																,'Add_Receptionist'
																,'Authorization Failed Add_Receptionist'
																,pvar_createduser
																);
																pvar_returnMessage := '401.1';
																
																END IF;
			  /*EXCEPTION WHEN OTHERS THEN
			 
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
						,'Store Proc Exception'
						,NOW()
						,'16'
						,'Add_Receptionist'
						,'insert failed'
						);
                        pvar_returnMessage := 'Add_Receptionist - Insert failed';*/
			  	
			  END
              $BODY$
              LANGUAGE plpgsql;

