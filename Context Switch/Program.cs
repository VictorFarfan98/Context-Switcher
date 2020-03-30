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


        //Screen drawing variables
        static List<string> area1 = new List<string>();
        static List<string> area2 = new List<string>();
        static List<string> area3 = new List<string>();
        static List<string> area4 = new List<string>();
        static int areaHeights;
        static int areaWidths;

        //Funcion 1 del programa 1
        public static void f1()
        {
            while(true)
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

        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            //drawScreen();

            int i = 0;
            while (true)
            {
                int height = Console.WindowHeight;
                int width = Console.WindowWidth;

                //Console.WriteLine("Height: " + height + "\nWidth: " + width);


                
                i++;


                //Console.WriteLine("AreaHeight: " + areaHeights + "\nAreaWidth: " + areaWidths);

                // jumb between areas                
                if(i % 4 == 0)
                {
                    AddLineToBuffer(ref area4, i.ToString());
                }
                else if(i % 3 == 0)
                {
                    AddLineToBuffer(ref area3, i.ToString());
                }
                else if(i % 2 == 0)
                {
                    AddLineToBuffer(ref area2, i.ToString());
                }
                else
                {
                    AddLineToBuffer(ref area1, i.ToString());
                }

                drawScreen();
                Console.ReadKey();
            }

        }

        private static void AddLineToBuffer(ref List<string> areaBuffer, string line)
        {
            areaBuffer.Insert(0, line);

            if (areaBuffer.Count == areaHeights)
            {
                areaBuffer.RemoveAt(areaHeights - 1);

            }
        }


        private static void drawScreen()
        {
            try
            {
                Console.Clear();
                areaHeights = (Console.WindowHeight - 2) / 2;
                areaWidths = (Console.WindowWidth) / 2;

                // Draw the area divider for rows
                for (int i = 0; i < Console.WindowWidth; i++)
                {
                    Console.SetCursorPosition(i, areaHeights);
                    Console.Write('-');

                }
                // Draw the area divider for columns
                for (int i = 0; i < Console.WindowHeight - 2; i++)
                {
                    Console.SetCursorPosition(areaWidths, i);
                    Console.Write('|');
                }


                //Draw Area1
                int currentLine = areaHeights - 1;

                for (int i = 0; i < area1.Count; i++)
                {
                    Console.SetCursorPosition(0, currentLine - (i + 1));
                    Console.WriteLine(area1[i]);

                }


                //Draw Area2
                currentLine = areaHeights - 1;
                int currentColumn = areaWidths + 1;
                for (int i = 0; i < area2.Count; i++)
                {
                    Console.SetCursorPosition(currentColumn, currentLine - (i + 1));
                    Console.WriteLine(area2[i]);

                }

                //Draw Area3
                currentLine = (areaHeights * 2);
                for (int i = 0; i < area3.Count; i++)
                {
                    Console.SetCursorPosition(0, currentLine - (i + 1));
                    Console.WriteLine(area3[i]);
                }


                //Draw Area4
                currentLine = (areaHeights * 2);
                for (int i = 0; i < area4.Count; i++)
                {
                    Console.SetCursorPosition(currentColumn, currentLine - (i + 1));
                    Console.WriteLine(area4[i]);
                }


                Console.SetCursorPosition(0, Console.WindowHeight - 1);
                Console.Write("> ");
            }
            catch (ArgumentOutOfRangeException)
            {

            }
            

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

