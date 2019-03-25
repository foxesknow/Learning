using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RandomForest
{
    public class ExpressionTreeFactory : ITreeFactory<IReadOnlyDictionary<string, double>, double>
    {
        private static readonly Expression s_Throw = Expression.Throw(Expression.Constant(new Exception()), typeof(double));
        private static readonly Expression s_Zero = Expression.Constant((double)0, typeof(double));

        private readonly Expression s_True = Expression.Constant(true, typeof(bool));

        private readonly ExpressionDictionaryDataReaderFactory m_DataReaderFactory = null;

        public ExpressionTreeFactory(ExpressionDictionaryDataReaderFactory factory)
        {
            m_DataReaderFactory = factory;
        }

        public ITree<IReadOnlyDictionary<string, double>, double> MakeTree(XElement element)
        {
            var source = Expression.Parameter(typeof(IReadOnlyDictionary<string, double>));

            var returnLabel = Expression.Label(typeof(double));
            Expression condition = DoMakeTree(element, source, returnLabel);

            var body = Expression.Block
            (
                condition,
                Expression.Label(returnLabel, s_Throw)
            );

            var lambda = Expression.Lambda<Func<IReadOnlyDictionary<string, double>, double>>(body, source);
            var function = lambda.Compile();

            return new ExpressionTree(function);
        }

        public Expression DoMakeTree(XElement element, ParameterExpression source, LabelTarget labal)
        {
            var elements = element.Elements();

            var predicateElement = elements.First();
            var leftElement = elements.Skip(1).FirstOrDefault();
            var rightElement = (leftElement == null ? null : elements.Skip(2).FirstOrDefault());

            Expression left = s_Throw;
            if(leftElement != null) left = MakeNode(leftElement, source, labal);

            Expression right = s_Throw;
            if(rightElement != null) right = MakeNode(rightElement, source, labal);

            switch(predicateElement.Name.LocalName)
            {
                case "True":
                {
                    var body = Expression.Block(left, right, s_Zero);
                    return Expression.IfThen(s_True, body);
                }

                case "SimplePredicate":
                {
                    var predicate = MakePredicate(predicateElement, source);
                    var body = Expression.Block(left, right, s_Zero);
                    return Expression.IfThen(predicate, body);
                }

                default:
                    throw new Exception($"unsupported predicate {predicateElement.Name.LocalName}");
            }
        }

        private Expression MakeNode(XElement element, ParameterExpression source, LabelTarget label)
        {
            if(element.Attribute("score") != null)
            {
                var elements = element.Elements();
                var predicateElement = elements.First();
                var predicate = MakePredicate(predicateElement, source);

                double value = double.Parse(element.Attribute("score").Value);
                var score = Expression.Constant(value, typeof(double));

                var condition = Expression.IfThen(predicate, Expression.Return(label, score, typeof(double)));
                return condition;
            }
            else
            {
                return DoMakeTree(element, source, label);
            }
        }

        private Expression MakePredicate(XElement element, ParameterExpression source)
        {
            var field = element.Attribute("field").Value;
            var op = element.Attribute("operator").Value;
            var value = double.Parse(element.Attribute("value").Value);

            //return Expression.Constant(true, typeof(bool));

            var reader = m_DataReaderFactory.GetDataReader(field, source);

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

        private Expression MakeLessOrEqual(Expression reader, double value)
        {
            return Expression.LessThanOrEqual(reader, Expression.Constant(value, typeof(double)));
        }

        private Expression MakeGreaterThan(Expression reader, double value)
        {
            return Expression.GreaterThan(reader, Expression.Constant(value, typeof(double)));
        }

        class ExpressionTree : ITree<IReadOnlyDictionary<string, double>, double>
        {
            private readonly Func<IReadOnlyDictionary<string, double>, double> m_Evaluator;

            public ExpressionTree(Func<IReadOnlyDictionary<string, double>, double> evaluator)
            {
                m_Evaluator = evaluator;
            }

            public int ID => 0;

            public double Evaluate(IReadOnlyDictionary<string, double> data)
            {
                return m_Evaluator(data);
            }

            public bool IsMatch(IReadOnlyDictionary<string, double> data)
            {
                return true;
            }
        }
    }
}
