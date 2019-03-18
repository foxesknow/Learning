using System;
using System.Collections.Generic;
using System.Text;

namespace RandomForest
{
    public interface IRandomForest<in TData, TScore>
    {
        TScore Predict(TData data);
    }
}
