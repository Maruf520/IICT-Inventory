using IICT_Store.Models;
using IICT_Store.Models.Users;
using IICT_Store.Repositories.BookingRepositories;
using IICT_Store.Repositories.BookingTimeSlotRepositories;
using IICT_Store.Repositories.CategoryRepositories;
using IICT_Store.Repositories.DamagedProductRepositories;
using IICT_Store.Repositories.DamagedProductSerialRepositories;
using IICT_Store.Repositories.DistributionRepositories;
using IICT_Store.Repositories.PersonRepositories;
using IICT_Store.Repositories.ProductNumberRepositories;
using IICT_Store.Repositories.ProductRepositories;
using IICT_Store.Repositories.ProductSerialNoRepositories;
using IICT_Store.Repositories.PurchaseRepositories;
using IICT_Store.Repositories.ReturnedProductRepositories;
using IICT_Store.Repositories.ReturnedProductSerialNoRepositories;
using IICT_Store.Repositories.RoleRepositories;
using IICT_Store.Repositories.TimeSlotRepository;
using IICT_Store.Repositories.UserRepositories;
using IICT_Store.Services.AdministrationServices;
using IICT_Store.Services.ApprovalServices;
using IICT_Store.Services.AuthServices;
using IICT_Store.Services.BookingServices;
using IICT_Store.Services.CategoryServices;
using IICT_Store.Services.DamagedProductServices;
using IICT_Store.Services.DistributionServices;
using IICT_Store.Services.PersonServices;
using IICT_Store.Services.ProductNumberServices;
using IICT_Store.Services.ProductServices;
using IICT_Store.Services.PurchaseServices;
using IICT_Store.Services.ReturnProductServices;
using IICT_Store.Services.TimeSlotService;
using IICT_Store.Services.TimeSlotServices;
using IICT_Store.Services.UserServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IICT_Store.Api
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
/*            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IICT_Store.Api", Version = "v1" });
            });*/
            services.AddDbContext<IICT_StoreDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("Connection"), optionsBuilder =>
            optionsBuilder.MigrationsAssembly("IICT-Store.Api")));            
            services.AddDbContext<IdentityContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("Connection"), optionsBuilder =>
            optionsBuilder.MigrationsAssembly("IICT-Store.Api")));
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IPurchaseRepository, PurchaseRepository>();
            services.AddScoped<IPurchaseService, PurchaseService>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IDistributionRepository, DistributionRepository>();
            services.AddScoped<IDistributionService, DistributionService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDamagedProductService, DamagedProductService>();
            services.AddScoped<IDamagedProductRepository, DamagedProductRepository>();
            services.AddScoped<IProductSerialNoRepository, ProductSerialNoRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IProductNumberRepository, ProductNumberRepository>();
            services.AddScoped<IProductNumberService, ProductNumberService>();
            services.AddScoped<IApprovalService, ApprovalService>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<ITimeSlotReposiotry, TimeSlotRepository>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<ITimeSlotService, TimeSlotService>();
            services.AddScoped<IBookingTimeSlotRepository, BookingTimeSlotRepository>();
            services.AddScoped<IReturnedProductRepository, ReturnedProductRepository>();
            services.AddScoped<IReturnedProductSerialNoRepository, ReturnedProductSerialNoRepository>();
            services.AddScoped<IReturnProductService, ReturnProductService>();
            services.AddScoped<IAdministrationService, AdministrationService>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddScoped<IDamagedProductSerialNoRepository, DamagedProductSerialNoRepository>();
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.User.RequireUniqueEmail = false;
            })
            .AddEntityFrameworkStores<IdentityContext>()
            .AddDefaultTokenProviders();
            services.AddScoped<IUserService, UserService>();
            services.AddControllersWithViews()
            .AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(option =>
            {
                option.SaveToken = true;
                option.RequireHttpsMetadata = false;
                option.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssure"],
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                };
                option.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = context =>
                    {
                        var Token = context.Request.Headers["UserCred1"].ToString();
                        context.Token = Token;
                        return Task.CompletedTask;
                    },
                };
            });

            services.AddSwaggerGen(swagger =>
            {
                //This is to generate the Default UI of Swagger Documentation    
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ASP.NET 5 Web API",
                    Description = "Authentication and Authorization in ASP.NET 5 with JWT and Swagger"
                });
                // To Enable authorization using Swagger (JWT)    
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                });
                swagger.DescribeAllEnumsAsStrings();
                swagger.MapType<TimeSpan>(() => new OpenApiSchema
                {
                    Type = "string",
                    Example = new Microsoft.OpenApi.Any.OpenApiString("00:00:00")
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IICT_Store.Api v1"));
            }
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "IICT_Store.Api v1");
            });
            app.UseSwagger();
            app.UseCors(cors =>
              cors
              .AllowAnyHeader()
              .AllowAnyMethod()
              .SetIsOriginAllowed(_ => true)
              .AllowCredentials()
            );
            app.UseHttpsRedirection();
            string path = Path.Combine(Directory.GetCurrentDirectory(), "files");
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(path),
                RequestPath = "/files"
            });
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
