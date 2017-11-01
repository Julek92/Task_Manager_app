using System;
using System.Collections.Generic;
using System.Text;

namespace Warsztat_1_Juliusz
{
    public class MyMethods
    {
        public static List<Task> LoadData()
        {
            string[] loadTasksData = Task.Load();
            List <Task> localTaskList = new List<Task>();
            for (int i = 0; i < loadTasksData.Length; i++)
            {
                Task myTask = new Task();
                myTask = myTask.Import(loadTasksData[i]);
                localTaskList.Add(myTask);
            }
            return localTaskList;
        }

        public static void ShowData(List<Task> myLocalTaskList)
        {
            Text.WriteLine("Wyświetlam dotychczasowe zadania:", ConsoleColor.White);
            Text.WriteLine("-  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -", ConsoleColor.White);
            Task.ShowWorks(myLocalTaskList);
        }

        public static void SaveData(List<Task> myLocalTaskList)
        {
            string[] exportData = new string[myLocalTaskList.Count];
            int i = 0;
            foreach (var myList in myLocalTaskList)
            {
                Task myTask = myList;
                string workData = myTask.Export();
                exportData[i++] = workData;
            }
            Task.Save(exportData);
        }

        public static List<Task> AddTaskToList(List<Task> myTaskList, Task myTask)
        {
            List<Task> newList = new List<Task>();

            if (myTaskList.Count == 0)
            {
                newList.Add(myTask);
                return newList;
            }
            bool flag = true;
            for (int i = 0; i < myTaskList.Count; i++)
            {
                if (myTaskList[i].startDate < myTask.startDate)
                {
                    newList.Add(myTaskList[i]);
                    if (i == myTaskList.Count - 1)
                    {
                        newList.Add(myTask);
                    }
                }
                else
                {
                    if (flag)
                    {
                        newList.Add(myTask);
                        flag = false;
                    }
                    newList.Add(myTaskList[i]);
                }
            }
            return newList;
        }
    }
}
