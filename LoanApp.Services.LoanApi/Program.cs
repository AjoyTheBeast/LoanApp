using LoanApp.Services.LoanApi;
using LoanApp.Services.LoanApi.Extensions;
using LoanApp.Services.LoanApi.Models;
using LoanApp.Services.LoanApi.Services;
using LoanApp.Services.LoanApi.Services.IService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("default")));
builder.Services.AddHttpClient("AzureFunction", x =>
{
    x.BaseAddress = new Uri(builder.Configuration["AzureFunctionUrl:ValidateBasicLoanDetailsUrl"]);
});
builder.Services.AddScoped<IAzureFunctionService, AzureFunctionService>();
builder.AddAppAuthentication();
builder.AddSwaggerGenAuthentication();

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

app.MapControllers();

app.Run();
