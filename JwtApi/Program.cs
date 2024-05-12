using System.Text;
using JwtApi;
using JwtApi.Claims.Handlers;
using JwtApi.Claims.Requirements;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder();
builder.Services.AddTransient<IAuthorizationHandler, AgeHandler>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Consts.SECRET_KEY)),
			ValidateIssuer = false,
			ValidateAudience = false,
			ValidateLifetime = true,
		};
	});
builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("AdminPolicy", policy =>
	{
		policy.RequireRole("admin", "ADMIN");
	});
	options.AddPolicy("UserPolicy", policy =>
	{
		policy.RequireRole("admin", "ADMIN", "user", "USER");
	});
	options.AddPolicy("AdultPolicy", policy =>
	{
		policy.Requirements.Add(new AgeRequirement(18));
	});

});
	
	
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();

app.Run();
