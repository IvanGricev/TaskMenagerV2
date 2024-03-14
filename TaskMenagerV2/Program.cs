using Microsoft.EntityFrameworkCore;
using TaskMenager.Components.services;
using TaskMenager.Components.Services;
using TaskMenagerV2.Pages.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("MyDatabase"),
        new MySqlServerVersion(new Version(8, 0, 21))));

builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ITaskService, Taskservice>();
builder.Services.AddScoped<IAchievementsService, AchievementsService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddDistributedMemoryCache(); // Используйте другой кэш, если это необходимо
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Добавление MVC и других сервисов
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Использование сессий
app.UseSession();

// Остальная конфигурация
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
