/*
 * Лихачев Юра
 * 1. Изменить программу вывода функции так, чтобы можно было передавать функции типа double(double,double). 
 * Продемонстрировать работу на функции с функцией a*x^2 и функцией a*sin(x).
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_DD__Delegate
{
    delegate double MyDelegate(double dParam1, double dParam2);

    class Program
    {

        static void Table(MyDelegate DelegatedMethod, double x, double a)
        {


            /*
            Console.WriteLine("----- X ----- Y -----");
            while (x <= b)
            {
                Console.WriteLine("| {0,8:0.000} | {1,8:0.000} |", x, F(x));
                x += 1;
            }
            Console.WriteLine("---------------------");
            */
        }

        static double MultiplyBySqrt(double x, double a)
        {
            return a * x * x;
        }

        static double MultiplyBySin(double x, double a)
        {
            return a * Math.Sin(x);
        }


        static void Main(string[] args)
        {
            // Создаем новый делегат и передаем ссылку на него в метод Table
            Console.WriteLine("Таблица функции MyFunc:");
            // Параметры метода и тип возвращаемого значения, должны совпадать с делегатом
            Table(new Fun(MyFunc), -2, 2);
            Console.WriteLine("Еще раз та же таблица, но вызов организован по новому");
            // Упрощение(c C# 2.0).Делегат создается автоматически.
            Table(MyFunc, -2, 2);
            Console.WriteLine("Таблица функции Sin:");
            Table(Math.Sin, -2, 2); // Можно передавать уже созданные методы
            Console.WriteLine("Таблица функции x^2:");
            // Упрощение(с C# 2.0). Использование анонимного метода
            Table(delegate (double x) { return x * x; }, 0, 3);


            Console.ReadKey();
        }
    }
}
