using Microsoft.EntityFrameworkCore;
using UCMS.Data;
using UCMS.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    // Apply filters globally to all controllers
    options.Filters.Add<FooterFilter>();
    options.Filters.Add<GlobalExceptionFilter>();
});

// Register filters for dependency injection
builder.Services.AddScoped<ActivityLogFilter>();
builder.Services.AddScoped<FooterFilter>();
builder.Services.AddScoped<GlobalExceptionFilter>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=ucms.db"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// Seeding the database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    SeedData.Initialize(context);
}

app.Run();
