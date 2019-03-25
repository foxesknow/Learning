using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;
using CsvHelper;
using RandomForest;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            RandomMain(args);
        }

        static void RandomMain(string[] args)
        {
            using(XmlReader xmlReader = XmlReader.Create(@"F:\Code\RandomForest\RandomForest.Sample\RandomForest.Sample\regression_rf.pmml"))
            {
                //var dataReaderFactory = new DictionaryDataReaderFactory();
                //var treeFactory = new TreeFactory<IReadOnlyDictionary<string, double>, double>(dataReaderFactory);

                var dataReaderFactory = new ExpressionDictionaryDataReaderFactory();
                var treeFactory = new ExpressionTreeFactory(dataReaderFactory);

                var loader = new ForestLoader<IReadOnlyDictionary<string, double>, double>(treeFactory);
                var trees = loader.Load(xmlReader);
                var forest = new RegressionRandomForest<IReadOnlyDictionary<string, double>>(trees);

                using (TextReader reader = File.OpenText(@"F:\Code\RandomForest\RandomForest.Sample\RandomForest.Sample\regression_rf_prediction.csv"))
                {
                    // the whole point of this is to build a dictionary with the keys and the values from the csv
                    var csv = new CsvParser(reader);

                    var sw = Stopwatch.StartNew();
                    string[] headers = csv.Read();
                    int lineNumber = 1;
                    while (true)
                    {
                        var row = csv.Read();
                        if(row == null)
                        {
                            break;
                        }

                        var item = headers.Zip(row, (key, value) => new { key, value }).ToDictionary(e => e.key, e => e.value);

                        var rPrediction = double.Parse(item["PredictedG3"]);

                        var realRow = MakeRealRow(item);
                        var cSharpPrediction = forest.Predict(realRow);

                        Console.WriteLine($"Processing line {lineNumber++} - R prediction: {rPrediction} - C#: {cSharpPrediction}");

                        var differencePercentage = Math.Abs((rPrediction - cSharpPrediction) / rPrediction);
                        if(differencePercentage >= 0.00001)
                        {
                            Console.WriteLine("   OOOOOPSSSSS");
                        }
                    }

                    var elapsed = sw.ElapsedMilliseconds;
                    Console.WriteLine(elapsed);
                }
            }
        }

        public static IReadOnlyDictionary<string, double> MakeRealRow(Dictionary<string, string> row)
        {
            var realRow = new Dictionary<string, double>();

            foreach (var item in row)
            {
                if (double.TryParse(item.Value, out var dbl))
                {
                    realRow[item.Key] = dbl;
                }
                else
                {
                    realRow[item.Key + item.Value] = 1;
                }
            }

            return realRow;
        }
    }
}
