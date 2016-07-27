USE [BusinessCenter]
GO
SET IDENTITY_INSERT [dbo].[UserRole] ON 

GO
INSERT [dbo].[UserRole] ([Id], [UserId], [RoleId]) VALUES (1, 1, 1)
GO
INSERT [dbo].[UserRole] ([Id], [UserId], [RoleId]) VALUES (2, 1, 2)
GO
INSERT [dbo].[UserRole] ([Id], [UserId], [RoleId]) VALUES (3, 1, 3)
GO

INSERT [dbo].[UserRole] ([Id], [UserId], [RoleId]) VALUES (4, 2, 3)
GO

SET IDENTITY_INSERT [dbo].[UserRole] OFF
GO
