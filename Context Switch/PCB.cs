using System;
using System.Collections.Generic;
using System.Text;

namespace Context_Switch
{
    public class PCB
    {
        public int idproc;
        public int quantum;
        public int quantumProgress;
        public int function_number;
        public Action funcion;
        public int quadrant;
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
            
            // setting a random processID for the user to save.
            Random rnd = new Random();
            this.idproc = rnd.Next(1000);  
            // -----------------RANDOM END -------
            
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

        public void checkProgress()
        {
            if(quantumProgress == quantum)
            {
                this.state = 3;
                deactivate();
            }
        }

    }
}
