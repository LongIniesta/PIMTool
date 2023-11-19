using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PIMTool.Core.Domain.Entities;

namespace PIMTool.Database
{
    public partial class PIMDBContext : DbContext
    {
        public PIMDBContext()
        {
        }

        public PIMDBContext(DbContextOptions<PIMDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Group> Groups { get; set; } = null!;
        public virtual DbSet<Project> Projects { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(local);Uid=sa;Pwd=12345;Database=PIMDB");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.Id)
                    .HasColumnType("decimal(19, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Version)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.Visa)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("Group");

                entity.Property(e => e.Id)
                    .HasColumnType("decimal(19, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.GroupLeaderId).HasColumnType("decimal(19, 0)");

                entity.Property(e => e.Version)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.GroupLeader)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.GroupLeaderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Group__GroupLead__398D8EEE");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("Project");

                entity.HasIndex(e => e.ProjectNumber, "UQ__Project__C66B6F6A60B592B6")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("decimal(19, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Customer)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.GroupId).HasColumnType("decimal(19, 0)");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProjectNumber).HasColumnType("decimal(4, 0)");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.Status)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Version)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Project__GroupId__3D5E1FD2");

                entity.HasMany(d => d.Employees)
                    .WithMany(p => p.Projects)
                    .UsingEntity<Dictionary<string, object>>(
                        "ProjectEmployee",
                        l => l.HasOne<Employee>().WithMany().HasForeignKey("EmployeeId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__ProjectEm__Emplo__412EB0B6"),
                        r => r.HasOne<Project>().WithMany().HasForeignKey("ProjectId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__ProjectEm__Proje__403A8C7D"),
                        j =>
                        {
                            j.HasKey("ProjectId", "EmployeeId").HasName("PK__ProjectE__71B7BA016A5D42C3");

                            j.ToTable("ProjectEmployee");

                            j.IndexerProperty<decimal>("ProjectId").HasColumnType("decimal(19, 0)");

                            j.IndexerProperty<decimal>("EmployeeId").HasColumnType("decimal(19, 0)");
                        });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
