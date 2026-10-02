
			  CREATE OR REPLACE FUNCTION  "Update_Payment"
			  (
				  pvar_Paymentid uuid
,pvar_tenantid uuid
,
pvar_paymentno Varchar(128)
,
pvar_roomrent decimal(18,2)
,
pvar_noofdays int
,
pvar_totalamount Varchar(256)
,
pvar_invoiceno  Varchar(1024)
,
pvar_guestno  Varchar(1024)
,
pvar_transactionno Varchar(128)
,
pvar_paymentmethod  Varchar(1024)
,
pvar_paymentdate date
,
pvar_paymentstatus  Varchar(1024)

				  ,pvar_modifieduser  uuid  

				  ,OUT pvar_returnMessage Varchar(4000)
			  )
			  RETURNS Varchar(4000) 
              AS $BODY$  
              DECLARE lv_viewactionroles Varchar(128);
              BEGIN

			  /*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 6:46:37 AM*/
			  IF "Check_Authorization"(pvar_modifieduser, 'Payment', 'edit') THEN


			  pvar_returnMessage:='';

			  
               IF(pvar_paymentstatus is not null AND pvar_paymentstatus!='0' AND LENGTH(pvar_paymentstatus)>0)
                                                            THEN                        
                                                                 if(CAST((SELECT Count(T1.T1) 
                                                                FROM regexp_split_to_table(pvar_paymentstatus, ',') AS T1
                                                                    INNER JOIN regexp_split_to_table((Select  fielddesc 
                                                                from lookups  where fieldname='paymentstatus'
                                                                and entityname='Payment' LIMIT 1), ',') AS T2 on T1.T1 = T2.T2) AS int) <> CAST((SELECT Count(T1.T1)
                                                                FROM regexp_split_to_table(pvar_paymentstatus, ',')  AS T1) AS int))
                                                                THEN
                                                                        pvar_returnMessage := pvar_returnMessage || 'paymentstatus value is invalid';


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
IF(pvar_invoiceno is not null AND pvar_invoiceno!='0' AND LENGTH(pvar_invoiceno)>0)
                                                            THEN                        
                                                                 if(CAST((SELECT Count(T1.T1) 
                                                                    FROM regexp_split_to_table(pvar_invoiceno, ',') AS T1
                                                                        INNER JOIN (Select DISTINCT Invoice.invoicenumber from Invoice) AS T2 on T1.T1 = T2.invoicenumber) AS int) <> CAST((SELECT Count(T1.T1)
                                                                    FROM regexp_split_to_table(pvar_invoiceno, ',')  AS T1) AS int))
                                                                    THEN
                                                                         pvar_returnMessage := pvar_returnMessage || ' invoiceno value is invalid';


                                                                    END IF;
                                                                    END IF;
IF(pvar_paymentmethod is not null AND pvar_paymentmethod!='0' AND LENGTH(pvar_paymentmethod)>0)
                                                            THEN                        
                                                                 if(CAST((SELECT Count(T1.T1) 
                                                                FROM regexp_split_to_table(pvar_paymentmethod, ',') AS T1
                                                                    INNER JOIN regexp_split_to_table((Select  fielddesc 
                                                                from lookups  where fieldname='paymentmethod'
                                                                and entityname='Payment' LIMIT 1), ',') AS T2 on T1.T1 = T2.T2) AS int) <> CAST((SELECT Count(T1.T1)
                                                                FROM regexp_split_to_table(pvar_paymentmethod, ',')  AS T1) AS int))
                                                                THEN
                                                                        pvar_returnMessage := pvar_returnMessage || 'paymentmethod value is invalid';


                                                                END IF;
                                                            END IF;
 
			  IF(pvar_returnMessage='')
			  THEN
               
                    INSERT INTO history
VALUES('Payment', NOW(),
(SELECT query_to_xml('SELECT * FROM Payment WHERE Payment.Paymentid= '''||pvar_Paymentid||'''', true, false, '')));

                    
                    UPDATE Payment SET
                    paymentno=pvar_paymentno
,roomrent=pvar_roomrent
,noofdays=pvar_noofdays
,totalamount=pvar_totalamount
,invoiceno=pvar_invoiceno
,guestno=pvar_guestno
,transactionno=pvar_transactionno
,paymentmethod=pvar_paymentmethod
,paymentdate=pvar_paymentdate
,paymentstatus=pvar_paymentstatus

                    
                    ,modifieduser=pvar_modifieduser,modifieddate=NOW()
                    WHERE Paymentid=pvar_Paymentid;

                    

                    


					
							
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
																,'Update_Payment'
																,'Authorization Failed Update_Payment'
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
						,'Update_Payment'
						,'update failed'
						);
                        pvar_returnMessage := 'Update_Payment - update failed';*/
			  	
			  END
              $BODY$
              LANGUAGE plpgsql;

