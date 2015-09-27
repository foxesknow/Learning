using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachineTests
{
	struct Change
	{
		private readonly int m_Coin;
		private readonly int m_Quantity;

		public Change(int coin, int quantity)
		{
			m_Coin=coin;
			m_Quantity=quantity;
		}

        public int Coin
        {
            get{return m_Coin;}
        }

        public int Quantity
        {
            get{return m_Quantity;}
        }

		public override string ToString()
		{
			if(m_Coin>=100)
			{
				int pounds=m_Coin/100;
				return string.Format("{0} x £{1}",m_Quantity,pounds);
			}
			else
			{
				return string.Format("{0} x {1}p",m_Quantity,m_Coin);
			}
		}
	}
}
