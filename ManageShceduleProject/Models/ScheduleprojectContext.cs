using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ManageShceduleProject.Models;

public partial class ScheduleprojectContext : DbContext
{
    public ScheduleprojectContext()
    {
    }

    public ScheduleprojectContext(DbContextOptions<ScheduleprojectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<LeaveRequest> LeaveRequests { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var ConnectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(ConnectionString);
        }

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LeaveRequest>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__LeaveReq__33A8517A69F84833");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ProcessedBy).HasMaxLength(50);
            entity.Property(e => e.Reason).HasMaxLength(500);
            entity.Property(e => e.Response).HasMaxLength(500);
            entity.Property(e => e.SenderId).HasColumnName("SenderID");
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.Sender).WithMany(p => p.LeaveRequests)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LeaveRequ__Sende__3B75D760");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__760965CC65843584");

            entity.HasIndex(e => e.RoleName, "UQ__Roles__783254B1ED3F7F2F").IsUnique();

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.TeamId).HasName("PK__Teams__123AE79902F12A16");

            entity.HasOne(d => d.Leader).WithMany(p => p.TeamLeaders)
                .HasForeignKey(d => d.LeaderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Teams__LeaderId__412EB0B6");

            entity.HasOne(d => d.Member).WithMany(p => p.TeamMembers)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Teams__MemberId__4222D4EF");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C14D6AD82");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E40AEE9718").IsUnique();

            entity.Property(e => e.Department).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
