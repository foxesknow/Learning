using System;
using System.Collections.Generic;
using System.Text;

namespace RandomForest
{
    public interface ITree<in TData, TScore>
    {
        int ID{get;}
        bool IsMatch(TData data);
        TScore Evaluate(TData data);
    }
}
