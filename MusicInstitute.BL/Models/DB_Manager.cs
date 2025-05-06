using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MusicInstitute.BL.Models;

public partial class DB_Manager : DbContext
{
    public DB_Manager()
    {
    }

    public DB_Manager(DbContextOptions<DB_Manager> options)
        : base(options)
    {
    }

    public virtual DbSet<AvailableLesson> AvailableLessons { get; set; }

    public virtual DbSet<BookedLesson> BookedLessons { get; set; }

    public virtual DbSet<Instrument> Instruments { get; set; }

    public virtual DbSet<PassedLesson> PassedLessons { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\נעמי\\Desktop\\PROJ\\MusicInstituteSolution\\MusicInstitute.DAL\\data\\DB.mdf;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AvailableLesson>(entity =>
        {
            entity.HasKey(e => e.LessonId).HasName("PK__Availabl__B084ACD065CC0C6A");

            entity.ToTable("Available_Lessons");

            entity.Property(e => e.Kind)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.TeacherIdLessons).HasColumnName("TeacherId_Lessons");

            entity.HasOne(d => d.TeacherIdLessonsNavigation).WithMany(p => p.AvailableLessons)
                .HasForeignKey(d => d.TeacherIdLessons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Available_Lessons_ToTable");
        });

        modelBuilder.Entity<BookedLesson>(entity =>
        {
            entity.HasKey(e => e.LessonId).HasName("PK__Booked_L__B084ACD07A775E64");

            entity.ToTable("Booked_Lessons");

            entity.Property(e => e.Kind)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.StudentIdLessons).HasColumnName("StudentId_Lessons");
            entity.Property(e => e.TeacherIdLessons).HasColumnName("TeacherId_Lessons");

            entity.HasOne(d => d.StudentIdLessonsNavigation).WithMany(p => p.BookedLessons)
                .HasForeignKey(d => d.StudentIdLessons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Booked_Lessons_ToTable1");

            entity.HasOne(d => d.TeacherIdLessonsNavigation).WithMany(p => p.BookedLessons)
                .HasForeignKey(d => d.TeacherIdLessons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Booked_Lessons_ToTable");
        });

        modelBuilder.Entity<Instrument>(entity =>
        {
            entity.HasKey(e => e.InstrumentId).HasName("PK__Instrume__430A53863BC0E3FC");

            entity.Property(e => e.InstrumentId).ValueGeneratedNever();
            entity.Property(e => e.LessonName)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");

            entity.HasMany(d => d.Teachers).WithMany(p => p.Instruments)
                .UsingEntity<Dictionary<string, object>>(
                    "InstrumentTeacher",
                    r => r.HasOne<Teacher>().WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Instrumen__teach__160F4887"),
                    l => l.HasOne<Instrument>().WithMany()
                        .HasForeignKey("InstrumentId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Instrumen__instr__5EBF139D"),
                    j =>
                    {
                        j.HasKey("InstrumentId", "TeacherId").HasName("PK__Instrume__38BDF46F11730734");
                        j.ToTable("Instrument_Teacher");
                        j.IndexerProperty<int>("InstrumentId").HasColumnName("instrument_id");
                        j.IndexerProperty<int>("TeacherId").HasColumnName("teacher_id");
                    });
        });

        modelBuilder.Entity<PassedLesson>(entity =>
        {
            entity.HasKey(e => e.LessonId).HasName("PK__Passed_L__B084ACD0048A6527");

            entity.ToTable("Passed_Lessons");

            entity.Property(e => e.Kind)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.StudentIdLessons).HasColumnName("StudentId_Lessons");
            entity.Property(e => e.TeacherIdLessons).HasColumnName("TeacherId_Lessons");

            entity.HasOne(d => d.StudentIdLessonsNavigation).WithMany(p => p.PassedLessons)
                .HasForeignKey(d => d.StudentIdLessons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Passed_Lessons_ToTable1");

            entity.HasOne(d => d.TeacherIdLessonsNavigation).WithMany(p => p.PassedLessons)
                .HasForeignKey(d => d.TeacherIdLessons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Passed_Lessons_ToTable");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Students__32C52B997E69B2CE");

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
            entity.HasKey(e => e.TeacherId).HasName("PK__tmp_ms_x__EDF25964EB31E436");

            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
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
