USE [master]
GO
/****** Object:  Database [QLBanDoThoiTrang]    Script Date: 30/10/2024 05:02:41 PM ******/
CREATE DATABASE [QLBanDoThoiTrang]
GO
ALTER DATABASE [QLBanDoThoiTrang] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QLBanDoThoiTrang].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QLBanDoThoiTrang] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QLBanDoThoiTrang] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QLBanDoThoiTrang] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QLBanDoThoiTrang] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QLBanDoThoiTrang] SET ARITHABORT OFF 
GO
ALTER DATABASE [QLBanDoThoiTrang] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [QLBanDoThoiTrang] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QLBanDoThoiTrang] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QLBanDoThoiTrang] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QLBanDoThoiTrang] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QLBanDoThoiTrang] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QLBanDoThoiTrang] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QLBanDoThoiTrang] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QLBanDoThoiTrang] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QLBanDoThoiTrang] SET  ENABLE_BROKER 
GO
ALTER DATABASE [QLBanDoThoiTrang] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QLBanDoThoiTrang] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QLBanDoThoiTrang] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QLBanDoThoiTrang] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QLBanDoThoiTrang] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QLBanDoThoiTrang] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QLBanDoThoiTrang] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QLBanDoThoiTrang] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [QLBanDoThoiTrang] SET  MULTI_USER 
GO
ALTER DATABASE [QLBanDoThoiTrang] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QLBanDoThoiTrang] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QLBanDoThoiTrang] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QLBanDoThoiTrang] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [QLBanDoThoiTrang] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [QLBanDoThoiTrang] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [QLBanDoThoiTrang] SET QUERY_STORE = ON
GO
ALTER DATABASE [QLBanDoThoiTrang] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [QLBanDoThoiTrang]
GO
/****** Object:  Table [dbo].[tAnhChiTietSP]    Script Date: 30/10/2024 01:58:41 PM ******/
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
/****** Object:  Table [dbo].[tAnhSP]    Script Date: 30/10/2024 01:58:41 PM ******/
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
/****** Object:  Table [dbo].[tChiTietHoaDonBan]    Script Date: 30/10/2024 01:58:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tChiTietHoaDonBan](
	[MaHoaDonBan] [int] NOT NULL,
	[MaChiTietSP] [int] NOT NULL,
	[DonGiaBan] [decimal](18, 2) NULL,
	[SoLuongBan] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tChiTietSanPham]    Script Date: 30/10/2024 01:58:41 PM ******/
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
	[SLTon] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaChiTietSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tDanhGia]    Script Date: 30/10/2024 01:58:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tDanhGia](
	[MaDanhGia] [int] IDENTITY(1,1) NOT NULL,
	[MaKhachHang] [int] NOT NULL,
	[MaSP] [int] NOT NULL,
	[Ngaytao] [datetime] NOT NULL,
	[Diem] [int] NOT NULL,
	[BinhLuan] [nvarchar](1000) NULL,
	[LichSu] [nvarchar](max) NULL,
	[MaNhanVien] [int] NULL,
	[TraLoi] [nvarchar](1000) NULL,
 CONSTRAINT [PK_tDanhGia] PRIMARY KEY CLUSTERED 
(
	[MaDanhGia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tDanhMucSP]    Script Date: 30/10/2024 01:58:41 PM ******/
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
	[Gia] [decimal](18, 2) NULL,
	[TagId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TDanhSachCuaHang]    Script Date: 30/10/2024 01:58:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TDanhSachCuaHang](
	[SDTCuaHang] [nchar](10) NOT NULL,
	[DiaChi] [nvarchar](300) NULL,
	[KhuVuc] [nvarchar](100) NULL,
	[KinhDo] [float] NULL,
	[ViDo] [float] NULL,
 CONSTRAINT [PK_DanhSachCuaHang] PRIMARY KEY CLUSTERED 
(
	[SDTCuaHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TempUserOtp]    Script Date: 30/10/2024 01:58:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TempUserOtp](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](255) NOT NULL,
	[OtpCode] [nvarchar](7) NULL,
	[OtpExpiration] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TGiaoHang]    Script Date: 30/10/2024 01:58:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TGiaoHang](
	[MaGiaoHang] [int] IDENTITY(1,1) NOT NULL,
	[MaHoaDonBan] [int] NULL,
	[ThanhPho] [nvarchar](50) NULL,
	[QuanHuyen] [nvarchar](50) NULL,
	[DiaChi] [nvarchar](300) NULL,
	[SoDienThoai] [nchar](10) NULL,
	[HoTenNguoiNhan] [nvarchar](200) NULL,
 CONSTRAINT [PK_TGiaoHang] PRIMARY KEY CLUSTERED 
(
	[MaGiaoHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tGioHang]    Script Date: 30/10/2024 01:58:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tGioHang](
	[MaGioHang] [int] IDENTITY(1,1) NOT NULL,
	[MaKhachHang] [int] NOT NULL,
	[MaChiTietSP] [int] NOT NULL,
	[SoLuong] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaGioHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tHoaDonBan]    Script Date: 30/10/2024 01:58:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tHoaDonBan](
	[MaHoaDonBan] [int] IDENTITY(1,1) NOT NULL,
	[NgayHoaDon] [datetime] NULL,
	[MaKhachHang] [int] NULL,
	[TongTienHD] [decimal](18, 2) NULL,
	[PhuongThucThanhToan] [nvarchar](100) NULL,
	[GhiChu] [nvarchar](max) NULL,
	[MaGiamGia] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaHoaDonBan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tKhachHang]    Script Date: 30/10/2024 01:58:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tKhachHang](
	[MaKhachHang] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
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
/****** Object:  Table [dbo].[tMaGiamGia]    Script Date: 30/10/2024 01:58:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tMaGiamGia](
	[MaGiamGia] [int] NOT NULL,
	[Code] [varchar](50) NULL,
	[TiLeGiam] [decimal](3, 2) NULL,
	[MoTa] [nvarchar](max) NULL,
	[NgayBatDau] [datetime] NULL,
	[NgayKetThuc] [datetime] NULL,
	[TrangThai] [int] NULL,
 CONSTRAINT [PK_tMaGiamGia] PRIMARY KEY CLUSTERED 
(
	[MaGiamGia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tNhanVien]    Script Date: 30/10/2024 01:58:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tNhanVien](
	[MaNhanVien] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
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
/****** Object:  Table [dbo].[tPhanHoi]    Script Date: 30/10/2024 01:58:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tPhanHoi](
	[MaKhachHang] [int] NOT NULL,
	[MaDanhGia] [int] NOT NULL,
	[Thich] [int] NULL,
	[HuuIch] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TTag]    Script Date: 30/10/2024 01:58:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TTag](
	[TagId] [int] IDENTITY(1,1) NOT NULL,
	[TagName] [nvarchar](50) NULL,
	[TagImage] [nvarchar](50) NULL,
 CONSTRAINT [PK_TTag] PRIMARY KEY CLUSTERED 
(
	[TagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tUser]    Script Date: 30/10/2024 01:58:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tUser](
	[Email] [nvarchar](50) NOT NULL,
	[password] [nvarchar](255) NOT NULL,
	[Salt] [nvarchar](50) NULL,
	[LoaiUser] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[tChiTietHoaDonBan] ([MaHoaDonBan], [MaChiTietSP], [DonGiaBan], [SoLuongBan]) VALUES (1, 3, CAST(160000.00 AS Decimal(18, 2)), 11)
INSERT [dbo].[tChiTietHoaDonBan] ([MaHoaDonBan], [MaChiTietSP], [DonGiaBan], [SoLuongBan]) VALUES (2, 3, CAST(160000.00 AS Decimal(18, 2)), 11)
INSERT [dbo].[tChiTietHoaDonBan] ([MaHoaDonBan], [MaChiTietSP], [DonGiaBan], [SoLuongBan]) VALUES (3, 3, CAST(160000.00 AS Decimal(18, 2)), 11)
INSERT [dbo].[tChiTietHoaDonBan] ([MaHoaDonBan], [MaChiTietSP], [DonGiaBan], [SoLuongBan]) VALUES (4, 2, CAST(160000.00 AS Decimal(18, 2)), 2)
INSERT [dbo].[tChiTietHoaDonBan] ([MaHoaDonBan], [MaChiTietSP], [DonGiaBan], [SoLuongBan]) VALUES (5, 3, CAST(160000.00 AS Decimal(18, 2)), 1)
GO
SET IDENTITY_INSERT [dbo].[tChiTietSanPham] ON 

INSERT [dbo].[tChiTietSanPham] ([MaChiTietSP], [MaSP], [KichThuoc], [MauSac], [AnhDaiDien], [SLTon]) VALUES (1, 32, N'Nhỏ', N'Trắng', NULL, NULL)
INSERT [dbo].[tChiTietSanPham] ([MaChiTietSP], [MaSP], [KichThuoc], [MauSac], [AnhDaiDien], [SLTon]) VALUES (2, 32, N'Lớn', N'Trắng', NULL, NULL)
INSERT [dbo].[tChiTietSanPham] ([MaChiTietSP], [MaSP], [KichThuoc], [MauSac], [AnhDaiDien], [SLTon]) VALUES (3, 32, N'Lớn', N'Đen', NULL, NULL)
SET IDENTITY_INSERT [dbo].[tChiTietSanPham] OFF
GO
SET IDENTITY_INSERT [dbo].[tDanhMucSP] ON 

INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [Gia], [TagId]) VALUES (23, N'Quạt điện', N'Nhựa', N'Đồ gia dụng', N'SENKO', 24, N'Đủ khả năng chịu nhiệt, đảm bảo an toàn khi sử dụng', N'0', CAST(300000.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [Gia], [TagId]) VALUES (24, N'Máy sấy tóc', N'Nhựa ', N'Đồ gia dụng', N'Kaning', 6, N'Có thiết kế tối ưu gió và nhiệt', N'1', CAST(180000.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [Gia], [TagId]) VALUES (25, N'Máy hút bụi', N'Nhựa', N'Đồ gia dụng', N'Puhi', 12, N'Công suất cực mạnh, tích hợp 6 đầu hút bụi linh hoạt', N'may-hut-bui-cam-tay-deerma-dx118c-pro-251023-033910-600x600.jpg', CAST(250000.00 AS Decimal(18, 2)), 2)
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [Gia], [TagId]) VALUES (26, N'Máy lọc không khí', N'Nhựa', N'Đồ gia dụng', N'Xiaomi', 12, N'Thiết kế màn hình cảm ứng OLED, tối giản, thanh lịch', N'0', CAST(3500000.00 AS Decimal(18, 2)), 2)
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [Gia], [TagId]) VALUES (27, N'Sữa tắm', N'Dạng kem', N'Sp chăm sóc sắc đẹp', N'Dove', NULL, N'Được Viện da liễu trung ương khuyên dùng', N'0', CAST(300000.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [Gia], [TagId]) VALUES (28, N'Chuột không dây', N'Nhựa', N'Đồ điện tử', N'Logitech', 3, N'Thiết kế thoải mái và chính xác, tiết kiệm năng lượng', N'1', CAST(150000.00 AS Decimal(18, 2)), 2)
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [Gia], [TagId]) VALUES (29, N'Giày thể thao', N'Vải, da', N'Mặt hàng thời trang', N'ASIA', NULL, N'Đế giày có độ ma sát cao, chắn chắn, an toàn', N'2', CAST(200000.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [Gia], [TagId]) VALUES (30, N'Bàn làm việc', N'Nhựa', N'Đồ gia dụng', N'Abc', NULL, N'Chất liệu cao cấp có độ bền cao, thiết kế hiện đại', N'IMG_3458.JPG', CAST(1000000.00 AS Decimal(18, 2)), 2)
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [Gia], [TagId]) VALUES (31, N'Bếp từ cảm ứng', N'Kính cường lực', N'Đồ gia dụng', N'Sunhouse', NULL, N'Mặt kính chịu lực cho mọi loại nồi', N'IMG_3458.JPG', CAST(500000.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [Gia], [TagId]) VALUES (32, N'Khẩu trang', N'Polyester', N'Mặt hàng thời trang', N'BS MASK', NULL, N'Cấu trúc đa lớp ngăn ngừa khói bụi', N'OIP.jpg', CAST(160000.00 AS Decimal(18, 2)), 2)
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [Gia], [TagId]) VALUES (33, N'Bộ thu đông bé trai BOBDOG', N'cotton ', N'Trẻ con', N'BOBDOG', 1, N'Bộ áo thun dài tay quần bo gấu chất cotton cho bé mang đến sự thoải mái và ấm áp cho bé yêu của bạn trong mọi hoạt động. Thiết kế áo thun dài tay giúp giữ ấm tốt hơn, đặc biệt trong những ngày thời tiết se lạnh. Quần bo gấu vừa vặn, co giãn tốt, tạo sự thoải mái khi bé vận động mà vẫn giữ dáng quần.', N'IMG_3458.JPG', CAST(145000.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [Gia], [TagId]) VALUES (34, N'Áo Khoác Gió 2 Lớp Cho Bé Gái', N'cotton ', N'Trẻ con', N'BOBDOG', 2, N'Bộ đồ có nhiều màu sắc tươi sáng, dễ thương, phù hợp với sở thích của các bé. Đặc biệt, sản phẩm có độ bền cao, không bị co rút hay xù lông sau nhiều lần giặt. Bộ áo thun dài tay quần bo gấu là lựa chọn hoàn hảo cho những ngày bé yêu vui chơi hay đi học, mang lại cảm giác dễ chịu suốt cả ngày.', N'IMG_3458.JPG', CAST(99000.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[tDanhMucSP] ([MaSP], [TenSP], [ChatLieu], [LoaiDT], [HangSX], [ThoiGianBaoHanh], [GioiThieuSP], [AnhDaiDien], [Gia], [TagId]) VALUES (35, N'váy cổ tim in hình labubu', N'cotton ', N'Trẻ con', N'Midi', 3, N'Bạn chọn màu sắc, kích cỡ và số lượng rồi cho vào giỏ hàng. Sau khi bạn chọn đủ thì vào giỏ hàng để tiến hành mua hàng. Bạn có thể điều chỉnh số lượng trong giỏ hàng trước khi quyết định mua', N'2', CAST(200000.00 AS Decimal(18, 2)), 2)
SET IDENTITY_INSERT [dbo].[tDanhMucSP] OFF
GO
INSERT [dbo].[TDanhSachCuaHang] ([SDTCuaHang], [DiaChi], [KhuVuc], [KinhDo], [ViDo]) VALUES (N'0371093688', N'Số 265 Cao Lỗ, Uy Nỗ, Đông Anh, Hà Nội', N'Đông Anh, Hà Nội', 21.1305, 105.856)
INSERT [dbo].[TDanhSachCuaHang] ([SDTCuaHang], [DiaChi], [KhuVuc], [KinhDo], [ViDo]) VALUES (N'0986534334', N'Số 3, Cầu Giấy, Đống Đa, Hà Nội', N'Đống Đa, Hà Nội', 21.0278, 105.8023)
INSERT [dbo].[TDanhSachCuaHang] ([SDTCuaHang], [DiaChi], [KhuVuc], [KinhDo], [ViDo]) VALUES (N'0986575473', N'18 Phạm Văn Ngôn, Hoà Khánh Bắc, Liên Chiểu, Đà Nẵng', N'Liên Chiểu, Đà Nẵng', 16.08073, 108.1264)
INSERT [dbo].[TDanhSachCuaHang] ([SDTCuaHang], [DiaChi], [KhuVuc], [KinhDo], [ViDo]) VALUES (N'0987654321', N'Sô 101 Đào Tấn, Ngọc Khánh, Ba Đình, Hà Nội', N'Ba Đình, Hà Nội', 21.0335, 105.806)
INSERT [dbo].[TDanhSachCuaHang] ([SDTCuaHang], [DiaChi], [KhuVuc], [KinhDo], [ViDo]) VALUES (N'0987654332', N'Số 123, Quận 1, Thành phố Hồ Chí Minh', N'Quận 1, Hồ Chí Minh', 10.8269, 106.6133)
GO
SET IDENTITY_INSERT [dbo].[TempUserOtp] ON 

INSERT [dbo].[TempUserOtp] ([Id], [Email], [OtpCode], [OtpExpiration]) VALUES (5, N'truongvan.minh1504@gmail.com', N'289873', CAST(N'2024-10-29T09:41:09.530' AS DateTime))
SET IDENTITY_INSERT [dbo].[TempUserOtp] OFF
GO
SET IDENTITY_INSERT [dbo].[TGiaoHang] ON 

INSERT [dbo].[TGiaoHang] ([MaGiaoHang], [MaHoaDonBan], [ThanhPho], [QuanHuyen], [DiaChi], [SoDienThoai], [HoTenNguoiNhan]) VALUES (1, 1, N'1', N'1', N'hi', N'0373294997', N'Minh Trương Văn')
INSERT [dbo].[TGiaoHang] ([MaGiaoHang], [MaHoaDonBan], [ThanhPho], [QuanHuyen], [DiaChi], [SoDienThoai], [HoTenNguoiNhan]) VALUES (2, 2, N'14', N'116', N'hi', N'0373294997', N'Minh Trương Văn')
INSERT [dbo].[TGiaoHang] ([MaGiaoHang], [MaHoaDonBan], [ThanhPho], [QuanHuyen], [DiaChi], [SoDienThoai], [HoTenNguoiNhan]) VALUES (3, 3, NULL, NULL, N'hi', N'0373294997', N'Minh Trương Văn')
INSERT [dbo].[TGiaoHang] ([MaGiaoHang], [MaHoaDonBan], [ThanhPho], [QuanHuyen], [DiaChi], [SoDienThoai], [HoTenNguoiNhan]) VALUES (4, 4, N'Tỉnh Nghệ An', N'Huyện Quỳnh Lưu', N'Xóm 10, Quỳnh Lâm', N'0373294997', N'Minh Trương Văn')
INSERT [dbo].[TGiaoHang] ([MaGiaoHang], [MaHoaDonBan], [ThanhPho], [QuanHuyen], [DiaChi], [SoDienThoai], [HoTenNguoiNhan]) VALUES (5, 5, N'Tỉnh Nghệ An', N'Huyện Quỳnh Lưu', N'Xóm 10, Quỳnh Lâm', N'0373294997', N'Minh Trương Văn')
SET IDENTITY_INSERT [dbo].[TGiaoHang] OFF
GO
SET IDENTITY_INSERT [dbo].[tGioHang] ON 

INSERT [dbo].[tGioHang] ([MaGioHang], [MaKhachHang], [MaChiTietSP], [SoLuong]) VALUES (59, 15, 3, 11)
SET IDENTITY_INSERT [dbo].[tGioHang] OFF
GO
SET IDENTITY_INSERT [dbo].[tHoaDonBan] ON 

INSERT [dbo].[tHoaDonBan] ([MaHoaDonBan], [NgayHoaDon], [MaKhachHang], [TongTienHD], [PhuongThucThanhToan], [GhiChu], [MaGiamGia]) VALUES (1, CAST(N'2024-10-26T21:33:49.587' AS DateTime), 15, CAST(0.00 AS Decimal(18, 2)), N'cash', NULL, NULL)
INSERT [dbo].[tHoaDonBan] ([MaHoaDonBan], [NgayHoaDon], [MaKhachHang], [TongTienHD], [PhuongThucThanhToan], [GhiChu], [MaGiamGia]) VALUES (2, CAST(N'2024-10-27T10:41:49.503' AS DateTime), 15, CAST(0.00 AS Decimal(18, 2)), N'online', NULL, NULL)
INSERT [dbo].[tHoaDonBan] ([MaHoaDonBan], [NgayHoaDon], [MaKhachHang], [TongTienHD], [PhuongThucThanhToan], [GhiChu], [MaGiamGia]) VALUES (3, CAST(N'2024-10-27T14:08:14.270' AS DateTime), 15, CAST(0.00 AS Decimal(18, 2)), N'cash', NULL, NULL)
INSERT [dbo].[tHoaDonBan] ([MaHoaDonBan], [NgayHoaDon], [MaKhachHang], [TongTienHD], [PhuongThucThanhToan], [GhiChu], [MaGiamGia]) VALUES (4, CAST(N'2024-10-29T15:59:47.770' AS DateTime), 17, CAST(320000.00 AS Decimal(18, 2)), N'online', NULL, NULL)
INSERT [dbo].[tHoaDonBan] ([MaHoaDonBan], [NgayHoaDon], [MaKhachHang], [TongTienHD], [PhuongThucThanhToan], [GhiChu], [MaGiamGia]) VALUES (5, CAST(N'2024-10-29T16:03:27.230' AS DateTime), 17, CAST(160000.00 AS Decimal(18, 2)), N'online', NULL, NULL)
SET IDENTITY_INSERT [dbo].[tHoaDonBan] OFF
GO
SET IDENTITY_INSERT [dbo].[tKhachHang] ON 

INSERT [dbo].[tKhachHang] ([MaKhachHang], [Email], [TenKhachHang], [NgaySinh], [SoDienThoai], [DiaChi], [GhiChu]) VALUES (2, N'truongvan.minh1504@gmail.com', N'Trương Văn Minh', CAST(N'2004-05-10' AS Date), N'0373294997', N'Xóm 10, quỳnh lâm quỳnh lưu nghệ an', NULL)
INSERT [dbo].[tKhachHang] ([MaKhachHang], [Email], [TenKhachHang], [NgaySinh], [SoDienThoai], [DiaChi], [GhiChu]) VALUES (9, N'Dung2004@gmail.com', N'Phạm Tiến Dũng', NULL, N'45274562', N'152 Nguyễn Đình Hoàn', N'không có')
INSERT [dbo].[tKhachHang] ([MaKhachHang], [Email], [TenKhachHang], [NgaySinh], [SoDienThoai], [DiaChi], [GhiChu]) VALUES (10, N'nam29432@gmail.com', N'Trần Tiến Nam', NULL, N'64356436', N'12 Vũ Tông Phan', N'')
INSERT [dbo].[tKhachHang] ([MaKhachHang], [Email], [TenKhachHang], [NgaySinh], [SoDienThoai], [DiaChi], [GhiChu]) VALUES (11, N'tungdan2120@gmail.com', N'Trần Thanh Tùng', NULL, N'5463456', N'34 Lê Trọng Tấn', N'')
INSERT [dbo].[tKhachHang] ([MaKhachHang], [Email], [TenKhachHang], [NgaySinh], [SoDienThoai], [DiaChi], [GhiChu]) VALUES (12, N'Levan22003@gmail.com', N'Lê Hồng Vân', NULL, N'3452342532', N'66 Hồ Tùng Mậu', N'')
INSERT [dbo].[tKhachHang] ([MaKhachHang], [Email], [TenKhachHang], [NgaySinh], [SoDienThoai], [DiaChi], [GhiChu]) VALUES (13, N'Anhtuan223@gmail.com', N'Phạm Anh Tuấn', NULL, N'35325432', N'90 Hoàng Quốc Việt', N'')
INSERT [dbo].[tKhachHang] ([MaKhachHang], [Email], [TenKhachHang], [NgaySinh], [SoDienThoai], [DiaChi], [GhiChu]) VALUES (14, N'Lehongah324@gmail.com', N'Vũ Phan Tuấn', NULL, N'534534532', N'ngã tư sông tô lịch', N'')
INSERT [dbo].[tKhachHang] ([MaKhachHang], [Email], [TenKhachHang], [NgaySinh], [SoDienThoai], [DiaChi], [GhiChu]) VALUES (15, N'truongminh10052004@gmail.com', N'Minh Trương Văn', CAST(N'2004-05-10' AS Date), N'0373294997', N'hi', NULL)
INSERT [dbo].[tKhachHang] ([MaKhachHang], [Email], [TenKhachHang], [NgaySinh], [SoDienThoai], [DiaChi], [GhiChu]) VALUES (17, N'truongvanminhxom14ql@gmail.com', N'Trương Văn Minh', CAST(N'2004-05-10' AS Date), N'0373294997', N'Xóm 10, Quỳnh Lâm,Huyện Quỳnh Lưu,Tỉnh Nghệ An', NULL)
INSERT [dbo].[tKhachHang] ([MaKhachHang], [Email], [TenKhachHang], [NgaySinh], [SoDienThoai], [DiaChi], [GhiChu]) VALUES (18, N'acckhac69@gmail.com', N'Khac', CAST(N'1111-11-11' AS Date), N'0373294997', N'hi,Quận Ba Đình,Thành phố Hà Nội', NULL)
SET IDENTITY_INSERT [dbo].[tKhachHang] OFF
GO
SET IDENTITY_INSERT [dbo].[tNhanVien] ON 

INSERT [dbo].[tNhanVien] ([MaNhanVien], [Email], [TenNhanVien], [NgaySinh], [ChucVu], [GhiChu]) VALUES (2, N'HongLysAnh22@gmail.com', N'Hông lý', CAST(N'2004-05-10' AS Date), N'Nhân Viên Kho', NULL)
INSERT [dbo].[tNhanVien] ([MaNhanVien], [Email], [TenNhanVien], [NgaySinh], [ChucVu], [GhiChu]) VALUES (3, N'aceqcrush11@gmail.com', N'Lê Hòng Phong', CAST(N'2004-05-10' AS Date), N'Nhân Viên Kho', NULL)
INSERT [dbo].[tNhanVien] ([MaNhanVien], [Email], [TenNhanVien], [NgaySinh], [ChucVu], [GhiChu]) VALUES (4, N'Minh221@gmail.com', N'Trương Văn Minh', CAST(N'2004-05-10' AS Date), N'Nhân Viên Kho', NULL)
INSERT [dbo].[tNhanVien] ([MaNhanVien], [Email], [TenNhanVien], [NgaySinh], [ChucVu], [GhiChu]) VALUES (5, N'SonLEee@gmail.com', N'Lê Đăng Sơn', CAST(N'2004-05-10' AS Date), N'Quản lý', NULL)
INSERT [dbo].[tNhanVien] ([MaNhanVien], [Email], [TenNhanVien], [NgaySinh], [ChucVu], [GhiChu]) VALUES (6, N'Minh23234@gmail.com', N'Trần Văn Minh', CAST(N'2004-05-10' AS Date), N'Quản lý', NULL)
SET IDENTITY_INSERT [dbo].[tNhanVien] OFF
GO
SET IDENTITY_INSERT [dbo].[TTag] ON 

INSERT [dbo].[TTag] ([TagId], [TagName], [TagImage]) VALUES (1, N'Nam', N'male')
INSERT [dbo].[TTag] ([TagId], [TagName], [TagImage]) VALUES (2, N'Nữ', N'female')
SET IDENTITY_INSERT [dbo].[TTag] OFF
GO
INSERT [dbo].[tUser] ([Email], [password], [Salt], [LoaiUser]) VALUES (N'acckhac69@gmail.com', N'f4b29633e3d9496bf37fd2b4b2c867da805bd1eb84d19a4ab0a573807afd3d24', N'izowdfBj6h9jVGlfsvdsna89I1NaQptf8flb3UA3IZY=', N'KhachHang')
INSERT [dbo].[tUser] ([Email], [password], [Salt], [LoaiUser]) VALUES (N'aceqcrush11@gmail.com', N'123', NULL, NULL)
INSERT [dbo].[tUser] ([Email], [password], [Salt], [LoaiUser]) VALUES (N'Anhtuan223@gmail.com', N'123', NULL, NULL)
INSERT [dbo].[tUser] ([Email], [password], [Salt], [LoaiUser]) VALUES (N'Dung2004@gmail.com', N'123', NULL, NULL)
INSERT [dbo].[tUser] ([Email], [password], [Salt], [LoaiUser]) VALUES (N'HongLysAnh22@gmail.com', N'123', NULL, NULL)
INSERT [dbo].[tUser] ([Email], [password], [Salt], [LoaiUser]) VALUES (N'Lehongah324@gmail.com', N'123', NULL, NULL)
INSERT [dbo].[tUser] ([Email], [password], [Salt], [LoaiUser]) VALUES (N'Levan22003@gmail.com', N'123', NULL, NULL)
INSERT [dbo].[tUser] ([Email], [password], [Salt], [LoaiUser]) VALUES (N'linhlinhpham2104@gmail.com', N'e9598abadd8f1a31655681da50c6c1a8de391a4e6a79929ea8511c62f9821f24', N'MoXfw2hz9EMDtVMUSTlR8qBed2XeSQB+2I9a6M2xldQ=', N'Admin')
INSERT [dbo].[tUser] ([Email], [password], [Salt], [LoaiUser]) VALUES (N'Minh221@gmail.com', N'123', NULL, NULL)
INSERT [dbo].[tUser] ([Email], [password], [Salt], [LoaiUser]) VALUES (N'Minh23234@gmail.com', N'123', NULL, NULL)
INSERT [dbo].[tUser] ([Email], [password], [Salt], [LoaiUser]) VALUES (N'nam29432@gmail.com', N'123', NULL, NULL)
INSERT [dbo].[tUser] ([Email], [password], [Salt], [LoaiUser]) VALUES (N'SonLEee@gmail.com', N'123', NULL, NULL)
INSERT [dbo].[tUser] ([Email], [password], [Salt], [LoaiUser]) VALUES (N'truongminh10052004@gmail.com', N'6568ab3302a39a57985807af6f6370055777df3e1ecaf4040d2dce1ecb19f907', N'1yYO2ATJUM/zjNqkx0ZsdJBocN8oawVE0OGtnG4GIbA=', N'KhachHang')
INSERT [dbo].[tUser] ([Email], [password], [Salt], [LoaiUser]) VALUES (N'truongvan.minh1504@gmail.com', N'de1b4e5554bf248a8310d759e452dfb93d05ccf9cb6b40f79bdd3f6fab7baa04', N'+XubEyAW4QCF8XLn1dOHG9eKi3sA4PK8uCAkVssLwNs=', N'KhachHang')
INSERT [dbo].[tUser] ([Email], [password], [Salt], [LoaiUser]) VALUES (N'truongvanminhxom14ql@gmail.com', N'5b310bc647d313ab15151b4d77c9c80659155ed572649363499a6136eef3ceac', N'EZzZInPvlnDAxiOMIGHJ6Zlnn7VZXfJ9A7Rm9lDKLvk=', N'KhachHang')
INSERT [dbo].[tUser] ([Email], [password], [Salt], [LoaiUser]) VALUES (N'tungdan2120@gmail.com', N'123', NULL, NULL)
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
ALTER TABLE [dbo].[tChiTietHoaDonBan]  WITH CHECK ADD  CONSTRAINT [FK_tChiTietHoaDonBan_tChiTietSanPham] FOREIGN KEY([MaChiTietSP])
REFERENCES [dbo].[tChiTietSanPham] ([MaChiTietSP])
GO
ALTER TABLE [dbo].[tChiTietHoaDonBan] CHECK CONSTRAINT [FK_tChiTietHoaDonBan_tChiTietSanPham]
GO
ALTER TABLE [dbo].[tChiTietHoaDonBan]  WITH CHECK ADD  CONSTRAINT [FK_tChiTietHoaDonBan_tHoaDonBan] FOREIGN KEY([MaHoaDonBan])
REFERENCES [dbo].[tHoaDonBan] ([MaHoaDonBan])
GO
ALTER TABLE [dbo].[tChiTietHoaDonBan] CHECK CONSTRAINT [FK_tChiTietHoaDonBan_tHoaDonBan]
GO
ALTER TABLE [dbo].[tDanhGia]  WITH CHECK ADD  CONSTRAINT [FK_tDanhGia_tDanhMucSP] FOREIGN KEY([MaSP])
REFERENCES [dbo].[tDanhMucSP] ([MaSP])
GO
ALTER TABLE [dbo].[tDanhGia] CHECK CONSTRAINT [FK_tDanhGia_tDanhMucSP]
GO
ALTER TABLE [dbo].[tDanhGia]  WITH CHECK ADD  CONSTRAINT [FK_tDanhGia_tKhachHang] FOREIGN KEY([MaKhachHang])
REFERENCES [dbo].[tKhachHang] ([MaKhachHang])
GO
ALTER TABLE [dbo].[tDanhGia] CHECK CONSTRAINT [FK_tDanhGia_tKhachHang]
GO
ALTER TABLE [dbo].[tDanhGia]  WITH CHECK ADD  CONSTRAINT [FK_tDanhGia_tNhanVien] FOREIGN KEY([MaNhanVien])
REFERENCES [dbo].[tNhanVien] ([MaNhanVien])
GO
ALTER TABLE [dbo].[tDanhGia] CHECK CONSTRAINT [FK_tDanhGia_tNhanVien]
GO
ALTER TABLE [dbo].[tDanhMucSP]  WITH CHECK ADD  CONSTRAINT [FK_tDanhMucSP_TTag] FOREIGN KEY([TagId])
REFERENCES [dbo].[TTag] ([TagId])
GO
ALTER TABLE [dbo].[tDanhMucSP] CHECK CONSTRAINT [FK_tDanhMucSP_TTag]
GO
ALTER TABLE [dbo].[TGiaoHang]  WITH CHECK ADD  CONSTRAINT [FK_TGiaoHang_THoaDonBan] FOREIGN KEY([MaHoaDonBan])
REFERENCES [dbo].[tHoaDonBan] ([MaHoaDonBan])
GO
ALTER TABLE [dbo].[TGiaoHang] CHECK CONSTRAINT [FK_TGiaoHang_THoaDonBan]
GO
ALTER TABLE [dbo].[tGioHang]  WITH CHECK ADD FOREIGN KEY([MaChiTietSP])
REFERENCES [dbo].[tChiTietSanPham] ([MaChiTietSP])
GO
ALTER TABLE [dbo].[tGioHang]  WITH CHECK ADD FOREIGN KEY([MaKhachHang])
REFERENCES [dbo].[tKhachHang] ([MaKhachHang])
GO
ALTER TABLE [dbo].[tHoaDonBan]  WITH CHECK ADD  CONSTRAINT [FK_HoaDonBan_KhachHang] FOREIGN KEY([MaKhachHang])
REFERENCES [dbo].[tKhachHang] ([MaKhachHang])
GO
ALTER TABLE [dbo].[tHoaDonBan] CHECK CONSTRAINT [FK_HoaDonBan_KhachHang]
GO
ALTER TABLE [dbo].[tHoaDonBan]  WITH CHECK ADD  CONSTRAINT [FK_tHoaDonBan_tMaGiamGia] FOREIGN KEY([MaGiamGia])
REFERENCES [dbo].[tMaGiamGia] ([MaGiamGia])
GO
ALTER TABLE [dbo].[tHoaDonBan] CHECK CONSTRAINT [FK_tHoaDonBan_tMaGiamGia]
GO
ALTER TABLE [dbo].[tKhachHang]  WITH CHECK ADD  CONSTRAINT [FK_KhachHang_User] FOREIGN KEY([Email])
REFERENCES [dbo].[tUser] ([Email])
GO
ALTER TABLE [dbo].[tKhachHang] CHECK CONSTRAINT [FK_KhachHang_User]
GO
ALTER TABLE [dbo].[tNhanVien]  WITH CHECK ADD  CONSTRAINT [FK_tNhanVien_tUser] FOREIGN KEY([Email])
REFERENCES [dbo].[tUser] ([Email])
GO
ALTER TABLE [dbo].[tNhanVien] CHECK CONSTRAINT [FK_tNhanVien_tUser]
GO
ALTER TABLE [dbo].[tPhanHoi]  WITH CHECK ADD  CONSTRAINT [FK_tPhanHoi_tDanhGia] FOREIGN KEY([MaDanhGia])
REFERENCES [dbo].[tDanhGia] ([MaDanhGia])
GO
ALTER TABLE [dbo].[tPhanHoi] CHECK CONSTRAINT [FK_tPhanHoi_tDanhGia]
GO
ALTER TABLE [dbo].[tPhanHoi]  WITH CHECK ADD  CONSTRAINT [FK_tPhanHoi_tKhachHang] FOREIGN KEY([MaKhachHang])
REFERENCES [dbo].[tKhachHang] ([MaKhachHang])
GO
ALTER TABLE [dbo].[tPhanHoi] CHECK CONSTRAINT [FK_tPhanHoi_tKhachHang]
GO
USE [master]
GO
ALTER DATABASE [QLBanDoThoiTrang] SET  READ_WRITE 
GO

