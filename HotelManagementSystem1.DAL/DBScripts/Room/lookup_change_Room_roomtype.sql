
								CREATE OR REPLACE FUNCTION  "lookup_change_Room_roomtype"(
								pvar_Reservationid Varchar(50)=null
                                )
								RETURNS TABLE("Reservationid" Varchar
,roomtype Varchar
,noofguests Varchar
) 
						 		AS $BODY$
								BEGIN
								/*This code generated from tDev Powered by Mahat, Build Number :#2024-01-001(Updated on 06-01-2024 12:57PM) on 7/31/2026 8:30:56 AM*/
										
                                RETURN QUERY
								SELECT  
									CAST(Reservation.Reservationid AS Varchar) as Reservationid
,CAST(Reservation.roomtype AS Varchar) as roomtype
,CAST(Reservation.noofguests AS Varchar) as noofguests

								FROM Reservation
							   WHERE (CAST(Reservation.Reservationid AS VARCHAR) = pvar_Reservationid)
;
								
											
								END
                                $BODY$
                                LANGUAGE plpgsql;

