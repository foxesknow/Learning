using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vending;

namespace VendingTests
{
	[TestClass]
	public class ChangeTests
	{
		[TestMethod]
		public void Construction()
		{
			Change fivePence = new Change(5, 1);
			Assert.AreEqual(fivePence.Denomination, 5);
			Assert.AreEqual(fivePence.Quantity, 1);

			// Try a "strange" denomination
			Change change = new Change(204, 2);
			Assert.AreEqual(change.Denomination, 204);
			Assert.AreEqual(change.Quantity, 2);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void Construction_InvalidDenomination()
		{
			Change change = new Change(0, 10);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void Construction_InvalidQuantity()
		{
			Change change = new Change(5, 0);
		}

		[TestMethod]
		public void TotalValue()
		{
			Change change = Change.FiftyPence(3);
			Assert.AreEqual(change.TotalValue, 150);
		}

		[TestMethod]
		public void AddChange()
		{
			Change change = Change.FiftyPence(3);			
			Change toAdd = Change.FiftyPence(1);

			Change newChange = change.Add(toAdd);

			// The original about shouldn't have changed
			Assert.AreEqual(change.Denomination, 50);
			Assert.AreEqual(change.Quantity, 3);

			// The amount to add shouldn't have changed
			Assert.AreEqual(toAdd.Denomination, 50);
			Assert.AreEqual(toAdd.Quantity, 1);

			// Finally, make sure the new change is right
			Assert.AreEqual(newChange.Denomination, 50);
			Assert.AreEqual(newChange.Quantity, 4);
		}

		[TestMethod]
		public void AddQuantity()
		{
			Change change = Change.FiftyPence(3);
			Change newChange = change.Add(1);

			// The original about shouldn't have changed
			Assert.AreEqual(change.Denomination, 50);
			Assert.AreEqual(change.Quantity, 3);

			// Finally, make sure the new change is right
			Assert.AreEqual(newChange.Denomination, 50);
			Assert.AreEqual(newChange.Quantity, 4);
		}

		[TestMethod]
		public void AddQuantity_Negative()
		{
			Change change = Change.FiftyPence(3);
			Change newChange = change.Add(-2);

			// The original about shouldn't have changed
			Assert.AreEqual(change.Denomination, 50);
			Assert.AreEqual(change.Quantity, 3);

			// Finally, make sure the new change is right
			Assert.AreEqual(newChange.Denomination, 50);
			Assert.AreEqual(newChange.Quantity, 1);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void AddQuantity_Negative_ToZeroQuantity()
		{
			Change change = Change.FiftyPence(3);
			Change newChange = change.Add(-3);
		}

		[TestMethod]
		public void FactoryMethods()
		{
			Change change = Change.OnePence(10);
			Assert.IsNotNull(change);
			Assert.AreEqual(change.TotalValue, 10);

			change = Change.TwoPence(7);
			Assert.IsNotNull(change);
			Assert.AreEqual(change.TotalValue, 14);

			change = Change.FivePence(6);
			Assert.IsNotNull(change);
			Assert.AreEqual(change.TotalValue, 30);

			change = Change.TenPence(8);
			Assert.IsNotNull(change);
			Assert.AreEqual(change.TotalValue, 80);

			change = Change.TwentyPence(9);
			Assert.IsNotNull(change);
			Assert.AreEqual(change.TotalValue, 180);

			change = Change.FiftyPence(11);
			Assert.IsNotNull(change);
			Assert.AreEqual(change.TotalValue, 550);

			change = Change.OnePound(3);
			Assert.IsNotNull(change);
			Assert.AreEqual(change.TotalValue, 300);

			change = Change.FivePound(3);
			Assert.IsNotNull(change);
			Assert.AreEqual(change.TotalValue, 1500);

			change = Change.TenPound(6);
			Assert.IsNotNull(change);
			Assert.AreEqual(change.TotalValue, 6000);

			change = Change.TwentyPound(2);
			Assert.IsNotNull(change);
			Assert.AreEqual(change.TotalValue, 4000);

			change = Change.FiftyPound(2);
			Assert.IsNotNull(change);
			Assert.AreEqual(change.TotalValue, 10000);
		}
	}
}
