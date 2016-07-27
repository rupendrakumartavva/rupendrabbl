USE [BusinessCenter]
GO

/****** Object:  Table [dbo].[AuditRecord]    Script Date: 05/31/2015 18:30:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AuditRecord](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[AuditId] [nvarchar](1000) NULL,
	[SessionId] [nvarchar](500) NULL,
	[IpAddress] [nvarchar](50) NULL,
	[UserName] [nvarchar](100) NULL,
	[UrlAccessed] [nvarchar](1000) NULL,
	[TimeAccessed] [datetime] NULL,
	[Data] [nvarchar](max) NULL,
 CONSTRAINT [PK_AuditRecord] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


