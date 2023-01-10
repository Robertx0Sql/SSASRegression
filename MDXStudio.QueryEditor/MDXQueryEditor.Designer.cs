

namespace MDXStudio.QueryEditor
{


    partial class MDXQueryEditor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MDXQueryEditor));
            this.imageListIntellisense = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.mdxEditor = new MDXStudio.QueryEditor.MdxEditor(this.components);
            this.autoCompleteList = new MDXStudio.AutoCompleteList();
            this.messagesControl1 = new MDXStudio.MessagesControl();
           
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageListIntellisense
            // 
            this.imageListIntellisense.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListIntellisense.ImageStream")));
            this.imageListIntellisense.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListIntellisense.Images.SetKeyName(0, "DbDimension.bmp");
            this.imageListIntellisense.Images.SetKeyName(1, "Hierarchy.bmp");
            this.imageListIntellisense.Images.SetKeyName(2, "Level2.bmp");
            this.imageListIntellisense.Images.SetKeyName(3, "Measure.bmp");
            this.imageListIntellisense.Images.SetKeyName(4, "MeasureCalculated.bmp");
            this.imageListIntellisense.Images.SetKeyName(5, "Member.bmp");
            this.imageListIntellisense.Images.SetKeyName(6, "Cube.bmp");
            this.imageListIntellisense.Images.SetKeyName(7, "Function.bmp");
            this.imageListIntellisense.Images.SetKeyName(8, "Property.bmp");
            this.imageListIntellisense.Images.SetKeyName(9, "Keyword.bmp");
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.mdxEditor);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.messagesControl1);
            this.splitContainer1.Size = new System.Drawing.Size(336, 297);
            this.splitContainer1.SplitterDistance = 185;
            this.splitContainer1.TabIndex = 7;
            // 
            // mdxEditor
            // 
            this.mdxEditor.AllowDrop = true;
            this.mdxEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mdxEditor.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mdxEditor.Location = new System.Drawing.Point(0, 0);
            this.mdxEditor.Name = "mdxEditor";
            this.mdxEditor.Size = new System.Drawing.Size(336, 185);
            this.mdxEditor.TabIndex = 0;
            this.mdxEditor.Text = "";
            this.mdxEditor.WordWrap = false;
            this.mdxEditor.TextChanged += new System.EventHandler(this.mdxEditor_TextChanged);
            this.mdxEditor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mdxEditor_KeyDown);
            this.mdxEditor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mdxEditor_KeyPress);
            this.mdxEditor.LostFocus += new System.EventHandler(this.mdxEditor_LostFocus);
            this.mdxEditor.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mdxEditor_MouseDown);
            // 
            // autoCompleteList
            // 
            this.autoCompleteList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.autoCompleteList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.autoCompleteList.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoCompleteList.FormattingEnabled = true;
            this.autoCompleteList.ImageList = this.imageListIntellisense;
            this.autoCompleteList.ItemHeight = 18;
            this.autoCompleteList.Location = new System.Drawing.Point(60, 45);
            this.autoCompleteList.Name = "autoCompleteList";
            this.autoCompleteList.Size = new System.Drawing.Size(173, 182);
            this.autoCompleteList.Sorted = true;
            this.autoCompleteList.TabIndex = 6;
            this.autoCompleteList.UseTabStops = false;
            this.autoCompleteList.Visible = false;
            // 
            // messagesControl1
            // 
            this.messagesControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messagesControl1.Location = new System.Drawing.Point(0, 0);
            this.messagesControl1.Name = "messagesControl1";
            this.messagesControl1.Size = new System.Drawing.Size(336, 108);
            this.messagesControl1.TabIndex = 0;
            // 
            // MDXQueryEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.autoCompleteList);
            this.Name = "MDXQueryEditor";
            this.Size = new System.Drawing.Size(336, 297);
            this.Load += new System.EventHandler(this.QueryWindowUC_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private QueryEditor.MdxEditor mdxEditor;
        private AutoCompleteList autoCompleteList;
        private System.Windows.Forms.ImageList imageListIntellisense;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private MessagesControl messagesControl1;
    
    }
}
