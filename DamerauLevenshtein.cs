/*
This code is provided "as is" and for educational or experimental purposes only. The author makes no warranties regarding the functionality, reliability, or safety of the code. It is not guaranteed to be production-ready, secure, or free from defects or bugs. Users should use this code at their own risk. The author disclaims any liability for damages resulting from using, modifying, or distributing this code. Before using in a production environment, thorough testing and validation are recommended.
*/

using System;

namespace ikatic.StringMetrics
{
    public class DamerauLevenshtein : Metric, IMetric
    {
        public override double Compare(string pattern, string target)
        {
            //base.Init will initilize the base class 
            //and it will also validate both strings
            //if not valid it will set the distance and score appropriatly
            if (!base.Init(pattern, target)) { Stop(); return Score; };

            //valid length pattern and target - compare bsm DamerauLevenshtein method:

            int[,] d = new int[lLength + 1, rLength + 1];

            int cost, del, ins, sub;

            for (int i = 0; i <= d.GetUpperBound(0); i++)
                d[i, 0] = i;

            for (int i = 0; i <= d.GetUpperBound(1); i++)
                d[0, i] = i;


            for (int i = 1; i <= d.GetUpperBound(0); i++)
            {
                for (int j = 1; j <= d.GetUpperBound(1); j++)
                {
                    cost = (pattern[i - 1] == target[j - 1] ? 0 : 1);

                    del = d[i - 1, j] + 1;
                    ins = d[i, j - 1] + 1;
                    sub = d[i - 1, j - 1] + cost;

                    d[i, j] = System.Math.Min(del, System.Math.Min(ins, sub));

                    //Transposition:
                    if (i > 1 && j > 1 && pattern[i - 1] == target[j - 2] && pattern[i - 2] == target[j - 1])
                        d[i, j] = System.Math.Min(d[i, j], d[i - 2, j - 2] + cost);
                }
            }

            Distance = (double)d[lLength, rLength];
            Score = ((double)System.Math.Max(lLength, rLength) - Distance) / (double)System.Math.Max(lLength, rLength);

            //Don't let the score drop below the min score:
            if (Score < base.MinScore)
                Score = base.MinScore;

            Stop();//stop the timer which will set the ElapsedMilliseconds property;
            return Score;
        }

    }
}
