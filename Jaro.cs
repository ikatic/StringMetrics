/*
This code is provided "as is" and for educational or experimental purposes only. The author makes no warranties regarding the functionality, reliability, or safety of the code. It is not guaranteed to be production-ready, secure, or free from defects or bugs. Users should use this code at their own risk. The author disclaims any liability for damages resulting from using, modifying, or distributing this code. Before using in a production environment, thorough testing and validation are recommended.
*/

using System;

namespace ikatic.StringMetrics
{
    public class Jaro : Metric, IMetric
    {
        public override double Compare(string pattern, string target)
        {
            //base.Init will initilize the base class 
            //and it will also validate both strings
            //if not valid it will set the distance and score appropriatly
            if (!base.Init(pattern, target)) { Stop(); return Score; };

            //valid length pattern and target - compare bsm Jaro method:

            //correct issues with one chars vs the entire word cases, e.g. "m" vs "michael"
            if ((pattern.Length > 0 && target.Length > 0) && (pattern.Length == 1 || target.Length == 1))
            {
                if (pattern[0] == target[0])
                {
                    return 1.0d / (double)System.Math.Max(pattern.Length, target.Length);

                    //set the score as one character matching out of the longer word, e.g. "m" vs. "michael" = 1/7 = 0.143
                }
            }

            double S1 = (double)lLength;
            double S2 = (double)rLength;

            double maxTranspositionDistance = (System.Math.Max(S1, S2) / 2d) - 1d;
            double matches = 0d;
            double transpositions = 0d;

            for (int patternIndex = 0; patternIndex < S1; patternIndex++)
            {

                if (patternIndex < S2)
                {
                    if (pattern[patternIndex] == target[patternIndex])
                    {
                        matches++;
                    }
                    else
                    {
                        for (int transpositionIndex = (int)maxTranspositionDistance; transpositionIndex > 0; transpositionIndex--)
                        {
                            if ((patternIndex - transpositionIndex) >= 0)
                            {
                                if (pattern[patternIndex] == target[patternIndex - transpositionIndex])
                                {
                                    transpositions++;
                                }
                                else
                                {
                                    if ((patternIndex + transpositionIndex) < S2)
                                    {
                                        if (pattern[patternIndex] == target[patternIndex + transpositionIndex])
                                        {
                                            transpositions++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //int transpositionRatio = (transpositions > 0 && maxTranspositionDistance  > 0 ? (int)(transpositions / maxTranspositionDistance) : 0);

            int ratio = (transpositions > 0 && maxTranspositionDistance > 0 ? (int)(transpositions / maxTranspositionDistance) : 0);

            if (matches > 0)
            {
                Score = (1d / 3d) * ((matches / S1) + ((matches / S2) > 1d ? 1 : matches / S2) + ((matches - ratio) / matches));
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

            Stop();
            return Score;
        }
    }
}
