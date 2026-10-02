CREATE TABLE IF NOT EXISTS Feedback
(
Feedbackid uuid  PRIMARY KEY
,tenantid uuid NULL
,viewertenantids Jsonb  NULL
,feedbackno Varchar(128) DEFAULT 'YYYY-MM-9999' NOT NULL
,guestno Varchar(1080) NULL
,staffrating Varchar(256) NULL
,roomrating Varchar(256) NULL
,UNIQUE(tenantid,feedbackno)

,createduser uuid NOT NULL
,createddate  Timestamp(3) NOT NULL DEFAULT NOW()
,modifieduser uuid
,modifieddate Timestamp(3)
,isdeleted Boolean DEFAULT false
)


