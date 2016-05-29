using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vending.Greedy
{
	/// <summary>
	/// This is an implementation of a vending machine using a greedy algorithm.
	/// It works by scanning the change it holds from highest denomination to lowest and
	/// using any denomination that is less than of equal to the price supplied.
	/// 
	/// Because the algorithm takes a greedy approach to consuming change it can sometimes
	/// fail even where there is a valid change balance in the machine.
	/// For example, if the machine has 3 x 2p and 1 x 5p and someone tenders 2 x 10p to buy
	/// a 14p item we will need to give 6p in change. Although we can do this by giving 3 x 2p
	/// in change the greedy algorithm will consume the 5p and then find it doesn't have a 1p
	/// to use, and therefore fail.
	/// </summary>
	public class GreedyVendingMachine : GreedyVendingMachineBase
	{
		/// <summary>
		/// Attempts to vend an item
		/// </summary>
		/// <param name="itemPriceInUnits">The unit price of the item</param>
		/// <param name="tenderedChange">The change tendered to purchase the item</param>
		/// <returns>The success of the vend</returns>
		protected override VendingResult DoVend(int itemPriceInUnits, IEnumerable<Change> tenderedChange)
		{
			// We can use the tendered change supplied by the user to work out the change we can give.
			// For examples, for an item costing 20p a user may supply a 2p followed by a 20p.
			// If the machine was empty and we didn't consider that they tendered then we couldn't
			// give change, but if we do include it in the calculation then we can
			// NOTE: In the event we can't give change we will need to remove it

			Add(tenderedChange);

			// The caller methods in the base have validated that the tendered change is at least the price of the item
			int changeRequired = tenderedChange.TotalValue() - itemPriceInUnits;
			
			VendingResult result = Greedy(changeRequired, this.Balance);

			if(result.Success)
			{
				// We were able to give the exact change.
				// We now need to remove the coins we're going to give from the machine
				Remove(result.Change);
			}
			else
			{
				// We aren't able to give change, so remove the tendered coins 
				// from the machine so that the balance is flat
				Remove(tenderedChange);
			}

			return result;
		}
	}
}
