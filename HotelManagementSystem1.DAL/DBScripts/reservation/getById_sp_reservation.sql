 
			  
			  CREATE OR REPLACE FUNCTION  "getById_sp_reservation"
			  (
				  pvar_reservationid Varchar
			  )
			  RETURNS TABLE(
                reservationidno Varchar
,guestidno Varchar
,bookinggate date
,checkindate date
,checkoutdate date
,roomtype Varchar
,noofguests Varchar
,reservationstatus Varchar
,createduser uuid
,createddate Timestamp(5)
,modifieduser uuid
,modifieddate Timestamp(5)
,tenantid uuid

                ,reservationid uuid
                
            )
            AS $BODY$
            BEGIN
			  /*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/30/2026 7:06:48 AM*/
               
              RETURN QUERY
			  SELECT 
				 reservation.reservationidno
,reservation.guestidno
,reservation.bookinggate
,reservation.checkindate
,reservation.checkoutdate
,reservation.roomtype
,reservation.noofguests
,reservation.reservationstatus

				 ,reservation.createduser,reservation.createddate,reservation.modifieduser,reservation.modifieddate
				 ,reservation.tenantid
                 ,reservation.reservationid
                    
			  FROM reservation
			  WHERE CAST(reservation.reservationid AS Varchar)=pvar_reservationid
                       ;

					 	
			  END
              $BODY$
              LANGUAGE plpgsql;

