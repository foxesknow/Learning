using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachineTests.DynamicProgramming;
using VendingMachineTests.Greedy;

namespace VendingMachineTests
{
	class Program
	{
		public static void Main(string[] args)
		{
			InfiniteCombinationGenerator_CalcChange();
		}

		private static void FiniteCombinationGenerator_CalcChange()
        {
            var generator=new FiniteCombinationGenerator
            (
                new Change[]
                {
                    new Change(50,1),
                    new Change(20,3),
                    new Change(15,1),
                    new Change(1,8),
                }
            );
            generator.Generate(60,change=>
			{
				var text=string.Join(", ", change.Collapse());
				Console.WriteLine("{0} coins => {1}",change.NumberOfCoins(),text);
			});
        }

		private static void InfiniteCombinationGenerator_CalcChange()
        {
            var generator=new InfiniteCombinationGenerator(1,2,5,10,20,50,100);

            generator.Generate(60,change=>
			{
				var text=string.Join(", ", change.Collapse());
				Console.WriteLine("{0} coins => {1}",change.NumberOfCoins(),text);
			});
        }

		private static void FiniteChangeCalculator_CalcChange()
        {
            var calculator=new FiniteChangeCalculator
            (
                new Change[]
                {
                    new Change(50,1),
                    new Change(20,3),
                    new Change(15,1),
                    new Change(1,8),
                }
            );
            var change=calculator.CalculateChange(60);

            var text=string.Join(", ", change.Collapse());
			Console.WriteLine("{0} coins => {1}",change.NumberOfCoins(),text);
        }
	}
}
