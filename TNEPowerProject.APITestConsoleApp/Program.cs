using Refit;
using System;
using System.Threading.Tasks;
using TNEPowerProject.Contract.DTO;
using TNEPowerProject.Contract.Interfaces;
using TNEPowerProject.Contract.DTO.Transformers;
using TNEPowerProject.APITestConsoleApp.LLConsoleMenu;
using TNEPowerProject.Contract.DTO.ElectricEnergyMeters;
using TNEPowerProject.Contract.DTO.AccountingDeviceInfos;
using TNEPowerProject.Contract.DTO.ExpiredElectricEquipment;
using TNEPowerProject.Contract.DTO.ElectricEnergyMeterTypes;
using TNEPowerProject.Contract.DTO.ElectricityMeasuringPoints;
using TNEPowerProject.Contract.DTO.ElectricityConsumptionObjects;

namespace TNEPowerProject.APITestConsoleApp
{
    internal class Program
    {
        private const string APIAddress = "https://localhost:8081";

        private static readonly RefitSettings refitSettings;

        private static readonly IHeartbeatAPI heartbeatAPI;

        public static readonly IAccountingDeviceInfosAPI accountingDeviceInfosAPI;

        private static readonly IElectricityConsumptionObjectsAPI electricityConsumptionObjectsAPI;

        private static readonly IElectricityMeasuringPointsAPI electricityMeasuringPointsAPI;

        private static readonly IElectricEnergyMetersAPI electricEnergyMetersAPI;
        private static readonly IElectricEnergyMeterTypesAPI electricEnergyMeterTypesAPI;

        private static readonly ICurrentTransformersAPI currentTransformersAPI;
        private static readonly IVoltageTransformersAPI voltageTransformersAPI;
        private static readonly ITransformerTypesAPI transformerTypesAPI;

        private static readonly IExpiredElectricEquipmentAPI expiredElectricEquipmentAPI;

        private static readonly LLMenu MainMenu = new LLMenu("Выберите действие:")
               .AddNewMenuEntry(new LLMenuEntry("Проверить статус API", TestHeartbeatAPI))
               .AddNewMenuEntry(new LLMenuEntry("[НЕРЕАЛИЗОВАНО] Автоматическое тестирование API", AutoTestAPI))
               .AddNewMenuEntry(new LLMenuEntry("Тестирование API для объектов потребления", TestEConsumptionObjectsAPI))
               .AddNewMenuEntry(new LLMenuEntry("Тестирование API для расчетных приборов учета", TestAccountingDeviceInfosAPI))
               .AddNewMenuEntry(new LLMenuEntry("Тестирование API для точек измерения электроэнергии", TestElectricityMeasuringPointsAPI))
               .AddNewMenuEntry(new LLMenuEntry("Тестирование API для счётчиков электрической энергии", TestElectricEnergyMetersAPI))
               .AddNewMenuEntry(new LLMenuEntry("Тестирование API для типов счётчиков электрической энергии", TestElectricEnergyMeterTypesAPI))
               .AddNewMenuEntry(new LLMenuEntry("Тестирование API для трансформаторов тока", TestCurrentTransformersAPI))
               .AddNewMenuEntry(new LLMenuEntry("Тестирование API для трансформаторов напряжения", TestVoltageTransformersAPI))
               .AddNewMenuEntry(new LLMenuEntry("Тестирование API для типов трансформаторов", TestTransformerTypesAPI))
               .AddNewMenuEntry(new LLMenuEntry("Тестирование API для объектов электрической инфраструктуры с истёкшими сроками поверки", TestExpiredElectricEquipmentAPI))
               .AppendDefaultExitItem("Выход");

        private static readonly LLMenu AutotestMenu = new LLMenu("Выберите действие:")
               .AppendDefaultExitItem();

        private static readonly LLMenu AccountingDeviceInfosTestMenu = new LLMenu("Выберите действие для тестирования API расчетных приборов учета:")
               .AddNewMenuEntry(new LLMenuEntry("Вывести список расчетных приборов учета для выбранного года", TestAccountingDeviceInfosAPI_GetByYear))
               .AppendDefaultExitItem();

        private static readonly LLMenu EConsumptionObjectsTestMenu = new LLMenu("Выберите действие для тестирования API объектов потребления:")
               .AddNewMenuEntry(new LLMenuEntry("Вывести список объектов потребления", TestEConsumptionObjectsAPI_List))
               .AppendDefaultExitItem();

        private static readonly LLMenu EMeasuringPointsTestMenu = new LLMenu("Выберите действие для тестирования API точек измерения электроэнергии:")
               .AddNewMenuEntry(new LLMenuEntry("Проверить существование точки измерения электроэнергии по Id", TestEMeasuringPointsAPI_CheckIdExists))
               .AddNewMenuEntry(new LLMenuEntry("Получить точку измерения электроэнергии по Id", TestEMeasuringPointsAPI_GetById))
               .AddNewMenuEntry(new LLMenuEntry("Добавить новую точку измерения электроэнергии", TestEMeasuringPointsAPI_Create))
               .AppendDefaultExitItem();

        private static readonly LLMenu EEMetersTestMenu = new LLMenu("Выберите действие для тестирования API счётчиков электрической энергии:")
               .AddNewMenuEntry(new LLMenuEntry("Проверить существование счётчика электрической энергии по Id", TestEEMetersAPI_CheckIdExists))
               .AddNewMenuEntry(new LLMenuEntry("Добавить новый счётчик электрической энергии", TestEEMetersAPI_Create))
               .AddNewMenuEntry(new LLMenuEntry("Вывести список счётчиков электрической энергии", TestEEMetersAPI_List))
               .AppendDefaultExitItem();
        private static readonly LLMenu EEMeterTypesTestMenu = new LLMenu("Выберите действие для тестирования API типов счётчиков электрической энергии:")
               .AddNewMenuEntry(new LLMenuEntry("Проверить существование типа счётчиков электрической энергии по Id", TestEEMeterTypesAPI_CheckIdExists))
               .AddNewMenuEntry(new LLMenuEntry("Добавить новый тип счётчиков электрической энергии", TestEEMeterTypesAPI_Create))
               .AddNewMenuEntry(new LLMenuEntry("Вывести список типов счётчиков электрической энергии", TestEEMeterTypesAPI_List))
               .AppendDefaultExitItem();

        private static readonly LLMenu CurrentTransformersTestMenu = new LLMenu("Выберите действие для тестирования API трансформаторов тока:")
               .AddNewMenuEntry(new LLMenuEntry("Проверить существование трансформатора тока по Id", TestCurrentTransformersAPI_CheckIdExists))
               .AddNewMenuEntry(new LLMenuEntry("Добавить новый трансформатор тока", TestCurrentTransformersAPI_Create))
               .AppendDefaultExitItem();
        private static readonly LLMenu VoltageTransformersTestMenu = new LLMenu("Выберите действие для тестирования API трансформаторов напряжения:")
               .AddNewMenuEntry(new LLMenuEntry("Проверить существование трансформатора напряжения по Id", TestVoltageTransformersAPI_CheckIdExists))
               .AddNewMenuEntry(new LLMenuEntry("Добавить новый трансформатор напряжения", TestVoltageTransformersAPI_Create))
               .AppendDefaultExitItem();
        private static readonly LLMenu TransformerTypesTestMenu = new LLMenu("Выберите действие для тестирования API типов трансформаторов:")
               .AddNewMenuEntry(new LLMenuEntry("Проверить существование типа трансформатора по Id", TestTransformerTypesAPI_CheckIdExists))
               .AddNewMenuEntry(new LLMenuEntry("Добавить новый тип трансформатора", TestTransformerTypesAPI_Create))
               .AddNewMenuEntry(new LLMenuEntry("Вывести список типов трансформаторов", TestTransformerTypesAPI_List))
               .AppendDefaultExitItem();

        private static readonly LLMenu ExpiredElectricEquipmentTestMenu = new LLMenu("Выберите действие для тестирования API объектов электрической инфраструктуры с истёкшими сроками поверки:")
               .AddNewMenuEntry(new LLMenuEntry("Получить список счётчиков электрической энергии с истёкшими сроками поверки", TestExpiredElectricEquipmentAPI_EEMeters))
               .AddNewMenuEntry(new LLMenuEntry("Получить список трансформаторов тока с истёкшими сроками поверки", TestExpiredElectricEquipmentAPI_CTransformers))
               .AddNewMenuEntry(new LLMenuEntry("Получить список трансформаторов напряжения с истёкшими сроками поверки", TestExpiredElectricEquipmentAPI_VTransformers))
               .AppendDefaultExitItem();

        private static AsyncSpinner spinner;

        static Program()
        {
            refitSettings = new RefitSettings { ContentSerializer = new NewtonsoftJsonContentSerializer() };

            heartbeatAPI = RestService.For<IHeartbeatAPI>(APIAddress, refitSettings);

            accountingDeviceInfosAPI = RestService.For<IAccountingDeviceInfosAPI>(APIAddress, refitSettings);

            electricityConsumptionObjectsAPI = RestService.For<IElectricityConsumptionObjectsAPI>(APIAddress, refitSettings);

            electricityMeasuringPointsAPI = RestService.For<IElectricityMeasuringPointsAPI>(APIAddress, refitSettings);

            electricEnergyMetersAPI = RestService.For<IElectricEnergyMetersAPI>(APIAddress, refitSettings);
            electricEnergyMeterTypesAPI = RestService.For<IElectricEnergyMeterTypesAPI>(APIAddress, refitSettings);

            currentTransformersAPI = RestService.For<ICurrentTransformersAPI>(APIAddress, refitSettings);
            voltageTransformersAPI = RestService.For<IVoltageTransformersAPI>(APIAddress, refitSettings);
            transformerTypesAPI = RestService.For<ITransformerTypesAPI>(APIAddress, refitSettings);

            expiredElectricEquipmentAPI = RestService.For<IExpiredElectricEquipmentAPI>(APIAddress, refitSettings);

            spinner = new AsyncSpinner();
        }
        static void Main(string[] args)
        {
            Console.Title = "Trans Neft Energo - Power Project Test App";

            try
            {
                MainMenu.DoMenuAsync().Wait();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine("Произошла критическая ошибка: ");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
        private static async Task<bool> DoTest(Func<Task<bool>> testAction)
        {
            bool ans = true;
            try
            {
                ans = await testAction();
            }
            catch (ValidationApiException ex)
            {
                spinner.StopSpinner();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine("[Ошибка]");
                Console.WriteLine(ex.Message);
                Console.WriteLine($"URI запроса: {ex.Uri}");
                Console.WriteLine($"Причина: {ex.ReasonPhrase}");
                Console.WriteLine($"Запрос: {ex.RequestMessage}");
            }
            catch (ApiException ex)
            {
                spinner.StopSpinner();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine("[Ошибка]");
                Console.WriteLine(ex.Message);
                Console.WriteLine($"URI запроса: {ex.Uri}");
                Console.WriteLine($"Причина: {ex.ReasonPhrase}");
                Console.WriteLine($"Запрос: {ex.RequestMessage}");
            }
            catch (Exception ex)
            {
                spinner.StopSpinner();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine("[Ошибка]");
                Console.WriteLine("Не удалось подключиться к серверу.");
                Console.WriteLine(ex.Message);
            }
            Console.CursorVisible = false;
            Console.ForegroundColor = MainMenu.DefaultForegroundColor;
            Console.Write("Нажмите любую клавишу для продолжения...");
            Console.ReadKey(true);
            return ans;
        }
        #region HeartbeatAPI
        private static async Task<bool> TestHeartbeatAPI()
        {
            return await DoTest(async () =>
            {
                Console.WriteLine($"Адрес API: {APIAddress}");
                string preparedString = Guid.NewGuid().ToString().ToLower();

                Console.WriteLine($"Подготовленная строка: {preparedString}");

                Console.Write("Выполнение запроса... ");
                spinner.StartSpinner();
                string answer = await heartbeatAPI.Heartbeat(preparedString);
                spinner.StopSpinner();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[OK]");
                Console.ForegroundColor = MainMenu.DefaultForegroundColor;
                Console.WriteLine($"Полученная строка: {answer}");
                if (!string.IsNullOrWhiteSpace(answer) && answer.Contains(preparedString))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Полученная строка содержит искомое значение, API живо и готово к свершениям!");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine($"Полученная строка не содержит искомого значения, либо сервер ответил не правильно.");
                }
                return true;
            });
        }
        #endregion
        #region AutoTestAPI
        public static async Task<bool> AutoTestAPI()
        {
            await AutotestMenu.DoMenuAsync();
            return true;
        }
        #endregion
        #region AccountingDeviceInfosAPI
        private static async Task<bool> TestAccountingDeviceInfosAPI()
        {
            await AccountingDeviceInfosTestMenu.DoMenuAsync();
            return true;
        }
        private static async Task<bool> TestAccountingDeviceInfosAPI_GetByYear()
        {
            return await DoTest(async () =>
            {
                Console.Write("Получение списка расчетных приборов учета для выбранного года");

                Console.Write("Введите год: ");
                Console.CursorVisible = true;
                if (!int.TryParse(Console.ReadLine(), out int year))
                {
                    Console.WriteLine("Неправильно введён год!");
                    return true;
                }
                Console.CursorVisible = false;

                Console.Write("Выполнение запроса... ");
                spinner.StartSpinner();
                TNEBaseDTO<AccountingDeviceInfosListDTO> result = await accountingDeviceInfosAPI.GetAllByYear(year);
                spinner.StopSpinner();

                if (result == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine("[FAIL]");
                    Console.WriteLine($"Полученный ответ не содержит ожидаемых данных.");
                }
                else
                {
                    Console.WriteLine("[OK]");
                    Console.WriteLine($"Ожидаемый статус: 200. Полученный статус: {result.StatusCode} [{((result.StatusCode == 200) ? "OK" : "FAIL")}]");
                    if (result.StatusCode == 200 && result.Result != null && result.Result.AccountingDeviceInfos != null)
                    {
                        if (result.Result.AccountingDeviceInfos.Count == 0)
                        {
                            Console.WriteLine($"Ответ получен правильный, однако, список пуст.");
                        }
                        else
                        {
                            Console.WriteLine($"Список расчетных приборов учета:");
                        }
                        for (int i = 0; i < result.Result.AccountingDeviceInfos.Count; i++)
                        {
                            AccountingDeviceInfosListDTO.AccountingDeviceInfoListItemDTO accDevInfo = result.Result.AccountingDeviceInfos[i];
                            Console.WriteLine($"{i + 1}. Id: {accDevInfo.Id}, начало периода: {accDevInfo.Interval_From:dd.MM.yyyy}" +
                                $", конец периода: {accDevInfo.Interval_To:dd.MM.yyyy}, потр. электр.: {accDevInfo.ConsumedElectricity}" +
                                $", ИдТПЭ: {accDevInfo.ElectricitySupplyPointId}, имя ТПЭ: {accDevInfo.ElectricitySupplyPointName}" +
                                $", ИдТИЭ: {accDevInfo.ElectricityMeasuringPointId}, имя ИдТИЭ: {accDevInfo.ElectricityMeasuringPointName}");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine($"Полученный ответ не содержит ожидаемых данных.");
                        Console.WriteLine($"Сообщение: {result.Message}");
                    }
                }
                return true;
            });
        }
        #endregion
        #region ElectricityConsumptionObjectsAPI
        private static async Task<bool> TestEConsumptionObjectsAPI()
        {
            await EConsumptionObjectsTestMenu.DoMenuAsync();
            return true;
        }
        private static async Task<bool> TestEConsumptionObjectsAPI_List()
        {
            return await DoTest(async () =>
            {
                Console.Write("Получение списка объектов потребления... ");
                spinner.StartSpinner();
                TNEBaseDTO<ElectricityConsumptionObjectsListDTO> result = await electricityConsumptionObjectsAPI.GetAll();
                spinner.StopSpinner();

                if (result == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine("[FAIL]");
                    Console.WriteLine($"Полученный ответ не содержит ожидаемых данных.");
                }
                else
                {
                    Console.WriteLine("[OK]");
                    Console.WriteLine($"Ожидаемый статус: 200. Полученный статус: {result.StatusCode} [{((result.StatusCode == 200) ? "OK" : "FAIL")}]");
                    if (result.StatusCode == 200 && result.Result != null && result.Result.ElectricityConsumptionObjects != null)
                    {
                        if (result.Result.ElectricityConsumptionObjects.Count == 0)
                        {
                            Console.WriteLine($"Ответ получен правильный, однако, список пуст.");
                        }
                        else
                        {
                            Console.WriteLine($"Список объектов потребления:");
                        }
                        for (int i = 0; i < result.Result.ElectricityConsumptionObjects.Count; i++)
                        {
                            ElectricityConsumptionObjectsListDTO.ElectricityConsumptionObjectListItemDTO eConsObj = result.Result.ElectricityConsumptionObjects[i];
                            Console.WriteLine($"{i + 1}. Id: {eConsObj.Id}, имя: {eConsObj.Name}" +
                                $", Id ДК: {eConsObj.SubOrganizationId}, имя ДК: {eConsObj.SubOrganizationName}");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine($"Полученный ответ не содержит ожидаемых данных.");
                        Console.WriteLine($"Сообщение: {result.Message}");
                    }
                }
                return true;
            });
        }
        #endregion
        #region ElectricityMeasuringPointsAPI
        private static async Task<bool> TestElectricityMeasuringPointsAPI()
        {
            await EMeasuringPointsTestMenu.DoMenuAsync();
            return true;
        }
        private static async Task<bool> TestEMeasuringPointsAPI_CheckIdExists()
        {
            return await DoTest(async () =>
            {
                Console.WriteLine("Проверка существования точки измерения электроэнергии по Id");

                Console.CursorVisible = true;
                Console.Write("Введите Id точки измерения электроэнергии: ");
                if (!int.TryParse(Console.ReadLine(), out int eMeasPointId))
                {
                    Console.WriteLine("Неправильно введён Id!");
                    return true;
                }
                Console.CursorVisible = false;

                Console.Write("Выполнение запроса... ");
                spinner.StartSpinner();
                TNEBaseDTO<ElectricityMeasuringPointExistenceDTO> result = await electricityMeasuringPointsAPI.CheckElectricityMeasuringPointExists(eMeasPointId);
                spinner.StopSpinner();
                Console.WriteLine("[OK]");

                if (result == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine("[FAIL]");
                    Console.WriteLine($"Полученный ответ не содержит ожидаемых данных.");
                }
                else
                {
                    Console.WriteLine($"Ожидаемый статус: 200. Полученный статус: {result.StatusCode} [{((result.StatusCode == 200) ? "OK" : "FAIL")}]");

                    if (result.StatusCode == 200)
                    {
                        Console.WriteLine($"Указанный Id {(result.Result.Exists ? "" : "не ")}существует");
                    }
                    else
                        Console.WriteLine("Получен не правильный код ответа.");
                }

                return true;
            });
        }
        private static async Task<bool> TestEMeasuringPointsAPI_GetById()
        {
            return await DoTest(async () =>
            {
                Console.WriteLine("Получение точки измерения электроэнергии по Id");

                Console.CursorVisible = true;
                Console.Write("Введите Id точки измерения электроэнергии: ");
                if (!int.TryParse(Console.ReadLine(), out int eMeasPointId))
                {
                    Console.WriteLine("Неправильно введён Id!");
                    return true;
                }
                Console.CursorVisible = false;

                Console.Write("Выполнение запроса... ");
                spinner.StartSpinner();
                TNEBaseDTO<ElectricityMeasuringPointDTO> result = await electricityMeasuringPointsAPI.GetElectricityMeasuringPoint(eMeasPointId);
                spinner.StopSpinner();
                Console.WriteLine("[OK]");

                if (result == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine("[FAIL]");
                    Console.WriteLine($"Полученный ответ не содержит ожидаемых данных.");
                }
                else
                {
                    Console.WriteLine($"Ожидаемый статус: 200. Полученный статус: {result.StatusCode} [{((result.StatusCode == 200) ? "OK" : "FAIL")}]");

                    if (result.StatusCode == 200 && result.Result != null)
                    {
                        Console.WriteLine($"По указанному Id [{eMeasPointId}] найдена точка измерения электроэнергии:");
                        Console.WriteLine($"Имя: {result.Result.Name}, СЭЭ: {result.Result.ElectricEnergyMeterId}/{result.Result.ElectricEnergyMeterNumber}" +
                            $", ТТ: {result.Result.CurrentTransformerId}/{result.Result.CurrentTransformerNumber}" +
                            $", ТН: {result.Result.VoltageTransformerId}/{result.Result.VoltageTransformerNumber}" +
                            $", ИдОП: {result.Result.ElectricityConsumptionObjectId}");
                    }
                    else if (result.StatusCode == 404)
                    {
                        Console.WriteLine($"Указанный Id не существует");
                    }
                    else
                        Console.WriteLine("Получен не правильный код ответа.");
                }

                return true;
            });
        }
        private static async Task<bool> TestEMeasuringPointsAPI_Create()
        {
            return await DoTest(async () =>
            {
                Console.WriteLine("Создание новой точки измерения электроэнергии:");

                CreateElectricityMeasuringPointDTO requestDTO = new CreateElectricityMeasuringPointDTO();

                Console.CursorVisible = true;
                Console.Write("Введите имя точки измерения электроэнергии: ");
                requestDTO.Name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(requestDTO.Name))
                {
                    Console.WriteLine("Неправильно введено имя точки измерения электроэнергии");
                    return true;
                }

                Console.Write("Введите Id счётчика электрической энергии: ");
                if (!int.TryParse(Console.ReadLine(), out int ElectricEnergyMeterId))
                {
                    Console.WriteLine("Неправильно введён Id счётчика электрической энергии");
                    return true;
                }
                requestDTO.ElectricEnergyMeterId = ElectricEnergyMeterId;

                Console.Write("Введите Id трансформатора тока: ");
                if (!int.TryParse(Console.ReadLine(), out int CurrentTransformerId))
                {
                    Console.WriteLine("Неправильно введён Id трансформатора тока");
                    return true;
                }
                requestDTO.CurrentTransformerId = CurrentTransformerId;

                Console.Write("Введите Id трансформатора напряжения: ");
                if (!int.TryParse(Console.ReadLine(), out int VoltageTransformerId))
                {
                    Console.WriteLine("Неправильно введён Id трансформатора напряжения");
                    return true;
                }
                requestDTO.VoltageTransformerId = VoltageTransformerId;

                Console.Write("Введите Id объекта потребления: ");
                if (!int.TryParse(Console.ReadLine(), out int ElectricityConsumptionObjectId))
                {
                    Console.WriteLine("Неправильно введён Id объекта потребления");
                    return true;
                }
                requestDTO.ElectricityConsumptionObjectId = ElectricityConsumptionObjectId;
                Console.CursorVisible = false;

                Console.Write("Выполнение запроса... ");
                spinner.StartSpinner();
                TNEBaseDTO<ElectricityMeasuringPointDTO> result = await electricityMeasuringPointsAPI.CreateElectricityMeasuringPoint(requestDTO);
                spinner.StopSpinner();

                if (result == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine("[FAIL]");
                    Console.WriteLine($"Полученный ответ не содержит ожидаемых данных.");
                }
                else
                {
                    Console.WriteLine("[OK]");
                    Console.WriteLine($"Ожидаемый статус: 201. Полученный статус: {result.StatusCode} [{((result.StatusCode == 201) ? "OK" : "FAIL")}]");
                    if (result.StatusCode == 201 && result.Result != null)
                    {
                        Console.WriteLine($"Новая точка измерения электроэнергии создана успешно.\n" +
                            $"Id: {result.Result.Id}, имя: {result.Result.Name}" +
                            $", СЭЭ: {result.Result.ElectricEnergyMeterId}/{result.Result.ElectricEnergyMeterNumber}" +
                            $", ТТ: {result.Result.CurrentTransformerId}/{result.Result.CurrentTransformerNumber}" +
                            $", ТН: {result.Result.VoltageTransformerId}/{result.Result.VoltageTransformerNumber}" +
                            $", ИдОП: {result.Result.ElectricityConsumptionObjectId}");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine($"Полученный ответ не содержит ожидаемых данных.");
                        Console.WriteLine($"Сообщение: {result.Message}");
                    }
                }
                return true;
            });
        }
        #endregion
        #region ElectricEnergyMetersAPI
        private static async Task<bool> TestElectricEnergyMetersAPI()
        {
            await EEMetersTestMenu.DoMenuAsync();
            return true;
        }
        private static async Task<bool> TestEEMetersAPI_CheckIdExists()
        {
            return await DoTest(async () =>
            {
                Console.WriteLine("Проверить существование счётчика электрической энергии по Id");
                Console.Write("Введите Id счётчика электрической энергии: ");
                Console.CursorVisible = true;
                if (!int.TryParse(Console.ReadLine(), out int eEMId))
                {
                    Console.WriteLine("Неправильно введён Id!");
                    return true;
                }
                Console.CursorVisible = false;
                Console.Write("Выполнение запроса... ");
                spinner.StartSpinner();
                TNEBaseDTO<ElectricEnergyMeterExistenceDTO> result = await electricEnergyMetersAPI.CheckElectricEnergyMeterExists(eEMId);
                spinner.StopSpinner();
                Console.WriteLine("[OK]");

                if (result == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine("[FAIL]");
                    Console.WriteLine($"Полученный ответ не содержит ожидаемых данных.");
                }
                else
                {
                    Console.WriteLine($"Ожидаемый статус: 200. Полученный статус: {result.StatusCode} [{(result.StatusCode == 200 ? "OK" : "FAIL")}]");

                    if (result.StatusCode == 200)
                    {
                        Console.WriteLine($"Указанный Id {(result.Result.Exists ? "" : "не ")}существует");
                    }
                    else
                        Console.WriteLine("Получен не правильный код ответа.");
                }

                return true;
            });
        }
        private static async Task<bool> TestEEMetersAPI_Create()
        {
            return await DoTest(async () =>
            {
                Console.WriteLine("Создание нового счётчика электрической энергии:");

                CreateElectricEnergyMeterDTO requestDTO = new CreateElectricEnergyMeterDTO();

                Console.CursorVisible = true;
                Console.Write("Введите номер счётчика электрической энергии: ");
                if (!long.TryParse(Console.ReadLine(), out long Number))
                {
                    Console.WriteLine("Неправильно введён номер счётчика электрической энергии");
                    return true;
                }
                requestDTO.Number = Number;


                Console.WriteLine("Введите дату поверки для счётчика электрической энергии:");

                if (!DateTime.TryParse(Console.ReadLine(), out DateTime VerificationDate))
                {
                    Console.WriteLine("Неправильно введена дата поверки счётчика электрической энергии");
                    return true;
                }
                requestDTO.VerificationDate = VerificationDate;

                Console.WriteLine("Введите срок поверки (дату окончания поверочного периода) счётчика электрической энергии:");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime VerificationPeriod))
                {
                    Console.WriteLine("Неправильно введён срок поверки (дата окончания поверочного периода) счётчика электрической энергии");
                    return true;
                }
                requestDTO.VerificationPeriod = VerificationPeriod;

                Console.Write("Введите Id типа счётчика электрической энергии: ");
                if (!int.TryParse(Console.ReadLine(), out int eEMTypeId))
                {
                    Console.WriteLine("Неправильно введён Id типа счётчика электрической энергии");
                    return true;
                }
                requestDTO.EEMeterTypeId = eEMTypeId;
                Console.CursorVisible = false;

                Console.Write("Выполнение запроса... ");
                spinner.StartSpinner();
                TNEBaseDTO<ElectricEnergyMeterDTO> result = await electricEnergyMetersAPI.CreateElectricEnergyMeter(requestDTO);
                spinner.StopSpinner();

                if (result == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine("[FAIL]");
                    Console.WriteLine($"Полученный ответ не содержит ожидаемых данных.");
                }
                else
                {
                    Console.WriteLine("[OK]");
                    Console.WriteLine($"Ожидаемый статус: 201. Полученный статус: {result.StatusCode} [{((result.StatusCode == 201) ? "OK" : "FAIL")}]");
                    if (result.StatusCode == 201 && result.Result != null)
                    {
                        Console.WriteLine($"Новый счётчик электрической энергии создан успешно. Id: {result.Result.Id}");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine($"Полученный ответ не содержит ожидаемых данных.");
                        Console.WriteLine($"Сообщение: {result.Message}");
                    }
                }
                return true;
            });
        }
        private static async Task<bool> TestEEMetersAPI_List()
        {
            return await DoTest(async () =>
            {
                Console.Write("Получение списка счётчиков электрической энергии... ");
                spinner.StartSpinner();
                TNEBaseDTO<ElectricEnergyMetersListDTO> result = await electricEnergyMetersAPI.GetAll();
                spinner.StopSpinner();

                if (result == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine("[FAIL]");
                    Console.WriteLine($"Полученный ответ не содержит ожидаемых данных.");
                }
                else
                {
                    Console.WriteLine("[OK]");
                    Console.WriteLine($"Ожидаемый статус: 200. Полученный статус: {result.StatusCode} [{((result.StatusCode == 200) ? "OK" : "FAIL")}]");
                    if (result.StatusCode == 200 && result.Result != null && result.Result.ElectricEnergyMeters != null)
                    {
                        if (result.Result.ElectricEnergyMeters.Count == 0)
                        {
                            Console.WriteLine($"Ответ получен правильный, однако, список пуст.");
                        }
                        else
                        {
                            Console.WriteLine($"Список счётчиков электрической энергии:");
                        }
                        for (int i = 0; i < result.Result.ElectricEnergyMeters.Count; i++)
                        {
                            ElectricEnergyMetersListDTO.ElectricEnergyMeterListItemDTO eEMeter = result.Result.ElectricEnergyMeters[i];
                            Console.WriteLine($"{i + 1}. Id: {eEMeter.Id}, номер: {eEMeter.Number}, д.п.: {eEMeter.VerificationDate:dd.MM.yyyy}, п.п.: {eEMeter.VerificationPeriod.Date:dd.MM:yyyy}, тип: {eEMeter.EEMeterTypeDescription}");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine($"Полученный ответ не содержит ожидаемых данных.");
                        Console.WriteLine($"Сообщение: {result.Message}");
                    }
                }
                return true;
            });
        }
        #endregion
        #region ElectricEnergyMeterTypesAPI
        private static async Task<bool> TestElectricEnergyMeterTypesAPI()
        {
            await EEMeterTypesTestMenu.DoMenuAsync();
            return true;
        }
        private static async Task<bool> TestEEMeterTypesAPI_CheckIdExists()
        {
            return await DoTest(async () =>
            {
                Console.WriteLine("Проверить существование типа счётчиков электрической энергии по Id");

                Console.CursorVisible = true;
                Console.Write("Введите Id типа счётчиков электрической энергии: ");
                if (!int.TryParse(Console.ReadLine(), out int eEMTypeId))
                {
                    Console.WriteLine("Неправильно введён Id!");
                    return true;
                }
                Console.CursorVisible = false;

                Console.Write("Выполнение запроса... ");
                spinner.StartSpinner();
                TNEBaseDTO<ElectricEnergyMeterTypeExistenceDTO> result = await electricEnergyMeterTypesAPI.CheckElectricEnergyMeterTypeExists(eEMTypeId);
                spinner.StopSpinner();
                Console.WriteLine("[OK]");

                if (result == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine("[FAIL]");
                    Console.WriteLine($"Полученный ответ не содержит ожидаемых данных.");
                }
                else
                {
                    Console.WriteLine($"Ожидаемый статус: 200. Полученный статус: {result.StatusCode} [{((result.StatusCode == 200) ? "OK" : "FAIL")}]");

                    if (result.StatusCode == 200)
                    {
                        Console.WriteLine($"Указанный Id {(result.Result.Exists ? "" : "не ")}существует");
                    }
                    else
                        Console.WriteLine("Получен не правильный код ответа.");
                }

                return true;
            });
        }
        private static async Task<bool> TestEEMeterTypesAPI_Create()
        {
            return await DoTest(async () =>
            {
                Console.WriteLine("Создание нового типа счётчиков электрической энергии:");

                CreateElectricEnergyMeterTypeDTO requestDTO = new CreateElectricEnergyMeterTypeDTO();

                Console.CursorVisible = true;
                Console.Write("Введите название типа счётчика электрической энергии: ");
                requestDTO.Description = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(requestDTO.Description))
                {
                    Console.WriteLine("Неправильно введёно название типа счётчика электрической энергии");
                    return true;
                }
                Console.CursorVisible = false;

                Console.Write("Выполнение запроса... ");
                spinner.StartSpinner();
                TNEBaseDTO<ElectricEnergyMeterTypeDTO> result = await electricEnergyMeterTypesAPI.CreateElectricEnergyMeterType(requestDTO);
                spinner.StopSpinner();

                if (result == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine("[FAIL]");
                    Console.WriteLine($"Полученный ответ не содержит ожидаемых данных.");
                }
                else
                {
                    Console.WriteLine("[OK]");
                    Console.WriteLine($"Ожидаемый статус: 201. Полученный статус: {result.StatusCode} [{((result.StatusCode == 201) ? "OK" : "FAIL")}]");
                    if (result.StatusCode == 201 && result.Result != null)
                    {
                        Console.WriteLine($"Новый тип счётчиков электрической энергии создан успешно. Id: {result.Result.Id}");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine($"Полученный ответ не содержит ожидаемых данных.");
                        Console.WriteLine($"Сообщение: {result.Message}");
                    }
                }
                return true;
            });
        }
        private static async Task<bool> TestEEMeterTypesAPI_List()
        {
            return await DoTest(async () =>
            {
                Console.Write("Получение списка типов счётчиков электрической энергии... ");
                spinner.StartSpinner();
                TNEBaseDTO<ElectricEnergyMeterTypesListDTO> result = await electricEnergyMeterTypesAPI.GetAll();
                spinner.StopSpinner();

                if (result == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine("[FAIL]");
                    Console.WriteLine($"Полученный ответ не содержит ожидаемых данных.");
                }
                else
                {
                    Console.WriteLine("[OK]");
                    Console.WriteLine($"Ожидаемый статус: 200. Полученный статус: {result.StatusCode} [{((result.StatusCode == 200) ? "OK" : "FAIL")}]");
                    if (result.StatusCode == 200 && result.Result != null && result.Result.ElectricEnergyMeterTypes != null)
                    {
                        if (result.Result.ElectricEnergyMeterTypes.Count == 0)
                        {
                            Console.WriteLine($"Ответ получен правильный, однако, список пуст.");
                        }
                        else
                        {
                            Console.WriteLine($"Список типов счётчиков электрической энергии:");
                        }
                        for (int i = 0; i < result.Result.ElectricEnergyMeterTypes.Count; i++)
                        {
                            ElectricEnergyMeterTypesListDTO.ElectricEnergyMeterTypeListItemDTO eEMType = result.Result.ElectricEnergyMeterTypes[i];
                            Console.WriteLine($"{i + 1}. Id: {eEMType.Id}, имя: {eEMType.Description}");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine($"Полученный ответ не содержит ожидаемых данных.");
                        Console.WriteLine($"Сообщение: {result.Message}");
                    }
                }
                return true;
            });
        }
        #endregion
        #region CurrentTransformersAPI
        public static async Task<bool> TestCurrentTransformersAPI()
        {
            await CurrentTransformersTestMenu.DoMenuAsync();
            return true;
        }
        private static async Task<bool> TestCurrentTransformersAPI_CheckIdExists()
        {
            return await DoTest(async () =>
            {
                Console.WriteLine("Проверка существования трансформатора тока по Id");

                Console.CursorVisible = true;
                Console.Write("Введите Id трансформатора тока: ");
                if (!int.TryParse(Console.ReadLine(), out int cTransId))
                {
                    Console.WriteLine("Неправильно введён Id!");
                    return true;
                }
                Console.CursorVisible = false;

                Console.Write("Выполнение запроса... ");
                spinner.StartSpinner();
                TNEBaseDTO<CurrentTransformerExistenceDTO> result = await currentTransformersAPI.CheckCurrentTransformerExists(cTransId);
                spinner.StopSpinner();
                Console.WriteLine("[OK]");

                if (result == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine("[FAIL]");
                    Console.WriteLine($"Полученный ответ не содержит ожидаемых данных.");
                }
                else
                {
                    Console.WriteLine($"Ожидаемый статус: 200. Полученный статус: {result.StatusCode} [{((result.StatusCode == 200) ? "OK" : "FAIL")}]");

                    if (result.StatusCode == 200)
                    {
                        Console.WriteLine($"Указанный Id {(result.Result.Exists ? "" : "не ")}существует");
                    }
                    else
                        Console.WriteLine("Получен не правильный код ответа.");
                }

                return true;
            });
        }
        private static async Task<bool> TestCurrentTransformersAPI_Create()
        {
            return await DoTest(async () =>
            {
                Console.WriteLine("Создание нового трансформатора тока:");

                CreateCurrentTransformerDTO requestDTO = new CreateCurrentTransformerDTO();

                Console.CursorVisible = true;
                Console.Write("Введите номер трансформатора тока: ");
                if (!long.TryParse(Console.ReadLine(), out long Number))
                {
                    Console.WriteLine("Неправильно введён номер трансформатора тока");
                    return true;
                }
                requestDTO.Number = Number;

                Console.Write("Введите коэфициент трансформации по току: ");
                if (!double.TryParse(Console.ReadLine().Replace(".", ","), out double TransformationRatio))
                {
                    Console.WriteLine("Неправильно введён коэфициент трансформации по току");
                    return true;
                }
                requestDTO.TransformationRatio = TransformationRatio;

                Console.WriteLine("Введите дату поверки для трансформатора тока:");

                if (!DateTime.TryParse(Console.ReadLine(), out DateTime VerificationDate))
                {
                    Console.WriteLine("Неправильно введена дата поверки трансформатора тока");
                    return true;
                }
                requestDTO.VerificationDate = VerificationDate;

                Console.WriteLine("Введите срок поверки (дату окончания поверочного периода) трансформатора тока:");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime VerificationPeriod))
                {
                    Console.WriteLine("Неправильно введён срок поверки (дата окончания поверочного периода) трансформатора тока");
                    return true;
                }
                requestDTO.VerificationPeriod = VerificationPeriod;

                Console.Write("Введите Id типа трансформатора тока: ");
                if (!int.TryParse(Console.ReadLine(), out int TransformerTypeId))
                {
                    Console.WriteLine("Неправильно введён Id типа трансформатора тока");
                    return true;
                }
                requestDTO.TransformerTypeId = TransformerTypeId;
                Console.CursorVisible = false;

                Console.Write("Выполнение запроса... ");
                spinner.StartSpinner();
                TNEBaseDTO<CurrentTransformerDTO> result = await currentTransformersAPI.CreateCurrentTransformer(requestDTO);
                spinner.StopSpinner();

                if (result == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine("[FAIL]");
                    Console.WriteLine($"Полученный ответ не содержит ожидаемых данных.");
                }
                else
                {
                    Console.WriteLine("[OK]");
                    Console.WriteLine($"Ожидаемый статус: 201. Полученный статус: {result.StatusCode} [{((result.StatusCode == 201) ? "OK" : "FAIL")}]");
                    if (result.StatusCode == 201 && result.Result != null)
                    {
                        Console.WriteLine($"Новый трансформатор тока создан успешно. Id: {result.Result.Id}");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine($"Полученный ответ не содержит ожидаемых данных.");
                        Console.WriteLine($"Сообщение: {result.Message}");
                    }
                }
                return true;
            });
        }
        #endregion
        #region VoltageTransformersAPI
        public static async Task<bool> TestVoltageTransformersAPI()
        {
            await VoltageTransformersTestMenu.DoMenuAsync();
            return true;
        }
        private static async Task<bool> TestVoltageTransformersAPI_CheckIdExists()
        {
            return await DoTest(async () =>
            {
                Console.WriteLine("Проверка существования трансформатора напряжения по Id");

                Console.CursorVisible = true;
                Console.Write("Введите Id трансформатора напряжения: ");
                if (!int.TryParse(Console.ReadLine(), out int vTransId))
                {
                    Console.WriteLine("Неправильно введён Id!");
                    return true;
                }
                Console.CursorVisible = false;

                Console.Write("Выполнение запроса... ");
                spinner.StartSpinner();
                TNEBaseDTO<VoltageTransformerExistenceDTO> result = await voltageTransformersAPI.CheckVoltageTransformerExists(vTransId);
                spinner.StopSpinner();
                Console.WriteLine("[OK]");

                if (result == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine("[FAIL]");
                    Console.WriteLine($"Полученный ответ не содержит ожидаемых данных.");
                }
                else
                {
                    Console.WriteLine($"Ожидаемый статус: 200. Полученный статус: {result.StatusCode} [{((result.StatusCode == 200) ? "OK" : "FAIL")}]");

                    if (result.StatusCode == 200)
                    {
                        Console.WriteLine($"Указанный Id {(result.Result.Exists ? "" : "не ")}существует");
                    }
                    else
                        Console.WriteLine("Получен не правильный код ответа.");
                }

                return true;
            });
        }
        private static async Task<bool> TestVoltageTransformersAPI_Create()
        {
            return await DoTest(async () =>
            {
                Console.WriteLine("Создание нового трансформатора напряжения:");

                CreateVoltageTransformerDTO requestDTO = new CreateVoltageTransformerDTO();

                Console.CursorVisible = true;
                Console.Write("Введите номер трансформатора напряжения: ");
                if (!long.TryParse(Console.ReadLine(), out long Number))
                {
                    Console.WriteLine("Неправильно введён номер трансформатора напряжения");
                    return true;
                }
                requestDTO.Number = Number;

                Console.Write("Введите коэфициент трансформации по напряжению: ");
                if (!double.TryParse(Console.ReadLine().Replace(".", ","), out double TransformationRatio))
                {
                    Console.WriteLine("Неправильно введён коэфициент трансформации по напряжению");
                    return true;
                }
                requestDTO.TransformationRatio = TransformationRatio;

                Console.WriteLine("Введите дату поверки для трансформатора напряжения:");

                if (!DateTime.TryParse(Console.ReadLine(), out DateTime VerificationDate))
                {
                    Console.WriteLine("Неправильно введена дата поверки трансформатора напряжения");
                    return true;
                }
                requestDTO.VerificationDate = VerificationDate;

                Console.WriteLine("Введите срок поверки (дату окончания поверочного периода) трансформатора напряжения:");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime VerificationPeriod))
                {
                    Console.WriteLine("Неправильно введён срок поверки (дата окончания поверочного периода) трансформатора напряжения");
                    return true;
                }
                requestDTO.VerificationPeriod = VerificationPeriod;

                Console.Write("Введите Id типа трансформатора напряжения: ");
                if (!int.TryParse(Console.ReadLine(), out int TransformerTypeId))
                {
                    Console.WriteLine("Неправильно введён Id типа трансформатора напряжения");
                    return true;
                }
                requestDTO.TransformerTypeId = TransformerTypeId;
                Console.CursorVisible = false;

                Console.Write("Выполнение запроса... ");
                spinner.StartSpinner();
                TNEBaseDTO<VoltageTransformerDTO> result = await voltageTransformersAPI.CreateVoltageTransformer(requestDTO);
                spinner.StopSpinner();

                if (result == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine("[FAIL]");
                    Console.WriteLine($"Полученный ответ не содержит ожидаемых данных.");
                }
                else
                {
                    Console.WriteLine("[OK]");
                    Console.WriteLine($"Ожидаемый статус: 201. Полученный статус: {result.StatusCode} [{((result.StatusCode == 201) ? "OK" : "FAIL")}]");
                    if (result.StatusCode == 201 && result.Result != null)
                    {
                        Console.WriteLine($"Новый трансформатор напряжения создан успешно. Id: {result.Result.Id}");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine($"Полученный ответ не содержит ожидаемых данных.");
                        Console.WriteLine($"Сообщение: {result.Message}");
                    }
                }
                return true;
            });
        }
        #endregion
        #region TransformerTypesAPI
        public static async Task<bool> TestTransformerTypesAPI()
        {
            await TransformerTypesTestMenu.DoMenuAsync();
            return true;
        }
        private static async Task<bool> TestTransformerTypesAPI_CheckIdExists()
        {
            return await DoTest(async () =>
            {
                Console.WriteLine("Проверить существование типа трансформаторов по Id");

                Console.CursorVisible = true;
                Console.Write("Введите Id типа трансформаторов: ");
                if (!int.TryParse(Console.ReadLine(), out int transformerTypeId))
                {
                    Console.WriteLine("Неправильно введён Id!");
                    return true;
                }
                Console.CursorVisible = false;

                Console.Write("Выполнение запроса... ");
                spinner.StartSpinner();
                TNEBaseDTO<TransformerTypeExistenceDTO> result = await transformerTypesAPI.CheckTransformerTypeExists(transformerTypeId);
                spinner.StopSpinner();
                Console.WriteLine("[OK]");

                if (result == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine("[FAIL]");
                    Console.WriteLine($"Полученный ответ не содержит ожидаемых данных.");
                }
                else
                {
                    Console.WriteLine($"Ожидаемый статус: 200. Полученный статус: {result.StatusCode} [{((result.StatusCode == 200) ? "OK" : "FAIL")}]");

                    if (result.StatusCode == 200)
                    {
                        Console.WriteLine($"Указанный Id {(result.Result.Exists ? "" : "не ")}существует");
                    }
                    else
                        Console.WriteLine("Получен не правильный код ответа.");
                }

                return true;
            });
        }
        private static async Task<bool> TestTransformerTypesAPI_Create()
        {
            return await DoTest(async () =>
            {
                Console.WriteLine("Создание нового типа трансформаторов:");

                CreateTransformerTypeDTO requestDTO = new CreateTransformerTypeDTO();

                Console.CursorVisible = true;
                Console.Write("Введите название типа трансформаторов: ");
                requestDTO.Description = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(requestDTO.Description))
                {
                    Console.WriteLine("Неправильно введёно название типа трансформаторов");
                    return true;
                }

                Console.WriteLine("Введите назначение типа трансформатора (0 - трансформатор тока, 1 - трансформатор напряжения):");
                if (!int.TryParse(Console.ReadLine(), out int TransformerPurpose))
                {
                    Console.WriteLine("Неправильно введено назначение типа трансформатора!");
                    return true;
                }
                requestDTO.TransformerPurpose = TransformerPurpose;
                Console.CursorVisible = false;

                Console.Write("Выполнение запроса... ");
                spinner.StartSpinner();
                TNEBaseDTO<TransformerTypeDTO> result = await transformerTypesAPI.CreateTransformerType(requestDTO);
                spinner.StopSpinner();

                if (result == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine("[FAIL]");
                    Console.WriteLine($"Полученный ответ не содержит ожидаемых данных.");
                }
                else
                {
                    Console.WriteLine("[OK]");
                    Console.WriteLine($"Ожидаемый статус: 201. Полученный статус: {result.StatusCode} [{((result.StatusCode == 201) ? "OK" : "FAIL")}]");
                    if (result.StatusCode == 201 && result.Result != null)
                    {
                        Console.WriteLine($"Новый тип трансформаторов создан успешно. Id: {result.Result.Id}");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine($"Полученный ответ не содержит ожидаемых данных.");
                        Console.WriteLine($"Сообщение: {result.Message}");
                    }
                }
                return true;
            });
        }
        private static async Task<bool> TestTransformerTypesAPI_List()
        {
            return await DoTest(async () =>
            {
                Console.Write("Получение списка типов трансформаторов... ");
                spinner.StartSpinner();
                TNEBaseDTO<TransformerTypesListDTO> result = await transformerTypesAPI.GetAll();
                spinner.StopSpinner();

                if (result == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine("[FAIL]");
                    Console.WriteLine($"Полученный ответ не содержит ожидаемых данных.");
                }
                else
                {
                    Console.WriteLine("[OK]");
                    Console.WriteLine($"Ожидаемый статус: 200. Полученный статус: {result.StatusCode} [{((result.StatusCode == 200) ? "OK" : "FAIL")}]");
                    if (result.StatusCode == 200 && result.Result != null && result.Result.TransformerTypes != null)
                    {
                        if (result.Result.TransformerTypes.Count == 0)
                        {
                            Console.WriteLine($"Ответ получен правильный, однако, список пуст.");
                        }
                        else
                        {
                            Console.WriteLine($"Список типов трансформаторов:");
                        }
                        for (int i = 0; i < result.Result.TransformerTypes.Count; i++)
                        {
                            TransformerTypesListDTO.TransformerTypeListItemDTO transformerType = result.Result.TransformerTypes[i];
                            Console.WriteLine($"{i + 1}. Id: {transformerType.Id}, имя: {transformerType.Description}");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine($"Полученный ответ не содержит ожидаемых данных.");
                        Console.WriteLine($"Сообщение: {result.Message}");
                    }
                }
                return true;
            });
        }
        #endregion
        #region ExpiredElectricEquipmentAPI
        public static async Task<bool> TestExpiredElectricEquipmentAPI()
        {
            await ExpiredElectricEquipmentTestMenu.DoMenuAsync();
            return true;
        }
        private static async Task<bool> TestExpiredElectricEquipmentAPI_EEMeters()
        {
            return await DoTest(async () =>
            {
                Console.Write("Получение списка счётчиков электрической энергии с истёкшими сроками поверки... ");

                Console.CursorVisible = true;
                Console.Write("Введите Id объекта потребления: ");
                if (!int.TryParse(Console.ReadLine(), out int consObjId))
                {
                    Console.WriteLine("Неправильно введён Id объекта потребления");
                    return true;
                }
                Console.CursorVisible = false;

                spinner.StartSpinner();
                TNEBaseDTO<ExpiredElectricEquipmentListDTO<ElectricEnergyMeterDTO>> result = await expiredElectricEquipmentAPI.GetExpiredElectricEnergyMeters(consObjId);
                spinner.StopSpinner();

                if (result == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine("[FAIL]");
                    Console.WriteLine($"Полученный ответ не содержит ожидаемых данных.");
                }
                else
                {
                    Console.WriteLine("[OK]");
                    Console.WriteLine($"Ожидаемый статус: 200. Полученный статус: {result.StatusCode} [{((result.StatusCode == 200) ? "OK" : "FAIL")}]");
                    if (result.StatusCode == 200 && result.Result != null && result.Result.ExpiredElectricEquipment != null)
                    {
                        Console.WriteLine($"Вывод списка для объекта потребления: [{result.Result.ConsumptionObjectId}] {result.Result.ConsumptionObjectName}");
                        if (result.Result.ExpiredElectricEquipment.Count == 0)
                        {
                            Console.WriteLine($"Ответ получен правильный, однако, список пуст.");
                        }
                        else
                        {
                            Console.WriteLine($"Список счётчиков электрической энергии с истёкшими сроками поверки:");
                        }
                        for (int i = 0; i < result.Result.ExpiredElectricEquipment.Count; i++)
                        {
                            ElectricEnergyMeterDTO expiredEquipment = result.Result.ExpiredElectricEquipment[i];
                            Console.WriteLine($"{i + 1}. Id: {expiredEquipment.Id}, ном.: {expiredEquipment.Number}" +
                                $", дата пов.: {expiredEquipment.VerificationDate:dd:MM:yyyy}" +
                                $", пер. пов.: {expiredEquipment.VerificationPeriod:dd:MM:yyyy}" +
                                $", тип: {expiredEquipment.EEMeterTypeDescription}");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine($"Полученный ответ не содержит ожидаемых данных.");
                        Console.WriteLine($"Сообщение: {result.Message}");
                    }
                }
                return true;
            });
        }
        private static async Task<bool> TestExpiredElectricEquipmentAPI_CTransformers()
        {
            return await DoTest(async () =>
            {
                Console.Write("Получение списка трансформаторов тока с истёкшими сроками поверки... ");

                Console.CursorVisible = true;
                Console.Write("Введите Id объекта потребления: ");
                if (!int.TryParse(Console.ReadLine(), out int consObjId))
                {
                    Console.WriteLine("Неправильно введён Id объекта потребления");
                    return true;
                }
                Console.CursorVisible = false;

                spinner.StartSpinner();
                TNEBaseDTO<ExpiredElectricEquipmentListDTO<CurrentTransformerDTO>> result = await expiredElectricEquipmentAPI.GetExpiredCurrentTransformers(consObjId);
                spinner.StopSpinner();

                if (result == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine("[FAIL]");
                    Console.WriteLine($"Полученный ответ не содержит ожидаемых данных.");
                }
                else
                {
                    Console.WriteLine("[OK]");
                    Console.WriteLine($"Ожидаемый статус: 200. Полученный статус: {result.StatusCode} [{((result.StatusCode == 200) ? "OK" : "FAIL")}]");
                    if (result.StatusCode == 200 && result.Result != null && result.Result.ExpiredElectricEquipment != null)
                    {
                        Console.WriteLine($"Вывод списка для объекта потребления: [{result.Result.ConsumptionObjectId}] {result.Result.ConsumptionObjectName}");
                        if (result.Result.ExpiredElectricEquipment.Count == 0)
                        {
                            Console.WriteLine($"Ответ получен правильный, однако, список пуст.");
                        }
                        else
                        {
                            Console.WriteLine($"Список трансформаторов тока с истёкшими сроками поверки:");
                        }
                        for (int i = 0; i < result.Result.ExpiredElectricEquipment.Count; i++)
                        {
                            CurrentTransformerDTO expiredEquipment = result.Result.ExpiredElectricEquipment[i];
                            Console.WriteLine($"{i + 1}. Id: {expiredEquipment.Id}, ном.: {expiredEquipment.Number}" +
                                $", дата пов.: {expiredEquipment.VerificationDate:dd:MM:yyyy}" +
                                $", пер. пов.: {expiredEquipment.VerificationPeriod:dd:MM:yyyy}" +
                                $", КТТ.: {expiredEquipment.TransformationRatio}" +
                                $", тип: {expiredEquipment.TransformerTypeDescription}");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine($"Полученный ответ не содержит ожидаемых данных.");
                        Console.WriteLine($"Сообщение: {result.Message}");
                    }
                }
                return true;
            });
        }
        private static async Task<bool> TestExpiredElectricEquipmentAPI_VTransformers()
        {
            return await DoTest(async () =>
            {
                Console.Write("Получение списка трансформаторов напряжения с истёкшими сроками поверки... ");

                Console.CursorVisible = true;
                Console.Write("Введите Id объекта потребления: ");
                if (!int.TryParse(Console.ReadLine(), out int consObjId))
                {
                    Console.WriteLine("Неправильно введён Id объекта потребления");
                    return true;
                }
                Console.CursorVisible = false;

                spinner.StartSpinner();
                TNEBaseDTO<ExpiredElectricEquipmentListDTO<VoltageTransformerDTO>> result = await expiredElectricEquipmentAPI.GetExpiredVoltageTransformers(consObjId);
                spinner.StopSpinner();

                if (result == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine("[FAIL]");
                    Console.WriteLine($"Полученный ответ не содержит ожидаемых данных.");
                }
                else
                {
                    Console.WriteLine("[OK]");
                    Console.WriteLine($"Ожидаемый статус: 200. Полученный статус: {result.StatusCode} [{((result.StatusCode == 200) ? "OK" : "FAIL")}]");
                    if (result.StatusCode == 200 && result.Result != null && result.Result.ExpiredElectricEquipment != null)
                    {
                        Console.WriteLine($"Вывод списка для объекта потребления: [{result.Result.ConsumptionObjectId}] {result.Result.ConsumptionObjectName}");
                        if (result.Result.ExpiredElectricEquipment.Count == 0)
                        {
                            Console.WriteLine($"Ответ получен правильный, однако, список пуст.");
                        }
                        else
                        {
                            Console.WriteLine($"Список трансформаторов напряжения с истёкшими сроками поверки:");
                        }
                        for (int i = 0; i < result.Result.ExpiredElectricEquipment.Count; i++)
                        {
                            VoltageTransformerDTO expiredEquipment = result.Result.ExpiredElectricEquipment[i];
                            Console.WriteLine($"{i + 1}. Id: {expiredEquipment.Id}, ном.: {expiredEquipment.Number}" +
                                $", дата пов.: {expiredEquipment.VerificationDate:dd:MM:yyyy}" +
                                $", пер. пов.: {expiredEquipment.VerificationPeriod:dd:MM:yyyy}" +
                                $", КТТ.: {expiredEquipment.TransformationRatio}" +
                                $", тип: {expiredEquipment.TransformerTypeDescription}");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine($"Полученный ответ не содержит ожидаемых данных.");
                        Console.WriteLine($"Сообщение: {result.Message}");
                    }
                }
                return true;
            });
        }
        #endregion
    }
}
