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
            Console.WriteLine("Hello World!");
            ProcessAdmin admin = new ProcessAdmin();
            admin.getInput(); // getting options from user afterwards: verify received input.
            admin.listener();

            
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

    public class ProcessAdmin
    {

      string cmd;
      int quantum;
      // commands: Add, Chn, Lstprc, Kill
      String[] accepted_cmds = new string[] { "Add", "Chn", "Lstprc", "Kill","-h"};

      

      public void listener(){

        Console.WriteLine("Press ESC to stop.");
        do {
          while (! Console.KeyAvailable) {
              // Do something when user presses ESC Key
              Console.Write("\n ..Listening.. \n");
              Thread.Sleep(1*1000);
          }   
        } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        // attending user interrupt
        Console.Write("\n Interruption \n");
        getInput();
        // after interrupt is solved, go back & keep listening.
        listener(); 
      }

      public void getInput()
      {
        

        Console.Write("\nEnter your command (-h to get help):\t");
        cmd = Console.ReadLine();
        String[] cmd_split = cmd.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
        verifyInput(cmd_split);
      }
      
      public void verifyInput(string[] cmds)
      {

        if(accepted_cmds.Contains(cmds[0])){
          // si esta dentro de los comandos permitidos
          solve(cmds);
        } else {
          Console.WriteLine("Sorry command not understood.");
          getInput();
        }

      }

      public void solve(string[] cmds)
      {
        /*
        foreach (string cmd in cmds){
          Console.WriteLine(cmd);  
        } */

        switch(cmds[0])
        {
          case "-h":
            // Code block to execute when command is -h (help)
            Console.WriteLine("Commands: \nAdd (int)<<function_type>>  (int)<<quantum number>> \n Function Types: 1. F1 | 2. F2 | 3. F3 | 4. F4 \n");
            Console.WriteLine("Chn (int)<<processId>> (int)<<quantum number>> \n");
            Console.WriteLine("Lstprc \n");
            Console.WriteLine("Kill (int)<<processId>> \n");
            Console.ReadKey();
            // recibir input de nuevo. 
            getInput();
            break;
          case "Add":
            // code block to execute when command is Add
            Console.WriteLine("Command Add: \n Function Type: {0} \n Quantum Number: {1}",cmds[1], cmds[2]);
            break;
          case "Chn":
            // code block to execute when comand is Chn (change)
            Console.WriteLine("Command Chn: \n ProcessId: {0} \n Quantum Number: {1}",cmds[1], cmds[2]);
            break;
          case "Lstprc":
            // code block to execute when comand is Lstprc (list processes)
            Console.WriteLine("Command Listprc");
            break;
          case "Kill":
            // code block to execute when command is Kill
            Console.WriteLine("Command Kill: \n ProcessId {0}\n",cmds[1]);
            break;
          default:
            // code block
            Console.WriteLine("Incorrect command");
            getInput();
            break;
        }
        
      }

    }
}

