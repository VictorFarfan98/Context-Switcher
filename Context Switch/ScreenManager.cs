using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Context_Switch
{
    public class ScreenManager
    {

        //Screen drawing variables
        public List<string> area1 = new List<string>();
        public List<string> area2 = new List<string>();
        public List<string> area3 = new List<string>();
        public List<string> area4 = new List<string>();

        //List<List<string>> areas = new List<List<string>> {area1, area2, area3, area4};

        public int areaHeights;
        public int areaWidths;

        public void AddLineToBuffer(ref List<string> areaBuffer, string line)
        {
            try
            {
                areaBuffer.Insert(0, line);
                //areaBuffer.Add(line);

                if (areaBuffer.Count > areaHeights)
                {
                    areaBuffer.RemoveAt(areaHeights - 1);
                    //areaBuffer.RemoveRange(areaHeights - 1, areaBuffer.Count - areaHeights - 1);

                }
                //Console.WriteLine(areaBuffer.Count);
            }
            catch (System.ArgumentException)
            {

            }
            
        }

        public void chooseQuadrant(int quadrant, string text)
        {
            if(quadrant == 1)
            {
                AddLineToBuffer(ref area1, text);
            }
            else if(quadrant == 2)
            {
                AddLineToBuffer(ref area2, text);
            }
            else if (quadrant == 3)
            {
                AddLineToBuffer(ref area3, text);
            }
            else if (quadrant == 4)
            {
                AddLineToBuffer(ref area4, text);
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
                Console.Write("> ");
                Console.Write("Enter your command (-h to get help):\t");
                //System.Threading.Thread.Sleep(5 * 1000);
            }
            catch (ArgumentOutOfRangeException)
            {

            }


        }
    }
}
