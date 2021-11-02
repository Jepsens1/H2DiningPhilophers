using System;
using System.Threading;

namespace H2DiningPhilophers
{
    class Program
    {
        static void Main(string[] args)
        {
            //each element indicates if a fork is in use (true)
            bool[] forks = new bool[5];
            Philophers[] philophers = new Philophers[5];
            //Creating 5 threads for each Philospher
            for (int i = 0; i < philophers.Length; i++)
            {
                Philophers philosof = new Philophers(forks, i);
                philophers[i] = philosof;
                Thread Philosofthread = new Thread(philosof.Start);
                Philosofthread.Start();
            }
            while (true)
            {
                //Displays the status of each philophers
                Console.WriteLine($"Philophers 1: {philophers[0].status}\nPhilophers 2: {philophers[1].status}\nPhilophers 3: " +
                    $"{philophers[2].status}\nPhilophers 4: {philophers[3].status}\nPhilophers 5: {philophers[4].status}\n");
                Thread.Sleep(2000);
                Console.Clear();
            }

        }
    }
}
