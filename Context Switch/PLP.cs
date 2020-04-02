using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;


namespace Context_Switch
{
    class PLP
    {
        ThreadManager tm = new ThreadManager();

        //List of Kernel Processes
        List<PCB> kernelProcesses = new List<PCB>();
        //Listo of user Processes
        List<PCB> userProcesses = new List<PCB>();


        //List of all Processes
        List<PCB> runningProcesses = new List<PCB>();

        //List of terminated processes 
        List<PCB> terminatedProcesses = new List<PCB>();

        public void addToKernel(PCB PCBtoadd)
        {
            runningProcesses.Add(PCBtoadd);
        }

        public void removeFromKernel(int pID)
        {
            foreach(PCB pcb in runningProcesses)
            {
                if(pcb.idproc == pID)
                {
                    //This process has been killed
                    terminatedProcesses.Add(runningProcesses[pID]);
                    runningProcesses.RemoveAt(pID);
                }
            }
        }

    }
}
