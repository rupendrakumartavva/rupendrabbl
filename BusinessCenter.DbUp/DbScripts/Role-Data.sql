USE [BusinessCenter]
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

GO
INSERT [dbo].[Role] ([Id], [Name]) VALUES (1, N'Super Admin')
GO
INSERT [dbo].[Role] ([Id], [Name]) VALUES (2, N'Admin')
GO
INSERT [dbo].[Role] ([Id], [Name]) VALUES (3, N'Employee')
GO
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
