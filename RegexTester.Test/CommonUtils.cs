using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegexTester.Test
{
    public class CommonUtils
    {
        public static IEnumerable<string> BuildCommandLine(string syntax, string pattern, bool isWildcard = false, bool ignoreCase = false)
        {
            List<string> args = new List<string>();
            if (isWildcard)
                args.Add(String.Format("/wildcard={0}", syntax));
            else
                args.Add(String.Format("/regex={0}", syntax));
            args.Add(String.Format("/pattern={0}", pattern));
            if (ignoreCase)
                args.Add(String.Format("/ignorecase=yes", syntax));
            return args;
        }
    }
}
