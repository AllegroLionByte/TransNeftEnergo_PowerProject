using System;
using System.Threading.Tasks;

namespace TNEPowerProject.APITestConsoleApp
{
    internal class AsyncSpinner
    {
        public readonly char[] SpinnerSymbols = new char[] { '\\', '|', '/', '-' };
        public bool IsSpinning
        {
            get
            {
                lock (_Lock)
                    return _isSpinning;
            }
            private set
            {
                lock (_Lock)
                    _isSpinning = value;
            }
        }
        private object _Lock = new object();
        private bool _isSpinning = false;
        private int LastPositionX = -1;
        private int LastPositionY = -1;

        public void StartSpinner()
        {
            if (!IsSpinning)
            {
                IsSpinning = true;
                Rotate();
            }
        }
        public void StopSpinner()
        {
            if (IsSpinning)
            {
                IsSpinning = false;
                if (LastPositionX + 1 == Console.CursorLeft && LastPositionY == Console.CursorTop)
                {
                    Console.SetCursorPosition(LastPositionX, LastPositionY);
                    Console.Write(' ');
                }
            }
        }
        private async void Rotate()
        {
            int currentSymbolIndex = 0;
            while (IsSpinning)
            {
                if (LastPositionX + 1 == Console.CursorLeft && LastPositionY == Console.CursorTop)
                    Console.SetCursorPosition(LastPositionX, LastPositionY);
                else
                {
                    LastPositionX = Console.CursorLeft;
                    LastPositionY = Console.CursorTop;
                }

                Console.Write(SpinnerSymbols[currentSymbolIndex]);

                currentSymbolIndex = ++currentSymbolIndex % SpinnerSymbols.Length;
                await Task.Delay(150);
            }
        }
    }
}
