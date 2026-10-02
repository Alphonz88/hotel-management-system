CREATE TABLE IF NOT EXISTS reservation
(
reservationid uuid  PRIMARY KEY
,tenantid uuid NULL
,viewertenantids Jsonb  NULL
,reservationidno Varchar(128) NULL
,guestidno Varchar(128) NULL
,bookinggate date NULL
,checkindate date NULL
,checkoutdate date NULL
,roomtype Varchar(128) NULL
,noofguests Varchar(128) NULL
,reservationstatus Varchar(128) NULL

,createduser uuid NOT NULL
,createddate  Timestamp(3) NOT NULL DEFAULT NOW()
,modifieduser uuid
,modifieddate Timestamp(3)
,isdeleted Boolean DEFAULT false
)


