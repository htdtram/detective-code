using DetectiveCodeBySelf.Model;
using System;
using System.IO;

namespace DetectiveCodeBySelf
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\tram.huynh\Downloads\Demo\Example\1.cs";
            string path2 = @"C:\Users\tram.huynh\Downloads\Demo\PreProcess\1.cs";
            string a = "using System;\nnamespace ConsoleApp1\n{\n    class Program\n    {  /*\n* abc desnnj \n*/\n      static void Main(string[] args)\n        {\n			//abc// ebc/\n            string c =\"acf\";\n			int a;\nint 			b,  s;\n			/*dvahsdbjas\n															dad**/\n            Console.WriteLine(b);\n            Console.ReadKey();/*dvahsdbjas*/\n        }\n    }\n}\n";
            string text = File.ReadAllText(path);

            var b = PreProcess.StripComments(text);
            //var b = PreProcess.StripComments(a);
            var c = PreProcess.RemoveBlank(b);
            var d = PreProcess.RemovePackage(c);
            File.WriteAllText(path2, d);
            //Console.ReadKey();
        }
    }
}
