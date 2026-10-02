
			  CREATE OR REPLACE FUNCTION  "Update_guest"
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

				  ,pvar_modifieduser  uuid  

				  ,OUT pvar_returnMessage Varchar(4000)
			  )
			  RETURNS Varchar(4000) 
              AS $BODY$  
              DECLARE lv_viewactionroles Varchar(128);
              BEGIN

			  /*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/30/2026 6:54:10 AM*/
			  IF "Check_Authorization"(pvar_modifieduser, 'guest', 'edit') THEN


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
 
			  IF(pvar_returnMessage='')
			  THEN
               
                    INSERT INTO history
VALUES('guest', NOW(),
(SELECT query_to_xml('SELECT * FROM guest WHERE guest.guestid= '''||pvar_guestid||'''', true, false, '')));

                    
                    UPDATE guest SET
                    guestidno=pvar_guestidno
,guestname=pvar_guestname
,gender=pvar_gender
,phonenumber=pvar_phonenumber
,email=pvar_email
,addressline1=pvar_addressline1
,addressline2=pvar_addressline2
,city=pvar_city
,statename=pvar_statename
,country=pvar_country
,idproof=pvar_idproof
,nationality=pvar_nationality

                    
                    ,modifieduser=pvar_modifieduser,modifieddate=NOW()
                    WHERE guestid=pvar_guestid;

                    

                    


					
							
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
																,'Update_guest'
																,'Authorization Failed Update_guest'
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
						,'Update_guest'
						,'update failed'
						);
                        pvar_returnMessage := 'Update_guest - update failed';*/
			  	
			  END
              $BODY$
              LANGUAGE plpgsql;

