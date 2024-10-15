USE [master]
GO
/****** Object:  Database [QLBangHangBTLWeb]    Script Date: 10/14/2024 9:47:54 PM ******/
CREATE DATABASE [QLBangHangBTLWeb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QLBangHangBTLWeb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\QLBangHangBTLWeb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'QLBangHangBTLWeb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\QLBangHangBTLWeb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [QLBangHangBTLWeb] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QLBangHangBTLWeb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QLBangHangBTLWeb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QLBangHangBTLWeb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QLBangHangBTLWeb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QLBangHangBTLWeb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QLBangHangBTLWeb] SET ARITHABORT OFF 
GO
ALTER DATABASE [QLBangHangBTLWeb] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [QLBangHangBTLWeb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QLBangHangBTLWeb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QLBangHangBTLWeb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QLBangHangBTLWeb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QLBangHangBTLWeb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QLBangHangBTLWeb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QLBangHangBTLWeb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QLBangHangBTLWeb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QLBangHangBTLWeb] SET  ENABLE_BROKER 
GO
ALTER DATABASE [QLBangHangBTLWeb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QLBangHangBTLWeb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QLBangHangBTLWeb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QLBangHangBTLWeb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QLBangHangBTLWeb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QLBangHangBTLWeb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QLBangHangBTLWeb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QLBangHangBTLWeb] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [QLBangHangBTLWeb] SET  MULTI_USER 
GO
ALTER DATABASE [QLBangHangBTLWeb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QLBangHangBTLWeb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QLBangHangBTLWeb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QLBangHangBTLWeb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [QLBangHangBTLWeb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [QLBangHangBTLWeb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [QLBangHangBTLWeb] SET QUERY_STORE = ON
GO
ALTER DATABASE [QLBangHangBTLWeb] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [QLBangHangBTLWeb]
GO
/****** Object:  Table [dbo].[tAnhChiTietSP]    Script Date: 10/14/2024 9:47:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tAnhChiTietSP](
	[MaChiTietSP] [int] NOT NULL,
	[TenFileAnh] [nvarchar](255) NOT NULL,
	[ViTri] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaChiTietSP] ASC,
	[TenFileAnh] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tAnhSP]    Script Date: 10/14/2024 9:47:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tAnhSP](
	[MaSP] [int] NOT NULL,
	[TenFileAnh] [nvarchar](255) NOT NULL,
	[ViTri] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaSP] ASC,
	[TenFileAnh] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tChiTietSanPham]    Script Date: 10/14/2024 9:47:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tChiTietSanPham](
	[MaChiTietSP] [int] IDENTITY(1,1) NOT NULL,
	[MaSP] [int] NOT NULL,
	[KichThuoc] [nvarchar](50) NULL,
	[MauSac] [nvarchar](50) NULL,
	[AnhDaiDien] [nvarchar](255) NULL,
	[DonGiaBan] [decimal](18, 2) NULL,
	[SLTon] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaChiTietSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tDanhMucSP]    Script Date: 10/14/2024 9:47:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tDanhMucSP](
	[MaSP] [int] IDENTITY(1,1) NOT NULL,
	[TenSP] [nvarchar](100) NULL,
	[ChatLieu] [nvarchar](100) NULL,
	[LoaiDT] [nvarchar](100) NULL,
	[HangSX] [nvarchar](100) NULL,
	[ThoiGianBaoHanh] [int] NULL,
	[GioiThieuSP] [nvarchar](max) NULL,
	[AnhDaiDien] [nvarchar](255) NULL,
	[GiaNhoNhat] [decimal](18, 2) NULL,
	[GiaLonNhat] [decimal](18, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tHoaDonBan]    Script Date: 10/14/2024 9:47:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tHoaDonBan](
	[MaSP] [int] NOT NULL,
	[NgayHoaDon] [date] NULL,
	[MaKhachHang] [int] NULL,
	[MaNhanVien] [int] NULL,
	[TongTienHD] [decimal](18, 2) NULL,
	[DonGiaBan] [decimal](18, 2) NULL,
	[SoLuongBan] [int] NULL,
	[PhuongThucThanhToan] [nvarchar](100) NULL,
	[GhiChu] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tKhachHang]    Script Date: 10/14/2024 9:47:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tKhachHang](
	[MaKhachHang] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](50) NOT NULL,
	[TenKhachHang] [nvarchar](100) NULL,
	[NgaySinh] [date] NULL,
	[SoDienThoai] [nvarchar](20) NULL,
	[DiaChi] [nvarchar](255) NULL,
	[GhiChu] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaKhachHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tNhanVien]    Script Date: 10/14/2024 9:47:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tNhanVien](
	[MaNhanVien] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](50) NOT NULL,
	[TenNhanVien] [nvarchar](100) NULL,
	[NgaySinh] [date] NULL,
	[ChucVu] [nvarchar](50) NULL,
	[GhiChu] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaNhanVien] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tUser]    Script Date: 10/14/2024 9:47:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tUser](
	[username] [nvarchar](50) NOT NULL,
	[password] [nvarchar](255) NOT NULL,
	[LoaiUser] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tDanhMucSP] ON 

INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (1, N'iphone 15', N'Titan', N'Đồ điện tử', N'apple', 1, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đạiChất liệu cao cấp có độ bền cao, thiết kế hiện đại', NULL, NULL, CAST(100000.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (2, N'iphone 16', N'Titan', N'Đồ điện tử', N'apple', 2, N'gioi thieu tiep', N'1', CAST(54756754.00 AS Decimal(18, 2)), CAST(6365345.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (20, N'Quạt điện', N'Nhựa', N'Đồ gia dụng', N'SENKO', 2, N'Đủ khả năng chịu nhiệt, đảm bảo an toàn khi sử dụng', N'1', CAST(250000.00 AS Decimal(18, 2)), CAST(300000.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (30, N'Máy sấy tóc', N'Nhựa ', N'Đồ gia dụng', N'Kaning', 2, N'Có thiết kế tối ưu gió và nhiệt', N'1', CAST(120000.00 AS Decimal(18, 2)), CAST(180000.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (31, N'Máy hút bụi', N'Nhựa', N'Đồ gia dụng', N'Puhi', 2, N'Công suất cực mạnh, tích hợp 6 đầu hút bụi linh hoạt', N'2', CAST(150000.00 AS Decimal(18, 2)), CAST(250000.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (32, N'Máy lọc không khí', N'Nhựa', N'Đồ gia dụng', N'Xiaomi', 2, N'Thiết kế màn hình cảm ứng OLED, tối giản, thanh lịch', N'2', CAST(1400000.00 AS Decimal(18, 2)), CAST(3500000.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (33, N'Sữa tắm', N'Dạng kem', N'Sp chăm sóc sắc đẹp', N'Dove', 2, N'Được Viện da liễu trung ương khuyên dùng', N'2', CAST(170000.00 AS Decimal(18, 2)), CAST(300000.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (34, N'Chuột không dây', N'Nhựa', N'Đồ điện tử', N'Logitech', 2, N'Thiết kế thoải mái và chính xác, tiết kiệm năng lượng', N'2', CAST(120000.00 AS Decimal(18, 2)), CAST(150000.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (35, N'Giày thể thao', N'Vải, da', N'Mặt hàng thời trang', N'ASIA', 2, N'Đế giày có độ ma sát cao, chắn chắn, an toàn', N'2', CAST(145000.00 AS Decimal(18, 2)), CAST(200000.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (36, N'Bàn làm việc', N'Nhựa', N'Đồ gia dụng', N'Abc', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'1', CAST(600000.00 AS Decimal(18, 2)), CAST(1000000.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (37, N'Bếp từ cảm ứng', N'Kính cường lực', N'Đồ gia dụng', N'Sunhouse', 2, N'Mặt kính chịu lực cho mọi loại nồi', N'1', CAST(300000.00 AS Decimal(18, 2)), CAST(500000.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (38, N'Khẩu trang', N'Polyester', N'Mặt hàng thời trang', N'BS MASK', 2, N'Cấu trúc đa lớp ngăn ngừa khói bụi', N'1', CAST(50000.00 AS Decimal(18, 2)), CAST(160000.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (40, N'Bàn học', N'Gỗ', N'Đồ gia dụng', N'apple', 2, N'giới thiệu vớ vẩn', N'1', CAST(12232.00 AS Decimal(18, 2)), CAST(1231313213.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (47, N'dũng', N'Gỗ', N'Đồ gia dụng', N'Sunhouse', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'blog_1', CAST(456456.00 AS Decimal(18, 2)), CAST(646546.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (49, N'Aniseed Syrup 123', N'Nhựa', N'Mặt hàng thời trang', N'Sunhouse', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'cloth_1', CAST(1378.00 AS Decimal(18, 2)), CAST(5346456.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (50, N'Change', N'Nhựa', N'Mặt hàng thời trang', N'Sunhouse', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(1562.00 AS Decimal(18, 2)), CAST(43523423.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (51, N'Aniseed Syrup', N'Nhựa', N'Mặt hàng thời trang', N'Sunhouse', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(491.00 AS Decimal(18, 2)), CAST(23423.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (52, N'Chef Anton''s Cajun Seasoning', N'Nhựa', N'Mặt hàng thời trang', N'Sunhouse', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(216.00 AS Decimal(18, 2)), CAST(423.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (53, N'Chef Anton''s Gumbo Mix', N'Nhựa', N'Mặt hàng thời trang', N'Sunhouse', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(134.00 AS Decimal(18, 2)), CAST(423.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (54, N'Grandma''s Boysenberry Spread', N'Nhựa', N'Mặt hàng thời trang', N'Sunhouse', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(153.00 AS Decimal(18, 2)), CAST(4.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (55, N'Uncle Bob''s Organic Dried Pears', N'Nhựa', N'Mặt hàng thời trang', N'Sunhouse', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(5006.00 AS Decimal(18, 2)), CAST(234.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (56, N'Northwoods Cranberry Sauce', N'Titan', N'Mặt hàng thời trang', N'Sunhouse', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(0.00 AS Decimal(18, 2)), CAST(2354.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (57, N'Mishi Kobe Niku', N'Titan', N'Mặt hàng thời trang', N'Sunhouse', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(3.00 AS Decimal(18, 2)), CAST(235.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (58, N'Ikura', N'Titan', N'Mặt hàng thời trang', N'Sunhouse', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(3.00 AS Decimal(18, 2)), CAST(23.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (59, N'Queso Cabrales', N'Titan', N'Mặt hàng thời trang', N'Sunhouse', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(1.00 AS Decimal(18, 2)), CAST(45.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (60, N'Queso Manchego La Pastora', N'Titan', N'Mặt hàng thời trang', N'Sunhouse', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(1.00 AS Decimal(18, 2)), CAST(435.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (61, N'Konbu', N'Titan', N'Mặt hàng thời trang', N'Sunhouse', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(0.00 AS Decimal(18, 2)), CAST(34.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (62, N'Tofu', N'Titan', N'Mặt hàng thời trang', N'Sunhouse', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(0.00 AS Decimal(18, 2)), CAST(65.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (63, N'Genen Shouyu', N'Gỗ', N'Mặt hàng thời trang', N'Sunhouse', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(0.00 AS Decimal(18, 2)), CAST(34.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (64, N'Pavlova', N'Gỗ', N'Mặt hàng thời trang', N'Sunhouse', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(329.00 AS Decimal(18, 2)), CAST(6534.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (66, N'Carnarvon Tigers', N'Gỗ', N'Mặt hàng thời trang', N'Apple', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(0.00 AS Decimal(18, 2)), CAST(245.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (67, N'Teatime Chocolate Biscuits', N'Kính cường lực', N'Đồ gia dụng', N'Apple', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(12.00 AS Decimal(18, 2)), CAST(23.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (68, N'Sir Rodney''s Marmalade', N'Kính cường lực', N'Đồ gia dụng', N'Apple', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(0.00 AS Decimal(18, 2)), CAST(4.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (69, N'Sir Rodney''s Scones', N'Kính cường lực', N'Đồ gia dụng', N'Apple', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(1.00 AS Decimal(18, 2)), CAST(34.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (70, N'Gustaf''s KnÃ¤ckebrÃ¶d', N'Kính cường lực', N'Đồ gia dụng', N'Apple', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(40.00 AS Decimal(18, 2)), CAST(534.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (71, N'TunnbrÃÂ¶d', N'Kính cường lực', N'Đồ gia dụng', N'Apple', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(762.00 AS Decimal(18, 2)), CAST(65345.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (72, N'GuaranÃ¡ FantÃ¡stica', N'Kính cường lực', N'Đồ gia dụng', N'Apple', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(96.00 AS Decimal(18, 2)), CAST(34.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (73, N'NuNuCa NuÃÂ-Nougat-Creme', N'Kính cường lực', N'Đồ gia dụng', N'Apple', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(0.00 AS Decimal(18, 2)), CAST(53.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (74, N'GumbÃ¤r GummibÃ¤rchen', N'Vải, da', N'Đồ gia dụng', N'Apple', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(0.00 AS Decimal(18, 2)), CAST(45.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (75, N'Schoggi Schokolade', N'Vải, da', N'Đồ gia dụng', N'Apple', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(0.00 AS Decimal(18, 2)), CAST(34.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (76, N'RÃ¶ssle Sauerkraut', N'Vải, da', N'Đồ gia dụng', N'Apple', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(0.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (77, N'ThÃ¼ringer Rostbratwurst', N'Vải, da', N'Đồ điện tử', N'Apple', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(2.00 AS Decimal(18, 2)), CAST(345.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (78, N'Nord-Ost Matjeshering', N'Vải, da', N'Đồ điện tử', N'Apple', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(0.00 AS Decimal(18, 2)), CAST(34.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (79, N'Gorgonzola Telino', N'Vải, da', N'Đồ điện tử', N'Apple', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(1.00 AS Decimal(18, 2)), CAST(534.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (80, N'Mascarpone Fabioli', N'Vải, da', N'Đồ điện tử', N'Apple', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(0.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (81, N'Geitost', N'Vải, da', N'Đồ điện tử', N'Apple', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(0.00 AS Decimal(18, 2)), CAST(34.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (82, N'Sasquatch Ale', N'Vải, da', N'Đồ điện tử', N'Apple', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(7.00 AS Decimal(18, 2)), CAST(65.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (83, N'Steeleye Stout', N'gạch', N'Đồ điện tử', N'Apple', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(1.00 AS Decimal(18, 2)), CAST(476.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (84, N'Inlagd Sill', N'gạch', N'Đồ điện tử', N'Xiaomi', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(0.00 AS Decimal(18, 2)), CAST(457.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (85, N'Gravad lax', N'gạch', N'Đồ điện tử', N'Xiaomi', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(0.00 AS Decimal(18, 2)), CAST(45.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (86, N'CÃ´te de Blaye', N'gạch', N'Đồ điện tử', N'Xiaomi', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(3.00 AS Decimal(18, 2)), CAST(6.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (87, N'Chartreuse verte', N'Vải, da', N'Đồ điện tử', N'Xiaomi', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(1.00 AS Decimal(18, 2)), CAST(543.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (88, N'Boston Crab Meat', N'Vải, da', N'Đồ điện tử', N'Xiaomi', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(0.00 AS Decimal(18, 2)), CAST(634.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (89, N'Jack''s New England Clam Chowder', N'Vải, da', N'Đồ điện tử', N'Xiaomi', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(0.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (90, N'Singaporean Hokkien Fried Mee', N'Vải, da', N'Đồ điện tử', N'Xiaomi', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(0.00 AS Decimal(18, 2)), CAST(34.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (91, N'Ipoh Coffee', N'Vải, da', N'Đồ điện tử', N'Xiaomi', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(2.00 AS Decimal(18, 2)), CAST(534.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (92, N'Gula Malacca', N'Vải, da', N'Đồ điện tử', N'Xiaomi', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(0.00 AS Decimal(18, 2)), CAST(534.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (93, N'Rogede sild', N'Vải, da', N'Đồ điện tử', N'Xiaomi', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(0.00 AS Decimal(18, 2)), CAST(534.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (94, N'Spegesild', N'Vải, da', N'Đồ điện tử', N'Xiaomi', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(1.00 AS Decimal(18, 2)), CAST(534.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (95, N'Zaanse koeken', N'Vải, da', N'Đồ điện tử', N'Xiaomi', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(1.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (96, N'Chocolade', N'Vải, da', N'Đồ điện tử', N'Xiaomi', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(0.00 AS Decimal(18, 2)), CAST(345.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (97, N'Maxilaku', N'Vải, da', N'Đồ điện tử', N'Xiaomi', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(0.00 AS Decimal(18, 2)), CAST(34.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (98, N'Valkoinen suklaa', N'Vải, da', N'Đồ điện tử', N'Xiaomi', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(0.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (99, N'Manjimup Dried Apples', N'Vải, da', N'Sp chăm sóc sắc đẹp', N'Xiaomi', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(0.00 AS Decimal(18, 2)), CAST(345.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (100, N'Filo Mix', N'Titan', N'Sp chăm sóc sắc đẹp', N'Xiaomi', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(0.00 AS Decimal(18, 2)), CAST(43.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (101, N'Perth Pasties', N'Titan', N'Sp chăm sóc sắc đẹp', N'Xiaomi', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(0.00 AS Decimal(18, 2)), CAST(53.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (102, N'TourtiÃ¨re', N'Titan', N'Sp chăm sóc sắc đẹp', N'Xiaomi', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(1.00 AS Decimal(18, 2)), CAST(345.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (103, N'PÃ¢tÃ© chinois', N'Titan', N'Sp chăm sóc sắc đẹp', N'Xiaomi', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(0.00 AS Decimal(18, 2)), CAST(345.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (104, N'Gnocchi di nonna Alice', N'Titan', N'Sp chăm sóc sắc đẹp', N'Xiaomi', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(0.00 AS Decimal(18, 2)), CAST(36745745.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (105, N'Ravioli Angelo', N'Titan', N'Sp chăm sóc sắc đẹp', N'Xiaomi', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(0.00 AS Decimal(18, 2)), CAST(34534.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (106, N'Escargots de Bourgogne', N'1007', N'Sp chăm sóc sắc đẹp', N'Xiaomi', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(0.00 AS Decimal(18, 2)), CAST(34534.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (107, N'Raclette Courdavault', N'1003', N'Sp chăm sóc sắc đẹp', N'Xiaomi', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(0.00 AS Decimal(18, 2)), CAST(345345345.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (108, N'Camembert Pierrot', N'1003', N'Sp chăm sóc sắc đẹp', N'Xiaomi', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(1.00 AS Decimal(18, 2)), CAST(345345.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (109, N'Sirop d''Ã©rable', N'Titan', N'Sp chăm sóc sắc đẹp', N'Xiaomi', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(0.00 AS Decimal(18, 2)), CAST(3435.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (110, N'Tarte au sucre', N'Titan', N'Sp chăm sóc sắc đẹp', N'Xiaomi', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(1.00 AS Decimal(18, 2)), CAST(56756.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (111, N'Vegie-spread', N'Titan', N'Sp chăm sóc sắc đẹp', N'Xiaomi', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(0.00 AS Decimal(18, 2)), CAST(5567.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (112, N'Wimmers gute SemmelknÃ¶del', N'Titan', N'Sp chăm sóc sắc đẹp', N'Xiaomi', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(0.00 AS Decimal(18, 2)), CAST(5675.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (113, N'Louisiana Fiery Hot Pepper Sauce', N'Titan', N'Sp chăm sóc sắc đẹp', N'Xiaomi', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(0.00 AS Decimal(18, 2)), CAST(75675.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (114, N'Louisiana Hot Spiced Okra', N'Titan', N'Sp chăm sóc sắc đẹp', N'Xiaomi', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(0.00 AS Decimal(18, 2)), CAST(678.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (115, N'Laughing Lumberjack Lager', N'Titan', N'Sp chăm sóc sắc đẹp', N'Xiaomi', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(1.00 AS Decimal(18, 2)), CAST(3456.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (116, N'Scottish Longbreads', N'Titan', N'Sp chăm sóc sắc đẹp', N'Xiaomi', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(0.00 AS Decimal(18, 2)), CAST(345.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (117, N'Gudbrandsdalsost', N'Titan', N'Sp chăm sóc sắc đẹp', N'Xiaomi', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(0.00 AS Decimal(18, 2)), CAST(63456.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (118, N'Outback Lager', N'Titan', N'Sp chăm sóc sắc đẹp', N'Xiaomi', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(1.00 AS Decimal(18, 2)), CAST(4532.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (119, N'Flotemysost', N'Titan', N'Sp chăm sóc sắc đẹp', N'Xiaomi', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(1.00 AS Decimal(18, 2)), CAST(63.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (120, N'Mozzarella di Giovanni', N'Titan', N'Đồ gia dụng', N'Logitech', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(1.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (121, N'RÃ¶d Kaviar', N'Titan', N'Đồ gia dụng', N'Logitech', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(0.00 AS Decimal(18, 2)), CAST(87.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (122, N'Longlife Tofu', N'Titan', N'Đồ gia dụng', N'Logitech', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(0.00 AS Decimal(18, 2)), CAST(7656.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (123, N'RhÃ¶nbrÃ¤u Klosterbier', N'Titan', N'Đồ gia dụng', N'Logitech', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(0.00 AS Decimal(18, 2)), CAST(5345.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (124, N'LakkalikÃ¶Ã¶ri', N'Titan', N'Đồ gia dụng', N'Logitech', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(0.00 AS Decimal(18, 2)), CAST(34.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (125, N'Original Frankfurter grÃ¼ne SoÃe', N'Titan', N'Đồ gia dụng', N'Logitech', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(1.00 AS Decimal(18, 2)), CAST(345.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (126, N'Chai88888888', N'Gỗ', N'Đồ gia dụng', N'Logitech', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(4.00 AS Decimal(18, 2)), CAST(5345345.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (127, N'Mishi Kobe Niku', N'Gỗ', N'Đồ gia dụng', N'Logitech', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(0.00 AS Decimal(18, 2)), CAST(354.00 AS Decimal(18, 2)))
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [GiaNhoNhat], [GiaLonNhat]) VALUES (128, N'Change-New', N'Gỗ', N'Đồ gia dụng', N'Logitech', 2, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'0', CAST(1.00 AS Decimal(18, 2)), CAST(4353544.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[tDanhMucSP] OFF
GO
ALTER TABLE [dbo].[tAnhChiTietSP]  WITH CHECK ADD  CONSTRAINT [FK_AnhChiTietSP_ChiTietSP] FOREIGN KEY([MaChiTietSP])
REFERENCES [dbo].[tChiTietSanPham] ([MaChiTietSP])
GO
ALTER TABLE [dbo].[tAnhChiTietSP] CHECK CONSTRAINT [FK_AnhChiTietSP_ChiTietSP]
GO
ALTER TABLE [dbo].[tAnhSP]  WITH CHECK ADD  CONSTRAINT [FK_AnhSP_DanhMucSP] FOREIGN KEY([MaSP])
REFERENCES [dbo].[tDanhMucSP] ([MaSP])
GO
ALTER TABLE [dbo].[tAnhSP] CHECK CONSTRAINT [FK_AnhSP_DanhMucSP]
GO
ALTER TABLE [dbo].[tChiTietSanPham]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietSP_DanhMucSP] FOREIGN KEY([MaSP])
REFERENCES [dbo].[tDanhMucSP] ([MaSP])
GO
ALTER TABLE [dbo].[tChiTietSanPham] CHECK CONSTRAINT [FK_ChiTietSP_DanhMucSP]
GO
ALTER TABLE [dbo].[tHoaDonBan]  WITH CHECK ADD  CONSTRAINT [FK_HoaDonBan_DanhMucSP] FOREIGN KEY([MaSP])
REFERENCES [dbo].[tDanhMucSP] ([MaSP])
GO
ALTER TABLE [dbo].[tHoaDonBan] CHECK CONSTRAINT [FK_HoaDonBan_DanhMucSP]
GO
ALTER TABLE [dbo].[tHoaDonBan]  WITH CHECK ADD  CONSTRAINT [FK_HoaDonBan_KhachHang] FOREIGN KEY([MaKhachHang])
REFERENCES [dbo].[tKhachHang] ([MaKhachHang])
GO
ALTER TABLE [dbo].[tHoaDonBan] CHECK CONSTRAINT [FK_HoaDonBan_KhachHang]
GO
ALTER TABLE [dbo].[tHoaDonBan]  WITH CHECK ADD  CONSTRAINT [FK_HoaDonBan_NhanVien] FOREIGN KEY([MaNhanVien])
REFERENCES [dbo].[tNhanVien] ([MaNhanVien])
GO
ALTER TABLE [dbo].[tHoaDonBan] CHECK CONSTRAINT [FK_HoaDonBan_NhanVien]
GO
ALTER TABLE [dbo].[tKhachHang]  WITH CHECK ADD  CONSTRAINT [FK_KhachHang_User] FOREIGN KEY([username])
REFERENCES [dbo].[tUser] ([username])
GO
ALTER TABLE [dbo].[tKhachHang] CHECK CONSTRAINT [FK_KhachHang_User]
GO
ALTER TABLE [dbo].[tNhanVien]  WITH CHECK ADD  CONSTRAINT [FK_tNhanVien_tUser] FOREIGN KEY([username])
REFERENCES [dbo].[tUser] ([username])
GO
ALTER TABLE [dbo].[tNhanVien] CHECK CONSTRAINT [FK_tNhanVien_tUser]
GO
USE [master]
GO
ALTER DATABASE [QLBangHangBTLWeb] SET  READ_WRITE 
GO
