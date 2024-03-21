/*
This code is provided "as is" and for educational or experimental purposes only. The author makes no warranties regarding the functionality, reliability, or safety of the code. It is not guaranteed to be production-ready, secure, or free from defects or bugs. Users should use this code at their own risk. The author disclaims any liability for damages resulting from using, modifying, or distributing this code. Before using in a production environment, thorough testing and validation are recommended.
*/

using System;

namespace ikatic.StringMetrics
{
    /// <summary>
    /// Factory class to use to create instances of specific string metric types.
    /// </summary>
    public static class Factory
    {
        /// <summary>
        /// Returns a new instance of a string metric specifed, e.g. Jaro-Winkler;
        /// </summary>
        /// <returns></returns>
        public static IMetric Create(Metrics m)
        {
            switch (m)
            {
                case Metrics.DamerauLevenshtein:
                    return new DamerauLevenshtein() as IMetric;

                case Metrics.Dice:
                    return new Dice() as IMetric;

                case Metrics.Hamming:
                    return new Hamming() as IMetric;

                case Metrics.Jaro:
                    return new Jaro() as IMetric;

                case Metrics.JaroWinkler:
                    return new JaroWinkler() as IMetric;

                case Metrics.Levenshtein:
                    return new Levenshtein() as IMetric;

                case Metrics.Soundex:
                    return new Soundex() as IMetric;

                default:
                    throw new Exception("0x00001 - Unrecognized string metric.");

            }
        }
    }
}
