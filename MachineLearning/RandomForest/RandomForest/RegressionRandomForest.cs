using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace RandomForest
{
    public class RegressionRandomForest<TData> : IRandomForest<TData, double>
    {
        private IReadOnlyList<ITree<TData, double>> m_Trees;

        public RegressionRandomForest(IReadOnlyList<ITree<TData, double>> trees)
        {
            m_Trees = trees;
        }

        public double Predict(TData data)
        {
            double prediction = 0;
            var predictionLock = new object();

            for(int i = 0; i < m_Trees.Count; i++)
            {
                var currentTree = m_Trees[i];
                var p = currentTree.Evaluate(data);
                lock (predictionLock)
                {
                    prediction += p;
                }
            }

            return prediction / m_Trees.Count;
        }
    }
}
