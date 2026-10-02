
			  CREATE OR REPLACE FUNCTION  "Add_reservation"
			  (
				  pvar_Reservationid uuid
,pvar_tenantid uuid
,
pvar_reservationno Varchar(128)
,
pvar_guestno  Varchar(1024)
,
pvar_bookingdate date
,
pvar_checkindate date
,
pvar_checkoutdate date
,
pvar_reservationstatus Varchar(128)
,
pvar_roomtype Varchar(256)
,
pvar_noofguests  Varchar(1024)
 
				  ,pvar_createduser  uuid 

				  ,OUT pvar_returnMessage Varchar(4000)
			  )
			  RETURNS Varchar(4000) 
              AS $BODY$  
              DECLARE lv_viewactionroles Varchar(128);
              BEGIN

				/*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 6:56:03 AM*/
		

			  
                                                                                    if pvar_Reservationid is null then
                                                                                    pvar_Reservationid:=gen_random_uuid();
                                                                                    end if;	
                                                                                    
			  
			  IF "Check_Authorization"(pvar_createduser, 'Reservation', 'create') THEN
			  pvar_returnMessage:='';
			  
              IF(pvar_noofguests is not null AND pvar_noofguests!='0' AND LENGTH(pvar_noofguests)>0)
                                                            THEN                        
                                                                 if(CAST((SELECT Count(T1.T1) 
                                                                FROM regexp_split_to_table(pvar_noofguests, ',') AS T1
                                                                    INNER JOIN regexp_split_to_table((Select  fielddesc 
                                                                from lookups  where fieldname='noofguests'
                                                                and entityname='Reservation' LIMIT 1), ',') AS T2 on T1.T1 = T2.T2) AS int) <> CAST((SELECT Count(T1.T1)
                                                                FROM regexp_split_to_table(pvar_noofguests, ',')  AS T1) AS int))
                                                                THEN
                                                                        pvar_returnMessage := pvar_returnMessage || 'noofguests value is invalid';


                                                                END IF;
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
IF(pvar_roomtype is not null AND pvar_roomtype!='0' AND LENGTH(pvar_roomtype)>0)
                                                            THEN                        
                                                                 if(CAST((SELECT Count(T1.T1) 
                                                                FROM regexp_split_to_table(pvar_roomtype, ',') AS T1
                                                                    INNER JOIN regexp_split_to_table((Select  fielddesc 
                                                                from lookups  where fieldname='roomtype'
                                                                and entityname='Reservation' LIMIT 1), ',') AS T2 on T1.T1 = T2.T2) AS int) <> CAST((SELECT Count(T1.T1)
                                                                FROM regexp_split_to_table(pvar_roomtype, ',')  AS T1) AS int))
                                                                THEN
                                                                        pvar_returnMessage := pvar_returnMessage || 'roomtype value is invalid';


                                                                END IF;
                                                            END IF;
  
			  if(pvar_returnMessage='')
			  THEN

			  

			  INSERT INTO Reservation(
				 reservationno
,guestno
,bookingdate
,checkindate
,checkoutdate
,reservationstatus
,roomtype
,noofguests

				 ,createduser
				 ,Reservationid
				 ,tenantid
                
			  )
			  VALUES (
 				 pvar_reservationno
,pvar_guestno
,pvar_bookingdate
,pvar_checkindate
,pvar_checkoutdate
,pvar_reservationstatus
,pvar_roomtype
,pvar_noofguests

				 ,pvar_createduser
				 ,pvar_Reservationid
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
																,'Add_reservation'
																,'Authorization Failed Add_reservation'
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
						,'Add_reservation'
						,'insert failed'
						);
                        pvar_returnMessage := 'Add_reservation - Insert failed';*/
			  	
			  END
              $BODY$
              LANGUAGE plpgsql;

