using System;

namespace xpath_analyzer.parsers
{
    public static class Predicate
    {
        public static object parse(Expr rootParser, XPathLexer lexer)
        {
            lexer.next();

            var predicate = rootParser.parse(lexer);

            if (!lexer.next().Equals("]"))
            {
                throw new Exception("Error: Unclosed brackets");
            }

            return predicate;
        }
    }
}


