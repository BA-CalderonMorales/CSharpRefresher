using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundamentals.Where
{
    internal class Where
    {
        public static void Lesson()
        {
            InternalLesson1();
            InternalLesson2();
            InternalLesson3();
        }
        public static void InternalLesson1()
        {
            List<int> numList = new List<int> { 1, 9, 2, 6, 3 };
            var evenList = numList.Where(a => a % 2 == 0).ToList();
            foreach (var n in evenList)
            {
                Console.WriteLine(n);
            }

            var rangeList = numList.Where(x => (x > 2) || (x < 9)).ToList();
            foreach (var r in rangeList)
            {
                Console.WriteLine(r);
            }
        }

        private static void InternalLesson2()
        {
            List<int> flipList = new List<int>();
            int i = 0;
            Random rnd = new Random();
            while (i < 100)
            {
                flipList.Add(rnd.Next(1, 3));
                i++;
            }
            Console.WriteLine("Heads : {0}",
                flipList.Where(a => a == 1).ToList().Count());

            Console.WriteLine("Tails: {0}",
                flipList.Where(a => a == 2).ToList().Count());
        }

        private static void InternalLesson3()
        {
            var nameList = new List<string>
            {
                "Doug",
                "Sally",
                "Sue"
            };
            var sNameList = nameList.Where(n => n.StartsWith("S"));
            foreach (var s in sNameList)
            {
                Console.WriteLine(s);
            }
        }
    }

}
