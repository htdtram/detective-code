using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace DetectiveCodeBySelf.Model
{
    public class PreProcess
    {

        public static void ReadAndWriteFileAfterPreProcess(string folderPath)
        {
            foreach (var file in Directory.EnumerateFiles(folderPath,"*.cs"))
            {
                string source = File.ReadAllText(file);
                var sourcePreprocess = RemovePackage(RemoveBlank(StripComments(source)));
                File.WriteAllText(RenderFileNameAfterPreprocess(file), sourcePreprocess);
            }
        }

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
            var usingstring = @"using(.*?)\r?\n";
            var includestring = @"#include(.*?)\r?\n";
            return Regex.Replace(input, usingstring + "|" + includestring, "");
            //return Regex.Replace(input, @"(?!(using*|#include*))(.*?)\r?\n", "");
        }

        public static string RenderFileNameAfterPreprocess(string filePath)
        {
            return Regex.Replace(filePath, "Example", "PreProcess");
        }
    }
}
