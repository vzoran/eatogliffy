namespace eatogliffyGUI
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonReload = new System.Windows.Forms.Button();
            this.buttonOpenFile = new System.Windows.Forms.Button();
            this.textSourceFile = new System.Windows.Forms.TextBox();
            this.labelInputFile = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonTargetFile = new System.Windows.Forms.Button();
            this.textTargetFile = new System.Windows.Forms.TextBox();
            this.labelTarget = new System.Windows.Forms.Label();
            this.treeDiagrams = new System.Windows.Forms.TreeView();
            this.imageTree = new System.Windows.Forms.ImageList(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Enterprise architect files|*.eap|All files|*.*";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "Gliffy files|*.Gliffy|All files|*.*";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonReload);
            this.panel1.Controls.Add(this.buttonOpenFile);
            this.panel1.Controls.Add(this.textSourceFile);
            this.panel1.Controls.Add(this.labelInputFile);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(563, 46);
            this.panel1.TabIndex = 6;
            // 
            // buttonReload
            // 
            this.buttonReload.Image = ((System.Drawing.Image)(resources.GetObject("buttonReload.Image")));
            this.buttonReload.Location = new System.Drawing.Point(527, 10);
            this.buttonReload.Name = "buttonReload";
            this.buttonReload.Size = new System.Drawing.Size(24, 23);
            this.buttonReload.TabIndex = 6;
            this.buttonReload.UseVisualStyleBackColor = true;
            this.buttonReload.Click += new System.EventHandler(this.buttonReload_Click);
            // 
            // buttonOpenFile
            // 
            this.buttonOpenFile.Image = ((System.Drawing.Image)(resources.GetObject("buttonOpenFile.Image")));
            this.buttonOpenFile.Location = new System.Drawing.Point(497, 10);
            this.buttonOpenFile.Name = "buttonOpenFile";
            this.buttonOpenFile.Size = new System.Drawing.Size(24, 23);
            this.buttonOpenFile.TabIndex = 5;
            this.buttonOpenFile.UseVisualStyleBackColor = true;
            this.buttonOpenFile.Click += new System.EventHandler(this.buttonOpenFile_Click);
            // 
            // textSourceFile
            // 
            this.textSourceFile.Location = new System.Drawing.Point(92, 12);
            this.textSourceFile.Name = "textSourceFile";
            this.textSourceFile.Size = new System.Drawing.Size(399, 20);
            this.textSourceFile.TabIndex = 4;
            // 
            // labelInputFile
            // 
            this.labelInputFile.AutoSize = true;
            this.labelInputFile.Location = new System.Drawing.Point(12, 15);
            this.labelInputFile.Name = "labelInputFile";
            this.labelInputFile.Size = new System.Drawing.Size(41, 13);
            this.labelInputFile.TabIndex = 3;
            this.labelInputFile.Text = "Source";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.buttonTargetFile);
            this.panel2.Controls.Add(this.textTargetFile);
            this.panel2.Controls.Add(this.labelTarget);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 407);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(563, 108);
            this.panel2.TabIndex = 7;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.buttonOk);
            this.panel3.Controls.Add(this.buttonCancel);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 42);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(563, 66);
            this.panel3.TabIndex = 9;
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(345, 19);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(100, 35);
            this.buttonOk.TabIndex = 1;
            this.buttonOk.Text = "Convert";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(451, 19);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 35);
            this.buttonCancel.TabIndex = 0;
            this.buttonCancel.Text = "Close";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonTargetFile
            // 
            this.buttonTargetFile.Image = ((System.Drawing.Image)(resources.GetObject("buttonTargetFile.Image")));
            this.buttonTargetFile.Location = new System.Drawing.Point(497, 9);
            this.buttonTargetFile.Name = "buttonTargetFile";
            this.buttonTargetFile.Size = new System.Drawing.Size(24, 23);
            this.buttonTargetFile.TabIndex = 8;
            this.buttonTargetFile.UseVisualStyleBackColor = true;
            this.buttonTargetFile.Click += new System.EventHandler(this.buttonTargetFile_Click);
            // 
            // textTargetFile
            // 
            this.textTargetFile.Location = new System.Drawing.Point(92, 12);
            this.textTargetFile.Name = "textTargetFile";
            this.textTargetFile.Size = new System.Drawing.Size(399, 20);
            this.textTargetFile.TabIndex = 7;
            // 
            // labelTarget
            // 
            this.labelTarget.AutoSize = true;
            this.labelTarget.Location = new System.Drawing.Point(12, 15);
            this.labelTarget.Name = "labelTarget";
            this.labelTarget.Size = new System.Drawing.Size(38, 13);
            this.labelTarget.TabIndex = 6;
            this.labelTarget.Text = "Target";
            // 
            // treeDiagrams
            // 
            this.treeDiagrams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeDiagrams.ImageIndex = 0;
            this.treeDiagrams.ImageList = this.imageTree;
            this.treeDiagrams.Location = new System.Drawing.Point(0, 46);
            this.treeDiagrams.Name = "treeDiagrams";
            this.treeDiagrams.SelectedImageIndex = 0;
            this.treeDiagrams.Size = new System.Drawing.Size(563, 361);
            this.treeDiagrams.TabIndex = 8;
            // 
            // imageTree
            // 
            this.imageTree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageTree.ImageStream")));
            this.imageTree.TransparentColor = System.Drawing.Color.Transparent;
            this.imageTree.Images.SetKeyName(0, "folder.png");
            this.imageTree.Images.SetKeyName(1, "chart_organisation.png");
            // 
            // FormMain
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(563, 515);
            this.Controls.Add(this.treeDiagrams);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main console";
            this.Deactivate += new System.EventHandler(this.formMain_Deactivate);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.formMain_FormClosed);
            this.Load += new System.EventHandler(this.formMain_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelTarget;
        private System.Windows.Forms.TextBox textTargetFile;
        private System.Windows.Forms.Button buttonTargetFile;
        private System.Windows.Forms.Label labelInputFile;
        private System.Windows.Forms.TextBox textSourceFile;
        private System.Windows.Forms.Button buttonOpenFile;
        private System.Windows.Forms.TreeView treeDiagrams;
        private System.Windows.Forms.ImageList imageTree;
        private System.Windows.Forms.Button buttonReload;
    }
}

