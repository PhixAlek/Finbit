using Finbit.Api.Auth;
using Finbit.Api.Auth.Interfaces;
using Finbit.Api.Auth.Models;
using Finbit.Api.Auth.Services;
using Finbit.Api.Data;
using Finbit.Api.Models;
using Finbit.Api.Repositories.Implementations;
using Finbit.Api.Repositories.Interfaces;
using Finbit.Api.Services.Implementations;
using Finbit.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowAngular", p =>
        p.WithOrigins("http://localhost:4200")
         .AllowAnyHeader()
         .AllowAnyMethod());
});



// Load JWT settings from appsettings.json
var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];
var keyBytes = Encoding.UTF8.GetBytes(jwtKey);

// Register JWT Token Service
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

// Register Repositories
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

// Add Controllers
builder.Services.AddControllers();


builder.Services.AddScoped<ITransactionService, TransactionService>();


// Configure JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes)
    };
});

// Enable Swagger + JWT support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FinBit API", Version = "v1" });

    // Add JWT bearer support in Swagger UI
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

app.UseCors("AllowAngular");


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

// Enable authentication & authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


// Seed temporal de datos reales para pruebas
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    if (!db.Transactions.Any())
    {
        db.Transactions.AddRange(new[]
        {
        new Transaction
        {
            Amount = 100,
            Concept = "Initial deposit",
            Date = DateTimeOffset.UtcNow,
            Type = TransactionType.Income,
            Category = TransactionCategory.Salary,
            Username = "admin" 
        },
        new Transaction
        {
            Amount = 25.5m,
            Concept = "Groceries",
            Date = DateTimeOffset.UtcNow,
            Type = TransactionType.Expense,
            Category = TransactionCategory.Food,
            Username = "user"
        }
    });

        db.SaveChanges();
    }


}



app.Run();
