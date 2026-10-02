
			  CREATE OR REPLACE FUNCTION  "getById_sp_all_Reservation"
              (
			  pvar_Reservationid Varchar
			  )
              RETURNS TABLE(
                tenantid uuid
,_tenantname Varchar
,"Reservationid" uuid
,reservationno Varchar
,guestno Varchar
,bookingdate Varchar
,checkindate Varchar
,checkoutdate Varchar
,reservationstatus Varchar
,roomtype Varchar
,noofguests Varchar
,createduser uuid
,createddate Timestamp(5)
,modifieduser uuid
,modifieddate Timestamp(5)

                
                
                
				
                )

              AS $BODY$
                BEGIN
			   
			  /*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 6:56:03 AM*/
			  		 
              RETURN QUERY
			  SELECT  
				 Reservation.tenantid
,tenant.businessname as _tenantname
,Reservation.Reservationid
,Reservation.reservationno
,Reservation.guestno
,CAST(COALESCE(to_char(Reservation.bookingdate,'dd/MM/yyyy'),'') AS Varchar) as bookingdate
,CAST(COALESCE(to_char(Reservation.checkindate,'dd/MM/yyyy'),'') AS Varchar) as checkindate
,CAST(COALESCE(to_char(Reservation.checkoutdate,'dd/MM/yyyy'),'') AS Varchar) as checkoutdate
,Reservation.reservationstatus
,Reservation.roomtype
,Reservation.noofguests

				 ,Reservation.createduser,Reservation.createddate,Reservation.modifieduser,Reservation.modifieddate
                 
                 
				 
			  FROM  Reservation 
 LEFT OUTER JOIN tenant ON Reservation.tenantid=tenant.tenantid

			  WHERE CAST(Reservation.Reservationid AS Varchar)=pvar_Reservationid ;
			  
					 	
			  END
              $BODY$
              LANGUAGE plpgsql;

