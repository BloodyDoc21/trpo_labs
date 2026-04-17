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
    }
}
