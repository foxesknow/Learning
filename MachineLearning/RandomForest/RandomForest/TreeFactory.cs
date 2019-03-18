using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace RandomForest
{
    public class TreeFactory<TData, TScore>
    {
        private readonly DataReaderFactory<TData> m_DataReaderFactory;

        public TreeFactory(DataReaderFactory<TData> dataReaderFactory)
        {
            m_DataReaderFactory = dataReaderFactory;
        }

        public ITree<TData, TScore> MakeTree(XElement element)
        {
            var elements = element.Elements();
                
            // TODO: Optimize this
            var id = int.Parse(element.Attribute("id").Value);
            var predicateElement = elements.First();
            var leftElement = elements.Skip(1).FirstOrDefault();
            var rightElement = (leftElement == null ? null : elements.Skip(2).FirstOrDefault());

            ITree<TData, TScore> left = null;
            if(leftElement != null) left = MakeNode(leftElement);

            ITree<TData, TScore> right = null;
            if(rightElement != null) right = MakeNode(rightElement);

            switch(predicateElement.Name.LocalName)
            {
                case "True":
                    return new AlwaysTrueTree<TData, TScore>(id, left, right);

                case "SimplePredicate":
                    var predicate = MakePredicate(predicateElement);
                    return new ConditionalTree<TData, TScore>(id, predicate, left, right);

                default:
                    throw new Exception($"unsupported predicate {predicateElement.Name.LocalName}");
            }
        }

        private ITree<TData, TScore> MakeNode(XElement element)
        {
            if(element.Attribute("score") != null)
            {
                var elements = element.Elements();
                var predicateElement = elements.First();
                var predicate = MakePredicate(predicateElement);

                var id = int.Parse(element.Attribute("id").Value);
                var value = (TScore)Convert.ChangeType(element.Attribute("score").Value, typeof(TScore));
                return new ScoreTree<TData, TScore>(id, value, predicate);
            }
            else
            {
                return MakeTree(element);
            }
        }

        private Func<TData, bool> MakePredicate(XElement element)
        {
            var field = element.Attribute("field").Value;
            var op = element.Attribute("operator").Value;
            var value = double.Parse(element.Attribute("value").Value);

            var reader = m_DataReaderFactory.GetDataReader(field);

            switch(op)
            {
                case "lessOrEqual":
                    return MakeLessOrEqual(reader, value);

                case "greaterThan":
                    return MakeGreaterThan(reader, value);

                default:
                    throw new Exception($"unknown operator {op}");
            }
        }

         private Func<TData, bool> MakeLessOrEqual(Func<TData, double> reader, double value)
         {
            return data => reader(data) <= value;
         }

         private Func<TData, bool> MakeGreaterThan(Func<TData, double> reader, double value)
         {
            return data => reader(data) > value;
         }
    }
}
