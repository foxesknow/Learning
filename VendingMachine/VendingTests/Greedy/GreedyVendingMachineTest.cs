using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vending;
using Vending.Greedy;

namespace VendingTests.Greedy
{
	[TestClass]
	public class GreedyVendingMachineTest
	{
		[TestMethod]
		public void ExactAmountTendered()
		{
			VendingMachine machine = new GreedyVendingMachine();
			machine.Add
			(
				Change.OnePence(100),
				Change.FivePence(50),
				Change.TenPence(50)
			);

			int totalValueMachineBeforeVend = machine.Balance.TotalValue();

			VendingResult result = machine.Vend(60, Change.FiftyPence(1), Change.TenPence(1));
			Assert.IsNotNull(result);
			Assert.IsTrue(result.Success);
			Assert.IsNotNull(result.Change);

			// We should have no change...
			Assert.AreEqual(result.Change.TotalValue(), 0);

			// ...and the machine should have 60p more than before then vend
			Assert.AreEqual(machine.Balance.TotalValue(), 60 + totalValueMachineBeforeVend);
		}

		[TestMethod]
		public void CanGiveChange()
		{
			VendingMachine machine = new GreedyVendingMachine();
			machine.Add
			(
				Change.OnePence(100),
				Change.FivePence(50),
				Change.TenPence(50)
			);

			int totalValueMachineBeforeVend = machine.Balance.TotalValue();

			VendingResult result = machine.Vend(25, Change.TwentyPence(2));
			Assert.IsNotNull(result);
			Assert.IsTrue(result.Success);
			Assert.IsNotNull(result.Change);

			// We should have 15p in change...
			Assert.AreEqual(result.Change.TotalValue(), 15);

			// ...and the machine should have 25p more than before then vend
			Assert.AreEqual(machine.Balance.TotalValue(), 25 + totalValueMachineBeforeVend);
		}

		[TestMethod]
		public void CannotGiveChange()
		{
			VendingMachine machine = new GreedyVendingMachine();
			machine.Add
			(
				Change.TwoPence(3),
				Change.FivePence(1)
			);

			int totalValueMachineBeforeVend = machine.Balance.TotalValue();

			// The change here is 6p.
			// We've got the right coins, 3 x 2p, but the gready algorithm
			// will take the 5p and then not be able to find a 1p
			VendingResult result = machine.Vend(14, Change.TenPence(2));
			Assert.IsNotNull(result);
			Assert.IsTrue(result.Failed);

			// We shouldn't have any change...
			Assert.AreEqual(result.Change.TotalValue(), 0);

			// ...and the machine should have the same balance as before the vend
			Assert.AreEqual(machine.Balance.TotalValue(), totalValueMachineBeforeVend);
		}

		[TestMethod]
		public void CannotGiveChange_2()
		{
			VendingMachine machine = new GreedyVendingMachine();
			machine.Add
			(
				Change.TwoPence(3),
				Change.FivePence(1),
				Change.TwentyPence(3),
				Change.FiftyPence(1)
			);

			int totalValueMachineBeforeVend = machine.Balance.TotalValue();

			// The change here is 66p and we've got the right coins, 3 x 20p and 3 x 2p
			// However, the greedy approach will fail here as it will take the 50p but not be able to make up the 16p
			VendingResult result = machine.Vend(34, Change.OnePound(1));
			Assert.IsNotNull(result);
			Assert.IsTrue(result.Failed);

			// We should have 6p change...
			Assert.AreEqual(result.Change.TotalValue(), 0);

			// ...and the machine should have 14p more than before then vend
			Assert.AreEqual(machine.Balance.TotalValue(), totalValueMachineBeforeVend);
		}
	}
}
