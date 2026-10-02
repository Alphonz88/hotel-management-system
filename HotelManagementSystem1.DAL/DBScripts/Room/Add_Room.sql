
			  CREATE OR REPLACE FUNCTION  "Add_Room"
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
 
				  ,pvar_createduser  uuid 

				  ,OUT pvar_returnMessage Varchar(4000)
			  )
			  RETURNS Varchar(4000) 
              AS $BODY$  
              DECLARE lv_viewactionroles Varchar(128);
              BEGIN

				/*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 8:30:56 AM*/
		

			  
                                                                                    if pvar_Roomid is null then
                                                                                    pvar_Roomid:=gen_random_uuid();
                                                                                    end if;	
                                                                                    
			  
			  IF "Check_Authorization"(pvar_createduser, 'Room', 'create') THEN
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
  
			  if(pvar_returnMessage='')
			  THEN

			  

			  INSERT INTO Room(
				 roomno
,floornumber
,capacitymaxnoofpersons
,bedtype
,pricepernight
,availablestauts
,roomtype
,noofguests

				 ,createduser
				 ,Roomid
				 ,tenantid
                
			  )
			  VALUES (
 				 pvar_roomno
,pvar_floornumber
,pvar_capacitymaxnoofpersons
,pvar_bedtype
,pvar_pricepernight
,pvar_availablestauts
,pvar_roomtype
,pvar_noofguests

				 ,pvar_createduser
				 ,pvar_Roomid
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
																,'Add_Room'
																,'Authorization Failed Add_Room'
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
						,'Add_Room'
						,'insert failed'
						);
                        pvar_returnMessage := 'Add_Room - Insert failed';*/
			  	
			  END
              $BODY$
              LANGUAGE plpgsql;

