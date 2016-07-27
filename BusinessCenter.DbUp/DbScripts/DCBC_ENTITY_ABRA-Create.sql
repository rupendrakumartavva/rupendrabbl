USE [BusinessCenter]
GO

/****** Object:  Table [dbo].[DCBC_ENTITY_ABRA]    Script Date: 05/09/2015 19:03:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[DCBC_ENTITY_ABRA](
	[DCBC_ENTITY_ID] [int] IDENTITY(20000000,1) NOT NULL,
	[REC_STATUS] [bit] NOT NULL,
	[LAST_UPDATE] [datetime] NOT NULL,
	[DCBC_ENTITY_SOURCE] [varchar](50) NOT NULL,
	[Serv_Prov_Code] [varchar](15) NOT NULL,
	[B1_PER_GROUP] [varchar](30) NOT NULL,
	[B1_PER_TYPE] [varchar](30) NOT NULL,
	[B1_PER_CATEGORY] [varchar](30) NOT NULL,
	[B1_APP_TYPE_ALIAS] [varchar](255) NULL,
	[APP_STATUS_GROUP_CODE] [varchar](30) NULL,
	[B1_SPECIAL_TEXT] [varchar](255) NULL,
	[License_Number] [varchar](30) NULL,
	[B1_TRACKING_NBR] [bigint] NULL,
	[License_Status] [varchar](30) NULL,
	[B1_APPL_STATUS_DATE] [datetime] NULL,
	[B1_CHECKBOX_TYPE] [varchar](30) NULL,
	[License_Expiration_Date] [varchar](4000) NULL,
	[Valid_Thru_Date] [varchar](4000) NULL,
	[Posting_Date] [varchar](4000) NULL,
	[Class_Type] [varchar](4000) NULL,
	[Class_Type_AttrValue] [varchar](200) NULL,
	[Establishment_Type] [varchar](4000) NULL,
	[Applicant_FNAME] [varchar](15) NULL,
	[Applicant_MNAME] [varchar](15) NULL,
	[Applicant_LNAME] [varchar](35) NULL,
	[Applicant_FULL_NAME] [varchar](80) NULL,
	[Applicant_RELATION] [varchar](30) NULL,
	[Applicant_BUSINESS_NAME] [varchar](255) NULL,
	[Solicitor_FNAME] [varchar](15) NULL,
	[Solicitor_MNAME] [varchar](15) NULL,
	[Solicitor_LNAME] [varchar](35) NULL,
	[Solicitor_FULL_NAME] [varchar](80) NULL,
	[Solicitor_RELATION] [varchar](30) NULL,
	[Solicitor_BUSINESS_NAME] [varchar](255) NULL,
	[Solicitor_ADDR1] [varchar](200) NULL,
	[Solicitor_ADDR2] [varchar](200) NULL,
	[Solicitor_ADDR3] [varchar](200) NULL,
	[Solicitor_CITY] [varchar](30) NULL,
	[Solicitor_STATE] [varchar](30) NULL,
	[Solicitor_ZIP] [varchar](10) NULL,
	[Manager_FNAME] [varchar](15) NULL,
	[Manager_MNAME] [varchar](15) NULL,
	[Manager_LNAME] [varchar](35) NULL,
	[Manager_FULL_NAME] [varchar](80) NULL,
	[Manager_RELATION] [varchar](30) NULL,
	[Manager_BUSINESS_NAME] [varchar](255) NULL,
	[Manager_ADDR1] [varchar](200) NULL,
	[Manager_ADDR2] [varchar](200) NULL,
	[Manager_ADDR3] [varchar](200) NULL,
	[Manager_CITY] [varchar](30) NULL,
	[Manager_STATE] [varchar](30) NULL,
	[Manager_ZIP] [varchar](10) NULL,
	[B1_PRIMARY_ADDR_FLG] [varchar](1) NULL,
	[B1_HSE_NBR_START] [int] NULL,
	[B1_HSE_NBR_END] [int] NULL,
	[B1_HSE_FRAC_NBR_START] [varchar](4) NULL,
	[B1_HSE_FRAC_NBR_END] [varchar](3) NULL,
	[B1_UNIT_START] [varchar](10) NULL,
	[B1_STR_NAME] [varchar](40) NULL,
	[B1_STR_SUFFIX] [varchar](30) NULL,
	[B1_STR_SUFFIX_DIR] [varchar](5) NULL,
	[B1_SITUS_CITY] [varchar](40) NULL,
	[B1_SITUS_STATE] [varchar](30) NULL,
	[B1_SITUS_ZIP] [varchar](10) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[DCBC_ENTITY_ABRA] ADD  CONSTRAINT [DF_DCBC_ENTITY_ABRA_REC_STATUS]  DEFAULT ((1)) FOR [REC_STATUS]
GO

ALTER TABLE [dbo].[DCBC_ENTITY_ABRA] ADD  CONSTRAINT [DF_DCBC_ENTITY_ABRA_LAST_UPDATE]  DEFAULT (getdate()) FOR [LAST_UPDATE]
GO


