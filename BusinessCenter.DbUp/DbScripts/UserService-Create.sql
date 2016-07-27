USE [BusinessCenter]
GO

/****** Object:  Table [dbo].[UserService]    Script Date: 05/09/2015 19:08:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserService](
	[UserListId] [int] IDENTITY(1,1) NOT NULL,
	[DATA_SOURCE] [nvarchar](50) NULL,
	[DCBC_ENTITY_ID] [int] NULL,
	[CompanyName] [nvarchar](255) NULL,
	[LicenseNumber] [nvarchar](255) NULL,
	[CreatedDate] [datetime] NULL,
	[Status] [nvarchar](50) NULL,
	[CreatedBy] [int] NULL,
	[updatedBy] [int] NULL,
	[UpdatedDate] [datetime] NULL,
	[UserId] [int] NULL,
 CONSTRAINT [PK_UserService] PRIMARY KEY CLUSTERED 
(
	[UserListId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


