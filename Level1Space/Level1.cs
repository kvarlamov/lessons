using System;
using System.Collections.Generic;

namespace Level1Space
{
    public static class Level1
    {
        public static bool MisterRobot(int N, int [] data)
        {
            int expected = 1;
            bool right = false;

            for (int i = 0; i < N; i++)
            {
                if (data[i] == expected)
                {
                    expected++;
                    continue;
                }

                var index = Array.IndexOf(data, expected);
                int leftIndex = i;
                int rightIndex = index;
                if (index - 3 >= i || index == i + 2)
                {
                    leftIndex = index - 2;
                    right = true;
                }

                while (right)
                {
                    int tmp = data[leftIndex + 1];
                    data[leftIndex + 1] = data[leftIndex];
                    data[leftIndex] = data[rightIndex];
                    data[rightIndex] = tmp;
                    
                    rightIndex = leftIndex;
                    if (rightIndex - 3 >= i || i+2 == rightIndex)
                    {
                        leftIndex = rightIndex - 2;
                    }
                    else
                    {
                        leftIndex = i;
                        right = false;
                        break;
                    }
                }

                if (leftIndex + 2 > N - 1 || (index == N - 1 && data[index] == N))
                    break;

                if (expected != data[i])
                {
                    int tmp2 = data[leftIndex + 2];
                    data[leftIndex + 2] = data[leftIndex];
                    data[leftIndex] = data[rightIndex];
                    data[rightIndex] = tmp2;
                }
                
                expected++;
            }

            return data[N-1] == N;
        }
    }
}