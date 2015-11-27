using System;
using System.Collections.Generic;
using System.Globalization;
using xpath_analyzer.validators;

namespace xpath_analyzer.parsers
{
    public static class PrimaryExpr
    {
        public static bool isValidOp(XPathLexer lexer)
        {
            if (string.IsNullOrEmpty(lexer.peak()))
                return false;

            char ch = lexer.peak()[0];

            return ch == '(' ||
     ch == '\\' ||
     ch == '\'' ||
     ch == '$' ||
    XPathLexer.RegexTest(ch + "", @"^\d+$") ||
      XPathLexer.RegexTest(ch + "", @"^d+$^(\d+)?\.\d+$") ||
         ((lexer.peak(1) != null) && (lexer.peak(1).Equals("(")) && !NodeTypeValidator.isValid(lexer.peak()));
        }

        public static object parse(Expr rootParser, XPathLexer lexer)
        {
            string token = lexer.peak();
            char ch = token[0];

            if (ch == '(')
            {
                lexer.next();

                var expr = rootParser.parse(lexer);

                if (!lexer.next().Equals(")"))
                {
                    throw new Exception("Error: Unclosed parentheses");
                }

                return expr;
            }

            if (ch == '"' || ch == '\'')
            {
                lexer.next();

                Dictionary<string, object> r = new Dictionary<string, object>();
                r.Add("type", XPathAnalyzer.ExprType.LITERAL);
                r.Add("string", token.Substring(1, (token.Length - 1) - 1));//token.slice(1, -1)

                return r;
            }

            if (ch == '$')
            {
                throw new Exception("Error: Variable reference are not implemented");
            }

            if (XPathLexer.RegexTest(token, @"^\d+$") || XPathLexer.RegexTest(token, @"^(\d+)?\.\d+$"))
            {
                lexer.next();

                Dictionary<string, object> r = new Dictionary<string, object>();
                r.Add("type", XPathAnalyzer.ExprType.NUMBER);
                r.Add("number", float.Parse(token, CultureInfo.InvariantCulture));//token.slice(1, -1)

                return r;
            }

            if (lexer.peak(1) == "(" && !NodeTypeValidator.isValid(lexer.peak()))
            {
                return FunctionCall.parse(rootParser, lexer);
            }

            return null;
            //throw new Exception("Error: Unhandle Expresion!");
        }
    }
}
