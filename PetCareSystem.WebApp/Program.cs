using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PetCareSystem.Data.EF;
using PetCareSystem.Data.Repositories.Bookings;
using PetCareSystem.Data.Repositories.Users;
using PetCareSystem.Data.Repositories.Customers;
using PetCareSystem.Data.Repositories.Services;
using PetCareSystem.Data.Repositories.Pets;
using PetCareSystem.Data.Repositories.Roles;
using PetCareSystem.Data.Repositories.Doctors;
using PetCareSystem.Data.Repositories.BookingServices;
using PetCareSystem.Data.Repositories.UserRoles;
using PetCareSystem.Data.Repositories.Staffs;
using PetCareSystem.Data.Repositories.Records;
using PetCareSystem.Data.Repositories.RecordDetails;
using PetCareSystem.Data.Repositories.Barns;
using PetCareSystem.Services.Models.Momo;
using PetCareSystem.Services.Services.Auth;
using PetCareSystem.Services.Helpers;
using PetCareSystem.Services.Services.Bookings;
using PetCareSystem.Services.Services.Serivces;
using PetCareSystem.Services.Services.Pets;
using PetCareSystem.Services.Services.Doctors;
using PetCareSystem.Services.Services.Records;
using PetCareSystem.Services.Services.Barns;
using PetCareSystem.Services.Services.Momo;
using Microsoft.OpenApi.Models;
using System.Text;
using PetCareSystem.WebApp.Helpers;

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

// Access configuration settings
var appSetting = configuration["AppSettings:Secret"];
var builder = WebApplication.CreateBuilder(args);

// Add Momo API configuration
builder.Services.Configure<MomoConfig>(builder.Configuration.GetSection("MomoAPI"));

// Add Google Authentication configuration
builder.Services.Configure<GoogleKeys>(builder.Configuration.GetSection("GoogleKeys"));

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

// Add Swagger for API documentation
builder.Services.AddSwaggerGen(swagger =>
{
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "JWT Token Authentication API",
        Description = ".NET 8 Web API"
    });
    // To Enable authorization using Swagger (JWT)
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
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
            Array.Empty<string>()
        }
    });
});

// Register the DbContext, Repositories, and Services
builder.Services.AddDbContext<PetHealthDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PetHealthCareDb")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserRolesRepository, UserRolesRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRecordRepository, RecordRepository>();
builder.Services.AddScoped<IRecordDetailRepository, RecordDetailRepository>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IBookingServiceRepository, BookingServiceRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IBarnRepository, BarnRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IServicesRepository, ServicesRepository>();
builder.Services.AddScoped<IPetRepository, PetRepository>();
builder.Services.AddScoped<IStaffRepository, StaffRepository>();
builder.Services.Configure<MomoConfig>(builder.Configuration.GetSection("MomoConfig"));

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IDoctorServices, DoctorServices>();
builder.Services.AddScoped<IBookingServices, BookingServices>();
builder.Services.AddScoped<IServiceServices, ServiceServices>();
builder.Services.AddScoped<IPetService, PetService>();
builder.Services.AddScoped<IRecordServices, RecordServices>();
builder.Services.AddScoped<IBarnService, BarnService>();
builder.Services.AddScoped<IMomoPaymentService, MomoPaymentService>();

// Configure Authentication and Authorization
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie()
.AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
{
    options.ClientId = builder.Configuration["GoogleKeys:ClientId"];
    options.ClientSecret = builder.Configuration["GoogleKeys:ClientSecret"];
    options.CallbackPath = "/signin-google"; // Ensure this matches the registered redirect URI
    options.SaveTokens = true;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSetting)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors();

app.UseHttpsRedirection();
app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

app.UseAuthentication(); // Add this line
app.UseAuthorization();  // Add this line
app.UseMiddleware<JwtMiddleware>(); // Add custom JWT middleware if necessary
app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run("http://localhost:4000");
