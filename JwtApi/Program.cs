using System.Reflection;
using System.Text;
using JwtApi;
using JwtApi.Claims.Handlers;
using JwtApi.Claims.Requirements;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;

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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v1", new OpenApiInfo
	{
		Version = "v1",
		Title = "Jwt Auth API",
		Description = "This is my description for jwt api",
		Contact = new OpenApiContact
		{
			Email = "testEmail@mail.ru",
			Name = "my_contact",
		}
	});
	var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

	options.AddSecurityDefinition
	(
		name: JwtBearerDefaults.AuthenticationScheme,
		securityScheme: new OpenApiSecurityScheme()
		{
			In = ParameterLocation.Header,
			Name = HeaderNames.Authorization,
			Scheme = JwtBearerDefaults.AuthenticationScheme,
			Type = SecuritySchemeType.Http
		}
	);
	options.AddSecurityRequirement
	(
		new OpenApiSecurityRequirement()
		{
			{
				new OpenApiSecurityScheme()
				{
					Reference = new OpenApiReference()
					{
						Id = JwtBearerDefaults.AuthenticationScheme,
						Type = ReferenceType.SecurityScheme
					}
				},
				Array.Empty<string>()
			}
		});


});
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(options => 
	{
		options.InjectStylesheet("/swagger-ui/custom.css");

		options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
		options.RoutePrefix = string.Empty;
	});
}
app.UseStaticFiles();
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();

app.Run();
