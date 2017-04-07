using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RegexTester.Test
{
    [TestFixture]
    public class RegexTest
    {
        // ^ : START OF A STRING
        [TestCase("^Hello.*.txt", "Hello World.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase("^Hello.*.txt", "Hi World.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase("^Hello.*.txt", "HELLO World.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase("^Hello.*.txt", "HELLO World.txt", true, RegexTester.Constants.ExitCode.Match)]

        // $ : END OF A STRING
        [TestCase("Hello.*.txt$", "Hello World.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase("Hello.*.txt$", "Hello World.txt2", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase("Hello.*.txt$", "Hello World.TXT", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase("Hello.*.txt$", "Hello World.TXT", true, RegexTester.Constants.ExitCode.Match)]

        // . : ANY CHARACTER
        [TestCase("ABC...def.txt", "ABC123def.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase("ABC...def.txt", "ABC1234def.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase("ABC...def.txt", "ABC12def.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase("ABC...def.txt", "ABC123DEF.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase("ABC...def.txt", "ABC123DEF.txt", true, RegexTester.Constants.ExitCode.Match)]

        // | : ALTERNATION
        [TestCase("ABC|def.txt", "ABC.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase("ABC|def.txt", "def.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase("ABC|def.txt", "abc.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase("ABC|def.txt", "DEF.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase("ABC|def.txt", "abc.txt", true, RegexTester.Constants.ExitCode.Match)]
        [TestCase("ABC|def.txt", "DEF.txt", true, RegexTester.Constants.ExitCode.Match)]

        // {...} : Explicit quantifier notation
        [TestCase("AB{2}C.txt", "ABBC.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase("AB{2}C.txt", "ABC.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase("AB{2}C.txt", "ABBBC.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase("AB{2}C.txt", "ABbC.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase("AB{2}C.txt", "ABbC.txt", true, RegexTester.Constants.ExitCode.Match)]

        // [...] : Explicit set of characters to match
        [TestCase("A[BCD]E.txt", "ABE.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase("A[BCD]E.txt", "ACE.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase("A[BCD]E.txt", "ADE.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase("A[BCD]E.txt", "ABCDE.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase("A[BCD]E.txt", "AbE.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase("A[BCD]E.txt", "ABe.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase("A[BCD]E.txt", "aDE.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase("A[BCD]E.txt", "AbE.txt", true, RegexTester.Constants.ExitCode.Match)]
        [TestCase("A[BCD]E.txt", "ABe.txt", true, RegexTester.Constants.ExitCode.Match)]
        [TestCase("A[BCD]E.txt", "aDE.txt", true, RegexTester.Constants.ExitCode.Match)]

        // (...) : Logical grouping of part of an expression
        [TestCase("(ABC|def)GHI.txt", "ABCGHI.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase("(ABC|def)GHI.txt", "defGHI.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase("(ABC|def)GHI.txt", "BCGHI.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase("(ABC|def)GHI.txt", "abcGHI.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase("(ABC|def)GHI.txt", "DEFGHI.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase("(ABC|def)GHI.txt", "ABCghi.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase("(ABC|def)GHI.txt", "defghi.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase("(ABC|def)GHI.txt", "abcGHI.txt", true, RegexTester.Constants.ExitCode.Match)]
        [TestCase("(ABC|def)GHI.txt", "DEFGHI.txt", true, RegexTester.Constants.ExitCode.Match)]
        [TestCase("(ABC|def)GHI.txt", "ABCghi.txt", true, RegexTester.Constants.ExitCode.Match)]
        [TestCase("(ABC|def)GHI.txt", "defghi.txt", true, RegexTester.Constants.ExitCode.Match)]

        // * : 0 or more of previous expression
        [TestCase("ABC.*XYZ.txt", "ABCXYZ.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase("ABC.*XYZ.txt", "ABCDXYZ.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase("ABC.*XYZ.txt", "ABCDEXYZ.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase("ABC.*XYZ.txt", "ABCDEFXYZ.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase("ABC.*XYZ.txt", "ABcxYZ.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase("ABC.*XYZ.txt", "ABcxYZ.txt", true, RegexTester.Constants.ExitCode.Match)]
        [TestCase("ABC.*XYZ.txt", "ABcdefxYZ.txt", true, RegexTester.Constants.ExitCode.Match)]
        [TestCase("ABCO*XYZ.txt", "ABCXYZ.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase("ABCO*XYZ.txt", "ABCOXYZ.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase("ABCO*XYZ.txt", "ABCOOXYZ.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase("ABCO*XYZ.txt", "ABCOOOXYZ.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase("ABCO*XYZ.txt", "ABCOJOXYZ.txt", false, RegexTester.Constants.ExitCode.NotMatch)]

        // + : 1 or more of previous expression
        [TestCase("ABC.+XYZ.txt", "ABCOXYZ.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase("ABC.+XYZ.txt", "ABCOPXYZ.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase("ABC.+XYZ.txt", "ABCOPQXYZ.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase("ABC.+XYZ.txt", "ABCXYZ.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase("ABC.+XYZ.txt", "ABcOxYZ.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase("ABC.+XYZ.txt", "ABcOPxYZ.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase("ABC.+XYZ.txt", "ABcOxYZ.txt", true, RegexTester.Constants.ExitCode.Match)]
        [TestCase("ABC.+XYZ.txt", "ABcOPxYZ.txt", true, RegexTester.Constants.ExitCode.Match)]
        [TestCase("ABCO+XYZ.txt", "ABCOXYZ.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase("ABCO+XYZ.txt", "ABCOOXYZ.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase("ABCO+XYZ.txt", "ABCOOOXYZ.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase("ABCO+XYZ.txt", "ABCOJOXYZ.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase("ABCO+XYZ.txt", "ABCXYZ.txt", false, RegexTester.Constants.ExitCode.NotMatch)]

        // ? : 0 or 1 of previous expression; also forces minimal matching when an expression might match several strings within a search string
        [TestCase("ABC.?XYZ.txt", "ABCXYZ.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase("ABC.?XYZ.txt", "ABCOXYZ.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase("ABC.?XYZ.txt", "ABCOOXYZ.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase("ABC.?XYZ.txt", "ABCxyz.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase("ABC.?XYZ.txt", "ABCxyz.txt", true, RegexTester.Constants.ExitCode.Match)]
        [TestCase("ABCI?XYZ.txt", "ABCXYZ.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase("ABCI?XYZ.txt", "ABCIXYZ.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase("ABCI?XYZ.txt", "ABCIIXYZ.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase("ABCI?XYZ.txt", "ABCiXYZ.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase("ABCI?XYZ.txt", "ABCiXYZ.txt", true, RegexTester.Constants.ExitCode.Match)]

        // [^aeiou]	: Matches any single character not in the specified set of characters
        [TestCase("A[^BCD]Z.txt", "AEZ.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase("A[^BCD]Z.txt", "AFZ.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase("A[^BCD]Z.txt", "AgZ.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase("A[^BCD]Z.txt", "A1Z.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase("A[^BCD]Z.txt", "ABZ.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase("A[^BCD]Z.txt", "ACZ.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase("A[^BCD]Z.txt", "ADZ.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase("A[^BCD]Z.txt", "AEEZ.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase("A[^BCD]Z.txt", "AZ.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase("A[^BCD]Z.txt", "AEz.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase("A[^BCD]Z.txt", "AEz.txt", true, RegexTester.Constants.ExitCode.Match)]

        // [0-9a-fA-F] : Use of a hyphen (–) allows specification of contiguous character ranges
        [TestCase("A[0-5a-fP-S]Z.txt", "A2Z.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase("A[0-5a-fP-S]Z.txt", "AcZ.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase("A[0-5a-fP-S]Z.txt", "AQZ.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase("A[0-5a-fP-S]Z.txt", "A6Z.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase("A[0-5a-fP-S]Z.txt", "AjZ.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase("A[0-5a-fP-S]Z.txt", "AAZ.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase("A[0-5a-fP-S]Z.txt", "A2cQZ.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase("A[0-5a-fP-S]Z.txt", "A0z.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase("A[0-5a-fP-S]Z.txt", "A0z.txt", true, RegexTester.Constants.ExitCode.Match)]

        /*************************************************************************************************************************
        \ : Preceding one of the above, it makes it a literal instead of a special character. Preceding a special matching character.
        Reference: http://www.regular-expressions.info/unicode.html#prop
        **************************************************************************************************************************/

        // \p{name} : Matches any character in the named character class specified by {name}. Supported names are Unicode groups and block ranges. For example, Ll, Nd, Z, IsGreek, IsBoxDrawing.
        // \p{L} : any kind of letter from any language
        [TestCase(@"\p{L}+.txt", "Azô是ணはయىድਜด.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase(@"\p{L}.txt", "1.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\p{L}.txt", " .txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\p{L}.txt", ",.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        // \p{Z} : any kind of whitespace or invisible separator
        [TestCase(@"\p{Z}.txt", " .txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase(@"\p{Z}.txt", "A.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\p{Z}.txt", "5.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\p{Z}.txt", ",.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        // \p{S} : math symbols, currency signs, dingbats, box-drawing characters, etc
        [TestCase(@"\p{S}+.txt", "$₤¥฿₱€©✫┐╠╈╬╣╳.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase(@"\p{S}.txt", "B.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\p{S}.txt", "7.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\p{S}.txt", ";.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        // \p{N} : any kind of numeric character in any script
        [TestCase(@"\p{N}+.txt", "0123456789.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase(@"\p{N}.txt", "B.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\p{N}.txt", "$.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\p{N}.txt", ";.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        // \p{P} : any kind of punctuation character
        [TestCase(@"\p{P}+.txt", "-{}“”_,;?!%@#&*().txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase(@"\p{P}.txt", "C.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\p{P}.txt", "9.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\p{P}.txt", "$.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        // \p{IsGreek} : any kind of Greek character
        [TestCase(@"\p{IsGreek}+.txt", "αβγδεζηθικλμνξοπρσςτυφχψω.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase(@"\p{IsGreek}+.txt", "ΑΒΓΔΕΖΗΘΙΚΛΜΝΞΟΠΡΣΤΥΦΧΨΩ.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase(@"\p{IsGreek}.txt", "B.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\p{IsGreek}.txt", "b.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\p{IsGreek}.txt", "是.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        // \p{IsBoxDrawing} : any kind of box drawing character
        [TestCase(@"\p{IsBoxDrawing}+.txt", "─━│┃┄┅┆┇┈┉┊┋┌┍┎┏┐┑┒┓└┕┖┗┘┙┚┛├┝┞┟┠┡┢┣┤┥┦┧┨┩┪┫┬┭┮┯┰┱┲┳┴┵┶┷┸┹┺┻┼┽┾┿╀╁╂╃╄╅╆╇╈╉╊╋╌╍╎╏═║╒╓╔╕╖╗╘╙╚╛╜╝╞╟╠╡╢╣╤╥╦╧╨╩╪╫╬╭╮╯╰╱╲╳╴╵╶╷╸╹╺╻╼╽╾╿.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase(@"\p{IsBoxDrawing}.txt", "C.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\p{IsBoxDrawing}.txt", "9.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\p{IsBoxDrawing}.txt", "$.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
                
        // \P{name}	: Matches text not included in groups and block ranges specified in {name}.
        // \P{L} : except any kind of letter from any language
        [TestCase(@"\P{L}+.txt", "0123456789-{}“”_,;?!%@#&*()$₤¥฿₱€©✫┐╠╈╬╣╳.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase(@"\P{L}.txt", "A.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\P{L}.txt", "是.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\P{L}.txt", "య.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        // \P{Z} : except any kind of whitespace or invisible separator
        [TestCase(@"\P{Z}+.txt", "0123456789-{}“”_,;?!%@#&*()$₤¥฿₱€©✫┐╠╈╬╣╳.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase(@"\P{Z}.txt", " .txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        // \P{S} : except math symbols, currency signs, dingbats, box-drawing characters, etc
        [TestCase(@"\P{S}+.txt", "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzαβγδεζηθικλμνξοπρσςτυφχψωô是ணはయىድਜด.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase(@"\P{S}.txt", "₤.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\P{S}.txt", "©.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\P{S}.txt", "✫.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\P{S}.txt", "╬.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        // \P{N} : except any kind of numeric character in any script
        [TestCase(@"\P{N}+.txt", "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzαβγδεζηθικλμνξοπρσςτυφχψωô是ணはయىድਜด-{}“”_,;?!%@#&*()$₤¥฿₱€©✫┐╠╈╬╣╳.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase(@"\P{N}.txt", "0.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\P{N}.txt", "5.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\P{N}.txt", "9.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        // \P{P} : except any kind of punctuation character
        [TestCase(@"\P{P}+.txt", "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzαβγδεζηθικλμνξοπρσςτυφχψωô是ணはయىድਜด.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase(@"\P{P}.txt", "{.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\P{P}.txt", ";.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\P{P}.txt", "%.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        // \P{IsGreek} : except any kind of Greek character
        [TestCase(@"\P{IsGreek}+.txt", "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzô是ணはయىድਜด-{}“”_,;?!%@#&*()$₤¥฿₱€©✫┐╠╈╬╣╳.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase(@"\P{IsGreek}.txt", "θ.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\P{IsGreek}.txt", "ψ.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\P{IsGreek}.txt", "Σ.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        // \P{IsBoxDrawing} : any kind of box drawing character
        [TestCase(@"\P{IsBoxDrawing}+.txt", "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzαβγδεζηθικλμνξοπρσςτυφχψωô是ணはయىድਜด-{}“”_,;?!%@#&*()$₤¥฿₱€©✫.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase(@"\P{IsBoxDrawing}.txt", "┅.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\P{IsBoxDrawing}.txt", "╄.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\P{IsBoxDrawing}.txt", "╠.txt", false, RegexTester.Constants.ExitCode.NotMatch)]

        // \w : Matches any word character. Equivalent to the Unicode character categories [\p{Ll}\p{Lu}\p{Lt}\p{Lo}\p{Nd}\p{Pc}]. If ECMAScript-compliant behavior is specified with the ECMAScript option, \w is equivalent to [a-zA-Z_0-9].
        [TestCase(@"\w\w\w.txt", "aB3.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase(@"\w\w\w.txt", "0z_.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase(@"\w\w\w.txt", "aB~.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\w\w\w.txt", "aB!.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\w\w\w.txt", "aB@.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\w\w\w.txt", "aB#.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\w\w\w.txt", "aB$.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\w\w\w.txt", "aB%.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\w\w\w.txt", "aB^.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\w\w\w.txt", "aB&.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\w\w\w.txt", "aB*.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\w\w\w.txt", "aB(.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\w\w\w.txt", "aB).txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\w\w\w.txt", "aB+.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\w\w\w.txt", "aB{.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\w\w\w.txt", "aB}.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\w\w\w.txt", "aB|.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\w\w\w.txt", "aB:.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\w\w\w.txt", "aB\".txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\w\w\w.txt", "aB<.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\w\w\w.txt", "aB>.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\w\w\w.txt", "aB?.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\w\w\w.txt", "aB`.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\w\w\w.txt", "aB-.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\w\w\w.txt", "aB=.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\w\w\w.txt", "aB[.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\w\w\w.txt", "aB].txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\w\w\w.txt", @"aB\.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\w\w\w.txt", "aB;.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\w\w\w.txt", "aB'.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\w\w\w.txt", "aB,.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\w\w\w.txt", "aB..txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\w\w\w.txt", "aB/.txt", false, RegexTester.Constants.ExitCode.NotMatch)]

        // \W : Matches any nonword character. Equivalent to the Unicode categories [^\p{Ll}\p{Lu}\p{Lt}\p{Lo}\p{Nd}\p{Pc}]. If ECMAScript-compliant behavior is specified with the ECMAScript option, \W is equivalent to [^a-zA-Z_0-9].
        [TestCase(@"\W+.txt", "~!@#$%^&*()+{}|:\"<>?`-=[]\\;',./.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase(@"\W+.txt", "aB3.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"\W+.txt", "0z_.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        
        // \s : Matches any white-space character. Equivalent to the Unicode character categories [\f\n\r\t\v\x85\p{Z}]. If ECMAScript-compliant behavior is specified with the ECMAScript option, \s is equivalent to [ \f\n\r\t\v].
        [TestCase(@"ABC\sXYZ.txt", "ABC XYZ.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase(@"ABC\s\sXYZ.txt", "ABC  XYZ.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase(@"ABC\sXYZ.txt", "ABCXYZ.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"ABC\sXYZ.txt", "ABC xyz.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"ABC\sXYZ.txt", "ABC xyz.txt", true, RegexTester.Constants.ExitCode.Match)]
        [TestCase(@"ABC\sXYZ.txt", "ABC\fXYZ.txt", false, RegexTester.Constants.ExitCode.Match)]
        //[TestCase(@"ABC\sXYZ.txt", "ABC\nXYZ.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase(@"ABC\sXYZ.txt", "ABC\rXYZ.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase(@"ABC\sXYZ.txt", "ABC\tXYZ.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase(@"ABC\sXYZ.txt", "ABC\vXYZ.txt", false, RegexTester.Constants.ExitCode.Match)]
        
        // \S : Matches any non-white-space character. Equivalent to the Unicode character categories [^\f\n\r\t\v\x85\p{Z}]. If ECMAScript-compliant behavior is specified with the ECMAScript option, \S is equivalent to [^ \f\n\r\t\v].
        [TestCase(@"ABC\SXYZ.txt", "ABC1XYZ.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase(@"ABC\SXYZ.txt", "ABCRXYZ.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase(@"ABC\SXYZ.txt", "ABCrXYZ.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase(@"ABC\SXYZ.txt", "ABC XYZ.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"ABC\SXYZ.txt", "ABC\fXYZ.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        //[TestCase(@"ABC\SXYZ.txt", "ABC\nXYZ.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"ABC\SXYZ.txt", "ABC\rXYZ.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"ABC\SXYZ.txt", "ABC\tXYZ.txt", false, RegexTester.Constants.ExitCode.NotMatch)]
        [TestCase(@"ABC\SXYZ.txt", "ABC\vXYZ.txt", false, RegexTester.Constants.ExitCode.NotMatch)]

        // \d : Matches any decimal digit. Equivalent to \p{Nd} for Unicode and [0-9] for non-Unicode, ECMAScript behavior.
        [TestCase(@"\d+.txt", "0123456789.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase(@"\d+.txt", "AbCdE!@#$%.txt", false, RegexTester.Constants.ExitCode.NotMatch)]

        // \D : Matches any nondigit. Equivalent to \P{Nd} for Unicode and [^0-9] for non-Unicode, ECMAScript behavior.
        [TestCase(@"\D+.txt", "ABCDEFGHIJKLMNOPQRSTUVWXYZ.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase(@"\D+.txt", "abcdefghijklmnopqrstuvwxyz.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase(@"\D+.txt", "~!@#$%^&*()+{}|:\"<>?`-=[]\\;',./.txt", false, RegexTester.Constants.ExitCode.Match)]
        [TestCase(@"\D+.txt", "0123456789.txt", false, RegexTester.Constants.ExitCode.NotMatch)]

        public void RegexTest_Syntax(string syntax, string pattern, bool ignoreCase, RegexTester.Constants.ExitCode expectedExitCode)
        {
            var args = CommonUtils.BuildCommandLine(syntax, pattern, isWildcard: false, ignoreCase: ignoreCase).ToArray();
            Assert.IsTrue(RegexTester.Program.Main(args) == (int)expectedExitCode);
        }
    }
}
