using Microsoft.EntityFrameworkCore;
using TaskMenager.Components.services;
using TaskMenager.Components.Services;
using TaskMenagerV2.Pages.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("MyDatabase"),
        new MySqlServerVersion(new Version(8, 0, 21))));

builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ITaskService, Taskservice>();
builder.Services.AddScoped<IAchievementsService, AchievementsService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ProjectService>();
builder.Services.AddScoped<Taskservice>();

builder.Services.AddSingleton(new EmailService("smtp.yandex.ru", 465, "ivangricev@yandex.com", "zglmlrdoheigzhmk"));

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseSession();

app.UseRouting();


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
