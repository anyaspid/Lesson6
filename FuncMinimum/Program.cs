/*
 * Лихачев Юра
 * 2. *Модифицировать программу нахождения минимума функции так, чтобы можно было передавать функцию в виде делегата. 
 * Сделать меню с различными функциями и представьте пользователю выбор для какой функции и на каком отрезке находить минимум. 
 * Используйте массив делегатов.
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FuncMinimum
{
    class Program
    {
        static class MenuItemsEnum
        {
            public const int First = 0;
            public const int Second = 1;
        }

        public delegate double MyDelegate(double dNumber);

        public static double F1(double x)
        {
            return x * x - 50 * x + 10;
        }

        public static double F2(double x)
        {
            return x * 2;
        }

        public static void SaveFunc(string fileName, MyDelegate myDelegate, double a, double b, double h)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);
            double x = a;
            while (x <= b)
            {
                bw.Write(myDelegate(x));
                x += h;// x=x+h;
            }
            bw.Close();
            fs.Close();
        }
        public static double Load(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader bw = new BinaryReader(fs);
            double min = double.MaxValue;
            double d;
            for (int i = 0; i < fs.Length / sizeof(double); i++)
            {
                // Считываем значение и переходим к следующему
                d = bw.ReadDouble();
                if (d < min) min = d;
            }
            bw.Close();
            fs.Close();
            return min;
        }


        // Method selecting menu, returns menu item index.
        public static int GetMenuIndex()
        {
            string sMenuItem1 = "Вычислить значение x*x-50*x+10";
            string sMenuItem2 = "Вычислить значение x*2";
            int iEnteredValue = -1;

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

        static void Main(string[] args)
        {
            MyDelegate[] aMyDelegates = new MyDelegate[2];

            aMyDelegates[MenuItemsEnum.First] = F1;
            aMyDelegates[MenuItemsEnum.Second] = F2;

            int iIndex = GetMenuIndex();

            SaveFunc("data.bin", aMyDelegates[iIndex], -100, 100, 0.5);

            Console.WriteLine(Load("data.bin"));


            Console.WriteLine("\nНажмите любую клавишу для выхода.");
            Console.ReadKey(true);
        }
    }
}