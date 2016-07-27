
/****** Object:  Table [dbo].[KeywordMaster]    Script Date: 05/31/2015 18:33:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[KeywordMaster](
	[KeyId] [bigint] IDENTITY(1,1) NOT NULL,
	[Keywords] [nvarchar](500) NULL,
	[TypeID] [nvarchar](50) NULL,
	[SearchCriteria] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_KeyWordMaster] PRIMARY KEY CLUSTERED 
(
	[KeyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


