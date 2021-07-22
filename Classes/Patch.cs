using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hex_Editor
{
    public class Patch
    {
        public Stack<PatchInstruction> Instructions;
        public string FileName { get; set; }
        public bool IsEmpty()
        {
            if (Instructions.Count == 0 && string.IsNullOrEmpty(FileName))
                return true;
            else
                return false;
        }
    }
}
