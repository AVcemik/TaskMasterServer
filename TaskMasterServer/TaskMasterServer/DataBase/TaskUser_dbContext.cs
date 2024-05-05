using Microsoft.EntityFrameworkCore;
using TaskMasterServer.DataBase.Service;

namespace TaskMasterServer.DataBase
{
    public partial class TaskUser_dbContext : DbContext
    {
        private readonly Connection _connect = new();
        public TaskUser_dbContext()
        {
        }

        public TaskUser_dbContext(DbContextOptions<TaskUser_dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Attachment> Attachments { get; set; } = null!;
        public virtual DbSet<Authorization> Authorizations { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Priority> Priorities { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;
        public virtual DbSet<Task> Tasks { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(_connect.GetPublicConnection());
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attachment>(entity =>
            {
                entity.ToTable("attachments", "TaskUser");

                entity.HasIndex(e => e.AttachmentPath, "attachments_attachment_path_key")
                    .IsUnique();

                entity.Property(e => e.AttachmentId).HasColumnName("attachment_id");

                entity.Property(e => e.AttachmentPath).HasColumnName("attachment_path");
            });

            modelBuilder.Entity<Authorization>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("authorizations_pkey");

                entity.ToTable("authorizations", "TaskUser");

                entity.HasIndex(e => e.Token, "authorizations_token_key")
                    .IsUnique();

                entity.Property(e => e.UserId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("user_id");

                entity.Property(e => e.Isauthorization).HasColumnName("isauthorization");

                entity.Property(e => e.Token)
                    .HasMaxLength(64)
                    .HasColumnName("token");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Authorization)
                    .HasForeignKey<Authorization>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("authorizations_user_id_fkey");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.CimmentId)
                    .HasName("comments_pkey");

                entity.ToTable("comments", "TaskUser");

                entity.Property(e => e.CimmentId).HasColumnName("cimment_id");

                entity.Property(e => e.Comment1).HasColumnName("comment");

                entity.Property(e => e.TaskId).HasColumnName("task_id");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("comments_task_id_fkey");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("departments", "TaskUser");

                entity.HasIndex(e => e.DepartmentName, "departments_department_name_key")
                    .IsUnique();

                entity.Property(e => e.DepartmentId).HasColumnName("department_id");

                entity.Property(e => e.DepartmentName)
                    .HasMaxLength(32)
                    .HasColumnName("department_name");
            });

            modelBuilder.Entity<Priority>(entity =>
            {
                entity.ToTable("priorities", "TaskUser");

                entity.Property(e => e.PriorityId).HasColumnName("priority_id");

                entity.Property(e => e.PriorityType)
                    .HasMaxLength(16)
                    .HasColumnName("priority_type");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("statuses", "TaskUser");

                entity.HasIndex(e => e.StatusType, "statuses_status_type_key")
                    .IsUnique();

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.StatusType)
                    .HasMaxLength(16)
                    .HasColumnName("status_type");
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.ToTable("tasks", "TaskUser");

                entity.Property(e => e.TaskId).HasColumnName("task_id");

                entity.Property(e => e.AttachmentId).HasColumnName("attachment_id");

                entity.Property(e => e.DateCreate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("date_create");

                entity.Property(e => e.Deadline)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("deadline");

                entity.Property(e => e.DepartmentId).HasColumnName("department_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.PriorityId).HasColumnName("priority_id");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.TaskName)
                    .HasMaxLength(32)
                    .HasColumnName("task_name");

                entity.HasOne(d => d.Attachment)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.AttachmentId)
                    .HasConstraintName("tasks_attachment_id_fkey");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("tasks_department_id_fkey");

                entity.HasOne(d => d.Priority)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.PriorityId)
                    .HasConstraintName("tasks_priority_id_fkey");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("tasks_status_id_fkey");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users", "TaskUser");

                entity.HasIndex(e => e.Email, "users_email_key")
                    .IsUnique();

                entity.HasIndex(e => e.Login, "users_login_key")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Brithday)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("brithday");

                entity.Property(e => e.Contactphone)
                    .HasMaxLength(16)
                    .HasColumnName("contactphone");

                entity.Property(e => e.DepartmentId).HasColumnName("department_id");

                entity.Property(e => e.Email)
                    .HasMaxLength(64)
                    .HasColumnName("email");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(32)
                    .HasColumnName("firstname");

                entity.Property(e => e.Isadmin).HasColumnName("isadmin");

                entity.Property(e => e.Isresponsible).HasColumnName("isresponsible");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(32)
                    .HasColumnName("lastname");

                entity.Property(e => e.Login)
                    .HasMaxLength(16)
                    .HasColumnName("login");

                entity.Property(e => e.Password)
                    .HasMaxLength(128)
                    .HasColumnName("password");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("users_department_id_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
