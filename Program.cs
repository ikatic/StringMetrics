/*
This code is provided "as is" and for educational or experimental purposes only. The author makes no warranties regarding the functionality, reliability, or safety of the code. It is not guaranteed to be production-ready, secure, or free from defects or bugs. Users should use this code at their own risk. The author disclaims any liability for damages resulting from using, modifying, or distributing this code. Before using in a production environment, thorough testing and validation are recommended.
*/

namespace Console;
using ikatic.StringMetrics;

class Program
{
    static void Main(string[] args)
    {
        System.Console.Clear();

        // Initialize the dictionary
        Dictionary<string, string> pairs = new Dictionary<string, string>();

        // Add pairs to be matched/compared to the dictionary

        //Compare individual words for similarity:
        pairs.Add("House", "Hause");
        pairs.Add("Intelligent", "Intelligence");
        pairs.Add("Train", "Cough");

        //Compare sentences for similarity:
        pairs.Add("The cat napped on the sunny mat", "The dog slept on the cozy bed");
        pairs.Add("The house is blue.", "Is the house red?");

        // Iterate over the dictionary and compare using each of the string metrics:
        foreach (KeyValuePair<string, string> pair in pairs)
        {
            System.Console.WriteLine("\nCompare using Hamming string metric......");
            ikatic.StringMetrics.IAlgorithm hamming = ikatic.StringMetrics.Factory.Create(ikatic.StringMetrics.Algorithms.Hamming) as ikatic.StringMetrics.IAlgorithm;
            Print(hamming, pair.Key, pair.Value);

            System.Console.WriteLine("\nCompare using Dice string metric......");
            ikatic.StringMetrics.IAlgorithm dice = ikatic.StringMetrics.Factory.Create(ikatic.StringMetrics.Algorithms.Dice) as ikatic.StringMetrics.IAlgorithm;
            Print(dice, pair.Key, pair.Value);

            System.Console.WriteLine("\nCompare using Jaro string metric......");
            ikatic.StringMetrics.IAlgorithm jaro = ikatic.StringMetrics.Factory.Create(ikatic.StringMetrics.Algorithms.Jaro) as ikatic.StringMetrics.IAlgorithm;
            Print(jaro, pair.Key, pair.Value);

            System.Console.WriteLine("\nCompare using Jaro-Winkler string metric......");
            ikatic.StringMetrics.IAlgorithm jw = ikatic.StringMetrics.Factory.Create(ikatic.StringMetrics.Algorithms.JaroWinkler) as ikatic.StringMetrics.IAlgorithm;
            Print(jw, pair.Key, pair.Value);

            System.Console.WriteLine("\nCompare using Soundex string metric......");
            ikatic.StringMetrics.IAlgorithm sdx = ikatic.StringMetrics.Factory.Create(ikatic.StringMetrics.Algorithms.Soundex) as ikatic.StringMetrics.IAlgorithm;
            Print(sdx, pair.Key, pair.Value);

            System.Console.WriteLine("\nCompare using Levenshtein string metric......");
            ikatic.StringMetrics.IAlgorithm lev = ikatic.StringMetrics.Factory.Create(ikatic.StringMetrics.Algorithms.Levenshtein) as ikatic.StringMetrics.IAlgorithm;
            Print(lev, pair.Key, pair.Value);

            System.Console.WriteLine("\nCompare using Damerau-Levenshtein string metric......");
            ikatic.StringMetrics.IAlgorithm dlev = ikatic.StringMetrics.Factory.Create(ikatic.StringMetrics.Algorithms.DamerauLevenshtein) as ikatic.StringMetrics.IAlgorithm;
            Print(dlev, pair.Key, pair.Value);
        }
    }

    
    static void Print(IAlgorithm sim, string p, string t)
    {
        //invokes compare method and prints results:
        sim.Compare(p, t);
        System.Console.WriteLine("'{0}' and '{1}' are {2} similar with a distance {3}; compare took {4} milliseconds.", p, t, sim.Score, sim.Distance, sim.Diagnostics.Milliseconds);
    }

}
