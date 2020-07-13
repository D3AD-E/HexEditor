using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Hex_Editor
{
    internal class FileWorker
    {
        public Hex ReadReader(string name)
        {
            //MemoryMappedFile RawD = MemoryMappedFile.CreateFromFile(name);
            //MemoryMappedViewStream stream = RawD.CreateViewStream();
            //BinaryReader RawB = new BinaryReader(stream);
            byte[] bytes = File.ReadAllBytes(name);
            Hex hex = new Hex();
            for (int i = 0; i < bytes.Length; i++)
            {
                string toAdd = Convert.ToString(bytes[i]);
                hex.hex += toAdd;
                if (i % 16 == 0)
                    hex.display += toAdd + Environment.NewLine;
                else
                    hex.display += toAdd + " ";
            }
            return hex;
        }

        public Hex Read(string name)
        {
            FileStream fs = new FileStream(name, FileMode.Open);
            Hex hex = new Hex();
            int hexIn;

            for (int i = 1; (hexIn = fs.ReadByte()) != -1; i++)
            {
                hex.hex += string.Format("{0:X2}", hexIn);
                if (i % 16 == 0)
                    hex.display += string.Format("{0:X2}" + Environment.NewLine, hexIn);
                else
                    hex.display += string.Format("{0:X2} ", hexIn);
            }
            fs.Close();
            return hex;
        }

        public string ReadAndFormat(string name)
        {
            FileStream fs = new FileStream(name, FileMode.Open);
            int hexIn;
            string hex = "";

            for (int i = 1; (hexIn = fs.ReadByte()) != -1; i++)
            {
                if (i % 16 == 0)
                    hex += string.Format("{0:X2}" + Environment.NewLine, hexIn);
                else
                    hex += string.Format("{0:X2} ", hexIn);
            }
            fs.Close();

            return hex;
        }

        public Patch ReadPatch(string name)
        {
            Patch patch = new Patch
            {
                instructions = new Stack<PatchInstruction>()
            };
            try
            {
                using (StreamReader file = new StreamReader(name))
                {
                    string ln;
                    if ((ln = file.ReadLine()) != null)
                    {
                        patch.fileName = ln.Substring(ln.LastIndexOf(">") + 1);
                        while ((ln = file.ReadLine()) != null)
                        {
                            PatchInstruction instruction = new PatchInstruction();
                            string[] parts = ln.Split(':');
                            instruction.offset = System.Convert.ToInt32(parts[0], 16);
                            instruction.oldHex = parts[1].Substring(0, 2);
                            instruction.newHex = parts[1].Substring(4, 2);
                            patch.instructions.Push(instruction);
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
    }
}