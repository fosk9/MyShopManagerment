USE master
GO

-- Drop the database if it exists
IF DB_ID('AppleStoreHouse') IS NOT NULL
    BEGIN
        ALTER DATABASE AppleStoreHouse SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
        DROP DATABASE AppleStoreHouse;
    END
GO

-- Create the database
CREATE DATABASE AppleStoreHouse
GO
USE AppleStoreHouse
GO

-- Create Category table
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Create Product table
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](200) NULL,
	[Quantity] [int] NOT NULL,
	[Price] [decimal](10,2) NOT NULL,
	[Status] [bit] NOT NULL,
	[CategoryID] [int] NOT NULL,
	[Image] [nvarchar](4000) NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Create Role table
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](20) NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Create User table
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Email] [varchar](324) NOT NULL,
	[Name] [nvarchar](32) NOT NULL,
	[Password] [varchar](80) NOT NULL,
	[Address] [nvarchar](120) NULL,
	[Phone] [varchar](20) NULL,
	[Enabled] [bit] NOT NULL,
	[RoleID] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Insert Categories
SET IDENTITY_INSERT [dbo].[Category] ON 
GO
INSERT [dbo].[Category] ([ID], [Name]) VALUES (1, N'Based')
GO
INSERT [dbo].[Category] ([ID], [Name]) VALUES (2, N'Pro')
GO
INSERT [dbo].[Category] ([ID], [Name]) VALUES (3, N'Pro Max')
GO
INSERT [dbo].[Category] ([ID], [Name]) VALUES (4, N'Plus')
GO
INSERT [dbo].[Category] ([ID], [Name]) VALUES (5, N'SE')
GO
SET IDENTITY_INSERT [dbo].[Category] OFF
GO

-- Insert Products
SET IDENTITY_INSERT [dbo].[Product] ON 
GO
INSERT [dbo].[Product] ([ID], [Name], [Description], [Quantity], [Price], [Status], [CategoryID], [Image]) VALUES (1, N'iPhone 13', N'iPhone 13 128GB', 50, 699.99, 1, 1, N'https://24hstore.vn/images/products/2025/05/27/large/iphone-13-cu-98-128gb.jpg')
GO
INSERT [dbo].[Product] ([ID], [Name], [Description], [Quantity], [Price], [Status], [CategoryID], [Image]) VALUES (2, N'iPhone 14 Pro', N'iPhone 14 256GB', 30, 999.99, 1, 2, N'https://thangtaostore.com/watermark/product/540x540x2/upload/product/14prm-nen-7811.png')
GO
INSERT [dbo].[Product] ([ID], [Name], [Description], [Quantity], [Price], [Status], [CategoryID], [Image]) VALUES (3, N'iPhone 14 Pro Max', N'iPhone 14 Pro Max 512GB', 20, 1299.99, 1, 3, N'https://pos.nvncdn.com/a135ac-81120/ps/20231121_zPEcyW8KB6.jpeg')
GO
INSERT [dbo].[Product] ([ID], [Name], [Description], [Quantity], [Price], [Status], [CategoryID], [Image]) VALUES (4, N'iPhone 13 Plus', N'iPhone 13 Pro Max 128GB', 40, 799.99, 1, 4, N'https://thaiduongmobile.vn/uploads/source/san-pham/ip13/iphone13promax-usato-1.jpg')
GO
INSERT [dbo].[Product] ([ID], [Name], [Description], [Quantity], [Price], [Status], [CategoryID], [Image]) VALUES (5, N'iPhone SE (3rd Gen)', N'iPhone SE 64GB', 60, 429.99, 1, 5, N'https://i.ebayimg.com/images/g/-7MAAOSwQQBkzHRJ/s-l1200.jpg')
GO
SET IDENTITY_INSERT [dbo].[Product] OFF
GO

-- Insert Roles (excluding Customer, renaming Admin to Supervisor)
SET IDENTITY_INSERT [dbo].[Role] ON 
GO
INSERT [dbo].[Role] ([ID], [Name]) VALUES (1, N'Manager')
GO
INSERT [dbo].[Role] ([ID], [Name]) VALUES (2, N'Supervisor')
GO
SET IDENTITY_INSERT [dbo].[Role] OFF
GO

-- Insert Users (reassigning RoleID for users previously assigned to Customer)
INSERT [dbo].[User] ([Email], [Name], [Password], [Address], [Phone], [Enabled], [RoleID]) VALUES (N'admin@gmail.com', N'Admin', N'123456', N'Sai Gon', N'0943487783', 1, 2)
GO
INSERT [dbo].[User] ([Email], [Name], [Password], [Address], [Phone], [Enabled], [RoleID]) VALUES (N'manager@gmail.com', N'Manager', N'123456', N'Sai Gon', N'0934986423', 1, 1)
GO

-- Add constraints
ALTER TABLE [dbo].[Product] ADD CONSTRAINT [DF_Product_Quantity] DEFAULT ((0)) FOR [Quantity]
GO
ALTER TABLE [dbo].[Product] ADD CONSTRAINT [DF_Product_Price] DEFAULT ((1.00)) FOR [Price]
GO
ALTER TABLE [dbo].[Product] ADD CONSTRAINT [DF_Product_Status] DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[User] ADD CONSTRAINT [DF_Table_1_enabled] DEFAULT ((1)) FOR [Enabled]
GO
ALTER TABLE [dbo].[Product] WITH CHECK ADD CONSTRAINT [FK_Product_Category] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Category] ([ID])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category]
GO
ALTER TABLE [dbo].[User] WITH CHECK ADD CONSTRAINT [FK_User_Role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([ID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role]
GO

-- Set database to read-write
USE [master]
GO
ALTER DATABASE [AppleStoreHouse] SET READ_WRITE 
GO