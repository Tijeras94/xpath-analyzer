using System.Collections.Generic;

namespace xpath_analyzer.parsers
{
    public static class OrExpr
    {
        public static object parse(Expr rootParser, XPathLexer lexer)
        {
            object lhs = AndExpr.parse(rootParser, lexer);

            if (!string.IsNullOrEmpty(lexer.peak()) && lexer.peak().Equals("or"))
            {
                lexer.next();

                object rhs = parse(rootParser, lexer);

                Dictionary<string, object> list = new Dictionary<string, object>();
                list.Add("type", XPathAnalyzer.ExprType.OR);
                list.Add("lhs", lhs);
                list.Add("rhs", rhs);
                return list;
            }else
            {
                return lhs;
            }
        }
    }
}
