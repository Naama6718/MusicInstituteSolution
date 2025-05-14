using MusicInstitute.BL.Mapping;
using MusicInstitute.BL.Services;
using MusicInstitute.DAL.Models;
using MusicInstitute.DAL.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MusicInstitute.DAL.Api;
using MusicInstitute.BL.Api;

var builder = WebApplication.CreateBuilder(args);

// ? 1. ����� ������� ����� ����� (Dependency Injection)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ? 2. ����� ������:
// ����� AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// ����� ���� BL �-DAL
builder.Services.AddScoped<Instrument_Manager_BL>();
builder.Services.AddScoped<Instrument_Manager_DAL>();

builder.Services.AddScoped<IInstrument_Manager_DAL, Instrument_Manager_DAL>();
builder.Services.AddScoped<IInstrument_Manager_BL, Instrument_Manager_BL>();

// ? 3. ����� ����� ���� ������
builder.Services.AddDbContext<DB_Manager>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

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
