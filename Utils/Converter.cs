using System;
using System.Text;

namespace Hex_Editor
{
    internal class Converter
    {
        private static int HexToAsciiPosition(string hex)
        {
            if (hex.Length > 2)
            {
                throw new NotSupportedException("Length of hex representation of single ascii char cannot exceed 2");
            }
            int res = int.Parse(hex, System.Globalization.NumberStyles.HexNumber);
            if ((res >= 0 && res <= 31) || (res >= 127 && res <= 129) || (res >= 141 && res <= 144) || (res >= 157 && res <= 158) || res == 173)
                res = 46;
            return res;
        }
        public static string HexToAscii(string hexString, char? separator)
        {
            StringBuilder sb = new();
            if (separator is null)
            {
                for (int i = 0; i < hexString.Length; i += 2)
                {
                    string hex = hexString.Substring(i, 2);

                    int decval = HexToAsciiPosition(hex);
                    char character = Convert.ToChar(decval);

                    sb.Append(character);
                }

                return sb.ToString();
            }
            else
            {
                string[] hexArray = hexString.Split(new char[] { (char)separator });
                int counter = 0;
                foreach (var hexData in hexArray)
                {
                    int decval = HexToAsciiPosition(hexData);
                    if (counter % 16 == 0 && counter != 0)
                        sb.Append(Environment.NewLine);
                    sb.Append(Convert.ToChar(decval));
                    counter++;
                }
                return sb.ToString();
            }
        }

        public static string AsciiToHex(string asciiString)
        {

            string hex = string.Empty;

            foreach (var el in asciiString)
            {
                int value = Convert.ToInt32(el);
                // Convert the decimal value to a hexadecimal value in string form.
                hex += String.Format("{0:X}", value);
            }

            return hex;



        }
    }
}