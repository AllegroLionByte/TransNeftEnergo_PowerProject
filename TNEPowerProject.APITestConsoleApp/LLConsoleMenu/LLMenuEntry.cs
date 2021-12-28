using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TNEPowerProject.APITestConsoleApp.LLConsoleMenu
{
    /// <summary>
    /// Элемент меню
    /// </summary>
    internal class LLMenuEntry
    {
        /// <summary>
        /// Текст элемента меню
        /// </summary>
        public string Text { get; private set; }
        /// <summary>
        /// Функция, запускаемая для данного элемента меню
        /// </summary>
        /// <remarks>
        /// Функция должна возвращать true, если ожидается продолжение работы с меню, в противном случае - false
        /// </remarks>
        public Func<Task<bool>> EntryAction { get; private set; }
        /// <summary>
        /// Элемент меню
        /// </summary>
        /// <param name="Text">Текст элемента меню</param>
        /// <param name="EntryAction">Функция, запускаемая для данного элемента меню</param>
        public LLMenuEntry(string Text, Func<Task<bool>> EntryAction)
        {
            this.Text = Text;
            this.EntryAction = EntryAction;
        }
        /// <summary>
        /// Выполняет действие данного элемента меню
        /// </summary>
        /// <returns>true, если ожидается продолжение работы с меню, в противном случае - false</returns>
        public async Task<bool> DoActionAsync()
        {
            return await EntryAction();
        }
    }
}
