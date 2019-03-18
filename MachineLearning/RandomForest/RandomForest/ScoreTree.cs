using System;
using System.Collections.Generic;
using System.Text;

namespace RandomForest
{
    public class ScoreTree<TData, TScore> : Tree<TData, TScore>
    {
        private readonly TScore m_Score;
        private readonly Func<TData, bool> m_Predicate;

        public ScoreTree(int id, TScore score, Func<TData, bool> predicate) : base(id)
        {
            m_Score = score;
            m_Predicate = predicate;
        }

        public override bool IsMatch(TData data)
        {
            return m_Predicate(data);
        }

        public override TScore Evaluate(TData data)
        {
            return m_Score;
        }
    }
}
