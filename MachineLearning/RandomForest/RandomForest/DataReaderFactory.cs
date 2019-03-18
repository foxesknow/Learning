using System;
using System.Collections.Generic;
using System.Text;

namespace RandomForest
{
    public abstract class DataReaderFactory<TData>
    {
        public abstract Func<TData, double> GetDataReader(string name);
    }
}
