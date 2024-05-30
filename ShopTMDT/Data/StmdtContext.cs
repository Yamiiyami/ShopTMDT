using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ShopTMDT.Data;

public partial class StmdtContext : DbContext
{
    public StmdtContext()
    {
    }

    public StmdtContext(DbContextOptions<StmdtContext> options)
        : base(options)
    {
    }

    public virtual DbSet<HangHoa> HangHoas { get; set; }

    public virtual DbSet<HinhAnh> HinhAnhs { get; set; }

    public virtual DbSet<KhuyenMai> KhuyenMais { get; set; }

    public virtual DbSet<LoaiHangHoa> LoaiHangHoas { get; set; }

    public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }

    public virtual DbSet<NhapHangHoa> NhapHangHoas { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Slide> Slides { get; set; }

    public virtual DbSet<ThongTinDonNhap> ThongTinDonNhaps { get; set; }

    public virtual DbSet<ThongTinXuat> ThongTinXuats { get; set; }

    public virtual DbSet<TrangThaiThanhToan> TrangThaiThanhToans { get; set; }

    public virtual DbSet<TrangThaiVanTruyen> TrangThaiVanTruyens { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<XuatHangHoa> XuatHangHoas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-FP6OH6IS;Initial Catalog=STMDT;Integrated Security=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HangHoa>(entity =>
        {
            entity.HasKey(e => e.IdHangHoa);

            entity.ToTable("HangHoa");

            entity.HasIndex(e => e.IdKhuyenMai, "IX_HangHoa_IdKhuyenMai");

            entity.HasIndex(e => e.IdLoaiHangHoa, "IX_HangHoa_IdLoaiHangHoa");

            entity.Property(e => e.Gia).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.HinhAnh).HasMaxLength(200);
            entity.Property(e => e.MauSac).HasMaxLength(100);
            entity.Property(e => e.MoTa).HasColumnType("ntext");
            entity.Property(e => e.Size)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TenHangHoa).HasMaxLength(200);

            entity.HasOne(d => d.IdKhuyenMaiNavigation).WithMany(p => p.HangHoas)
                .HasForeignKey(d => d.IdKhuyenMai)
                .HasConstraintName("FK_HangHoa_KhuyenMai");

            entity.HasOne(d => d.IdLoaiHangHoaNavigation).WithMany(p => p.HangHoas)
                .HasForeignKey(d => d.IdLoaiHangHoa)
                .HasConstraintName("FK_HangHoa_LoaiHangHoa");
        });

        modelBuilder.Entity<HinhAnh>(entity =>
        {
            entity.HasKey(e => e.IdHinhAnh);

            entity.ToTable("HinhAnh");

            entity.HasIndex(e => e.IdHangHoa, "IX_HinhAnh_IdHangHoa");

            entity.Property(e => e.HinhAnh1)
                .HasMaxLength(200)
                .HasColumnName("HinhAnh");
            entity.Property(e => e.TenHinhAnh).HasMaxLength(100);

            entity.HasOne(d => d.IdHangHoaNavigation).WithMany(p => p.HinhAnhs)
                .HasForeignKey(d => d.IdHangHoa)
                .HasConstraintName("FK_HinhAnh_HangHoa");
        });

        modelBuilder.Entity<KhuyenMai>(entity =>
        {
            entity.HasKey(e => e.IdKhuyenMai);

            entity.ToTable("KhuyenMai");

            entity.Property(e => e.GiaKhuyenMai).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.MoTa).HasMaxLength(200);
            entity.Property(e => e.TenKhuyenMai).HasMaxLength(200);
        });

        modelBuilder.Entity<LoaiHangHoa>(entity =>
        {
            entity.HasKey(e => e.IdLoaiHangHoa);

            entity.ToTable("LoaiHangHoa");

            entity.HasIndex(e => e.IdKhuyenMai, "IX_LoaiHangHoa_IdKhuyenMai");

            entity.Property(e => e.TenLoai).HasMaxLength(150);

            entity.HasOne(d => d.IdKhuyenMaiNavigation).WithMany(p => p.LoaiHangHoas)
                .HasForeignKey(d => d.IdKhuyenMai)
                .HasConstraintName("FK_LoaiHangHoa_KhuyenMai");
        });

        modelBuilder.Entity<NhaCungCap>(entity =>
        {
            entity.HasKey(e => e.IdNhaCungCap);

            entity.ToTable("NhaCungCap");

            entity.Property(e => e.DiaChi).HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.NgayHopTac).HasColumnType("datetime");
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Ten).HasMaxLength(150);
        });

        modelBuilder.Entity<NhapHangHoa>(entity =>
        {
            entity.HasKey(e => e.IdNhapHangHoa);

            entity.ToTable("NhapHangHoa");

            entity.HasIndex(e => e.IdNhaCungCap, "IX_NhapHangHoa_IdNhaCungCap");

            entity.HasIndex(e => e.IdUser, "IX_NhapHangHoa_IdUser");

            entity.Property(e => e.NgayTao).HasColumnType("datetime");

            entity.HasOne(d => d.IdNhaCungCapNavigation).WithMany(p => p.NhapHangHoas)
                .HasForeignKey(d => d.IdNhaCungCap)
                .HasConstraintName("FK_NhapHangHoa_NhaCungCap");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.NhapHangHoas)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_NhapHangHoa_NguoiDung");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.IdRating);

            entity.ToTable("Rating");

            entity.HasIndex(e => e.IdHangHoa, "IX_Rating_IdHangHoa");

            entity.HasIndex(e => e.IdUser, "IX_Rating_IdUser");

            entity.Property(e => e.DanhGia).HasColumnType("text");
            entity.Property(e => e.NgayTao).HasColumnType("datetime");

            entity.HasOne(d => d.IdHangHoaNavigation).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.IdHangHoa)
                .HasConstraintName("FK_Rating_HangHoa");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_Rating_NguoiDung");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<Slide>(entity =>
        {
            entity.HasKey(e => e.IdSlide);

            entity.ToTable("Slide");

            entity.Property(e => e.Anh).HasMaxLength(150);
            entity.Property(e => e.Link)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Ten).HasMaxLength(100);
        });

        modelBuilder.Entity<ThongTinDonNhap>(entity =>
        {
            entity.HasKey(e => e.IdThongTinDonNhap);

            entity.ToTable("ThongTinDonNhap");

            entity.HasIndex(e => e.IdHangHoa, "IX_ThongTinDonNhap_IdHangHoa");

            entity.HasIndex(e => e.IdNhapHangHoa, "IX_ThongTinDonNhap_IdNhapHangHoa");

            entity.Property(e => e.Gia).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.TongGia).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.IdHangHoaNavigation).WithMany(p => p.ThongTinDonNhaps)
                .HasForeignKey(d => d.IdHangHoa)
                .HasConstraintName("FK_ThongTinDonNhap_HangHoa");

            entity.HasOne(d => d.IdNhapHangHoaNavigation).WithMany(p => p.ThongTinDonNhaps)
                .HasForeignKey(d => d.IdNhapHangHoa)
                .HasConstraintName("FK_ThongTinDonNhap_NhapHangHoa");
        });

        modelBuilder.Entity<ThongTinXuat>(entity =>
        {
            entity.HasKey(e => e.IdThongTinXuat);

            entity.ToTable("ThongTinXuat");

            entity.HasIndex(e => e.IdHangHoa, "IX_ThongTinXuat_IdHangHoa");

            entity.HasIndex(e => e.IdKhuyenMai, "IX_ThongTinXuat_IdKhuyenMai");

            entity.HasIndex(e => e.IdXuatHangHoa, "IX_ThongTinXuat_IdXuatHangHoa");

            entity.Property(e => e.Gia).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.TongGia).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.IdHangHoaNavigation).WithMany(p => p.ThongTinXuats)
                .HasForeignKey(d => d.IdHangHoa)
                .HasConstraintName("FK_ThongTinXuat_HangHoa");

            entity.HasOne(d => d.IdKhuyenMaiNavigation).WithMany(p => p.ThongTinXuats)
                .HasForeignKey(d => d.IdKhuyenMai)
                .HasConstraintName("FK_ThongTinXuat_KhuyenMai");

            entity.HasOne(d => d.IdXuatHangHoaNavigation).WithMany(p => p.ThongTinXuats)
                .HasForeignKey(d => d.IdXuatHangHoa)
                .HasConstraintName("FK_ThongTinXuat_XuatHangHoa1");
        });

        modelBuilder.Entity<TrangThaiThanhToan>(entity =>
        {
            entity.HasKey(e => e.IdThanhToan);

            entity.ToTable("TrangThaiThanhToan");

            entity.Property(e => e.Mota).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<TrangThaiVanTruyen>(entity =>
        {
            entity.HasKey(e => e.IdVanTruyen);

            entity.Property(e => e.Mota).HasMaxLength(200);
            entity.Property(e => e.StatusName).HasMaxLength(100);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Anh)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.Qrcode)
                .HasMaxLength(200)
                .HasColumnName("QRCode");
            entity.Property(e => e.RoleId).HasMaxLength(450);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_Users_Roles");
        });

        modelBuilder.Entity<XuatHangHoa>(entity =>
        {
            entity.HasKey(e => e.IdHoaDon);

            entity.ToTable("XuatHangHoa");

            entity.HasIndex(e => e.IdThanhToan, "IX_XuatHangHoa_IdThanhToan");

            entity.HasIndex(e => e.IdUser, "IX_XuatHangHoa_IdUser");

            entity.HasIndex(e => e.IdVanChuyen, "IX_XuatHangHoa_IdVanChuyen");

            entity.Property(e => e.DiaChi).HasColumnType("ntext");
            entity.Property(e => e.GhiChu).HasColumnType("ntext");
            entity.Property(e => e.NgayXuat).HasColumnType("datetime");
            entity.Property(e => e.Phone).HasMaxLength(10);

            entity.HasOne(d => d.IdThanhToanNavigation).WithMany(p => p.XuatHangHoas)
                .HasForeignKey(d => d.IdThanhToan)
                .HasConstraintName("FK_XuatHangHoa_ThanhToan");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.XuatHangHoas)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_XuatHangHoa_NguoiDung");

            entity.HasOne(d => d.IdVanChuyenNavigation).WithMany(p => p.XuatHangHoas)
                .HasForeignKey(d => d.IdVanChuyen)
                .HasConstraintName("FK_XuatHangHoa_vanchuyen");
        });
        modelBuilder.HasSequence<int>("RandomSequence").StartsAt(1000L);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
