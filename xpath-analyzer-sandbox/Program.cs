using System;
using xpath_analyzer;
namespace xpath_analyzer_sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            XPathAnalyzer analizer = new XPathAnalyzer("1 + 1");
            object obj  = analizer.parse();
            var json = JsonHelper.ToJson(obj);
            Console.WriteLine(json);

        }
    }
}
