using Storage.Core.Interfaces;
using System;

namespace WarehouseCRUD.Storage.Sevices
{
    public class ConsoleLogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"{DateTime.Now} => (log) => {message}", ConsoleColor.White);
        }

        public void LogWarning(string message)
        {
            Console.WriteLine($"{DateTime.Now} => (WARN) => {message}", ConsoleColor.Yellow);
        }

        public void LogError(string message)
        {
            Console.WriteLine($"{DateTime.Now} => (ERROR) => {message}", ConsoleColor.Red);
        }
    }
}
