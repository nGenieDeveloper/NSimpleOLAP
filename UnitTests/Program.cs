/*
 * Created by SharpDevelop.
 * User: Calex
 * Date: 22-03-2012
 * Time: 14:25
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using NUnit.ConsoleRunner;
using System.Reflection;

namespace UnitTests
{
	class Program
	{
		public static void Main(string[] args)
		{
			string[] my_args = { Assembly.GetExecutingAssembly().Location };
			
			int returnCode = Runner.Main(my_args);

            if (returnCode != 0)
                Console.Beep();
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}