/*
 * Лихачев Юра
 * 1. Изменить программу вывода функции так, чтобы можно было передавать функции типа double(double,double). 
 * Продемонстрировать работу на функции с функцией a*x^2 и функцией a*sin(x).
 */

 /*
  * Мне так хотелось поиграться со связкой Dictionary + Delegate, что итог выглядит несколько избыточным и сумбурным.
  * 
  */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_DD__Delegate
{
    // I wanted enum, but it require (int) converting before usage like (int)MenuItems.First, it was boring
    static class MenuItemsEnum
    {
        public const int First = 1;
        public const int Second = 2;
    }


    class Program
    {

        // Method selecting menu, returns menu item index.
        public static int GetMenuIndex()
        {
            string sMenuItem1 = "Вычислить значение a*x^2";
            string sMenuItem2 = "Вычислить значение a*sin(x)";
            int iEnteredValue = 0;

            Dictionary<int, string> menuDictionary = new Dictionary<int, string>()
            {
                { MenuItemsEnum.First, sMenuItem1 },
                { MenuItemsEnum.Second, sMenuItem2 }
            };

            Console.WriteLine("Пожалуйста, введите номер варианта для выбора.");
            Console.WriteLine("{0,2} : {1}", MenuItemsEnum.First, sMenuItem1);
            Console.WriteLine("{0,2} : {1}", MenuItemsEnum.Second, sMenuItem2);

            while (!menuDictionary.ContainsKey(iEnteredValue))
                int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out iEnteredValue);
            return iEnteredValue;
        }

        // Delegate definition
        delegate double MyDelegate(double dParam1, double dParam2);

        // Struct for storage delegate and parametres
        struct DelegateUsageFromDictionary
        {
            MyDelegate myDelegate;
            double dParam1;
            double dParam2;

            public DelegateUsageFromDictionary(MyDelegate myDelegate, double dParam1, double dParam2)
            {
                this.myDelegate = myDelegate;
                this.dParam1 = dParam1;
                this.dParam2 = dParam2;
            }

            public double Execute()
            {
                return myDelegate.Invoke(dParam1, dParam2);
            }

        }

        // Method 1
        static double MultiplyBySqrt(double x, double a)
        {
            return a * x * x;
        }

        // Method 2
        static double MultiplyBySin(double x, double a)
        {
            return a * Math.Sin(x);
        }

        static void Main(string[] args)
        {
            double dNumber1 = 1;
            double dNumber2 = 0.5;

            // Initialize struct entries
            DelegateUsageFromDictionary structEntryOne = new DelegateUsageFromDictionary(MultiplyBySqrt, dNumber1, dNumber2);
            DelegateUsageFromDictionary structEntryTwo = new DelegateUsageFromDictionary(MultiplyBySin, dNumber1, dNumber2);

            // Adding struct entries into dictionary
            Dictionary<int, DelegateUsageFromDictionary> myDictionary = new Dictionary<int, DelegateUsageFromDictionary>()
            {
                {MenuItemsEnum.First, structEntryOne},
                {MenuItemsEnum.Second, structEntryTwo}
            };

            Console.Title = "Delegate usage example 1.0";
            Console.WriteLine("Данное приложение использует делегаты.");

            // Execute Delegate from chosen by GetMenuIndex method  struct entry
            Console.WriteLine("{0,5:0.000}", myDictionary[GetMenuIndex()].Execute());

            Console.WriteLine("\nНажмите любую клавишу для выхода.");
            Console.ReadKey(true);
        }
    }
}