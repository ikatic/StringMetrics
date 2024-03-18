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
        public static IAlgorithm Create(Algorithms algorithm)
        {
            switch (algorithm)
            {
                case Algorithms.DamerauLevenshtein:
                    return new DamerauLevenshtein() as IAlgorithm;

                case Algorithms.Dice:
                    return new Dice() as IAlgorithm;

                case Algorithms.Hamming:
                    return new Hamming() as IAlgorithm;

                case Algorithms.Jaro:
                    return new Jaro() as IAlgorithm;

                case Algorithms.JaroWinkler:
                    return new JaroWinkler() as IAlgorithm;

                case Algorithms.Levenshtein:
                    return new Levenshtein() as IAlgorithm;

                case Algorithms.Soundex:
                    return new Soundex() as IAlgorithm;

                default:
                    throw new Exception("0x00001 - Unrecognized string metric algorithm.");

            }
        }
    }
}
