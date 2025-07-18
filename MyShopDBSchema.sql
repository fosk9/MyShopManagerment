CREATE DATABASE [MyShop]
GO
ALTER DATABASE [MyShop] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MyShop] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MyShop] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MyShop] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MyShop] SET ARITHABORT OFF 
GO
ALTER DATABASE [MyShop] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MyShop] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MyShop] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MyShop] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MyShop] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MyShop] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MyShop] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MyShop] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MyShop] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MyShop] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MyShop] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MyShop] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MyShop] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MyShop] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MyShop] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MyShop] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MyShop] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MyShop] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MyShop] SET  MULTI_USER 
GO
ALTER DATABASE [MyShop] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MyShop] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MyShop] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MyShop] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MyShop] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MyShop] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [MyShop] SET QUERY_STORE = OFF
GO
USE [MyShop]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 2/21/2024 8:50:42 AM ******/
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
/****** Object:  Table [dbo].[Product]    Script Date: 2/21/2024 8:50:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](200) NULL,
	[Quantity] [int] NOT NULL,
	[Price] [float] NOT NULL,
	[Status] [bit] NOT NULL,
	[CategoryID] [int] NOT NULL,
	[Image] [nvarchar](4000) NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 2/21/2024 8:50:42 AM ******/
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
/****** Object:  Table [dbo].[User]    Script Date: 2/21/2024 8:50:42 AM ******/
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
SET IDENTITY_INSERT [dbo].[Category] ON 
GO
INSERT [dbo].[Category] ([ID], [Name]) VALUES (1, N'Laptop Gaming')
GO
INSERT [dbo].[Category] ([ID], [Name]) VALUES (2, N'Ultrabooks')
GO
INSERT [dbo].[Category] ([ID], [Name]) VALUES (3, N'Chromebooks')
GO
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 
GO
INSERT [dbo].[Product] ([ID], [Name], [Description], [Quantity], [Price], [Status], [CategoryID], [Image]) VALUES (1, N'Laptop Asus TUF GAMING F15 FX506HF-HN014W', N'Super Stars.', 100, 750, 1, 1, N'https://encrypted-tbn3.gstatic.com/shopping?q=tbn:ANd9GcTj-3Wwoz2gZ0NQgBVx-v0CMzSWZ_zgjoN0IP4arQ36X-yMLNssMbmFKSc3HBN0jM9vKjn85rIJtsGrp_sdixr0WlyNx2oBsX9JPNLrRk9YDMedCI6265YkPveSBdBg1iKHfy4-KmWPYe4&usqp=CAc')
GO
INSERT [dbo].[Product] ([ID], [Name], [Description], [Quantity], [Price], [Status], [CategoryID], [Image]) VALUES (3, N'Laptop Gaming Acer Nitro 5 Eagle AN515-57-54MV', N'So fast, so serious', 100, 800, 1, 1, N'https://encrypted-tbn3.gstatic.com/shopping?q=tbn:ANd9GcSTD_wYu9l9iKU-wKCR2Wlg69X26WNfcDE1xFrdHW-mbcko0OGTulm2St09xdz5BWCxXB2-dbo1qKvLtOlpNrot6VBWHYRgbVmkdORgjze51_XenVlRL0iR2Am_-9BV4SvouDvQVaokwQ&usqp=CAc')
GO
INSERT [dbo].[Product] ([ID], [Name], [Description], [Quantity], [Price], [Status], [CategoryID], [Image]) VALUES (6, N'MacBook Air M1 Mini', N'Light and Bright. Laptop Apple MacBook Air 13 inch M1 2020 8-core CPU/8GB/256GB/7-core GPU (MGND3SA/A)', 99, 776, 1, 2, N'https://vietbis.vn/Image/Picture/Laptop/macbook-air-M1.jpg')
GO
INSERT [dbo].[Product] ([ID], [Name], [Description], [Quantity], [Price], [Status], [CategoryID], [Image]) VALUES (8, N'Lenovo Chromebook IdeaPad Duet 5', N'Go ultraportable with the Lenovo Duet 5 2 in 1 Chromebook with detachable keyboard .', 100, 400, 1, 3, N'https://cdn.mos.cms.futurecdn.net/rP5rovvVi9wsceYpf6HRSd.jpg')
GO
INSERT [dbo].[Product] ([ID], [Name], [Description], [Quantity], [Price], [Status], [CategoryID], [Image]) VALUES (9, N'Laptop Lenovo Thinkpad P52', N'Core i7-8850H, Ram 32GB, SSD 512GB, 15.6 Inch FHD, Nvidia Quadro P3200', 40, 600, 1, 1, N'https://hungthinhhitech.vn/uploads/product/LAPTOP/LENOVO/laptop-lenovo-thinkpad-p52-core-i7-8850h-1.jpg')
GO
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 
GO
INSERT [dbo].[Role] ([ID], [Name]) VALUES (1, N'Customer')
GO
INSERT [dbo].[Role] ([ID], [Name]) VALUES (2, N'Manager')
GO
INSERT [dbo].[Role] ([ID], [Name]) VALUES (3, N'Admin')
GO
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
INSERT [dbo].[User] ([Email], [Name], [Password], [Address], [Phone], [Enabled], [RoleID]) VALUES (N'admin@gmail.com', N'Admin', N'123456', N'Sai Gon', N'0943487783', 1, 3)
GO
INSERT [dbo].[User] ([Email], [Name], [Password], [Address], [Phone], [Enabled], [RoleID]) VALUES (N'duyduc@gmail.com', N'Duy Duc', N'123456', N'Sai Gon', N'0934968395', 1, 1)
GO
INSERT [dbo].[User] ([Email], [Name], [Password], [Address], [Phone], [Enabled], [RoleID]) VALUES (N'manager@gmail.com', N'Manager', N'123456', N'Sai Gon', N'0934986423', 1, 2)
GO
INSERT [dbo].[User] ([Email], [Name], [Password], [Address], [Phone], [Enabled], [RoleID]) VALUES (N'nguoiewm@gmail.com', N'Thanh thanh', N'3123131', N'', N'0934968395', 1, 2)
GO
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_Quantity]  DEFAULT ((0)) FOR [Quantity]
GO
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_Price]  DEFAULT ((1)) FOR [Price]
GO
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_Table_1_enabled]  DEFAULT ((1)) FOR [Enabled]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Category] ([ID])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([ID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role]
GO
USE [master]
GO
ALTER DATABASE [MyShop] SET  READ_WRITE 
GO
