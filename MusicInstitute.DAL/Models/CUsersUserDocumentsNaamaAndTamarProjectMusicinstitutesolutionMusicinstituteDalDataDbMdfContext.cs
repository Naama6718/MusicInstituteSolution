using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MusicInstitute.DAL.Models;

public partial class CUsersUserDocumentsNaamaAndTamarProjectMusicinstitutesolutionMusicinstituteDalDataDbMdfContext : DbContext
{
    public CUsersUserDocumentsNaamaAndTamarProjectMusicinstitutesolutionMusicinstituteDalDataDbMdfContext()
    {
    }

    public CUsersUserDocumentsNaamaAndTamarProjectMusicinstitutesolutionMusicinstituteDalDataDbMdfContext(DbContextOptions<CUsersUserDocumentsNaamaAndTamarProjectMusicinstitutesolutionMusicinstituteDalDataDbMdfContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Instrument> Instruments { get; set; }

    public virtual DbSet<Lesson> Lessons { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\User\\Documents\\naama and tamar project\\MusicInstituteSolution\\MusicInstitute.DAL\\data\\DB.mdf\";Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Instrument>(entity =>
        {
            entity.HasKey(e => e.InstrumentId).HasName("PK__Instrume__430A53863BC0E3FC");

            entity.Property(e => e.InstrumentId).ValueGeneratedNever();
            entity.Property(e => e.LessonName).HasMaxLength(50);

            entity.HasMany(d => d.Teachers).WithMany(p => p.Instruments)
                .UsingEntity<Dictionary<string, object>>(
                    "InstrumentTeacher",
                    r => r.HasOne<Teacher>().WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Instrumen__teach__5FB337D6"),
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

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.Instrument).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.StudentPassword).HasMaxLength(100);
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.TeacherId).HasName("PK__Teachers__EDF25964E9FE6320");

            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.TeacherPassword).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
