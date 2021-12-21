using Microsoft.EntityFrameworkCore;
using TNEPowerProject.Domain.Entities;

namespace TNEPowerProject.Infrastructure.Database.EFCore
{
    /// <summary>
    /// Контекст данных для EnergoDB
    /// </summary>
    public class EnergoDBContext : DbContext
    {
        /// <summary>
        /// Список типов счётчиков электрической энергии
        /// </summary>
        public DbSet<ElectricEnergyMeterType> EEMeterTypes { get; set; }
        /// <summary>
        /// Список типов трансформаторов
        /// </summary>
        public DbSet<TransformerType> TransformerTypes { get; set; }
        /// <summary>
        /// Список организаций
        /// </summary>
        public DbSet<Organization> Organizations { get; set; }
        /// <summary>
        /// Список дочерних организаций
        /// </summary>
        public DbSet<SubOrganization> SubOrganizations { get; set; }
        /// <summary>
        /// Список объектов потребления
        /// </summary>
        public DbSet<ElectricityConsumptionObject> ElectricityConsumptionObjects { get; set; }
        /// <summary>
        /// Список точек измерения электроэнергии
        /// </summary>
        public DbSet<ElectricityMeasuringPoint> ElectricityMeasuringPoints { get; set; }
        /// <summary>
        /// Список счётчиков электрической энергии
        /// </summary>
        public DbSet<ElectricEnergyMeter> EEMeters { get; set; }
        /// <summary>
        /// Список трансформаторов тока
        /// </summary>
        public DbSet<CurrentTransformer> CurrentTransformers { get; set; }
        /// <summary>
        /// Список трансформаторов напряжения
        /// </summary>
        public DbSet<VoltageTransformer> VoltageTransformers { get; set; }
        /// <summary>
        /// Список точек поставки электроэнергии
        /// </summary>
        public DbSet<ElectricitySupplyPoint> ElectricitySupplyPoints { get; set; }
        /// <summary>
        /// Список расчётных приборов учёта
        /// </summary>
        public DbSet<AccountingDeviceInfo> AccountingDeviceInfos { get; set; }
        /// <summary>
        /// Создаёт контекст данных для EnergoDB, используя передаваемые параметры DbContextOptions
        /// </summary>
        public EnergoDBContext(DbContextOptions<EnergoDBContext> options)
            : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        /// <summary>
        /// Используется для настройки связей и заполнения БД начальными данными
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TransformerType>().HasMany(t => t.CurrentTransformers).WithOne(c => c.TransformerType).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<TransformerType>().HasMany(t => t.VoltageTransformers).WithOne(v => v.TransformerType).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<ElectricEnergyMeterType>().HasMany(t => t.ElectricEnergyMeters).WithOne(e => e.EEMeterType).OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<ElectricEnergyMeter>().HasOne(e => e.EEMeterType).WithMany(t => t.ElectricEnergyMeters).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<CurrentTransformer>().HasOne(c => c.TransformerType).WithMany(t => t.CurrentTransformers).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<VoltageTransformer>().HasOne(v => v.TransformerType).WithMany(t => t.VoltageTransformers).OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<AccountingDeviceInfo>()
            //    .HasKey(a => new { a.ElectricityMeasuringPointId, a.ElectricitySupplyPointId });
            modelBuilder.Entity<AccountingDeviceInfo>()
                .HasOne(a => a.MeasuringPoint)
                .WithMany(m => m.AccountingDeviceInfos)
                .HasForeignKey(a => a.ElectricityMeasuringPointId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<AccountingDeviceInfo>()
                .HasOne(a => a.SupplyPoint)
                .WithMany(s => s.AccountingDeviceInfos)
                .HasForeignKey(a => a.ElectricitySupplyPointId)
                .OnDelete(DeleteBehavior.NoAction);

            EnergoDBSeed.SeedDB(modelBuilder);
        }
    }
}
