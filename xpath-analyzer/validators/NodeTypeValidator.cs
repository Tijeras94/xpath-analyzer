using System;
using System.Collections.Generic;

namespace xpath_analyzer.validators
{
    public static class NodeTypeValidator
    {
        public static bool isValid(string type)
        {
            return getValidTypes().Contains(type);
        }
        
        public static List<String> getValidTypes()
        {
            List<String> values = new List<string>();
            foreach (var type in typeof(XPathAnalyzer.NodeType).GetFields())
            {
                values.Add(type.GetValue(typeof(XPathAnalyzer.NodeType)).ToString());


            }
            return values;
        }

    }
}
