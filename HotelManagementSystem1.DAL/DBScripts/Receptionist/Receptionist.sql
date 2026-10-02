CREATE TABLE IF NOT EXISTS Receptionist
(
Receptionistid uuid  PRIMARY KEY
,tenantid uuid NULL
,viewertenantids Jsonb  NULL
,employeeno Varchar(128) NULL
,employeename Varchar(128) NULL
,shift Varchar(128) NULL
,experience Varchar(128) NULL
,salary Varchar(128) NULL
,phonenumber Varchar(20) NULL
,email Varchar(128) NULL

,createduser uuid NOT NULL
,createddate  Timestamp(3) NOT NULL DEFAULT NOW()
,modifieduser uuid
,modifieddate Timestamp(3)
,isdeleted Boolean DEFAULT false
)


