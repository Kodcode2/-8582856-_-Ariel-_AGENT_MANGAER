using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MossadAPI.Data;
using MossadAPI.Services.Interfaces;
using MossadAPI.Services.Implementation;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MossadAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MossadAPIContext") ?? throw new InvalidOperationException("Connection string 'MossadAPIContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITargetService, TargetService>();
builder.Services.AddScoped<IAgentService, AgentService>();
builder.Services.AddScoped<IMissionService, MissionService>();
builder.Services.AddScoped<ILoginService, LoginService>();

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
