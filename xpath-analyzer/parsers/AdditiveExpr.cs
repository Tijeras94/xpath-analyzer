using System.Collections.Generic;

namespace xpath_analyzer.parsers
{
    public static class AdditiveExpr
    {
        public static object parse(Expr rootParser, XPathLexer lexer)
        {
            object lhs = MultiplicativeExpr.parse(rootParser, lexer);

            Dictionary<string, string> additiveTypes = new Dictionary<string, string>();
            additiveTypes.Add("+", XPathAnalyzer.ExprType.ADDITIVE);
            additiveTypes.Add("-", XPathAnalyzer.ExprType.SUBTRACTIVE);

            
            if (!string.IsNullOrEmpty(lexer.peak()) && additiveTypes.ContainsKey(lexer.peak()))
            {
                string op = lexer.next();

                object rhs = parse(rootParser, lexer);

                Dictionary<string, object> list = new Dictionary<string, object>();
                list.Add("type", additiveTypes[op]);
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
