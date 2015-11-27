# xpath-analyzer
An analyzer / parser for XPath 1.0 expressions.  
This is a C# port, originally available in Javascript here: https://github.com/badeball/xpath-analyzer


```cs
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
//{
//    "type": "additive",
//    "lhs": {
//      "type": "number",
//      "number": 1
//    },
//    "rhs": {
//      "type": "number",
//      "number": 1
//    }
//  }
```
