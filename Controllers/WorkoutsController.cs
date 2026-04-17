using Microsoft.AspNetCore.Mvc;
using laba1.Models;
using laba1.Repositories;

namespace laba1.Controllers
{
    public class WorkoutsController : Controller
    {
        private readonly IWorkoutRepository _repository;

        public WorkoutsController(IWorkoutRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View(_repository.GetAll());
        }

        public IActionResult Details(int id)
        {
            var workout = _repository.GetById(id);
            if (workout == null)
                return NotFound();
            return View(workout);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Workout workout)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(workout);
                TempData["SuccessMessage"] = "Тренировка успешно добавлена!";
                return RedirectToAction(nameof(Index));
            }
            return View(workout);
        }

        public IActionResult Edit(int id)
        {
            var workout = _repository.GetById(id);
            if (workout == null)
                return NotFound();
            return View(workout);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Workout workout)
        {
            if (id != workout.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                _repository.Update(workout);
                TempData["SuccessMessage"] = "Тренировка успешно обновлена!";
                return RedirectToAction(nameof(Index));
            }
            return View(workout);
        }

        public IActionResult Delete(int id)
        {
            var workout = _repository.GetById(id);
            if (workout == null)
                return NotFound();
            return View(workout);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _repository.Delete(id);
            TempData["SuccessMessage"] = "Тренировка удалена!";
            return RedirectToAction(nameof(Index));
        }
    }
}
