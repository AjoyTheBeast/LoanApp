using LoanApp.Web.Service;
using LoanApp.Web.Service.IService;
using LoanApp.Web.Utility;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

SD.LoanApiBaseUrl = builder.Configuration["ServiceUrls:LoanApiBaseUrl"];
SD.AuthApiBaseUrl = builder.Configuration["ServiceUrls:AuthApiBaseUrl"];

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient<ILoanService, LoanService>();
builder.Services.AddScoped<ILoanService, LoanService>();
builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenProvider, TokenProvider>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
        options.LoginPath = "/Auth/Login";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=DashBoard}/{action=Index}/{id?}");

app.Run();
