using ApplicationLayer.IUnitOfWork;
using ApplicationLayer.Mappings;
using ApplicationLayer.Services.IAuthService;
using ApplicationLayer.Services.IMailSendService;
using ApplicationLayer.Services.ITableBookingService;
using DomainLayer.Entities.IdentityDbUser;
using InfrastructureLayer.AuthService;
using InfrastructureLayer.Data;
using InfrastructureLayer.MailSend;
using InfrastructureLayer.Repositories;
using InfrastructureLayer.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Restaurant_Table_Booking_Web_Api.ExceptionHandler;
using System.Reflection;
using System.Text;

namespace Restaurant_Table_Booking_Web_Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();


         /*   //For Enum Conversion
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });*/


            //For Authentication
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "RestaurantManagement", Version = "v1" });
                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                           Reference = new OpenApiReference
                           {
                               Type = ReferenceType.SecurityScheme,
                               Id = JwtBearerDefaults.AuthenticationScheme
                           },
                           Scheme = "Oauth2",
                           Name = JwtBearerDefaults.AuthenticationScheme,
                           In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });

            //For DbContext
            builder.Services.AddDbContext<AuthDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("RestaurantManagementConnectionString")));


            //For implimenting interface in class
            builder.Services.AddScoped<IAuthRepository, AuthRepository>();
            builder.Services.AddScoped<IUnitOfWorkRepository, UnitOfWorkRepository>();
            builder.Services.AddScoped<ITableBookingRepository, TableBookingRepository>();
            builder.Services.AddScoped<IMailSender, MailSenderRepository>();

            //MediatR
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies: Assembly.Load("Restaurant Table Booking.Application")));
            //For mapping 
            builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));


            builder.Services.AddCors((options) =>
            {
                options.AddPolicy("React", options =>
                {
                    options.AllowAnyHeader();
                    options.AllowAnyMethod();
                    options.AllowAnyOrigin();
                });
            });
            //For Add Identity User
            builder.Services.AddIdentityCore<ApplicationUser>().AddRoles<IdentityRole>()
                .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>("RestaurantManagement")
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();



            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredUniqueChars = 1;
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? ""))
            });

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

            app.UseCors("React");
            app.UseExceptionHandler();
            app.MapControllers();

            app.Run();
        }
    }
}
