using Microsoft.EntityFrameworkCore;
using laba1.Models;
namespace laba1.Data
{
    public class AppDbContext : DbContext
    {
        // Конструктор, принимающий параметры подключения
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }
        // DbSet представляет таблицы в базе данных
        public DbSet<Product> Products { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        // Дополнительная настройка модели (опционально)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Настройка таблицы Products
            modelBuilder.Entity<Product>(entity =>
            {
                // Первичный ключ
                entity.HasKey(p => p.Id);
                // Настройка поля Name
                entity.Property(p => p.Name)
                .IsRequired() // NOT NULL
               .HasMaxLength(100); // VARCHAR(100)
                                   // Настройка поля Price (точность для денежных сумм)
                entity.Property(p => p.Price)
                .HasColumnType("decimal(18,2)");
                // Индекс для быстрого поиска по категории
                entity.HasIndex(p => p.Category)
                .HasDatabaseName("IX_Products_Category");
            });

            modelBuilder.Entity<Workout>(entity =>
            {
                entity.HasKey(w => w.Id);
                entity.Property(w => w.Name).IsRequired().HasMaxLength(100);
                entity.Property(w => w.Type).IsRequired().HasMaxLength(50);
                entity.Property(w => w.Difficulty).IsRequired().HasMaxLength(20);
                entity.Property(w => w.Instructor).HasMaxLength(100);
            });
        }
    }
}