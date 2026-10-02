CREATE TABLE IF NOT EXISTS guest
(
guestid uuid  PRIMARY KEY
,tenantid uuid NULL
,viewertenantids Jsonb  NULL
,guestidno Varchar(128) NULL
,guestname Varchar(128) NULL
,gender Varchar(128) NULL
,phonenumber Varchar(20) NULL
,email Varchar(128) NULL
,addressline1 Varchar(256) NULL
,addressline2 Varchar(256) NULL
,city Varchar(256) NULL
,statename Varchar(256) NULL
,country Varchar(1080) NULL
,idproof Varchar(4000) NULL
,nationality Varchar(128) NULL

,createduser uuid NOT NULL
,createddate  Timestamp(3) NOT NULL DEFAULT NOW()
,modifieduser uuid
,modifieddate Timestamp(3)
,isdeleted Boolean DEFAULT false
)


