/*
This code is provided "as is" and for educational or experimental purposes only. The author makes no warranties regarding the functionality, reliability, or safety of the code. It is not guaranteed to be production-ready, secure, or free from defects or bugs. Users should use this code at their own risk. The author disclaims any liability for damages resulting from using, modifying, or distributing this code. Before using in a production environment, thorough testing and validation are recommended.
*/

using System;

namespace ikatic.StringMetrics
{
    public class Hamming : Algorithm, IAlgorithm
    {

        public override double Compare(string pattern, string target)
        {
            //base.Init will initilize the base class 
            //and it will also validate both strings
            //if not valid it will set the distance and score appropriatly
            if (!base.Init(pattern, target)) { Stop(); return Score; };

            //valid length pattern and target - compare bsm Hamming method:

            //this is a trick to speed it up.  since hamming is position sensitive
            //and originaly was designed to compare strings of the same length,
            //the starting distance will be the difference in length of two strings.
            //then the distance can go up for each character difference of the shortest
            //string compared to the other

            //this is 15 times faster than the other way:
            int pLen = pattern.Length;
            int tLen = target.Length;

            Distance = Math.Abs(pLen - tLen); //use the difference between the lengths of the two strings as the starting point for distance, then add to it for every character that does not match.
            int min = Math.Min(pLen, tLen);
            int max = Math.Max(pLen, tLen);

            for (int index = 0; index < min; index++)
            {
                if (pattern[index] != target[index])
                    Distance++;
            }

            Score = ((double)max - Distance) / (double)max;

            //System.Threading.Thread.Sleep(100);

            //Don't let the score drop below the min score:
            if (Score < base.MinScore)
                Score = base.MinScore;

            Stop();
            return Score;
        }
    }
}
