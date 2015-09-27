using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachineTests
{
	interface ICombintationGenerator
	{
		void Generate(int amount, Action<IEnumerable<Change>> emit);
	}
}
