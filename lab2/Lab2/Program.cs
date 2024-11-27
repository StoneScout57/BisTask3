using Lab2.Repositories;

var builder = WebApplication.CreateBuilder(args);

// ����������� ��������
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ����������� �����������
builder.Services.AddSingleton(new Repository("file.txt"));

var app = builder.Build();

// ��������� Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
