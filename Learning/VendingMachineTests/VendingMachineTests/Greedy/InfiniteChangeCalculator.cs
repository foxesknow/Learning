using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachineTests.Greedy
{
	class InfiniteChangeCalculator : IChangeCalculator
	{
		private readonly int[] m_Coins={1,2,5,10,20,50,100};

		public IList<Change> CalculateChange(int amount)
		{
			var change=new List<Change>();

            int index=FindStartIndex(amount);
            while(amount!=0)
            {
                if(amount>=m_Coins[index])
                {
                    int coinsRequired=amount/m_Coins[index];
                    amount-=(coinsRequired*m_Coins[index]);

                    change.Add(new Change(m_Coins[index], coinsRequired));
                }

                index--;
            }

			return change;
		}

        private int FindStartIndex(int amount)
		{
			for(int i=0; i<m_Coins.Length; i++)
			{
				if(m_Coins[i]==amount) return i;

				// If we've gone into a higher change bracket then 
				// use the previous coin as the start
				if(m_Coins[i]>amount) return i-1;
			}

			// If we made it here then the amount is bigger than the largest coin
			// Therefore we can start with that one
			return m_Coins.Length-1;
		}
	}
}
