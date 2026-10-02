 
			  
			  CREATE OR REPLACE FUNCTION  "getById_sp_Reservation"
			  (
				  pvar_Reservationid Varchar
			  )
			  RETURNS TABLE(
                reservationno Varchar
,guestno Varchar
,bookingdate date
,checkindate date
,checkoutdate date
,reservationstatus Varchar
,roomtype Varchar
,noofguests Varchar
,createduser uuid
,createddate Timestamp(5)
,modifieduser uuid
,modifieddate Timestamp(5)
,tenantid uuid

                ,Reservationid uuid
                
            )
            AS $BODY$
            BEGIN
			  /*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 6:56:03 AM*/
               
              RETURN QUERY
			  SELECT 
				 Reservation.reservationno
,Reservation.guestno
,Reservation.bookingdate
,Reservation.checkindate
,Reservation.checkoutdate
,Reservation.reservationstatus
,Reservation.roomtype
,Reservation.noofguests

				 ,Reservation.createduser,Reservation.createddate,Reservation.modifieduser,Reservation.modifieddate
				 ,Reservation.tenantid
                 ,Reservation.Reservationid
                    
			  FROM Reservation
			  WHERE CAST(Reservation.Reservationid AS Varchar)=pvar_Reservationid
                       ;

					 	
			  END
              $BODY$
              LANGUAGE plpgsql;

