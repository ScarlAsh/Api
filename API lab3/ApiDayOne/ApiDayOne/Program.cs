
using ApiDayOne.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ApiDayOne
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			builder.Services.AddDbContext<StudentContext>(options => 
			options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

			builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
			.AddEntityFrameworkStores<StudentContext>()
			.AddDefaultTokenProviders();


			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddAuthentication()
				.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = "https://localhost:7185/",
						ValidAudience = "http://localhost:5050/",
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("my_keyyyy_123_tokennnnnnnnnnnnnn_keyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy"))
					};
				});


			builder.Services.AddCors(options => 
			options.AddPolicy("mypolicy" , builder =>
			{
				builder.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader(); ;
			}));

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseCors("mypolicy");
			app.MapControllers();

			app.Run();
		}
	}
}
