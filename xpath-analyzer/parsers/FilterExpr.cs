using System.Collections.Generic;

namespace xpath_analyzer.parsers
{
    public static class FilterExpr
    {
        //PrimaryExpr primExp = new PrimaryExpr();
        public static bool isValidOp(XPathLexer lexer)
        {
            return PrimaryExpr.isValidOp(lexer);
        }

        public static object parse(Expr rootParser, XPathLexer lexer)
        {
            object primary = PrimaryExpr.parse(rootParser, lexer);


            if(!string.IsNullOrEmpty(lexer.peak()) && lexer.peak().Equals("["))
            {
                Dictionary<string, object> filter = new Dictionary<string, object>();
                filter.Add("type", XPathAnalyzer.ExprType.FILTER);
                filter.Add("primary", primary);
                filter.Add("predicates", new List<object>());


                while (!string.IsNullOrEmpty(lexer.peak()) && lexer.peak().Equals("["))
                {
                    ((List<object>)filter["predicates"]).Add(Predicate.parse(rootParser, lexer));
                }

                return filter;
            }
            else
            {
                return primary;
            }
        }
    }
}
