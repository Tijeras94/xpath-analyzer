using System.Collections.Generic;

namespace xpath_analyzer.parsers
{
    public static class FunctionCall
    {
        public static object parse(Expr rootParser, XPathLexer lexer)
        {
            Dictionary<string, object> funCall = new Dictionary<string, object>();
            funCall.Add("type", XPathAnalyzer.ExprType.FUNCTION_CALL);
            funCall.Add("name", lexer.next());

            lexer.next();


            if (lexer.peak().Equals(")"))
            {
                lexer.next();
            }else
            {
                funCall.Add("args", new List<object>());

                while (!lexer.peak().Equals(")"))
                {
                    ((List<object>)funCall["args"]).Add(rootParser.parse(lexer));

                    if (lexer.peak().Equals(","))
                    {
                        lexer.next();
                    }
                }


                lexer.next();
            }

            return funCall;
        }
    }
}
