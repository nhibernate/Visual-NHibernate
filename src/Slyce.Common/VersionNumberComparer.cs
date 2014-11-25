using System;
using System.Collections.Generic;
using System.Text;

namespace Slyce.Common
{
    public class VersionNumberUtility
    {
        /// <summary>
        /// Increments the last ordinal version part ie: the right-most value.
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public static string Increment(string version)
        {
            return Increment(version, version.Split('.').Length - 1);
        }

        /// <summary>
        /// Increments the value of the specified version part.
        /// </summary>
        /// <param name="version"></param>
        /// <param name="ordinalPosition"></param>
        /// <returns></returns>
        public static string Increment(string version, int ordinalPosition)
        {
            string[] parts = version.Split('.');

            if (ordinalPosition > parts.Length)
            {
                throw new InvalidOperationException("ordinalPosition is greater than the number of parts in the version string.");
            }
        	if (ordinalPosition == parts.Length)
        	{
        		return version + ".1";
        	}
        	StringBuilder sb = new StringBuilder();

        	for (int i = 0; i < parts.Length; i++)
        	{
        		if (i > 0)
        		{
        			sb.Append(".");
        		}
        		if (i == ordinalPosition)
        		{
        			int testVal;

        			if (!int.TryParse(parts[ordinalPosition], out testVal))
        			{
        				throw new InvalidOperationException("Invalid version string. Can only be integers and periods.");
        			}
        			testVal++;
        			sb.Append(testVal);
        		}
        		else
        		{
        			sb.Append(parts[i]);
        		}
        	}
        	return sb.ToString();
        }

        public static bool IsValidVersionNumber(string text)
        {
            string[] parts = text.Split('.');
            int testVal;

            foreach (string part in parts)
            {
                if (!int.TryParse(part, out testVal))
                {
                    return false;
                }
            }
            return true;
        }

        public class VersionNumberComparer : IComparer<string>
        {
            public int Compare(string versionLeft, string versionRight)
            {
                string[] partsLeft = versionLeft.Split('.');
                string[] partsRight = versionRight.Split('.');
                int index = 0;

                while (index < partsLeft.Length && index < partsRight.Length)
                {
                    int leftVal = int.Parse(partsLeft[index]);
                    int rightVal = int.Parse(partsRight[index]);

                    if (leftVal.CompareTo(rightVal) != 0)
                    {
                        return leftVal.CompareTo(rightVal);
                    }
                    index++;
                }
                if (partsLeft.Length == partsRight.Length)
                {
                    return 0;
                }
            	// Thye both have the same numbers up till this point, so the one with the
            	// greater precision must be the latest.
            	return partsLeft.Length.CompareTo(partsRight.Length);
            }
        }
    }
}
