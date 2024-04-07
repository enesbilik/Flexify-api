using Application;
using Persistence;
using Infrastructure;
using Core.CrossCuttingConcerns.Exceptions.Extensions;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// builder.Services.AddCors(
//     options =>
//     {
//         options.AddDefaultPolicy(policy =>
//         {
//             policy.AllowCredentials();
//             policy.AllowAnyHeader();
//             policy.AllowAnyOrigin();
//             policy.AllowAnyMethod();
//         });
//     }
// );

builder.Services.AddControllers();
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddHttpContextAccessor();

builder.Services.AddStackExchangeRedisCache(opt => opt.Configuration = "localhost:6379");


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo
            { Title = "Flexify API", Version = "v1", Description = "Flexify API swagger client." });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description =
            "'Bearer' yazip bosluk biraktiktan sonra Token girebilirsiniz \r\n\r\n Örnegin: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\""
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.ConfigureCustomExceptionMiddleware();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();