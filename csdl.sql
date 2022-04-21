USE [master]
GO
/****** Object:  Database [QuanLyThueXe]    Script Date: 4/21/2022 4:46:04 PM ******/
CREATE DATABASE [QuanLyThueXe]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QuanLyThueXe', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\QuanLyThueXe.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'QuanLyThueXe_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\QuanLyThueXe_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [QuanLyThueXe] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QuanLyThueXe].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QuanLyThueXe] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QuanLyThueXe] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QuanLyThueXe] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QuanLyThueXe] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QuanLyThueXe] SET ARITHABORT OFF 
GO
ALTER DATABASE [QuanLyThueXe] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [QuanLyThueXe] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QuanLyThueXe] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QuanLyThueXe] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QuanLyThueXe] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QuanLyThueXe] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QuanLyThueXe] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QuanLyThueXe] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QuanLyThueXe] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QuanLyThueXe] SET  ENABLE_BROKER 
GO
ALTER DATABASE [QuanLyThueXe] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QuanLyThueXe] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QuanLyThueXe] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QuanLyThueXe] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QuanLyThueXe] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QuanLyThueXe] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QuanLyThueXe] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QuanLyThueXe] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [QuanLyThueXe] SET  MULTI_USER 
GO
ALTER DATABASE [QuanLyThueXe] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QuanLyThueXe] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QuanLyThueXe] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QuanLyThueXe] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [QuanLyThueXe] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [QuanLyThueXe] SET QUERY_STORE = OFF
GO
USE [QuanLyThueXe]
GO
/****** Object:  Table [dbo].[rental]    Script Date: 4/21/2022 4:46:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[rental](
	[rental_id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[vehicle_id] [int] NOT NULL,
	[voucher_id] [int] NULL,
	[date_rental] [varchar](255) NULL,
	[number_day] [int] NULL,
	[amount] [int] NULL,
	[status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[rental_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[role]    Script Date: 4/21/2022 4:46:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[role](
	[role_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](250) NULL,
PRIMARY KEY CLUSTERED 
(
	[role_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users]    Script Date: 4/21/2022 4:46:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[role_id] [int] NOT NULL,
	[fullname] [nvarchar](250) NULL,
	[email] [nvarchar](250) NULL,
	[phonenumber] [varchar](255) NULL,
	[password] [nvarchar](250) NULL,
PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vehicle]    Script Date: 4/21/2022 4:46:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vehicle](
	[vehicle_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NULL,
	[image] [nvarchar](500) NULL,
	[type_vehicle] [nvarchar](255) NULL,
	[price] [int] NULL,
	[description] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[vehicle_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[voucher]    Script Date: 4/21/2022 4:46:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[voucher](
	[voucher_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NULL,
	[discount] [int] NULL,
	[status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[voucher_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[role] ON 

INSERT [dbo].[role] ([role_id], [name]) VALUES (1, N'Người dùng')
INSERT [dbo].[role] ([role_id], [name]) VALUES (2, N'Quản trị')
SET IDENTITY_INSERT [dbo].[role] OFF
GO
SET IDENTITY_INSERT [dbo].[users] ON 

INSERT [dbo].[users] ([user_id], [role_id], [fullname], [email], [phonenumber], [password]) VALUES (1, 2, N'Quản trị viên', N'admin@gmail.com', N'0394738573', N'admin')
INSERT [dbo].[users] ([user_id], [role_id], [fullname], [email], [phonenumber], [password]) VALUES (2, 1, N'Lê Văn C', N'levanc@gmail.com', N'0324242342', N'levanc')
SET IDENTITY_INSERT [dbo].[users] OFF
GO
SET IDENTITY_INSERT [dbo].[vehicle] ON 

INSERT [dbo].[vehicle] ([vehicle_id], [name], [image], [type_vehicle], [price], [description]) VALUES (1, N'Xe Sirius', N'sii.jpg', N'Xe máy', 100000, N'Xe chạy rất tốt, trạng thái ổn định')
INSERT [dbo].[vehicle] ([vehicle_id], [name], [image], [type_vehicle], [price], [description]) VALUES (2, N'Xe Sedan 4 chỗ', N'sedan.jpg', N'Xê ô tô', 200000, N'Xe còn mới, chạy mượt')
SET IDENTITY_INSERT [dbo].[vehicle] OFF
GO
SET IDENTITY_INSERT [dbo].[voucher] ON 

INSERT [dbo].[voucher] ([voucher_id], [name], [discount], [status]) VALUES (1, N'TESTT', 10, 0)
INSERT [dbo].[voucher] ([voucher_id], [name], [discount], [status]) VALUES (2, N'TEST', 20, 1)
SET IDENTITY_INSERT [dbo].[voucher] OFF
GO
ALTER TABLE [dbo].[rental]  WITH CHECK ADD  CONSTRAINT [fk_user] FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([user_id])
GO
ALTER TABLE [dbo].[rental] CHECK CONSTRAINT [fk_user]
GO
ALTER TABLE [dbo].[rental]  WITH CHECK ADD  CONSTRAINT [fk_vehicle] FOREIGN KEY([vehicle_id])
REFERENCES [dbo].[vehicle] ([vehicle_id])
GO
ALTER TABLE [dbo].[rental] CHECK CONSTRAINT [fk_vehicle]
GO
ALTER TABLE [dbo].[rental]  WITH CHECK ADD  CONSTRAINT [fk_voucher] FOREIGN KEY([voucher_id])
REFERENCES [dbo].[voucher] ([voucher_id])
GO
ALTER TABLE [dbo].[rental] CHECK CONSTRAINT [fk_voucher]
GO
ALTER TABLE [dbo].[users]  WITH CHECK ADD  CONSTRAINT [fk_role] FOREIGN KEY([role_id])
REFERENCES [dbo].[role] ([role_id])
GO
ALTER TABLE [dbo].[users] CHECK CONSTRAINT [fk_role]
GO
USE [master]
GO
ALTER DATABASE [QuanLyThueXe] SET  READ_WRITE 
GO
