using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BallRoomDancersQueue
{
    class Program
    {

        public static SemaphoreSlim LeaderQueue = new SemaphoreSlim(1);
        public static SemaphoreSlim FollowerQueue = new SemaphoreSlim(1);


        static void Main(string[] args)
        {
            Console.WriteLine("How many Dancers in line: ");
            var numDancers = Console.ReadLine();

            OpenDanceFloor(numDancers);
        }

        public static void OpenDanceFloor(string numDancers)
        {
            for(int i = 0; i < Int32.Parse(numDancers); i++)
            {
                Thread leadThread = new Thread(new ParameterizedThreadStart(LeaderThread));
                leadThread.Start(i);
                Thread folThread = new Thread(new ParameterizedThreadStart(FollowerThread));
                folThread.Start(i);
            }

        }


        public static void LeaderThread(object dancerNum)
        {
            FollowerQueue.Release();
            LeaderQueue.Wait();
            Dance(dancerNum);
        }

        public static void FollowerThread(object dancerNum)
        {
            LeaderQueue.Release();
            FollowerQueue.Wait();
            Dance(dancerNum);

        }

        public static void Dance(object dancerNum)
        {
            Console.WriteLine("I'm Dancing: {0}", dancerNum);
            Thread.Sleep(1000);
        }

    }
}
