using System;
using System.Collections.Generic;

namespace Level1Space
{
    public static class Level1
    {
        public static string MassVote(int N, int [] Votes)
        {
            const string case1 = "majority winner ";
            const string case2 = "minority winner ";
            const string case3 = "no winner";
            
            if (N == 1)
            {
                return case1 + "1";
            }
            
            double allVotes = Votes[0];
            List<int> maxIndices = new List<int>();
            //number of candidate (index + 1)
            maxIndices.Add(1);
            double leader = Votes[0];

            for (var i = 1; i < Votes.Length; i++)
            {
                allVotes += Votes[i];
                
                if (Votes[i] == leader)
                {
                    maxIndices.Add(i + 1);
                }
                
                if (Votes[i] > leader)
                {
                    maxIndices.Clear();
                    leader = Votes[i];
                    maxIndices.Add(i + 1);
                }
            }

            if (maxIndices.Count > 1)
            {
                return case3;
            }

            var leaderPercent = Math.Round(100 * (leader / allVotes), 3);

            if (leaderPercent > 50)
            {
                return case1 + maxIndices[0];
            }

            return case2 + maxIndices[0];
        }
    }
}