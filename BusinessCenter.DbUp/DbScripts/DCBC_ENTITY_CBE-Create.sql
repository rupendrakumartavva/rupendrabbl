USE [BusinessCenter]
GO

/****** Object:  Table [dbo].[DCBC_ENTITY_CBE]    Script Date: 05/09/2015 19:04:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[DCBC_ENTITY_CBE](
	[DCBC_ENTITY_ID] [int] IDENTITY(30000000,1) NOT NULL,
	[REC_STATUS] [bit] NOT NULL,
	[LAST_UPDATE] [datetime] NOT NULL,
	[DCBC_ENTITY_SOURCE] [varchar](50) NOT NULL,
	[LsdbeID] [int] NULL,
	[CompanyID] [int] NULL,
	[BusinessName] [nvarchar](128) NOT NULL,
	[BusinessAddress] [varchar](4000) NULL,
	[BusinessAddressShort] [varchar](4000) NULL,
	[BusinessPhone] [varchar](16) NULL,
	[BusinessFax] [varchar](16) NULL,
	[BusinessEmail] [varchar](256) NULL,
	[GIS_Ward] [varchar](128) NULL,
	[CertificationExpiry] [datetime] NULL,
	[NIGPAppID] [int] NULL,
	[TradeDivAppID] [int] NULL,
	[BAPAppID] [int] NULL,
	[BusinessStructure] [varchar](128) NULL,
	[IncorporationDate] [datetime] NULL,
	[BusinessDescription] [varchar](2000) NOT NULL,
	[BusinessContact] [nvarchar](129) NULL,
	[LSDBE_Number] [varchar](50) NULL,
	[RefPoints] [int] NULL,
	[RefPointsDesc] [varchar](255) NULL,
	[BusinessAddress1] [varchar](256) NULL,
	[BusinessAddress2] [varchar](256) NULL,
	[BusinessCity] [varchar](256) NULL,
	[BusinessState] [varchar](2) NULL,
	[BusinessZip1] [varchar](5) NULL,
	[BusinessZip2] [varchar](4) NULL,
	[LSDBEBusinessOptions] [varchar](512) NULL,
	[BusinessWebsite] [varchar](256) NULL,
	[Lsdbe_PersonAppID] [int] NULL,
	[MailingAddress] [varchar](361) NULL,
	[PublicContact] [nvarchar](129) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[DCBC_ENTITY_CBE] ADD  CONSTRAINT [DF_DCBC_ENTITY_CBE_REC_STATUS]  DEFAULT ((1)) FOR [REC_STATUS]
GO

ALTER TABLE [dbo].[DCBC_ENTITY_CBE] ADD  CONSTRAINT [DF_DCBC_ENTITY_CBE_LAST_UPDATE]  DEFAULT (getdate()) FOR [LAST_UPDATE]
GO


