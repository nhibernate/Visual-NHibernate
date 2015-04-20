using System;
using System.Diagnostics;
using System.IO;

namespace Provider.Test
{
	public class Tester
	{

		/// <summary>
		/// Runs the test.
		/// </summary>
		/// <param name="consoleMessage">The console message.</param>
		/// <param name="Test">The test.</param>
		/// <param name="outputFile">The output file.</param>
		/// <param name="numTests">The num tests.</param>
		/// <param name="stepSize">Size of the step.</param>
		public void RunTest(string consoleMessage, Func<int, TimeSpan> Test, string outputFile, int numTests, int stepSize)
		{
			// Warm up
			Test(stepSize);

			if(outputFile == null) throw new ArgumentNullException("outputFile");
			
			DirectoryInfo parentDir = Directory.GetParent(outputFile);
			if(parentDir == null) throw new ArgumentException("outputFile is not a valid path");

			Directory.CreateDirectory(parentDir.FullName);

			using (var stream = File.CreateText(outputFile))
			{
				for (int i = 1; i < numTests; i++)
				{
					int numEntities = i * stepSize;
					TimeSpan timeTaken = Test(numEntities);

					Console.WriteLine(consoleMessage, timeTaken.TotalMilliseconds, numEntities);

					stream.WriteLine(string.Format("{0}, {1}", numEntities, timeTaken.TotalMilliseconds));
					stream.Flush();
				}
			}
			//Process.Start(outputFile);
		}

		public void RunTest(string consoleMessage, Func<TimeSpan> Test, string outputFile)
		{
			// Warm up
			Test();

			using (var stream = File.CreateText(outputFile))
			{
				stream.WriteLine("Test Number, Milliseconds Taken");
				for (int i = 0; i < 20; i++)
				{
					TimeSpan timeTaken = Test();

					Console.WriteLine(consoleMessage, timeTaken.TotalMilliseconds);

					stream.WriteLine(string.Format("{0}, {1}", i, timeTaken.TotalMilliseconds));
					stream.Flush();
				}
			}
			//Process.Start(outputFile);
		}
	}
}
