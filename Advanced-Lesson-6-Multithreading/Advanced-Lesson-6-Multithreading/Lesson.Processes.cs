using Advanced_Lesson_6_Diagnostic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Advanced_Lesson_6_Multithreading
{
    public static partial class Lesson
    {
        public static void SinglePlayerExample()
        {
            var player = new Player("Player 1");
            player.Play();            

            Diagnostic.ListAllRunningProcesses();
            Diagnostic.ListAllProcessThreads();
            Diagnostic.ListAllProcessCodeModules();
            Diagnostic.ListAllAppDomains();
        }

        public static void AppDomainPlayersExample()
        {
            for (int i = 0; i < 4; i++)
            {
                var index = i + 1;
                AppDomain anotherAD = AppDomain.CreateDomain($"Player {index}");
                try
                {
                    var assempbly = anotherAD.Load("Advanced-Lesson-6-AppDomain-Player");
                    assempbly.EntryPoint.Invoke(null, new object[] { new string[] { (index).ToString() } });
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Diagnostic.ListAllAppDomains();
        }

        public static void MultiThreadingExample()
        {
            for (int i = 1; i <= 3; i++)
            {
                var thread = new Thread(() =>
                {
                    var player = new Player($"Thread {i}", i);
                    player.Play();
                });

                thread.Start();

                Console.WriteLine($"Diagnostic from thread #{i}");
                Diagnostic.ListAllProcessThreads();
            }

            Console.WriteLine("Diagnostic from main thread");
            Diagnostic.ListAllProcessThreads();
        }

        public static void ThreadPoolExample()
        {
            for (int i = 1; i <= 3; i++)
            {
                ThreadPool.QueueUserWorkItem((object state) =>
                {
                    var player = new Player($"Thread {i}", i);
                    player.Play();
                });

                
                Console.WriteLine($"Diagnostic from thread #{i}");
                Diagnostic.ListAllProcessThreads();
            }

            Console.WriteLine("Diagnostic from main thread");
            Diagnostic.ListAllProcessThreads();
        }
    }  


    public class Player
    {
        public string Name { get; set; }
        private int? _color;

        public Player(string name, int? color = null)
        {
            this.Name = name;
            this._color = color;
        }        

        public void Play()
        {
            var counter = 0;

            while(counter ++ < 10)
            {
                if (this._color.HasValue)
                    Console.BackgroundColor = (ConsoleColor)_color;

                Console.WriteLine($"Playing from instance {this.Name}");

                Thread.Sleep(500);
                Console.ResetColor();
            }
        }
    }
}
