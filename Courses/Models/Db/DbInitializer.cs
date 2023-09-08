using Microsoft.EntityFrameworkCore;

namespace Courses.Models.Db;

public class DbInitializer
{
    public void InitDb(WebApplication app)
    {
        using (var serviceScope = app.Services.CreateScope())
        {
            var dbContext = serviceScope.ServiceProvider.GetService<CoursesDbContext>();
            if (dbContext.Database.GetPendingMigrations().Any())
            {
                dbContext.Database.Migrate();
            }

            AddDefaultData(dbContext);
        }
    }

    private void AddDefaultData(CoursesDbContext dbContext)
    {
        if (!dbContext.Courses.Any())
        {
            dbContext.Courses.Add(new Course() { CourseName = "Повышение квалификации" });
            
            var teacher1 = new Teacher(){FullName = "Владимиров Д. О.", Email = "vladimirov@work.com"};
            var teacher2 = new Teacher(){FullName = "Жданов Д. Г.", Email = "jdanov@work.com"};
            var teacher3 = new Teacher(){FullName = "Карташова Е. М.", Email = "koshtanov@work.com"};
            dbContext.Teachers.AddRange(new []
            {
                teacher1,
                teacher2,
                teacher3
            });
            
            var organization1 = new Organization(){Inn = "1111111111", Name = "РЖД", Teacher = teacher1};
            var organization2 = new Organization(){Inn = "2222222222", Name = "Ростелеком", Teacher = teacher1};
            var organization3 = new Organization(){Inn = "3333333333", Name = "МТС", Teacher = teacher2};
            var organization4 = new Organization(){Inn = "4444444444", Name = "Аэрофлот", Teacher = teacher2};
            var organization5 = new Organization(){Inn = "5555555555", Name = "Сбербанк", Teacher = teacher3};
            
            dbContext.Organizations.AddRange(new []
            {
                organization1,
                organization2,
                organization3,
                organization4,
                organization5
            });
            
            dbContext.Employees.AddRange(new []
            {
                new Employee(){FullName = "Климова А. Ю.", Organization = organization1},
                new Employee(){FullName = "Грачев Г. Д.", Organization = organization1},
                new Employee(){FullName = "Михайлов И. К.", Organization = organization1},
                new Employee(){FullName = "Медведева М. С.", Organization = organization1},
                new Employee(){FullName = "Осипов Я. Д.", Organization = organization1},
                new Employee(){FullName = "Дроздов Р. А.", Organization = organization1},
                new Employee(){FullName = "Аксенова В. К.", Organization = organization1},
                new Employee(){FullName = "Трофимова Е. М.", Organization = organization2},
                new Employee(){FullName = "Кузнецова В. А.", Organization = organization2},
                new Employee(){FullName = "Жаров В. А.", Organization = organization2},
                new Employee(){FullName = "Яшина Е. М.", Organization = organization2},
                new Employee(){FullName = "Сафонова А. И.", Organization = organization2},
                new Employee(){FullName = "Карасев Д. М.", Organization = organization2},
                new Employee(){FullName = "Севастьянова Е. Д.", Organization = organization2},
                new Employee(){FullName = "Петрова Э. М.", Organization = organization3},
                new Employee(){FullName = "Ушаков С. Л.", Organization = organization3},
                new Employee(){FullName = "Мельникова С. М.", Organization = organization3},
                new Employee(){FullName = "Мальцева О. М.", Organization = organization3},
                new Employee(){FullName = "Филимонова А. Д.", Organization = organization3},
                new Employee(){FullName = "Воронцова Т. М.", Organization = organization3},
                new Employee(){FullName = "Захарова Э. М.", Organization = organization3},
                new Employee(){FullName = "Исаева А. А.", Organization = organization4},
                new Employee(){FullName = "Кузнецова Э. Н.", Organization = organization4},
                new Employee(){FullName = "Полякова В. Я.", Organization = organization4},
                new Employee(){FullName = "Мельникова А. С.", Organization = organization4},
                new Employee(){FullName = "Крючков М. Б.", Organization = organization4},
                new Employee(){FullName = "Иванова Е. Д.", Organization = organization4},
                new Employee(){FullName = "Афанасьев П. Н.", Organization = organization4},
                new Employee(){FullName = "Широков А. Т.", Organization = organization4},
                new Employee(){FullName = "Михайлова М. П.", Organization = organization5},
                new Employee(){FullName = "Романов Е. К.", Organization = organization5},
                new Employee(){FullName = "Никольская Д. М.", Organization = organization5},
                new Employee(){FullName = "Алексеев Т. М.", Organization = organization5},
                new Employee(){FullName = "Комарова К. Д.", Organization = organization5},
                new Employee(){FullName = "Серов А. Е.", Organization = organization5}
            });

            dbContext.SaveChanges();
        }
    }
}