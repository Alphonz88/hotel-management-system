
			  CREATE OR REPLACE FUNCTION  "Update_Room"
			  (
				  pvar_Roomid uuid
,pvar_tenantid uuid
,
pvar_roomno int
,
pvar_floornumber int
,
pvar_capacitymaxnoofpersons Varchar(128)
,
pvar_bedtype  Varchar(1024)
,
pvar_pricepernight Varchar(128)
,
pvar_availablestauts  Varchar(1024)
,
pvar_roomtype  uuid
,
pvar_noofguests  Varchar(1024)

				  ,pvar_modifieduser  uuid  

				  ,OUT pvar_returnMessage Varchar(4000)
			  )
			  RETURNS Varchar(4000) 
              AS $BODY$  
              DECLARE lv_viewactionroles Varchar(128);
              BEGIN

			  /*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 8:30:56 AM*/
			  IF "Check_Authorization"(pvar_modifieduser, 'Room', 'edit') THEN


			  pvar_returnMessage:='';

			  
               IF(pvar_availablestauts is not null AND pvar_availablestauts!='0' AND LENGTH(pvar_availablestauts)>0)
                                                            THEN                        
                                                                 if(CAST((SELECT Count(T1.T1) 
                                                                FROM regexp_split_to_table(pvar_availablestauts, ',') AS T1
                                                                    INNER JOIN regexp_split_to_table((Select  fielddesc 
                                                                from lookups  where fieldname='availablestauts'
                                                                and entityname='Room' LIMIT 1), ',') AS T2 on T1.T1 = T2.T2) AS int) <> CAST((SELECT Count(T1.T1)
                                                                FROM regexp_split_to_table(pvar_availablestauts, ',')  AS T1) AS int))
                                                                THEN
                                                                        pvar_returnMessage := pvar_returnMessage || 'availablestauts value is invalid';


                                                                END IF;
                                                            END IF;
IF(pvar_bedtype is not null AND pvar_bedtype!='0' AND LENGTH(pvar_bedtype)>0)
                                                            THEN                        
                                                                 if(CAST((SELECT Count(T1.T1) 
                                                                FROM regexp_split_to_table(pvar_bedtype, ',') AS T1
                                                                    INNER JOIN regexp_split_to_table((Select  fielddesc 
                                                                from lookups  where fieldname='bedtype'
                                                                and entityname='Room' LIMIT 1), ',') AS T2 on T1.T1 = T2.T2) AS int) <> CAST((SELECT Count(T1.T1)
                                                                FROM regexp_split_to_table(pvar_bedtype, ',')  AS T1) AS int))
                                                                THEN
                                                                        pvar_returnMessage := pvar_returnMessage || 'bedtype value is invalid';


                                                                END IF;
                                                            END IF;
IF(pvar_noofguests is not null AND pvar_noofguests!='0' AND LENGTH(pvar_noofguests)>0)
                                                            THEN                        
                                                                 if(CAST((SELECT Count(T1.T1) 
                                                                FROM regexp_split_to_table(pvar_noofguests, ',') AS T1
                                                                    INNER JOIN regexp_split_to_table((Select  fielddesc 
                                                                from lookups  where fieldname='noofguests'
                                                                and entityname='Room' LIMIT 1), ',') AS T2 on T1.T1 = T2.T2) AS int) <> CAST((SELECT Count(T1.T1)
                                                                FROM regexp_split_to_table(pvar_noofguests, ',')  AS T1) AS int))
                                                                THEN
                                                                        pvar_returnMessage := pvar_returnMessage || 'noofguests value is invalid';


                                                                END IF;
                                                            END IF;
 
			  IF(pvar_returnMessage='')
			  THEN
               
                    INSERT INTO history
VALUES('Room', NOW(),
(SELECT query_to_xml('SELECT * FROM Room WHERE Room.Roomid= '''||pvar_Roomid||'''', true, false, '')));

                    
                    UPDATE Room SET
                    roomno=pvar_roomno
,floornumber=pvar_floornumber
,capacitymaxnoofpersons=pvar_capacitymaxnoofpersons
,bedtype=pvar_bedtype
,pricepernight=pvar_pricepernight
,availablestauts=pvar_availablestauts
,roomtype=pvar_roomtype
,noofguests=pvar_noofguests

                    
                    ,modifieduser=pvar_modifieduser,modifieddate=NOW()
                    WHERE Roomid=pvar_Roomid;

                    

                    


					
							
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
																,'Update_Room'
																,'Authorization Failed Update_Room'
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
						,'Update_Room'
						,'update failed'
						);
                        pvar_returnMessage := 'Update_Room - update failed';*/
			  	
			  END
              $BODY$
              LANGUAGE plpgsql;

