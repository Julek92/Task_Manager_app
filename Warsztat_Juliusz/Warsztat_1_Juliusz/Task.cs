using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Warsztat_1_Juliusz
{
    public class Task
    {
        public string description;
        public DateTime startDate;
        public DateTime? endDate;
        public bool isAllDay;
        public bool isImportant;
        public static bool showMode = true;

        public void AddWork()//dodaje zadanie do listy
        {
            ConsoleColor myColor = ConsoleColor.Green;
            Text.WriteLine("Dodawanie zadania. ", myColor);
            Text.WriteLine("Podaj opis zadania: ", myColor);
            description = Console.ReadLine();

            Text.WriteLine("Czy zadanie będzie ważne? [t/n] ", myColor);
            string _isImportant = Console.ReadLine();
            if (_isImportant == "t")
            {
                isImportant = true;
            }
            else if (_isImportant == "n")
            {
                isImportant = false;
            }
            else
            {
                Text.WriteLine("Odpowiedź musi być typu t/n!!!", ConsoleColor.Red);
            }

            Text.WriteLine("Podaj datę rozpoczęcia zadania:[yyyy-MM-dd]", myColor);
            string readTime = Console.ReadLine();
            startDate = Convert.ToDateTime(readTime);

            Text.WriteLine("Czy chcesz, aby zdarzenie było całodniowe? [t/n]", myColor);
            string _isAllDay = Console.ReadLine();

            if (_isAllDay == "t")
            {
                isAllDay = true;
                endDate = null;
            }
            else if (_isAllDay == "n")
            {
                isAllDay = false;
                Text.WriteLine("Podaj godzinę rozpoczęcia zadania:[hh:mm:ss]", myColor);
                readTime = Console.ReadLine();
                DateTime myReadTime = Convert.ToDateTime(readTime);
                TimeSpan myTime = new TimeSpan(myReadTime.Hour,myReadTime.Minute,myReadTime.Second);
                startDate = startDate + myTime;

                Text.WriteLine("Ile czasu będzie trwało zadanie? [hh:mm:ss]\nJeśli chcesz podać pełną datę wpisz 'date'", myColor);
                readTime = Console.ReadLine();

                if (readTime != "date")
                {
                    myReadTime = Convert.ToDateTime(readTime);
                    myTime = new TimeSpan(myReadTime.Hour, myReadTime.Minute, myReadTime.Second);
                    endDate = startDate + myTime;
                }
                else
                {
                    Text.WriteLine("Podaj pełną datę zakończenia zadania: [yyyy-mm-dd hh:mm:ss]", myColor);
                    readTime = Console.ReadLine();
                    endDate = Convert.ToDateTime(readTime);
                }
            }
        }

        public static void ShowWorks(List<Task> mylist)//wyświetla wszystkie zadania
        {
            int i = 0;
            foreach (var numbers in mylist)
            {
                ConsoleColor myConsoleColor = ConsoleColor.White;
                string flag = "";
                if (numbers.isImportant)
                {
                    myConsoleColor = ConsoleColor.Yellow;
                    flag = " [WAŻNE] ";
                }
                if (showMode)
                {
                    Text.WriteLine($"{numbers.description} {flag}", myConsoleColor);
                }
                else
                {
                    Text.WriteLine($"[{i++}] {numbers.description} {flag}", myConsoleColor);
                }
                
                if (numbers.isAllDay)
                {
                    string start = numbers.startDate.ToString("D");
                    Text.WriteLine($"{start} - [CAŁODNIOWE]", myConsoleColor);
                }
                
                else
                {
                    string start = numbers.startDate.ToString("f");
                    DateTime endDate = Convert.ToDateTime(numbers.endDate); //endDate jest typu nullable (DateTime?), więc trzeba przekonwertować na (DateTime)

                    if (numbers.startDate.Year == endDate.Year)
                    {
                        if (numbers.startDate.DayOfYear == endDate.DayOfYear)
                        {
                            start = numbers.startDate.ToString("t");
                            string startDate = numbers.startDate.ToString("D");
                            string end = endDate.ToString("t");
                            Text.WriteLine($"{startDate}, Od {start}  do {end}", myConsoleColor);
                        }
                        else
                        {
                            string end = endDate.ToString("dddd, dd MMMM HH:mm");
                            Text.WriteLine($"Początek zadania: {start}. Koniec zadania: {end}", myConsoleColor);
                        }
                    }
                    else
                    {
                        string end = endDate.ToString("f");
                        Text.WriteLine($"Początek zadania: {start}. Koniec zadania: {end}", myConsoleColor);
                    }
                }
                Console.ResetColor();
                Console.WriteLine("-  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -");
            }
        }

        public string Export() //zwrata tekst z danymi całego zadania
        {
            string _isAllDay;
            string _isImportant;

            if (isAllDay) {_isAllDay = "t";}
            else { _isAllDay = "n";}

            if (isImportant) { _isImportant = "t"; }
            else { _isImportant = "n"; }

            string workData = $"{description};{startDate};{endDate};{_isAllDay};{_isImportant}";
            return workData;
        }

        public Task Import(string workData)//zamienia ciąg tekstowy na komponenty Task
        {
            string[] taskComponents = workData.Split(';');
            description = taskComponents[0];
            startDate = Convert.ToDateTime(taskComponents[1]);
            if (taskComponents[3] == "t")
            {
                isAllDay = true;
            }
            else
            {
                endDate = Convert.ToDateTime(taskComponents[2]);
            }
            if (taskComponents[4] == "t")
            {
                isImportant = true;
            }
            return this;
        }

        public static int RemoveWork(List<Task> mylist)//usuwa zadanie
        {
            showMode = false;
            Text.WriteLine("Jesteś w trybie usuwania zadań", ConsoleColor.Cyan);
            Text.WriteLine("Wyświetlam listę zadań. Wpisz Numer zadania, aby go usunąć.", ConsoleColor.Cyan);
            ShowWorks(mylist);
            Text.WriteLine("Wpisz Numer zadania, aby go usunąć:", ConsoleColor.Cyan);
            int readWorkNumber = int.Parse(Console.ReadLine());
            return readWorkNumber;
        }

        public static void Save(string[] text)//zapisuje ciąg tekstowy do pliku
        {
            string file = @"ProgramData\Data.txt";
            if (!File.Exists(file))
            {
                File.Create((file));
            }
            File.WriteAllLines(file, text);
        }

        public static string[] Load()
        {
            string file = @"ProgramData\Data.txt";
            string[] loadData = File.ReadAllLines(file);
            return loadData;
        }
    }
}
