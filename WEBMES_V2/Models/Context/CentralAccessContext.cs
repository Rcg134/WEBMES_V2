using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WEBMES_V2.Models.DomainModels.Login;

namespace WEBMES_V2.Models.Context;

public partial class CentralAccessContext : DbContext
{
    public CentralAccessContext()
    {
    }

    public CentralAccessContext(DbContextOptions<CentralAccessContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {


        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserCode).HasName("PK_UserMaster");

            entity.Property(e => e.Active).HasDefaultValueSql("((1))");
            entity.Property(e => e.DateModified).HasColumnType("datetime");
            entity.Property(e => e.DateRegistered)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DateRegistrationVerified).HasColumnType("datetime");
            entity.Property(e => e.EmailAddress).HasMaxLength(100);
            entity.Property(e => e.EmpNo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Emp_no");
            entity.Property(e => e.FullName).HasMaxLength(150);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Remarks).HasMaxLength(100);
            entity.Property(e => e.Username).HasMaxLength(50);
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
