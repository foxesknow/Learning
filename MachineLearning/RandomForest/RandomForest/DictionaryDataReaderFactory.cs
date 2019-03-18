using System;
using System.Collections.Generic;
using System.Text;

namespace RandomForest
{
    public class DictionaryDataReaderFactory : DataReaderFactory<IReadOnlyDictionary<string, double>>
    {
        private readonly Dictionary<string, Func<IReadOnlyDictionary<string, double>, double>> m_Cache = new Dictionary<string, Func<IReadOnlyDictionary<string, double>, double>>();

        public override Func<IReadOnlyDictionary<string, double>, double> GetDataReader(string name)
        {
            if(m_Cache.TryGetValue(name, out var accessor) == false)
            {
                accessor = data =>
                {
                    // Default to zero if not found
                    if(data.TryGetValue(name, out var value) == false)
                    {
                        value = 0;
                    }
                    return value;
                };

                m_Cache.Add(name, accessor);
            }

            return accessor;
        }
    }
}
