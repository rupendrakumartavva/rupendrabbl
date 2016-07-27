USE [BusinessCenter]
GO

/****** Object:  Table [dbo].[User]    Script Date: 05/09/2015 19:07:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Address] [nvarchar](max) NULL,
	[City] [nvarchar](max) NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[MobileNumber] [nvarchar](max) NULL,
	[PostalCode] [nvarchar](max) NULL,
	[State] [nvarchar](max) NULL,
	[LastLoginDateandTime] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[ChangeEmailValidate] [datetime] NULL,
	[ChangeEmailConfirmed] [bit] NULL,
	[PreviousEmailValidate] [datetime] NULL,
	[PreviousEmailConfirmed] [bit] NULL,
	[ActivationCode] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[SecurityQuestion1] [nvarchar](200) NULL,
	[SecurityQuestion2] [nvarchar](200) NULL,
	[SecurityQuestion3] [nvarchar](200) NULL,
	[SecurityAnswer1] [nvarchar](500) NULL,
	[SecurityAnswer2] [nvarchar](500) NULL,
	[SecurityAnswer3] [nvarchar](500) NULL,
	[Title] [nvarchar](50) NULL,
	[ActivationDate] [datetime] NULL,
	[SecondaryEmail] [nvarchar](150) NULL,
	[IsDelete] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[IsForgot] [bit] NULL,
	[UpdatedDate] [datetime] NULL,
	[NormalizedName]  AS (lower([Username])),
 CONSTRAINT [PK_dbo.User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


