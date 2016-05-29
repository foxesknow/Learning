using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vending
{
    /// <summary>
    /// Represents some change
    /// </summary>
	public sealed class Change
    {
		public readonly int m_Denomination;
		public readonly int m_Quantity;

		/// <summary>
		/// Initializes the instance.
		/// </summary>
		/// <param name="denomination">The denomination of the change in base units (eg pence)</param>
		/// <param name="quantity">The quantity of the denominatin</param>
		public Change(int denomination, int quantity)
		{
			if(denomination < 1) throw new ArgumentException("denomination must be at least 1", "denomination");
			if(quantity < 1) throw new ArgumentException("quantity must be at least 1", "quantity");

			m_Denomination = denomination;
			m_Quantity = quantity;
		}

		/// <summary>
		/// The denomination of the currency in it's base units (eg pence, cent etc)
		/// </summary>
		public int Denomination
		{
			get{return m_Denomination;}
		}

		/// <summary>
		/// The number coins in the change
		/// </summary>
		public int Quantity
		{
			get{return m_Quantity;}
		}

		/// <summary>
		/// The total value of the change
		/// </summary>
		public int TotalValue
		{
			get{return m_Denomination * m_Quantity;}
		}

		/// <summary>
		/// Creates a new change instance which is the sum 
		/// of this quanty and the supplied quantiy
		/// </summary>
		/// <param name="change">The change to add</param>
		/// <returns>A new change object</returns>
		public Change Add(Change change)
		{
			if(change == null) throw new ArgumentNullException("change");
			if(change.Denomination != m_Denomination) throw new ArgumentException("change has different denomination", "change");

			return new Change(m_Denomination, m_Quantity + change.Quantity);
		}

		/// <summary>
		/// Creates a new change instance by adding the supplied quantity.
		/// To reduce the quantity pass a negative number
		/// </summary>
		/// <param name="change">The change to add</param>
		/// <returns>A new change object</returns>
		public Change Add(int quantity)
		{
			int newQuantity = m_Quantity + quantity;
			if(newQuantity < 1)
			{
				throw new InvalidOperationException("new quantity would be less than 1");
			}

			return new Change(m_Denomination, newQuantity);
		}

		/// <summary>
		/// Renders the change as a string
		/// </summary>
		/// <returns>A string representation of the change</returns>
		public override string ToString()
		{
			// The test document mentions pence, so assuming pence/pounds 
			// for amount to create something human readable

			if(m_Denomination < 100)
			{
				return string.Format("{0} x {1}p", m_Quantity, m_Denomination);
			}
			else
			{
				// We'll allow for the fact that someone may have created
				// change with a denomination like 109, for example
				int pounds = m_Denomination / 100;
				int pence = m_Denomination % 100;

				if(pence == 0)
				{
					return string.Format("{0} x £{1}", m_Quantity, pounds);
				}
				else
				{
					return string.Format("{0} x £{1}.{2:00}p", m_Quantity, pounds, pence);
				}
			}
		}

		/// <summary>
		/// Factory method to create 1p change
		/// </summary>
		/// <param name="quantity">The number of coins</param>
		/// <returns>The change</returns>
		public static Change OnePence(int quantity)
		{
			return new Change(1, quantity);
		}

		/// <summary>
		/// Factory method to create 2p change
		/// </summary>
		/// <param name="quantity">The number of coins</param>
		/// <returns>The change</returns>
		public static Change TwoPence(int quantity)
		{
			return new Change(2, quantity);
		}

		/// <summary>
		/// Factory method to create 5p change
		/// </summary>
		/// <param name="quantity">The number of coins</param>
		/// <returns>The change</returns>
		public static Change FivePence(int quantity)
		{
			return new Change(5, quantity);
		}

		/// <summary>
		/// Factory method to create 10p change
		/// </summary>
		/// <param name="quantity">The number of coins</param>
		/// <returns>The change</returns>
		public static Change TenPence(int quantity)
		{
			return new Change(10, quantity);
		}

		/// <summary>
		/// Factory method to create 20p change
		/// </summary>
		/// <param name="quantity">The number of coins</param>
		/// <returns>The change</returns>
		public static Change TwentyPence(int quantity)
		{
			return new Change(20, quantity);
		}

		/// <summary>
		/// Factory method to create 50p change
		/// </summary>
		/// <param name="quantity">The number of coins</param>
		/// <returns>The change</returns>
		public static Change FiftyPence(int quantity)
		{
			return new Change(50, quantity);
		}

		/// <summary>
		/// Factory method to create £1 change
		/// </summary>
		/// <param name="quantity">The number of coins</param>
		/// <returns>The change</returns>
		public static Change OnePound(int quantity)
		{
			return new Change(100, quantity);
		}

		/// <summary>
		/// Factory method to create £5 change
		/// </summary>
		/// <param name="quantity">The number of notes</param>
		/// <returns>The change</returns>
		public static Change FivePound(int quantity)
		{
			return new Change(500, quantity);
		}

		/// <summary>
		/// Factory method to create £10 change
		/// </summary>
		/// <param name="quantity">The number of notes</param>
		/// <returns>The change</returns>
		public static Change TenPound(int quantity)
		{
			return new Change(1000, quantity);
		}

		/// <summary>
		/// Factory method to create £20 change
		/// </summary>
		/// <param name="quantity">The number of notes</param>
		/// <returns>The change</returns>
		public static Change TwentyPound(int quantity)
		{
			return new Change(2000, quantity);
		}

		/// <summary>
		/// Factory method to create £50 change
		/// </summary>
		/// <param name="quantity">The number of notes</param>
		/// <returns>The change</returns>
		public static Change FiftyPound(int quantity)
		{
			return new Change(5000, quantity);
		}
    }
}
