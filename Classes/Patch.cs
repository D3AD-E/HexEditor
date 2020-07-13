using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hex_Editor
{
    public class Patch
    {
        public Stack<PatchInstruction> instructions;
        public string fileName;
        public bool isEmpty()
        {
            if (instructions.Count == 0 && string.IsNullOrEmpty(fileName))
                return true;
            else
                return false;
        }
    }
}
