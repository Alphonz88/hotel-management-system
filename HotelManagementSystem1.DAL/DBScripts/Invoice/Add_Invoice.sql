
			  CREATE OR REPLACE FUNCTION  "Add_Invoice"
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
 
				  ,pvar_createduser  uuid 

				  ,OUT pvar_returnMessage Varchar(4000)
			  )
			  RETURNS Varchar(4000) 
              AS $BODY$  
              DECLARE lv_viewactionroles Varchar(128);
              BEGIN

				/*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 8:57:49 AM*/
		

			  
                                                                                    if pvar_Invoiceid is null then
                                                                                    pvar_Invoiceid:=gen_random_uuid();
                                                                                    end if;	
                                                                                    
			  
			  IF "Check_Authorization"(pvar_createduser, 'Invoice', 'create') THEN
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
  
			  if(pvar_returnMessage='')
			  THEN

			  pvar_verifiedstatus:='Ready For Review';

			  INSERT INTO Invoice(
				 invoicenumber
,guestno
,bookingnumber
,invoicedate
,roomcharges
,taxdiscount
,totalamount
,verifiedstatus

				 ,createduser
				 ,Invoiceid
				 
                
			  )
			  VALUES (
 				 pvar_invoicenumber
,pvar_guestno
,pvar_bookingnumber
,pvar_invoicedate
,pvar_roomcharges
,pvar_taxdiscount
,pvar_totalamount
,pvar_verifiedstatus

				 ,pvar_createduser
				 ,pvar_Invoiceid
				 
                   
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
																,'Add_Invoice'
																,'Authorization Failed Add_Invoice'
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
						,'Add_Invoice'
						,'insert failed'
						);
                        pvar_returnMessage := 'Add_Invoice - Insert failed';*/
			  	
			  END
              $BODY$
              LANGUAGE plpgsql;

