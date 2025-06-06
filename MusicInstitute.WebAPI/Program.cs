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

// ? 1. ����� ������� ����� ����� (Dependency Injection)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ? 2. ����� ������:
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddHostedService<MonthlyLessonMoverService>();

builder.Services.AddScoped<Instrument_Manager_BL>();
builder.Services.AddScoped<Instrument_Manager_DAL>();
builder.Services.AddScoped<Student_Manager_BL>();
builder.Services.AddScoped<Students_Manager_DAL>();
builder.Services.AddScoped<PasswordResetService>();
builder.Services.AddScoped<AvailableLessons_Manager_BL>();
builder.Services.AddScoped<AvailableLessons_Manager_DAL>();

builder.Services.AddScoped<IInstrument_Manager_DAL, Instrument_Manager_DAL>();
builder.Services.AddScoped<IInstrument_Manager_BL, Instrument_Manager_BL>();
builder.Services.AddScoped<IStudents_Manager_DAL, Students_Manager_DAL>();
builder.Services.AddScoped<IStudent_Manager_BL, Student_Manager_BL>();
builder.Services.AddScoped<ITeacher_Manager_BL, Teacher_Manager_BL>();
builder.Services.AddScoped<ITeacher_Manager_DAL, Teacher_Manager_DAL>();
builder.Services.AddScoped<IAvailableLessons_Manager_BL, AvailableLessons_Manager_BL>();
builder.Services.AddScoped<IAvailableLessons_Manager_DAL, AvailableLessons_Manager_DAL>();
builder.Services.AddScoped<IBookedLessons_Manager_BL, BookedLessons_Manager_BL>();
builder.Services.AddScoped<IBookedLessons_Manager_DAL, BookedLessons_Manager_DAL>();
builder.Services.AddScoped<IPassedLessons_Manager_BL, PassedLessons_Manager_BL>();
builder.Services.AddScoped<IPassedLessons_Manager_DAL, PassedLessons_Manager_DAL>();
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
builder.Services.AddScoped<IEmailService, SmtpEmailService>();

// ? 3. ����� ����� ���� ������
builder.Services.AddScoped<DB_Manager>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000") // ������ �� �-React ���
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
var app = builder.Build();

app.UseCors("AllowReactApp");

// ? 4. ����� ����� ������ (Pipeline)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
