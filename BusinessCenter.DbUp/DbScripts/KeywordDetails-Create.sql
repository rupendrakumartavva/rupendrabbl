
/****** Object:  Table [dbo].[KeywordDetails]    Script Date: 05/31/2015 18:33:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[KeywordDetails](
	[KeywordDid] [bigint] IDENTITY(1,1) NOT NULL,
	[KeyId] [bigint] NULL,
	[KeyCount] [int] NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_KeywordDetails] PRIMARY KEY CLUSTERED 
(
	[KeywordDid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[KeywordDetails]  WITH CHECK ADD  CONSTRAINT [FK_KeywordDetails_KeywordDetails] FOREIGN KEY([KeywordDid])
REFERENCES [dbo].[KeywordDetails] ([KeywordDid])
GO

ALTER TABLE [dbo].[KeywordDetails] CHECK CONSTRAINT [FK_KeywordDetails_KeywordDetails]
GO


