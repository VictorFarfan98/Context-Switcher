using System;
using System.Threading;
using System.Collections.Generic;


namespace Context_Switch
{
    class Program
    {
        //Variables that all functions will access
        static int a = 0, b = 1, c, d, cont;

        //Current Process
        static int pid;

        //Current PCB
        static PCB currentPCB;

        //PCBs Queue
        Queue<PCB> PCBqueue = new Queue<PCB>();

        //Funcion 1 del programa 1
        public static void f1()
        {
            if(a > 2000)
            {
                a = 0;
            }
            a++;
            //Here goes a print
        }

        //Funcion 1 del programa 2
        public static void f2()
        {
            if(b > 12000)
            {
                b = 1;
            }
            b = b * (b + 1);
            //Here goes a print
        }

        //Funcion 1 del programa 3
        public static void f3()
        {
            //Current PCB quantum
            c = currentPCB.quantum;

            //Should be PCB memory location
            d = currentPCB.quantum;

            //Here goes a print

        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    public class PCB
    {
        public int quantum;
        public int quantumProgress;
        public int function_number;
        private volatile bool active; //Defines if the PCB is currently active on a thread

        /* Possible states
         * 0 = Admitted
           1 = Ready Suspended
           2 = Ready 
           3 = Finished
             */
        public int state;

        /*
        public int[] context;
        public Action funcion;     
        
        */

        public PCB(int quantum, int function)
        {
            /*  
             quantum = amount of "time" the process should be active
             function = # of the function it is going to execute
             */ 
            this.quantum = quantum;
            this.function_number = function;
            this.state = 0;
        }
        
        public void activate()
        {
            active = true;
        }
        public void deactivate()
        {
            this.active = false;
        }
        public bool isActive()
        {
            return active;
        }

        
    }
}

