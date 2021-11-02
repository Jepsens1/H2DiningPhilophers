using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace H2DiningPhilophers
{
    class Philophers
    {
        bool[] forks;
        public int leftfork { get; set; }
        int number;
        public int rightfork { get; set; }
        public string status { get; set; } = "Thinking";

        public Philophers(bool[] forks, int number)
        {
            this.forks = forks;
            this.number = number;
            leftfork = this.number;
            rightfork = (this.number + 1) % 5;
        }
        public void Start()
        {
            //Creating random, so its random how long a philosphers will wait and how long he will eat
            Random r = new Random(Guid.NewGuid().GetHashCode());
            while (true)
            {
                Thread.Sleep(r.Next(1000, 5000));
                //Changes status
                status = "Waiting";
                //Locks
                Monitor.Enter(forks);
                try
                {
                    while (forks[rightfork] == true || forks[leftfork] == true)
                    {
                        Monitor.Wait(forks);
                    }
                    //Sets right and left fork to true, which means its in use, and changes status and uses pulseall, to let every thread know
                    forks[rightfork] = true;
                    forks[leftfork] = true;
                    status = "Eating";
                    Monitor.PulseAll(forks);

                }
                finally
                {
                    Monitor.Exit(forks);
                }

                //Sleeps to simulate eating
                Thread.Sleep(r.Next(1000, 5000));

                //Locks
                Monitor.Enter(forks);
                try
                {
                    if (forks[rightfork] == false || forks[leftfork] == false)
                    {
                        Debug.WriteLine("Something went wrong");
                    }
                    forks[rightfork] = false;
                    forks[leftfork] = false;
                    Monitor.PulseAll(forks);
                }
                finally
                {
                    Monitor.Exit(forks);
                }
                status = "Thinking";
            }
        }
    }
}
