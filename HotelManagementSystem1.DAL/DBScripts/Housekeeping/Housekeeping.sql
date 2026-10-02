CREATE TABLE IF NOT EXISTS Housekeeping
(
Housekeepingid uuid  PRIMARY KEY
,tenantid uuid NULL
,viewertenantids Jsonb  NULL
,housekeepingno Varchar(128) DEFAULT 'YYYY-MM-9999' NOT NULL
,cleaningdate date NOT NULL
,roomno Varchar(1080) NULL
,cleaningstatus Varchar(1080) NOT NULL
,supervisor Varchar(128) NULL
,remark text NULL
,UNIQUE(tenantid,housekeepingno)

,createduser uuid NOT NULL
,createddate  Timestamp(3) NOT NULL DEFAULT NOW()
,modifieduser uuid
,modifieddate Timestamp(3)
,isdeleted Boolean DEFAULT false
)


