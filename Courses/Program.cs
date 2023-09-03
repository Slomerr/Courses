using Courses.Models;
using Courses.Models.Db;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
//services.AddControllersWithViews();
services.AddControllers();
services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

services.AddDbContext<CoursesDbContext>(options =>
{
    options.UseSqlite("Filename=Courses.db");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=StudyGroups}/{action=GetAll}/{id?}");

using (var serviceScope = app.Services.CreateScope())
{
    serviceScope.ServiceProvider.GetService<CoursesDbContext>()?.Database.Migrate();
}

app.Run();