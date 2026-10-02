
			  CREATE OR REPLACE FUNCTION  "Add_Housekeeping"
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
 
				  ,pvar_createduser  uuid 

				  ,OUT pvar_returnMessage Varchar(4000)
			  )
			  RETURNS Varchar(4000) 
              AS $BODY$  
              DECLARE lv_viewactionroles Varchar(128);lvar_curday_housekeepingno Varchar(10);lvar_val_housekeepingno int;
              BEGIN

				/*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 6:08:31 AM*/
		

			  
                                                                                    if pvar_Housekeepingid is null then
                                                                                    pvar_Housekeepingid:=gen_random_uuid();
                                                                                    end if;	
                                                                                    
			  
select cast(to_char(NOW(),'yyyy') as Varchar(4)) ||'-'|| RIGHT('00' ||cast(to_char(NOW(),'MM') as Varchar(2)),2) 
                                          INTO lvar_curday_housekeepingno;
                                        select COALESCE(max(RIGHT(Housekeeping.housekeepingno,4)),'0') INTO lvar_val_housekeepingno from
                                        Housekeeping where substring(Housekeeping.housekeepingno,1,7) = lvar_curday_housekeepingno and (Housekeeping.housekeepingno) NOT LIKE '%/%';
                                        lvar_val_housekeepingno:=lvar_val_housekeepingno + 1;
                                        pvar_housekeepingno:= (lvar_curday_housekeepingno||'-'|| cast(to_char(lvar_val_housekeepingno,'fm0000') as Varchar(4)));

			  IF "Check_Authorization"(pvar_createduser, 'Housekeeping', 'create') THEN
			  pvar_returnMessage:='';
			  IF EXISTS (SELECT * from Housekeeping where upper(Housekeeping.housekeepingno::varchar) = upper(pvar_housekeepingno::varchar) and Housekeeping.tenantid=pvar_tenantid)
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
  
			  if(pvar_returnMessage='')
			  THEN

			  

			  INSERT INTO Housekeeping(
				 housekeepingno
,cleaningdate
,roomno
,cleaningstatus
,supervisor
,remark

				 ,createduser
				 ,Housekeepingid
				 ,tenantid
                
			  )
			  VALUES (
 				 pvar_housekeepingno
,pvar_cleaningdate
,pvar_roomno
,pvar_cleaningstatus
,pvar_supervisor
,pvar_remark

				 ,pvar_createduser
				 ,pvar_Housekeepingid
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
																,'Add_Housekeeping'
																,'Authorization Failed Add_Housekeeping'
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
						,'Add_Housekeeping'
						,'insert failed'
						);
                        pvar_returnMessage := 'Add_Housekeeping - Insert failed';*/
			  	
			  END
              $BODY$
              LANGUAGE plpgsql;

