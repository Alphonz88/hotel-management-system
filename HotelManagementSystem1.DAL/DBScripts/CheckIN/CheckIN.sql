CREATE TABLE IF NOT EXISTS CheckIN
(
CheckINid uuid  PRIMARY KEY
,tenantid uuid NULL
,viewertenantids Jsonb  NULL
,checkinno Varchar(128) NULL
,guestno Varchar(1080) NULL
,checkindate date NULL
,depositamount decimal(18,2) NULL
,status Varchar(128) NULL
,roomno Varchar(1080) NULL
,checkintime Varchar(10) NULL

,createduser uuid NOT NULL
,createddate  Timestamp(3) NOT NULL DEFAULT NOW()
,modifieduser uuid
,modifieddate Timestamp(3)
,isdeleted Boolean DEFAULT false
)


