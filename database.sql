USE [master]
GO
CREATE DATABASE [QuanLyThueXe]
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
/****** Object:  Table [dbo].[rental]    Script Date: 4/28/2022 12:04:12 AM ******/
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
	[number_vehicle] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[rental_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[role]    Script Date: 4/28/2022 12:04:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[role](
	[role_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[role_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users]    Script Date: 4/28/2022 12:04:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[role_id] [int] NOT NULL,
	[fullname] [nvarchar](255) NULL,
	[email] [nvarchar](255) NULL,
	[phonenumber] [nvarchar](255) NULL,
	[password] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vehicle]    Script Date: 4/28/2022 12:04:12 AM ******/
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
	[quantity] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[vehicle_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[voucher]    Script Date: 4/28/2022 12:04:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[voucher](
	[voucher_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NULL,
	[discount] [int] NULL,
	[status] [int] NULL,
	[quantity] [int] NULL,
	[date_expire] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[voucher_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[rental] ON 

INSERT [dbo].[rental] ([rental_id], [user_id], [vehicle_id], [voucher_id], [date_rental], [number_day], [amount], [status], [number_vehicle]) VALUES (3, 2, 6, NULL, N'2022-04-25', 1, 100000000, 2, NULL)
INSERT [dbo].[rental] ([rental_id], [user_id], [vehicle_id], [voucher_id], [date_rental], [number_day], [amount], [status], [number_vehicle]) VALUES (4, 2, 6, NULL, N'2022-04-30', 12, 100000000, 2, NULL)
INSERT [dbo].[rental] ([rental_id], [user_id], [vehicle_id], [voucher_id], [date_rental], [number_day], [amount], [status], [number_vehicle]) VALUES (5, 2, 6, 1, N'2022-04-25', 2, 180000000, 2, NULL)
INSERT [dbo].[rental] ([rental_id], [user_id], [vehicle_id], [voucher_id], [date_rental], [number_day], [amount], [status], [number_vehicle]) VALUES (6, 2, 1, 2, N'2022-04-27', 2, 4800000, 3, NULL)
INSERT [dbo].[rental] ([rental_id], [user_id], [vehicle_id], [voucher_id], [date_rental], [number_day], [amount], [status], [number_vehicle]) VALUES (7, 2, 1, 1, N'2022-04-28', 2, 16200000, 3, 3)
INSERT [dbo].[rental] ([rental_id], [user_id], [vehicle_id], [voucher_id], [date_rental], [number_day], [amount], [status], [number_vehicle]) VALUES (8, 2, 1, NULL, N'2022-04-30', 2, 18000000, 2, 3)
SET IDENTITY_INSERT [dbo].[rental] OFF
GO
SET IDENTITY_INSERT [dbo].[role] ON 

INSERT [dbo].[role] ([role_id], [name]) VALUES (1, N'Admin')
INSERT [dbo].[role] ([role_id], [name]) VALUES (2, N'Người dùng')
SET IDENTITY_INSERT [dbo].[role] OFF
GO
SET IDENTITY_INSERT [dbo].[users] ON 

INSERT [dbo].[users] ([user_id], [role_id], [fullname], [email], [phonenumber], [password]) VALUES (2, 2, N'Minh Nha', N'minhnha2308@gmail.com', N'01223415449', N'25f9e794323b453885f5181f1b624db')
INSERT [dbo].[users] ([user_id], [role_id], [fullname], [email], [phonenumber], [password]) VALUES (3, 1, N'Quản trị', N'admin@gmail.com', N'0394073758', N'25f9e794323b453885f5181f1b624db')
SET IDENTITY_INSERT [dbo].[users] OFF
GO
SET IDENTITY_INSERT [dbo].[vehicle] ON 

INSERT [dbo].[vehicle] ([vehicle_id], [name], [image], [type_vehicle], [price], [description], [quantity]) VALUES (1, N'Exceter V1', N'637866931704703937Bf4EFMSz9Q70ZZj3BVm1.png', N'Xe máy', 3000000, N'<p>đẹp a</p>
', 20)
INSERT [dbo].[vehicle] ([vehicle_id], [name], [image], [type_vehicle], [price], [description], [quantity]) VALUES (3, N'Exceter', N'anh1.jpg', N'Xe máy', 3000000, N'đẹp', 11)
INSERT [dbo].[vehicle] ([vehicle_id], [name], [image], [type_vehicle], [price], [description], [quantity]) VALUES (4, N'Vinfat', N'oto.jpg', N'Xe ô tô', 100000000, N'Đẹp', 12)
INSERT [dbo].[vehicle] ([vehicle_id], [name], [image], [type_vehicle], [price], [description], [quantity]) VALUES (5, N'Exceter', N'anh1.jpg', N'Xe máy', 3000000, N'đẹp', 12)
INSERT [dbo].[vehicle] ([vehicle_id], [name], [image], [type_vehicle], [price], [description], [quantity]) VALUES (6, N'Vinfat1', N'oto.jpg', N'Xe ô tô', 100000000, N'Đẹp', 12)
INSERT [dbo].[vehicle] ([vehicle_id], [name], [image], [type_vehicle], [price], [description], [quantity]) VALUES (7, N'Vinfat', N'oto.jpg', N'Xe ô tô', 100000000, N'Đẹp', 13)
INSERT [dbo].[vehicle] ([vehicle_id], [name], [image], [type_vehicle], [price], [description], [quantity]) VALUES (8, N'Vinfat', N'oto.jpg', N'Xe ô tô', 100000000, N'Đẹp', 14)
INSERT [dbo].[vehicle] ([vehicle_id], [name], [image], [type_vehicle], [price], [description], [quantity]) VALUES (9, N'Exceter', N'anh1.jpg', N'Xe máy', 3000000, N'đẹp', 13)
INSERT [dbo].[vehicle] ([vehicle_id], [name], [image], [type_vehicle], [price], [description], [quantity]) VALUES (10, N'Exceter', N'anh1.jpg', N'Xe máy', 3000000, N'đẹp', 14)
INSERT [dbo].[vehicle] ([vehicle_id], [name], [image], [type_vehicle], [price], [description], [quantity]) VALUES (11, N'Vision', N'637866913968943317Bf4EFMSz9Q70ZZj3BVm1.png', N'Xe máy', 500000, N'<p>Th&ocirc;ng số kĩ thuật</p>

<p>Khối lượng bản th&acirc;n</p>

<p>Phi&ecirc;n bản Ti&ecirc;u chuẩn: 96kg<br />
Phi&ecirc;n bản Đặc biệt v&agrave; Cao cấp: 97kg</p>

<p>D&agrave;i x Rộng x Cao</p>

<p>1.871mm x 686mm x 1.101mm</p>

<p>Khoảng c&aacute;ch trục b&aacute;nh xe</p>

<p>1.255mm</p>

<p>Độ cao y&ecirc;n</p>

<p>761mm</p>

<p>Khoảng s&aacute;ng gầm xe</p>

<p>120mm</p>

<p>Dung t&iacute;ch b&igrave;nh xăng</p>

<p>4,9 l&iacute;t</p>

<p>K&iacute;ch cỡ lớp trước/ sau</p>

<p>Trước: 80/90-14M/C 40P<br />
Sau: 90/90-14M/C 46P</p>

<p>Phuộc trước</p>

<p>Ống lồng, giảm chấn thủy lực</p>

<p>Phuộc sau</p>

<p>L&ograve; xo trụ đơn, giảm chấn thủy lực</p>

<p>Loại động cơ</p>

<p>Xăng, 4 kỳ, 1 xi-lanh, l&agrave;m m&aacute;t bằng kh&ocirc;ng kh&iacute;</p>

<p>C&ocirc;ng suất tối đa</p>

<p>6,59kW/7.500 v&ograve;ng/ph&uacute;t</p>

<p>Dung t&iacute;ch nhớt m&aacute;y</p>

<p>0,65 l&iacute;t khi thay dầu<br />
0,8 l&iacute;t khi r&atilde; m&aacute;y&quot;</p>

<p>Mức ti&ecirc;u thụ nhi&ecirc;n liệu</p>

<p>1,88 (L/100km)</p>

<p>Loại truyền động</p>

<p>Đai</p>

<p>Hệ thống khởi động</p>

<p>Điện</p>

<p>Moment cực đại</p>

<p>9,29Nm/6.000 v&ograve;ng/ph&uacute;t</p>

<p>Dung t&iacute;ch xy-lanh</p>

<p>109,5cm3</p>

<p>Đường k&iacute;nh x H&agrave;nh tr&igrave;nh p&iacute;t t&ocirc;ng</p>

<p>47,0mm x 63,1mm</p>

<p>Tỷ số n&eacute;n</p>

<p>10,0:1</p>
', 20)
SET IDENTITY_INSERT [dbo].[vehicle] OFF
GO
SET IDENTITY_INSERT [dbo].[voucher] ON 

INSERT [dbo].[voucher] ([voucher_id], [name], [discount], [status], [quantity], [date_expire]) VALUES (1, N'D101', 10, 1, 8, N'2022-04-30')
INSERT [dbo].[voucher] ([voucher_id], [name], [discount], [status], [quantity], [date_expire]) VALUES (2, N'TESTA', 20, 1, 19, N'2022-04-30')
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
/*** Store Procedure ***/
CREATE PROCEDURE SelectAllUsers
AS
SELECT * FROM users
GO;
CREATE PROCEDURE SelectAllRoles
AS
SELECT * FROM role
GO;
CREATE PROCEDURE SelectAllVouchers
AS
SELECT * FROM voucher
GO;
CREATE PROCEDURE SelectAllVehicles
AS
SELECT * FROM vehicle
GO;
CREATE PROCEDURE SelectAllRentals
AS
SELECT * FROM rental
GO;
/*** Trigger ***/
CREATE TRIGGER trigger_vehicle
ON vehicle
AFTER INSERT, DELETE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO vehicle_backup(
        vehicle_id,
        name,
        image,
        type_vehicle,
        price,
        description,
        quantity
    )
    SELECT
        i.vehicle_id,
        i.name,
        i.image,
        i.type_vehicle,
        i.price,
        i.description,
        i.quantity
    FROM
        inserted i
    UNION ALL
    SELECT
        d.vehicle_id,
        d.name,
        d.image,
        d.type_vehicle,
        d.price,
        d.description,
        d.quantity
    FROM
        deleted d;
END
/*** View ***/
CREATE VIEW GetAllUser AS
SELECT	*
FROM	users
GO;
CREATE VIEW GetAllRole AS
SELECT	*
FROM	role
GO;
CREATE VIEW GetAllVehicle AS
SELECT	*
FROM	vehicle
GO;
CREATE VIEW GetAllVoucher AS
SELECT	*
FROM	voucher
GO;
CREATE VIEW GetAllRental AS
SELECT	*
FROM	rental
GO;