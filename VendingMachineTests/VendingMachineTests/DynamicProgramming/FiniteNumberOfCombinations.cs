using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachineTests.DynamicProgramming
{
	class FiniteNumberOfCombinations : INumberOfCombinations
	{
		private readonly Change[] m_Coins;

        public FiniteNumberOfCombinations(params Change[] coins)
        {
            m_Coins=coins;
        }

        public int Calculate(int amount)
        {
            return Count(amount, m_Coins.Length-1);
        }

        private int Count(int amount, int index)
        {
            if(amount==0) return 1;

            if(amount<0 || index<0) return 0;

            // It's the sum of the solutions without the current coin : Count(amount, index - 1)
            // plus the sum of the solutions minus the current amount and including the current coin
            int count1=Count(amount, index-1);
            int count2=0;

            if(m_Coins[index].Quantity>0)
            {
                var temp=m_Coins[index];
                m_Coins[index]=new Change(temp.Coin, Math.Max(0,temp.Quantity-1));

                count2=Count(amount-m_Coins[index].Coin, index);
                m_Coins[index]=temp;
            }

            return count1+count2;
        }
	}
}
