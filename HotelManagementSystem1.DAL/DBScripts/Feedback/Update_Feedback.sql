
			  CREATE OR REPLACE FUNCTION  "Update_Feedback"
			  (
				  pvar_Feedbackid uuid
,pvar_tenantid uuid
,
pvar_feedbackno Varchar(256)
,
pvar_guestno  Varchar(1024)
,
pvar_staffrating Varchar(256)
,
pvar_roomrating Varchar(256)

				  ,pvar_modifieduser  uuid  

				  ,OUT pvar_returnMessage Varchar(4000)
			  )
			  RETURNS Varchar(4000) 
              AS $BODY$  
              DECLARE lv_viewactionroles Varchar(128);
              BEGIN

			  /*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 7:00:03 AM*/
			  IF "Check_Authorization"(pvar_modifieduser, 'Feedback', 'edit') THEN


			  pvar_returnMessage:='';

			  if EXISTS (SELECT * from Feedback where upper(Feedback.feedbackno) = upper(pvar_feedbackno) and Feedback.tenantid=pvar_tenantid  and Feedback.Feedbackid <> pvar_Feedbackid)
																THEN

																  pvar_returnMessage := pvar_returnMessage||'Feedback No Already Exists.';

																END IF;

               IF(pvar_guestno is not null AND pvar_guestno!='0' AND LENGTH(pvar_guestno)>0)
                                                            THEN                        
                                                                 if(CAST((SELECT Count(T1.T1) 
                                                                    FROM regexp_split_to_table(pvar_guestno, ',') AS T1
                                                                        INNER JOIN (Select DISTINCT Guest.guestno from Guest) AS T2 on T1.T1 = T2.guestno) AS int) <> CAST((SELECT Count(T1.T1)
                                                                    FROM regexp_split_to_table(pvar_guestno, ',')  AS T1) AS int))
                                                                    THEN
                                                                         pvar_returnMessage := pvar_returnMessage || ' guestno value is invalid';


                                                                    END IF;
                                                                    END IF;
 
			  IF(pvar_returnMessage='')
			  THEN
               
                    INSERT INTO history
VALUES('Feedback', NOW(),
(SELECT query_to_xml('SELECT * FROM Feedback WHERE Feedback.Feedbackid= '''||pvar_Feedbackid||'''', true, false, '')));

                    
                    UPDATE Feedback SET
                    feedbackno=pvar_feedbackno
,guestno=pvar_guestno
,staffrating=pvar_staffrating
,roomrating=pvar_roomrating

                    
                    ,modifieduser=pvar_modifieduser,modifieddate=NOW()
                    WHERE Feedbackid=pvar_Feedbackid;

                    

                    


					
							
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
																,'Update_Feedback'
																,'Authorization Failed Update_Feedback'
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
						,'Update_Feedback'
						,'update failed'
						);
                        pvar_returnMessage := 'Update_Feedback - update failed';*/
			  	
			  END
              $BODY$
              LANGUAGE plpgsql;

