using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced_Lesson_6_Multithreading
{
    class Program
    {
        static void Main(string[] args)
        {
            //Lesson.SinglePlayerExample();
            //Lesson.AppDomainPlayersExample();
            //Lesson.MultiThreadingExample();
            //Lesson.ThreadPoolExample();
            //Lesson.UnsyncPlayersExample();
            //Lesson.LockPlayersExample();
            Lesson.MutexPlayerExample();

            Console.ReadLine();
        }
    }
}
