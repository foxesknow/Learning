using System;
using System.Collections.Generic;
using System.Text;

namespace RandomForest
{
    public class AlwaysTrueTree<TData, TScore> : Tree <TData, TScore>
    {
        private ITree<TData, TScore> m_Left;
        private ITree<TData, TScore> m_Right;

        public AlwaysTrueTree(int id, ITree<TData, TScore> left, ITree<TData, TScore> right) : base(id)
        {
            m_Left = left;
            m_Right = right;
        }

        public override bool IsMatch(TData data)
        {
            return true;
        }

        public override TScore Evaluate(TData data)
        {
            if(m_Left != null && m_Left.IsMatch(data))
            {
                return m_Left.Evaluate(data);
            }

            if(m_Right != null && m_Right.IsMatch(data))
            {
                return m_Right.Evaluate(data);
            }

            throw new Exception("no prediction");
        }
    }
}
