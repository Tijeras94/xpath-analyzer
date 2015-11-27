namespace xpath_analyzer.parsers
{
    public static class LocationPath
    {
        public static object parse(Expr rootParser, XPathLexer lexer)
        {
            string token = lexer.peak();
            if(!string.IsNullOrEmpty(lexer.peak()) && token[0] == '/')
            {
                return AbsoluteLocationPath.parse(rootParser, lexer);
            }else
            {
                return RelativeLocationPath.parse(rootParser, lexer);
            }

        }
    }
}
