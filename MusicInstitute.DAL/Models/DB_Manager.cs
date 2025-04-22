using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MusicInstitute.DAL.Models;

public partial class DB_Manager : DbContext
{
    public DB_Manager()
    {
    }

    public DB_Manager(DbContextOptions<DB_Manager> options)
        : base(options)
    {
    }

    public virtual DbSet<GroupLesson> GroupLessons { get; set; }

    public virtual DbSet<Lesson> Lessons { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\נעמי\\Desktop\\new\\MusicInstituteSolution\\MusicInstitute.DAL\\data\\DB.mdf;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GroupLesson>(entity =>
        {
            entity.HasKey(e => e.GroupLessonId).HasName("PK__GroupLes__46A36CB39D58953B");

            entity.Property(e => e.TeacherIdGroupLessons).HasColumnName("TeacherId_GroupLessons");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");

            entity.HasOne(d => d.TeacherIdGroupLessonsNavigation).WithMany(p => p.GroupLessons)
                .HasForeignKey(d => d.TeacherIdGroupLessons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GroupLessons_ToTable");

            entity.HasMany(d => d.StudentIdGroupLessonStudents).WithMany(p => p.GroupLessons)
                .UsingEntity<Dictionary<string, object>>(
                    "GroupLessonStudent",
                    r => r.HasOne<Student>().WithMany()
                        .HasForeignKey("StudentIdGroupLessonStudents")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__GroupLess__Stude__4222D4EF"),
                    l => l.HasOne<GroupLesson>().WithMany()
                        .HasForeignKey("GroupLessonId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__GroupLess__Group__412EB0B6"),
                    j =>
                    {
                        j.HasKey("GroupLessonId", "StudentIdGroupLessonStudents").HasName("PK__GroupLes__EAF5496A93E74974");
                        j.ToTable("GroupLessonStudents");
                        j.IndexerProperty<int>("StudentIdGroupLessonStudents").HasColumnName("StudentId_GroupLessonStudents");
                    });
        });

        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.HasKey(e => e.LessonId).HasName("PK__Lessons__B084ACD097BD49F8");

            entity.Property(e => e.StudentIdLessons).HasColumnName("StudentId_Lessons");
            entity.Property(e => e.TeacherIdLessons).HasColumnName("TeacherId_Lessons");

            entity.HasOne(d => d.StudentIdLessonsNavigation).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.StudentIdLessons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Lessons_ToTable1");

            entity.HasOne(d => d.TeacherIdLessonsNavigation).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.TeacherIdLessons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Lessons_ToTable");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Students__32C52B997E69B2CE");

            entity.Property(e => e.EMail1)
                .HasMaxLength(255)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("E-mail");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Instrument)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.StudentPassword)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.TeacherId).HasName("PK__Teachers__EDF25964E9FE6320");

            entity.Property(e => e.AvailableHours)
                .HasMaxLength(200)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.EMail)
                .HasMaxLength(255)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("E-mail");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Instruments)
                .HasMaxLength(200)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.TeacherPassword)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
