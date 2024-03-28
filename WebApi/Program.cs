using Application;
using Persistence;
using Infrastructure;
using Core.CrossCuttingConcerns.Exceptions.Extensions;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Persistence.Contexts;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddHttpContextAccessor();

builder.Services.AddStackExchangeRedisCache(opt => opt.Configuration = "localhost:6379");


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddIdentityCore<AppUser>(opt =>
{
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequiredLength = 2;
    opt.Password.RequireDigit = false;
    opt.SignIn.RequireConfirmedEmail = false;

})
    .AddRoles<AppRole>()
    .AddEntityFrameworkStores<BaseDbContext>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureCustomExceptionMiddleware();

app.UseSession();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

