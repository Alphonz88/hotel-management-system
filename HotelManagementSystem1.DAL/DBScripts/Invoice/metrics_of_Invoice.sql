
			  CREATE OR REPLACE FUNCTION  "metrics_of_Invoice"
              ()
			  RETURNS TABLE(reportjson text)
			  AS $BODY$
                
			  BEGIN     
			  /*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 8:57:49 AM*/
               DROP TABLE IF EXISTS report_temp;
              CREATE TEMP TABLE IF NOT EXISTS report_temp(col text);
              DELETE FROM report_temp;
			  		

              
                insert into report_temp (select row_to_json(a)
                from (
                select 
                (       select array_to_json(array_agg(row_to_json(c)))
                        from (
                            select  
                            sum(cast(invoice.totalamount as decimal(18,2))) as metric,cast(coalesce(to_char(invoice.invoicedate,'dd/mm/yyyy'),'') as varchar)
as dimension
                            from  invoice 

                            where invoice.isdeleted=false
 and invoice.verifiedstatus  ilike '%approved%'

                             group by cast(coalesce(to_char(invoice.invoicedate,'dd/mm/yyyy'),'') as varchar)

                        ) c
                ) as line_invoicedate_sum_totalamount
                )a);
insert into report_temp (select row_to_json(a)
                from (
                select 
                (       select array_to_json(array_agg(row_to_json(c)))
                        from (
                            select  
                            max(cast(invoice.taxdiscount  as decimal(18,2))) as metric,invoice.invoicenumber
as dimension
                            from  invoice 

                            where invoice.isdeleted=false
 and invoice.verifiedstatus  ilike '%approved%'

                             group by invoice.invoicenumber

                        ) c
                ) as doughnut_invoicenumber_maximum_taxdiscount
                )a);
insert into report_temp (select row_to_json(a)
                from (
                select 
                (       select array_to_json(array_agg(row_to_json(c)))
                        from (
                            select  
                            cast(count(*) as decimal(18,2)) as metric,invoice.bookingnumber
as dimension
                            from  invoice 

                            where invoice.isdeleted=false
 and invoice.verifiedstatus  ilike '%approved%'

                             group by invoice.bookingnumber

                        ) c
                ) as pie_bookingnumber_count_roomcharges
                )a);


                
    			RETURN QUERY 
                SELECT * FROM report_temp;

                

			  END
              $BODY$
              LANGUAGE plpgsql;

