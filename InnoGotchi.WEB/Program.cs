using InnoGotchi.Http.Components;
using InnoGotchi.Http.HttpServices;
using InnoGotchi.Http.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews()
    .AddSessionStateTempDataProvider();

builder.Services.AddHttpClient<IIdentityService, IdentityService>();

builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.ConfigurationHttpServices();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
