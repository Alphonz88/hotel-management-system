
			  CREATE OR REPLACE FUNCTION  "Add_reservation"
			  (
				  pvar_reservationid uuid
,pvar_tenantid uuid
,
pvar_reservationidno Varchar(128)
,
pvar_guestidno Varchar(128)
,
pvar_bookinggate date
,
pvar_checkindate date
,
pvar_checkoutdate date
,
pvar_roomtype Varchar(128)
,
pvar_noofguests Varchar(128)
,
pvar_reservationstatus Varchar(128)
 
				  ,pvar_createduser  uuid 

				  ,OUT pvar_returnMessage Varchar(4000)
			  )
			  RETURNS Varchar(4000) 
              AS $BODY$  
              DECLARE lv_viewactionroles Varchar(128);
              BEGIN

				/*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/30/2026 7:06:48 AM*/
		

			  
                                                                                    if pvar_reservationid is null then
                                                                                    pvar_reservationid:=gen_random_uuid();
                                                                                    end if;	
                                                                                    
			  
			  IF "Check_Authorization"(pvar_createduser, 'reservation', 'create') THEN
			  pvar_returnMessage:='';
			  
                
			  if(pvar_returnMessage='')
			  THEN

			  

			  INSERT INTO reservation(
				 reservationidno
,guestidno
,bookinggate
,checkindate
,checkoutdate
,roomtype
,noofguests
,reservationstatus

				 ,createduser
				 ,reservationid
				 ,tenantid
                
			  )
			  VALUES (
 				 pvar_reservationidno
,pvar_guestidno
,pvar_bookinggate
,pvar_checkindate
,pvar_checkoutdate
,pvar_roomtype
,pvar_noofguests
,pvar_reservationstatus

				 ,pvar_createduser
				 ,pvar_reservationid
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

