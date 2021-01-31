using Hangfire;
using Hangfire.SqlServer;
using Helper.Models.AppDbContext;
using Helper.Repositories;
using Helper.Repositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Helper
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


            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Add Hangfire services.
            services.AddHangfire(configuration => configuration
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection"), new SqlServerStorageOptions
        {
            CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
            SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
            QueuePollInterval = TimeSpan.Zero,
            UseRecommendedIsolationLevel = true,
            UsePageLocksOnDequeue = true,
            DisableGlobalLocks = true
        }));
            // Add the processing server as IHostedService
            services.AddHangfireServer();

            services.AddControllersWithViews();
            services.AddTransient<IPersonRepository, PersonRepository>();
            services.AddTransient<IDocumentRepository, DocumentRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseHangfireServer();

            app.UseHangfireDashboard();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            //TestDateUtils();
            TestDates();
        }

        private void TestDates()
        {
            DateTime actualDatePlusMonth = DateTime.Now.AddMonths(1);
            string date = actualDatePlusMonth.Month.ToString("d2") + "-" + actualDatePlusMonth.Year.ToString();
            Console.WriteLine(date);
        }

        private void TestDateUtils()
        {
            DateTime date1 = new DateTime(2020, 12, 12); // should be 14
            DateTime date2 = new DateTime(2020, 12, 20); // should be 21
            DateTime date3 = new DateTime(2020, 12, 25); // should be 27

            DateTime date1check = DateUtils.NextWorkingDay(date1);
            DateTime date2check = DateUtils.NextWorkingDay(date2);
            DateTime date3check = DateUtils.NextWorkingDay(date3);

        }
    }
}
