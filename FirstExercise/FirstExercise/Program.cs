using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace FirstExercise
{
    public class ManagerSettings
    {
        //требование намбер ван
        private static ManagerSettings _instance;
        private ManagerSettings() { }
        public static ManagerSettings GetInstance()
        {
            if (_instance == null) _instance = new ManagerSettings();
            return _instance;
        }
        Dictionary<string, string> settings = new Dictionary<string, string>();
        public void SetSetting(string key, string value)
        {
            settings[key] = value;
        }
        public string GetSetting(string key)
        {
            return settings[key];
        }
        public bool HaveSetting(string key)
        {
            return settings.ContainsKey(key);
        }
        public string DelSetting(string key)
        {
            if (HaveSetting(key))
            {
                string x = $"настройка {key} со значением {settings[key]} была удалена, всего настроек {letSettings() - 1}";
                settings.Remove(key);
                return x;
            }
            else
            {
                return $"Настройка не найдена, всего настроек {letSettings()}";
            }
        }
        public void ShowAllSettings()
        {
            int i = 0;
            foreach (KeyValuePair<string, string> item in settings)
            {
                i += 1;
                Console.WriteLine($"{i}.Настройка: {item.Key}, Значение: {item.Value}");
            }
        }
        public void ClearSettings()
        {
            settings.Clear();
        }
        public bool accept()
        {
            Console.WriteLine("Вы уверены? д/н");
            switch (Console.ReadLine().ToLower())
            {
                case "д": return true;
                default: Console.WriteLine("Отмена");
                    return false;
            }
        }
        public int letSettings()
        {
            return settings.Count;
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("МЕНЕДЖЕР НАСТРОЕК ПРИЛОЖЕНИЯ\n");
            Console.WriteLine("Филиппов Захар\n9ис-450к\nПР №1:ПАТТЕРН ОДИНОЧКА\nДемонстрация работы менеджера настроек\n");

            //проверка (критерий фиве)
            ManagerSettings manager1 = ManagerSettings.GetInstance();
            ManagerSettings manager2 = ManagerSettings.GetInstance();
            Console.WriteLine($"Оба обращения от одного объекта: {ReferenceEquals(manager1, manager2)}");
            Console.WriteLine($"manager1: {manager1.GetHashCode()} manager2: {manager2.GetHashCode()} \n");

            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("\n---МЕНЮ---\n1.Показать все настройки\n2.Добавить/Изменить настройку\n3.Получить значение настройки\n4.Удалить настройку\n5.Очистить все настройки\n6.Выйти из программы");
                
                ManagerSettings manager = ManagerSettings.GetInstance();
                string a, b;
                if (int.TryParse(Console.ReadLine(), out int userInput))
                {
                    switch (userInput)
                    {
                        case 1:
                            if(manager.letSettings() == 0)
                            {
                                Console.WriteLine("Настройки не были найдены");
                            }
                            else
                            {
                                manager.ShowAllSettings();
                            }
                            break;
                        case 2:
                            if (manager.accept())
                            {
                                Console.Write("Введите настройку, которую хотите добавить/изменить: ");
                                a = Console.ReadLine();
                                Console.Write("Введите значение найстройки, которую хотите добавить/изменить: ");
                                b = Console.ReadLine();
                                manager.SetSetting(a, b);
                                Console.WriteLine($"Настройка добавлена/изменена, всего настроек: {manager.letSettings()}");
                            }
                            break;
                        case 3:
                            Console.Write("Введите настройку, у которой хотите получить значение настройки: ");
                            a = Console.ReadLine();
                            if (manager.HaveSetting(a))
                            {
                                Console.WriteLine(manager.GetSetting(a));
                            }
                            else
                            {
                                Console.WriteLine("Настройка не найдена");
                            }
                            break;
                        case 4:
                            if (manager.accept())
                            {
                                Console.Write("Введите настройку для удаления: ");
                                a = Console.ReadLine();
                                Console.WriteLine(manager.DelSetting(a));
                            }
                            break;
                        case 5:
                            if (manager.accept()) 
                            {
                                manager.ClearSettings(); 
                                Console.WriteLine($"Все настройки были удалены, всего настроек: {manager.letSettings()}");
                            }
                            break;
                        case 6:
                            if(manager.accept())
                            {
                                isRunning = false;
                            }
                            break;
                        default:
                            Console.WriteLine("Такого пункта меню не было найдено!");
                            break;

                    }
                }
                else
                {
                    Console.WriteLine("Это не является числом!");
                }
            }
            
            
        }
    }
}
