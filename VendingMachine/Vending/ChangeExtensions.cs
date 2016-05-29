using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vending
{
	/// <summary>
	/// Useful extension methods for change
	/// </summary>
	public static class ChangeExtensions
	{
		/// <summary>
		/// Calculate the total value of a sequence of change
		/// </summary>
		/// <param name="change">The change to sum up</param>
		/// <returns>The total value of the change</returns>
		public static int TotalValue(this IEnumerable<Change> change)
		{
			if(change == null) throw new ArgumentNullException("change");
			
			return change.Sum(c => c.TotalValue);
		}
	}
}
