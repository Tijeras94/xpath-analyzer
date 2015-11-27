namespace xpath_analyzer.parsers
{
    public class Expr
    {
        public object parse(XPathLexer lexer)
        {
            return OrExpr.parse(this, lexer);
        }
    }
}
