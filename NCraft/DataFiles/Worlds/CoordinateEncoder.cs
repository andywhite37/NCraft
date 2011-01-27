using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NCraft.DataFiles.Worlds
{
    public class CoordinateEncoder
    {
        private static string BASE_36_CHARS = "0123456789abcdefghijklmnopqrstuvwxyz";

        public static int GetCoordinateFromDirectoryName(string name)
        {
            // Not sure if this is possible with the mod 63 in the reverse conversion 
            throw new NotSupportedException();

            //var result = ToBase10(name);

            //return result;
        }

        public static string GetDirectoryNameFromCoordinate(int coordinate)
        {
            coordinate &= 63;

            return ToBase36(coordinate);
        }

        private static int ToBase10(string base36)
        {
            var isNegative = false;
            if (base36[0] == '-')
            {
                isNegative = true;
                base36 = base36.Substring(1);
            }

            var reversed = base36.Reverse().ToArray();

            int result = 0;

            for (int i = 0; i < reversed.Count(); ++i)
            {
                result += BASE_36_CHARS.IndexOf(reversed[i]) * (int)Math.Pow(36, i);
            }

            if (isNegative)
            {
                result *= -1;
            }

            return result;
        }

        private static string ToBase36(int base10)
        {
            if (base10 == 0)
            {
                return "0";
            }

            var isNegative = false;
            if (base10 < 0)
            {
                isNegative = true;
                base10 *= -1;
            }

            var sb = new StringBuilder();

            while (base10 != 0)
            {
                var base10Digit = base10 % 36;
                var base36Digit = BASE_36_CHARS[base10Digit];
                sb.Append(base36Digit);
                base10 /= 36;
            }

            var base36Reversed = sb.ToString();

            var result = new string(base36Reversed.Reverse().ToArray());

            if (isNegative)
            {
                result = result.Insert(0, "-");
            }

            return result;
        }
    }
}
