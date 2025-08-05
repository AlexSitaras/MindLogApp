using Microsoft.EntityFrameworkCore;
using MindLog.Data;
using MindLog.Mapping;
using MindLog.Middleware;
using MindLog.Services.Implementations;
using MindLog.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()
    )
);




builder.Services.AddScoped<IMoodEntryService, MoodEntryService>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=MoodEntries}/{action=Index}/{id?}");

app.Run();
