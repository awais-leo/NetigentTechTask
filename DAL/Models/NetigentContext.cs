using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

public partial class NetigentContext : DbContext
{
    public NetigentContext()
    {
    }

    public NetigentContext(DbContextOptions<NetigentContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<Inqury> Inquries { get; set; }

    public virtual DbSet<StatusLevel> StatusLevels { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=AWAIS-PC\\SQLEXPRESS;Initial Catalog=Netigent;Integrated Security=True;TrustServerCertificate=True;Pooling=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

        modelBuilder.Entity<Application>(entity =>
        {
            entity.ToTable("Application", "gov");

            entity.Property(e => e.AppStatus).HasMaxLength(250);
            entity.Property(e => e.CompletedDt).HasColumnType("datetime");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.Modified).HasColumnType("datetime");
            entity.Property(e => e.OpenDt).HasColumnType("datetime");
            entity.Property(e => e.ProjectLocation).HasMaxLength(250);
            entity.Property(e => e.ProjectName).HasMaxLength(512);
            entity.Property(e => e.ProjectRef).HasMaxLength(250);
            entity.Property(e => e.ProjectValue).HasColumnType("money");
            entity.Property(e => e.StartDt).HasColumnType("datetime");

            entity.HasOne(d => d.Status).WithMany(p => p.Applications)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Application_StatusLevel");
        });

        modelBuilder.Entity<Inqury>(entity =>
        {
            entity.ToTable("Inquries", "gov");

            entity.Property(e => e.AskedDt).HasColumnType("datetime");
            entity.Property(e => e.CompletedDt).HasColumnType("datetime");
            entity.Property(e => e.Inquiry).HasMaxLength(4000);
            entity.Property(e => e.Response).HasMaxLength(4000);
            entity.Property(e => e.SendToPerson).HasMaxLength(100);
            entity.Property(e => e.SendToRole).HasMaxLength(100);
            entity.Property(e => e.Subject).HasMaxLength(200);

            entity.HasOne(d => d.Application).WithMany(p => p.Inquries)
                .HasForeignKey(d => d.ApplicationId)
                .HasConstraintName("FK_Inquries_Application");
        });

        modelBuilder.Entity<StatusLevel>(entity =>
        {
            entity.ToTable("StatusLevel");

            entity.Property(e => e.StatusName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
