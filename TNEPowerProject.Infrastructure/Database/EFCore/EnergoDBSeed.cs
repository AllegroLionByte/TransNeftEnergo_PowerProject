using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TNEPowerProject.Domain.Entities;

namespace TNEPowerProject.Infrastructure.Database.EFCore
{
    /// <summary>
    /// Обеспечивает инициализацию БД EnergoDB тестовыми данными
    /// </summary>
    internal static class EnergoDBSeed
    {
        /// <summary>
        /// Определяет общее количество родительских организаций
        /// </summary>
        private const int Count_Organizations = 10;
        /// <summary>
        /// Определяет максимально возможное количество дочерних организаций у каждой родительской организации
        /// </summary>
        private const int Max_SubOrganizations = 8;
        /// <summary>
        /// Определяет максимально возможное количество объектов потребления у дочерней организации
        /// </summary>
        private const int Max_ConsumptionObjects = 3;
        /// <summary>
        /// Определяет максимально возможное количество точек измерения электроэнергии
        /// </summary>
        private const int Max_ElectricityMeasuringPoints = 14;
        /// <summary>
        /// Определяет максимально возможное количество точек поставки электроэнергии
        /// </summary>
        private const int Max_ElectricitySupplyPoints = 5;
        /// <summary>
        /// Определяет максимально возможное количество расчётных приборов учёта для точек поставки электроэнергии
        /// </summary>
        private const int Max_AccountingDevices = 30;

        private static string[] OrganizationNames_First => new string[] { "Рос", "Глав", "Мос", "Газ", "Авиа", "Инж", "Жмых", "Стар", "Старт" };
        private static string[] OrganizationNames_Second => new string[] { "Телеком", "Энерго", "Проект", "НефтеПром", "Строй", "Пром", "Сети",
            "Технологии", "Перспектива", "Ком" };
        private static string[] Addresses_Cities => new string[] { "Москва", "Санкт-Петербург", "Архангельск", "Магнитогорск", "Ялта",
            "Находка", "Нижний Новгород", "Новороссийск", "Минеральные Воды", "Комсомольск-на-Амуре", "Краснодар", "Кисловодск",
            "Ханты-Мансийск", "Хабаровск", "Казань", "Калининград", "Иркутск", "Екатеринбург", "Чита", "Мурманск", "Новокузнецк", "Астрахань" };
        private static string[] Addresses_Streets => new string[] { "ул. Ленина", "Проектируемый проезд", "Инженерная улица", "Путевой проезд",
            "Улица Авиаконструктора Микояна", "Улица Авиаконструктора Миля", "Улица Авиаконструктора Сухого", "Улица Авиаконструктора Яковлева",
            "Улица Академика Королёва", "Красноармейская улица", "Кожевнический проезд", "Улица Строителей", "Мирная улица",
            "Энергетическая улица", "Энергетический проезд", "Электрозаводская улица", "Площадь победы", "Улица Ветеранов Победы",
            "Проспект Ветеранов", "Проспект Маршала Жукова", "Улица Маршала Вершинина", "Кутузовский проспект" };
        private static string[] EETypes_General => new string[] { "Электромеханический", "Электронный", "Гибридный" };
        private static string[] EETypes_Phases => new string[] { "однофазный", "трёхфазный" };
        private static string[] EETypes_Tariff => new string[] { "однотарифный", "многотарифный" };
        private static string[] TransformerTypes_Current => new string[] { "Тороидальный", "Сухой", "Высоковольтный масляный",
            "Высоковольтный газовый", "Переносной", "Накладной", "Встраиваемый" };
        private static string[] TransformerTypes_Voltage => new string[] { "Двухобмоточный", "Ттрехобмоточный", "Заземляемый",
            "Незаземляемый", "Каскадный", "Ёмкостный", "Антирезонансный" };
        private static string[] Point_Names1 => new string[] { "ГРЩ", "РУ", "ВУ", "ВРУ" };
        private static string[] Point_Names2 => new string[] { "С", "СВ", "В", "ЮВ", "Ю", "ЮЗ", "З", "СЗ" };
        private static double[] Voltages => new double[] { 0.11, 0.22, 0.38, 0.66, 1.14, 6, 10, 35, 110, 250, 500 };
        private static string[] ConsObj_Names1 => new string[] { "ПС", "ЭС", "ВУ", "РО", "Ш", "РНДЗ", "ОДЗ", "ТВТ", "КТП" };
        private static string[] ConsObj_Names2 => new string[] { "Весна", "Лето", "Москва", "Полесское", "Лянтор", "Высокиничи", "Лена", "Ока", "Волга" };

        public static void SeedDB(ModelBuilder modelBuilder)
        {
            Random rnd = new Random();
            List<ElectricEnergyMeterType> eEMeterTypes = new List<ElectricEnergyMeterType>();
            List<TransformerType> transformerTypes = new List<TransformerType>();

            List<Organization> organizations = new List<Organization>();
            List<SubOrganization> subOrganizations = new List<SubOrganization>();
            List<ElectricityConsumptionObject> eConsumptionObjects = new List<ElectricityConsumptionObject>();
            List<ElectricityMeasuringPoint> eMeasuringPoints = new List<ElectricityMeasuringPoint>();
            List<ElectricEnergyMeter> eEMeters = new List<ElectricEnergyMeter>();
            List<CurrentTransformer> currentTransformers = new List<CurrentTransformer>();
            List<VoltageTransformer> voltageTransformers = new List<VoltageTransformer>();
            List<ElectricitySupplyPoint> eSupplyPoints = new List<ElectricitySupplyPoint>();
            List<AccountingDeviceInfo> accountingDeviceInfos = new List<AccountingDeviceInfo>();


            // Начальные значения Id
            int organizationId = 1;
            int subOrganizationId = 1;
            int eEMeterTypeId = 1;
            int transformerTypeId = 1;
            int consumptionObjectId = 1;
            int measuringPointId = 1;
            int supplyPointId = 1;
            int accountingDeviceInfoId = 1;

            // Заполнение типов счётчиков
            for (int i = 0; i < EETypes_General.Length; i++)
                for (int j = 0; j < EETypes_Phases.Length; j++)
                    for (int k = 0; k < EETypes_Tariff.Length; k++)
                        eEMeterTypes.Add(new ElectricEnergyMeterType()
                        {
                            Id = eEMeterTypeId++,
                            Description = $"{EETypes_General[i]} {EETypes_Phases[j]} {EETypes_Tariff[k]}"
                        });

            // Заполнение типов трансформаторов
            for (int i = 0; i < TransformerTypes_Current.Length; i++)
                transformerTypes.Add(new TransformerType()
                {
                    Id = transformerTypeId++,
                    Description = TransformerTypes_Current[i],
                    Purpose = TransformerType.TransformerPurpose.Current
                });
            for (int i = 0; i < TransformerTypes_Voltage.Length; i++)
                transformerTypes.Add(new TransformerType()
                {
                    Id = transformerTypeId++,
                    Description = TransformerTypes_Voltage[i],
                    Purpose = TransformerType.TransformerPurpose.Voltage
                });

            // Заполнение организаций
            for (; organizationId <= Count_Organizations; organizationId++)
                organizations.Add(new Organization()
                {
                    Id = organizationId,
                    Name = GetRandomOrganizationName(rnd),
                    Address = GetRandomAddress(rnd)
                });

            // Заполнение дочерних организаций
            foreach (Organization org in organizations)
            {
                int subOrganizationsCount = rnd.Next(1, Max_SubOrganizations + 1);
                for (int i = 0; i < subOrganizationsCount; i++)
                    subOrganizations.Add(new SubOrganization()
                    {
                        Id = subOrganizationId++,
                        Name = GetRandomOrganizationName(rnd),
                        Address = GetRandomAddress(rnd),
                        ParentOrganizationId = org.Id
                    });
            }

            // Генерирование объектов потребления для дочерних организаций и зависимых элементов
            foreach (SubOrganization subOrg in subOrganizations)
            {
                int consumptionObjectsCount = rnd.Next(1, Max_ConsumptionObjects + 1);
                for (int i = 0; i < consumptionObjectsCount; i++)
                {
                    // Начальные Id точек измерения и поставки электроэнергии (для расчётных приборов учёта)KDS
                    int eMeasPointStartId = measuringPointId;
                    int eSuppPointStartId = supplyPointId;

                    ElectricityConsumptionObject eConsObj = new ElectricityConsumptionObject()
                    {
                        Id = consumptionObjectId++,
                        Name = GetRandomElectricityConsumptionObjectName(rnd),
                        Address = GetRandomAddress(rnd),
                        SubOrganizationId = subOrg.Id
                    };
                    eConsumptionObjects.Add(eConsObj);

                    // Генерирование элементов для объектов потребления
                    // Создание точек измерения электроэнергии
                    int electricityMeasuringPointsCount = rnd.Next(1, Max_ElectricityMeasuringPoints + 1);
                    for (int j = 0; j < electricityMeasuringPointsCount; j++)
                    {
                        // Создание счётчика электрической энергии
                        DateTime verificationDate = GetRandomVerificationDate(rnd);
                        ElectricEnergyMeter eEMeter = new ElectricEnergyMeter()
                        {
                            Id = measuringPointId, // Можно допустить, что к точке измерения прикрепляется счётчик с тем же Id
                            Number = rnd.Next(100000, int.MaxValue),
                            VerificationDate = verificationDate,
                            VerificationPeriod = GetRandomVerificationPeriod(rnd, verificationDate),
                            EEMeterTypeId = eEMeterTypes[rnd.Next(eEMeterTypes.Count)].Id
                        };
                        eEMeters.Add(eEMeter);

                        // Создание трансформатора тока
                        verificationDate = GetRandomVerificationDate(rnd);
                        CurrentTransformer cTrans = new CurrentTransformer()
                        {
                            Id = measuringPointId, // Можно допустить, что к точке измерения прикрепляется трансформатор с тем же Id
                            Number = rnd.Next(100000, int.MaxValue),
                            VerificationDate = verificationDate,
                            VerificationPeriod = GetRandomVerificationPeriod(rnd, verificationDate),
                            TransformationRatio = rnd.Next(10, 100) * 10,
                            TransformerTypeId = rnd.Next(1, TransformerTypes_Current.Length + 1)
                        };
                        currentTransformers.Add(cTrans);

                        // Создание трансформатора напряжения
                        verificationDate = GetRandomVerificationDate(rnd);
                        VoltageTransformer vTrans = new VoltageTransformer()
                        {
                            Id = measuringPointId, // Можно допустить, что к точке измерения прикрепляется трансформатор с тем же Id
                            Number = rnd.Next(100000, int.MaxValue),
                            VerificationDate = verificationDate,
                            VerificationPeriod = GetRandomVerificationPeriod(rnd, verificationDate),
                            TransformationRatio = rnd.Next(10, 100) * 10,
                            TransformerTypeId = rnd.Next(TransformerTypes_Current.Length, TransformerTypes_Current.Length +
                            TransformerTypes_Voltage.Length) + 1
                        };
                        voltageTransformers.Add(vTrans);

                        // Создание точки измерения электроэнергии
                        ElectricityMeasuringPoint eMeasPoint = new ElectricityMeasuringPoint()
                        {
                            Id = measuringPointId++,
                            Name = GetRandomPointName(rnd),
                            CurrentTransformerId = cTrans.Id,
                            VoltageTransformerId = vTrans.Id,
                            ElectricEnergyMeterId = eEMeter.Id,
                            ElectricityConsumptionObjectId = eConsObj.Id
                        };
                        eMeasuringPoints.Add(eMeasPoint);
                    }

                    // Создание точек поставки электроэнергии
                    int electricitySupplyPointsCount = rnd.Next(1, Max_ElectricitySupplyPoints + 1);
                    for (int j = 0; j < electricitySupplyPointsCount; j++)
                    {
                        ElectricitySupplyPoint eSuppObj = new ElectricitySupplyPoint()
                        {
                            Id = supplyPointId++,
                            Name = GetRandomPointName(rnd),
                            MaximumPower = rnd.Next(1, 1000) + Math.Round(rnd.NextDouble(), 1),
                            ElectricityConsumptionObjectId = eConsObj.Id
                        };
                        eSupplyPoints.Add(eSuppObj);
                    }

                    // Генерирование расчётных приборов учёта
                    int accountingDeviceInfosCount = rnd.Next(1, Max_AccountingDevices);
                    DateTime accountingDeviceInfoFromDate = GetRandomVerificationDate(rnd);
                    DateTime accountingDeviceInfoToDate;
                    for (int j = 0; j < accountingDeviceInfosCount; j++)
                    {
                        accountingDeviceInfoToDate = accountingDeviceInfoFromDate.AddDays(rnd.Next(1, 15)).AddMonths(rnd.Next(1, 3));
                        accountingDeviceInfos.Add(new AccountingDeviceInfo()
                        {
                            Id = accountingDeviceInfoId++,
                            ConsumedElectricity = rnd.Next(1, 50) + Math.Round(rnd.NextDouble(), 1),
                            ElectricityMeasuringPointId = rnd.Next(eMeasPointStartId, measuringPointId),
                            ElectricitySupplyPointId = rnd.Next(eSuppPointStartId, supplyPointId),
                            Interval_From = accountingDeviceInfoFromDate,
                            Interval_To = accountingDeviceInfoToDate
                        });
                        accountingDeviceInfoFromDate = accountingDeviceInfoToDate;
                    }
                }
            }

            modelBuilder.Entity<ElectricEnergyMeterType>().HasData(eEMeterTypes.ToArray());
            modelBuilder.Entity<TransformerType>().HasData(transformerTypes.ToArray());
            modelBuilder.Entity<Organization>().HasData(organizations.ToArray());
            modelBuilder.Entity<SubOrganization>().HasData(subOrganizations.ToArray());
            modelBuilder.Entity<ElectricityConsumptionObject>().HasData(eConsumptionObjects.ToArray());
            modelBuilder.Entity<ElectricityMeasuringPoint>().HasData(eMeasuringPoints.ToArray());
            modelBuilder.Entity<ElectricEnergyMeter>().HasData(eEMeters.ToArray());
            modelBuilder.Entity<CurrentTransformer>().HasData(currentTransformers.ToArray());
            modelBuilder.Entity<VoltageTransformer>().HasData(voltageTransformers.ToArray());
            modelBuilder.Entity<ElectricitySupplyPoint>().HasData(eSupplyPoints.ToArray());
            modelBuilder.Entity<AccountingDeviceInfo>().HasData(accountingDeviceInfos.ToArray());
        }

        private static DateTime GetRandomVerificationDate(Random rnd) => DateTime.Now.AddDays(-rnd.Next(31, 1500));
        private static DateTime GetRandomVerificationPeriod(Random rnd, DateTime verificationDate) => verificationDate.AddDays(rnd.Next(100, 2200));
        private static string GetRandomOrganizationName(Random rnd) =>
            $"{OrganizationNames_First[rnd.Next(OrganizationNames_First.Length)]}{(rnd.Next(100) > 50 ? $" " : "")}" +
            $"{OrganizationNames_Second[rnd.Next(OrganizationNames_Second.Length)]}";
        private static string GetRandomAddress(Random rnd) =>
            $"{rnd.Next(100000, 1000000)}, г. {Addresses_Cities[rnd.Next(Addresses_Cities.Length)]}, " +
            $"{Addresses_Streets[rnd.Next(Addresses_Streets.Length)]}, д. {rnd.Next(1, 201)}" +
            $"{(rnd.Next(100) > 50 ? $", стр. {rnd.Next(1, 100)}" : "")}" +
            $", {(rnd.Next(100) > 50 ? $"офис {rnd.Next(1, 1000)}" : $"пом. {rnd.Next(1, 1000)}")}";
        private static string GetRandomElectricityConsumptionObjectName(Random rnd) =>
            $"{ConsObj_Names1[rnd.Next(ConsObj_Names1.Length)]} {Voltages[rnd.Next(Voltages.Length)]}/{Voltages[rnd.Next(Voltages.Length)]} " +
            $"{ConsObj_Names2[rnd.Next(ConsObj_Names2.Length)]}";
        private static string GetRandomPointName(Random rnd) =>
            $"{Point_Names1[rnd.Next(Point_Names1.Length)]} №{rnd.Next(1, 31)} - {Point_Names2[rnd.Next(Point_Names2.Length)]}";
    }
}
