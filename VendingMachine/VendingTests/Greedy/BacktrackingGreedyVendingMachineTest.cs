using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vending;
using Vending.Greedy;

namespace VendingTests.Greedy
{
	[TestClass]
	public class BacktrackingGreedyVendingMachineTest
	{
		[TestMethod]
		public void ExactAmountTendered()
		{
			VendingMachine machine = new BacktrackingGreedyVendingMachine();
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

			// ...and the machine should have 60 more than before then vend
			Assert.AreEqual(machine.Balance.TotalValue(), 60 + totalValueMachineBeforeVend);
		}

		[TestMethod]
		public void CanGiveChange()
		{
			VendingMachine machine = new BacktrackingGreedyVendingMachine();
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
			VendingMachine machine = new BacktrackingGreedyVendingMachine();
			machine.Add
			(
				Change.TwoPence(3),
				Change.FivePence(1),
				Change.TenPence(1)
			);

			int totalValueMachineBeforeVend = machine.Balance.TotalValue();

			// The change here is 3p, which we don't have
			VendingResult result = machine.Vend(17, Change.TenPence(2));
			Assert.IsNotNull(result);
			Assert.IsTrue(result.Failed);

			// We shouldn't have any change...
			Assert.AreEqual(result.Change.TotalValue(), 0);

			// ...and the machine should have the same balance as before the vend
			Assert.AreEqual(machine.Balance.TotalValue(), totalValueMachineBeforeVend);
		}

		[TestMethod]
		public void GiveChangeAfterRecursiveAdjustment_1()
		{
			VendingMachine machine = new BacktrackingGreedyVendingMachine();
			machine.Add
			(
				Change.TwoPence(3),
				Change.FivePence(1),
				Change.TenPence(10),
				Change.FiftyPence(2)
			);

			int totalValueMachineBeforeVend = machine.Balance.TotalValue();

			// The change here is 6p and we've got the right coins, 3 x 2p
			// A pure greedy approach will fail here as it will take the 5p but not find a 1p
			// However, the backtracking greedy approach will ignore the 5p and try with the remaining coins
			VendingResult result = machine.Vend(14, Change.TenPence(2));
			Assert.IsNotNull(result);
			Assert.IsTrue(result.Success);

			// We should have 6p change...
			Assert.AreEqual(result.Change.TotalValue(), 6);

			// ...and the machine should have 14p more than before then vend
			Assert.AreEqual(machine.Balance.TotalValue(), 14 + totalValueMachineBeforeVend);
		}

		[TestMethod]
		public void GiveChangeAfterRecursiveAdjustment_2()
		{
			VendingMachine machine = new BacktrackingGreedyVendingMachine();
			machine.Add
			(
				Change.TwoPence(3),
				Change.FivePence(1),
				Change.TwentyPence(3),
				Change.FiftyPence(1)
			);

			// NOTE: The backtracking machine removes coins in its balance that
			// are greater than the change amount. In this scenerio it won't remove anything

			int totalValueMachineBeforeVend = machine.Balance.TotalValue();

			// The change here is 66p and we've got the right coins, 3 x 20p and 3 x 2p
			// A pure greedy approach will fail here as it will take the 50p but not be able to make up the 16p
			// However, the backtracking greedy approach will ignore the 50p and try with the remaining coins
			VendingResult result = machine.Vend(34, Change.OnePound(1));
			Assert.IsNotNull(result);
			Assert.IsTrue(result.Success);

			// We should have 66p change...
			Assert.AreEqual(result.Change.TotalValue(), 66);

			// ...and the machine should have 34p more than before then vend
			Assert.AreEqual(machine.Balance.TotalValue(), 34 + totalValueMachineBeforeVend);
		}
	}
}
