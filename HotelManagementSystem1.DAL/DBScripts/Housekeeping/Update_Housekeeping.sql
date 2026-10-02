
			  CREATE OR REPLACE FUNCTION  "Update_Housekeeping"
			  (
				  pvar_Housekeepingid uuid
,pvar_tenantid uuid
,
pvar_housekeepingno Varchar(256)
,
pvar_cleaningdate date
,
pvar_roomno  Varchar(1024)
,
pvar_cleaningstatus  Varchar(1024)
,
pvar_supervisor Varchar(128)
,
pvar_remark text

				  ,pvar_modifieduser  uuid  

				  ,OUT pvar_returnMessage Varchar(4000)
			  )
			  RETURNS Varchar(4000) 
              AS $BODY$  
              DECLARE lv_viewactionroles Varchar(128);
              BEGIN

			  /*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 6:08:31 AM*/
			  IF "Check_Authorization"(pvar_modifieduser, 'Housekeeping', 'edit') THEN


			  pvar_returnMessage:='';

			  if EXISTS (SELECT * from Housekeeping where upper(Housekeeping.housekeepingno) = upper(pvar_housekeepingno) and Housekeeping.tenantid=pvar_tenantid  and Housekeeping.Housekeepingid <> pvar_Housekeepingid)
																THEN

																  pvar_returnMessage := pvar_returnMessage||'Housekeeping No Already Exists.';

																END IF;

               IF(pvar_cleaningstatus is not null AND pvar_cleaningstatus!='0' AND LENGTH(pvar_cleaningstatus)>0)
                                                            THEN                        
                                                                 if(CAST((SELECT Count(T1.T1) 
                                                                FROM regexp_split_to_table(pvar_cleaningstatus, ',') AS T1
                                                                    INNER JOIN regexp_split_to_table((Select  fielddesc 
                                                                from lookups  where fieldname='cleaningstatus'
                                                                and entityname='Housekeeping' LIMIT 1), ',') AS T2 on T1.T1 = T2.T2) AS int) <> CAST((SELECT Count(T1.T1)
                                                                FROM regexp_split_to_table(pvar_cleaningstatus, ',')  AS T1) AS int))
                                                                THEN
                                                                        pvar_returnMessage := pvar_returnMessage || 'cleaningstatus value is invalid';


                                                                END IF;
                                                            END IF;
IF(pvar_roomno is not null AND pvar_roomno!='0' AND LENGTH(pvar_roomno)>0)
                                                            THEN                        
                                                                 if(CAST((SELECT Count(T1.T1) 
                                                                    FROM regexp_split_to_table(pvar_roomno, ',') AS T1
                                                                        INNER JOIN (Select DISTINCT Room.roomno from Room) AS T2 on T1.T1 = T2.roomno) AS int) <> CAST((SELECT Count(T1.T1)
                                                                    FROM regexp_split_to_table(pvar_roomno, ',')  AS T1) AS int))
                                                                    THEN
                                                                         pvar_returnMessage := pvar_returnMessage || ' roomno value is invalid';


                                                                    END IF;
                                                                    END IF;
 
			  IF(pvar_returnMessage='')
			  THEN
               
                    INSERT INTO history
VALUES('Housekeeping', NOW(),
(SELECT query_to_xml('SELECT * FROM Housekeeping WHERE Housekeeping.Housekeepingid= '''||pvar_Housekeepingid||'''', true, false, '')));

                    
                    UPDATE Housekeeping SET
                    housekeepingno=pvar_housekeepingno
,cleaningdate=pvar_cleaningdate
,roomno=pvar_roomno
,cleaningstatus=pvar_cleaningstatus
,supervisor=pvar_supervisor
,remark=pvar_remark

                    
                    ,modifieduser=pvar_modifieduser,modifieddate=NOW()
                    WHERE Housekeepingid=pvar_Housekeepingid;

                    

                    


					
							
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
																,'Update_Housekeeping'
																,'Authorization Failed Update_Housekeeping'
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
						,'Update_Housekeeping'
						,'update failed'
						);
                        pvar_returnMessage := 'Update_Housekeeping - update failed';*/
			  	
			  END
              $BODY$
              LANGUAGE plpgsql;

