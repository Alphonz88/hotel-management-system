CREATE TABLE IF NOT EXISTS Invoice
(
Invoiceid uuid  PRIMARY KEY
,verifiedby uuid  NULL
,verifieddate Timestamp(3)  NULL
,reviewcomments Varchar(4000)  NULL
,invoicenumber Varchar(128) NULL
,guestno Varchar(1080) NULL
,bookingnumber Varchar(1080) NULL
,invoicedate date NULL
,roomcharges decimal(18,2) NULL
,taxdiscount decimal(18,2) NULL
,totalamount Varchar(256) NULL
,verifiedstatus Varchar(1080) NULL

,createduser uuid NOT NULL
,createddate  Timestamp(3) NOT NULL DEFAULT NOW()
,modifieduser uuid
,modifieddate Timestamp(3)
,isdeleted Boolean DEFAULT false
)


