
			  CREATE OR REPLACE FUNCTION  "Add_Payment"
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
 
				  ,pvar_createduser  uuid 

				  ,OUT pvar_returnMessage Varchar(4000)
			  )
			  RETURNS Varchar(4000) 
              AS $BODY$  
              DECLARE lv_viewactionroles Varchar(128);
              BEGIN

				/*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 6:46:37 AM*/
		

			  
                                                                                    if pvar_Paymentid is null then
                                                                                    pvar_Paymentid:=gen_random_uuid();
                                                                                    end if;	
                                                                                    
			  
			  IF "Check_Authorization"(pvar_createduser, 'Payment', 'create') THEN
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
  
			  if(pvar_returnMessage='')
			  THEN

			  

			  INSERT INTO Payment(
				 paymentno
,roomrent
,noofdays
,totalamount
,invoiceno
,guestno
,transactionno
,paymentmethod
,paymentdate
,paymentstatus

				 ,createduser
				 ,Paymentid
				 ,tenantid
                
			  )
			  VALUES (
 				 pvar_paymentno
,pvar_roomrent
,pvar_noofdays
,pvar_totalamount
,pvar_invoiceno
,pvar_guestno
,pvar_transactionno
,pvar_paymentmethod
,pvar_paymentdate
,pvar_paymentstatus

				 ,pvar_createduser
				 ,pvar_Paymentid
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
																,'Add_Payment'
																,'Authorization Failed Add_Payment'
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
						,'Add_Payment'
						,'insert failed'
						);
                        pvar_returnMessage := 'Add_Payment - Insert failed';*/
			  	
			  END
              $BODY$
              LANGUAGE plpgsql;

