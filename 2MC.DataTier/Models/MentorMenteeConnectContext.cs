using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace _2MC.DataTier.Models
{
    public partial class MentorMenteeConnectContext : DbContext
    {
        public MentorMenteeConnectContext()
        {
        }

        public MentorMenteeConnectContext(DbContextOptions<MentorMenteeConnectContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Certificate> Certificates { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Major> Majors { get; set; }
        public virtual DbSet<MenteeSession> MenteeSessions { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<SubjectMajor> SubjectMajors { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                // optionsBuilder.UseSqlServer("Server=18.136.108.204;Database=MentorMenteeConnect;UID=admin;PWD=Nguyenduy111;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Certificate>(entity =>
            {
                entity.ToTable("Certificate");

                entity.Property(e => e.ImageUrl)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.Mentor)
                    .WithMany(p => p.Certificates)
                    .HasForeignKey(d => d.MentorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Certificate_Mentor");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Certificates)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Certificate_Subject");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.FinishDate).HasColumnType("datetime");

                entity.Property(e => e.ImageUrl).HasMaxLength(500);

                entity.Property(e => e.Location).HasMaxLength(256);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.Slug)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.Mentor)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.MentorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Course_Account");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Course_Subject");
            });

            modelBuilder.Entity<Major>(entity =>
            {
                entity.ToTable("Major");

                entity.Property(e => e.ImageUrl).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(256);
            });

            modelBuilder.Entity<MenteeSession>(entity =>
            {
                entity.ToTable("MenteeSession");

                entity.HasOne(d => d.Mentee)
                    .WithMany(p => p.MenteeSessions)
                    .HasForeignKey(d => d.MenteeId)
                    .HasConstraintName("FK_MenteeSession_Mentor");

                entity.HasOne(d => d.Report)
                    .WithMany(p => p.MenteeSessions)
                    .HasForeignKey(d => d.ReportId)
                    .HasConstraintName("FK_MenteeSession_Report");

                entity.HasOne(d => d.Session)
                    .WithMany(p => p.MenteeSessions)
                    .HasForeignKey(d => d.SessionId)
                    .HasConstraintName("FK_MenteeSession_Session");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.CheckInDate).HasColumnType("datetime");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Discount).HasColumnType("money");

                entity.Property(e => e.FinalAmount).HasColumnType("money");

                entity.Property(e => e.OrderCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.TotalAmount).HasColumnType("money");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Course");

                entity.HasOne(d => d.Mentee)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.MenteeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Mentor");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.ToTable("Report");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.ToTable("Session");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Sessions)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_Session_Course");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.ToTable("Subject");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(256);
            });

            modelBuilder.Entity<SubjectMajor>(entity =>
            {
                entity.HasKey(e => new { e.SubjectId, e.MajorId })
                    .HasName("SubjectMajor_pk");

                entity.ToTable("SubjectMajor");

                entity.HasOne(d => d.Major)
                    .WithMany(p => p.SubjectMajors)
                    .HasForeignKey(d => d.MajorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubjectMajor_Major");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.SubjectMajors)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubjectMajor_Subject");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Address).HasMaxLength(256);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.DayOfBirth).HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FullName).HasMaxLength(256);

                entity.Property(e => e.ImageUrl).HasMaxLength(500);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_User_Role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
