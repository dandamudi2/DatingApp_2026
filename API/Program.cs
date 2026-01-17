using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// register token service
builder.Services.AddScoped<API.Interfaces.ITokenService, API.Interfaces.TokenService>();

// Register AppDbContext with SQLite
builder.Services.AddDbContext<API.Data.AppDbContext>(options =>
	options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add authentication using JWT bearer tokens
var tokenKey = builder.Configuration["TokenKey"] ?? string.Empty;
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
			ValidateIssuer = false,
			ValidateAudience = false
		};
	});

builder.Services.AddAuthorization();

// Add CORS policy
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAngularApp", policy =>
	{
		policy.AllowAnyOrigin()
			  .AllowAnyMethod()
			  .AllowAnyHeader();
	});
});

var app = builder.Build();
// Configure the HTTP request pipeline.
// Use CORS
app.UseCors("AllowAngularApp");

// Use authentication/authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
