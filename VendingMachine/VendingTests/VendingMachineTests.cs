using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vending;
using Vending.Greedy;

namespace VendingTests
{
	/// <summary>
	/// Test for the base vending class
	/// </summary>
	[TestClass]
	public class VendingMachineTests
	{
		[TestMethod]
		public void Add()
		{
			VendingMachine machine = new GreedyVendingMachine();
			machine.Add
			(
				Change.TenPence(5),
				Change.TwentyPence(1),
				Change.OnePence(3),
				Change.FiftyPence(1),
				Change.TwoPence(3)
			);

			// The balance order should be from high to low
			CompareBalanceOrder(machine.Balance, 50, 20, 10, 2, 1);
		}

		[TestMethod]
		public void Add_Duplicates()
		{
			VendingMachine machine = new GreedyVendingMachine();
			machine.Add
			(
				Change.TenPence(5),
				Change.TwentyPence(1),
				Change.OnePence(3),
				Change.FiftyPence(1),
				Change.TwoPence(3),
				Change.TenPence(2),
				Change.TwoPence(1),
				Change.OnePence(10)
			);

			// The balance order should be from high to low
			CompareBalanceOrder(machine.Balance, 50, 20, 10, 2, 1);
		}

		[TestMethod]
		public void RemoveChangeItem()
		{
			VendingMachine machine = new GreedyVendingMachine();
			machine.Add
			(
				Change.TenPence(5),
				Change.TwentyPence(1)
			);

			int preBalance = machine.Balance.TotalValue();

			// Take out 20p and make sure the balance is right
			machine.Remove(Change.TenPence(2));
			Assert.AreEqual(machine.Balance.TotalValue(), preBalance - 20);
		}

		[TestMethod]
		public void RemoveAllChangeItem()
		{
			VendingMachine machine = new GreedyVendingMachine();
			machine.Add
			(
				Change.TenPence(5),
				Change.TwentyPence(1)
			);

			int preBalance = machine.Balance.TotalValue();

			// Take out 20p and make sure the balance is right
			machine.Remove(Change.TenPence(5));
			Assert.AreEqual(machine.Balance.TotalValue(), preBalance - 50);

			// There shouldn't be any 10p items in the machine
			Assert.IsFalse(machine.Balance.Any(c => c.Denomination == 10));
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void RemoveChangeItem_DoesNotExist()
		{
			VendingMachine machine = new GreedyVendingMachine();
			machine.Add
			(
				Change.TenPence(5),
				Change.TwentyPence(1)
			);

			// Take out 5p. This isn't in the machine, so should fail
			machine.Remove(Change.FivePence(1));			
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void RemoveChangeItem_TooManySpecified()
		{
			VendingMachine machine = new GreedyVendingMachine();
			machine.Add
			(
				Change.TenPence(5),
				Change.TwentyPence(1)
			);

			// Take out 6 x 10p. That's more than is in the machine, so should fail
			machine.Remove(Change.TenPence(6));			
		}

		[TestMethod]
		public void RemoveChangeItem_Rollback()
		{
			VendingMachine machine = new GreedyVendingMachine();
			machine.Add
			(
				Change.TenPence(5),
				Change.TwentyPence(3),
				Change.FiftyPence(1)
			);

			int preBalance = machine.Balance.TotalValue();

			Change[] changeToRemove = 
			{
				Change.FiftyPence(1), 
				Change.TwentyPence(4)
			};

			bool caughtException = false;

			try
			{
				machine.Remove(changeToRemove);
			}
			catch(ArgumentException)
			{
				caughtException = true;
			}

			Assert.IsTrue(caughtException);

			// The balance should be the same
			Assert.AreEqual(preBalance, machine.Balance.TotalValue());

			// And the coins should be the same
			Assert.AreEqual(3, machine.Balance.Count());

			Assert.IsTrue(machine.Balance.FirstOrDefault(c => c.Denomination == 10 && c.Quantity == 5) != null);
			Assert.IsTrue(machine.Balance.FirstOrDefault(c => c.Denomination == 20 && c.Quantity == 3) != null);
			Assert.IsTrue(machine.Balance.FirstOrDefault(c => c.Denomination == 50 && c.Quantity == 1) != null);
		}

		private void CompareBalanceOrder(IEnumerable<Change> change, params int[] denominations)
		{
			Assert.AreEqual(change.Count(), denominations.Length);

			int index = 0;
			foreach(Change c in change)
			{
				Assert.AreEqual(c.Denomination, denominations[index]);
				index++;
			}
		}
	}
}
