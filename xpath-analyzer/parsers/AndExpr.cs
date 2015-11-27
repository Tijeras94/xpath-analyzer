using System.Collections.Generic;

namespace xpath_analyzer.parsers
{
    public static class AndExpr
    {
        public static object parse(Expr rootParser, XPathLexer lexer)
        {
            object lhs = EqualityExpr.parse(rootParser, lexer);

            if (!string.IsNullOrEmpty(lexer.peak()) && lexer.peak().Equals("and"))
            {
                lexer.next();

                object rhs = parse(rootParser, lexer);

                Dictionary<string, object> list = new Dictionary<string, object>();
                list.Add("type", XPathAnalyzer.ExprType.AND);
                list.Add("lhs", lhs);
                list.Add("rhs", rhs);
                return list;
            }
            else
            {
                return lhs;
            }
        }
    }
}
