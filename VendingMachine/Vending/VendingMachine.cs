using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vending
{
	/// <summary>
	/// Base class for vending machine.
	/// This class manages the coins in the vending machine, but does not do any vending
	/// </summary>
	public abstract class VendingMachine 
	{
		private readonly List<Change> m_Change = new List<Change>();

		protected VendingMachine()
		{
		}

		/// <summary>
		/// Vends an item
		/// </summary>
		/// <param name="itemPriceInUnits">The price of the item</param>
		/// <param name="tenderedChange">The change tendered to purchase the item</param>
		/// <returns>The outcome of the vending action</returns>
		public VendingResult Vend(int itemPriceInUnits, IEnumerable<Change> tenderedChange)
		{			
			ValidateVendData(itemPriceInUnits, tenderedChange);
			
			return DoVend(itemPriceInUnits, tenderedChange);
		}

		/// <summary>
		/// Vends an item
		/// </summary>
		/// <param name="itemPriceInUnits">The price of the item</param>
		/// <param name="tenderedChange">The change tendered to purchase the item</param>
		/// <returns>The outcome of the vending action</returns>
		public VendingResult Vend(int itemPriceInUnits, params Change[] tenderedChange)
		{
			ValidateVendData(itemPriceInUnits, tenderedChange);
			
			return DoVend(itemPriceInUnits, tenderedChange);
		}

		/// <summary>
		/// Validates the supplied vending parameters to save each base class having to do it
		/// </summary>
		/// <param name="itemPriceInUnits">The price of the item</param>
		/// <param name="tenderedChange">The change tendered to purchase the item</param>
		private void ValidateVendData(int itemPriceInUnits, IEnumerable<Change> tenderedChange)
		{
			// We need to be buying something that cost something...
			if(itemPriceInUnits < 1) throw new ArgumentException("invalid price", "itemPriceInUnits");
			
			// The caller needs to have supplied some money to buy the items
			if(tenderedChange == null) throw new ArgumentNullException("tenderedChange");

			// Make sure there are no nulls in the tendered change
			bool hasNullChange = tenderedChange.Contains(null);
			if(hasNullChange) throw new ArgumentException("there are null items in the tendered change", "tenderedChange");

			// Make sure the change is at least the value of the item
			int totalTenderedAmount = tenderedChange.TotalValue();
			if(totalTenderedAmount < itemPriceInUnits) throw new ArgumentException("not enough change tendered", "tenderedChange");
		}

		/// <summary>
		/// Attempts to vend an item
		/// </summary>
		/// <param name="itemPriceInUnits">The unit price of the item</param>
		/// <param name="tenderedChange">The change tendered to purchase the item</param>
		/// <returns>The success of the vend</returns>
		protected abstract VendingResult DoVend(int itemPriceInUnits, IEnumerable<Change> tenderedChange);

		/// <summary>
		/// Adds change to the balance of the vending machine
		/// </summary>
		/// <param name="change">The change to add</param>
		public void Add(Change change)
		{
			if(change == null) throw new ArgumentNullException("change");

			// We order the change list from highest denomination to lowest.
			// This will simplify certain change algorithms (like greedy)
			// where we go from highest to lowest

			int index = m_Change.BinarySearch(change, HighToLowChangeComparer.Instance);
			if(index < 0)
			{
				// We've not seen this denomination before so insert it
				m_Change.Insert(~index, change);
			}
			else
			{
				// Just update the existing item
				m_Change[index] = m_Change[index].Add(change);
			}
		}

		/// <summary>
		/// Adds change to the balance of the vending machine
		/// </summary>
		/// <param name="change">The change to add</param>
		public void Add(IEnumerable<Change> change)
		{
			if(change == null) throw new ArgumentNullException("change");
			
			// Make there no invalid change in the sequence, so that it won't fail part way through adding
			if(change.Any(c => c == null)) throw new ArgumentException("null change in sequence", "change");

			foreach(Change c in change)
			{
				Add(c);
			}
		}

		/// <summary>
		/// Removes change from the balance of the vending machine
		/// </summary>
		/// <param name="change">The change to add</param>
		public void Remove(Change change)
		{
			if(change == null) throw new ArgumentNullException("change");

			int index = m_Change.FindIndex(c => c.Denomination == change.Denomination);

			if(index == -1)
			{
				// If we don't have the specified denomination then flag is as an error
				throw new ArgumentException("the vending machine is not holding the specified denomination", "change");
			}

			Change vendingChange = m_Change[index];

			// If we've been asked to remove more of the denomintion than we've got then it's an error
			if(change.Quantity > vendingChange.Quantity)
			{
				throw new ArgumentException("the vending machine is not holding enough of that denomination", "change");
			}

			int newQuantity = vendingChange.Quantity - change.Quantity;
			
			if(newQuantity == 0)
			{
				// We've got no more instances of the denomination, so remove it
				m_Change.RemoveAt(index);
			}
			else
			{
				// We've still got some left, so update it be removing the supplied quantity
				m_Change[index] = vendingChange.Add(-change.Quantity);
			}
		}

		/// <summary>
		/// Removes change from the balance of the vending machine
		/// </summary>
		/// <param name="change">The change to add</param>
		public void Remove(IEnumerable<Change> change)
		{
			if(change == null) throw new ArgumentNullException("change");

			// Take a copy of the existing change state so that if it fails we can roll back
			List<Change> preRemoveState = new List<Change>(m_Change);

			try
			{
				foreach(Change c in change)
				{
					Remove(c);
				}
			}
			catch
			{
				// Something went wrong removing an individual change item, so roll back
				m_Change.Clear();
				m_Change.AddRange(preRemoveState);

				throw;
			}
		}

		/// <summary>
		/// The balance of change in the machine
		/// </summary>
		public IEnumerable<Change> Balance
		{
			get{return m_Change;}
		}
	}
}
