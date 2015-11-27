using System.Collections.Generic;

namespace xpath_analyzer.parsers
{
    public static class PathExpr
    {
       
        public static object parse(Expr rootParser, XPathLexer lexer)
        {
            if (FilterExpr.isValidOp(lexer))
            {
                object filter = FilterExpr.parse(rootParser, lexer);

                if (!lexer.empty() && (lexer.peak()[0] == '/'))
                {
                    Dictionary<string, object> path = new Dictionary<string, object>();
                    path.Add("type", XPathAnalyzer.ExprType.PATH);
                    path.Add("filter", filter);
                    path.Add("steps", new List<Dictionary<string, object>>());

                    while (!lexer.empty() && (lexer.peak()[0] == '/'))
                    {
                        if (lexer.next().Equals("//"))
                        {
                            Dictionary<string, object> itm = new Dictionary<string, object>();
                            itm.Add("axis", XPathAnalyzer.AxisSpecifier.DESCENDANT_OR_SELF);

                            Dictionary<string, string> test = new Dictionary<string, string>();
                            test.Add("type", XPathAnalyzer.NodeType.NODE);

                            itm.Add("test", test);


                            ((List<Dictionary<string, object>>)path["steps"]).Add(itm);
                        }

                        ((List<Dictionary<string, object>>)path["steps"]).Add(Step.parse(rootParser, lexer));
                    }

                    return path;
                }
                else
                {
                    return filter;
                }

            }
            else
            {
                return LocationPath.parse(rootParser, lexer);
            }
        }
    }
}
