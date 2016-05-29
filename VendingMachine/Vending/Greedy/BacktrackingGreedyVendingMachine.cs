using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vending.Greedy
{
	/// <summary>
	/// This is an implementation of a vending machine using a recursive backtracking greedy algorithm.
	/// It works by scanning the change it holds from highest denomination to lowest and
	/// using any denomination that is less than of equal to the price supplied. In the event that
	/// it can't find a match is alters the quantity of coins and tries again until it either
	/// finds a match or exists all combinations
	/// 
	/// For example, if the machine has 3 x 2p and 1 x 5p and someone tenders 2 x 10p to buy
	/// a 14p item we will need to give 6p in change. Initially it will try to do this by
	/// taking the 5p and then failing to get a 1p. Rather than fail it will reduce the number of
	/// 5p by 1 and then try again. Because there are no longer any 5p coins it will just consider
	/// the 2p coins and succeed.
	/// 
	/// The performance of this approach can be quite high as the number of attempts it has to make
	/// is the product of the change quantities within the machine. However, it will return a successful
	/// solution in places where the regular greedy algorithm will not
	/// </summary>
	public class BacktrackingGreedyVendingMachine : GreedyVendingMachineBase
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

			List<Change> changeToGive = new List<Change>();

			// The caller methods in the base have validated that the tendered change is at least the price of the item
			int outstandingChange = tenderedChange.TotalValue() - itemPriceInUnits;

			// The recursive algorithm is more performance intensive that a regular greedy
			// algorithm as it considers all possibilities. We can simplify this a bit
			// by only using change which the denomination is no bigger than the amount we need to give,
			// as anything bigger will be ignored
			List<Change> changeAvailable = this.Balance.Where(c => (c.Denomination <= outstandingChange)).ToList();
			
			// Kick off the recursion
			VendingResult result = RecursiveVend(outstandingChange, 0, changeAvailable);

			if(result.Success)
			{
				// We were able to give the exact change.
				// We now need to remove the coins we're going to give from the machine
				Remove(result.Change);
			}
			else
			{
				// We aren't able to give change, so remove the tendered change from the machine
				Remove(tenderedChange);
			}

			return result;
		}

		private VendingResult RecursiveVend(int changeRequired, int changeIndex, List<Change> changeAvailable)
		{
			// This is the recursion termination condition.
			// Once we get here we can do a greedy vend on the change
			if(changeIndex == changeAvailable.Count)
			{
				return Greedy(changeRequired, changeAvailable);
			}

			VendingResult result = null;

			Change change = changeAvailable[changeIndex];

			for(int quantityToRemove = 0; quantityToRemove <= change.Quantity; quantityToRemove++) // NOTE: <= is correct
			{
				int newQuantity = change.Quantity - quantityToRemove;

				// If we've reached the point where we're going to completly ignore
				// the change then set it to null so that the list size stays the same
				// and the indicies remain consistant. It also avoids resizing the list
				if(newQuantity == 0)
				{
					changeAvailable[changeIndex] = null;
				}
				else
				{
					changeAvailable[changeIndex] = change.Add(-quantityToRemove);
				}

				// Now recurse...
				result = RecursiveVend(changeRequired, changeIndex + 1, changeAvailable);
				if(result.Success)
				{
					// We've matched!
					break;
				}
			}

			// Finally, put the original change back for subsequent recursion
			changeAvailable[changeIndex] = change;

			return result == null ? VendingResult.CreateFailed() : result;
		}
	}
}
