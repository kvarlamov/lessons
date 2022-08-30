using System;
using System.Collections.Generic;

namespace Level1Space
{
    public class Level1Solved
    {
        public static int squirrel(int N)
        {
            long result = 1;

            for (int i = 2; i <= N; i++)
            {
                result = result * i;
            }

            while (result >= 10)
            {
                result = result / 10;
            }
            
            return (int)result ;
        }
        
        public static int odometer(int [] oksana)
        {
            int s = 0;
            int previousTime = 0;

            for (int i = 0; i < oksana.Length-1; i+=2)
            {
                int speed = oksana[i];
                int time = oksana[i + 1] - previousTime;
                previousTime = oksana[i + 1];

                s += time * speed;
            }

            return s;
        }
        
        public static int ConquestCampaign(int N, int M, int L, int [] battalion)
        {
            int day=0;
            int counter = 0;
            int allFields = N * M;
            bool[,] field = new bool[N, M];
            
            List<int> nextStep = new List<int>(battalion);
            for (int i = 0; i < nextStep.Count; i++)
            {
                nextStep[i]--;
            }
            List<int> toInvade;
            char[] directions = new[] {'n','s','w','e'};

            while (allFields > counter)
            {
                toInvade = nextStep;
                nextStep = new List<int>();
	
                for(int i = 0; i < toInvade.Count - 1; i+=2)
                {
                    foreach(var d in directions) 
                    {
                        int row = toInvade[i];
                        int column = toInvade[i+1];

                        if (day > 0)
                        {
                            switch (d)
                            {
                                case 'n':
                                    column += 1;
                                    break;
                                case 's':
                                    column -= 1;
                                    break;
                                case 'w':
                                    row -= 1;
                                    break;
                                case 'e':
                                    row += 1;
                                    break;
                            }
                        }

                        if (row > N-1 || column > M-1 || row < 0 || column < 0)
                            continue;
		
                        if (!field[row,column])
                        {
                            field[row,column] = true;
                            nextStep.Add(row);
                            nextStep.Add(column);
                            counter++;
                        }
                        
                        if (day == 0)
                            break;
                    }
                }
	
                day++;
                if (allFields == counter)
                    break;
            }

            return day;
        }
    }
}