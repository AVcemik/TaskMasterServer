using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TaskMasterServer.DataBase;

public partial class TaskUserDbContext : DbContext
{
    private readonly string _cifraYandex = "Server=127.0.0.1; Port=6465; Database=TaskUser_db; User Id=cemik; Password=22464427ceM;";
    private readonly string _home = "Server=127.0.0.1; Port=6432; Database=TaskUser_db; User Id=cemik; Password=22464427ceM;";

    public TaskUserDbContext()
    {
    }

    public TaskUserDbContext(DbContextOptions<TaskUserDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attachment> Attachments { get; set; }

    public virtual DbSet<Authorization> Authorizations { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Priority> Priorities { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql(_cifraYandex);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attachment>(entity =>
        {
            entity.HasKey(e => e.AttachmentId).HasName("attachment_pkey");

            entity.ToTable("attachment", "TaskUser");

            entity.HasIndex(e => e.AttachmentPath, "attachment_attachment_path_key").IsUnique();

            entity.Property(e => e.AttachmentId).HasColumnName("attachment_id");
            entity.Property(e => e.AttachmentPath).HasColumnName("attachment_path");
        });

        modelBuilder.Entity<Authorization>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("authorization_pkey");

            entity.ToTable("authorization", "TaskUser");

            entity.HasIndex(e => e.Token, "authorization_token_key").IsUnique();

            entity.Property(e => e.UserId)
                .ValueGeneratedOnAdd()
                .HasColumnName("user_id");
            entity.Property(e => e.Isauthorization).HasColumnName("isauthorization");
            entity.Property(e => e.Token)
                .HasMaxLength(64)
                .HasColumnName("token");

            entity.HasOne(d => d.User).WithOne(p => p.Authorization)
                .HasForeignKey<Authorization>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("authorization_user_id_fkey");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CimmentId).HasName("comment_pkey");

            entity.ToTable("comment", "TaskUser");

            entity.Property(e => e.CimmentId).HasColumnName("cimment_id");
            entity.Property(e => e.Comment1).HasColumnName("comment");
            entity.Property(e => e.TaskId).HasColumnName("task_id");

            entity.HasOne(d => d.Task).WithMany(p => p.Comments)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("comment_task_id_fkey");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("department_pkey");

            entity.ToTable("department", "TaskUser");

            entity.HasIndex(e => e.DepartmentName, "department_department_name_key").IsUnique();

            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(32)
                .HasColumnName("department_name");
        });

        modelBuilder.Entity<Priority>(entity =>
        {
            entity.HasKey(e => e.PriorityId).HasName("priority_pkey");

            entity.ToTable("priority", "TaskUser");

            entity.Property(e => e.PriorityId).HasColumnName("priority_id");
            entity.Property(e => e.PriorityType)
                .HasMaxLength(16)
                .HasColumnName("priority_type");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("status_pkey");

            entity.ToTable("status", "TaskUser");

            entity.HasIndex(e => e.StatusType, "status_status_type_key").IsUnique();

            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.StatusType)
                .HasMaxLength(16)
                .HasColumnName("status_type");
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("task_pkey");

            entity.ToTable("task", "TaskUser");

            entity.Property(e => e.TaskId).HasColumnName("task_id");
            entity.Property(e => e.AttachmentId).HasColumnName("attachment_id");
            entity.Property(e => e.DateCreate).HasColumnName("date_create");
            entity.Property(e => e.Deadline).HasColumnName("deadline");
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.PriorityId).HasColumnName("priority_id");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.TaskName)
                .HasMaxLength(32)
                .HasColumnName("task_name");

            entity.HasOne(d => d.Attachment).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.AttachmentId)
                .HasConstraintName("task_attachment_id_fkey");

            entity.HasOne(d => d.Department).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("task_department_id_fkey");

            entity.HasOne(d => d.Priority).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.PriorityId)
                .HasConstraintName("task_priority_id_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("task_status_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("user_pkey");

            entity.ToTable("user", "TaskUser");

            entity.HasIndex(e => e.Email, "user_email_key").IsUnique();

            entity.HasIndex(e => e.Login, "user_login_key").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.Email)
                .HasMaxLength(64)
                .HasColumnName("email");
            entity.Property(e => e.Isresponsible).HasColumnName("isresponsible");
            entity.Property(e => e.Login)
                .HasMaxLength(16)
                .HasColumnName("login");
            entity.Property(e => e.Password)
                .HasMaxLength(128)
                .HasColumnName("password");
            entity.Property(e => e.UserName)
                .HasMaxLength(32)
                .HasColumnName("user_name");

            entity.HasOne(d => d.Department).WithMany(p => p.Users)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("user_department_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
