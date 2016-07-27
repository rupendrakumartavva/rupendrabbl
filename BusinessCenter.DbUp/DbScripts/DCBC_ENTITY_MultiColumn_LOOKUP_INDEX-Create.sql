USE [BusinessCenter]
GO

/****** Object:  Table [dbo].[DCBC_ENTITY_MultiColumn_LOOKUP_INDEX]    Script Date: 05/09/2015 19:05:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[DCBC_ENTITY_MultiColumn_LOOKUP_INDEX](
	[DATA_SOURCE] [varchar](50) NOT NULL,
	[DCBC_ENTITY_ID] [int] NOT NULL,
	[LicenseNumberOrig] [varchar](255) NOT NULL,
	[LicenseNumberLookup] [varchar](255) NOT NULL,
	[CompanyNameOrig] [varchar](255) NOT NULL,
	[CompanyNameLookup] [varchar](255) NOT NULL,
	[FirstNameOrig] [varchar](255) NOT NULL,
	[FirstNameLookup] [varchar](255) NOT NULL,
	[LastNameOrig] [varchar](255) NOT NULL,
	[LastNameLookup] [varchar](255) NOT NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


