
			  CREATE OR REPLACE FUNCTION  "Update_Check_IN"
			  (
				  pvar_CheckINid uuid
,pvar_tenantid uuid
,
pvar_checkinno Varchar(128)
,
pvar_guestno  Varchar(1024)
,
pvar_checkindate date
,
pvar_depositamount decimal(18,2)
,
pvar_status Varchar(128)
,
pvar_roomno  Varchar(1024)
,
pvar_checkintime Varchar(256)

				  ,pvar_modifieduser  uuid  

				  ,OUT pvar_returnMessage Varchar(4000)
			  )
			  RETURNS Varchar(4000) 
              AS $BODY$  
              DECLARE lv_viewactionroles Varchar(128);
              BEGIN

			  /*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 6:08:23 AM*/
			  IF "Check_Authorization"(pvar_modifieduser, 'CheckIN', 'edit') THEN


			  pvar_returnMessage:='';

			  
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
VALUES('CheckIN', NOW(),
(SELECT query_to_xml('SELECT * FROM CheckIN WHERE CheckIN.CheckINid= '''||pvar_CheckINid||'''', true, false, '')));

                    
                    UPDATE CheckIN SET
                    checkinno=pvar_checkinno
,guestno=pvar_guestno
,checkindate=pvar_checkindate
,depositamount=pvar_depositamount
,status=pvar_status
,roomno=pvar_roomno
,checkintime=pvar_checkintime

                    
                    ,modifieduser=pvar_modifieduser,modifieddate=NOW()
                    WHERE CheckINid=pvar_CheckINid;

                    

                    


					
							
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
																,'Update_Check_IN'
																,'Authorization Failed Update_Check_IN'
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
						,'Update_Check_IN'
						,'update failed'
						);
                        pvar_returnMessage := 'Update_Check_IN - update failed';*/
			  	
			  END
              $BODY$
              LANGUAGE plpgsql;

