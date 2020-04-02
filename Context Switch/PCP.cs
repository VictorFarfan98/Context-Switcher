using System;
using System.Collections.Generic;
using System.Text;

namespace Context_Switch
{
    public class PCP
    {
        List<PCB> PCB_cola = new List<PCB>();

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

        public void PCB_chn(int idp, int qtm){
          bool encontro = false;
          foreach(PCB pcb in PCB_cola){
            if(pcb.idproc == idp){
              Console.WriteLine("Nuevo Quantum {0} asignado al Proceso ID#{1}",qtm,pcb.idproc);
              pcb.quantum = qtm;
              encontro = true;
            }
            //Console.WriteLine("IdP: {0}\n",pcb.idproc);
          }
          if(!encontro){
            Console.WriteLine("No se pudo cambiar el quantum {1} del proceso #{0} | Ya que no fue encontrado. \n",idp,qtm);
          }

        }

        public void PCB_kill(int idp){
          bool encontro = false;
          foreach(PCB pcb in PCB_cola){
            if(pcb.idproc == idp){
              PCB_cola.Remove(pcb);
              encontro = true;
              Console.WriteLine("Proceso ID#{0} killed",pcb.idproc);
              break;
            }
            //Console.WriteLine("IdP: {0}\n",pcb.idproc);
          }
          if(!encontro){
            Console.WriteLine("No se pudo kill el proceso proceso #{0} | Ya que no fue encontrado. \n",idp);
          }
        }

    }
}
