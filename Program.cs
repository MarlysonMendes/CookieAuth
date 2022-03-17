using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = "GoogleOpenId";
    //options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;

}).AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/denied";
    })
 .AddOpenIdConnect("GoogleOpenId", options =>
 {
     options.Authority = "https://accounts.google.com";
     options.ClientId = "890344721081-h3govp4p4mjhu4k0ajt32c20fhqa2in4.apps.googleusercontent.com";
     options.ClientSecret = "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";
     options.CallbackPath = "/auth";
     options.SaveTokens = true;
 });
    //.AddGoogle(options =>
    //{
    //    options.ClientId = "890344721081-h3govp4p4mjhu4k0ajt32c20fhqa2in4.apps.googleusercontent.com";
    //    options.ClientSecret = "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";
    //    options.CallbackPath = "/auth";
    //    options.AuthorizationEndpoint += "?prompt=consent";
    //});

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
