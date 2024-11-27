using Lab2;
using Microsoft.EntityFrameworkCore;
using Lab2.Repositories;
using System.Globalization;


var builder = WebApplication.CreateBuilder(args);

// �������� ������ ����������� � SQL Server
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// ����������� ��������
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ����������� �����������
builder.Services.AddScoped<Repository>();

var app = builder.Build();

// ��������� Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
