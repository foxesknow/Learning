using System;
using System.Collections.Generic;

namespace Pipelines
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Print(PipelineParser.Parse("dir *.txt"));
                //Print(PipelineParser.Parse("dir | more"));
                //Print(PipelineParser.Parse("dir|more"));
                //Print(PipelineParser.Parse("  dir| \"hello`tthere\" "));
                //Print(PipelineParser.Parse("dir *.txt | echo foo bar \"   1 2 3 4      \""));
                //Print(PipelineParser.Parse("dir *.txt | more | top 5"));
                Print(PipelineParser.Parse("                         "));
            }
            catch(Exception e)
            {
                Console.Error.WriteLine(e.Message);
            }
        }

        static void Print(IEnumerable<Token> tokens)
        {
            foreach(var token in tokens)
            {
                Console.WriteLine(token);
            }
        }
    }
}
