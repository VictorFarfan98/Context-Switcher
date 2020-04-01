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
        
        
        //Initiliaze all kernel threads
        public void startProgram()
        {
            ProcessAdmin pa = new ProcessAdmin();
            io = new Thread(new ThreadStart(pa.getInput));
        }
    }
}
