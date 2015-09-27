using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachineTests.DynamicProgramming
{
	class InfiniteNumberOfCombinations : INumberOfCombinations
	{
        private readonly int[] m_Coins;

        public InfiniteNumberOfCombinations(params int[] coins)
        {
            m_Coins=coins;
        }

        public int Calculate(int amount)
        {
            return Count(amount,m_Coins.Length-1);
        }

        private int Count(int amount, int index)
        {
            if(amount==0) return 1;

            if(amount<0 || index<0) return 0;

            // It's the sum of the solutions without the current coin : Count(amount, index - 1)
            // plus the sum of the solutions minus the current amount and including the current coin
            return Count(amount, index-1)+Count(amount-m_Coins[index], index);
        }
    }
}
