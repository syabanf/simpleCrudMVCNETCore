using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Test.Models.DB
{
    public partial class DBTestContext : DbContext
    {
        public DBTestContext()
        {
        }

        public DBTestContext(DbContextOptions<DBTestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Tbcourier> Tbcouriers { get; set; }
        public virtual DbSet<Tbinvoice> Tbinvoices { get; set; }
        public virtual DbSet<TbinvoiceDetail> TbinvoiceDetails { get; set; }
        public virtual DbSet<Tbpayment> Tbpayments { get; set; }
        public virtual DbSet<Tbsale> Tbsales { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Tbcourier>(entity =>
            {
                entity.HasKey(e => e.Guidcourier);

                entity.ToTable("TBCourier");

                entity.Property(e => e.Guidcourier)
                    .ValueGeneratedNever()
                    .HasColumnName("GUIDCourier");
            });

            modelBuilder.Entity<Tbinvoice>(entity =>
            {
                entity.HasKey(e => e.Guidinvoice);

                entity.ToTable("TBInvoice");

                entity.Property(e => e.Guidinvoice)
                    .ValueGeneratedNever()
                    .HasColumnName("GUIDInvoice");

                entity.Property(e => e.Guidcourier).HasColumnName("GUIDCourier");

                entity.Property(e => e.Guidpayment).HasColumnName("GUIDPayment");

                entity.Property(e => e.Guidsales).HasColumnName("GUIDSales");

                entity.Property(e => e.InvoiceDate).HasColumnType("datetime");

                entity.Property(e => e.ShipTo).HasMaxLength(256);

                entity.Property(e => e.TargetTo).HasMaxLength(256);

                entity.HasOne(d => d.GuidcourierNavigation)
                    .WithMany(p => p.Tbinvoices)
                    .HasForeignKey(d => d.Guidcourier)
                    .HasConstraintName("FK_Courier");

                entity.HasOne(d => d.GuidpaymentNavigation)
                    .WithMany(p => p.Tbinvoices)
                    .HasForeignKey(d => d.Guidpayment)
                    .HasConstraintName("FK_Payment");

                entity.HasOne(d => d.GuidsalesNavigation)
                .WithMany(p => p.Tbinvoices)
                .HasForeignKey(d => d.Guidsales)
                .HasConstraintName("FK_SALES");
            });

            modelBuilder.Entity<TbinvoiceDetail>(entity =>
            {
                entity.HasKey(e => e.GuidinvoiceDetail);

                entity.ToTable("TBInvoiceDetail");

                entity.Property(e => e.GuidinvoiceDetail)
                    .ValueGeneratedNever()
                    .HasColumnName("GUIDInvoiceDetail");

                entity.Property(e => e.Guidinvoice).HasColumnName("GUIDInvoice");

                entity.Property(e => e.Item)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.GuidinvoiceNavigation)
                    .WithMany(p => p.TbinvoiceDetails)
                    .HasForeignKey(d => d.Guidinvoice)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Invoice");
            });

            modelBuilder.Entity<Tbpayment>(entity =>
            {
                entity.HasKey(e => e.Guidpayment);

                entity.ToTable("TBPayment");

                entity.Property(e => e.Guidpayment)
                    .ValueGeneratedNever()
                    .HasColumnName("GUIDPayment");
            });

            modelBuilder.Entity<Tbsale>(entity =>
            {
                entity.HasKey(e => e.Guidsales);

                entity.ToTable("TBSales");

                entity.Property(e => e.Guidsales)
                    .ValueGeneratedNever()
                    .HasColumnName("GUIDSales");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
