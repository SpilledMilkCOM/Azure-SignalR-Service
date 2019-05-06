using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SignalRService.Core.Hubs;

namespace SignalRService.Core
{
	public class Startup
	{
		private const string AZURE_SIGNALR_CONNECTION_STRING_KEY = "Azure:ConnectionString";

		public Startup(IConfiguration configuration)
		{
			// This is the DEFAULT setting.
			Configuration = configuration;

			// Need to be able to use TWO different appsetting.json files.
			// Many examples show this being built in the Configure() method to take advantage of IHostingEnvironment.
			// A work-around (HACK) to be able to add the appsettings files you need, make sure you set their file attribute to "copy if newer".
			// By default these files are set to "do not copy"
			// Since the connection string is needed in the ConfigureServices() method, and THAT is called BEFORE Configure() then set this up here.
			// (And you can keep the Configuration property "read only" versus adding a "private set;" method.)

			var builder = new ConfigurationBuilder()
			//.SetBasePath(env.ContentRootPath)
			.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
			.AddJsonFile("appsettings.secret.json", optional: true)
			.AddEnvironmentVariables();

			Configuration = builder.Build();
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});


			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

			// Pasting the the "Quickstart" code here NEEDS MORE!

			var connection = Configuration[AZURE_SIGNALR_CONNECTION_STRING_KEY];

			// Sorry, this is a demo so make sure you verify this connection isn't null.

			services.AddSignalR()
					.AddAzureSignalR(connection);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseCookiePolicy();

			app.UseMvc();

			app.UseAzureSignalR(routes =>
			{
				routes.MapHub<Bus>("/bus");
			});

		}
	}
}