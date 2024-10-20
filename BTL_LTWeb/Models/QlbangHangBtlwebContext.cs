using System;
using System.Collections.Generic;
using System.Drawing;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BTL_LTWeb.Models;

public partial class QlbangHangBtlwebContext : DbContext
{
    public QlbangHangBtlwebContext()
    {
    }

    public QlbangHangBtlwebContext(DbContextOptions<QlbangHangBtlwebContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TAnhChiTietSp> TAnhChiTietSps { get; set; }

    public virtual DbSet<TAnhSp> TAnhSps { get; set; }

    public virtual DbSet<TChiTietSanPham> TChiTietSanPhams { get; set; }

    public virtual DbSet<TDanhMucSp> TDanhMucSps { get; set; }

    public virtual DbSet<THoaDonBan> THoaDonBans { get; set; }

    public virtual DbSet<TKhachHang> TKhachHangs { get; set; }

    public virtual DbSet<TNhanVien> TNhanViens { get; set; }

    public virtual DbSet<TUser> TUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-PIULBJ0\\SQLEXPRESS01;Initial Catalog=QLBangHangBTLWeb;Integrated Security=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TAnhChiTietSp>(entity =>
        {
            entity.HasKey(e => new { e.MaChiTietSp, e.TenFileAnh }).HasName("PK__tAnhChiT__6DFA63355C4D9532");

            entity.ToTable("tAnhChiTietSP");

            entity.Property(e => e.MaChiTietSp).HasColumnName("MaChiTietSP");
            entity.Property(e => e.TenFileAnh).HasMaxLength(255);
            entity.Property(e => e.ViTri).HasMaxLength(255);

            entity.HasOne(d => d.MaChiTietSpNavigation).WithMany(p => p.TAnhChiTietSps)
                .HasForeignKey(d => d.MaChiTietSp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AnhChiTietSP_ChiTietSP");
        });

        modelBuilder.Entity<TAnhSp>(entity =>
        {
            entity.HasKey(e => new { e.MaSp, e.TenFileAnh }).HasName("PK__tAnhSP__2FC2FB7E27DAAE4B");

            entity.ToTable("tAnhSP");

            entity.Property(e => e.MaSp).HasColumnName("MaSP");
            entity.Property(e => e.TenFileAnh).HasMaxLength(255);
            entity.Property(e => e.ViTri).HasMaxLength(255);

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.TAnhSps)
                .HasForeignKey(d => d.MaSp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AnhSP_DanhMucSP");
        });

        modelBuilder.Entity<TChiTietSanPham>(entity =>
        {
            entity.HasKey(e => e.MaChiTietSp).HasName("PK__tChiTiet__651D9057021AE26D");

            entity.ToTable("tChiTietSanPham");

            entity.Property(e => e.MaChiTietSp).HasColumnName("MaChiTietSP");
            entity.Property(e => e.AnhDaiDien).HasMaxLength(255);
            entity.Property(e => e.DonGiaBan).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.KichThuoc).HasMaxLength(50);
            entity.Property(e => e.MaSp).HasColumnName("MaSP");
            entity.Property(e => e.MauSac).HasMaxLength(50);
            entity.Property(e => e.Slton).HasColumnName("SLTon");

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.TChiTietSanPhams)
                .HasForeignKey(d => d.MaSp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietSP_DanhMucSP");
        });

        modelBuilder.Entity<TDanhMucSp>(entity =>
        {
            entity.HasKey(e => e.MaSp).HasName("PK__tDanhMuc__2725081C34ED1B0E");

            entity.ToTable("tDanhMucSP");

            entity.Property(e => e.MaSp).HasColumnName("MaSP");
            entity.Property(e => e.AnhDaiDien).HasMaxLength(255);
            entity.Property(e => e.ChatLieu).HasMaxLength(100);
            entity.Property(e => e.GiaLonNhat).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.GiaNhoNhat).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.GioiThieuSp).HasColumnName("GioiThieuSP");
            entity.Property(e => e.HangSx)
                .HasMaxLength(100)
                .HasColumnName("HangSX");
            entity.Property(e => e.LoaiDt)
                .HasMaxLength(100)
                .HasColumnName("LoaiDT");
            entity.Property(e => e.TenSp)
                .HasMaxLength(100)
                .HasColumnName("TenSP");
        });

        modelBuilder.Entity<THoaDonBan>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tHoaDonBan");

            entity.Property(e => e.DonGiaBan).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MaSp).HasColumnName("MaSP");
            entity.Property(e => e.PhuongThucThanhToan).HasMaxLength(100);
            entity.Property(e => e.TongTienHd)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("TongTienHD");

            entity.HasOne(d => d.MaKhachHangNavigation).WithMany()
                .HasForeignKey(d => d.MaKhachHang)
                .HasConstraintName("FK_HoaDonBan_KhachHang");

            entity.HasOne(d => d.MaNhanVienNavigation).WithMany()
                .HasForeignKey(d => d.MaNhanVien)
                .HasConstraintName("FK_HoaDonBan_NhanVien");

            entity.HasOne(d => d.MaSpNavigation).WithMany()
                .HasForeignKey(d => d.MaSp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HoaDonBan_DanhMucSP");
        });

        modelBuilder.Entity<TKhachHang>(entity =>
        {
            entity.HasKey(e => e.MaKhachHang).HasName("PK__tKhachHa__88D2F0E5693BB92D");

            entity.ToTable("tKhachHang");

            entity.Property(e => e.DiaChi).HasMaxLength(255);
            entity.Property(e => e.SoDienThoai).HasMaxLength(20);
            entity.Property(e => e.TenKhachHang).HasMaxLength(100);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.TKhachHangs)
                .HasForeignKey(d => d.Username)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_KhachHang_User");
        });

        modelBuilder.Entity<TNhanVien>(entity =>
        {
            entity.HasKey(e => e.MaNhanVien).HasName("PK__tNhanVie__77B2CA474377FBA4");

            entity.ToTable("tNhanVien");

            entity.Property(e => e.ChucVu).HasMaxLength(50);
            entity.Property(e => e.TenNhanVien).HasMaxLength(100);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.TNhanViens)
                .HasForeignKey(d => d.Username)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tNhanVien_tUser");
        });

        modelBuilder.Entity<TUser>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("PK__tUser__F3DBC5736648EC50");

            entity.ToTable("tUser");

            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
            entity.Property(e => e.LoaiUser).HasMaxLength(50);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
