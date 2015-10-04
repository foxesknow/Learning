using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachineTests.DynamicProgramming
{
	class FiniteCombinationGenerator : ICombintationGenerator
	{
		private readonly Change[] m_Coins;

        public FiniteCombinationGenerator(params Change[] coins)
        {
            m_Coins=coins;
        }


        public void Generate(int amount, Action<IEnumerable<Change>> emit)
        {
            Recurse(amount, m_Coins.Length-1, new Stack<Change>(), emit);
        }

        private void Recurse(int amount, int index, Stack<Change> coins, Action<IEnumerable<Change>> emit)
        {
            if(amount == 0)
            {
                if(coins.Count!=0) emit(coins);
                return;
            }

            if(amount<0 || index<0) return;

            // It's the combination of solutions without the current coin : Recurse(amount, index - 1)
            // and the combination of the solutions minus the current amount and including the current coin
            Recurse(amount, index-1, coins, emit);

			if(m_Coins[index].Quantity>0)
			{
				var temp=m_Coins[index];
                m_Coins[index]=new Change(temp.Coin,Math.Max(0, temp.Quantity-1));

				coins.Push(new Change(m_Coins[index].Coin, 1));
				Recurse(amount-m_Coins[index].Coin, index, coins, emit);
				
				m_Coins[index]=temp;
				coins.Pop();
			}
        }
	}
}
