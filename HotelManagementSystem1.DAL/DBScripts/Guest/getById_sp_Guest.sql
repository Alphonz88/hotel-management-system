 
			  
			  CREATE OR REPLACE FUNCTION  "getById_sp_Guest"
			  (
				  pvar_Guestid Varchar
			  )
			  RETURNS TABLE(
                guestno Varchar
,guestname Varchar
,gender Varchar
,phonenumber Varchar
,email Varchar
,addressline1 Varchar
,city Varchar
,statename Varchar
,country Varchar
,idproof Varchar
,createduser uuid
,createddate Timestamp(5)
,modifieduser uuid
,modifieddate Timestamp(5)
,tenantid uuid

                ,Guestid uuid
                
            )
            AS $BODY$
            BEGIN
			  /*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 6:08:15 AM*/
               
              RETURN QUERY
			  SELECT 
				 Guest.guestno
,Guest.guestname
,Guest.gender
,Guest.phonenumber
,Guest.email
,Guest.addressline1
,Guest.city
,Guest.statename
,Guest.country
,Guest.idproof

				 ,Guest.createduser,Guest.createddate,Guest.modifieduser,Guest.modifieddate
				 ,Guest.tenantid
                 ,Guest.Guestid
                    
			  FROM Guest
			  WHERE CAST(Guest.Guestid AS Varchar)=pvar_Guestid
                       ;

					 	
			  END
              $BODY$
              LANGUAGE plpgsql;

