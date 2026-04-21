using laba1.Data;
using laba1.Models;
namespace laba1.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(AppDbContext context)
        {
            var seedWorkouts = new[]
            {
                new Workout
                {
                    Name = "Утренняя пробежка",
                    Type = "Кардио",
                    Duration = 45,
                    Calories = 350,
                    Difficulty = "Средняя",
                    Instructor = "Анна Иванова",
                    Date = DateTime.Today.AddDays(-2)
                },
                new Workout
                {
                    Name = "Силовая тренировка",
                    Type = "Силовая",
                    Duration = 60,
                    Calories = 500,
                    Difficulty = "Высокая",
                    Instructor = "Михаил Петров",
                    Date = DateTime.Today.AddDays(-1)
                },
                new Workout
                {
                    Name = "Йога для начинающих",
                    Type = "Йога",
                    Duration = 90,
                    Calories = 220,
                    Difficulty = "Низкая",
                    Instructor = "Елена Сидорова",
                    Date = DateTime.Today
                }
            };

            var existingNames = context.Workouts
                .Select(w => w.Name)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            var missingWorkouts = seedWorkouts
                .Where(w => !existingNames.Contains(w.Name))
                .ToArray();

            if (missingWorkouts.Length == 0)
                return;

            await context.Workouts.AddRangeAsync(missingWorkouts);
            await context.SaveChangesAsync();
        }
    }
}