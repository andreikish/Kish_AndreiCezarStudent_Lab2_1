using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Kish_AndreiCezarStudent_Lab2.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Kish_AndreiCezarStudent_Lab2Context>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Kish_AndreiCezarStudent_Lab2Context")
        ?? throw new InvalidOperationException("Connection string 'Kish_AndreiCezarStudent_Lab2Context' not found."),
        sqlOptions =>
        {
            // Reattempt transient SQL errors (e.g., LocalDB cold start).
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(10),
                errorNumbersToAdd: null);
        }));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    DbInitializer.Initialize(services);
}

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
    pattern: "{controller=Books}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
