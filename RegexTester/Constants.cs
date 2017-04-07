using System;
using System.Collections.Generic;
using System.Text;

namespace RegexTester
{
    public static class Constants
    {
        public const string RegexKeyword = "/REGEX";
        public const string PatternKeyword = "/PATTERN";
        public const string WildcardKeyword = "/WILDCARD";
        public const string IgnoreCaseKeyword = "/IGNORECASE";

        public enum InputParameters
        {
            Regex,
            Pattern
        }

        public enum ExitCode
        {
            Match                   = 0,
            NotMatch                = 1,
            InvalidRegex            = -1,
            RegexKeywordNotFound    = -2,
            PatternKeywordNotFound  = -3,
            RegexValueEmpty         = -4,
            PatternValueEmpty       = -5,
            GeneralException        = -999
        }

        public static Dictionary<ExitCode, string> InfoMessage
        { 
            get
            {
                return new Dictionary<ExitCode, string>()
                {
                    { ExitCode.Match, "The pattern matches with regex/wildcard." },
                    { ExitCode.NotMatch, "The pattern doesn't match with regex/wildcard." },
                    { ExitCode.InvalidRegex, "Invalid regex." },
                    { ExitCode.RegexKeywordNotFound, "/regex or /wildcard argument is not found." },
                    { ExitCode.PatternKeywordNotFound, "/pattern argument is not found" },
                    { ExitCode.RegexValueEmpty, "Regex or Wildcard value is empty." },
                    { ExitCode.PatternValueEmpty, "Pattern value is empty." },
                    { ExitCode.GeneralException, "General exception." },
                };
            }
        }
    }
}
