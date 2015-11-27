using System;
using System.Collections.Generic;
using xpath_analyzer.validators;

namespace xpath_analyzer.parsers
{
    public static class NodeTest
    {
        public static object parse(Expr rootParser, XPathLexer lexer)
        {
            Dictionary<string, object> ret = new Dictionary<string, object>();
            
            if (!string.IsNullOrEmpty(lexer.peak()) &&  lexer.peak().Equals("*"))
            {
                lexer.next();

                ret.Add("name", "*");
                return ret;
            }

            if (!string.IsNullOrEmpty(lexer.peak(1)) && lexer.peak(1).Equals("("))
            {
                if (NodeTypeValidator.isValid(lexer.peak()))
                {
                    ret.Add("type", lexer.next());
                    lexer.next();

                    if (lexer.peak().Equals(")"))
                    {
                        lexer.next();
                    }
                    else
                    {
                        ret.Add("name", lexer.next());
                        lexer.next();
                    }

                    return ret;
                }
                else
                {
                    throw new Exception("Error: Unexpected token " + lexer.peak());

                }
            }


            ret = new Dictionary<string, object>();
            ret.Add("name", lexer.next());
            return ret;
        }
    }
}
