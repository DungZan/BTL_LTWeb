USE [master]
GO
/****** Object:  Database [QLBanDoThoiTrang]    Script Date: 21/10/2024 04:35:18 PM ******/
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
/****** Object:  Table [dbo].[tAnhChiTietSP]    Script Date: 21/10/2024 04:35:18 PM ******/
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
/****** Object:  Table [dbo].[tAnhSP]    Script Date: 21/10/2024 04:35:18 PM ******/
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
/****** Object:  Table [dbo].[tChiTietHoaDonBan]    Script Date: 21/10/2024 04:35:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tChiTietHoaDonBan](
	[MaHoaDonBan] [int] NOT NULL,
	[MaSP] [int] NOT NULL,
	[DonGiaBan] [decimal](18, 2) NULL,
	[SoLuongBan] [int] NULL,
 CONSTRAINT [PK__tChiTiet__6A50CA8AF98C3478] PRIMARY KEY CLUSTERED 
(
	[MaHoaDonBan] ASC,
	[MaSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tChiTietSanPham]    Script Date: 21/10/2024 04:35:18 PM ******/
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
/****** Object:  Table [dbo].[tDanhMucSP]    Script Date: 21/10/2024 04:35:18 PM ******/
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
PRIMARY KEY CLUSTERED 
(
	[MaSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TDanhSachCuaHang]    Script Date: 21/10/2024 04:35:18 PM ******/
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
/****** Object:  Table [dbo].[tGioHang]    Script Date: 21/10/2024 04:35:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tGioHang](
	[Email] [nvarchar](50) NOT NULL,
	[MaChiTietSP] [int] NOT NULL,
	[SoLuong] [int] NULL,
 CONSTRAINT [PK_tGioHang] PRIMARY KEY CLUSTERED 
(
	[Email] ASC,
	[MaChiTietSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tHoaDonBan]    Script Date: 21/10/2024 04:35:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tHoaDonBan](
	[MaHoaDonBan] [int] IDENTITY(1,1) NOT NULL,
	[NgayHoaDon] [datetime] NULL,
	[MaKhachHang] [int] NULL,
	[MaNhanVien] [int] NULL,
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
/****** Object:  Table [dbo].[tKhachHang]    Script Date: 21/10/2024 04:35:18 PM ******/
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
/****** Object:  Table [dbo].[tMaGiamGia]    Script Date: 21/10/2024 04:35:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tMaGiamGia](
	[MaGiamGia] [int] NOT NULL,
	[Code] [varchar](50) NULL,
	[TiLeGiam] [decimal](3, 2) NULL,
	[MoTa] [nvarchar](max) NULL,
	[NgayBatDau] [date] NULL,
	[NgayKetThuc] [date] NULL,
	[TrangThai] [int] NULL,
 CONSTRAINT [PK_tMaGiamGia] PRIMARY KEY CLUSTERED 
(
	[MaGiamGia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tNhanVien]    Script Date: 21/10/2024 04:35:18 PM ******/
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
/****** Object:  Table [dbo].[tUser]    Script Date: 21/10/2024 04:35:18 PM ******/
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
SET IDENTITY_INSERT [dbo].[tKhachHang] ON 

INSERT [dbo].[tKhachHang] ([MaKhachHang], [Email], [TenKhachHang], [NgaySinh], [SoDienThoai], [DiaChi], [GhiChu]) VALUES (1, N'linhlinhpham2104@gmail.com', N'Đỗ Thị Thùy Linh', CAST(N'2004-12-21' AS Date), N'0359214377', N'Hưng yên', NULL)
INSERT [dbo].[tKhachHang] ([MaKhachHang], [Email], [TenKhachHang], [NgaySinh], [SoDienThoai], [DiaChi], [GhiChu]) VALUES (2, N'truongvan.minh1504@gmail.com', N'Trương Văn Minh', CAST(N'2004-05-10' AS Date), N'0373294997', N'Xóm 10, quỳnh lâm quỳnh lưu nghệ an', NULL)
SET IDENTITY_INSERT [dbo].[tKhachHang] OFF
GO
INSERT [dbo].[tUser] ([Email], [password], [Salt], [LoaiUser]) VALUES (N'linhlinhpham2104@gmail.com', N'e9598abadd8f1a31655681da50c6c1a8de391a4e6a79929ea8511c62f9821f24', N'MoXfw2hz9EMDtVMUSTlR8qBed2XeSQB+2I9a6M2xldQ=', N'KhachHang')
INSERT [dbo].[tUser] ([Email], [password], [Salt], [LoaiUser]) VALUES (N'truongvan.minh1504@gmail.com', N'de1b4e5554bf248a8310d759e452dfb93d05ccf9cb6b40f79bdd3f6fab7baa04', N'+XubEyAW4QCF8XLn1dOHG9eKi3sA4PK8uCAkVssLwNs=', N'KhachHang')
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
ALTER TABLE [dbo].[tChiTietHoaDonBan]  WITH CHECK ADD  CONSTRAINT [FK_tChiTietHoaDonBan_tDanhMucSP] FOREIGN KEY([MaSP])
REFERENCES [dbo].[tDanhMucSP] ([MaSP])
GO
ALTER TABLE [dbo].[tChiTietHoaDonBan] CHECK CONSTRAINT [FK_tChiTietHoaDonBan_tDanhMucSP]
GO
ALTER TABLE [dbo].[tChiTietHoaDonBan]  WITH CHECK ADD  CONSTRAINT [FK_tChiTietHoaDonBan_tHoaDonBan] FOREIGN KEY([MaHoaDonBan])
REFERENCES [dbo].[tHoaDonBan] ([MaHoaDonBan])
GO
ALTER TABLE [dbo].[tChiTietHoaDonBan] CHECK CONSTRAINT [FK_tChiTietHoaDonBan_tHoaDonBan]
GO
ALTER TABLE [dbo].[tChiTietSanPham]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietSP_DanhMucSP] FOREIGN KEY([MaSP])
REFERENCES [dbo].[tDanhMucSP] ([MaSP])
GO
ALTER TABLE [dbo].[tChiTietSanPham] CHECK CONSTRAINT [FK_ChiTietSP_DanhMucSP]
GO
ALTER TABLE [dbo].[tGioHang]  WITH CHECK ADD  CONSTRAINT [FK_tGioHang_tChiTietSanPham] FOREIGN KEY([MaChiTietSP])
REFERENCES [dbo].[tChiTietSanPham] ([MaChiTietSP])
GO
ALTER TABLE [dbo].[tGioHang] CHECK CONSTRAINT [FK_tGioHang_tChiTietSanPham]
GO
ALTER TABLE [dbo].[tGioHang]  WITH CHECK ADD  CONSTRAINT [FK_tGioHang_tUser] FOREIGN KEY([Email])
REFERENCES [dbo].[tUser] ([Email])
GO
ALTER TABLE [dbo].[tGioHang] CHECK CONSTRAINT [FK_tGioHang_tUser]
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
USE [master]
GO
ALTER DATABASE [QLBanDoThoiTrang] SET  READ_WRITE 
GO
