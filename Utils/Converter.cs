using System;
using System.Text;

namespace Hex_Editor
{
    internal class Converter
    {
        public static string from_hex(string s)
        {
            StringBuilder sb = new StringBuilder();
            string[] a = s.Split(new char[] { '-' });
            int counter = 0;
            foreach (var h in a)
            {
                int decval = int.Parse(h, System.Globalization.NumberStyles.HexNumber);
                if ((decval >= 0 && decval <= 31)|| (decval >= 127 && decval <= 129) || (decval >= 141 && decval <= 144)||(decval >= 157 && decval <= 158)||decval == 173)
                    decval = 46;
                if(counter % 16==0 && counter!=0)
                    sb.Append(Environment.NewLine);
                sb.Append((char)decval);
                counter++;
            }
            return sb.ToString();
        }

        public static string ConvertHex(string hexString)
        {
            try
            {
                string ascii = string.Empty;

                for (int i = 0; i < hexString.Length; i += 2)
                {
                    String hs = string.Empty;

                    hs = hexString.Substring(i, 2);
                    //if (hs == "0C" || hs == "0E" || hs == "0F" || hs == "00" || hs == "08" || hs == "09" || hs == "0B" || hs == "0D" || hs == "0A")
                    //hs = "2E";

                    uint decval = System.Convert.ToUInt32(hs, 16);
                    if ((decval >= 0 && decval <= 31) || (decval >= 127 && decval <= 129) || (decval >= 141 && decval <= 144) || (decval >= 157 && decval <= 158) || decval == 173)
                        decval = 46;
                    char character = System.Convert.ToChar(decval);

                    ascii += character;
                }

                return ascii;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            return string.Empty;
        }

        public static string ConvertAscii(string asciiString)
        {
            try
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
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            return string.Empty;
        }


    }
}