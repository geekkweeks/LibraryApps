USE [LibraryDB]
GO
/****** Object:  Table [dbo].[Access]    Script Date: 02/04/2020 11:52:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Access](
	[AccessId] [int] IDENTITY(1,1) NOT NULL,
	[MenuId] [int] NULL,
	[UserId] [int] NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[CreatedTime] [datetime] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Access] PRIMARY KEY CLUSTERED 
(
	[AccessId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BookTransaction]    Script Date: 02/04/2020 11:52:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookTransaction](
	[TransactionId] [int] IDENTITY(1,1) NOT NULL,
	[BookId] [int] NULL,
	[StatusId] [int] NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
	[Days] [int] NULL,
	[Price] [float] NULL,
	[Total] [float] NULL,
	[CreatedBy] [nvarchar](200) NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL,
 CONSTRAINT [PK_BookTransaction] PRIMARY KEY CLUSTERED 
(
	[TransactionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MasterBook]    Script Date: 02/04/2020 11:52:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MasterBook](
	[BookId] [int] IDENTITY(1,1) NOT NULL,
	[BookTitle] [nvarchar](200) NOT NULL,
	[Author] [nvarchar](200) NULL,
	[CategoryId] [int] NULL,
	[Price] [float] NULL,
	[IsAvailable] [bit] NOT NULL,
 CONSTRAINT [PK_MasterBook] PRIMARY KEY CLUSTERED 
(
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MasterCategoryBook]    Script Date: 02/04/2020 11:52:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MasterCategoryBook](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](200) NULL,
 CONSTRAINT [PK_MasterCategoryBook] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MasterMenu]    Script Date: 02/04/2020 11:52:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MasterMenu](
	[MenuId] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NULL,
	[MethodName] [nvarchar](50) NULL,
	[MenuName] [nvarchar](50) NULL,
	[ControllerName] [nvarchar](50) NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_MasterMenu] PRIMARY KEY CLUSTERED 
(
	[MenuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MasterRole]    Script Date: 02/04/2020 11:52:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MasterRole](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](200) NULL,
	[CreatedTime] [datetime] NULL,
	[CreatedBy] [nvarchar](200) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_MasterRole] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MasterStatus]    Script Date: 02/04/2020 11:52:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MasterStatus](
	[StatusId] [int] IDENTITY(1,1) NOT NULL,
	[StatusName] [nvarchar](200) NULL,
 CONSTRAINT [PK_MasterStatus] PRIMARY KEY CLUSTERED 
(
	[StatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 02/04/2020 11:52:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Password] [nvarchar](4000) NULL,
	[UserName] [nvarchar](100) NULL,
	[FullName] [nvarchar](1000) NULL,
	[RoleId] [int] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[BookTransaction] ON 

INSERT [dbo].[BookTransaction] ([TransactionId], [BookId], [StatusId], [StartDate], [EndDate], [Days], [Price], [Total], [CreatedBy], [CreatedTime], [UpdatedTime]) VALUES (2, 6, NULL, CAST(N'2020-04-01' AS Date), CAST(N'2020-04-03' AS Date), 2, 342, 684, N'alfan', CAST(N'2020-04-01T07:24:50.143' AS DateTime), CAST(N'2020-04-01T07:24:50.907' AS DateTime))
INSERT [dbo].[BookTransaction] ([TransactionId], [BookId], [StatusId], [StartDate], [EndDate], [Days], [Price], [Total], [CreatedBy], [CreatedTime], [UpdatedTime]) VALUES (1002, 6, NULL, CAST(N'2020-04-01' AS Date), CAST(N'2020-04-03' AS Date), 2, 2300, 4600, N'zahri', CAST(N'2020-04-01T16:53:50.213' AS DateTime), CAST(N'2020-04-01T16:53:50.213' AS DateTime))
INSERT [dbo].[BookTransaction] ([TransactionId], [BookId], [StatusId], [StartDate], [EndDate], [Days], [Price], [Total], [CreatedBy], [CreatedTime], [UpdatedTime]) VALUES (1003, 7, NULL, CAST(N'2020-04-01' AS Date), CAST(N'2020-04-02' AS Date), 1, 1200, 1200, N'alfan', CAST(N'2020-04-01T18:06:52.247' AS DateTime), CAST(N'2020-04-01T18:06:52.247' AS DateTime))
SET IDENTITY_INSERT [dbo].[BookTransaction] OFF
SET IDENTITY_INSERT [dbo].[MasterBook] ON 

INSERT [dbo].[MasterBook] ([BookId], [BookTitle], [Author], [CategoryId], [Price], [IsAvailable]) VALUES (6, N'Fiction 1', N'MR Fic A', 1, 2300, 1)
INSERT [dbo].[MasterBook] ([BookId], [BookTitle], [Author], [CategoryId], [Price], [IsAvailable]) VALUES (7, N'Horror 1 Update', N'MR Horror A', 2, 1200, 0)
INSERT [dbo].[MasterBook] ([BookId], [BookTitle], [Author], [CategoryId], [Price], [IsAvailable]) VALUES (8, N'Comedy 1', N'MR Comedy A', 3, 290, 1)
INSERT [dbo].[MasterBook] ([BookId], [BookTitle], [Author], [CategoryId], [Price], [IsAvailable]) VALUES (9, N'Science 2', N'MR Science A', 4, 34500, 1)
SET IDENTITY_INSERT [dbo].[MasterBook] OFF
SET IDENTITY_INSERT [dbo].[MasterCategoryBook] ON 

INSERT [dbo].[MasterCategoryBook] ([CategoryId], [CategoryName]) VALUES (1, N'Fiction')
INSERT [dbo].[MasterCategoryBook] ([CategoryId], [CategoryName]) VALUES (2, N'Horror')
INSERT [dbo].[MasterCategoryBook] ([CategoryId], [CategoryName]) VALUES (3, N'Comedy')
INSERT [dbo].[MasterCategoryBook] ([CategoryId], [CategoryName]) VALUES (4, N'Science')
SET IDENTITY_INSERT [dbo].[MasterCategoryBook] OFF
SET IDENTITY_INSERT [dbo].[MasterMenu] ON 

INSERT [dbo].[MasterMenu] ([MenuId], [RoleId], [MethodName], [MenuName], [ControllerName], [CreatedBy], [CreatedTime], [UpdatedTime], [IsActive]) VALUES (1, 1, N'Index', N'Master Book', N'Book', NULL, NULL, NULL, 1)
INSERT [dbo].[MasterMenu] ([MenuId], [RoleId], [MethodName], [MenuName], [ControllerName], [CreatedBy], [CreatedTime], [UpdatedTime], [IsActive]) VALUES (2, 1, N'BookTransaction', N'Book Transaction', N'Book', NULL, NULL, NULL, NULL)
INSERT [dbo].[MasterMenu] ([MenuId], [RoleId], [MethodName], [MenuName], [ControllerName], [CreatedBy], [CreatedTime], [UpdatedTime], [IsActive]) VALUES (3, 2, N'BookTransaction', N'Book Transaction', N'Book', NULL, NULL, NULL, NULL)
INSERT [dbo].[MasterMenu] ([MenuId], [RoleId], [MethodName], [MenuName], [ControllerName], [CreatedBy], [CreatedTime], [UpdatedTime], [IsActive]) VALUES (4, 1, N'Index', N'Users', N'User', NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[MasterMenu] OFF
SET IDENTITY_INSERT [dbo].[MasterRole] ON 

INSERT [dbo].[MasterRole] ([RoleId], [RoleName], [CreatedTime], [CreatedBy], [IsActive]) VALUES (1, N'Admin', CAST(N'2020-03-28T00:00:00.000' AS DateTime), N'System', 1)
INSERT [dbo].[MasterRole] ([RoleId], [RoleName], [CreatedTime], [CreatedBy], [IsActive]) VALUES (2, N'Customer', CAST(N'2020-03-28T00:00:00.000' AS DateTime), N'System', 1)
SET IDENTITY_INSERT [dbo].[MasterRole] OFF
SET IDENTITY_INSERT [dbo].[MasterStatus] ON 

INSERT [dbo].[MasterStatus] ([StatusId], [StatusName]) VALUES (1, N'Outstanding')
INSERT [dbo].[MasterStatus] ([StatusId], [StatusName]) VALUES (2, N'Approved')
INSERT [dbo].[MasterStatus] ([StatusId], [StatusName]) VALUES (3, N'Rejected')
SET IDENTITY_INSERT [dbo].[MasterStatus] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserId], [Password], [UserName], [FullName], [RoleId], [IsActive]) VALUES (3, N'wZ9wLxspfHTuJqbYd4f3fg==', N'lula', N'zahriyono', 2, 1)
INSERT [dbo].[User] ([UserId], [Password], [UserName], [FullName], [RoleId], [IsActive]) VALUES (6, N'wZ9wLxspfHTuJqbYd4f3fg==', N'alfan', N'zahri', 2, 1)
INSERT [dbo].[User] ([UserId], [Password], [UserName], [FullName], [RoleId], [IsActive]) VALUES (1004, N'wZ9wLxspfHTuJqbYd4f3fg==', N'nuran', N'nuran ferhana', 1, 1)
INSERT [dbo].[User] ([UserId], [Password], [UserName], [FullName], [RoleId], [IsActive]) VALUES (2004, N'wZ9wLxspfHTuJqbYd4f3fg==', N'zahri', N'Alfan zahriyono', 2, 1)
INSERT [dbo].[User] ([UserId], [Password], [UserName], [FullName], [RoleId], [IsActive]) VALUES (2006, N'Ce4XEBWdobGwH4tUaopeKg==', N'bambang', N'purwanto', 2, 0)
INSERT [dbo].[User] ([UserId], [Password], [UserName], [FullName], [RoleId], [IsActive]) VALUES (2007, N'Ce4XEBWdobGwH4tUaopeKg==', N'afgan', N'wati', 1, 1)
INSERT [dbo].[User] ([UserId], [Password], [UserName], [FullName], [RoleId], [IsActive]) VALUES (2008, N'Ce4XEBWdobGwH4tUaopeKg==', N'ajeng', N'wati', 2, 0)
INSERT [dbo].[User] ([UserId], [Password], [UserName], [FullName], [RoleId], [IsActive]) VALUES (2009, N'Ce4XEBWdobGwH4tUaopeKg==', N'brian', N'brian', 2, 0)
INSERT [dbo].[User] ([UserId], [Password], [UserName], [FullName], [RoleId], [IsActive]) VALUES (2010, N'Ce4XEBWdobGwH4tUaopeKg==', N'brian', N'brian', 2, 1)
INSERT [dbo].[User] ([UserId], [Password], [UserName], [FullName], [RoleId], [IsActive]) VALUES (2011, N'Ce4XEBWdobGwH4tUaopeKg==', N'kanjen', N'kanjen', 2, 0)
INSERT [dbo].[User] ([UserId], [Password], [UserName], [FullName], [RoleId], [IsActive]) VALUES (2012, N'Ce4XEBWdobGwH4tUaopeKg==', N'admin', N'admin', 1, 1)
SET IDENTITY_INSERT [dbo].[User] OFF
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO
/****** Object:  StoredProcedure [dbo].[SpGetMasterBook]    Script Date: 02/04/2020 11:52:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SpGetMasterBook] @bookId int = 0 
AS
BEGIN
	SELECT A.*,B.CategoryName 
	FROM MasterBook A
	JOIN MasterCategoryBook B ON A.CategoryId = B.CategoryId
	WHERE ((A.BookId = @bookId AND @bookId != 0) OR @bookId = 0) 
END
GO
/****** Object:  StoredProcedure [dbo].[SpGetTransactionDetail]    Script Date: 02/04/2020 11:52:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SpGetTransactionDetail] @roleid int = 1,@borrower NVARCHAR(200) = ''
AS
BEGIN
	SELECT 
		 A.TransactionId
		,COALESCE(B.BookTitle,'') BookTitle
		,COALESCE(B.Author,'') Author
		,COALESCE(D.CategoryName,'') CategoryName
		--,COALESCE(C.StatusName,'') StatusName
		,COALESCE(A.Price,0) Price
		,COALESCE(A.Days,0) Days
		,COALESCE(A.Total,0) Total
		,COALESCE(A.CreatedBy,'') CreatedBy
		,COALESCE(U.FULLNAME,'') FullName
	FROM BookTransaction A
	LEFT JOIN MasterBook B ON A.BookId = B.BookId
	LEFT JOIN MasterStatus C ON A.StatusId = C.StatusId
	LEFT JOIN MasterCategoryBook D ON B.CategoryId = D.CategoryId
	LEFT JOIN [User] U ON A.CreatedBy = U.UserName
	WHERE ((A.CreatedBy = @borrower AND @roleid = 2) OR (@roleid = 1))
END
GO
/****** Object:  StoredProcedure [dbo].[SpGetTransactiondETAILByID]    Script Date: 02/04/2020 11:52:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SpGetTransactiondETAILByID] @TransactionId int
AS
BEGIN
	SELECT 
		 A.TransactionId
		,B.BookTitle
		,B.BookId
		,B.Author
		,D.CategoryName
		,C.StatusName
		,A.Price
		,A.StartDate
		,A.EndDate
		,A.Days
		,A.Total
		,A.CreatedBy
	FROM BookTransaction A
	LEFT JOIN MasterBook B ON A.BookId = B.BookId
	LEFT JOIN MasterStatus C ON A.StatusId = C.StatusId
	LEFT JOIN MasterCategoryBook D ON B.CategoryId = D.CategoryId
	WHERE A.TransactionId = @TransactionId
END

GO
/****** Object:  StoredProcedure [dbo].[SpGetUserWithRole]    Script Date: 02/04/2020 11:52:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SpGetUserWithRole]
AS
BEGIN
	SELECT 
		U.*,
		R.RoleName
	FROM [User] U
	JOIN MasterRole R 
	ON U.ROLEID = R.RoleId
END
GO
