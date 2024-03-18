using System;
using System.Text;


namespace ikatic.StringMetrics
{
    public class Soundex : Algorithm, IAlgorithm
    {
        /*
            Retain the first letter of the name
            Drop all vowels, and h, y in all but the first letter
            Replace b,f,p,v with the number 1
            Replace c,g,j,k,q,s,x,z with the number 2
            Replace d and t with the number 3
            Replace l with 4, m and n with 5 and r with 6
            Finally convert to a 4 character code like {letter}{number}{number}{number} by truncation
         */

        public string Encode(string word, int length)
        {
            if (String.IsNullOrEmpty(word))
                return string.Empty;


            word = Normilize(word);

            if (word.Length == 0)
                return string.Empty;


            StringBuilder soundex = new StringBuilder();
            for (int counter = 0; counter < word.Length; counter++)
            {
                if (counter == 0)
                {
                    soundex.Append(word[counter]);
                }
                else
                {
                    switch (word[counter])
                    {
                        //Drop all vowels, and h, y in all but the first letter
                        case 'A':
                        case 'O':
                        case 'E':
                        case 'U':
                        case 'I':
                        case 'Y':
                        case 'H':
                            //do nothing / remove:
                            break;

                        //b, f, p, v ==> 1
                        case 'B':
                        case 'F':
                        case 'P':
                        case 'V':
                            soundex.Append("1");
                            break;

                        //c,g,j,k,q,s,x,z ==> 2
                        case 'C':
                        case 'G':
                        case 'J':
                        case 'K':
                        case 'Q':
                        case 'S':
                        case 'X':
                        case 'Z':
                            soundex.Append("2");
                            break;

                        //Replace d and t ==> 3
                        case 'D':
                        case 'T':
                            soundex.Append("3");
                            break;

                        //Replace l ==> 4
                        case 'L':
                            soundex.Append("4");
                            break;
                        //Replace m and n ==> 5
                        case 'M':
                        case 'N':
                            soundex.Append("5");
                            break;
                        //Replace r ==> 6
                        case 'R':
                            soundex.Append("6");
                            break;

                    }
                }

                if (length > 0 && counter == length)
                    break;
            }

            return soundex.ToString();
        }
        private string Normilize(string text)
        {
            StringBuilder clean = new StringBuilder();

            text = text.Trim().ToUpper();

            for (int counter = 0; counter < text.Length; counter++)
            {
                #region "allow only letters and numbers"

                switch (text[counter])
                {
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                    case 'A':
                    case 'B':
                    case 'C':
                    case 'D':
                    case 'E':
                    case 'F':
                    case 'G':
                    case 'H':
                    case 'I':
                    case 'J':
                    case 'K':
                    case 'L':
                    case 'M':
                    case 'N':
                    case 'O':
                    case 'P':
                    case 'Q':
                    case 'R':
                    case 'S':
                    case 'T':
                    case 'U':
                    case 'V':
                    case 'W':
                    case 'X':
                    case 'Y':
                    case 'Z':
                        clean.Append(text[counter]);
                        break;
                }
                #endregion
            }

            return clean.ToString();
        }

        public override double Compare(string pattern, string target)
        {
            //base.Init will initilize the base class 
            //and it will also validate both strings
            //if not valid it will set the distance and score appropriatly
            if (!base.Init(pattern, target)) { Stop(); return Score; };

            //valid length pattern and target - compare Soundex method:

            //TO DO: Encode each word in the pattern and target and compare it that way
            //e.g. IGOR KATIC => I233 K122 => then compare that against encoded words from target.
            //e.g measure distance the same way as all other metrics - bsm matrix;

            //soundex uses 3 digit encoding:
            Score = (Encode(pattern, 3) == Encode(target, 3) ? base.MaxScore : base.MinScore);//i don't know if this should be 0.99 since it is not an exact match.
            Distance = base.MaxScore - Score;

            Stop();
            return Score;
        }
    }
}
