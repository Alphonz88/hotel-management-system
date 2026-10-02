CREATE TABLE IF NOT EXISTS Checkout
(
Checkoutid uuid  PRIMARY KEY
,tenantid uuid NULL
,viewertenantids Jsonb  NULL
,checkoutno Varchar(128) NULL
,guestno Varchar(1080) NULL
,roomno Varchar(1080) NULL
,checkoutdate date NULL
,checkouttime Varchar(10) NULL
,finalbill decimal(18,2) NULL
,status Varchar(1080) NULL

,createduser uuid NOT NULL
,createddate  Timestamp(3) NOT NULL DEFAULT NOW()
,modifieduser uuid
,modifieddate Timestamp(3)
,isdeleted Boolean DEFAULT false
)


