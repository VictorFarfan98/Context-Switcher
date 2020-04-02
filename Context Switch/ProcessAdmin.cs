using System;
using System.Threading;
using System.Linq;

namespace Context_Switch
{    
    public class ProcessAdmin
    {

        string cmd;
        int quantum;
        // commands: Add, Chn, Lstprc, Kill
        String[] accepted_cmds = new string[] { "Add", "Chn", "Lstprc", "Kill", "-h" };

        private Functions f = new Functions();
        private PCP pcp = new PCP();
        private PLP plp = new PLP();

        public void listener()
        {
            
            Console.WriteLine("Press ESC to stop.");
            do
            {
                while (!Console.KeyAvailable)
                {
                    // Do something when user presses ESC Key
                    Console.Write("\n ..Listening.. \n");
                    Thread.Sleep(1 * 1000);
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
            if (string.IsNullOrEmpty(cmd))
            {
                //Had error on empty input. Force input to NOT be empty
                cmd = "none";
            }
            String[] cmd_split = cmd.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
            verifyInput(cmd_split);
        }

        public void verifyInput(string[] cmds)
        {
            
            if (accepted_cmds.Contains(cmds[0]))
            {
                // si esta dentro de los comandos permitidos
                solve(cmds);
            }
            else
            {
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

            switch (cmds[0])
            {
                case "-h":
                    Console.Clear();
                    // Code block to execute when command is -h (help)
                    Console.WriteLine("Commands: \nAdd (int)<<function_type>>  (int)<<quantum number>> \n Function Types: 1. F1 | 2. F2 | 3. F3 | 4. F4 \n");
                    Console.WriteLine("Chn (int)<<processId>> (int)<<quantum number>> \n");
                    Console.WriteLine("Lstprc \n");
                    Console.WriteLine("Kill (int)<<processId>> \n");
                    Console.WriteLine("Press ANY key to continue...");
                    Console.ReadKey();
                    // recibir input de nuevo. 
                    //getInput();
                    break;
                case "Add":
                    // code block to execute when command is Add
                    //Console.WriteLine(cmd);
                    //Console.WriteLine(cmd.Length.ToString());
                    if(cmds.Length == 3)
                    {                        
                        Console.WriteLine("Command Add: \n Function Type: {0} \n Quantum Number: {1}", cmds[1], cmds[2]);
                        PCB current_pcb = new PCB(Convert.ToInt32(cmds[2]),Convert.ToInt32(cmds[1]));
                        pcp.PCB_encola(current_pcb);
                        pcp.PCB_cola = plp.manageQueue(pcp.PCB_cola);
                        f.waitForKeyPress();
                    }
                    else
                    {
                        Console.WriteLine("Incomplete command.");
                        f.waitForKeyPress();
                    }
                    
                    break;
                case "Chn":
                    // code block to execute when comand is Chn (change)
                    if(cmds.Length == 3){
                        bool encontro;
                        int idp = Convert.ToInt32(cmds[1]);
                        int qtm = Convert.ToInt32(cmds[2]);

                        Console.WriteLine("Command Chn: \n ProcessId: {0} \n Quantum Number: {1}", cmds[1], cmds[2]);
                        encontro = pcp.PCB_chn(Convert.ToInt32(cmds[1]), Convert.ToInt32(cmds[2]));

                        if(encontro)
                        {
                            Console.WriteLine("Nuevo Quantum {0} asignado al Proceso ID#{1}", qtm, idp);
                        }
                        else
                        {
                            Console.WriteLine("No se pudo cambiar el quantum {1} del proceso #{0} | Ya que no fue encontrado. \n", idp, qtm);
                        }
                        f.waitForKeyPress();
                    } else {
                        Console.WriteLine("Incomplete command.");
                        f.waitForKeyPress();
                    }
                    
                    break;
                case "Lstprc":
                    // code block to execute when comand is Lstprc (list processes)
                    Console.WriteLine("Command Listprc");
                    plp.PLP_list();
                    pcp.PCB_list();

                    f.waitForKeyPress();
                    break;
                case "Kill":
                    // code block to execute when command is Kill
                    if(cmds.Length == 2)
                    {
                        bool encontro;

                        encontro = plp.PLP_kill(Convert.ToInt32(cmds[1]));
                        if (!encontro)
                        {
                            pcp.PCB_kill(Convert.ToInt32(cmds[1]));
                        }
                        
                        Console.WriteLine("ProcessId {0} kiled \n", cmds[1]);


                        if (encontro)
                        {
                            Console.WriteLine("Proceso ID#{0} killed", Convert.ToInt32(cmds[1]));
                        }
                        else
                        {
                            Console.WriteLine("No se pudo kill el proceso proceso #{0} | Ya que no fue encontrado. \n", Convert.ToInt32(cmds[1]));
                        }


                        f.waitForKeyPress();
                    }
                    else
                    {
                        Console.WriteLine("Incomplete command.");
                        f.waitForKeyPress();
                    }
                    
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