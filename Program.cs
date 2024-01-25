
using Microsoft.Extensions.Configuration;
using RedisTest;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// RedisClient.Init("127.0.0.1:6379");
// builder.Services.AddSingleton(RedisClient.Instance);
RedisClient2.Init("127.0.0.1:6379");
builder.Services.AddSingleton<RedisClient2>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
