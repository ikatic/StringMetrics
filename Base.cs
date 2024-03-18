using System;

namespace ikatic.StringMetrics
{


    public enum Algorithms
    {
        Unknown = 0,
        Hamming = 10,
        Dice = 20,
        Jaro = 30,
        JaroWinkler = 40,
        Soundex = 50,
        Levenshtein = 60,
        DamerauLevenshtein = 70
    }


    public interface IAlgorithm
    {
        double Compare(string left, string right);
        string Left { get; }
        string Right { get; }
        double Score { get; }
        double Distance { get; }
        IDiagnostics Diagnostics { get; }
    }

    public class IDiagnostics
    {
        /// <summary>
        /// Elapsed milliseconds
        /// </summary>
        public double Milliseconds { get; set; }
    }


    public abstract class Algorithm : IAlgorithm
    {
        private System.Diagnostics.Stopwatch _sw = null;
        protected int lLength;
        protected int rLength;


        protected bool Init(string left, string right)
        {
            //start timing:
            Start();

            if (left == right)
            {
                Distance = MinScore;
                Score = MaxScore;
                return false;
            }

            if (left == null && right == null)
            {
                Distance = MinScore;
                Score = MaxScore;
                return false;
            }

            if (left == null)
            {
                Distance = right.Length;
                Score = MinScore;
                return false;
            }

            if (right == null)
            {
                Distance = left.Length;
                Score = MinScore;
                return false;
            }

            if (left == string.Empty && right == string.Empty)
            {
                Distance = MinScore;
                Score = MinScore;
                return false;
            }

            if (left == string.Empty)
            {
                Distance = right.Length;
                Score = MinScore;
                return false;
            }

            if (right == string.Empty)
            {
                Distance = left.Length;
                Score = MinScore;
                return false;
            }

            _left = left;
            lLength = left.Length;

            _right = right;
            rLength = right.Length;

            if (left.Equals(right))
            {
                Distance = MinScore;
                Score = MaxScore; //exact match - no distance and perfect score:
                return false;
            }

            if (lLength == 0)
            {
                //Missing pattern ; set distance to target's length and score to min score:
                Distance = (double)rLength;
                Score = MinScore;
                return false;
            }

            if (rLength == 0)
            {
                //Missing target ; set distance to pattern's length and score to min score:
                Distance = (double)lLength;
                Score = MinScore;
                return false;
            }


            //both strings are valid, set distance and score to min 
            //and return true so the strings get compared

            _distance = MinScore;
            _score = MinScore;

            return true;

        }

        /// <summary>
        /// (Void) Stops the timing and sets the private variable for ElapsedMilliseconds property;
        /// </summary>
        protected void Stop()
        {
            _sw.Stop();
            _diagnostics.Milliseconds = _sw.Elapsed.TotalMilliseconds;
        }

        /// <summary>
        /// (Void) Starts or resets the stopwatch and ElapsedMilliseconds property
        /// </summary>
        protected void Start()
        {
            _diagnostics.Milliseconds = double.MinValue;
            _sw = System.Diagnostics.Stopwatch.StartNew();
        }


        #region "IAlgorithm implementation:"

        private string _left = null;
        public string Left
        {
            get { return _left; }
            internal set { _left = value; }
        }

        private string _right = null;
        public string Right
        {
            get { return _right; }
            protected set { _left = value; }
        }

        private double _distance = double.MinValue;
        public double Distance
        {
            get { return _distance; }
            internal set { _distance = value; }
        }

        private double _score = double.MinValue;
        public double Score
        {
            get { return _score; }
            internal set { _score = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private IDiagnostics _diagnostics = new IDiagnostics();
        public IDiagnostics Diagnostics
        {
            get { return _diagnostics; }
            internal set { _diagnostics = value; }
        }

        /// <summary>
        /// returns basic compare of two strings and returns 0d or 1d for no match and match respectivley.
        /// derived classes must implement this method with their specific compare logic:
        ///</summary>
        public virtual double Compare(string pattern, string target)
        {
            return (pattern == target ? 1d : 0d);
        }

        #endregion

        #region "Min and Max scores:"
        protected double MaxScore
        {
            get { return _maxScore; }
            set { _maxScore = value; }
        }
        private double _maxScore = 1.0d;

        protected double MinScore
        {
            get { return _minScore; }
            set { _minScore = value; }
        }
        private double _minScore = 0.0d;
        #endregion

        #region "Helper and common functions/methods"

        protected List<string> ToBigrams(string text, out List<string> bigrams)
        {
            //List<string> bigrams = new List<string>();
            bigrams = new List<string>();
            if (text != null && text.Length > 0)
            {
                for (int index = 0; index <= text.Length; index++)
                {
                    if (index == 0)
                        bigrams.Add(String.Format("${0}", text[0]));
                    else if (index == text.Length)
                        bigrams.Add(String.Format("{0}^", text[index - 1]));
                    else
                        bigrams.Add(String.Format("{0}{1}", text[index - 1], text[index]));
                }
            }

            return bigrams;
        }

        protected List<string> ToBigrams(string text)
        {
            List<string> bigrams = new List<string>();

            if (text != null && text.Length > 0)
            {
                for (int index = 0; index <= text.Length; index++)
                {
                    if (index == 0)
                        bigrams.Add(String.Format("${0}", text[0]));
                    else if (index == text.Length)
                        bigrams.Add(String.Format("{0}^", text[index - 1]));
                    else
                        bigrams.Add(String.Format("{0}{1}", text[index - 1], text[index]));
                }
            }

            return bigrams;
        }

        protected List<string> ToTrigrams(string text)
        {
            List<string> trigrams = new List<string>();

            if (text.Length == 0)
            {
            }
            else if (text.Length == 1)
                trigrams.Add(String.Format("${0}", text[0]));
            else if (text.Length == 2)
                trigrams.Add(String.Format("${0}{1}", text[0], text[1]));
            else
            {
                for (int index = 1; index < text.Length - 1; index += 2)
                {
                    if (index == 1)
                    {
                        trigrams.Add(String.Format("${0}{1}", text[index - 1], text[index]));
                    }
                    else if (index == text.Length - 2)
                    {
                        trigrams.Add(String.Format("{0}{1}^", text[index], text[index + 1]));
                    }
                    else
                    {
                        trigrams.Add(String.Format("{0}{1}{2}", text[index], text[index + 1], text[index + 2]));
                    }
                }
            }

            return trigrams;
        }

        protected int MatchGrams(List<string> patterns, List<string> targets)
        {
            //return patterns.Intersect(targets).Count();

            int matches = 0;


            for (int p = 0; p < patterns.Count; p++)
            {
                for (int t = 0; t < targets.Count; t++)
                {
                    if (patterns[p].Equals(targets[t]))
                    {
                        matches++;
                        break;
                    }
                }
            }

            return matches;

            //// Parallelize the outer loop to partion each comparison to targets:
            //Parallel.For(0, patterns.Count, p =>
            //{
            //    for (int t = 0; t < targets.Count; t++)
            //    {
            //        if (patterns[p].Equals(targets[t]))
            //        {
            //            matches++;
            //            break;
            //        }
            //    }
            //}); // Parallel.For

            //return matches;
        }


        protected int GetCommonPrefix(string pattern, string target, int max)
        {
            if (pattern == null || pattern.Length == 0)
                return 0;
            if (target == null || target.Length == 0)
                return 0;

            int returnValue = 0;

            for (int index = 0; index < max; index++)
            {
                if (index < pattern.Length && index < target.Length)
                {
                    if (pattern[index] == target[index])
                    {
                        returnValue++;
                    }
                    else
                    {
                        break;
                    }
                }
            }


            return returnValue;

        }

        public static bool Equal(string a, string b)
        {
            if (a.Length != b.Length)
                return false;

            for (int i = 0; i < a.Length; i++)
            {
                if (!a[i].Equals(b[i]))
                    return false;
            }
            return true;
        }

        protected void Swap(ref string pattern, ref string target)
        {
            string temp = pattern;
            pattern = target;
            target = temp;

            _left = pattern;
            _right = target;

            lLength = pattern.Length;
            rLength = target.Length;
        }
        #endregion

    }
}
