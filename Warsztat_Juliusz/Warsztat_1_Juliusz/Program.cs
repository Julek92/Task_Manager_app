using System;
using System.Collections.Generic;
using System.Text;

namespace Warsztat_1_Juliusz
{
    class Program
    {
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Console.InputEncoding = Encoding.GetEncoding(852);
            Console.OutputEncoding = Encoding.UTF8;
            List<Task> workList = new List<Task>();
            Text.WriteLine("Witaj. To jest mini menedżer Twoich zadań! Wciśnij dowolny klawisz, aby kontynuować.", ConsoleColor.Magenta);
            Console.ReadKey();
            Console.Clear();
            workList = MyMethods.LoadData();
            MyMethods.ShowData(workList);
            while (true)
            {
                Console.ResetColor();
                Console.WriteLine("============================================================================================");
                Console.WriteLine("Wpisz komendę:\n[EXIT- Wyjście], [ADD- Dodawanie zadania], [SHOW- Wyświetlanie zadań], " +
                                  "[REMOVE- Wybierz i usuń zadanie], \n[SAVE,- Zapisz dane do pliku]. " +
                                  "[LOAD- Odczyt danych z pliku]. [CLEAR- Wyczyść konsolę]");
                string command = Console.ReadLine();
                command = command.ToLower();
                if (command == "exit")
                {
                    MyMethods.SaveData(workList);
                    break;
                }
                else if (command == "add")
                {
                    Task task = new Task();
                    task.AddWork();
                    workList = MyMethods.AddTaskToList(workList, task);
                }
                else if (command == "show")
                {
                    MyMethods.ShowData(workList);
                }
                else if (command == "remove")
                {
                    int indexToRemove = Task.RemoveWork(workList);
                    string taskDescription = workList[indexToRemove].description;
                    workList.RemoveAt(indexToRemove);
                    Text.WriteLine($"Usunięto zadanie: {taskDescription}", ConsoleColor.Cyan);
                }
                else if (command == "save")
                {
                    MyMethods.SaveData(workList);
                }
                else if (command == "load")
                {
                    workList = MyMethods.LoadData();
                }
                else if (command == "clear")
                {
                    Console.Clear();
                }
            }
        }
    }
}
