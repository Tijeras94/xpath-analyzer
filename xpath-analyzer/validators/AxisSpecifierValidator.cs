using System;
using System.Collections.Generic;

namespace xpath_analyzer.validators
{
    public static class AxisSpecifierValidator
    {
        public static bool isValid(string type)
        {
            return getValidTypes().Contains(type);
        }

        public static List<String> getValidTypes()
        {
            List<String> values = new List<string>();
            foreach (var type in typeof(XPathAnalyzer.AxisSpecifier).GetFields())
            {
                values.Add(type.GetValue(typeof(XPathAnalyzer.AxisSpecifier)).ToString());
            }
            return values;
        }
    }
}
