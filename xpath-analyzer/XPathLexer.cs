using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace xpath_analyzer
{
    /// <summary>
    /// A lexer for XPath 1.0 expressions.
    /// </summary>
    public class XPathLexer
    {
        private int index;
        private string[] tokens;

        public XPathLexer(string expression)
        {
            this.tokens = XPathLexer.tokenize(expression);
            this.index = 0;
        }

        /// <summary>
        /// should always return the next token
        /// </summary>
        /// <returns></returns>
        public string next()
        {
            return this.tokens[this.index++];
        }

        /// <summary>
        /// should move the lexer backwards once
        /// </summary>
        public void back()
        {
            this.index--;
        }

        /// <summary>
        /// should return the Nth next token without moving forwards
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public string peak(int n = 0)
        {
            if (this.index + n >= this.tokens.Length)
                return null;

            return this.tokens[this.index + n];
        }

        /// <summary>
        /// should return false when the tokens has not been traversed
        /// </summary>
        /// <returns></returns>
        public bool empty()
        {
            return this.tokens.Length <= this.index;
        }

        public static string[] tokenize(string expression)
        {
            String[] exps = new string[] {
                 "\\$?(?:(?![0-9-])[\\w-]+:)?(?![0-9-])[\\w-]+",
                "\\d+\\.\\d+",
                "\\.\\d+",
                "\\d+",
                "\\/\\/",
                @"\/",
                "\\.\\.",
                "\\.",
                "\\s+",
                "::",
                ",",
                "@",
                "-",
                "=",
                "!=",
                "<=",
                "<",
                ">=",
                ">",
                "\\|",
                "\\+",
                "\\*",
                "\\(",
                "\\)",
                "\\[",
                "\\]",
                "\"[^\"]*\"",
                "'[^']*'"
            };

            string ex = string.Join("|", exps);

            List<String> pString = new List<string>();

            Match match = Regex.Match(expression, ex, RegexOptions.IgnoreCase);
            
            while (match.Success)
            {
                if(!XPathLexer.RegexTest(match.Value, @"^\s+$")){
                    pString.Add(match.Value);
                }
                match = match.NextMatch();
            }

            if(pString.Count == 0)
            {
                throw new Exception("Invalid XPath expression");
            }


            return pString.ToArray();
        }

        public static bool RegexTest(string input, string regex_pattern)
        {
            Regex regex = new Regex(regex_pattern);
            Match match = regex.Match(input);
            return match.Success;
        }
    }
}
