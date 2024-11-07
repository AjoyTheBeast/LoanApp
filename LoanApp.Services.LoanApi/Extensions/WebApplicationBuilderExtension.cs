using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace LoanApp.Services.LoanApi.Extensions
{
    public static class WebApplicationBuilderExtension
    {
        public static WebApplicationBuilder AddAppAuthentication(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["ApiSettings:JwtOptions:Issuer"],
                        ValidAudience = builder.Configuration["ApiSettings:JwtOptions:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["ApiSettings:JwtOptions:ClientSecretKey"]))
                    };
                    //// Necessary for SignalR authentication
                    //options.Events = new JwtBearerEvents
                    //{
                    //    OnMessageReceived = context =>
                    //    {
                    //        var accessToken = context.Request.Cookies["JwtToken"];

                    //        // If the request is for our SignalR hub, we get the token from the query string
                    //        var path = context.HttpContext.Request.Path;
                    //        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs/loanStatus"))
                    //        {
                    //            context.Token = accessToken;
                    //        }
                    //        return Task.CompletedTask;
                    //    }
                    //};
                });
            return builder;
        }
        public static WebApplicationBuilder AddSwaggerGenAuthentication(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "LoanApp", Version = "v1" });
                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Description = JwtBearerDefaults.AuthenticationScheme + " Jwt-token",
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
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });
            return builder;
        }
    }
}
