using System;

namespace MdxQueryEditor
{
    partial class MDXParserOptions
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
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Format");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("MDX", new System.Windows.Forms.TreeNode[] {
            treeNode3});
            this.buttonOK = new System.Windows.Forms.Button();
            this.panelFormat = new System.Windows.Forms.Panel();
            this.groupBoxColoring = new System.Windows.Forms.GroupBox();
            this.checkBoxColorFunction = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownIndent = new System.Windows.Forms.NumericUpDown();
            this.groupBoxLists = new System.Windows.Forms.GroupBox();
            this.radioButtonNLBeforeComma = new System.Windows.Forms.RadioButton();
            this.radioButtonNLAfterComma = new System.Windows.Forms.RadioButton();
            this.treeViewOptions = new System.Windows.Forms.TreeView();
            this.panelFormat.SuspendLayout();
            this.groupBoxColoring.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIndent)).BeginInit();
            this.groupBoxLists.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonOK.Location = new System.Drawing.Point(521, 274);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 5;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // panelFormat
            // 
            this.panelFormat.Controls.Add(this.groupBoxColoring);
            this.panelFormat.Controls.Add(this.label1);
            this.panelFormat.Controls.Add(this.numericUpDownIndent);
            this.panelFormat.Controls.Add(this.groupBoxLists);
            this.panelFormat.Location = new System.Drawing.Point(237, 17);
            this.panelFormat.Name = "panelFormat";
            this.panelFormat.Size = new System.Drawing.Size(359, 235);
            this.panelFormat.TabIndex = 4;
            // 
            // groupBoxColoring
            // 
            this.groupBoxColoring.Controls.Add(this.checkBoxColorFunction);
            this.groupBoxColoring.Location = new System.Drawing.Point(17, 161);
            this.groupBoxColoring.Name = "groupBoxColoring";
            this.groupBoxColoring.Size = new System.Drawing.Size(324, 60);
            this.groupBoxColoring.TabIndex = 6;
            this.groupBoxColoring.TabStop = false;
            this.groupBoxColoring.Text = "Coloring";
            // 
            // checkBoxColorFunction
            // 
            this.checkBoxColorFunction.AutoSize = true;
            this.checkBoxColorFunction.Location = new System.Drawing.Point(11, 28);
            this.checkBoxColorFunction.Name = "checkBoxColorFunction";
            this.checkBoxColorFunction.Size = new System.Drawing.Size(125, 17);
            this.checkBoxColorFunction.TabIndex = 5;
            this.checkBoxColorFunction.Text = "Color function names";
            this.checkBoxColorFunction.UseVisualStyleBackColor = true;
            this.checkBoxColorFunction.CheckedChanged += new System.EventHandler(this.checkBoxColorFunction_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Indent size";
            // 
            // numericUpDownIndent
            // 
            this.numericUpDownIndent.Location = new System.Drawing.Point(28, 126);
            this.numericUpDownIndent.Name = "numericUpDownIndent";
            this.numericUpDownIndent.Size = new System.Drawing.Size(44, 20);
            this.numericUpDownIndent.TabIndex = 3;
            this.numericUpDownIndent.ValueChanged += new System.EventHandler(this.numericUpDownIndent_ValueChanged);
            // 
            // groupBoxLists
            // 
            this.groupBoxLists.Controls.Add(this.radioButtonNLBeforeComma);
            this.groupBoxLists.Controls.Add(this.radioButtonNLAfterComma);
            this.groupBoxLists.Location = new System.Drawing.Point(14, 14);
            this.groupBoxLists.Name = "groupBoxLists";
            this.groupBoxLists.Size = new System.Drawing.Size(327, 77);
            this.groupBoxLists.TabIndex = 2;
            this.groupBoxLists.TabStop = false;
            this.groupBoxLists.Text = "Multiline list formatting";
            // 
            // radioButtonNLBeforeComma
            // 
            this.radioButtonNLBeforeComma.AutoSize = true;
            this.radioButtonNLBeforeComma.Checked = true;
            this.radioButtonNLBeforeComma.Location = new System.Drawing.Point(14, 19);
            this.radioButtonNLBeforeComma.Name = "radioButtonNLBeforeComma";
            this.radioButtonNLBeforeComma.Size = new System.Drawing.Size(188, 17);
            this.radioButtonNLBeforeComma.TabIndex = 0;
            this.radioButtonNLBeforeComma.TabStop = true;
            this.radioButtonNLBeforeComma.Text = "Comma at the beginning of the line";
            this.radioButtonNLBeforeComma.UseVisualStyleBackColor = true;
            this.radioButtonNLBeforeComma.CheckedChanged += new System.EventHandler(this.radioButtonNLBeforeComma_CheckedChanged);
            // 
            // radioButtonNLAfterComma
            // 
            this.radioButtonNLAfterComma.AutoSize = true;
            this.radioButtonNLAfterComma.Location = new System.Drawing.Point(14, 42);
            this.radioButtonNLAfterComma.Name = "radioButtonNLAfterComma";
            this.radioButtonNLAfterComma.Size = new System.Drawing.Size(160, 17);
            this.radioButtonNLAfterComma.TabIndex = 1;
            this.radioButtonNLAfterComma.Text = "Comma at the end of the line";
            this.radioButtonNLAfterComma.UseVisualStyleBackColor = true;
            this.radioButtonNLAfterComma.CheckedChanged += new System.EventHandler(this.radioButtonNLAfterComma_CheckedChanged);
            // 
            // treeViewOptions
            // 
            this.treeViewOptions.Location = new System.Drawing.Point(14, 14);
            this.treeViewOptions.Name = "treeViewOptions";
            treeNode3.Name = "optionsNodeFormat";
            treeNode3.Text = "Format";
            treeNode4.Name = "optionsNodeMDX";
            treeNode4.Text = "MDX";
            this.treeViewOptions.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4});
            this.treeViewOptions.Size = new System.Drawing.Size(203, 283);
            this.treeViewOptions.TabIndex = 0;
            // 
            // Options
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonOK;
            this.ClientSize = new System.Drawing.Size(610, 311);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.panelFormat);
            this.Controls.Add(this.treeViewOptions);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Options";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Options";
            this.panelFormat.ResumeLayout(false);
            this.panelFormat.PerformLayout();
            this.groupBoxColoring.ResumeLayout(false);
            this.groupBoxColoring.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIndent)).EndInit();
            this.groupBoxLists.ResumeLayout(false);
            this.groupBoxLists.PerformLayout();
            this.ResumeLayout(false);

        }



        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Panel panelFormat;
        private System.Windows.Forms.GroupBox groupBoxColoring;
        private System.Windows.Forms.CheckBox checkBoxColorFunction;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownIndent;
        private System.Windows.Forms.GroupBox groupBoxLists;
        private System.Windows.Forms.RadioButton radioButtonNLBeforeComma;
        private System.Windows.Forms.RadioButton radioButtonNLAfterComma;
        private System.Windows.Forms.TreeView treeViewOptions;

    }
}