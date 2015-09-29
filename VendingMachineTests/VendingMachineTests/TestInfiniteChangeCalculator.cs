using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachineTests.Greedy;

namespace VendingMachineTests
{
	/// <summary>
	/// Summary description for TestInfiniteChangeCalculator
	/// </summary>
	[TestClass]
	public class TestInfiniteChangeCalculator
	{
		[TestMethod]
		public void EightyPence()
		{
			var calculator=new InfiniteChangeCalculator();

			var coins=calculator.CalculateChange(80);
			Assert.IsNotNull(coins);
			Assert.IsTrue(coins.Count==3);

			coins=coins.Collapse().ToArray();

			Assert.IsTrue(coins[0].Coin==10 && coins[0].Quantity==1);
			Assert.IsTrue(coins[1].Coin==20 && coins[1].Quantity==1);
			Assert.IsTrue(coins[2].Coin==50 && coins[2].Quantity==1);
		}
	}
}
