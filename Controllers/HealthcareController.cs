using Microsoft.AspNetCore.Mvc;
using laba1.Models;


namespace laba1.Controllers
{
    public class HealthcareController : Controller
    {
        private static readonly Doctor[] DoctorsData =
 {
    new Doctor { Id = 1, Name = "Иванова Анна Петровна", Specialization = "Терапевт", ExperienceYears = 12 },
    new Doctor { Id = 2, Name = "Сидоров Михаил Владимирович", Specialization = "Хирург", ExperienceYears = 18 },
    new Doctor { Id = 3, Name = "Козлова Елена Сергеевна", Specialization = "Педиатр", ExperienceYears = 8 },
    new Doctor { Id = 4, Name = "Петров Дмитрий Александрович", Specialization = "Кардиолог", ExperienceYears = 15 },
    new Doctor { Id = 5, Name = "Новикова Ольга Игоревна", Specialization = "Невролог", ExperienceYears = 10 }
};


        public IActionResult Doctors()
        {
            ViewBag.Doctors = DoctorsData;
            ViewData["PageTitle"] = "Наши врачи";
            return View();
        }


        public IActionResult Doctor(int? doctorId)
        {
            if (!doctorId.HasValue || doctorId.Value < 1)
            {
                ViewData["Specialization"] = "";
                ViewData["ExperienceYears"] = 0;
                ViewBag.Doctor = null;
                return View();
            }

            var doctor = System.Array.Find(DoctorsData, d => d.Id == doctorId.Value);
            if (doctor == null || doctor.Id == 0)
            {
                ViewData["Specialization"] = "";
                ViewData["ExperienceYears"] = 0;
                ViewBag.Doctor = null;
                return View();
            }

            ViewBag.Doctor = doctor;
            ViewData["Specialization"] = doctor.Specialization;
            ViewData["ExperienceYears"] = doctor.ExperienceYears;
            ViewData["PageTitle"] = doctor.Name;
            return View();
        }

        public IActionResult Services()
        {
            ViewBag.Services = new[]
            {
                ("Консультация терапевта", 1500),
                ("Приём хирурга", 2000),
                ("Приём педиатра", 1800),
                ("ЭКГ с расшифровкой", 800),
                ("УЗИ (одна зона)", 1200),
                ("Анализы (клинический анализ крови)", 500)
            };
            ViewData["PageTitle"] = "Медицинские услуги";
            return View();
        }

        public IActionResult Appointment()
        {
            ViewData["PageTitle"] = "Запись на приём";
            ViewBag.Doctors = DoctorsData;
            return View();
        }
    }
}
