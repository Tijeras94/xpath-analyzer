using System.Collections.Generic;

namespace xpath_analyzer.parsers
{
    public static class UnionExpr
    {
        public static object parse(Expr rootParser, XPathLexer lexer)
        {
            object lhs = PathExpr.parse(rootParser, lexer);

            if (!string.IsNullOrEmpty(lexer.peak()) && lexer.peak().Equals("|"))
            {
                lexer.next();

                object rhs = parse(rootParser, lexer);

                Dictionary<string, object> list = new Dictionary<string, object>();
                list.Add("type", XPathAnalyzer.ExprType.UNION);
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
