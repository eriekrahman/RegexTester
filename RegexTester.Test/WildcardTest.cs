using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RegexTester.Test
{
    [TestFixture]
    public class WildcardTest
    {
        // * : 0 or more of any character
        [TestCase("Hello*.txt", "Hello World.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase("Hello*.txt", "Hello World, welcome to the jungle.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase("Hello*.txt", "Hello World. 0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzαβγδεζηθικλμνξοπρσςτυφχψωô是ணはయىድਜด-{}“”_,;?!%@#&*()[]<>$₤¥฿₱€©✫┐╠╈╬╣╳.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase("Hello*.txt", "xHello World.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase("Hello*.txt", "Hello World.txtx", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase("Hello*.txt", "HeLLO World.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase("Hello*.txt", "HeLLO World.txt", true, RegexTester.Constants.ExitCode.Match)]

        // ? : match 1 of any character
        [TestCase("?????Hello?????.txt", "Hell Hello Hell.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase("?????Hello?????.txt", "1234 Hello 5678.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase("?????Hello?????.txt", "αβγδ Hello 是ணはయ.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase("?????Hello?????.txt", "@#&* Hello $₤¥฿.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase("?????Hello?????.txt", "©✫┐\t Hello ┐╠╈╬.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase("?????Hello?????.txt", "Hell  Hello Hell.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase("?????Hello?????.txt", "Hell Hello Hello.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase("?????Hello?????.txt", "Hell Hello Hellotxt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase("?????Hello?????.txt", "Hell HEllo Hell.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase("?????Hello?????.txt", "Hell HEllo Hell.txt", true, RegexTester.Constants.ExitCode.Match)]

        public void WildcardTest_Syntax(string syntax, string pattern, bool ignoreCase, RegexTester.Constants.ExitCode expectedExitCode)
        {
            var args = CommonUtils.BuildCommandLine(syntax, pattern, isWildcard: true, ignoreCase: ignoreCase).ToArray();
            Assert.IsTrue(RegexTester.Program.Main(args) == (int)expectedExitCode);
        }
    }
}
