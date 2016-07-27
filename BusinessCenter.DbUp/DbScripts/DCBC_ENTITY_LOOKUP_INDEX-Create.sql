USE [BusinessCenter]
GO

/****** Object:  Table [dbo].[DCBC_ENTITY_LOOKUP_INDEX]    Script Date: 05/09/2015 19:05:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[DCBC_ENTITY_LOOKUP_INDEX](
	[DATA_SOURCE] [varchar](50) NOT NULL,
	[LOOKUP_TYPE] [varchar](50) NOT NULL,
	[DCBC_ENTITY_ID] [int] NOT NULL,
	[LOOKUP_VALUE] [varchar](255) NOT NULL,
	[SEARCH_STRING] [varchar](255) NOT NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


