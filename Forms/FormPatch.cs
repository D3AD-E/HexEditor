using System;
using System.Windows.Forms;

namespace Hex_Editor
{
    public partial class FormPatch : Form
    {
        public Patch patch { get; set; }
        public string fileName { get; set; }

        public FormPatch()
        {
            InitializeComponent();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void FormPatch_Shown(object sender, EventArgs e)
        {
            label.Text += patch.FileName;
            foreach (var instruction in patch.Instructions)
            {
                string[] toList = { instruction.Offset.ToString(), instruction.OldHex, instruction.NewHex };
                listView.Items.Add(new ListViewItem(toList));//Undone
            }

            if (fileName != patch.FileName)
                labelWarning.Text = "WARNING: Current file and file name found in patch file do not match";
            listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}