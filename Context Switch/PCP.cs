using System;
using System.Collections.Generic;
using System.Text;

namespace Context_Switch
{
    public class PCP
    {
        public List<PCB> PCB_cola = new List<PCB>();

        public void PCB_encola(PCB newPCB)
        {
            
            PCB_cola.Add(newPCB);
            Console.WriteLine("Se insertó a cola Idproc:  {0}\n", newPCB.idproc);
            
        }



        public void PCB_list(){
          // antes de enlistar la cola hay que enlistar la cola de kernel?
          int i = 1;
          foreach(PCB pcb in PCB_cola){
            Console.WriteLine("{0}) ProcessID: {1} - Function: {2} - Quantum:{3} \n",i,pcb.idproc,pcb.function_number,pcb.quantum);
            i ++;
          }
          
        }

        public bool PCB_chn(int idp, int qtm){
          
          foreach(PCB pcb in PCB_cola){
            if(pcb.idproc == idp){
              Console.WriteLine("Nuevo Quantum {0} asignado al Proceso ID#{1}",qtm,pcb.idproc);
              pcb.quantum = qtm;
              return true;
            }
            //Console.WriteLine("IdP: {0}\n",pcb.idproc);
          }
          return false;

        }

        public bool PCB_kill(int idp){          
          foreach(PCB pcb in PCB_cola){
            if(pcb.idproc == idp){
              PCB_cola.Remove(pcb);
              
              Console.WriteLine("Proceso ID#{0} killed",pcb.idproc);
              return true;              
            }
            //Console.WriteLine("IdP: {0}\n",pcb.idproc);
          }
            return false;
        }

    }
}
