
			  CREATE OR REPLACE FUNCTION  "Add_Feedback"
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
 
				  ,pvar_createduser  uuid 

				  ,OUT pvar_returnMessage Varchar(4000)
			  )
			  RETURNS Varchar(4000) 
              AS $BODY$  
              DECLARE lv_viewactionroles Varchar(128);lvar_curday_feedbackno Varchar(10);lvar_val_feedbackno int;
              BEGIN

				/*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 7:00:03 AM*/
		

			  
                                                                                    if pvar_Feedbackid is null then
                                                                                    pvar_Feedbackid:=gen_random_uuid();
                                                                                    end if;	
                                                                                    
			  
select cast(to_char(NOW(),'yyyy') as Varchar(4)) ||'-'|| RIGHT('00' ||cast(to_char(NOW(),'MM') as Varchar(2)),2) 
                                          INTO lvar_curday_feedbackno;
                                        select COALESCE(max(RIGHT(Feedback.feedbackno,4)),'0') INTO lvar_val_feedbackno from
                                        Feedback where substring(Feedback.feedbackno,1,7) = lvar_curday_feedbackno and (Feedback.feedbackno) NOT LIKE '%/%';
                                        lvar_val_feedbackno:=lvar_val_feedbackno + 1;
                                        pvar_feedbackno:= (lvar_curday_feedbackno||'-'|| cast(to_char(lvar_val_feedbackno,'fm0000') as Varchar(4)));

			  IF "Check_Authorization"(pvar_createduser, 'Feedback', 'create') THEN
			  pvar_returnMessage:='';
			  IF EXISTS (SELECT * from Feedback where upper(Feedback.feedbackno::varchar) = upper(pvar_feedbackno::varchar) and Feedback.tenantid=pvar_tenantid)
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
  
			  if(pvar_returnMessage='')
			  THEN

			  

			  INSERT INTO Feedback(
				 feedbackno
,guestno
,staffrating
,roomrating

				 ,createduser
				 ,Feedbackid
				 ,tenantid
                
			  )
			  VALUES (
 				 pvar_feedbackno
,pvar_guestno
,pvar_staffrating
,pvar_roomrating

				 ,pvar_createduser
				 ,pvar_Feedbackid
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
																,'Add_Feedback'
																,'Authorization Failed Add_Feedback'
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
						,'Add_Feedback'
						,'insert failed'
						);
                        pvar_returnMessage := 'Add_Feedback - Insert failed';*/
			  	
			  END
              $BODY$
              LANGUAGE plpgsql;

