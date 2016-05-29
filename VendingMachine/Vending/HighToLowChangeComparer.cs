using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vending
{
	/// <summary>
	/// Compares change so that a sort places the change is descending order of denomination
	/// </summary>
	class HighToLowChangeComparer : IComparer<Change>
	{
		public static readonly IComparer<Change> Instance = new HighToLowChangeComparer();

		public int Compare(Change x, Change y)
		{
			return y.Denomination.CompareTo(x.Denomination);
		}
	}
}
