using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fb2ePub
{
    /// <summary>
    /// Used to output text to console in such way it shown with pauses even if it does not fit the screen
    /// </summary>
    internal class ConsoleWriter
    {
        private readonly int _consoleHeight = Console.WindowHeight;

        public void WriteLine(string stringToWrite)
        {
            int cursorPosition = Console.CursorTop;
            if (cursorPosition + 2 >= _consoleHeight)
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
            Console.WriteLine(stringToWrite);
        }
        public void WriteLine()
        {
            WriteLine("");
        }
    }
}
