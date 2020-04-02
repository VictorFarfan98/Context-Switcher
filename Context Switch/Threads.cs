using System;
using System.Threading;
using System.Collections.Generic;


namespace Context_Switch
{
    class Threads
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

            while (true)
            {
                if (a > 2000)
                {
                    a = 0;
                }
                a++;
                //Here goes a print
            }

        }

        //Funcion 1 del programa 2
        public static void f2()
        {
            while (true)
            {
                if (b > 12000)
                {
                    b = 1;
                }
                b = b * (b + 1);
                //Here goes a print
            }
        }

        //Funcion 1 del programa 3
        public static void f3()
        {
            while (true)
            {
                //Current PCB quantum
                c = currentPCB.quantum;

                //Should be PCB memory location
                d = currentPCB.quantum;

                //Here goes a print
            }

        }

        //Funcion de prueba para Thread 1 
        public static void f4()
        {
            // Set the timeout value, 5 seconds 
            TimeSpan timeout = new TimeSpan(0, 0, 5); //Esto es solo para el parametro del sleep, no es neesario.
            Console.WriteLine("Thread 1 is working!");

            for (int x =0; x <5; x++)
            {
                Console.WriteLine("Hello this is the " + (x+1).ToString() + " iteration of the loop. ---- Thread 1.");
                // Sleep for 5 seconds 
                // Using Sleep() method 
                Thread.Sleep(timeout);
            }Console.ReadKey(); //Para que la consola no se quite

        }
        // Funcion para el Thread 2
        public static void f5()
        {
            for (int x = 0; x < 10; x++)
            {
                Console.WriteLine("Thread 2 is working!");
  
            }
            Console.WriteLine("\nThread 2 finished!\n");
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!\n");
            
            // Creacion de los threads y asignacion a las funciones que corresponden.
            Thread thread = new Thread(new ThreadStart(f4));
            Thread thread2 = new Thread(new ThreadStart(f5));

            // Nombre de los threads
            thread.Name = "1st Thread";         
            thread2.Name = "2nd Thread";

            //Empieza Thread 1
            Console.WriteLine("Thread 1 started...");
            thread.Start();

            //Se suspende el Thread 1
            Thread.Sleep(4000);
            thread.Suspend();
            Console.WriteLine("\nThread 1 suspended...\n");

            //Empieza Thread 2
            Console.WriteLine("Thread 2 started...");
            thread2.Start();

            //Se resume el Thread 1, desde donde se quedo
            thread.Resume();
            Console.WriteLine("\nThread 1 resumed...\n"); //Este se imprime antes, pero la ejecucion del thread si respeta la del thread 2.











            //Console.ReadLine();
            //while (true)
            //{
            //    int height = Console.WindowHeight;
            //    int width = Console.WindowWidth;

            //    Console.WriteLine("Height: " + height + "\nWidth: " + width);
            //}

        }


    }
    