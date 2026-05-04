using laba1.Data;
using laba1.Models;

namespace laba1.Repositories
{
    public class EfWorkoutRepository : IWorkoutRepository
    {
        private readonly AppDbContext _context;

        public EfWorkoutRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Workout> GetAll()
        {
            return _context.Workouts.ToList();
        }

        public Workout? GetById(int id)
        {
            return _context.Workouts.Find(id);
        }

        public void Add(Workout workout)
        {
            _context.Workouts.Add(workout);
            _context.SaveChanges();
        }

        public void Update(Workout workout)
        {
            _context.Workouts.Update(workout);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var workout = GetById(id);
            if (workout != null)
            {
                _context.Workouts.Remove(workout);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Workout> GetByDuration(int min, int max)
        {
            return _context.Workouts
                .Where(w => w.Duration >= min && w.Duration <= max)
                .ToList();
        }

        public IEnumerable<Workout> GetTopCalories(int count)
        {
            return _context.Workouts
                .OrderByDescending(w => w.Calories)
                .Take(count)
                .ToList();
        }

        public IEnumerable<Workout> Search(string term)
        {
            return _context.Workouts
                .Where(w => w.Name.Contains(term) || w.Type.Contains(term))
                .ToList();
        }

        public double GetAverageCalories()
        {
            return _context.Workouts.Average(w => w.Calories);
        }

        public IEnumerable<IGrouping<string, Workout>> GroupByType()
        {
            return _context.Workouts
                .GroupBy(w => w.Type)
                .ToList();
        }



        public IEnumerable<Workout> GetWithPagination(int page, int pageSize)
        {
            return _context.Workouts
                .OrderBy(w => w.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public int GetTotalPages(int pageSize)
        {
            var count = _context.Workouts.Count();
            return (int)Math.Ceiling(count / (double)pageSize);
        }
    }
}
