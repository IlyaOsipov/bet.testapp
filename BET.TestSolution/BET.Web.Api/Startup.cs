using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BET.Infrastructure.Repositories;
using BET.Infrastructure.Services;
using BET.Repositories.DataContext;
using BET.Repositories.Repos;
using BET.Services;

namespace BET.Web.Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IProductRepository, ProductRepository>();
			services.AddScoped<ICartRepository, CartRepository>();
			services.AddScoped<IAuthorizationService, AuthorizationService>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IProductService, ProductService>();
			services.AddScoped<ICartService, CartService>();
			services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); 
			services.AddScoped<BETDataContext>(_ =>
				new BETDataContext(Configuration.GetConnectionString("BETDataDB")));

			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
				{
					Title = "BET Service API",
					Version = "v2",
					Description = "Service for BET",
				});
			});

			var allowedCors = Configuration.GetSection("CORS-Settings:AllowedCorsOrigins")
				.Get<string[]>();

			//Add CORS policy for permissible domains
			services.AddCors(options =>
			{
				options.AddPolicy("ALLOWED_ORIGINS",
					builder =>
					{
						builder.WithOrigins(allowedCors != null ? allowedCors : new string[] { })
							.AllowAnyHeader()
							.AllowAnyMethod();
					});
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();
			app.UseCors(builder =>
			{
				builder
					.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader();
			});
			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
			app.UseSwagger();
			app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v2/swagger.json", "BET Services"));
		}
	}
}
