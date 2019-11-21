using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DetectiveCodeBySelf.Model
{
    public class PreProcess
    {
        public static string StripComments(string input)
        {
            var blockComments = @"/\*(.*?)\*/";
            var lineComments = @"//(.*?)\r?\n";
            var strings = @"""((\\[^\n]|[^""\n])*)""";
            var verbatimStrings = @"@(""[^""]*"")+";
            string noComments = Regex.Replace(input, blockComments + "|" + lineComments + "|" + strings + "|" + verbatimStrings,
            me =>
            {
                if (me.Value.StartsWith("/*") || me.Value.StartsWith("//"))
                    return me.Value.StartsWith("//") ? Environment.NewLine : "";
                return me.Value;
            }, RegexOptions.Singleline);
            return noComments;
        }

        public static string RemoveBlank(string input)
        {
            return Regex.Replace(input.Trim(), @"[^\S\r\n]+", " ");
        }

        public static string RemovePackage(string input)
        {
            return Regex.Replace(input, @"using(.*?)\r?\n", " ");
        }
    }
}
