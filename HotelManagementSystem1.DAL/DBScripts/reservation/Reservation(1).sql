CREATE TABLE IF NOT EXISTS Reservation
(
Reservationid uuid  PRIMARY KEY
,tenantid uuid NULL
,viewertenantids Jsonb  NULL
,reservationno Varchar(128) NULL
,guestno Varchar(1080) NULL
,bookingdate date NULL
,checkindate date NULL
,checkoutdate date NULL
,reservationstatus Varchar(128) NULL
,roomtype Varchar(1080) NULL
,noofguests Varchar(1080) NULL

,createduser uuid NOT NULL
,createddate  Timestamp(3) NOT NULL DEFAULT NOW()
,modifieduser uuid
,modifieddate Timestamp(3)
,isdeleted Boolean DEFAULT false
)


