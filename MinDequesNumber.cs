using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;

namespace MinDequesNumber
{
    public class MinDequesNumber
    {
        // Create a list of linked lists names deques
        public static List<LinkedList<int>> deques = new List<LinkedList<int>>();
        public static List<int> dequeOrderList = new List<int>();


        public static int Solve(int[] data)
        {

            // Clear deques list
            deques.Clear();

            // Add first deque
            deques.Add(new LinkedList<int>());
            deques[0].AddFirst(data[0]);
            dequeOrderList.Add(0);



            // Cycle through input data from index 1 and assign positions in deques
            for (int i = 1; i < data.Length; i++)
            {
                // Setup variables to send and recieve from Determin Action
                int[] actionArray = DeterminAction(data, i);
                int action = actionArray[0];
                switch (action)
                {
                    // Case 1, Insert to High in deque [,x]
                    case 1:
                        deques[actionArray[1]].AddLast(data[i]);
                        break;

                    // Case 2, Insert Low in deque [,x]
                    case 2:
                        deques[actionArray[1]].AddFirst(data[i]);
                        break;

                    // insert into new Deque
                    case 3:
                        deques.Add(new LinkedList<int>());
                        //deques.Count++;
                        int newDeque = deques.Count - 1;
                        deques[newDeque].AddFirst(data[i]);
                        break;
                    default:
                        break;
                }

            }
            //PrintDeques();
            return deques.Count;

        }// end solve


        private static int[] DeterminAction(int[] data, int index)
        {
            int toProcess = data[index];
            for (int i = 0; i < deques.Count; i++)
            {
                int dequeHigh = deques[i].Last.Value;
                // If larget than Last (Highest in deque)
                if (toProcess > deques[i].Last.Value)
                {
                    bool inbetweenFound = false;
                    // check no value inbetween the 2
                    for (int j = 0; j < data.Length; j++)
                    {
                        if (data[j] < toProcess && data[j] > deques[i].Last.Value)
                        {
                            inbetweenFound = true;
                        }

                    }
                    if (!inbetweenFound)
                    {
                        return new int[] { 1, i };
                    }
                }

                // Ifsmaller  than First (Lowest in deque)
                if (toProcess < deques[i].First.Value)
                {
                    bool inbetweenFound = false;
                    // check no value inbetween the 2
                    for (int j = 0; j < data.Length; j++)
                    {
                        if (data[j] > toProcess && data[j] < deques[i].First.Value)
                        {
                            inbetweenFound = true;
                        }

                    }
                    if (!inbetweenFound)
                    {
                        return new int[] { 2, i };
                    }
                }

            }
            return new int[] { 3, 0 };
        } // end DeterminAction

        private static void CalculateDequeOrder()
        {
            List<int[]> dequeValues = new List<int[]>();
            dequeOrderList = new List<int>();

            for (int j = 0; j < deques.Count; j++)
            {
                int[] temp = new int[3];
                temp[0] = deques[j].First.Value;
                temp[1] = j;
                temp[2] = 0;
                dequeValues.Add(temp);
            }

            int i = 0;
            while (i < deques.Count)
            {
                int minValue = 9999999;
                int minDeque = -1;
                for (int d = 0; d < dequeValues.Count; d++)
                {
                    if (dequeValues[d][2] == 0 && dequeValues[d][0] < minValue)
                    {
                        minValue = dequeValues[d][0];
                        minDeque = d;
                    }
                }
                dequeOrderList.Add(minDeque);
                dequeValues[minDeque][2] = 1;
                i++;
            }
        }// end Calculate Order

        private static void PrintDeques()
        {
            CalculateDequeOrder();
            string toPrint = "";

            for (int i = 0; i < dequeOrderList.Count; i++)
            {
                foreach (var value in deques[dequeOrderList[i]])
                {
                    toPrint += (value.ToString() + ", ");
                }

            }
            Console.WriteLine(toPrint);
        }
    }// end class
}// end namespace