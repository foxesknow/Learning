using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachineTests.Greedy
{
	class FiniteChangeCalculator : IChangeCalculator
    {
        private readonly Change[] m_Coins;

        public FiniteChangeCalculator(IList<Change> coins)
        {
            m_Coins=coins.OrderBy(change=>change.Coin).ToArray();
        }

        public IList<Change> CalculateChange(int amount)
        {
            var change=new List<Change>();

            int index=FindStartIndex(amount);
            while(amount!=0)
            {
                if(amount>=m_Coins[index].Coin)
                {
                    var item=m_Coins[index];

                    int coinsRequired=Math.Min(item.Quantity, amount/item.Coin);
                    amount-=(coinsRequired*item.Coin);

                    change.Add(new Change(item.Coin, coinsRequired));
                }

                index--;
            }

            return change;
        }

        private int FindStartIndex(int amount)
        {
            for(int i=0; i<m_Coins.Length; i++)
            {
                if(m_Coins[i].Coin==amount) return i;

                // If we've gone into a higher change bracket then 
                // use the previous coin as the start
                if(m_Coins[i].Coin>amount) return i-1;
            }

            // If we made it here then the amount is bigger than the largest coin
            // Therefore we can start with that one
            return m_Coins.Length-1;
        }
    }
}
