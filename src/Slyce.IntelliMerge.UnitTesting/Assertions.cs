using NUnit.Framework;

namespace Slyce.IntelliMerge.UnitTesting
{
    public static class Assertions
    {
        /// <summary>
        /// Determines if originalString contains stringToFind once and only once.
        /// </summary>
        /// <param name="originalString">The string to search in.</param>
        /// <param name="stringToFind">The string to look for.</param>
        public static void StringContains(string originalString, string stringToFind)
        {
            int index = originalString.IndexOf(stringToFind);
            if (index == -1)
                Assert.Fail("Could not find " + stringToFind + " in " + originalString);

            if(index != originalString.LastIndexOf(stringToFind))
            {
                Assert.Fail(stringToFind + " exists multiple times in " + originalString);
            }
        }

        /// <summary>
        /// Determines if originalString contains stringToFind exactly n times.
        /// </summary>
        /// <param name="originalString">The string to search in.</param>
        /// <param name="stringToFind">The string to look for.</param>
        /// <param name="numberOfTimes">The number of times the stringToFind should exist in originalString</param>
        public static void StringContains(string originalString, string stringToFind, int numberOfTimes)
        {
            StringContains(originalString, stringToFind, numberOfTimes, "");
        }

        /// <summary>
        /// Determines if originalString contains stringToFind exactly n times.
        /// </summary>
        /// <param name="originalString">The string to search in.</param>
        /// <param name="stringToFind">The string to look for.</param>
        /// <param name="numberOfTimes">The number of times the stringToFind should exist in originalString</param>
        /// <param name="message">A message to indicate which test this belongs to</param>
        public static void StringContains(string originalString, string stringToFind, int numberOfTimes, string message)
        {
            int index = originalString.IndexOf(stringToFind);

            if (numberOfTimes == 0 && index == -1)
                return;
            if(numberOfTimes != 0 && index == -1)
                Assert.Fail(message + ": " + stringToFind + " does not exist in " + originalString);
            index += stringToFind.Length;
            int counter = 0;
            while(index != -1)
            {
                counter++;
                int tempIndex = originalString.Substring(index).IndexOf(stringToFind);
                if (tempIndex == -1)
                    break;
            	
				index += tempIndex + stringToFind.Length;
            }

            if (counter != numberOfTimes)
                Assert.Fail(message + ": " + stringToFind + " exists " + counter + " times in " + originalString + ", not " + numberOfTimes);
        }
    }
}
