using System;

namespace FrizzApp.DataLoader.Common
{
    public static class DataLoaderHelper
    {
        public static void ShowMainMenu()
        {
            Console.WriteLine();
            Console.WriteLine("  ¿Qué querés hacer?");
            Console.WriteLine("1. Crear productos con datos random");
        }

        public static void Reset()
        {
            Program.Main(default);
        }
    }
}
