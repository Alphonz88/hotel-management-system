
			  CREATE OR REPLACE FUNCTION  "Update_Invoice"
			  (
				  pvar_Invoiceid uuid
,
pvar_invoicenumber Varchar(128)
,
pvar_guestno  Varchar(1024)
,
pvar_bookingnumber  Varchar(1024)
,
pvar_invoicedate date
,
pvar_roomcharges decimal(18,2)
,
pvar_taxdiscount decimal(18,2)
,
pvar_totalamount Varchar(256)
,
pvar_verifiedstatus  Varchar(1024)

				  ,pvar_modifieduser  uuid  

				  ,OUT pvar_returnMessage Varchar(4000)
			  )
			  RETURNS Varchar(4000) 
              AS $BODY$  
              DECLARE lv_viewactionroles Varchar(128);
              BEGIN

			  /*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 8:57:49 AM*/
			  IF "Check_Authorization"(pvar_modifieduser, 'Invoice', 'edit') THEN


			  pvar_returnMessage:='';

			  
               IF(pvar_bookingnumber is not null AND pvar_bookingnumber!='0' AND LENGTH(pvar_bookingnumber)>0)
                                                            THEN                        
                                                                 if(CAST((SELECT Count(T1.T1) 
                                                                    FROM regexp_split_to_table(pvar_bookingnumber, ',') AS T1
                                                                        INNER JOIN (Select DISTINCT Reservation.reservationno from Reservation) AS T2 on T1.T1 = T2.reservationno) AS int) <> CAST((SELECT Count(T1.T1)
                                                                    FROM regexp_split_to_table(pvar_bookingnumber, ',')  AS T1) AS int))
                                                                    THEN
                                                                         pvar_returnMessage := pvar_returnMessage || ' bookingnumber value is invalid';


                                                                    END IF;
                                                                    END IF;
IF(pvar_verifiedstatus is not null AND pvar_verifiedstatus!='0' AND LENGTH(pvar_verifiedstatus)>0)
                                                            THEN                        
                                                                 if(CAST((SELECT Count(T1.T1) 
                                                                FROM regexp_split_to_table(pvar_verifiedstatus, ',') AS T1
                                                                    INNER JOIN regexp_split_to_table((Select  fielddesc 
                                                                from lookups  where fieldname='verifiedstatus'
                                                                and entityname='Invoice' LIMIT 1), ',') AS T2 on T1.T1 = T2.T2) AS int) <> CAST((SELECT Count(T1.T1)
                                                                FROM regexp_split_to_table(pvar_verifiedstatus, ',')  AS T1) AS int))
                                                                THEN
                                                                        pvar_returnMessage := pvar_returnMessage || 'verifiedstatus value is invalid';


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
 
			  IF(pvar_returnMessage='')
			  THEN
                IF EXISTS (
                SELECT 1
                FROM Invoice
                WHERE Invoiceid = pvar_Invoiceid
                  AND lower(coalesce(verifiedstatus,'')) IN ('approved','rejected')
            ) THEN
                pvar_returnMessage := 'Cannot update: Already Approved/Rejected';
                RETURN;
            END IF;
                    INSERT INTO history
VALUES('Invoice', NOW(),
(SELECT query_to_xml('SELECT * FROM Invoice WHERE Invoice.Invoiceid= '''||pvar_Invoiceid||'''', true, false, '')));

                    pvar_verifiedstatus:='Ready For Review';
                    UPDATE Invoice SET
                    invoicenumber=pvar_invoicenumber
,guestno=pvar_guestno
,bookingnumber=pvar_bookingnumber
,invoicedate=pvar_invoicedate
,roomcharges=pvar_roomcharges
,taxdiscount=pvar_taxdiscount
,totalamount=pvar_totalamount
,verifiedstatus=pvar_verifiedstatus

                    
                    ,modifieduser=pvar_modifieduser,modifieddate=NOW()
                    WHERE Invoiceid=pvar_Invoiceid;

                    

                    


					
							
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
																,'Update_Invoice'
																,'Authorization Failed Update_Invoice'
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
						,'Update_Invoice'
						,'update failed'
						);
                        pvar_returnMessage := 'Update_Invoice - update failed';*/
			  	
			  END
              $BODY$
              LANGUAGE plpgsql;

