USE [BusinessCenter]
GO



/****** Object:  Table [dbo].[MailTemplate]    Script Date: 12/09/2015 09:09:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MailTemplate](
	[EmailTemplateId] [nvarchar](100) NOT NULL,
	[Type] [nvarchar](20) NULL,
	[Subject] [nvarchar](500) NULL,
	[MailSentFailCount] [int] NULL,
	[MailContent] [text] NULL,
	[IsMailSent] [bit] NULL,
	[MailCreatedDate] [datetime] NULL,
	[MailSentDate] [datetime] NULL,
 CONSTRAINT [PK_MailTemplate] PRIMARY KEY CLUSTERED 
(
	[EmailTemplateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


