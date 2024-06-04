using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PetCareSystem.Data.EF;
using PetCareSystem.Data.Repositories.Bookings;
using PetCareSystem.Data.Repositories.Users;
using PetCareSystem.Services;
using PetCareSystem.Services.Auth;
using PetCareSystem.Services.Helpers;
using PetCareSystem.Services.Services.Bookings;
using System.Text;
using Microsoft.AspNetCore.Mvc.ViewFeatures;




IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

// Access configuration settings
var appSetting = configuration["AppSettings:Secret"];
var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<GoogleKeys>(builder.Configuration.GetSection("GoogleKeys"));


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
builder.Services.AddSwaggerGen();

// Register the DbContext, Repositories, and Services
builder.Services.AddDbContext<PetHealthDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PetHealthCareDb")));

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<ITempDataDictionaryFactory, TempDataDictionaryFactory>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IBookingServices, BookingServices>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();

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
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});
app.MapControllers();
app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run("http://localhost:4000");
