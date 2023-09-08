using Courses.Models.Db;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddControllersWithViews();
services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");
services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

if (builder.Environment.IsProduction())
{
    builder.Services.AddDbContext<CoursesDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString(DbConstants.DbConnectionString));
        options.UseSnakeCaseNamingConvention();
    });
}
else
{
    builder.Services.AddDbContext<CoursesDbContext>(options =>
    {
        options.UseSqlite("Filename=Courses.db");
        options.UseSnakeCaseNamingConvention();
    });
}

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
    pattern: "{controller=StudyGroups}/{action=GetAllGroups}/{id?}");

new DbInitializer().InitDb(app);

app.Run();