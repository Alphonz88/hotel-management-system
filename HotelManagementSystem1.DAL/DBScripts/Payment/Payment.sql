CREATE TABLE IF NOT EXISTS Payment
(
Paymentid uuid  PRIMARY KEY
,tenantid uuid NULL
,viewertenantids Jsonb  NULL
,paymentno Varchar(128) NULL
,roomrent decimal(18,2) NULL
,noofdays int NULL
,totalamount Varchar(256) NULL
,invoiceno Varchar(1080) NULL
,guestno Varchar(1080) NULL
,transactionno Varchar(128) NULL
,paymentmethod Varchar(1080) NULL
,paymentdate date NULL
,paymentstatus Varchar(1080) NULL

,createduser uuid NOT NULL
,createddate  Timestamp(3) NOT NULL DEFAULT NOW()
,modifieduser uuid
,modifieddate Timestamp(3)
,isdeleted Boolean DEFAULT false
)


