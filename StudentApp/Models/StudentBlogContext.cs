using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StudentApp.Models;

public partial class StudentBlogContext : DbContext
{
    public StudentBlogContext()
    {
    }

    public StudentBlogContext(DbContextOptions<StudentBlogContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<DocumentCategory> DocumentCategories { get; set; }

    public virtual DbSet<DocumentCategoryView> DocumentCategoryViews { get; set; }

    public virtual DbSet<UserList> UserLists { get; set; }

    public virtual DbSet<UserMessage> UserMessages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=conn");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.DocId).HasName("PK__Document__3EF188ADD8BFD505");

            entity.HasOne(d => d.DocCategory).WithMany(p => p.Documents)
                .HasForeignKey(d => d.DocCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Documents__DocCa__3C69FB99");
        });

        modelBuilder.Entity<DocumentCategory>(entity =>
        {
            entity.HasKey(e => e.DocCategoryId).HasName("PK__Document__3A134CB95C87146B");

            entity.ToTable("DocumentCategory");

            entity.HasIndex(e => e.CategoryName, "UQ__Document__8517B2E0696ED868").IsUnique();

            entity.Property(e => e.CategoryName).HasMaxLength(100);
        });

        modelBuilder.Entity<DocumentCategoryView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("DocumentCategoryView");

            entity.Property(e => e.CategoryName).HasMaxLength(100);
        });

        modelBuilder.Entity<UserList>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__UserList__1788CC4CDF5A9738");

            entity.ToTable("UserList");

            entity.Property(e => e.UserAddress).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);
            entity.Property(e => e.UserPassword).HasMaxLength(40);
            entity.Property(e => e.UserPhone).HasMaxLength(50);
            entity.Property(e => e.UserRoleType).HasMaxLength(50);
        });

        modelBuilder.Entity<UserMessage>(entity =>
        {
            entity.HasKey(e => e.MsgId).HasName("PK__UserMess__6623587221F1029B");

            entity.Property(e => e.Uaddress)
                .HasMaxLength(50)
                .HasColumnName("UAddress");
            entity.Property(e => e.Umsg)
                .HasMaxLength(50)
                .HasColumnName("UMsg");
            entity.Property(e => e.Uname)
                .HasMaxLength(50)
                .HasColumnName("UName");
            entity.Property(e => e.Uphone)
                .HasMaxLength(50)
                .HasColumnName("UPhone");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
