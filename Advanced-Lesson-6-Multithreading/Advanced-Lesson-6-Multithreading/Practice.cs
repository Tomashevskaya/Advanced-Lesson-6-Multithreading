using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Advanced_Lesson_6_Multithreading
{
    class Practice
    {      
        /// <summary>
        /// LA8.P1/X. Написать консольные часы, которые можно останавливать и запускать с 
        /// консоли без перезапуска приложения.
        /// </summary>
        public static void LA8_P1_5()
        {            
        }

        /// <summary>
        /// LA8.P2/X. Написать консольное приложение, которое “делает массовую рассылку”. 
        /// </summary>
        public static void LA8_P2_5()
        {           
        }

        /// <summary>
        /// Написать код, который в цикле (10 итераций) эмулирует посещение 
        /// сайта увеличивая на единицу количество посещений для каждой из страниц.  
        /// </summary>
        public static void LA8_P3_5()
        {
            var pages = RemoteDataBase.GetPages();

            for (int j = 0; j < pages.Length; j++)
            {
                var key = pages[j];

                for (int i = 0; i < 10; i++)
                {
                    var thread = new Thread((object pageKey) =>
                    {
                        //lock (random)
                        {
                            var views = RemoteDataBase.GetViews(pageKey.ToString());
                            RemoteDataBase.SetViews(pageKey.ToString(), ++views);
                        }
                        
                    });

                    thread.Start(key);
                }
            }

            Thread.Sleep(5000);

            foreach (var key in pages)
            {
                Console.WriteLine($"{key, 5}: {RemoteDataBase.GetViews(key), 4}");
            }
        }

        /// <summary>
        /// LA8.P4/X. Отредактировать приложение по “рассылке” “писем”. 
        /// Сохранять все “тела” “писем” в один файл. Использовать блокировку потоков, чтобы избежать проблем синхронизации.  
        /// </summary>
        public static void LA8_P4_5()
        {
            var random = new Random();

            for (int i = 0; i < 10; i++)
            {
                var thread = new Thread(() =>
                {
                    lock (random)
                    {
                        File.AppendAllText(@"c:\temp\thread-test\99.txt", random.Next(1000).ToString() + Environment.NewLine);
                    }
                    Thread.Sleep(random.Next(1000));
                });

                thread.Start();

            }
        }

        /// <summary>
        /// LA8.P5/5. Асинхронная “отсылка” “письма” с блокировкой вызывающего потока 
        /// и информировании об окончании рассылки (вывод на консоль информации 
        /// удачно ли завершилась отсылка). 
        /// </summary>
        public async static void LA8_P5_5()
        {
            var email = "text of email";
            var succeed = await SmptServer.SendEmail(email);
            Console.WriteLine(succeed ? "Succeed" : "Failed");
        }
    }    
}
