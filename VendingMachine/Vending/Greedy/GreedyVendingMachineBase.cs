using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vending.Greedy
{
	/// <summary>
	/// This is the base class for all greedy algorithm approaches to working out change
	/// </summary>
	public abstract class GreedyVendingMachineBase : VendingMachine
	{
		/// <summary>
		/// Performs a greedy algorithm change algorithm over the available coins.
		/// NOTE: It is valid for the changeAvailable sequence to have null values to
		/// indicate the absence of change. This simplifies the backtracking algorithm
		/// </summary>
		/// <param name="changeRequired">The change we need to give</param>
		/// <param name="changeAvailable">The change available for the algorithm</param>
		/// <returns>The outcome of the vending action</returns>
		protected VendingResult Greedy(int changeRequired, IEnumerable<Change> changeAvailable)
		{
			if(changeRequired < 0) throw new ArgumentException("change required must be at least zero", "changeRequired");
			if(changeAvailable == null) throw new ArgumentNullException("changeAvailable");

			int outstandingChangeRequired = changeRequired;
			List<Change> changeToGive = new List<Change>();

			if(outstandingChangeRequired != 0)
			{
				foreach(Change change in changeAvailable)
				{
					if(change == null) continue;

					if(change.Denomination <= outstandingChangeRequired)
					{
						// We can use this denomination, work out how many we need
						int quantityNeeded = Math.Min(change.Quantity, outstandingChangeRequired / change.Denomination);

						// NOTE: Don't remove the change yet as this will invalidate the foreach enumeration
						Change changeToReturn = new Change(change.Denomination, quantityNeeded);
						changeToGive.Add(changeToReturn);

						outstandingChangeRequired -= changeToReturn.TotalValue;

						// Finished..?
						if(outstandingChangeRequired == 0)
						{
							break;
						}
					}
				}
			}

			if(outstandingChangeRequired == 0)
			{
				return VendingResult.CreateSuccess(changeToGive);
			}
			else
			{
				return VendingResult.CreateFailed();
			}
		}
	}
}
