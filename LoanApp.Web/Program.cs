using LoanApp.Web.Service;
using LoanApp.Web.Service.IService;
using LoanApp.Web.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

SD.LoanApiBaseUrl = builder.Configuration["ServiceUrls:LoanApiBaseUrl"];

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient<ILoanService, LoanService>();
builder.Services.AddScoped<ILoanService, LoanService>();
builder.Services.AddScoped<IBaseService, BaseService>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
