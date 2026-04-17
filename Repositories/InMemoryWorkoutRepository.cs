using System;
using System.Collections.Generic;
using System.Linq;
using laba1.Models;

namespace laba1.Repositories
{
    public class InMemoryWorkoutRepository : IWorkoutRepository
    {
        private readonly List<Workout> _workouts = new();
        private int _nextId = 1;

        public InMemoryWorkoutRepository()
        {
            SeedData();
        }

        private void SeedData()
        {
            Add(new Workout
            {
                Name = "Утренняя пробежка",
                Type = "Кардио",
                Duration = 45,
                Calories = 350,
                Difficulty = "Средняя",
                Instructor = "Анна Иванова",
                Date = DateTime.Today.AddDays(-2)
            });
            Add(new Workout
            {
                Name = "Силовая тренировка",
                Type = "Силовая",
                Duration = 60,
                Calories = 450,
                Difficulty = "Высокая",
                Instructor = "Михаил Петров",
                Date = DateTime.Today.AddDays(-1)
            });
            Add(new Workout
            {
                Name = "Йога для начинающих",
                Type = "Йога",
                Duration = 90,
                Calories = 200,
                Difficulty = "Низкая",
                Instructor = "Елена Сидорова",
                Date = DateTime.Today
            });
        }

        public IEnumerable<Workout> GetAll() => _workouts;
        public Workout? GetById(int id) => _workouts.FirstOrDefault(w => w.Id == id);
        public void Add(Workout workout)
        {
            workout.Id = _nextId++;
            _workouts.Add(workout);
        }
        public void Update(Workout workout)
        {
            var existing = GetById(workout.Id);
            if (existing != null)
            {
                existing.Name = workout.Name;
                existing.Type = workout.Type;
                existing.Duration = workout.Duration;
                existing.Calories = workout.Calories;
                existing.Difficulty = workout.Difficulty;
                existing.Instructor = workout.Instructor;
                existing.Date = workout.Date;
            }
        }
        public void Delete(int id)
        {
            var workout = GetById(id);
            if (workout != null)
                _workouts.Remove(workout);
        }
    }
}
