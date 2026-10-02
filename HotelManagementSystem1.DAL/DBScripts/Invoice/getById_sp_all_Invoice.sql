
			  CREATE OR REPLACE FUNCTION  "getById_sp_all_Invoice"
              (
			  pvar_Invoiceid Varchar
			  )
              RETURNS TABLE(
                "Invoiceid" uuid
,invoicenumber Varchar
,guestno Varchar
,bookingnumber Varchar
,invoicedate Varchar
,roomcharges decimal
,taxdiscount decimal
,totalamount Varchar
,verifiedstatus Varchar
,createduser uuid
,createddate Timestamp(5)
,modifieduser uuid
,modifieddate Timestamp(5)

                
                
				,automaton_review_logs json
                ,automaton_review_logs_history json
				
			
                ,authorized_users text
                ,authorized_users_mobile text
                 ,verifieddate Timestamp(3)
                ,reviewcomments Varchar(1024)
                )

              AS $BODY$
                BEGIN
			   
			  /*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 8:57:49 AM*/
			  		 
            RETURN QUERY
            SELECT  
            Invoice.Invoiceid
,Invoice.invoicenumber
,Invoice.guestno
,Invoice.bookingnumber
,CAST(COALESCE(to_char(Invoice.invoicedate,'dd/MM/yyyy'),'') AS Varchar) as invoicedate
,Invoice.roomcharges
,Invoice.taxdiscount
,Invoice.totalamount
,Invoice.verifiedstatus

            ,Invoice.createduser,Invoice.createddate,Invoice.modifieduser,Invoice.modifieddate
            		
            
 ,(SELECT json_agg(J) FROM (
                    SELECT  
                    CAST(COALESCE(to_char(reviewlogsInvoice.createddate,'dd/MM/yyyy HH24:MI'),'') AS Varchar) as "Reviewed On"
                    ,users.firstname as "Reviewed By"
                    ,reviewlogsInvoice.verifiedstatus "Status" 
                    ,COALESCE(reviewlogsInvoice.reviewcomments,'-')  "Comments"
                    FROM reviewlogsInvoice
                    INNER JOIN users
                    ON reviewlogsInvoice.createduser=users.usersid
                    WHERE reviewlogsInvoice.Invoiceid=Invoice.Invoiceid
                    ORDER BY Invoice.createddate DESC
                    ) J)
                    as automaton_review_logs
                    ,(SELECT json_agg(J) FROM (
                    SELECT  
                    CAST(COALESCE(to_char(reviewlogsInvoice.createddate,'dd/MM/yyyy HH24:MI'),'') AS Varchar) as "Reviewed On"
                    ,users.firstname as "Reviewed By"
                    ,reviewlogsInvoice.verifiedstatus "Status" 
                    ,COALESCE(reviewlogsInvoice.reviewcomments,'-')  "Comments"
                    ,reviewlogsInvoice.Invoiceid as "Invoiceid"
                    FROM reviewlogsInvoice
                    INNER JOIN users
                    ON reviewlogsInvoice.createduser=users.usersid
  
                    INNER JOIN Invoice INInvoice ON reviewlogsInvoice.Invoiceid=INInvoice.Invoiceid
                    WHERE reviewlogsInvoice.Invoiceid=Invoice.Invoiceid
                    ORDER BY INInvoice.createddate DESC
                    ) J)
                    as automaton_review_logs_history
			
            ,(SELECT STRING_AGG(u.emailid, ', ') AS authorized_users
            FROM users u
            JOIN RoleAuthorization ra ON (
            u.userrole = ANY(string_to_array(ra.viewactionroles, ',')) -- Split the authorizedroles column by commas
            )
            WHERE ra.actionname = 'CheckerView' and controllername='Invoice') as authorized_users 
            ,(SELECT STRING_AGG(u.mobilenumber, ', ') AS authorized_users_mobile
            FROM users u
            JOIN RoleAuthorization ra ON (
            u.userrole = ANY(string_to_array(ra.viewactionroles, ',')) -- Split the authorizedroles column by commas
            AND u.isdeleted=false
            )
            WHERE ra.actionname = 'CheckerView' and controllername='Invoice') as authorized_users_mobile 
            ,Invoice.verifieddate
            ,Invoice.reviewcomments
            FROM  Invoice 

            WHERE CAST(Invoice.Invoiceid AS Varchar)=pvar_Invoiceid ;
			  
					 	
			  END
              $BODY$
              LANGUAGE plpgsql;

