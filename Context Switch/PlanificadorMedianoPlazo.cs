using System;
using System.Collections.Generic;
using System.Text;

namespace Context_Switch
{
    class PlanificadorMedianoPlazo
    {
        Queue<PCB> PCBqueue = new Queue<PCB>();

        public void Enqueue(PCB newPCB)
        {
            PCBqueue.Enqueue(newPCB);
        }

        public PCB Dequeue()
        {
            if(PCBqueue.Peek() != null)
            {
                return PCBqueue.Dequeue();
            }
            else
            {
                return null;
            }
            
        }

        public void runProcess()
        {
            //Append to kernel running proccesses.             
        }

    }
}
