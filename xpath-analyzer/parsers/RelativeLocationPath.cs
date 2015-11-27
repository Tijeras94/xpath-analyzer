using System.Collections.Generic;

namespace xpath_analyzer.parsers
{
    public static class RelativeLocationPath
    {
        public static object parse(Expr rootParser, XPathLexer lexer)
        {
            Dictionary<string, object> relativeLocation = new Dictionary<string, object>();
            relativeLocation.Add("type", XPathAnalyzer.ExprType.RELATIVE_LOCATION_PATH);
            relativeLocation.Add("steps", new List<Dictionary<string, object>>());

            ((List<Dictionary<string, object>>)relativeLocation["steps"]).Add(Step.parse(rootParser, lexer));


            while (!lexer.empty() && (!string.IsNullOrEmpty(lexer.peak()) && lexer.peak()[0] == '/'))
            {
                if (lexer.next().Equals("/"))
                {
                    ((List<Dictionary<string, object>>)relativeLocation["steps"]).Add(Step.parse(rootParser, lexer));
                }
                else
                {
                    Dictionary<string, object> itm = new Dictionary<string, object>();
                    itm.Add("axis", XPathAnalyzer.AxisSpecifier.DESCENDANT_OR_SELF);

                    Dictionary<string, object> test = new Dictionary<string, object>();
                    test.Add("type", XPathAnalyzer.NodeType.NODE);

                    itm.Add("test", test);


                    ((List<Dictionary<string, object>>)relativeLocation["steps"]).Add(itm);

                    ((List<Dictionary<string, object>>)relativeLocation["steps"]).Add(Step.parse(rootParser, lexer));
                }
            }

            return relativeLocation;
        }
    }
}
