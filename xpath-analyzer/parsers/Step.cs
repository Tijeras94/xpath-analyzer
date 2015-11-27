using System;
using System.Collections.Generic;
using xpath_analyzer.validators;

namespace xpath_analyzer.parsers
{
    public static class Step
    {
        public static Dictionary<string, object> parse(Expr rootParser, XPathLexer lexer)
        {
            Dictionary<string, object> step = new Dictionary<string, object>();
            step.Add("axis", "");
            step.Add("test", new object());

            if (!string.IsNullOrEmpty(lexer.peak(1)) && lexer.peak(1).Equals("::"))
            {
                var axisSpecifier = lexer.next();

                lexer.next();

                if (AxisSpecifierValidator.isValid(axisSpecifier))
                {
                    step["axis"] = axisSpecifier;
                }
                else
                {
                    throw new Exception("Error: Unexpected token " + axisSpecifier);
                }
            }
            else if (!string.IsNullOrEmpty(lexer.peak()) && lexer.peak().Equals("@"))
            {
                lexer.next();

                step["axis"] = XPathAnalyzer.AxisSpecifier.ATTRIBUTE;
            }else if (!string.IsNullOrEmpty(lexer.peak()) &&  lexer.peak().Equals("..")) {
                lexer.next();

                Dictionary<string, object> test = new Dictionary<string, object>();
                test.Add("type", XPathAnalyzer.NodeType.NODE);

                step["axis"] = XPathAnalyzer.AxisSpecifier.PARENT;
                step["test"] = test;
                return step;
            }
            else if (!string.IsNullOrEmpty(lexer.peak()) &&  lexer.peak().Equals("."))
            {
                lexer.next();

                Dictionary<string, object> test = new Dictionary<string, object>();
                test.Add("type", XPathAnalyzer.NodeType.NODE);

                step["axis"] = XPathAnalyzer.AxisSpecifier.SELF;
                step["test"] = test;

                return step;
            }
            else
            {
                step["axis"] = XPathAnalyzer.AxisSpecifier.CHILD;
            }

            step["test"] = NodeTest.parse(rootParser, lexer);

            while (!string.IsNullOrEmpty(lexer.peak()) &&  lexer.peak().Equals("["))
            {
                if(!step.ContainsKey("predicates"))
                {
                    step.Add("predicates", new List<object>());
                }
                ((List<object>)step["predicates"]).Add(Predicate.parse(rootParser, lexer));
            }

            return step;
        }
    }
}
