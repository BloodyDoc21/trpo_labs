using System.Collections.Generic;
using laba1.Models;

namespace laba1.Repositories
{
    public interface IWorkoutRepository
    {
        IEnumerable<Workout> GetAll();
        Workout? GetById(int id);
        void Add(Workout workout);
        void Update(Workout workout);
        void Delete(int id);

        IEnumerable<Workout> GetByDuration(int min, int max);
        IEnumerable<Workout> GetTopCalories(int count);
        IEnumerable<Workout> Search(string term);
        double GetAverageCalories();
        IEnumerable<IGrouping<string, Workout>> GroupByType();
        IEnumerable<Workout> GetWithPagination(int page, int pageSize);
        int GetTotalPages(int pageSize);
    }
}
