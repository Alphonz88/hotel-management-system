
			  CREATE OR REPLACE FUNCTION  "Add_Check_out"
			  (
				  pvar_Checkoutid uuid
,pvar_tenantid uuid
,
pvar_checkoutno Varchar(128)
,
pvar_guestno  Varchar(1024)
,
pvar_roomno  Varchar(1024)
,
pvar_checkoutdate date
,
pvar_checkouttime Varchar(256)
,
pvar_finalbill decimal(18,2)
,
pvar_status  Varchar(1024)
 
				  ,pvar_createduser  uuid 

				  ,OUT pvar_returnMessage Varchar(4000)
			  )
			  RETURNS Varchar(4000) 
              AS $BODY$  
              DECLARE lv_viewactionroles Varchar(128);
              BEGIN

				/*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 6:08:29 AM*/
		

			  
                                                                                    if pvar_Checkoutid is null then
                                                                                    pvar_Checkoutid:=gen_random_uuid();
                                                                                    end if;	
                                                                                    
			  
			  IF "Check_Authorization"(pvar_createduser, 'Checkout', 'create') THEN
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
IF(pvar_status is not null AND pvar_status!='0' AND LENGTH(pvar_status)>0)
                                                            THEN                        
                                                                 if(CAST((SELECT Count(T1.T1) 
                                                                FROM regexp_split_to_table(pvar_status, ',') AS T1
                                                                    INNER JOIN regexp_split_to_table((Select  fielddesc 
                                                                from lookups  where fieldname='status'
                                                                and entityname='Checkout' LIMIT 1), ',') AS T2 on T1.T1 = T2.T2) AS int) <> CAST((SELECT Count(T1.T1)
                                                                FROM regexp_split_to_table(pvar_status, ',')  AS T1) AS int))
                                                                THEN
                                                                        pvar_returnMessage := pvar_returnMessage || 'status value is invalid';


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
  
			  if(pvar_returnMessage='')
			  THEN

			  

			  INSERT INTO Checkout(
				 checkoutno
,guestno
,roomno
,checkoutdate
,checkouttime
,finalbill
,status

				 ,createduser
				 ,Checkoutid
				 ,tenantid
                
			  )
			  VALUES (
 				 pvar_checkoutno
,pvar_guestno
,pvar_roomno
,pvar_checkoutdate
,pvar_checkouttime
,pvar_finalbill
,pvar_status

				 ,pvar_createduser
				 ,pvar_Checkoutid
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
																,'Add_Check_out'
																,'Authorization Failed Add_Check_out'
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
						,'Add_Check_out'
						,'insert failed'
						);
                        pvar_returnMessage := 'Add_Check_out - Insert failed';*/
			  	
			  END
              $BODY$
              LANGUAGE plpgsql;

