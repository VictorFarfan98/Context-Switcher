using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;


namespace Context_Switch
{
    class PLP
    {
        ThreadManager tm = new ThreadManager();

        /*
        //List of Kernel Processes
        List<PCB> kernelProcesses = new List<PCB>();
        //Listo of user Processes
        List<PCB> userProcesses = new List<PCB>();
        */

        //List of all Processes
        List<PCB> runningProcesses = new List<PCB>();

        //List of terminated processes 
        List<PCB> terminatedProcesses = new List<PCB>();

        //Current index of running PCB
        private int currentIndex = 0;

        public void addToKernel(PCB PCBtoadd)
        {
            runningProcesses.Add(PCBtoadd);
        }

        public void removeFromKernel(int pID)
        {
            foreach (PCB pcb in runningProcesses)
            {
                if (pcb.idproc == pID)
                {
                    //This process has been killed
                    terminatedProcesses.Add(runningProcesses[pID]);
                    runningProcesses.RemoveAt(pID);
                }
            }
        }

        public PCB getNextPCB()
        {
            if (currentIndex == runningProcesses.Count)
            {
                currentIndex = 0;
            }
            PCB nextPCB = runningProcesses[currentIndex];
            currentIndex++;

            return nextPCB;
        }

        public List<PCB> manageQueue(List<PCB> cola)
        {
            //Pending to add kernel processes to count
            if(runningProcesses.Count < 4)
            {                
                runningProcesses.Add(cola[0]);
                cola.RemoveAt(0);
                cola.TrimExcess();                
            }
            return cola;
        }

        public bool PLP_kill(int idp)
        {
            
            foreach (PCB pcb in runningProcesses)
            {
                if (pcb.idproc == idp)
                {
                    terminatedProcesses.Add(pcb); //Manage history of terminated processes
                    runningProcesses.Remove(pcb);
                    
                    Console.WriteLine("Proceso ID#{0} killed", pcb.idproc);
                    return true;
                    
                }
                //Console.WriteLine("IdP: {0}\n",pcb.idproc);
            }
            return false;
        }

        public void PLP_list()
        {
            // antes de enlistar la cola hay que enlistar la cola de kernel?
            int i = 1;
            foreach (PCB pcb in runningProcesses)
            {
                Console.WriteLine("{0}) ProcessID: {1} - Function: {2} - Quantum:{3} \n", i, pcb.idproc, pcb.function_number, pcb.quantum);
                i++;
            }

        }

        public void PLP_listRemoved()
        {
            // antes de enlistar la cola hay que enlistar la cola de kernel?
            int i = 1;
            foreach (PCB pcb in terminatedProcesses)
            {
                Console.WriteLine("{0}) ProcessID: {1} - Function: {2} - Quantum:{3} \n", i, pcb.idproc, pcb.function_number, pcb.quantum);
                i++;
            }
        }


    }
}
