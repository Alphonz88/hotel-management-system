
			  CREATE OR REPLACE FUNCTION  "Update_reservation"
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

				  ,pvar_modifieduser  uuid  

				  ,OUT pvar_returnMessage Varchar(4000)
			  )
			  RETURNS Varchar(4000) 
              AS $BODY$  
              DECLARE lv_viewactionroles Varchar(128);
              BEGIN

			  /*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/30/2026 7:06:48 AM*/
			  IF "Check_Authorization"(pvar_modifieduser, 'reservation', 'edit') THEN


			  pvar_returnMessage:='';

			  
                
			  IF(pvar_returnMessage='')
			  THEN
               
                    INSERT INTO history
VALUES('reservation', NOW(),
(SELECT query_to_xml('SELECT * FROM reservation WHERE reservation.reservationid= '''||pvar_reservationid||'''', true, false, '')));

                    
                    UPDATE reservation SET
                    reservationidno=pvar_reservationidno
,guestidno=pvar_guestidno
,bookinggate=pvar_bookinggate
,checkindate=pvar_checkindate
,checkoutdate=pvar_checkoutdate
,roomtype=pvar_roomtype
,noofguests=pvar_noofguests
,reservationstatus=pvar_reservationstatus

                    
                    ,modifieduser=pvar_modifieduser,modifieddate=NOW()
                    WHERE reservationid=pvar_reservationid;

                    

                    


					
							
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
																,'Update_reservation'
																,'Authorization Failed Update_reservation'
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
						,'Update_reservation'
						,'update failed'
						);
                        pvar_returnMessage := 'Update_reservation - update failed';*/
			  	
			  END
              $BODY$
              LANGUAGE plpgsql;

