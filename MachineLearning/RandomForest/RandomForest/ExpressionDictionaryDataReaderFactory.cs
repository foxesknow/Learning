using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace RandomForest
{
    public class ExpressionDictionaryDataReaderFactory
    {
        private readonly Dictionary<string, Expression> m_Cache = new Dictionary<string, Expression>();

        public Expression GetDataReader(string name, ParameterExpression source)
        {
            var expression = MakeGetValue(name, source);
            return expression;
        }

        private Expression MakeGetValue(string name, ParameterExpression source)
        {
            var getValue = typeof(ExpressionDictionaryDataReaderFactory).GetMethod(nameof(GetValue), BindingFlags.NonPublic | BindingFlags.Static);
            var nameExpression = Expression.Constant(name, typeof(string));
            return Expression.Call(null, getValue, nameExpression, source);
        }

        private static double GetValue(string name, IReadOnlyDictionary<string, double> data)
        {
            if(data.TryGetValue(name, out var value) == false) value = 0;
            return value;
        }
    }
}
