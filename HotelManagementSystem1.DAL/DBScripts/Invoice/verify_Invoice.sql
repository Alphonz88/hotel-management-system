 
			  
			  CREATE OR REPLACE FUNCTION  "verify_Invoice"
			  (
				  pvar_invoiceid Varchar
				  ,pvar_verifiedby Varchar(50)
				  ,pvar_verifiedstatus Varchar(128)
				  ,pvar_reviewcomments Varchar(128)
				  
				  ,OUT pvar_returnMessage Varchar(4000)
			  )
			  RETURNS Varchar(4000) 
              AS $BODY$  
                DECLARE lvar_invoiceid_array UUID[];
                lvar_invoiceid UUID;  
                 lvar_invalid_status_count INTEGER;
                  lvar_total_invoice_count INTEGER;
              BEGIN
	        /*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 8:57:49 AM*/

              lvar_invoiceid_array := STRING_TO_ARRAY(pvar_invoiceid, ',');

              lvar_total_invoice_count := array_length(lvar_invoiceid_array, 1);
               -- Check for invalid status orders
               SELECT COUNT(*) INTO lvar_invalid_status_count
               FROM Invoice 
               WHERE invoiceid = ANY(lvar_invoiceid_array)
               AND verifiedstatus <> 'Ready For Review';

               IF lvar_invalid_status_count > 0 THEN
        CASE 
            WHEN lvar_total_invoice_count = 1 THEN 
                pvar_returnMessage := 'The given record is already reviewed. Please try again.';
            WHEN lvar_invalid_status_count = lvar_total_invoice_count THEN
                pvar_returnMessage := 'All given records are already reviewed. Please try again.';
            ELSE 
                pvar_returnMessage := 'Some of the given records are already reviewed. Please try again.';
        END CASE;
        RETURN;
    END IF;


            FOREACH lvar_invoiceid IN ARRAY lvar_invoiceid_array
            LOOP

			  UPDATE Invoice
			  SET verifiedby=CAST(pvar_verifiedby AS UUID)
			  ,verifiedstatus=pvar_verifiedstatus
			  ,verifieddate=NOW()
			  ,reviewcomments=pvar_reviewcomments
			  
			  WHERE  invoiceid=lvar_invoiceid;


             INSERT INTO reviewlogsInvoice(invoiceid,  verifiedstatus, reviewcomments, createduser)
	         VALUES (lvar_invoiceid, pvar_verifiedstatus, pvar_reviewcomments,CAST(pvar_verifiedby AS UUID));
            
            END LOOP;

			   
				pvar_returnMessage:='201.1';
					 	
			  END
              $BODY$
              LANGUAGE plpgsql;

