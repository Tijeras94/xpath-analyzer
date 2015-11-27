using System.Collections.Generic;
namespace xpath_analyzer.parsers
{
    public static class AbsoluteLocationPath
    {
        public static object parse(Expr rootParser, XPathLexer lexer)
        {
            Dictionary<string, object> absoluteLocation = new Dictionary<string, object>();
            absoluteLocation.Add("type", XPathAnalyzer.ExprType.ABSOLUTE_LOCATION_PATH);
            
            while (!lexer.empty() && (lexer.peak()[0] == '/'))
            {
                if(!absoluteLocation.ContainsKey("steps"))
                    absoluteLocation.Add("steps", new List<Dictionary<string, object>>());

                if (lexer.next().Equals("/"))
                {
                    var next = lexer.peak();

                    if (!lexer.empty() && (next.Equals(".") || next.Equals("..") || next.Equals("@") || next.Equals("*") || XPathLexer.RegexTest(next, @"(?![0 - 9])[\w]")))
                    {
                        ((List<Dictionary<string, object>>)absoluteLocation["steps"]).Add(Step.parse(rootParser, lexer));
                    }
                }else
                {
                    Dictionary<string, object> itm = new Dictionary<string, object>();
                    itm.Add("axis", XPathAnalyzer.AxisSpecifier.DESCENDANT_OR_SELF);

                    Dictionary<string, object> test = new Dictionary<string, object>();
                    test.Add("type", XPathAnalyzer.NodeType.NODE);

                    itm.Add("test", test);

                    ((List<Dictionary<string, object>>)absoluteLocation["steps"]).Add(itm);

                    ((List<Dictionary<string, object>>)absoluteLocation["steps"]).Add(Step.parse(rootParser, lexer));
                }
            }
            return absoluteLocation;
        }
    }
}
