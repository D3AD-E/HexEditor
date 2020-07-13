namespace Hex_Editor
{
    partial class FormMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.richTBMain = new System.Windows.Forms.RichTextBox();
            this.labelLeft = new System.Windows.Forms.Label();
            this.labelTop = new System.Windows.Forms.Label();
            this.richTBAscii = new System.Windows.Forms.RichTextBox();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.applyPatchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.breakFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFDialog = new System.Windows.Forms.OpenFileDialog();
            this.openFDialogPatch = new System.Windows.Forms.OpenFileDialog();
            this.btnForward = new System.Windows.Forms.Button();
            this.btnRevert = new System.Windows.Forms.Button();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.btnBrowseChange = new System.Windows.Forms.Button();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTBMain
            // 
            this.richTBMain.BackColor = System.Drawing.Color.White;
            this.richTBMain.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.richTBMain.Location = new System.Drawing.Point(80, 99);
            this.richTBMain.Name = "richTBMain";
            this.richTBMain.ReadOnly = true;
            this.richTBMain.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTBMain.Size = new System.Drawing.Size(354, 329);
            this.richTBMain.TabIndex = 3;
            this.richTBMain.Text = "";
            this.richTBMain.SelectionChanged += new System.EventHandler(this.richTBMain_SelectionChanged);
            this.richTBMain.Enter += new System.EventHandler(this.richTBMain_Enter);
            this.richTBMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.richTBMain_KeyDown);
            this.richTBMain.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.richTBMain_KeyPress);
            this.richTBMain.Leave += new System.EventHandler(this.richTBMain_Leave);
            this.richTBMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.richTBMain_MouseDown);
            // 
            // labelLeft
            // 
            this.labelLeft.AutoSize = true;
            this.labelLeft.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelLeft.Location = new System.Drawing.Point(38, 102);
            this.labelLeft.Name = "labelLeft";
            this.labelLeft.Size = new System.Drawing.Size(14, 14);
            this.labelLeft.TabIndex = 4;
            this.labelLeft.Text = " ";
            // 
            // labelTop
            // 
            this.labelTop.AutoSize = true;
            this.labelTop.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTop.Location = new System.Drawing.Point(24, 72);
            this.labelTop.Name = "labelTop";
            this.labelTop.Size = new System.Drawing.Size(63, 14);
            this.labelTop.TabIndex = 5;
            this.labelTop.Text = "Offset: ";
            // 
            // richTBAscii
            // 
            this.richTBAscii.BackColor = System.Drawing.Color.White;
            this.richTBAscii.DetectUrls = false;
            this.richTBAscii.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.richTBAscii.Location = new System.Drawing.Point(473, 99);
            this.richTBAscii.Name = "richTBAscii";
            this.richTBAscii.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.richTBAscii.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTBAscii.Size = new System.Drawing.Size(148, 329);
            this.richTBAscii.TabIndex = 6;
            this.richTBAscii.Text = "";
            this.richTBAscii.WordWrap = false;
            this.richTBAscii.SelectionChanged += new System.EventHandler(this.richTBAscii_SelectionChanged);
            this.richTBAscii.Enter += new System.EventHandler(this.richTBAscii_Enter);
            this.richTBAscii.KeyDown += new System.Windows.Forms.KeyEventHandler(this.richTBAscii_KeyDown);
            this.richTBAscii.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.richTBAscii_KeyPress);
            this.richTBAscii.Leave += new System.EventHandler(this.richTBAscii_Leave);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.extraToolStripMenuItem,
            this.infoToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(686, 24);
            this.menuStrip.TabIndex = 8;
            this.menuStrip.Text = "menuStrip1";
            this.menuStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItem1.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // extraToolStripMenuItem
            // 
            this.extraToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.applyPatchToolStripMenuItem,
            this.breakFileToolStripMenuItem});
            this.extraToolStripMenuItem.Name = "extraToolStripMenuItem";
            this.extraToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.extraToolStripMenuItem.Text = "Extra";
            // 
            // applyPatchToolStripMenuItem
            // 
            this.applyPatchToolStripMenuItem.Name = "applyPatchToolStripMenuItem";
            this.applyPatchToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.applyPatchToolStripMenuItem.Text = "Apply patch";
            this.applyPatchToolStripMenuItem.Click += new System.EventHandler(this.applyPatchToolStripMenuItem_Click);
            // 
            // breakFileToolStripMenuItem
            // 
            this.breakFileToolStripMenuItem.Name = "breakFileToolStripMenuItem";
            this.breakFileToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.breakFileToolStripMenuItem.Text = "Break file";
            this.breakFileToolStripMenuItem.Click += new System.EventHandler(this.breakFileToolStripMenuItem_Click);
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.infoToolStripMenuItem.Text = "Info";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // openFDialogPatch
            // 
            this.openFDialogPatch.Filter = "Patch files|*.1337";
            // 
            // btnForward
            // 
            this.btnForward.Location = new System.Drawing.Point(330, 27);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(104, 37);
            this.btnForward.TabIndex = 10;
            this.btnForward.Text = "-->";
            this.btnForward.UseVisualStyleBackColor = true;
            this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
            // 
            // btnRevert
            // 
            this.btnRevert.Location = new System.Drawing.Point(80, 27);
            this.btnRevert.Name = "btnRevert";
            this.btnRevert.Size = new System.Drawing.Size(114, 37);
            this.btnRevert.TabIndex = 9;
            this.btnRevert.Text = "<--";
            this.btnRevert.UseVisualStyleBackColor = true;
            this.btnRevert.Click += new System.EventHandler(this.btnRevert_Click);
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Location = new System.Drawing.Point(624, 99);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(17, 329);
            this.vScrollBar1.TabIndex = 11;
            this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
            // 
            // btnBrowseChange
            // 
            this.btnBrowseChange.Location = new System.Drawing.Point(478, 27);
            this.btnBrowseChange.Name = "btnBrowseChange";
            this.btnBrowseChange.Size = new System.Drawing.Size(142, 36);
            this.btnBrowseChange.TabIndex = 12;
            this.btnBrowseChange.Text = "Browse changes";
            this.btnBrowseChange.UseVisualStyleBackColor = true;
            this.btnBrowseChange.Click += new System.EventHandler(this.btnBrowseChange_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 487);
            this.Controls.Add(this.btnBrowseChange);
            this.Controls.Add(this.vScrollBar1);
            this.Controls.Add(this.btnForward);
            this.Controls.Add(this.btnRevert);
            this.Controls.Add(this.richTBAscii);
            this.Controls.Add(this.labelTop);
            this.Controls.Add(this.labelLeft);
            this.Controls.Add(this.richTBMain);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.Text = "Helium Hex Editor";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyDown);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox richTBMain;
        private System.Windows.Forms.Label labelLeft;
        private System.Windows.Forms.Label labelTop;
        private System.Windows.Forms.RichTextBox richTBAscii;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem applyPatchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFDialog;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFDialogPatch;
        private System.Windows.Forms.ToolStripMenuItem breakFileToolStripMenuItem;
        private System.Windows.Forms.Button btnRevert;
        private System.Windows.Forms.Button btnForward;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.Button btnBrowseChange;
    }
}

