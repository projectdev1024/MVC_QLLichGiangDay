USE [master]
GO
/****** Object:  Database [TeachingSchedule]    Script Date: 2018-05-16 16:52:14 ******/
CREATE DATABASE [TeachingSchedule]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TeachingSchedule', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\TeachingSchedule.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'TeachingSchedule_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\TeachingSchedule_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [TeachingSchedule] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TeachingSchedule].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TeachingSchedule] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TeachingSchedule] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TeachingSchedule] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TeachingSchedule] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TeachingSchedule] SET ARITHABORT OFF 
GO
ALTER DATABASE [TeachingSchedule] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TeachingSchedule] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TeachingSchedule] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TeachingSchedule] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TeachingSchedule] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TeachingSchedule] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TeachingSchedule] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TeachingSchedule] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TeachingSchedule] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TeachingSchedule] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TeachingSchedule] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TeachingSchedule] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TeachingSchedule] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TeachingSchedule] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TeachingSchedule] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TeachingSchedule] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TeachingSchedule] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TeachingSchedule] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [TeachingSchedule] SET  MULTI_USER 
GO
ALTER DATABASE [TeachingSchedule] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TeachingSchedule] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TeachingSchedule] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TeachingSchedule] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [TeachingSchedule] SET DELAYED_DURABILITY = DISABLED 
GO
USE [TeachingSchedule]
GO
/****** Object:  Table [dbo].[BoMon]    Script Date: 2018-05-16 16:52:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BoMon](
	[MaBoMon] [int] IDENTITY(1,1) NOT NULL,
	[TenBoMon] [nvarchar](50) NULL,
	[Active] [bit] NULL,
 CONSTRAINT [PK_BoMon] PRIMARY KEY CLUSTERED 
(
	[MaBoMon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ChucVu]    Script Date: 2018-05-16 16:52:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChucVu](
	[MaCV] [int] IDENTITY(1,1) NOT NULL,
	[TenChucVu] [nvarchar](50) NULL,
	[Active] [bit] NOT NULL,
	[HeSo] [int] NULL,
 CONSTRAINT [PK_MaCV] PRIMARY KEY CLUSTERED 
(
	[MaCV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CT_DMG]    Script Date: 2018-05-16 16:52:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CT_DMG](
	[IDCT_DMG] [int] IDENTITY(1,1) NOT NULL,
	[MaDMG] [int] NULL,
	[MaCV] [int] NULL,
 CONSTRAINT [PK_CT_DMG] PRIMARY KEY CLUSTERED 
(
	[IDCT_DMG] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DMG]    Script Date: 2018-05-16 16:52:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DMG](
	[MaDMG] [int] IDENTITY(1,1) NOT NULL,
	[MaGV] [int] NULL,
	[IDNamHoc] [int] NULL,
	[TongHeSo] [int] NULL,
	[MaHocHam] [int] NULL,
	[TongDMG] [int] NULL,
	[Active] [bit] NULL,
 CONSTRAINT [PK_DMG] PRIMARY KEY CLUSTERED 
(
	[MaDMG] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DNDoiGio]    Script Date: 2018-05-16 16:52:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DNDoiGio](
	[MaDN] [int] IDENTITY(1,1) NOT NULL,
	[MaLichGD] [int] NULL,
	[MaLichGD2] [int] NULL,
	[Status] [nvarchar](50) NULL,
	[NgayTao] [datetime] NULL,
 CONSTRAINT [PK_DNDoiGio] PRIMARY KEY CLUSTERED 
(
	[MaDN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DNDoiGV]    Script Date: 2018-05-16 16:52:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DNDoiGV](
	[MaDN] [int] IDENTITY(1,1) NOT NULL,
	[MaPCGD] [int] NULL,
	[NgayTao] [datetime] NULL,
	[MaGV] [int] NULL,
	[Status] [nvarchar](50) NULL,
	[NgayBD] [date] NULL,
	[NgayKT] [date] NULL,
 CONSTRAINT [PK_DNDoiGV] PRIMARY KEY CLUSTERED 
(
	[MaDN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GV]    Script Date: 2018-05-16 16:52:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GV](
	[MaGV] [int] IDENTITY(1,1) NOT NULL,
	[HoTen] [nvarchar](50) NULL,
	[GioiTinh] [bit] NULL,
	[Avatar] [nvarchar](500) NULL,
	[DiaChi] [nvarchar](50) NULL,
	[SDT] [nvarchar](50) NULL,
	[TenDangNhap] [nvarchar](50) NULL,
	[MatKhau] [nvarchar](50) NULL,
	[QuyenHan] [nvarchar](50) NULL,
	[Active] [bit] NULL,
	[MaBoMon] [int] NULL,
 CONSTRAINT [PK_GV_1] PRIMARY KEY CLUSTERED 
(
	[MaGV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HocHam]    Script Date: 2018-05-16 16:52:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HocHam](
	[MaHocHam] [int] IDENTITY(1,1) NOT NULL,
	[TenHocHam] [nvarchar](50) NULL,
	[Active] [bit] NULL,
	[HeSo] [float] NULL,
	[DMG] [int] NULL,
 CONSTRAINT [PK_HocHam] PRIMARY KEY CLUSTERED 
(
	[MaHocHam] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LichGD]    Script Date: 2018-05-16 16:52:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LichGD](
	[MaLichGD] [int] IDENTITY(1,1) NOT NULL,
	[MaPCGD] [int] NULL,
	[NgayBD] [datetime] NULL,
	[NgayKT] [datetime] NULL,
	[Thu] [int] NULL,
	[Tiet] [nvarchar](50) NULL,
	[Status] [nvarchar](50) NULL,
 CONSTRAINT [PK_LichGD] PRIMARY KEY CLUSTERED 
(
	[MaLichGD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LopHP]    Script Date: 2018-05-16 16:52:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LopHP](
	[MaHP] [int] IDENTITY(1,1) NOT NULL,
	[IDNamHoc] [int] NULL,
	[SiSo] [int] NULL,
	[LHDT] [nvarchar](50) NULL,
	[DiaDiem] [nvarchar](50) NULL,
	[Kip] [nvarchar](50) NULL,
	[SoTietTKB] [int] NULL,
	[TenLop] [nvarchar](50) NULL,
	[TGTHIKT] [date] NULL,
	[HinhThucThi] [nvarchar](50) NULL,
	[MaMH] [int] NULL,
	[Active] [bit] NULL,
	[HeSoKip] [float] NULL,
	[HeSoLHDT] [float] NULL,
	[HeSoQS] [float] NULL,
	[HeSoDD] [float] NULL,
	[TongHeSo] [float] NULL,
	[SoTietQuyChuan] [int] NULL,
 CONSTRAINT [PK_LopHP] PRIMARY KEY CLUSTERED 
(
	[MaHP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MonHoc]    Script Date: 2018-05-16 16:52:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MonHoc](
	[MaMH] [int] IDENTITY(1,1) NOT NULL,
	[TenMonHoc] [nvarchar](50) NULL,
	[MaBoMon] [int] NULL,
	[Active] [bit] NULL,
 CONSTRAINT [PK_MonHoc] PRIMARY KEY CLUSTERED 
(
	[MaMH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NamHoc]    Script Date: 2018-05-16 16:52:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NamHoc](
	[IDNamHoc] [int] IDENTITY(1,1) NOT NULL,
	[KyHoc] [nvarchar](50) NULL,
	[NamHoc] [nvarchar](50) NULL,
	[Active] [bit] NULL,
	[BatDau] [date] NULL,
	[KetThuc] [date] NULL,
	[TrangThai] [nvarchar](50) NULL,
 CONSTRAINT [PK_NamHoc] PRIMARY KEY CLUSTERED 
(
	[IDNamHoc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NCKH]    Script Date: 2018-05-16 16:52:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NCKH](
	[MaNCKH] [int] IDENTITY(1,1) NOT NULL,
	[MaGV] [int] NULL,
	[HangMuc] [nvarchar](50) NULL,
	[NoiDung] [nvarchar](max) NULL,
	[TaiLieu] [nvarchar](max) NULL,
	[ThoiGian] [nvarchar](50) NULL,
	[SoGioNC] [int] NULL,
 CONSTRAINT [PK_NCKH] PRIMARY KEY CLUSTERED 
(
	[MaNCKH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PCGD]    Script Date: 2018-05-16 16:52:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PCGD](
	[MaPCGD] [int] IDENTITY(1,1) NOT NULL,
	[MaGV] [int] NULL,
	[MaHP] [int] NULL,
	[Status] [nvarchar](50) NULL,
	[MaGV2] [int] NULL,
	[MaGV3] [int] NULL,
 CONSTRAINT [PK_PCGD] PRIMARY KEY CLUSTERED 
(
	[MaPCGD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[BoMon] ON 

INSERT [dbo].[BoMon] ([MaBoMon], [TenBoMon], [Active]) VALUES (3, N'Bộ môn A', 1)
INSERT [dbo].[BoMon] ([MaBoMon], [TenBoMon], [Active]) VALUES (4, N'Bộ môn B', 1)
INSERT [dbo].[BoMon] ([MaBoMon], [TenBoMon], [Active]) VALUES (5, N'Bộ môn C', 1)
SET IDENTITY_INSERT [dbo].[BoMon] OFF
SET IDENTITY_INSERT [dbo].[ChucVu] ON 

INSERT [dbo].[ChucVu] ([MaCV], [TenChucVu], [Active], [HeSo]) VALUES (1005, N'Admin', 1, 50)
INSERT [dbo].[ChucVu] ([MaCV], [TenChucVu], [Active], [HeSo]) VALUES (1006, N'Chủ nhiệm khoa', 1, 30)
INSERT [dbo].[ChucVu] ([MaCV], [TenChucVu], [Active], [HeSo]) VALUES (1007, N'Phó chủ nhiệm khoa', 1, 25)
INSERT [dbo].[ChucVu] ([MaCV], [TenChucVu], [Active], [HeSo]) VALUES (1008, N'Chủ nhiệm bộ môn', 1, 25)
INSERT [dbo].[ChucVu] ([MaCV], [TenChucVu], [Active], [HeSo]) VALUES (1009, N'Giáo viên đang học cao học không tập trung', 1, 40)
INSERT [dbo].[ChucVu] ([MaCV], [TenChucVu], [Active], [HeSo]) VALUES (1010, N'Cán bộ nữ có con nhỏ dưới 36 tháng tuổi', 1, 10)
SET IDENTITY_INSERT [dbo].[ChucVu] OFF
SET IDENTITY_INSERT [dbo].[CT_DMG] ON 

INSERT [dbo].[CT_DMG] ([IDCT_DMG], [MaDMG], [MaCV]) VALUES (5, 6, 1010)
INSERT [dbo].[CT_DMG] ([IDCT_DMG], [MaDMG], [MaCV]) VALUES (6, 6, 1008)
INSERT [dbo].[CT_DMG] ([IDCT_DMG], [MaDMG], [MaCV]) VALUES (7, 6, 1006)
INSERT [dbo].[CT_DMG] ([IDCT_DMG], [MaDMG], [MaCV]) VALUES (8, 6, 1005)
INSERT [dbo].[CT_DMG] ([IDCT_DMG], [MaDMG], [MaCV]) VALUES (10, 8, 1007)
INSERT [dbo].[CT_DMG] ([IDCT_DMG], [MaDMG], [MaCV]) VALUES (11, 8, 1008)
INSERT [dbo].[CT_DMG] ([IDCT_DMG], [MaDMG], [MaCV]) VALUES (13, 8, 1009)
INSERT [dbo].[CT_DMG] ([IDCT_DMG], [MaDMG], [MaCV]) VALUES (1018, 7, 1009)
INSERT [dbo].[CT_DMG] ([IDCT_DMG], [MaDMG], [MaCV]) VALUES (1020, 7, 1010)
SET IDENTITY_INSERT [dbo].[CT_DMG] OFF
SET IDENTITY_INSERT [dbo].[DMG] ON 

INSERT [dbo].[DMG] ([MaDMG], [MaGV], [IDNamHoc], [TongHeSo], [MaHocHam], [TongDMG], [Active]) VALUES (6, 1010, 2, 200, 1005, NULL, 1)
INSERT [dbo].[DMG] ([MaDMG], [MaGV], [IDNamHoc], [TongHeSo], [MaHocHam], [TongDMG], [Active]) VALUES (7, 1009, 2, 50, 1005, 100, NULL)
INSERT [dbo].[DMG] ([MaDMG], [MaGV], [IDNamHoc], [TongHeSo], [MaHocHam], [TongDMG], [Active]) VALUES (8, 1011, 5, 90, 1005, 200, NULL)
SET IDENTITY_INSERT [dbo].[DMG] OFF
SET IDENTITY_INSERT [dbo].[DNDoiGio] ON 

INSERT [dbo].[DNDoiGio] ([MaDN], [MaLichGD], [MaLichGD2], [Status], [NgayTao]) VALUES (4, 2, NULL, NULL, CAST(N'2018-03-29 23:58:18.090' AS DateTime))
INSERT [dbo].[DNDoiGio] ([MaDN], [MaLichGD], [MaLichGD2], [Status], [NgayTao]) VALUES (5, 5, 3, NULL, CAST(N'2018-03-30 00:07:02.177' AS DateTime))
INSERT [dbo].[DNDoiGio] ([MaDN], [MaLichGD], [MaLichGD2], [Status], [NgayTao]) VALUES (6, 7, 6, NULL, CAST(N'2018-03-30 00:11:12.867' AS DateTime))
SET IDENTITY_INSERT [dbo].[DNDoiGio] OFF
SET IDENTITY_INSERT [dbo].[DNDoiGV] ON 

INSERT [dbo].[DNDoiGV] ([MaDN], [MaPCGD], [NgayTao], [MaGV], [Status], [NgayBD], [NgayKT]) VALUES (1, 4, CAST(N'2018-03-30 00:46:49.757' AS DateTime), 1009, NULL, CAST(N'2018-03-03' AS Date), CAST(N'2018-03-11' AS Date))
SET IDENTITY_INSERT [dbo].[DNDoiGV] OFF
SET IDENTITY_INSERT [dbo].[GV] ON 

INSERT [dbo].[GV] ([MaGV], [HoTen], [GioiTinh], [Avatar], [DiaChi], [SDT], [TenDangNhap], [MatKhau], [QuyenHan], [Active], [MaBoMon]) VALUES (1009, N'Quản trị', 1, NULL, N'HN', N'0987654321', N'admin', N'admin', N'Admin', 1, 3)
INSERT [dbo].[GV] ([MaGV], [HoTen], [GioiTinh], [Avatar], [DiaChi], [SDT], [TenDangNhap], [MatKhau], [QuyenHan], [Active], [MaBoMon]) VALUES (1010, N'gv1', NULL, N'/Content/Upload/img/05274029032018_heart.png', N'gv1', N'gv1', N'gv1', N'admin', N'GiangVien', NULL, 3)
INSERT [dbo].[GV] ([MaGV], [HoTen], [GioiTinh], [Avatar], [DiaChi], [SDT], [TenDangNhap], [MatKhau], [QuyenHan], [Active], [MaBoMon]) VALUES (1011, N'demo', NULL, N'/Content/Upload/img/01504230032018_doctor.png', N'HN', N'123', N'demo', N'admin', N'GiangVien', NULL, 3)
SET IDENTITY_INSERT [dbo].[GV] OFF
SET IDENTITY_INSERT [dbo].[HocHam] ON 

INSERT [dbo].[HocHam] ([MaHocHam], [TenHocHam], [Active], [HeSo], [DMG]) VALUES (1005, N'GS', 1, 2, 200)
INSERT [dbo].[HocHam] ([MaHocHam], [TenHocHam], [Active], [HeSo], [DMG]) VALUES (1006, N'NCS', 1, NULL, 100)
SET IDENTITY_INSERT [dbo].[HocHam] OFF
SET IDENTITY_INSERT [dbo].[LichGD] ON 

INSERT [dbo].[LichGD] ([MaLichGD], [MaPCGD], [NgayBD], [NgayKT], [Thu], [Tiet], [Status]) VALUES (1, 4, CAST(N'2018-02-27 00:00:00.000' AS DateTime), CAST(N'2018-03-18 00:00:00.000' AS DateTime), 2, N'3', NULL)
INSERT [dbo].[LichGD] ([MaLichGD], [MaPCGD], [NgayBD], [NgayKT], [Thu], [Tiet], [Status]) VALUES (2, 4, CAST(N'2018-03-09 00:00:00.000' AS DateTime), CAST(N'2018-03-01 00:00:00.000' AS DateTime), 3, N'34', NULL)
INSERT [dbo].[LichGD] ([MaLichGD], [MaPCGD], [NgayBD], [NgayKT], [Thu], [Tiet], [Status]) VALUES (3, 4, CAST(N'2018-03-30 00:00:00.000' AS DateTime), CAST(N'2018-09-01 00:00:00.000' AS DateTime), 2, N'3', NULL)
INSERT [dbo].[LichGD] ([MaLichGD], [MaPCGD], [NgayBD], [NgayKT], [Thu], [Tiet], [Status]) VALUES (4, 4, CAST(N'2018-03-30 00:00:00.000' AS DateTime), CAST(N'2018-09-01 00:00:00.000' AS DateTime), 2, N'3', NULL)
INSERT [dbo].[LichGD] ([MaLichGD], [MaPCGD], [NgayBD], [NgayKT], [Thu], [Tiet], [Status]) VALUES (5, 5, CAST(N'2018-03-01 00:00:00.000' AS DateTime), CAST(N'2018-07-01 00:00:00.000' AS DateTime), 3, N'4', NULL)
INSERT [dbo].[LichGD] ([MaLichGD], [MaPCGD], [NgayBD], [NgayKT], [Thu], [Tiet], [Status]) VALUES (6, 5, CAST(N'2018-03-01 00:00:00.000' AS DateTime), CAST(N'2018-03-24 00:00:00.000' AS DateTime), 6, N'5', NULL)
INSERT [dbo].[LichGD] ([MaLichGD], [MaPCGD], [NgayBD], [NgayKT], [Thu], [Tiet], [Status]) VALUES (7, 4, CAST(N'2018-03-07 00:00:00.000' AS DateTime), CAST(N'2018-03-23 00:00:00.000' AS DateTime), 2, N'34', NULL)
INSERT [dbo].[LichGD] ([MaLichGD], [MaPCGD], [NgayBD], [NgayKT], [Thu], [Tiet], [Status]) VALUES (8, 7, CAST(N'2018-04-02 00:00:00.000' AS DateTime), CAST(N'2018-04-13 00:00:00.000' AS DateTime), 1, N'5', NULL)
INSERT [dbo].[LichGD] ([MaLichGD], [MaPCGD], [NgayBD], [NgayKT], [Thu], [Tiet], [Status]) VALUES (9, 7, CAST(N'2018-04-04 00:00:00.000' AS DateTime), CAST(N'2018-04-15 00:00:00.000' AS DateTime), 1, N'9', NULL)
INSERT [dbo].[LichGD] ([MaLichGD], [MaPCGD], [NgayBD], [NgayKT], [Thu], [Tiet], [Status]) VALUES (10, 12, CAST(N'2018-01-31 00:00:00.000' AS DateTime), CAST(N'2018-04-29 00:00:00.000' AS DateTime), 1, N'6', NULL)
SET IDENTITY_INSERT [dbo].[LichGD] OFF
SET IDENTITY_INSERT [dbo].[LopHP] ON 

INSERT [dbo].[LopHP] ([MaHP], [IDNamHoc], [SiSo], [LHDT], [DiaDiem], [Kip], [SoTietTKB], [TenLop], [TGTHIKT], [HinhThucThi], [MaMH], [Active], [HeSoKip], [HeSoLHDT], [HeSoQS], [HeSoDD], [TongHeSo], [SoTietQuyChuan]) VALUES (3, 3, 100, N'abc', N'HN', N'1', 30, N'abc20162017', CAST(N'2018-03-01' AS Date), N'abc', 1004, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[LopHP] ([MaHP], [IDNamHoc], [SiSo], [LHDT], [DiaDiem], [Kip], [SoTietTKB], [TenLop], [TGTHIKT], [HinhThucThi], [MaMH], [Active], [HeSoKip], [HeSoLHDT], [HeSoQS], [HeSoDD], [TongHeSo], [SoTietQuyChuan]) VALUES (4, 2, 100, N'1', N'hn', N'1', 123, N'123', CAST(N'2018-03-29' AS Date), N'123', 1004, NULL, 2, 3, 4, 5, NULL, NULL)
INSERT [dbo].[LopHP] ([MaHP], [IDNamHoc], [SiSo], [LHDT], [DiaDiem], [Kip], [SoTietTKB], [TenLop], [TGTHIKT], [HinhThucThi], [MaMH], [Active], [HeSoKip], [HeSoLHDT], [HeSoQS], [HeSoDD], [TongHeSo], [SoTietQuyChuan]) VALUES (5, 2, 100, N'1', N'11', N'1', 1, N'1', CAST(N'2018-03-01' AS Date), N'1', 1004, NULL, 1, 1, 1, 1, NULL, NULL)
INSERT [dbo].[LopHP] ([MaHP], [IDNamHoc], [SiSo], [LHDT], [DiaDiem], [Kip], [SoTietTKB], [TenLop], [TGTHIKT], [HinhThucThi], [MaMH], [Active], [HeSoKip], [HeSoLHDT], [HeSoQS], [HeSoDD], [TongHeSo], [SoTietQuyChuan]) VALUES (1005, 2, 23, N'23', N'23', N'23', 23, N'23', CAST(N'2018-04-04' AS Date), N'23', 1006, NULL, 23, 23, 23, 23, NULL, NULL)
SET IDENTITY_INSERT [dbo].[LopHP] OFF
SET IDENTITY_INSERT [dbo].[MonHoc] ON 

INSERT [dbo].[MonHoc] ([MaMH], [TenMonHoc], [MaBoMon], [Active]) VALUES (1004, N'Lập trình cơ bản', 3, 1)
INSERT [dbo].[MonHoc] ([MaMH], [TenMonHoc], [MaBoMon], [Active]) VALUES (1005, N'Phân tích và thiết kế HTTT', 4, 1)
INSERT [dbo].[MonHoc] ([MaMH], [TenMonHoc], [MaBoMon], [Active]) VALUES (1006, N'ĐA phân tích và TK HTTT', 5, 1)
INSERT [dbo].[MonHoc] ([MaMH], [TenMonHoc], [MaBoMon], [Active]) VALUES (1007, N'Tin học 1(Dạy tiếng Nga)', 3, NULL)
INSERT [dbo].[MonHoc] ([MaMH], [TenMonHoc], [MaBoMon], [Active]) VALUES (1008, N'ĐA nhập môn xử lý ảnh', 3, NULL)
SET IDENTITY_INSERT [dbo].[MonHoc] OFF
SET IDENTITY_INSERT [dbo].[NamHoc] ON 

INSERT [dbo].[NamHoc] ([IDNamHoc], [KyHoc], [NamHoc], [Active], [BatDau], [KetThuc], [TrangThai]) VALUES (2, N'Kỳ 2', N'Năm học 2017 - 2018', 1, CAST(N'2018-01-27' AS Date), CAST(N'2018-05-05' AS Date), N'INIT')
INSERT [dbo].[NamHoc] ([IDNamHoc], [KyHoc], [NamHoc], [Active], [BatDau], [KetThuc], [TrangThai]) VALUES (3, N'Kỳ 1', N'Năm học 2016 - 2017', 1, CAST(N'2018-02-01' AS Date), CAST(N'2018-05-05' AS Date), N'CLOSED')
INSERT [dbo].[NamHoc] ([IDNamHoc], [KyHoc], [NamHoc], [Active], [BatDau], [KetThuc], [TrangThai]) VALUES (4, N'Kỳ 2', N'Năm học 2016-2017', 1, CAST(N'2018-01-16' AS Date), CAST(N'2018-05-05' AS Date), N'CLOSED')
INSERT [dbo].[NamHoc] ([IDNamHoc], [KyHoc], [NamHoc], [Active], [BatDau], [KetThuc], [TrangThai]) VALUES (5, N'Kỳ 1', N'Năm học 2017- 2018', 1, CAST(N'2017-10-30' AS Date), CAST(N'2018-05-05' AS Date), N'CLOSED')
SET IDENTITY_INSERT [dbo].[NamHoc] OFF
SET IDENTITY_INSERT [dbo].[NCKH] ON 

INSERT [dbo].[NCKH] ([MaNCKH], [MaGV], [HangMuc], [NoiDung], [TaiLieu], [ThoiGian], [SoGioNC]) VALUES (1, 1010, N'Cấp trường', N'abc', N'abc', N'1/1/2017 - 4/4/2017', 500)
INSERT [dbo].[NCKH] ([MaNCKH], [MaGV], [HangMuc], [NoiDung], [TaiLieu], [ThoiGian], [SoGioNC]) VALUES (2, 1011, N'Cấp trường', NULL, NULL, N'3/3/1996 - 3/3-2000', 56432)
SET IDENTITY_INSERT [dbo].[NCKH] OFF
SET IDENTITY_INSERT [dbo].[PCGD] ON 

INSERT [dbo].[PCGD] ([MaPCGD], [MaGV], [MaHP], [Status], [MaGV2], [MaGV3]) VALUES (4, 1010, 3, NULL, NULL, NULL)
INSERT [dbo].[PCGD] ([MaPCGD], [MaGV], [MaHP], [Status], [MaGV2], [MaGV3]) VALUES (5, 1009, 3, NULL, 1010, 1010)
INSERT [dbo].[PCGD] ([MaPCGD], [MaGV], [MaHP], [Status], [MaGV2], [MaGV3]) VALUES (6, 1010, 4, NULL, 1010, 1010)
INSERT [dbo].[PCGD] ([MaPCGD], [MaGV], [MaHP], [Status], [MaGV2], [MaGV3]) VALUES (7, 1009, 5, NULL, NULL, NULL)
INSERT [dbo].[PCGD] ([MaPCGD], [MaGV], [MaHP], [Status], [MaGV2], [MaGV3]) VALUES (8, 1011, 4, NULL, NULL, NULL)
INSERT [dbo].[PCGD] ([MaPCGD], [MaGV], [MaHP], [Status], [MaGV2], [MaGV3]) VALUES (9, 1011, 4, NULL, NULL, NULL)
INSERT [dbo].[PCGD] ([MaPCGD], [MaGV], [MaHP], [Status], [MaGV2], [MaGV3]) VALUES (10, 1009, 4, NULL, NULL, NULL)
INSERT [dbo].[PCGD] ([MaPCGD], [MaGV], [MaHP], [Status], [MaGV2], [MaGV3]) VALUES (11, 1010, 4, NULL, NULL, NULL)
INSERT [dbo].[PCGD] ([MaPCGD], [MaGV], [MaHP], [Status], [MaGV2], [MaGV3]) VALUES (12, 1010, 1005, NULL, NULL, NULL)
INSERT [dbo].[PCGD] ([MaPCGD], [MaGV], [MaHP], [Status], [MaGV2], [MaGV3]) VALUES (13, 1009, 1005, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[PCGD] OFF
ALTER TABLE [dbo].[CT_DMG]  WITH CHECK ADD  CONSTRAINT [FK_CT_DMG_ChucVu] FOREIGN KEY([MaCV])
REFERENCES [dbo].[ChucVu] ([MaCV])
GO
ALTER TABLE [dbo].[CT_DMG] CHECK CONSTRAINT [FK_CT_DMG_ChucVu]
GO
ALTER TABLE [dbo].[CT_DMG]  WITH CHECK ADD  CONSTRAINT [FK_CT_DMG_DMG] FOREIGN KEY([MaDMG])
REFERENCES [dbo].[DMG] ([MaDMG])
GO
ALTER TABLE [dbo].[CT_DMG] CHECK CONSTRAINT [FK_CT_DMG_DMG]
GO
ALTER TABLE [dbo].[DMG]  WITH CHECK ADD  CONSTRAINT [FK_DMG_GV] FOREIGN KEY([MaGV])
REFERENCES [dbo].[GV] ([MaGV])
GO
ALTER TABLE [dbo].[DMG] CHECK CONSTRAINT [FK_DMG_GV]
GO
ALTER TABLE [dbo].[DMG]  WITH CHECK ADD  CONSTRAINT [FK_DMG_HocHam] FOREIGN KEY([MaHocHam])
REFERENCES [dbo].[HocHam] ([MaHocHam])
GO
ALTER TABLE [dbo].[DMG] CHECK CONSTRAINT [FK_DMG_HocHam]
GO
ALTER TABLE [dbo].[DMG]  WITH CHECK ADD  CONSTRAINT [FK_DMG_NamHoc] FOREIGN KEY([IDNamHoc])
REFERENCES [dbo].[NamHoc] ([IDNamHoc])
GO
ALTER TABLE [dbo].[DMG] CHECK CONSTRAINT [FK_DMG_NamHoc]
GO
ALTER TABLE [dbo].[DNDoiGio]  WITH CHECK ADD  CONSTRAINT [FK_DNDoiGio_LichGD] FOREIGN KEY([MaLichGD])
REFERENCES [dbo].[LichGD] ([MaLichGD])
GO
ALTER TABLE [dbo].[DNDoiGio] CHECK CONSTRAINT [FK_DNDoiGio_LichGD]
GO
ALTER TABLE [dbo].[DNDoiGV]  WITH CHECK ADD  CONSTRAINT [FK_DNDoiGV_GV] FOREIGN KEY([MaGV])
REFERENCES [dbo].[GV] ([MaGV])
GO
ALTER TABLE [dbo].[DNDoiGV] CHECK CONSTRAINT [FK_DNDoiGV_GV]
GO
ALTER TABLE [dbo].[DNDoiGV]  WITH CHECK ADD  CONSTRAINT [FK_DNDoiGV_PCGD] FOREIGN KEY([MaPCGD])
REFERENCES [dbo].[PCGD] ([MaPCGD])
GO
ALTER TABLE [dbo].[DNDoiGV] CHECK CONSTRAINT [FK_DNDoiGV_PCGD]
GO
ALTER TABLE [dbo].[GV]  WITH CHECK ADD  CONSTRAINT [FK_GV_BoMon] FOREIGN KEY([MaBoMon])
REFERENCES [dbo].[BoMon] ([MaBoMon])
GO
ALTER TABLE [dbo].[GV] CHECK CONSTRAINT [FK_GV_BoMon]
GO
ALTER TABLE [dbo].[LichGD]  WITH CHECK ADD  CONSTRAINT [FK_LichGD_PCGD] FOREIGN KEY([MaPCGD])
REFERENCES [dbo].[PCGD] ([MaPCGD])
GO
ALTER TABLE [dbo].[LichGD] CHECK CONSTRAINT [FK_LichGD_PCGD]
GO
ALTER TABLE [dbo].[LopHP]  WITH CHECK ADD  CONSTRAINT [FK_LopHP_MonHoc] FOREIGN KEY([MaMH])
REFERENCES [dbo].[MonHoc] ([MaMH])
GO
ALTER TABLE [dbo].[LopHP] CHECK CONSTRAINT [FK_LopHP_MonHoc]
GO
ALTER TABLE [dbo].[LopHP]  WITH CHECK ADD  CONSTRAINT [FK_LopHP_NamHoc] FOREIGN KEY([IDNamHoc])
REFERENCES [dbo].[NamHoc] ([IDNamHoc])
GO
ALTER TABLE [dbo].[LopHP] CHECK CONSTRAINT [FK_LopHP_NamHoc]
GO
ALTER TABLE [dbo].[MonHoc]  WITH CHECK ADD  CONSTRAINT [FK_MonHoc_BoMon] FOREIGN KEY([MaBoMon])
REFERENCES [dbo].[BoMon] ([MaBoMon])
GO
ALTER TABLE [dbo].[MonHoc] CHECK CONSTRAINT [FK_MonHoc_BoMon]
GO
ALTER TABLE [dbo].[NCKH]  WITH CHECK ADD  CONSTRAINT [FK_NCKH_GV] FOREIGN KEY([MaGV])
REFERENCES [dbo].[GV] ([MaGV])
GO
ALTER TABLE [dbo].[NCKH] CHECK CONSTRAINT [FK_NCKH_GV]
GO
ALTER TABLE [dbo].[PCGD]  WITH CHECK ADD  CONSTRAINT [FK_PCGD_GV] FOREIGN KEY([MaGV])
REFERENCES [dbo].[GV] ([MaGV])
GO
ALTER TABLE [dbo].[PCGD] CHECK CONSTRAINT [FK_PCGD_GV]
GO
ALTER TABLE [dbo].[PCGD]  WITH CHECK ADD  CONSTRAINT [FK_PCGD_LopHP] FOREIGN KEY([MaHP])
REFERENCES [dbo].[LopHP] ([MaHP])
GO
ALTER TABLE [dbo].[PCGD] CHECK CONSTRAINT [FK_PCGD_LopHP]
GO
USE [master]
GO
ALTER DATABASE [TeachingSchedule] SET  READ_WRITE 
GO
