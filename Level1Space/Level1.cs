using System;
using System.Collections.Generic;

namespace Level1Space
{
    public static class Level1
    {
        public static int odometer(int[] oksana)
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
    }
}