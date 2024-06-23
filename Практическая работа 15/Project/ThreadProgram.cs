using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Project
{
    public class MyThread : Form1
    {
        Thread progresLive;
        Thread honeyEat;
        List<Thread> thBee = new List<Thread>();

        public MyThread()
        {
            progresLive = new Thread(Live);
            progresLive.Name = "Live";
            honeyEat = new Thread(HoneyEat);
            honeyEat.Name = "Eat";    
        }
        public void ThreadWorkinBeegAdd(int number){
            for (int i = thBee.Count; i < number; i++)
            {
                thBee.Add(new Thread(Moved));
                thBee[i].Name = "WorkingBee" + (i+1);
            }
        }
        public void ThreadWorkingBeeStart()
        {
            foreach (var t in thBee)
                t.Start(thBee.IndexOf(t));
        }

        public void  StartProces()
        {
            progresLive.Start();
            honeyEat.Start();
        }

        public void Stop()
        {
            
        }
    }
}