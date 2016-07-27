USE [BusinessCenter]
GO

/****** Object:  Table [dbo].[DCBC_ENTITY_BBL]    Script Date: 05/09/2015 19:03:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[DCBC_ENTITY_BBL](
	[DCBC_ENTITY_ID] [int] IDENTITY(10000000,1) NOT NULL,
	[REC_STATUS] [bit] NOT NULL,
	[LAST_UPDATE] [datetime] NOT NULL,
	[DCBC_ENTITY_SOURCE] [varchar](50) NOT NULL,
	[SERV_PROV_CODE] [varchar](15) NOT NULL,
	[B1_PER_ID1] [varchar](5) NOT NULL,
	[B1_PER_ID2] [varchar](5) NOT NULL,
	[B1_PER_ID3] [varchar](5) NOT NULL,
	[B1_PER_GROUP] [varchar](30) NOT NULL,
	[B1_PER_TYPE] [varchar](30) NOT NULL,
	[B1_PER_SUB_TYPE] [varchar](30) NOT NULL,
	[B1_PER_CATEGORY] [varchar](30) NOT NULL,
	[B1_APPL_STATUS] [varchar](30) NULL,
	[B1_ALT_ID] [varchar](30) NULL,
	[B1_APPL_STATUS_DATE] [char](10) NULL,
	[ISSUED_DATE] [datetime] NULL,
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
	[B1_SITUS_ZIP] [varchar](10) NULL,
	[FULL_ADDRESS] [varchar](300) NULL,
	[B1_APP_TYPE_ALIAS] [varchar](255) NULL,
	[SSL] [varchar](24) NULL,
	[REC_FUL_NAM] [varchar](70) NOT NULL,
	[Contact_Business_Name] [varchar](255) NULL,
	[Contact_FirstName] [varchar](15) NULL,
	[Contact_MiddleName] [varchar](15) NULL,
	[Contact_LastName] [varchar](35) NULL,
	[Billing_Address1] [varchar](200) NULL,
	[Billing_Address2] [varchar](200) NULL,
	[Billing_Address3] [varchar](200) NULL,
	[Billing_CITY] [varchar](30) NULL,
	[Billing_STATE] [varchar](30) NULL,
	[Billing_ZIP] [varchar](10) NULL,
	[OwnrApplicant_BUSINESS_NAME] [varchar](255) NULL,
	[OwnrApplicant_FNAME] [varchar](15) NULL,
	[OwnrApplicant_MNAME] [varchar](15) NULL,
	[OwnrApplicant_LNAME] [varchar](35) NULL,
	[OwnrApplicant_Address1] [varchar](200) NULL,
	[OwnrApplicant_Address2] [varchar](200) NULL,
	[OwnrApplicant_Address3] [varchar](200) NULL,
	[OwnrApplicant_CITY] [varchar](30) NULL,
	[OwnrApplicant_STATE] [varchar](30) NULL,
	[OwnrApplicant_ZIP] [varchar](10) NULL,
	[RegAgent_BUSINESS_NAME] [varchar](255) NULL,
	[RegAgent_FNAME] [varchar](15) NULL,
	[RegAgent_MNAME] [varchar](15) NULL,
	[RegAgent_LNAME] [varchar](35) NULL,
	[RegAgent_Address1] [varchar](200) NULL,
	[RegAgent_Address2] [varchar](200) NULL,
	[RegAgent_Address3] [varchar](200) NULL,
	[RegAgent_CITY] [varchar](30) NULL,
	[RegAgent_STATE] [varchar](30) NULL,
	[RegAgent_ZIP] [varchar](10) NULL,
	[Attr_TRADE_NAME] [varchar](200) NULL,
	[Business_Org] [varchar](200) NULL,
	[Expiration_Date] [varchar](4000) NULL,
	[License_Issued_Date] [varchar](4000) NULL,
	[Period_Start_Date] [varchar](4000) NULL,
	[License_Category] [nvarchar](1024) NULL,
	[License_Category_Full] [nvarchar](1024) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[DCBC_ENTITY_BBL] ADD  CONSTRAINT [DF_DCBC_ENTITY_BBL_REC_STATUS]  DEFAULT ((1)) FOR [REC_STATUS]
GO

ALTER TABLE [dbo].[DCBC_ENTITY_BBL] ADD  CONSTRAINT [DF_DCBC_ENTITY_BBL_LAST_UPDATE]  DEFAULT (getdate()) FOR [LAST_UPDATE]
GO


