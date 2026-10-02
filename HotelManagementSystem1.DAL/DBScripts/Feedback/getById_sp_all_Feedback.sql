
			  CREATE OR REPLACE FUNCTION  "getById_sp_all_Feedback"
              (
			  pvar_Feedbackid Varchar
			  )
              RETURNS TABLE(
                tenantid uuid
,_tenantname Varchar
,"Feedbackid" uuid
,feedbackno Varchar
,guestno Varchar
,staffrating Varchar
,roomrating Varchar
,createduser uuid
,createddate Timestamp(5)
,modifieduser uuid
,modifieddate Timestamp(5)

                
                
                
				
                )

              AS $BODY$
                BEGIN
			   
			  /*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 7:00:03 AM*/
			  		 
              RETURN QUERY
			  SELECT  
				 Feedback.tenantid
,tenant.businessname as _tenantname
,Feedback.Feedbackid
,Feedback.feedbackno
,Feedback.guestno
,Feedback.staffrating
,Feedback.roomrating

				 ,Feedback.createduser,Feedback.createddate,Feedback.modifieduser,Feedback.modifieddate
                 
                 
				 
			  FROM  Feedback 
 LEFT OUTER JOIN tenant ON Feedback.tenantid=tenant.tenantid

			  WHERE CAST(Feedback.Feedbackid AS Varchar)=pvar_Feedbackid ;
			  
					 	
			  END
              $BODY$
              LANGUAGE plpgsql;

