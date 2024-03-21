/*
This code is provided "as is" and for educational or experimental purposes only. The author makes no warranties regarding the functionality, reliability, or safety of the code. It is not guaranteed to be production-ready, secure, or free from defects or bugs. Users should use this code at their own risk. The author disclaims any liability for damages resulting from using, modifying, or distributing this code. Before using in a production environment, thorough testing and validation are recommended.
*/

using System;

namespace ikatic.StringMetrics
{
    public class Dice : Metric, IMetric
    {
        public override double Compare(string pattern, string target)
        {
            //base.Init will initilize the base class 
            //and it will also validate both strings
            //if not valid it will set the distance and score appropriatly
            if (!base.Init(pattern, target)) { Stop(); return Score; };

            //valid length pattern and target - compare bsm Dice method:

            List<string> patternBigrams = ToBigrams(pattern);
            List<string> targetBigrams = ToBigrams(target);

            int matches = MatchGrams(patternBigrams, targetBigrams);

            if (matches > 0)
            {
                Score = (2.0d * matches) / (double)(patternBigrams.Count + targetBigrams.Count);

                //Don't let the score drop below the min score:
                if (Score < base.MinScore)
                    Score = base.MinScore;

                Distance = base.MaxScore - Score;
            }
            else
            {
                Score = base.MinScore;
                Distance = base.MaxScore;
            }

            Stop();//stop the timer which will set the ElapsedMilliseconds property;
            return Score;
        }
    }
}
