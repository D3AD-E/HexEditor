using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            label.Text += patch.fileName;
            foreach (var instruction in patch.instructions)
            {
                string[] toList = { instruction.offset.ToString(), instruction.oldHex, instruction.newHex };
                listView.Items.Add(new ListViewItem(toList));//Undone
            }

            if (fileName != patch.fileName)
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
