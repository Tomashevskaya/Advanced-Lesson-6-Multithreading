using Advanced_Lesson_6_Diagnostic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Advanced_Lesson_6_Multithreading
{
    public static partial class Lesson
    {

        public static void AwaitThreadPlayer()
        {
            var player = new AwaitThreadPlayer();
            var thrd = player.Load(new string[5]);
            //thrd.Join();
            player.Play();
        }

        

        public static void AwaitTaskPlayerExample()
        {
            var player = new AwaitTaskPlayer();
            var task = player.Load(new string[5]);
            task.Wait();
            player.Play();                
        }

        public static void AwaitTaskPlayerExample2()
        {
            var player = new AwaitTaskPlayer();
            player
                .Load(new string[5])
                .ContinueWith((t) => player.Play());           
        }
    }


    public class AwaitThreadPlayer
    {
        public Thread Load(string[] songs)
        {
            var thread = new Thread(() =>
            {
                for (int i = 0; i < songs.Length; i++)
                {
                    Console.WriteLine($"#--> Song #{i + 1} loading");
                    Thread.Sleep(1000);
                }
            });

            thread.Start();

            return thread;
        }

        public void Play()
        {
            Console.WriteLine("Player is playing...");
        }
    }
    

    public class AwaitTaskPlayer
    {
        public Task Load(string[] songs)
        {
            var task = new Task(() =>
            {
                for (int i = 0; i < songs.Length; i++)
                {
                    Console.WriteLine($"#--> Song #{i + 1} loading");
                    Thread.Sleep(1000);
                }
            });

            task.Start();

            return task;
        }

        public void Play()
        {
            Console.WriteLine("Player is playing...");
        }
    }
}
