﻿

using Client.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System.Configuration;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddHttpContextAccessor();

builder.Services.Configure<IdentityServerSettings>(builder.Configuration.GetSection("IdentityServerSettings"));
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddHttpClient();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
.AddOpenIdConnect(
    OpenIdConnectDefaults.AuthenticationScheme,
    options =>
    {
         options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
         options.SignOutScheme = OpenIdConnectDefaults.AuthenticationScheme;
         options.Authority = builder.Configuration["InteractiveServiceSettings:AuthorityUrl"];
         options.ClientId = builder.Configuration["InteractiveServiceSettings:ClientId"];
         options.ClientSecret = builder.Configuration["InteractiveServiceSettings:ClientSecret"];

         options.ResponseType = "code";
         options.SaveTokens = true;
         options.GetClaimsFromUserInfoEndpoint = true;
         options.RequireHttpsMetadata = false;


     }
 );

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
    pattern: "{controller=Home}/{action=Index}/{id?}").RequireAuthorization();






app.Run();
