using System.Collections.Generic;

namespace xpath_analyzer.parsers
{
    public static class RelationalExpr
    {
        public static object parse(Expr rootParser, XPathLexer lexer)
        {
            object lhs = AdditiveExpr.parse(rootParser, lexer);

            Dictionary<string, string> relationalTypes = new Dictionary<string, string>();
            relationalTypes.Add("<", XPathAnalyzer.ExprType.LESS_THAN);
            relationalTypes.Add(">", XPathAnalyzer.ExprType.GREATER_THAN);
            relationalTypes.Add("<=", XPathAnalyzer.ExprType.LESS_THAN_OR_EQUAL);
            relationalTypes.Add(">=", XPathAnalyzer.ExprType.GREATER_THAN_OR_EQUAL);


            if (!string.IsNullOrEmpty(lexer.peak()) && relationalTypes.ContainsKey(lexer.peak()))
            {
                string op = lexer.next();

                object rhs = parse(rootParser, lexer);

                Dictionary<string, object> list = new Dictionary<string, object>();
                list.Add("type", relationalTypes[op]);
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
