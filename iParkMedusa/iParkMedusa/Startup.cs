using iParkMedusa.Contexts;
using iParkMedusa.Models;
using iParkMedusa.Repositories;
using iParkMedusa.Services;
using iParkMedusa.Services.ParkingLot;
using iParkMedusa.Services.PaypalService;
using iParkMedusa.Settings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.Owin.Security.Google;
using System;
using System.Text;

namespace iParkMedusa
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
            //Google Auth 
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
                    .AddCookie(options =>
                    {
                        options.LoginPath = "/account/google-login"; // Must be lowercase
                    })
                    .AddGoogle(options =>
                    {
                        options.ClientId = "709641791608-vv91i55b7ar74gt3hkj1kq8tqrbq0s7m.apps.googleusercontent.com";
                        options.ClientSecret = "K8A6zzKwro1MZltAiLcP6z0t";
                    });

            //Configuration from AppSettings
            services.Configure<JWT>(Configuration.GetSection("JWT"));

            //Configure Secrets Sections
            services.Configure<ParkAPISecrets>(Configuration.GetSection("ParkAPISecrets"));
            services.Configure<PayPalCredentials>(Configuration.GetSection("PayPalCredentials"));

            //User Manager Service
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddScoped<IUserService, UserService>();

            //ParkingLotService
            services.AddScoped<IParkingLotService, ParkAPIService>();


            //PaymentMethods
            services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
            services.AddTransient<PaymentMethodService>();

            //PayPalService
            services.AddTransient<PayPalService>();

            //Transactions
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddTransient<TransactionService>();

            //Reservations
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddTransient<ReservationService>();

            //Adding DB Context with MSSQL
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            //Adding Athentication - JWT
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = Configuration["JWT:Issuer"],
                        ValidAudience = Configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"]))
                    };
                });
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "iParkMedusa", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "iParkMedusa v1"));
            }


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
