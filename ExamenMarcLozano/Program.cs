using System;
using System.Threading;
namespace ExamenMarcLozano
{
    class Program
    {
        private static Robot[] robots = new Robot[3];
        private static Thread[] threads = new Thread[3];
        private static int redBucket = 0;
        private static int greenBucket = 0;
        private static int blueBucket = 0;
        private static volatile int doneRobots = 0;
        private static object myObject = new object();

        private static int MinTimeToMounting = 500;
        private static int MaxTimeToMounting = 1000;
        private static int MinTimePainting = 2000;
        private static int MaxTimePainting = 4000;
        private static int TimeOfCharging = 10000;
        private static Semaphore BuckrtsSem = new Semaphore(1, 1);
        private static bool exit = false;
        static void Main(string[] args)
        {

            robots[0] = new Robot('A',2,1,0,ConsoleColor.Red);
            robots[1] = new Robot('B', 0, 1, 2,ConsoleColor.Green);
            robots[2] = new Robot('C', 1, 1, 1, ConsoleColor.Yellow);

            Thread transporterRobot = new Thread(TransporterRobotThread);
            transporterRobot.Start();

            for (int i = 0; i < 3; i++)
            {
                threads[i] = new Thread(RobotThread);
                threads[i].Name = "Robot " + robots[i].getName();
                threads[i].IsBackground = true;
                threads[i].Start(robots[i]);
            }
            ConsoleKey key = ConsoleKey.Escape;
            while (key != ConsoleKey.Enter)
            {
                key = Console.ReadKey().Key;
                exit = true;
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Waiting for all the robots to finish their jobs...");
            }
            while(doneRobots<4)
            {
            }
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("All threads have successfully finished! Press enter to finish");
            key = ConsoleKey.Escape;
            while (key != ConsoleKey.Enter)
            {
                key = Console.ReadKey().Key;
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Finishing program");
        }
        private static void TransporterRobotThread()
        {
            Random random = new Random();
            int red = 0;
            int green = 0;
            int blue = 0;
            while(!exit)
            {
                BuckrtsSem.WaitOne();
                {
                    red = random.Next(3, 5);
                    redBucket += red;
                    green = random.Next(3, 5);
                    greenBucket += green;
                    blue = random.Next(3, 5);
                    blueBucket += blue;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("The transporter robot is bringing buckets of paint rgb=({0},{1},{2}). Current bucks rgb=({3},{4},{5})", red, green, blue, redBucket, greenBucket, blueBucket);
                }
                BuckrtsSem.Release();
                Thread.Sleep(TimeOfCharging);
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Recharger Robot has finished");
            lock (myObject)
            {
                doneRobots++;
            }
        }
        private static void RobotThread(object robot)
        {
            Robot rob = (Robot)robot;
            Random random = new Random();
            bool canPaint = false;
            bool check = false;
            while (!exit)
            {
                if (rob.emptyBuckets())
                {
                    Console.ForegroundColor = rob.getColor();
                    //Console.WriteLine("{0} is waiting for color", Thread.CurrentThread.Name);
                    BuckrtsSem.WaitOne();
                    {
                        if (redBucket >= rob.getRed() && greenBucket >= rob.getGreen() && blueBucket >= rob.getBlue())
                        {
                            redBucket -= rob.getRed();
                            greenBucket -= rob.getGreen();
                            blueBucket -= rob.getBlue();
                            int sleep = random.Next(MinTimeToMounting, MaxTimeToMounting);
                            Console.ForegroundColor = rob.getColor();
                            Console.WriteLine("[{0}] has adquired rgb=({1},{2},{3}). The current painting is rgb=({4},{5},{6}). Im going to mount the bucks for {7}msc", Thread.CurrentThread.Name, rob.getRed(), rob.getGreen(), rob.getBlue(), redBucket, greenBucket, blueBucket, sleep);
                            Thread.Sleep(sleep);
                            canPaint = true;
                        }
                        else if(!check)
                        {
                            Console.WriteLine("{0} is waiting for color, it will try it again every 250ms", Thread.CurrentThread.Name);
                            check = true;
                        }
                    }
                    BuckrtsSem.Release();
                    if(!canPaint)
                    {
                        Thread.Sleep(250);
                    }
                    
                }
                if (canPaint && !exit)
                {
                    Paint(rob, random);
                    canPaint = false;
                    check = false;
                }
            }
            Console.ForegroundColor = rob.getColor();
            Console.WriteLine("{0} has finished",Thread.CurrentThread.Name);
            lock (myObject)
            {
                doneRobots++;
            }
        }
        private static void Paint(Robot rob,Random random)
        {
            int paintingTime = random.Next(MinTimePainting, MaxTimePainting);
            Console.ForegroundColor = rob.getColor();
            Console.WriteLine("[{0}] is painting ({1} msc)", Thread.CurrentThread.Name, paintingTime);
            rob.useBuckets();
            Thread.Sleep(paintingTime);
        }
    }
}
