using System.Collections.Generic;

namespace xpath_analyzer.parsers
{
    public static class EqualityExpr
    {
        public static object parse(Expr rootParser, XPathLexer lexer)
        {
            object lhs = RelationalExpr.parse(rootParser, lexer);

            Dictionary<string, string> equalityTypes = new Dictionary<string, string>();
            equalityTypes.Add("=", XPathAnalyzer.ExprType.EQUALITY);
            equalityTypes.Add("!=", XPathAnalyzer.ExprType.INEQUALITY);

            if (!string.IsNullOrEmpty(lexer.peak()) && equalityTypes.ContainsKey(lexer.peak()))
            {
                string op = lexer.next();

                object rhs = parse(rootParser, lexer);

                Dictionary<string, object> list = new Dictionary<string, object>();
                list.Add("type", equalityTypes[op]);
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
