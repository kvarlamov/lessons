using System;
using System.Collections.Generic;

namespace Level1Space
{
    public static class Level1
    {
        public static int Unmanned(int L, int N, int [][] track)
        {
            int travelTime = 0;
            int carCoordinates = 0;

            for (int i = 0; i < N && L > 0; i++)
            {
                int signalInterval = track[i][1] + track[i][2];
                //go to traffic light
                while (carCoordinates < track[i][0] && L > 0)
                {
                    travelTime++;
                    carCoordinates++;
                    L--;
                }
                
                if (L <= 0)
                    break;
                
                int currentTimePointOfCar = travelTime - signalInterval * (travelTime / signalInterval);
                
                while(currentTimePointOfCar < track[i][1])
                {
                    //red - wait for green
                    travelTime++;
                    currentTimePointOfCar = travelTime - signalInterval * (travelTime / signalInterval);
                }

                L--;
                carCoordinates++;
                travelTime++;
            }

            while (L > 0)
            {
                travelTime++;
                L--;
            }
            
            return travelTime;
        }
    }
}