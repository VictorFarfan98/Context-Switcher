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
                    Console.WriteLine("Command Add: \n Function Type: {0} \n Quantum Number: {1}", cmds[1], cmds[2]);
                    break;
                case "Chn":
                    // code block to execute when comand is Chn (change)
                    Console.WriteLine("Command Chn: \n ProcessId: {0} \n Quantum Number: {1}", cmds[1], cmds[2]);
                    break;
                case "Lstprc":
                    // code block to execute when comand is Lstprc (list processes)
                    Console.WriteLine("Command Listprc");
                    break;
                case "Kill":
                    // code block to execute when command is Kill
                    Console.WriteLine("Command Kill: \n ProcessId {0}\n", cmds[1]);
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
