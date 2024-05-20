using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetHealthSys.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PetHealthSysContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PetHealthSysContext") ?? throw new InvalidOperationException("Connection string 'PetHealthSysContext' not found.")));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();

