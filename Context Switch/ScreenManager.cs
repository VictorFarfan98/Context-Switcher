using System;
using System.Collections.Generic;
using System.Text;

namespace Context_Switch
{
    public class ScreenManager
    {

        //Screen drawing variables
        public List<string> area1 = new List<string>();
        public List<string> area2 = new List<string>();
        public List<string> area3 = new List<string>();
        public List<string> area4 = new List<string>();

        //List<List<string>> areas = new List<List<string>>

        public int areaHeights;
        public int areaWidths;

        public void AddLineToBuffer(ref List<string> areaBuffer, string line)
        {
            areaBuffer.Insert(0, line);

            if (areaBuffer.Count == areaHeights)
            {
                areaBuffer.RemoveAt(areaHeights - 1);

            }
        }



        public void drawScreen()
        {
            try
            {
                Console.Clear();
                areaHeights = (Console.WindowHeight - 2) / 2;
                areaWidths = (Console.WindowWidth) / 2;

                // Draw the area divider for rows
                for (int i = 0; i < Console.WindowWidth; i++)
                {
                    Console.SetCursorPosition(i, areaHeights);
                    Console.Write('-');

                }
                // Draw the area divider for columns
                for (int i = 0; i < Console.WindowHeight - 2; i++)
                {
                    Console.SetCursorPosition(areaWidths, i);
                    Console.Write('|');
                }


                //Draw Area1
                int currentLine = areaHeights - 1;

                for (int i = 0; i < area1.Count; i++)
                {
                    Console.SetCursorPosition(0, currentLine - (i + 1));
                    Console.WriteLine(area1[i]);

                }


                //Draw Area2
                currentLine = areaHeights - 1;
                int currentColumn = areaWidths + 1;
                for (int i = 0; i < area2.Count; i++)
                {
                    Console.SetCursorPosition(currentColumn, currentLine - (i + 1));
                    Console.WriteLine(area2[i]);

                }

                //Draw Area3
                currentLine = (areaHeights * 2);
                for (int i = 0; i < area3.Count; i++)
                {
                    Console.SetCursorPosition(0, currentLine - (i + 1));
                    Console.WriteLine(area3[i]);
                }


                //Draw Area4
                currentLine = (areaHeights * 2);
                for (int i = 0; i < area4.Count; i++)
                {
                    Console.SetCursorPosition(currentColumn, currentLine - (i + 1));
                    Console.WriteLine(area4[i]);
                }


                Console.SetCursorPosition(0, Console.WindowHeight - 1);
                //Console.Write("> ");
            }
            catch (ArgumentOutOfRangeException)
            {

            }


        }
    }
}
