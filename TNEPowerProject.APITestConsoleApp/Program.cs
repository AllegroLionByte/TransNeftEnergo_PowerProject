using Refit;
using System;
using System.Threading.Tasks;
using TNEPowerProject.APITestConsoleApp.LLConsoleMenu;
using TNEPowerProject.Contract.Interfaces;

namespace TNEPowerProject.APITestConsoleApp
{
    internal class Program
    {
        private const string APIAddress = "https://localhost:8081";

        private static readonly RefitSettings refitSettings;

        private static readonly IHeartbeatAPI heartbeatAPI;
        private static readonly IElectricEnergyMeterTypesAPI electricEnergyMeterTypesAPI;
        private static readonly ITransformerTypesAPI transformerTypesAPI;

        private static readonly LLMenu MainMenu = new LLMenu("Выберите действие:")
               .AddNewMenuEntry(new LLMenuEntry("Проверить статус API", TestHeartbeatAPI))
               .AddNewMenuEntry(new LLMenuEntry("Тестирование API для типов счётчиков электрической энергии", TestElectricEnergyMeterTypesAPI))
               .AppendDefaultExitItem("Выход");



        private static readonly LLMenu EEMeterTypesTestMenu = new LLMenu("Выберите действие для тестирования API типов счётчиков электрической энергии:")
               .AddNewMenuEntry(new LLMenuEntry("Вывести список типов счётчиков электрической энергии", TestEEMeterTypesAPI_TypesList))
               .AppendDefaultExitItem();

        static Program()
        {
            refitSettings = new RefitSettings { ContentSerializer = new NewtonsoftJsonContentSerializer() };
            heartbeatAPI = RestService.For<IHeartbeatAPI>(APIAddress, refitSettings);
            electricEnergyMeterTypesAPI = RestService.For<IElectricEnergyMeterTypesAPI>(APIAddress, refitSettings);
            //transformerTypesAPI = RestService.For<ITransformerTypesAPI>(APIAddress, refitSettings);
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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine("[Ошибка]");
                Console.WriteLine("Не удалось подключиться к серверу.");
                Console.WriteLine(ex.Message);
            }
            Console.ForegroundColor = MainMenu.DefaultForegroundColor;
            Console.Write("Нажмите любую клавишу для продолжения...");
            Console.ReadKey(true);
            return ans;
        }

        private static async Task<bool> TestHeartbeatAPI()
        {
            return await DoTest(async () =>
            {
                Console.WriteLine($"Адрес API: {APIAddress}");
                string preparedString = Guid.NewGuid().ToString().ToLower();

                Console.WriteLine($"Подготовленная строка: {preparedString}");
                Console.Write("Запрос... ");

                string answer = await heartbeatAPI.Heartbeat(preparedString);
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

        #region ElectricEnergyMeterTypesAPI
        private static async Task<bool> TestElectricEnergyMeterTypesAPI()
        {
            await EEMeterTypesTestMenu.DoMenuAsync();
            return true;
        }
        private static async Task<bool> TestEEMeterTypesAPI_TypesList()
        {
            Console.Write("TEST");
            await Task.Delay(1000);
            return true;
        }
        #endregion
    }
}
