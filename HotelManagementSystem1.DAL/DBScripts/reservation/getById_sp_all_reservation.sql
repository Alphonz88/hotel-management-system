
			  CREATE OR REPLACE FUNCTION  "getById_sp_all_reservation"
              (
			  pvar_reservationid Varchar
			  )
              RETURNS TABLE(
                tenantid uuid
,_tenantname Varchar
,"reservationid" uuid
,reservationidno Varchar
,guestidno Varchar
,bookinggate Varchar
,checkindate Varchar
,checkoutdate Varchar
,roomtype Varchar
,noofguests Varchar
,reservationstatus Varchar
,createduser uuid
,createddate Timestamp(5)
,modifieduser uuid
,modifieddate Timestamp(5)

                
                
                
				
                )

              AS $BODY$
                BEGIN
			   
			  /*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/30/2026 7:06:48 AM*/
			  		 
              RETURN QUERY
			  SELECT  
				 reservation.tenantid
,tenant.businessname as _tenantname
,reservation.reservationid
,reservation.reservationidno
,reservation.guestidno
,CAST(COALESCE(to_char(reservation.bookinggate,'dd/MM/yyyy'),'') AS Varchar) as bookinggate
,CAST(COALESCE(to_char(reservation.checkindate,'dd/MM/yyyy'),'') AS Varchar) as checkindate
,CAST(COALESCE(to_char(reservation.checkoutdate,'dd/MM/yyyy'),'') AS Varchar) as checkoutdate
,reservation.roomtype
,reservation.noofguests
,reservation.reservationstatus

				 ,reservation.createduser,reservation.createddate,reservation.modifieduser,reservation.modifieddate
                 
                 
				 
			  FROM  reservation 
 LEFT OUTER JOIN tenant ON reservation.tenantid=tenant.tenantid

			  WHERE CAST(reservation.reservationid AS Varchar)=pvar_reservationid ;
			  
					 	
			  END
              $BODY$
              LANGUAGE plpgsql;

