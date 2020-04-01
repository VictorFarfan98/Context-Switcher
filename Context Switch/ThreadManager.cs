using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Context_Switch
{
    class ThreadManager
    {
        private Thread io;
        private Thread master;

        /* Possible states
         * 0 = Ready
         * 1 = Running
         * 2 = Suspended 
         * 3 = Killed
        */
        public int state;

        public ThreadManager(string name, int actual_f, int state, bool background)
        {
            /*  
             quantum = amount of "time" the process should be active
             function = # of the function it is going to execute
             */
            this.name = name;
            this.function_num = actual_f;    //Number of fucntion where the thread is running/suspended
            this.state = 0;
            this.background = false;
        }

        public void isBackground()
        {
            this.background = true;
        }

        //Initiliaze all kernel threads
        public void startProgram()
        {
            ProcessAdmin pa = new ProcessAdmin();
            io = new Thread(new ThreadStart(pa.getInput));
        }
    }
}
