using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace RegexTester
{
    public class Program
    {
        static Constants.ExitCode ExitCode { get; set; }

        public static int Main(string[] args)
        {
            ExitCode = Constants.ExitCode.Match;
            try
            {
                // Parse the arguments
                var parsedArgs = ParseArguments(args);

                // Check the argument validity
                if (CheckArgumentValidity(parsedArgs))
                    CheckPatternMatching(parsedArgs);

                // Display the message into console
                Console.WriteLine("Result: " + Constants.InfoMessage[ExitCode]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                ExitCode = Constants.ExitCode.GeneralException;
            }

            //Environment.Exit((int)ExitCode); // comment this because unit test cannot get exit code
            return (int)ExitCode;
        }

        private static bool CheckArgumentValidity(Dictionary<string, string> parsedArgs)
        {
            // check the argument availability
            if (!parsedArgs.ContainsKey(Constants.RegexKeyword) && !parsedArgs.ContainsKey(Constants.WildcardKeyword))
                ExitCode = Constants.ExitCode.RegexKeywordNotFound;
            else if (!parsedArgs.ContainsKey(Constants.PatternKeyword))
                ExitCode = Constants.ExitCode.PatternKeywordNotFound;
            else if ((parsedArgs.ContainsKey(Constants.RegexKeyword) && String.IsNullOrEmpty(parsedArgs[Constants.RegexKeyword])) &&
                (parsedArgs.ContainsKey(Constants.WildcardKeyword) && String.IsNullOrEmpty(parsedArgs[Constants.WildcardKeyword])))
                ExitCode = Constants.ExitCode.RegexValueEmpty;
            else if (String.IsNullOrEmpty(parsedArgs[Constants.PatternKeyword]))
                ExitCode = Constants.ExitCode.PatternValueEmpty;

            if (ExitCode == Constants.ExitCode.RegexKeywordNotFound ||
                ExitCode == Constants.ExitCode.PatternKeywordNotFound ||
                ExitCode == Constants.ExitCode.RegexValueEmpty ||
                ExitCode == Constants.ExitCode.PatternValueEmpty)
                return false;
            else
                return true;
        }

        private static void CheckPatternMatching(Dictionary<string, string> parsedArgs)
        {
            var regex = parsedArgs.ContainsKey(Constants.RegexKeyword) ? parsedArgs[Constants.RegexKeyword] : String.Empty;
            var wildcard = parsedArgs.ContainsKey(Constants.WildcardKeyword) ? parsedArgs[Constants.WildcardKeyword] : String.Empty;
            var pattern = parsedArgs[Constants.PatternKeyword];
            var ignoreCase = parsedArgs.ContainsKey(Constants.IgnoreCaseKeyword) ? 
                (parsedArgs[Constants.IgnoreCaseKeyword].Equals("yes") ? true : false )
                : false;

            // Check if the tester mode is for REGEX or WILDCARD
            if (!String.IsNullOrEmpty(regex))
            {
                // check if regex is valid or not
                if (!IsValidRegex(regex))
                    ExitCode = Constants.ExitCode.InvalidRegex;
                else
                {
                    // check if the pattern match with regex
                    if (IsRegexMatch(regex, pattern, ignoreCase))
                        ExitCode = Constants.ExitCode.Match;
                    else
                        ExitCode = Constants.ExitCode.NotMatch;
                }
            }
            else // Wildcard
            {
                if (IsWildcardMatch(wildcard, pattern, !ignoreCase))
                    ExitCode = Constants.ExitCode.Match;
                else
                    ExitCode = Constants.ExitCode.NotMatch;
            }
        }

        private static bool IsValidRegex(string regex)
        {
            if (String.IsNullOrEmpty(regex))
                return false;

            try
            {
                Regex.Match("", regex);
            }
            catch (ArgumentException)
            {
                return false;
            }
            return true;
        }

        private static bool IsRegexMatch(string regex, string pattern, bool ignoreCase)
        {
            if (ignoreCase)
                return Regex.IsMatch(pattern, regex, RegexOptions.IgnoreCase);
            else
                return Regex.IsMatch(pattern, regex);
        }

        private static bool IsWildcardMatch(string wildcard, string text, bool caseSensitive = false)
        {
            string patternResult = "^" + Regex.Escape(wildcard)
                .Replace("\\*", ".*")
                .Replace("\\?", ".") + "$";

            return new Regex(patternResult, caseSensitive ? RegexOptions.None : RegexOptions.IgnoreCase).IsMatch(text);
        }

        private static Dictionary<string, string> ParseArguments(string[] args)
        {
            Dictionary<string, string> parsedArgs = new Dictionary<string,string>();

            // parse with template [key]=[value] arguments
            List<string> allKeys = new List<string>() { Constants.RegexKeyword, Constants.WildcardKeyword, Constants.PatternKeyword, Constants.IgnoreCaseKeyword };
            foreach (string arg in args)
            {
                Match m = Regex.Match(arg, @"
                    ^(?<key>/[A-Za-z0-9 _]+)  # The key can be any alphanumeric character normally allowed for a variable name
                    =                        # The separator entree the key and the value
                    (?<value>.*)$            # The value can be absolutely anything",
                    RegexOptions.IgnorePatternWhitespace);
                if (m.Success)
                {
                    if (allKeys.Contains(m.Groups["key"].Value.ToUpper()) && 
                        !parsedArgs.ContainsKey(m.Groups["key"].Value.ToUpper()))
                        parsedArgs.Add(m.Groups["key"].Value.ToUpper(), m.Groups["value"].Value);
                }
            }

            return parsedArgs;
        }

    }
}
