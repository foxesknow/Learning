using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachineTests.DynamicProgramming;

namespace VendingMachineTests
{
	[TestClass]
	public class TestInfiniteCombinationGenerator
	{
		[TestMethod]
		public void ZeroPence()
		{
			var generator=new InfiniteCombinationGenerator(1, 2, 3);

			int combinations=0;

			generator.Generate(0, change=>
			{
				var text=string.Join(", ", change.Collapse());
				Console.WriteLine(text);

				combinations++;
			});

			Assert.IsTrue(combinations==0);
		}

		[TestMethod]
		public void FourPence()
		{
			var generator=new InfiniteCombinationGenerator(1, 2, 3);

			int combinations=0;

			generator.Generate(4, change=>
			{
				var text=string.Join(", ", change.Collapse());
				Console.WriteLine(text);

				combinations++;
			});

			Assert.IsTrue(combinations==4);
		}

		[TestMethod]
		public void TenPence()
		{
			var generator=new InfiniteCombinationGenerator(1, 2, 3);

			int combinations=0;

			generator.Generate(10, change=>
			{
				var text=string.Join(", ", change.Collapse());
				Console.WriteLine(text);

				combinations++;
			});

			Assert.IsTrue(combinations==14);
		}

	}
}
