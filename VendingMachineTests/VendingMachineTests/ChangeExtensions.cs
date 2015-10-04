using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachineTests
{
	static class ChangeExtensions
	{
		public static IEnumerable<Change> Collapse(this IEnumerable<Change> change)
		{			
            return change.GroupBy(c=>c.Coin)
                    .Select(group=>new Change(group.Key, group.Sum(c=>c.Quantity)))
					.OrderBy(item=>item.Coin);
		}

		public static int AmountInPence(this IEnumerable<Change> change)
		{
			int sum=change.Sum(c=>c.Coin*c.Quantity);
			return sum;
		}

		public static int NumberOfCoins(this IEnumerable<Change> change)
		{
			int count=change.Sum(c=>c.Quantity);
			return count;
		}
	}
}
