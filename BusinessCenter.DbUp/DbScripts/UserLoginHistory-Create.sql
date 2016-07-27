
/****** Object:  Table [dbo].[UserLoginHistory]    Script Date: 05/31/2015 18:32:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserLoginHistory](
	[LoginHisId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[LastLoginDate] [datetime] NULL,
	[Count] [int] NULL,
 CONSTRAINT [PK_UserLoginHistory] PRIMARY KEY CLUSTERED 
(
	[LoginHisId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


