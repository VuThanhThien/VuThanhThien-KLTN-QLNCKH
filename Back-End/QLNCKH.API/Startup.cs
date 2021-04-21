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
using QLNCKH.BL.Base;
using QLNCKH.BL.Dictionary;
using QLNCKH.BL.Interface;
using QLNCKH.Common.IdentityApplication;
using QLNCKH.DL.Base;
using QLNCKH.DL.DbContext;
using QLNCKH.DL.Dictionary;
using QLNCKH.DL.Interface;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QLNCKH.API
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            //========== connect string: cấu hình liên kết đến CSDL
            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            //var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            var migrationsAssembly = "QLNCKH.Common";

            // ===== Thêm mới Application DbContext: context quản lý tương tác với CSDL Identity ========
            services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly)));


            // ===== Add Identity ========
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.Stores.MaxLengthForKeys = 128;
            }).AddEntityFrameworkStores<ApplicationDbContext>()
            .AddRoles<IdentityRole>()
            .AddDefaultTokenProviders()
            .AddRoleManager<RoleManager<IdentityRole>>();

            services.AddControllers();


            // Adding Authentication  
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            // Adding Jwt Bearer  
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = "http://localhost:4200",
                    ValidIssuer = "http://localhost:61955",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ByYM000OLlMQG6VVVp1OH7Xzyr7gHuw1qvUC5dcGt3SNM"))
                };
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };
            });

            //sửa lỗi cors
            services.AddCors(o => o.AddPolicy("AllowOrigin", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "QLNCKH",
                    Description = "API cho web quản lý đề tài nghiên cứu khoa học cho ĐH.",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Shayne Boyer",
                        Email = string.Empty,
                        Url = new Uri("https://twitter.com/spboyer"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });


            services.AddScoped(typeof(IDbContext<>), typeof(DbContext<>));
            services.AddScoped(typeof(IBaseDL<>), typeof(BaseDL<>));
            services.AddScoped(typeof(IBaseBL<>), typeof(BaseBL<>));
            services.AddScoped<IResearchTopicBL, ResearchTopicBL>();
            services.AddScoped<IResearchTopicDL, ResearchTopicDL>();

            services.AddScoped<IUserBL, UserBL>();
            services.AddScoped<IUserDL, UserDL>();

            services.AddScoped<IMemberTopicDL, MemberTopicDL>();
            services.AddScoped<IMemberTopicBL, MemberTopicBL>();

        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "QLNCKH V1");
            });

            app.UseRouting();
            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            //Sửa lỗi cors
            app.UseCors("AllowOrigin");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
