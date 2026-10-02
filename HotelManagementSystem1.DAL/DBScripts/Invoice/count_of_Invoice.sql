
			  CREATE OR REPLACE FUNCTION  "count_of_Invoice"
              ()
			  RETURNS TABLE(count bigint,verifiedstatus varchar)
			  AS $BODY$
              
              
			  BEGIN
			  /*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 8:57:49 AM*/
			  		
              
              RETURN QUERY
			  SELECT  
			  Count(*),Invoice.verifiedstatus 
			  FROM Invoice
			  WHERE Invoice.isdeleted=false 

               GROUP BY Invoice.verifiedstatus;
			  
					 	
			  END
              $BODY$
              LANGUAGE plpgsql;

