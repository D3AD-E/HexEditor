using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hex_Editor
{
    public class PatchInstruction
    {
        public int Offset;
        public string OldHex;
        public string NewHex;
    }
}
