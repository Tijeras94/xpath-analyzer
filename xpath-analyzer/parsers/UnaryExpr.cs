using System.Collections.Generic;

namespace xpath_analyzer.parsers
{
    public static class UnaryExpr
    {
        public static object parse(Expr rootParser, XPathLexer lexer)
        {
            if (!string.IsNullOrEmpty(lexer.peak()) && lexer.peak().Equals("-"))
            {
                lexer.next();

                Dictionary<string, object> list = new Dictionary<string, object>();
                list.Add("type", XPathAnalyzer.ExprType.NEGATION);
                list.Add("lhs", parse(rootParser, lexer));
                return list;
            }
            else
            {
                return UnionExpr.parse(rootParser, lexer);
            }
        }
    }
}
