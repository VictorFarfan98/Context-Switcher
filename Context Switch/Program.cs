using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;


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
       
        //ScreenManger instance
        static ScreenManager sm = new ScreenManager();

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
            ProcessAdmin admin = new ProcessAdmin();
            
            //admin.listener();

            
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
                    sm.AddLineToBuffer(ref sm.area4, i.ToString());
                }
                else if(i % 3 == 0)
                {
                    sm.AddLineToBuffer(ref sm.area3, i.ToString());
                }
                else if(i % 2 == 0)
                {
                    sm.AddLineToBuffer(ref sm.area2, i.ToString());
                }
                else
                {
                    sm.AddLineToBuffer(ref sm.area1, i.ToString());
                }

                sm.drawScreen();
                admin.getInput(); // getting options from user afterwards: verify received input.
            }

        }

        


    }

    

    
}

