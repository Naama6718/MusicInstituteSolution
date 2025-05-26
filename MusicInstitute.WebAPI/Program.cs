using MusicInstitute.BL.Mapping;
using MusicInstitute.BL.Services;
using MusicInstitute.DAL.Models;
using MusicInstitute.DAL.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MusicInstitute.DAL.Api;
using MusicInstitute.BL.Api;
using MusicInstitute.BL.Email;

var builder = WebApplication.CreateBuilder(args);

// ? 1. הוספת שירותים למיכל התלות (Dependency Injection)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IEmailService, SmtpEmailService>();

// ? 2. שימוש בשכבות:
// הוספת AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddHostedService<MonthlyLessonMoverService>();

// הוספת שכבת BL ו-DAL
builder.Services.AddScoped<Instrument_Manager_BL>();
builder.Services.AddScoped<Instrument_Manager_DAL>();
builder.Services.AddScoped<Student_Manager_BL>();
builder.Services.AddScoped<Students_Manager_DAL>();
builder.Services.AddScoped<AvailableLessons_Manager_BL>();
builder.Services.AddScoped<AvailableLessons_Manager_DAL>();

builder.Services.AddScoped<IInstrument_Manager_DAL, Instrument_Manager_DAL>();
builder.Services.AddScoped<IInstrument_Manager_BL, Instrument_Manager_BL>();
builder.Services.AddScoped<IStudents_Manager_DAL, Students_Manager_DAL>();
builder.Services.AddScoped<IStudent_Manager_BL, Student_Manager_BL>();
builder.Services.AddScoped<ITeacher_Manager_BL,Teacher_Manager_BL>();
builder.Services.AddScoped < ITeacher_Manager_DAL, Teacher_Manager_DAL>();
builder.Services.AddScoped<IAvailableLessons_Manager_BL, AvailableLessons_Manager_BL>();
builder.Services.AddScoped<IAvailableLessons_Manager_DAL, AvailableLessons_Manager_DAL>();
builder.Services.AddScoped<IBookedLessons_Manager_BL, BookedLessons_Manager_BL>();
builder.Services.AddScoped<IBookedLessons_Manager_DAL, BookedLessons_Manager_DAL>();
builder.Services.AddScoped<IPassedLessons_Manager_BL, PassedLessons_Manager_BL>();
builder.Services.AddScoped<IPassedLessons_Manager_DAL, PassedLessons_Manager_DAL>();

// ? 3. הגדרת קישור למסד נתונים
builder.Services.AddDbContext<DB_Manager>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// ? 4. הגדרת צינור הבקשות (Pipeline)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
