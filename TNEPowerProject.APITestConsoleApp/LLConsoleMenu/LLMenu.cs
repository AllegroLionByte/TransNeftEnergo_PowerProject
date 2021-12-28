using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TNEPowerProject.APITestConsoleApp.LLConsoleMenu
{
    /// <summary>
    /// Предоставляет функционал консольного меню
    /// </summary>
    internal class LLMenu
    {
        /// <summary>
        /// Отображаемое в заголовке меню сообщение
        /// </summary>
        public string Title;
        /// <summary>
        /// Список элементов меню
        /// </summary>
        public List<LLMenuEntry> MenuEntries { get; private set; }
        /// <summary>
        /// Цвет текста для элементов по умолчанию
        /// </summary>
        public ConsoleColor DefaultForegroundColor { get; set; } = ConsoleColor.White;
        /// <summary>
        /// Цвет фона для элементов по умолчанию
        /// </summary>
        public ConsoleColor DefaultBackgroundColor { get; set; } = ConsoleColor.Black;
        /// <summary>
        /// Цвет текста для выбранного элемента
        /// </summary>
        public ConsoleColor SelectionForegroundColor { get; set; } = ConsoleColor.DarkGreen;
        /// <summary>
        /// Цвет фона для выбранного элемента
        /// </summary>
        public ConsoleColor SelectionBackgroundColor { get; set; } = ConsoleColor.White;
        /// <summary>
        /// Индекс выбранного в данный момент элемента меню
        /// </summary>
        public int SelectedItemIndex { get; private set; } = -1;
        /// <summary>
        /// Указывает, следует ли автоматически очищать экран после перехода в пункт меню и выхода из меню
        /// </summary>
        public bool AutoClear { get; set; }
        /// <summary>
        /// Указывает, разрешена ли клавиша Escape для выхода из данного меню
        /// </summary>
        public bool AllowEscape { get; set; }
        private int PositionX;
        private int PositionY;
        private ConsoleColor PrevForegroundColor;
        private ConsoleColor PrevBackgroundColor;
        /// <summary>
        /// Предоставляет функционал консольного меню
        /// </summary>
        /// <param name="Title">Отображаемое в заголовке меню сообщение</param>
        /// <param name="AutoClear">Указывает, следует ли автоматически очищать экран после перехода в пункт меню и выхода из меню</param>
        /// <param name="AllowEscape">Указывает, разрешена ли клавиша Escape для выхода из данного меню</param>
        public LLMenu(string Title, bool AutoClear = true, bool AllowEscape = true)
        {
            this.Title = Title;
            this.AutoClear = AutoClear;
            this.AllowEscape = AllowEscape;
            MenuEntries = new List<LLMenuEntry>();
        }
        /// <summary>
        /// Добавляет новый элемент в коллекцию элементов меню
        /// </summary>
        /// <remarks>
        /// Применяется паттерн Builder
        /// </remarks>
        /// <param name="menuEntry">Элемент для добавления</param>
        /// <param name="insertionIndex">
        /// Индекс для вставки элемента. Значения меньше нуля означают вставку в конец
        /// </param>
        public LLMenu AddNewMenuEntry(LLMenuEntry menuEntry, int insertionIndex = -1)
        {
            if (menuEntry == null)
                throw new ArgumentNullException(nameof(menuEntry));
            if (insertionIndex < 0)
                MenuEntries.Add(menuEntry);
            else if (insertionIndex < MenuEntries.Count)
                MenuEntries.Insert(insertionIndex, menuEntry);
            else
                throw new ArgumentOutOfRangeException(nameof(insertionIndex));
            return this;
        }
        /// <summary>
        /// Добавляет элемент для выхода по-умолчанию, позволяет указать текст для элемента
        /// </summary>
        /// <remarks>
        /// Применяется паттерн Builder
        /// </remarks>
        public LLMenu AppendDefaultExitItem(string ExitText = "Назад")
        {
            MenuEntries.Add(new LLMenuEntry(ExitText, async () => await Task.FromResult(false)));
            return this;
        }
        /// <summary>
        /// Выполняет прорисовку меню и ожидает окончание работы с ним
        /// </summary>
        public async Task DoMenuAsync()
        {
            if (this.MenuEntries.Count == 0)
                throw new InvalidOperationException("В меню не добавлены элементы");

            bool prevCurVisible = Console.CursorVisible;
            Console.CursorVisible = false;

            if (SelectedItemIndex == -1)
            {
                PositionX = Console.CursorLeft;
                PositionY = Console.CursorTop;
                SelectedItemIndex = 0;
            }

            PrevForegroundColor = Console.ForegroundColor;
            PrevBackgroundColor = Console.BackgroundColor;

            if (AutoClear)
                Console.Clear();

            bool continueMenu = true;
            while (continueMenu)
            {
                if (AutoClear)
                    Console.Clear();
                Console.SetCursorPosition(PositionX, PositionY);
                Console.ForegroundColor = DefaultForegroundColor;
                Console.BackgroundColor = DefaultBackgroundColor;
                Console.WriteLine(Title);
                for (int i = 0; i < MenuEntries.Count; i++)
                {
                    Console.SetCursorPosition(PositionX, Console.CursorTop);
                    if (i == SelectedItemIndex)
                    {
                        Console.ForegroundColor = SelectionForegroundColor;
                        Console.BackgroundColor = SelectionBackgroundColor;

                        Console.WriteLine($"{i + 1}. {MenuEntries[i].Text}");

                        Console.ForegroundColor = DefaultForegroundColor;
                        Console.BackgroundColor = DefaultBackgroundColor;
                    }
                    else
                    {
                        Console.WriteLine($"{i + 1}. {MenuEntries[i].Text}");
                    }
                }

                bool continueSelection = true;
                while (continueSelection)
                {
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.UpArrow:
                            if (SelectedItemIndex > 0)
                            {
                                Console.ForegroundColor = DefaultForegroundColor;
                                Console.BackgroundColor = DefaultBackgroundColor;
                                Console.SetCursorPosition(PositionX, PositionY + SelectedItemIndex + 1);
                                Console.WriteLine($"{SelectedItemIndex + 1}. {MenuEntries[SelectedItemIndex].Text}");

                                Console.ForegroundColor = SelectionForegroundColor;
                                Console.BackgroundColor = SelectionBackgroundColor;

                                SelectedItemIndex--;
                                Console.SetCursorPosition(PositionX, PositionY + SelectedItemIndex + 1);

                                Console.WriteLine($"{SelectedItemIndex + 1}. {MenuEntries[SelectedItemIndex].Text}");

                                Console.ForegroundColor = DefaultForegroundColor;
                                Console.BackgroundColor = DefaultBackgroundColor;
                            }
                            break;
                        case ConsoleKey.DownArrow:
                            if (SelectedItemIndex < MenuEntries.Count - 1)
                            {
                                Console.ForegroundColor = DefaultForegroundColor;
                                Console.BackgroundColor = DefaultBackgroundColor;
                                Console.SetCursorPosition(PositionX, PositionY + SelectedItemIndex + 1);
                                Console.WriteLine($"{SelectedItemIndex + 1}. {MenuEntries[SelectedItemIndex].Text}");

                                Console.ForegroundColor = SelectionForegroundColor;
                                Console.BackgroundColor = SelectionBackgroundColor;

                                SelectedItemIndex++;
                                Console.SetCursorPosition(PositionX, PositionY + SelectedItemIndex + 1);

                                Console.WriteLine($"{SelectedItemIndex + 1}. {MenuEntries[SelectedItemIndex].Text}");

                                Console.ForegroundColor = DefaultForegroundColor;
                                Console.BackgroundColor = DefaultBackgroundColor;
                            }
                            break;
                        case ConsoleKey.Enter:
                            continueSelection = false;
                            if (AutoClear)
                                Console.Clear();
                            continueMenu = await MenuEntries[SelectedItemIndex].DoActionAsync();
                            break;
                        case ConsoleKey.Escape:
                            continueSelection = continueMenu = false;
                            break;
                    }
                }
            }

            if (AutoClear)
                Console.Clear();
            Console.CursorVisible = prevCurVisible;
            Console.ForegroundColor = PrevForegroundColor;
            Console.BackgroundColor = PrevBackgroundColor;
        }
    }
}
