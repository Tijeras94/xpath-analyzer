using System;
using xpath_analyzer.parsers;

namespace xpath_analyzer
{
    public class XPathAnalyzer
    {

        private XPathLexer lexer;

        public XPathAnalyzer(string expression)
        {
            this.lexer = new XPathLexer(expression);
        }

        public dynamic parse()
        {
            object ast = new Expr().parse(this.lexer);

            if (this.lexer.empty())
                return ast;
            else
                throw new Exception("Unexpected token " + this.lexer.peak());
        }

        public static class AxisSpecifier
        {
            public static string ANCESTOR = "ancestor";
            public static string ANCESTOR_OR_SELF = "ancestor-or-self";
            public static string ATTRIBUTE = "attribute";
            public static string CHILD = "child";
            public static string DESCENDANT = "descendant";
            public static string DESCENDANT_OR_SELF = "descendant-or-self";
            public static string FOLLOWING = "following";
            public static string FOLLOWING_SIBLING = "following-sibling";
            public static string NAMESPACE = "namespace";
            public static string PARENT = "parent";
            public static string PRECEDING = "preceding";
            public static string PRECEDING_SIBLING = "preceding-sibling";
            public static string SELF = "self";
        }

        public static class ExprType
        {
            public static string ABSOLUTE_LOCATION_PATH = "absolute-location-path";
            public static string ADDITIVE = "additive";
            public static string AND = "and";
            public static string DIVISIONAL = "divisional";
            public static string EQUALITY = "equality";
            public static string FILTER = "filter";
            public static string FUNCTION_CALL = "function-call";
            public static string GREATER_THAN = "greater-than";
            public static string GREATER_THAN_OR_EQUAL = "greater-than-or-equal";
            public static string INEQUALITY = "inequality";
            public static string LESS_THAN = "less-than";
            public static string LESS_THAN_OR_EQUAL = "less-than-or-equal";
            public static string LITERAL = "literal";
            public static string MODULUS = "modulus";
            public static string MULTIPLICATIVE = "multiplicative";
            public static string NEGATION = "negation";
            public static string NUMBER = "number";
            public static string OR = "or";
            public static string PATH = "path";
            public static string RELATIVE_LOCATION_PATH = "relative-location-path";
            public static string SUBTRACTIVE = "subtractive";
            public static string UNION = "union";
        }

        public static class NodeType
        {
            public static string COMMENT = "comment";
            public static string NODE = "node";
            public static string PROCESSING_INSTRUCTION = "processing-instruction";
            public static string TEXT = "text";
        }
    }
}
