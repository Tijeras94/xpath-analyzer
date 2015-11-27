using System.Collections.Generic;

namespace xpath_analyzer.parsers
{
    public static class MultiplicativeExpr
    {
        public static object parse(Expr rootParser, XPathLexer lexer)
        {
            object lhs = UnaryExpr.parse(rootParser, lexer);

            Dictionary<string, string> multiplicativeTypes = new Dictionary<string, string>();
            multiplicativeTypes.Add("*", XPathAnalyzer.ExprType.MULTIPLICATIVE);
            multiplicativeTypes.Add("div", XPathAnalyzer.ExprType.DIVISIONAL);
            multiplicativeTypes.Add("mod", XPathAnalyzer.ExprType.MODULUS);

            if (!string.IsNullOrEmpty(lexer.peak()) && multiplicativeTypes.ContainsKey(lexer.peak()))
            {
                string op = lexer.next();

                object rhs = parse(rootParser, lexer);

                Dictionary<string, object> list = new Dictionary<string, object>();
                list.Add("type", multiplicativeTypes[op]);
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
