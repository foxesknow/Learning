using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachineTests
{
	interface IChangeCalculator
	{
		IList<Change> CalculateChange(int amount);
	}
}
