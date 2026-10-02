
			  CREATE OR REPLACE FUNCTION  "Add_guest"
			  (
				  pvar_guestid uuid
,pvar_tenantid uuid
,
pvar_guestidno Varchar(128)
,
pvar_guestname Varchar(128)
,
pvar_gender Varchar(128)
,
pvar_phonenumber Varchar(10)
,
pvar_email Varchar(128)
,
pvar_addressline1 Varchar(256)
,
pvar_addressline2 Varchar(256)
,
pvar_city Varchar(256)
,
pvar_statename Varchar(256)
,
pvar_country  Varchar(1024)
,
pvar_idproof Varchar(256)
,
pvar_nationality Varchar(128)
 
				  ,pvar_createduser  uuid 

				  ,OUT pvar_returnMessage Varchar(4000)
			  )
			  RETURNS Varchar(4000) 
              AS $BODY$  
              DECLARE lv_viewactionroles Varchar(128);
              BEGIN

				/*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/30/2026 6:54:10 AM*/
		

			  
                                                                                    if pvar_guestid is null then
                                                                                    pvar_guestid:=gen_random_uuid();
                                                                                    end if;	
                                                                                    
			  
			  IF "Check_Authorization"(pvar_createduser, 'guest', 'create') THEN
			  pvar_returnMessage:='';
			  
              IF(pvar_country is not null AND pvar_country!='0' AND LENGTH(pvar_country)>0)
                                                            THEN                        
                                                                 if(CAST((SELECT Count(T1.T1) 
                                                                FROM regexp_split_to_table(pvar_country, ',') AS T1
                                                                    INNER JOIN regexp_split_to_table((Select  fielddesc 
                                                                from lookups  where fieldname='country'
                                                                and entityname='guest' LIMIT 1), ',') AS T2 on T1.T1 = T2.T2) AS int) <> CAST((SELECT Count(T1.T1)
                                                                FROM regexp_split_to_table(pvar_country, ',')  AS T1) AS int))
                                                                THEN
                                                                        pvar_returnMessage := pvar_returnMessage || 'country value is invalid';


                                                                END IF;
                                                            END IF;
  
			  if(pvar_returnMessage='')
			  THEN

			  

			  INSERT INTO guest(
				 guestidno
,guestname
,gender
,phonenumber
,email
,addressline1
,addressline2
,city
,statename
,country
,idproof
,nationality

				 ,createduser
				 ,guestid
				 ,tenantid
                
			  )
			  VALUES (
 				 pvar_guestidno
,pvar_guestname
,pvar_gender
,pvar_phonenumber
,pvar_email
,pvar_addressline1
,pvar_addressline2
,pvar_city
,pvar_statename
,pvar_country
,pvar_idproof
,pvar_nationality

				 ,pvar_createduser
				 ,pvar_guestid
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
																,'Add_guest'
																,'Authorization Failed Add_guest'
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
						,'Add_guest'
						,'insert failed'
						);
                        pvar_returnMessage := 'Add_guest - Insert failed';*/
			  	
			  END
              $BODY$
              LANGUAGE plpgsql;

