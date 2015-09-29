using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachineTests.DynamicProgramming
{
	class InfiniteCombinationGenerator : ICombintationGenerator
	{
		private readonly int[] m_Coins;

        public InfiniteCombinationGenerator(params int[] coins)
        {
            m_Coins=coins;
        }


        public void Generate(int amount, Action<IEnumerable<Change>> emit)
        {
            Recurse(amount, m_Coins.Length-1, new Stack<Change>(),emit);
        }

        private void Recurse(int amount, int index, Stack<Change> coins, Action<IEnumerable<Change>> emit)
        {
            if(amount==0)
            {
                if(coins.Count!=0) emit(coins);
                return;
            }

            if(amount<0 || index < 0) return;

            // It's the combination of solutions without the current coin : Recurse(amount, index - 1)
            // and the combination of the solutions minus the current amount and including the current coin
            Recurse(amount,index-1,coins,emit);

            coins.Push(new Change(m_Coins[index], 1));
            Recurse(amount-m_Coins[index], index, coins, emit);
            coins.Pop();
        }
	}
}
