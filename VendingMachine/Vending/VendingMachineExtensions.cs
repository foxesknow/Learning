using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vending
{
	/// <summary>
	/// Useful vending machine extensions
	/// </summary>
	public static class VendingMachineExtensions
	{
		/// <summary>
		/// Adds a sequence to change to the machine
		/// </summary>
		/// <param name="vendingMachine">The machine to add to</param>
		/// <param name="change">The change to add</param>
		public static void Add(this VendingMachine vendingMachine, params Change[] change)
		{
			if(vendingMachine == null) throw new ArgumentNullException("vendingMachine");

			// Defer down to the IEnumerable implementation
			vendingMachine.Add((IEnumerable<Change>)change);
		}
	}
}
