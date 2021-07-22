using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Hex_Editor
{
    internal class FileWorker
    {
        public static Patch ReadPatch(string name)
        {
            Patch patch = new Patch
            {
                Instructions = new Stack<PatchInstruction>()
            };
            try
            {
                using (StreamReader file = new StreamReader(name))
                {
                    string ln;
                    if ((ln = file.ReadLine()) != null)
                    {
                        patch.FileName = ln.Substring(ln.LastIndexOf(">") + 1);
                        while ((ln = file.ReadLine()) != null)
                        {
                            PatchInstruction instruction = new PatchInstruction();
                            string[] parts = ln.Split(':');
                            instruction.Offset = System.Convert.ToInt32(parts[0], 16);
                            instruction.OldHex = parts[1].Substring(0, 2);
                            instruction.NewHex = parts[1].Substring(4, 2);
                            patch.Instructions.Push(instruction);
                        }
                    }

                    file.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return patch;
        }

        public static void ApplyPatches(string fileName, ObservableList<PatchInstruction> patches)
        {
            using (var stream = File.Open(fileName, FileMode.Open))//inefficient
            {
                foreach (var instruction in patches)
                {
                    stream.Position = instruction.Offset;
                    stream.WriteByte(byte.Parse(instruction.NewHex, System.Globalization.NumberStyles.HexNumber));
                }
            }
        }

        public static byte[] ReadFileData(string fileName)
        {
            return File.ReadAllBytes(fileName);
        }
    }
}