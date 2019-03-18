using System;
using System.Collections.Generic;
using System.Text;

namespace RandomForest
{
    public abstract class Tree<TData, TScore> : ITree<TData, TScore>
    {
        protected Tree(int id)
        {
            this.ID = id;
        }

        public int ID{get;}

        public abstract TScore Evaluate(TData data);

        public abstract bool IsMatch(TData data);

        public override string ToString()
        {
            return this.ID.ToString();
        }
    }
}
