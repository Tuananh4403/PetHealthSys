using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PetCareSystem.Data.EF;
using PetCareSystem.Data.Repositories.Bookings;
using PetCareSystem.Data.Repositories.Users;
using PetCareSystem.Services.Services.Auth;
using PetCareSystem.Services.Helpers;
using PetCareSystem.Services.Services.Bookings;
using System.Text;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using PetCareSystem.Data.Repositories.Customers;
using PetCareSystem.Services.Services.Serivces;
using PetCareSystem.Data.Repositories.Services;
using PetCareSystem.Services.Services.Pets;
using PetCareSystem.Data.Entites;
using PetCareSystem.Data.Repositories.Pets;
using PetCareSystem.Data.Repositories.Roles;
using PetCareSystem.Data.Repositories.Doctors;
using PetCareSystem.WebApp.Helpers;
using Microsoft.OpenApi.Models;
using PetCareSystem.Data.Repositories.BookingServices;
using PetCareSystem.Data.Repositories.UserRoles;
using PetCareSystem.Data.Repositories.Staffs;
using PetCareSystem.Services.Services.Doctors;


IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

// Access configuration settings
var appSetting = configuration["AppSettings:Secret"];
var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<GoogleKeys>(builder.Configuration.GetSection("GoogleKeys"));
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddAuthentication(option =>
{
    option.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
    .AddCookie()
    .AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
    {
        options.ClientId = "548056226336-9r91b1s78lcvvefd4chijuo0hb09gs25.apps.googleusercontent.com";
        options.ClientSecret = "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";
        options.CallbackPath = "/signin-google"; // Ensure this matches the registered redirect URI
        options.SaveTokens = true;
    });

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(swagger =>
{
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "JWT Token Authentication API",
        Description = ".NET 8 Web API"
    });
    // To Enable authorization using Swagger (JWT)
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
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
}
);

// Register the DbContext, Repositories, and Services
builder.Services.AddDbContext<PetHealthDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PetHealthCareDb")));

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<ITempDataDictionaryFactory, TempDataDictionaryFactory>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserRolesRepository, UserRolesRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IBookingServiceRepository, BookingServiceRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IServicesRepository, ServicesRepository>();
builder.Services.AddScoped<IPetRepository, PetRepository>();
builder.Services.AddScoped<IStaffRepository, StaffRepository>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IDoctorServices, DoctorServices>();
builder.Services.AddScoped<IBookingServices, BookingServices>();
builder.Services.AddScoped<IServiceServices, ServiceServices>();
builder.Services.AddScoped<IPetService, PetService>();


// Configure JWT authentication
var key = Encoding.ASCII.GetBytes(appSetting);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
builder.Services.AddCors();
var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());


app.UseHttpsRedirection();
app.UseRouting();
app.UseSwagger();

app.UseAuthentication(); // Add this line
app.UseAuthorization(); // Add this line
app.UseMiddleware<JwtMiddleware>();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});
app.MapControllers();
app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run("http://*:4000");

