﻿using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Context_Switch
{
    class Program
    {
        private static EventWaitHandle wh = new AutoResetEvent(false);


        //Current Process
        static int pid;

        //Current PCB
        static PCB currentPCB;
       
        //ScreenManger instance
        static ScreenManager sm = new ScreenManager();

        //Variables that all functions will access
        static int a = 0, b = 1, c, d, cont;

        //Funcion 1 del programa 1
        public static void f1()
        {
            while (currentPCB.quantumProgress < currentPCB.quantum)
            { 
                if (a > 2000)
                {
                    a = 0;
                }
                a++;

                //Here goes a print               
                string texto = String.Format("Funcion {0}, ProcessID: {1}, Resultado: {2}", 1, currentPCB.idproc, a);
                sm.chooseQuadrant(currentPCB.quadrant, texto);
                //Thread.Sleep(500);
                currentPCB.quantumProgress++;
                currentPCB.checkProgress();
            }
        }

        //Funcion 1 del programa 2
        public static void f2()
        {
            while (currentPCB.quantumProgress < currentPCB.quantum)
            {
                if (b > 12000)
                {
                    b = 1;
                }
                b = b * (b + 1);
                
                //Here goes a print                
                string texto = String.Format("Funcion {0}, ProcessID: {1}, Resultado: {2}", 2, currentPCB.idproc, b);
                sm.chooseQuadrant(currentPCB.quadrant, texto);
                currentPCB.quantumProgress++;
                currentPCB.checkProgress();
            }
        }

        //Funcion 1 del programa 3
        public static void f3()
        {
            while (currentPCB.quantumProgress < currentPCB.quantum)
            {
                //Current PCB quantum
                c = currentPCB.quantum;

                //Should be PCB memory location
                d = currentPCB.quantum;

                //Here goes a print
                
                string texto = String.Format("Funcion {0}, ProcessID: {1}, Resultado: {2}", 3, currentPCB.idproc, c);
                sm.chooseQuadrant(currentPCB.quadrant, texto);
                currentPCB.quantumProgress++;
                currentPCB.checkProgress();
            }

        }

        public static void idle()
        {
            sm.chooseQuadrant(currentPCB.quadrant, "Soy idle...");                  
        }

        public static void chooseFunc(PCB pcb)
        {
            switch (pcb.function_number)
            {
                case 0:
                    pcb.funcion = idle;
                    //pcb.quadrant = 4;
                    break;
                case 1:
                    pcb.funcion = f1;
                    //pcb.quadrant = 1;
                    break;
                case 2:
                    pcb.funcion = f2;
                    //pcb.quadrant = 2;
                    break;
                case 3:
                    pcb.funcion = f3;
                    //pcb.quadrant = 3;
                    break;
            }
        }

        [Obsolete]
        static void Main(string[] args)
        {
            //drawScreen();
            PCB testingPCB = new PCB(10, 1);
            
            testingPCB.funcion = f1;
            testingPCB.quadrant = 1;
            //currentPCB = testingPCB;


            PCB otroPCB = new PCB(5, 1);
            otroPCB.quadrant = 2;
            otroPCB.funcion = idle;

            int i = 0;            
            ProcessAdmin admin = new ProcessAdmin();



            //admin.listener();
            //currentPCB = admin.getCurrentRunningProcess()
            currentPCB = admin.getRunningPCB();
            currentPCB.activate();
            while (true)
            {
                try
                {
                    Thread input = new Thread(() => admin.getInput());
                    //Console.WriteLine(currentPCB.idproc);
                    chooseFunc(currentPCB);


                    currentPCB.funcion();
                    sm.drawScreen();
                    input.Start();




                    //Task screen = new Task(() => sm.drawScreen());
                    //screen.Start();

                    //thread.IsBackground = true;
                    //sm.drawScreen();
                    //Thread t2 = new Thread(() => admin.getInput());
                    //t2.Start();
                    //admin.getInput();                    
                    currentPCB = admin.getRunningPCB();
                    
                    
                    
                }
                catch (System.ArgumentException)
                {

                }
                System.Threading.Thread.Sleep(3 * 1000);
                /*
                int height = Console.WindowHeight;
                int width = Console.WindowWidth;

                //Console.WriteLine("Height: " + height + "\nWidth: " + width);


                
                i++;


                //Console.WriteLine("AreaHeight: " + areaHeights + "\nAreaWidth: " + areaWidths);

                // jumb between areas                
                if(i % 4 == 0)
                {
                    idle(4);
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
                */

                //admin.getInput(); // getting options from user afterwards: verify received input.
            }

        }

        


    }

    

    
}

