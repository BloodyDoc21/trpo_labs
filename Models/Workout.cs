using System.ComponentModel.DataAnnotations;

namespace laba1.Models
{
    public class Workout
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название обязательно")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Название от 2 до 100 символов")]
        [Display(Name = "Название")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Тип обязателен")]
        [RegularExpression(@"^(Силовая|Кардио|Йога|Пилатес)$", ErrorMessage = "Тип: Силовая, Кардио, Йога или Пилатес")]
        [Display(Name = "Тип")]
        public string Type { get; set; } = string.Empty;

        [Required(ErrorMessage = "Длительность обязательна")]
        [Range(5, 180, ErrorMessage = "Длительность от 5 до 180 минут")]
        [Display(Name = "Длительность (мин)")]
        public int Duration { get; set; }

        [Required(ErrorMessage = "Калории обязательны")]
        [Range(10, 2000, ErrorMessage = "Калории от 10 до 2000")]
        [Display(Name = "Калории")]
        public int Calories { get; set; }

        [Required(ErrorMessage = "Сложность обязательна")]
        [RegularExpression(@"^(Низкая|Средняя|Высокая)$", ErrorMessage = "Сложность: Низкая, Средняя или Высокая")]
        [Display(Name = "Сложность")]
        public string Difficulty { get; set; } = string.Empty;

        [StringLength(100)]
        [Display(Name = "Инструктор")]
        public string Instructor { get; set; } = string.Empty;

        [Required(ErrorMessage = "Дата обязательна")]
        [DataType(DataType.Date)]
        [Display(Name = "Дата")]
        public DateTime Date { get; set; }

        public double GetIntensity() => Duration > 0 ? (double)Calories / Duration : 0;
    }
}
