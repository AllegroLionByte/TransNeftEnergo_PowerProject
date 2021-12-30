using Microsoft.EntityFrameworkCore;
using TNEPowerProject.Logics.Services;
using Microsoft.Extensions.DependencyInjection;
using TNEPowerProject.Logics.Interfaces.Services;
using TNEPowerProject.Infrastructure.Database.EFCore;

namespace TNEPowerProject.Logics.Extensions
{
    /// <summary>
    /// Расширение для проекта Trans Neft Energo Power Project, позволяющее зарегистрировать необходимые сервисы и контекст БД
    /// </summary>
    public static class TNEServicesExtensions
    {
        /// <summary>
        /// Позволяет зарегистрировать необходимые для работы проекта TNE PP сервисы
        /// </summary>
        public static IServiceCollection RegisterTNEServices(this IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddScoped<ITransformerTypesService, TransformerTypesService>();
            serviceDescriptors.AddScoped<IAccountingDeviceInfosService, AccountingDeviceInfosService>();
            serviceDescriptors.AddScoped<IElectricityConsumptionObjectsService, ElectricityConsumptionObjectsService>();
            serviceDescriptors.AddScoped<IElectricEnergyMeterTypesService, ElectricEnergyMeterTypesService>();
            serviceDescriptors.AddScoped<IElectricityMeasuringPointsService, ElectricityMeasuringPointsService>();
            serviceDescriptors.AddScoped<IElectricEnergyMetersService, ElectricEnergyMetersService>();
            serviceDescriptors.AddScoped<ICurrentTransformersService, CurrentTransformersService>();
            serviceDescriptors.AddScoped<IVoltageTransformersService, VoltageTransformersService>();
            serviceDescriptors.AddScoped<IExpiredElectricEquipmentService, ExpiredElectricEquipmentService>();
            return serviceDescriptors;
        }
        /// <summary>
        /// Позволяет зарегистрировать необходимые для работы проекта TNE PP сервисы
        /// </summary>
        public static IServiceCollection RegisterTNEDataBaseContext(this IServiceCollection serviceDescriptors, string energoDBConnectionString)
        {
            serviceDescriptors.AddDbContext<EnergoDBContext>(options => options.UseLazyLoadingProxies().UseSqlServer(energoDBConnectionString));
            return serviceDescriptors;
        }
    }
}
