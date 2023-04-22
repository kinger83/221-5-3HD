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

        public static int Solve(int[] data)
        {
            // Clear deques list
            deques.Clear();

            // Add first deque
            deques.Add(new LinkedList<int>());
            deques[0].AddFirst(data[0]);

            // Cycle through input data from index 1 and assign positions in deques
            for (int i = 1; i < data.Length; i++)
            {
                // Setup variables to send and recieve from Determin Action
                int[] actionArray = DeterminAction1(data, i);
                int action = actionArray[0];
                switch (action)
                {
                    // Case 1, Insert first in deque [,x]
                    case 1:
                        deques[actionArray[1]].AddLast(data[i]);
                        break;

                    // Case 2, Insert Last in deque [,x]
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
            PrintDeques();
            return deques.Count;

        }// end solve


        private static int[] DeterminAction2(int[] data, int index)
        {
            // calculate how many deques needed
            for (int i = 0; i < data.Length; i++)
            {

            }




            // temp retunr to block errors
            return new int[] { 0, 0 };
        }// end determinAction

        private static int[] DeterminAction1(int[] data, int index)
        {
            // declare result array
            int[] result = new int[2];


            // do algorithms
            for (int i = 0; i < deques.Count; i++)
            {
                // find which deque to use
                int currentHigh = 0;
                int currentLow = 0;
                int toJudge = data[index];

                int dequeToUseIndex = -1;
                int minDiff = 50000000;
                // find deque value is closest to
                for (int j = 0; j < deques.Count; j++)
                {
                    if (toJudge > deques[j].Last.Value)
                    {
                        if (Math.Abs(toJudge - deques[j].Last.Value) < Math.Abs(minDiff))
                        {
                            minDiff = toJudge - deques[j].Last.Value;
                            dequeToUseIndex = j;
                            currentHigh = deques[j].Last.Value;
                            currentLow = deques[j].First.Value;
                        }

                    }

                    if (toJudge < deques[j].First.Value)
                    {
                        if (Math.Abs(deques[j].First.Value - toJudge) < Math.Abs(minDiff))
                        {
                            minDiff = toJudge - deques[j].First.Value;
                            dequeToUseIndex = j;
                            currentHigh = deques[j].Last.Value;
                            currentLow = deques[j].First.Value;
                        }

                    }
                } // end forloop finding correct deque




                // process current value on selected deque
                if (toJudge > currentHigh)
                {
                    bool inbetweenFound = false;
                    // check for a value inbetween, if found set found bool to true
                    for (int j = index + 1; j < data.Count(); j++)
                    {
                        if (data[j] > currentHigh && data[j] < toJudge)
                        {
                            inbetweenFound = true;
                        }
                    }
                    // If not inbetween found return result
                    if (!inbetweenFound)
                    {
                        result = new int[] { 1, dequeToUseIndex };
                        return result;
                    }
                }

                // check if value to process is lower than value at bottom of deque
                else if (toJudge < currentLow)
                {
                    bool inbetweenFound = false;
                    // check for a value inbetween, if found set found bool to true
                    for (int j = index + 1; j < data.Count(); j++)
                    {
                        if (data[j] < currentLow && data[j] > toJudge)
                        {
                            inbetweenFound = true;
                        }
                    }
                    // If not inbetween found return result
                    if (!inbetweenFound)
                    {
                        result = new int[] { 2, dequeToUseIndex };
                        return result;
                    }
                }


            }
            result = new int[] { 3, -1 };
            return result;
        }// end determinAction

        private static int[] DeterminAction(int[] data, int index)
        {
            // declare result array
            int[] result = new int[2];


            // do algorithms
            for (int i = 0; i < deques.Count; i++)
            {
                // check if to judge is higher or lower than current deque
                int currentHigh = deques[i].Last.Value;
                int currentLow = deques[i].First.Value;
                int toJudge = data[index];
                // check if value to process is higher than value at top of deque
                if (toJudge > currentHigh)
                {
                    bool inbetweenFound = false;
                    // check for a value inbetween, if found set found bool to true
                    for (int j = index + 1; j < data.Count(); j++)
                    {
                        if (data[j] > currentHigh && data[j] < toJudge)
                        {
                            inbetweenFound = true;
                        }
                    }
                    // If not inbetween found return result
                    if (!inbetweenFound)
                    {
                        result = new int[] { 1, i };
                        return result;
                    }
                }

                // check if value to process is lower than value at bottom of deque
                else if (toJudge < currentLow)
                {
                    bool inbetweenFound = false;
                    // check for a value inbetween, if found set found bool to true
                    for (int j = index + 1; j < data.Count(); j++)
                    {
                        if (data[j] < currentLow && data[j] > toJudge)
                        {
                            inbetweenFound = true;
                        }
                    }
                    // If not inbetween found return result
                    if (!inbetweenFound)
                    {
                        result = new int[] { 2, i };
                        return result;
                    }
                }
            }
            result = new int[] { 3, -1 };
            return result;
        }// end determinAction
        private static void PrintDeques()
        {
            string toPrint = "";
            foreach (var deque in deques)
            {
                foreach (var value in deque)
                {
                    toPrint += (value.ToString() + ", ");
                }
            }
            Console.WriteLine(toPrint);
        }
    }// end class
}// end namespace