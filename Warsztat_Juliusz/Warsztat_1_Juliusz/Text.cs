using System;
using System.Collections.Generic;
using System.Text;

namespace Warsztat_1_Juliusz
{
    public static class Text
    {
        public static void WriteLine(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
        }

        public static void Write(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
        }
    }
}
