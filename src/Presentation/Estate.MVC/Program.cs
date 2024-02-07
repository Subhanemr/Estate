using Estate.Application.ServiceRegistrations;
using Estate.Infrastructure.ServiceRegistrations;
using Estate.Persistance.Contexts;
using Estate.Persistance.ServiceRegistrations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices();

builder.Services.ConfigureApplicationCookie(opt => opt.LoginPath = $"/Account/Login/{opt.ReturnUrlParameter}");
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<AppDbContextInitializer>();
    initializer.InitializeDbContextAsync().Wait();
    initializer.CreateUserRolesAsync().Wait();
    initializer.InitializeAdminAsync().Wait();
    initializer.InitializeModeratorAsync().Wait();
}

app.UseRouting();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

//app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "/{controller=Home}/{action=Index}/{id?}"
    );
});

app.Run();
