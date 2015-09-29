using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachineTests.Greedy;

namespace VendingMachineTests
{
	[TestClass]
	public class TestFiniteChangeCalculator
	{
		[TestMethod]
		public void NoChangeForSixtyPence()
		{
			var calculator=new FiniteChangeCalculator
			(
				new Change(20,3), 
				new Change(50,1)
			);

			var coins=calculator.CalculateChange(60);
			Assert.IsNotNull(coins);

			// Because the greedy algorithm works from the largest coin
			// down it will consume the 50p but fail to find anything to make
			// 60p. Because it's greedy it will notdiscard the 50p and try the 3 20p coins
			Assert.IsTrue(coins.AmountInPence()!=60);
			Assert.IsTrue(coins.AmountInPence()==50);
		}
	}
}
