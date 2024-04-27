using Flozacode.Models;
using Microsoft.EntityFrameworkCore;
using KasirApi.Repository.Entities;

namespace KasirApi.Repository.Contexts
{
    public partial class AppDbContext : Dbs
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Member> Members { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Transaction> Transactions { get; set; } = null!;
        public virtual DbSet<TransactionDetail> TransactionDetails { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=ep-raspy-mouse-73643979-pooler.ap-southeast-1.aws.neon.tech;Port=5432;Database=verceldb;Username=default;Password=Cd1asulNGhi8;timeout=180");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member>(entity =>
            {
                entity.ToTable("member");

                entity.HasIndex(e => e.MemberNumber, "member_member_number_key")
                    .IsUnique();

                entity.HasIndex(e => e.Nik, "member_nik_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(150)
                    .HasColumnName("address");

                entity.Property(e => e.BirthDate).HasColumnName("birth_date");

                entity.Property(e => e.BirthPlace)
                    .HasMaxLength(50)
                    .HasColumnName("birth_place");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("created_at");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.DataStatusId)
                    .HasColumnName("data_status_id")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Gender)
                    .HasMaxLength(1)
                    .HasColumnName("gender");

                entity.Property(e => e.JoinDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("join_date");

                entity.Property(e => e.MemberNumber)
                    .HasMaxLength(50)
                    .HasColumnName("member_number");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Nik)
                    .HasMaxLength(16)
                    .HasColumnName("nik")
                    .IsFixedLength();

                entity.Property(e => e.Point).HasColumnName("point");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .HasMaxLength(100)
                    .HasColumnName("code");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("created_at");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("name");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Stock).HasColumnName("stock");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .HasColumnName("code");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("created_at");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.DataStatusId)
                    .HasColumnName("data_status_id")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Name)
                    .HasMaxLength(75)
                    .HasColumnName("name");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("transaction");

                entity.HasIndex(e => e.ReferenceNumber, "transaction_reference_number_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("created_at");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.ReferenceNumber)
                    .HasMaxLength(150)
                    .HasColumnName("reference_number");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("transaction_member_id_fkey");
            });

            modelBuilder.Entity<TransactionDetail>(entity =>
            {
                entity.ToTable("transaction_detail");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("created_at");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.Discount).HasColumnName("discount");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Qty).HasColumnName("qty");

                entity.Property(e => e.Total).HasColumnName("total");

                entity.Property(e => e.TransactionId).HasColumnName("transaction_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TransactionDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("transaction_detail_product_id_fkey");

                entity.HasOne(d => d.Transaction)
                    .WithMany(p => p.TransactionDetails)
                    .HasForeignKey(d => d.TransactionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("transaction_detail_transaction_id_fkey");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.Email, "user_email_key")
                    .IsUnique();

                entity.HasIndex(e => e.Nip, "user_nip_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("created_at");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.DataStatusId)
                    .HasColumnName("data_status_id")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(75)
                    .HasColumnName("name");

                entity.Property(e => e.Nip)
                    .HasMaxLength(50)
                    .HasColumnName("nip");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .HasColumnName("password");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_role_id_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
