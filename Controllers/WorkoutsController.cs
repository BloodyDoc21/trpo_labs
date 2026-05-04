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

        public IActionResult ByDuration(int min, int max)
        {
            var data = _repository.GetByDuration(min, max);

            ViewBag.Min = min;
            ViewBag.Max = max;

            return View(data);
        }

        public IActionResult TopCalories(int count = 5)
        {
            var data = _repository.GetTopCalories(count);

            ViewBag.Count = count;

            return View(data);
        }

        public IActionResult Search(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
                return View(new List<Workout>()); // ❗ НЕ редирект

            var data = _repository.Search(term);
            ViewBag.Term = term;

            return View(data);
        }

        public IActionResult Statistics()
        {
            var avg = _repository.GetAverageCalories();
            return View(avg);
        }

        public IActionResult Grouped()
        {
            var data = _repository.GroupByType();
            return View(data);
        }

        public IActionResult Paginated(int page = 1)
        {
            int pageSize = 5;

            var data = _repository.GetWithPagination(page, pageSize);
            var totalPages = _repository.GetTotalPages(pageSize);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(data);
        }
    }
}
