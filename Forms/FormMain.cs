using Hex_Editor.Classes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Hex_Editor
{
    public partial class FormMain : Form
    {
        private readonly FileWorker fileWorker = new FileWorker();

       // private string Hex;

        private string fileName;

        private int currentInst = 0;

        private ObservableList<PatchInstruction> history = new ObservableList<PatchInstruction>();

        public FormMain()
        {
            InitializeComponent();
            history.OnAdd += History_OnAdd;
            history.OnClear += History_OnClear;
            btnForward.Enabled = false;
            btnRevert.Enabled = false;
            breakFileToolStripMenuItem.Enabled = false;
            applyPatchToolStripMenuItem.Enabled = false;
            richTBMain.Enabled = false;
            richTBAscii.Enabled = false;
            vScrollBar1.Enabled = false;
        }
        //Disable buttons for reverting changes if history is empty
        private void History_OnClear(object sender, EventArgs e)
        {
            currentInst = 0;
            btnForward.Enabled = false;
            btnRevert.Enabled = false;
        }
        //Remove all instructions from history if we reverted any changes
        private void History_OnAdd(object sender, EventArgs e)
        {
            btnRevert.Enabled = true;
            btnForward.Enabled = false;
            int count = history.Count;
            if (currentInst == count)
                currentInst++;
            else
            {
                history.RemoveRange(currentInst, count - currentInst);
                currentInst++;
            }
        }

        //Setup top and left borders for richTBMain
        private void SetupBorders()
        {
            string topBorder = "Offset  ";
            for (int i = 0; i < 16; i++)
            {
                topBorder += string.Format("{0:X2} ", i);
            }
            labelTop.Text = topBorder;
            SetupBorderLeft(0);
        }
        //topline determines starting offset at top
        private void SetupBorderLeft(int topline)
        {
            string leftBorder = "";
            for (int i = topline; i < topline+23; i++)
            {
                leftBorder += (i * 16).ToString("X") + Environment.NewLine;
            }
            labelLeft.Text = leftBorder;
        }

        [Obsolete("Too inefficient")]
        private string FormatAscii(string input)
        {
            Regex regex = new Regex(@"\s*\n");
            input = regex.Replace(input, "..");
            for (int i = 16; i < input.Length; i += 16)
            {
                {
                    //input = input.Insert(i, Environment.NewLine);
                    // i += Environment.NewLine.Length;
                }
            }
            return input;
        }

        private bool eatEventMain = false; //if true richTBMain does not see selectionChanged
        private bool eatEventAscii = false;//if true richTBAscii does not see selectionChanged
        //Select representation of hex in ascii
        private void richTBMain_SelectionChanged(object sender, EventArgs e)
        {
            richTBAscii.SelectionBackColor = Color.White;
            if (richTBMain.SelectedText.Length == 0 || eatEventMain)
            {
                return;
            }

            if (richTBMain.SelectedText == " ")
            {
                richTBMain.SelectionStart++;
                //richTBMain.SelectionLength--;
            }
            else if (richTBMain.SelectedText.StartsWith(" "))
            {
                richTBMain.SelectionStart++;
                //richTBMain.SelectionLength--;
            }
            else if (richTBMain.SelectedText.EndsWith(" "))
            {
                richTBMain.SelectionLength--;
            }
            else if (richTBMain.SelectionStart % 3 == 0 && (richTBMain.SelectionLength - 1) % 3 == 0)
            {
                richTBMain.SelectionLength++;
            }
            else if ((richTBMain.SelectionStart - 1) % 3 == 0 && (richTBMain.SelectionLength - 1) % 3 == 0)
            {
                richTBMain.SelectionStart--;
                richTBMain.SelectionLength++;
            }

            richTBAscii.SelectionStart = richTBMain.SelectionStart / 3 + richTBMain.SelectionStart / 48;
            richTBAscii.SelectionLength = (richTBMain.SelectionLength + 1) / 3;
            richTBAscii.SelectionBackColor = Color.LightBlue;
        }

        private void richTBAscii_SelectionChanged(object sender, EventArgs e)
        {
            richTBMain.SelectionBackColor = Color.White;

            if (richTBAscii.SelectionLength == 0 || eatEventAscii)
                return;

            if (Converter.ConvertAscii(richTBAscii.SelectedText) == "A")
            {
                eatEventMain = true;
                richTBAscii.SelectionLength = 0;
                richTBAscii.SelectionStart++;
                richTBAscii.SelectionLength = 0;
                eatEventMain = false;
                return;
            }
            int start = richTBAscii.SelectionStart - richTBAscii.GetLineFromCharIndex(richTBAscii.SelectionStart);
            int length = richTBAscii.SelectionLength;
            richTBMain.SelectionStart = start * 3;
            richTBMain.SelectionLength = length * 3 - 1;
            richTBMain.SelectionBackColor = Color.LightBlue;
        }

        private void richTBAscii_Leave(object sender, EventArgs e)
        {
            richTBMain.SelectionBackColor = Color.White;
            eatEventAscii = false;
            eatEventMain = false;
        }

        private void richTBMain_Leave(object sender, EventArgs e)
        {
            richTBAscii.SelectionBackColor = Color.White;
            eatEventAscii = false;
            eatEventMain = false;
        }

        private void richTBMain_Enter(object sender, EventArgs e)
        {
            eatEventAscii = true;
        }

        private void richTBAscii_Enter(object sender, EventArgs e)
        {
            eatEventMain = true;
        }

        //Only for richTBMain
        private bool isTextAtBorder(int pos)
        {
            int line = richTBMain.GetLineFromCharIndex(pos);
            return ((pos - line) % 47 == 0 && pos != 0);
        }

        private void richTBMain_KeyPress(object sender, KeyPressEventArgs e)
        {
            char currentChar = e.KeyChar;
            currentChar = char.ToUpper(currentChar);

            if ((currentChar >= 65 && currentChar <= 70) || char.IsDigit(currentChar))
            {
                Color highlightColor = Color.Red;
                int pos = richTBMain.SelectionStart;

                if (pos >= richTBMain.Text.Length)
                {
                    bool newline = (isTextAtBorder(pos));
                    
                    ChangeText(currentChar.ToString(), highlightColor, pos, ref richTBMain, newline);
                    PatchInstruction toadd = new PatchInstruction
                    {
                        oldHex = "",
                        newHex = richTBMain.Text.Substring(richTBMain.Text.Length - 2),
                        offset = (richTBMain.Text.Length - 2) / 3
                    };
                    history.Add(toadd);
                    HexAdded(newline);

                    e.Handled = true;
                    richTBMain.SelectionStart = pos + 2;
                }
                else
                {
                    PatchInstruction toadd = new PatchInstruction();
                    int blockStart;
                    if (pos == richTBMain.Text.Length - 1)
                    {
                        blockStart = pos - 1;
                    }
                    else
                        blockStart = GetBlockStart(pos);
                    toadd.oldHex = richTBMain.Text.Substring(blockStart, 2);
                    ChangeText(currentChar.ToString(), highlightColor, pos, ref richTBMain);

                    toadd.newHex = richTBMain.Text.Substring(blockStart, 2);
                    toadd.offset = blockStart / 3;
                    history.Add(toadd);

                    HexChanged(pos);
                    e.Handled = true;
                    if (pos >= richTBMain.Text.Length - 1) //REDO
                    {
                        richTBMain.SelectionStart = pos + 1;
                    }
                    else if (richTBMain.Text[pos + 1] == ' ' || richTBMain.Text[pos + 1] == '\n')
                    {
                        richTBMain.SelectionStart = pos + 2;
                    }
                    else
                        richTBMain.SelectionStart = pos + 1;
                }
            }
            else
            {
                e.Handled = true;
            }
        }

        private void HexAdded(bool newline)
        {
            string asciiPart = Converter.ConvertHex(richTBMain.Text.Substring(richTBMain.Text.Length - 2));
            ChangeText(asciiPart, Color.Red, richTBAscii.Text.Length, ref richTBAscii, newline);
        }

        private void HexChanged(int pos)
        {
            int blockStart;
            if (pos == richTBMain.Text.Length - 1)
            {
                blockStart = pos - 1;
            }
            else
                blockStart = GetBlockStart(pos);
            string asciiPart = Converter.ConvertHex(richTBMain.Text.Substring(blockStart, 2));
            ChangeText(asciiPart, Color.Red, (blockStart / 3) + (blockStart / 48), ref richTBAscii);
        }

        private int GetBlockStart(int pos)
        {
            if (pos >= richTBMain.Text.Length - 1) //REDO
            {
                return pos - 2;
            }
            else if (richTBMain.Text[pos + 1] == ' ' || richTBMain.Text[pos + 1] == '\n')
            {
                return pos - 1;
            }
            else
                return pos;
        }

        private void ChangeText(string text, Color color, int pos, ref RichTextBox box, bool newline = false, bool isHistory = false)
        {
            eatEventMain = true;
            eatEventAscii = true;
            box.SelectionStart = pos;
            if (pos == box.Text.Length)
            {
                box.SelectionLength = 0;
                box.SelectionColor = color;
                if (box == richTBMain)
                {
                    if(isHistory)
                    {
                        if (newline)
                            box.AppendText(Environment.NewLine + text);
                        else
                            box.AppendText(" " + text);
                    }
                    else
                    {
                        if (newline)
                            box.AppendText(Environment.NewLine + text + "0");
                        else
                            box.AppendText(" " + text + "0");
                    }
                }
                else if (box == richTBAscii)
                {
                    if (newline)
                        box.AppendText(Environment.NewLine + text);
                    else
                        box.AppendText(text);
                }
            }
            else
            {
                box.SelectionLength = text.Length;
                box.SelectionColor = color;
                box.SelectedText = text;
                box.SelectionLength = 0;
            }
            box.SelectionColor = box.ForeColor;
            eatEventAscii = false;
            eatEventMain = false;
        }

        private void richTBMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                int pos = richTBMain.SelectionStart;
                if (pos >= richTBMain.Text.Length - 1)
                {
                }
                else if (richTBMain.Text[pos + 1] == ' ' || richTBMain.Text[pos + 1] == '\n')
                {
                    richTBMain.SelectionStart += 2;
                    e.SuppressKeyPress = true;
                }
            }
            else if (e.KeyCode == Keys.Left)
            {
                int pos = richTBMain.SelectionStart;
                if (pos == 0)
                {
                }
                else if (richTBMain.Text[pos - 1] == ' ' || richTBMain.Text[pos - 1] == '\n')
                {
                    richTBMain.SelectionStart -= 2;
                    e.SuppressKeyPress = true;
                }
            }
        }

        private void richTBMain_MouseDown(object sender, MouseEventArgs e)
        {
            int pos = richTBMain.SelectionStart;
            if (pos >= richTBMain.Text.Length || richTBMain.Text[pos] == '\n')
            {
                richTBMain.SelectionStart -= 1;
            }
            else if (richTBMain.Text[pos] == ' ')
            {
                richTBMain.SelectionStart += 1;
            }
        }

        private void richTBAscii_KeyPress(object sender, KeyPressEventArgs e)
        {
            char currentChar = e.KeyChar;

            if (!(currentChar >= 0 && currentChar <= 31))
            {
                Color highlightColor = Color.Red;
                int pos = richTBAscii.SelectionStart;
                int line = richTBAscii.GetLineFromCharIndex(pos);
                PatchInstruction toAdd = new PatchInstruction
                {
                    offset = (pos - richTBAscii.GetLineFromCharIndex(pos))
                };
                if (pos >= richTBAscii.Text.Length)
                {
                    bool newline = ((pos - line) % 16 == 0);

                    ChangeText(currentChar.ToString(), highlightColor, pos, ref richTBAscii, newline);
                    AsciiAdded();
                    toAdd.oldHex = "";
                    toAdd.newHex = richTBMain.Text.Substring((pos - richTBAscii.GetLineFromCharIndex(pos)) * 3, 2);
                    history.Add(toAdd);
                    e.Handled = true;
                    if (newline)
                        richTBAscii.SelectionStart = pos + 2;
                    else
                        richTBAscii.SelectionStart = pos + 1;
                }
                else
                {
                    if ((pos - line) % 16 == 0 && pos != 0)
                    {
                        ChangeText(currentChar.ToString(), highlightColor, pos + 1, ref richTBAscii, true);
                        toAdd.oldHex = richTBMain.Text.Substring((pos + 1 - richTBAscii.GetLineFromCharIndex(pos)) * 3, 2);
                        AsciiChanged(pos + 1);
                        toAdd.offset = (pos+1 - richTBAscii.GetLineFromCharIndex(pos));
                        toAdd.newHex = richTBMain.Text.Substring((pos+1 - richTBAscii.GetLineFromCharIndex(pos)) * 3, 2);
                        history.Add(toAdd);
                        e.Handled = true;
                        richTBAscii.SelectionStart = pos + 2;
                    }
                    else
                    {
                        ChangeText(currentChar.ToString(), highlightColor, pos, ref richTBAscii);
                        toAdd.oldHex = richTBMain.Text.Substring((pos - richTBAscii.GetLineFromCharIndex(pos)) * 3, 2);
                        AsciiChanged(pos);
                        
                        toAdd.newHex = richTBMain.Text.Substring((pos - richTBAscii.GetLineFromCharIndex(pos)) * 3, 2);
                        history.Add(toAdd);
                        e.Handled = true;
                        richTBAscii.SelectionStart = pos + 1;
                    }
                }
            }
            else
            {
                e.Handled = true;
            }
        }

        private void AsciiAdded()
        {
            string hexPart = Converter.ConvertAscii(richTBAscii.Text.Substring(richTBAscii.Text.Length - 1));
            ChangeText(hexPart, Color.Red, richTBMain.Text.Length, ref richTBMain, false, true);
        }

        private void AsciiChanged(int pos)
        {
            string hexPart = Converter.ConvertAscii(richTBAscii.Text.Substring(pos, 1));
            ChangeText(hexPart, Color.Red, (pos - richTBAscii.GetLineFromCharIndex(pos)) * 3, ref richTBMain);
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == null)
                return;
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFDialog.ShowDialog();
            fileName = Path.GetFileName(openFDialog.FileName);
            if(!string.IsNullOrEmpty(fileName))
            {
                ReadFile();
                SetupBorders();
            }
            
        }

        private void ReadFile()
        {
            string src = openFDialog.FileName;
            var data = File.ReadAllBytes(src);
            //Hex = BitConverter.ToString(data).Replace("-", string.Empty);
            richTBMain.Text = BitConverter.ToString(data).Replace("-", " ");
            richTBAscii.Text = Converter.from_hex(BitConverter.ToString(data));
            history.Clear();
            vScrollBar1.Maximum = richTBAscii.Lines.Length-1;
            breakFileToolStripMenuItem.Enabled = true;
            applyPatchToolStripMenuItem.Enabled = true;
            richTBMain.Enabled = true;
            richTBAscii.Enabled = true ;
            vScrollBar1.Enabled = true;
        }

        private void applyPatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFDialogPatch.ShowDialog();
            string path = openFDialogPatch.FileName;
            Patch currentPatch = fileWorker.ReadPatch(path);
            if (currentPatch.isEmpty())
                return;
            FormPatch formPatch = new FormPatch
            {
                patch = currentPatch,
                fileName = fileName
            };
            var res = formPatch.ShowDialog();
            if (res == DialogResult.Cancel)
                return;
            else if (res == DialogResult.Yes)
            {
                ApplyPatch(currentPatch);
            }
            else
                throw new NotImplementedException();
        }

        private void ApplyPatch(Patch patch)
        {
            Stack<string> errors = new Stack<string>();
            int maxOffset = richTBMain.Text.Length/3;
            while (patch.instructions.Count > 0)
            {
                var instruc = patch.instructions.Pop();
                int offset = instruc.offset * 3;
                if (maxOffset < offset)
                {
                    errors.Push("Offset " + instruc.offset + " greater than size of file");
                }
                else if (instruc.oldHex != richTBMain.Text.Substring(offset, 2) && instruc.oldHex != "**")
                {
                    errors.Push("Byte " + instruc.oldHex + " not the same as " + richTBMain.Text.Substring(offset, 2));
                }
                else
                {
                    PatchInstruction toAdd = new PatchInstruction
                    {
                        offset = instruc.offset,
                        oldHex = richTBMain.Text.Substring(offset, 2),
                        newHex = instruc.newHex
                    };
                    history.Add(toAdd);
                    
                    ChangeText(instruc.newHex, Color.Red, offset, ref richTBMain);

                    HexChanged(offset);
                }
            }

            if (errors.Count > 0)
            {
                int counter = 1;
                string toCreate = "Errors:";
                while (errors.Count > 0)
                {
                    toCreate += Environment.NewLine + counter.ToString() + ". " + errors.Pop();
                    counter++;
                }
                MessageBox.Show(toCreate, "Patch failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Patch successfully applied");
            }
        }

        [Obsolete("Used only for testing, use HexChanged instead")]
        private void ReloadHex()
        {
            //richTBMain.Text = FormatHex(Hex);
        }
        [Obsolete("Use ChangeText instead")]
        private void ChangeHex(int pos, string hex, Color color, bool isAtRichbox = false)
        {
            //Hex = Hex.Remove(pos, 2).Insert(pos, hex);
            eatEventMain = true;
            int spaceCount = pos / 2;
            if (string.IsNullOrEmpty(hex))
            {
                richTBMain.Text = richTBMain.Text.Remove(richTBMain.Text.Length - 2);
                richTBAscii.Text = richTBAscii.Text.Remove(richTBAscii.Text.Length - 1);
            }
            else
            {
                richTBMain.SelectionStart = pos + spaceCount;
                richTBMain.SelectionLength = 2;
                richTBMain.SelectionColor = color;
                richTBMain.SelectedText = hex;
                richTBMain.SelectionLength = 0;
                HexChanged(pos + spaceCount);
            }
            eatEventMain = false;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Made by D3AD", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void breakFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var rand = new Random();
            for (int i = 0; i < 20; i++)
            {
                int pos = rand.Next(richTBMain.Text.Length / 9) * 3;
                PatchInstruction toAdd = new PatchInstruction
                {
                    offset = pos / 3,
                    oldHex = richTBMain.Text.Substring(pos, 2),
                    newHex = "90"
                };
                ChangeText("90", Color.Red, pos, ref richTBMain);
                history.Add(toAdd);
                HexChanged(pos);
            }
            MessageBox.Show("File probably could not be opened now, otherwise use break again\n(do not forget to save file to apply changes)");
        }

        private void richTBAscii_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                int pos = richTBAscii.SelectionStart;
                if (pos >= richTBAscii.Text.Length - 1)
                {
                }
                else if (richTBAscii.Text[pos + 1] == '\n')
                {
                    richTBAscii.SelectionStart += 2;
                    e.SuppressKeyPress = true;
                }
            }
            else if (e.KeyCode == Keys.Left)
            {
                int pos = richTBAscii.SelectionStart;
                if (pos == 0)
                {
                }
                else if (richTBAscii.Text[pos - 1] == '\n')
                {
                    richTBAscii.SelectionStart -= 2;
                    e.SuppressKeyPress = true;
                }
            }
        }



        private void SaveFile()
        {
            ResetColor();
            using (var stream = File.Open(openFDialog.FileName, FileMode.Open))//inefficient
            {
                foreach (var instruction in history)
                {
                    stream.Position = instruction.offset;
                    stream.WriteByte(byte.Parse(instruction.newHex, System.Globalization.NumberStyles.HexNumber));
                }
            }
            history.Clear();
        }

        private void ResetColor()
        {
            eatEventAscii = true;
            eatEventMain = true;

            richTBAscii.SelectAll();
            richTBAscii.SelectionColor = Color.Black;
            richTBAscii.SelectionLength = 0;
            richTBMain.SelectAll();
            richTBMain.SelectionColor = Color.Black;
            richTBMain.SelectionLength = 0;

            eatEventAscii = false;
            eatEventMain = false;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
            
        }



        private void btnRevert_Click(object sender, EventArgs e)
        {
            HistoryBackward();
        }

        private void RevertChange()
        {
            var inst = history[currentInst - 1];
            int pos = inst.offset * 3;
            if (string.IsNullOrEmpty(inst.oldHex))
            {
                bool border = isTextAtBorder(GetBlockStart(pos));
                RemoveHex(pos-1, border);
            }
            else
            {
                bool occ = false;
                for(int i =0; i<currentInst-1; i++)
                {
                    if(history[i].offset == inst.offset)
                    {
                        occ = true;
                        break;
                    }
                }
                if(occ)
                    ChangeText(inst.oldHex, Color.Red, pos, ref richTBMain);
                else
                    ChangeText(inst.oldHex, Color.Black, pos, ref richTBMain);
                HexChanged(pos);
            }
        }

        private void RemoveHex(int pos, bool isBorder)
        {
            richTBMain.Text = richTBMain.Text.Remove(GetBlockStart(pos));
            if (isBorder)
                richTBAscii.Text = richTBAscii.Text.Remove(richTBAscii.Text.Length - 2);
            else
                richTBAscii.Text = richTBAscii.Text.Remove(richTBAscii.Text.Length-1);
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            HistoryForward();
        }
        private void HistoryForward()
        {
            btnRevert.Enabled = true;
            if (currentInst >= history.Count || currentInst < 0)
                return;
            var inst = history[currentInst];
            int pos = inst.offset * 3;
            if (string.IsNullOrEmpty(inst.oldHex))
            {
                bool newline = (isTextAtBorder(pos - 1));
                ChangeText(inst.newHex, Color.Red, pos - 1, ref richTBMain, false, true); 
                HexAdded(newline);
            }
            else
            {
                ChangeText(inst.newHex, Color.Red, pos, ref richTBMain);
                HexChanged(pos);
            }
            currentInst++;
            if (currentInst == history.Count)
                btnForward.Enabled = false;
        }

        private void HistoryBackward()
        {
            btnForward.Enabled = true;
            RevertChange();
            currentInst--;
            if (currentInst <= 0)
                btnRevert.Enabled = false;
        }
        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                SaveFile();
            }
            else if (e.Control && e.KeyCode == Keys.Z)
            {
                if(btnRevert.Enabled)
                    HistoryBackward();
            }
            else if (e.Control && e.KeyCode == Keys.Y)
            {
                if(btnForward.Enabled)
                    HistoryForward();
            }
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            int position = richTBAscii.GetFirstCharIndexFromLine(vScrollBar1.Value);
            eatEventAscii = true;
            eatEventMain = true;
            richTBAscii.Select(position, 0);
            richTBMain.Select(vScrollBar1.Value*48+1, 0);
            richTBAscii.ScrollToCaret();
            richTBMain.ScrollToCaret();
            SetupBorderLeft(vScrollBar1.Value);
            eatEventAscii = false;
            eatEventMain = false;
        }

        private void btnBrowseChange_Click(object sender, EventArgs e)
        {
            string temp = "Offset/Old byte -> New Byte\n";
            int counter = 0;
            foreach (var inst in history)
            {
                if ((currentInst - 1) == counter)
                    temp += ">>> ";
                if (string.IsNullOrEmpty(inst.oldHex))
                    temp += inst.offset.ToString("X") + " / " + "__" + "->" + inst.newHex + Environment.NewLine;
                else
                    temp += inst.offset.ToString("X") + " / " + inst.oldHex + "->" + inst.newHex + Environment.NewLine;
                counter++;
            }
            MessageBox.Show(temp, "History");
        }
    }
}