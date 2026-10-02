CREATE TABLE IF NOT EXISTS Room
(
Roomid uuid  PRIMARY KEY
,tenantid uuid NULL
,viewertenantids Jsonb  NULL
,roomno int NULL
,floornumber int NULL
,capacitymaxnoofpersons Varchar(128) NULL
,bedtype Varchar(1080) NULL
,pricepernight Varchar(128) NULL
,availablestauts Varchar(1080) NULL
,roomtype uuid REFERENCES Reservation(Reservationid) NULL
,noofguests Varchar(1080) NULL

,createduser uuid NOT NULL
,createddate  Timestamp(3) NOT NULL DEFAULT NOW()
,modifieduser uuid
,modifieddate Timestamp(3)
,isdeleted Boolean DEFAULT false
)


