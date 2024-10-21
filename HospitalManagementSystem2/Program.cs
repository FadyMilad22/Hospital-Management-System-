using HospitalManagementSystem2.Models;
using HospitalManagementSystem2.Repository;
using HospitalManagementSystem2.Repository.Interfaces;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace HospitalManagementSystem2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddTransient<HospitalManagementSystem2.Repository.Interfaces.IEmailSender, HospitalManagementSystem2.Repository.EmailSender>();


            builder.Services.AddDbContext<HospitalContext>(options =>
            {
                builder.Configuration.GetConnectionString("cs");
            });


            builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            builder.Services.AddScoped<IStaffScheduleRepository, StaffScheduleRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
