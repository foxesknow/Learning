using System;
using System.Collections.Generic;
using System.Text;

namespace Pipelines
{
    static class PipelineParser
    {
        private static readonly char DoubleQuote = '"';
        private static readonly char SingleQuote = '\'';
        private static readonly char Pipe = '|';
        private static readonly char Escape = '`';

        public static IEnumerable<Token> Parse(string line)
        {
            bool inEscape = false;
            bool inQuote = false;
            var lastToken = TokenType.None;
            char quoteBlockTerminator = '\0';

            var active = "";

            foreach(var c in line)
            {
                if(inQuote)
                {
                    if(inEscape)
                    {
                        active += ConvertEscape(c);
                        inEscape = false;
                        continue;
                    }

                    // Only escape text in double quotes, like Powershell
                    if(c == Escape && quoteBlockTerminator == DoubleQuote)
                    {
                        inEscape = true;
                        continue;
                    }

                    if(c == quoteBlockTerminator)
                    {
                        yield return MakeAndReset(TokenType.QuotedText);
                        inQuote = false;
                        quoteBlockTerminator = '\0';
                        continue;
                    }

                    active += c;
                }
                else
                {
                    if(c == Escape) throw new Exception("can only escape in quoted text");

                    if(char.IsWhiteSpace(c))
                    {
                        if(active.Length != 0)
                        {
                            yield return MakeAndReset(TokenType.String);
                        }

                        continue;
                    }

                    if(c == DoubleQuote || c == SingleQuote || c == Pipe)
                    {
                        if(active.Length != 0)
                        {
                            yield return MakeAndReset(TokenType.String);
                        }

                        if(c == Pipe)
                        {
                            if(lastToken != TokenType.String) throw new Exception("an empty pipe element is not allowed");

                            yield return MakeAndReset(TokenType.Pipe);
                            continue;
                        }

                        inQuote = true;
                        quoteBlockTerminator = c;

                        continue;
                    }

                    active += c;
                }
            }

            if(inQuote) throw new Exception($"the string is missing the terminator : {quoteBlockTerminator}");
            if(inEscape) throw new Exception("incomplete escape sequence");
            

            if(active.Length != 0)
            {
                yield return new Token(TokenType.String, active);
            }
            else if(lastToken == TokenType.None) 
            {
                yield return new Token();
            }            
            else if(lastToken == TokenType.Pipe) 
            {
                throw new Exception("an empty pipe element is not allowed");
            }

            Token MakeAndReset(TokenType type)
            {
                var token = new Token(type, active);
                lastToken = type;
                active = "";

                return token;
            }
        }

        private static char ConvertEscape(char c)
        {
            switch(c)
            {
                case 't':   return '\t';
                case 'r':   return '\r';
                case 'n':   return '\n';
                default:    return c;
            }
        }
    }

    struct Token
    {
        public Token(TokenType type, string value)
        {
            this.Type = type;
            this.Value = value;
        }

        public readonly TokenType Type;
        public readonly string Value;

        public override string ToString()
        {
            if(this.Type == TokenType.String)
            {
                return $"String = [{this.Value}]";
            }
            else if(this.Type == TokenType.QuotedText)
            {
                return $"Quoted = [{this.Value}]";
            }
            else
            {
                return this.Type.ToString();
            }
        }
    }

    enum TokenType
    {
        None,
        Pipe,
        String,
        QuotedText
    }
}
