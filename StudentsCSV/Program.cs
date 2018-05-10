/*
 * Лихачев Юра
 * 3. Подсчитать количество студентов:
 * а) учащихся на 5 и 6 курсах;
 * б)подсчитать сколько студентов в возрасте от 18 до 20 лет на каком курсе учатся(частотный массив);
 * в) отсортировать список по возрасту студента;
 * г) *отсортировать список по курсу и возрасту студента.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace StudentsCSV
{
    class Program
    {
        enum FileFormat
        {
            Name = 0,
            LastName = 1,
            Univeristy = 2,
            Faculty = 3,
            Department = 4,
            Age = 5,
            Course = 6,
            Group = 7,
            City = 8
        }

        enum Courses
        {
            First = 1,
            Second = 2,
            Third  = 3,
            Fourth = 4,
            Fifth = 5,
            Sixth = 6
        }

        // Checking student age
        static bool AgeCheck(string[] asInputData, int iAgeMin, int iAgeMax)
        {
            int iAge = int.Parse(asInputData[(int)FileFormat.Age]);
            return (iAge >= iAgeMin) && (iAge <= iAgeMax);
        }

        // Comparing by course and age
        static int CSVComparer(string[] asInputData1, string[] asInputData2)
        {
            int iCourse1 = int.Parse(asInputData1[(int)FileFormat.Course]);
            int iCourse2 = int.Parse(asInputData2[(int)FileFormat.Course]);

            int iAge1 = int.Parse(asInputData1[(int)FileFormat.Age]);
            int iAge2 = int.Parse(asInputData2[(int)FileFormat.Age]);

            if (iCourse1 > iCourse2)
            {
                return 1;
            }
            else if (iCourse1 == iCourse2)
            {
                if (iAge1 > iAge2) { return 1; }
                else if (iAge1 == iAge2) { return 0; }
                else { return -1; }
            }
            else
            {
                return -1;
            }
        }

        static void Main(string[] args)
        {
            FileStream fileStreamStudentsInput = new FileStream("..\\..\\students_1.csv", FileMode.Open, FileAccess.Read);
            StreamReader streamReader = new StreamReader(fileStreamStudentsInput);

            FileStream fileStreamStudentsSortedOutput = new FileStream("..\\..\\students_2.csv", FileMode.Create, FileAccess.Write);
            StreamWriter streamWriter = new StreamWriter(fileStreamStudentsSortedOutput);

            // Array with courses we are interesting in
            int[] aiCoursesShortScope = new int[2] { (int)Courses.Fifth, (int)Courses.Sixth };
            int iStudentsCounter1 = 0;

            // Frequency array
            int[] aiCourses = new int[7];
            for (int iCounter1 = 0; iCounter1 < aiCourses.Length; iCounter1++)
                aiCourses[iCounter1] = 0;

            const int MINIMUM_AGE = 18;
            const int MAXIMUM_AGE = 20;

            // For sorting purposes
            List<string[]> lsRows = new List<string[]>();

            string[] sTemp;

            while(!streamReader.EndOfStream)
            {
                sTemp = streamReader.ReadLine().Split(';');
                
                // Is student from our courses scope?
                if (aiCoursesShortScope.Contains(int.Parse(sTemp[(int)FileFormat.Course])))
                    iStudentsCounter1++;

                if (AgeCheck(sTemp, MINIMUM_AGE, MAXIMUM_AGE))
                    aiCourses[int.Parse(sTemp[(int)FileFormat.Course])]++;

                lsRows.Add(sTemp);
            }
            streamReader.Close();
            fileStreamStudentsInput.Close();

            // List sorting with custom comparer
            lsRows.Sort(CSVComparer);

            // Writing sorted list to file 
            foreach (string[] asRow in lsRows){
                streamWriter.WriteLine(String.Join(";", asRow));
            }
            streamWriter.Close();
            fileStreamStudentsSortedOutput.Close();

            // Output part
            Console.Title = "Some fun with CSV 1.0";
            Console.WriteLine("Данное приложение обрабатывает CSV файл {0} заранее определенным образом.", fileStreamStudentsInput.Name);
            Console.WriteLine("Количество студентов, обучающихся на {0} и {1} курсах:", (int)Courses.Fifth, (int)Courses.Sixth);
            Console.WriteLine(iStudentsCounter1);

            Console.WriteLine();

            Console.WriteLine("Подсчет количества студентов, в возрасте от {0} до {1}, обучающихся на различных курсах:", MINIMUM_AGE, MAXIMUM_AGE);
            for (int iCounter1 = 1; iCounter1 < aiCourses.Length; iCounter1++)
            {
                Console.WriteLine("Курс {0} : {1}", iCounter1, aiCourses[iCounter1]);
            }

            Console.WriteLine();

            Console.WriteLine("Перечень студентов, отсортированный по курсу и возрасту студентов, записан в файл {0}\n", fileStreamStudentsSortedOutput.Name);

            Console.WriteLine("Нажмите любую клавишу для выхода.");
            Console.ReadKey(true);
        }
    }
}
