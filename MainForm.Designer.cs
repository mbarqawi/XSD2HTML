namespace WindowsFormsApp1
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.textBoxOutput = new System.Windows.Forms.TextBox();
            this.webBrowserHtml = new System.Windows.Forms.WebBrowser();
            this.tabControlDocumantion = new System.Windows.Forms.TabControl();
            this.tabPageDocuantion = new System.Windows.Forms.TabPage();
            this.tabPageOutputTabPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.listBoxXsdEle = new System.Windows.Forms.ListBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonGenrateHtml = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonCopy = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRefresh = new System.Windows.Forms.ToolStripButton();
            this.tabPageJsonDoc = new System.Windows.Forms.TabPage();
            this.textBoxJsonDoc = new System.Windows.Forms.TextBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.tabControlDocumantion.SuspendLayout();
            this.tabPageDocuantion.SuspendLayout();
            this.tabPageOutputTabPage.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabPageJsonDoc.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxOutput
            // 
            this.textBoxOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxOutput.Location = new System.Drawing.Point(3, 3);
            this.textBoxOutput.Multiline = true;
            this.textBoxOutput.Name = "textBoxOutput";
            this.textBoxOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxOutput.Size = new System.Drawing.Size(659, 448);
            this.textBoxOutput.TabIndex = 1;
            // 
            // webBrowserHtml
            // 
            this.webBrowserHtml.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserHtml.Location = new System.Drawing.Point(3, 3);
            this.webBrowserHtml.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserHtml.Name = "webBrowserHtml";
            this.webBrowserHtml.Size = new System.Drawing.Size(659, 448);
            this.webBrowserHtml.TabIndex = 3;
            // 
            // tabControlDocumantion
            // 
            this.tabControlDocumantion.Controls.Add(this.tabPageDocuantion);
            this.tabControlDocumantion.Controls.Add(this.tabPageOutputTabPage);
            this.tabControlDocumantion.Controls.Add(this.tabPageJsonDoc);
            this.tabControlDocumantion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlDocumantion.Location = new System.Drawing.Point(285, 3);
            this.tabControlDocumantion.Name = "tabControlDocumantion";
            this.tabControlDocumantion.SelectedIndex = 0;
            this.tabControlDocumantion.Size = new System.Drawing.Size(673, 480);
            this.tabControlDocumantion.TabIndex = 4;
            // 
            // tabPageDocuantion
            // 
            this.tabPageDocuantion.Controls.Add(this.webBrowserHtml);
            this.tabPageDocuantion.Location = new System.Drawing.Point(4, 22);
            this.tabPageDocuantion.Name = "tabPageDocuantion";
            this.tabPageDocuantion.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDocuantion.Size = new System.Drawing.Size(665, 454);
            this.tabPageDocuantion.TabIndex = 0;
            this.tabPageDocuantion.Text = "Documantion";
            this.tabPageDocuantion.UseVisualStyleBackColor = true;
            // 
            // tabPageOutputTabPage
            // 
            this.tabPageOutputTabPage.Controls.Add(this.textBoxOutput);
            this.tabPageOutputTabPage.Location = new System.Drawing.Point(4, 22);
            this.tabPageOutputTabPage.Name = "tabPageOutputTabPage";
            this.tabPageOutputTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOutputTabPage.Size = new System.Drawing.Size(665, 454);
            this.tabPageOutputTabPage.TabIndex = 1;
            this.tabPageOutputTabPage.Text = "Output";
            this.tabPageOutputTabPage.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 282F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tabControlDocumantion, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(961, 486);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.listBoxXsdEle, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(276, 480);
            this.tableLayoutPanel2.TabIndex = 7;
            this.tableLayoutPanel2.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel2_Paint);
            // 
            // listBoxXsdEle
            // 
            this.listBoxXsdEle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxXsdEle.FormattingEnabled = true;
            this.listBoxXsdEle.Location = new System.Drawing.Point(3, 29);
            this.listBoxXsdEle.Name = "listBoxXsdEle";
            this.listBoxXsdEle.ScrollAlwaysVisible = true;
            this.listBoxXsdEle.Size = new System.Drawing.Size(270, 448);
            this.listBoxXsdEle.TabIndex = 5;
            this.listBoxXsdEle.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxXsdEle_MouseDoubleClick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButtonGenrateHtml,
            this.toolStripButtonCopy,
            this.toolStripButtonRefresh});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(276, 25);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(40, 22);
            this.toolStripButton1.Text = "Open";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButtonGenrateHtml
            // 
            this.toolStripButtonGenrateHtml.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonGenrateHtml.Enabled = global::XSD2HTML.Properties.Settings.Default.CanGenerate;
            this.toolStripButtonGenrateHtml.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonGenrateHtml.Image")));
            this.toolStripButtonGenrateHtml.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonGenrateHtml.Name = "toolStripButtonGenrateHtml";
            this.toolStripButtonGenrateHtml.Size = new System.Drawing.Size(58, 22);
            this.toolStripButtonGenrateHtml.Text = "Generate";
            this.toolStripButtonGenrateHtml.Click += new System.EventHandler(this.toolStripButtonGenrateHtml_Click);
            // 
            // toolStripButtonCopy
            // 
            this.toolStripButtonCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonCopy.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonCopy.Image")));
            this.toolStripButtonCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCopy.Name = "toolStripButtonCopy";
            this.toolStripButtonCopy.Size = new System.Drawing.Size(39, 22);
            this.toolStripButtonCopy.Text = "Copy";
            this.toolStripButtonCopy.Click += new System.EventHandler(this.toolStripButtonCopy_Click);
            // 
            // toolStripButtonRefresh
            // 
            this.toolStripButtonRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonRefresh.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRefresh.Image")));
            this.toolStripButtonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRefresh.Name = "toolStripButtonRefresh";
            this.toolStripButtonRefresh.RightToLeftAutoMirrorImage = true;
            this.toolStripButtonRefresh.Size = new System.Drawing.Size(50, 22);
            this.toolStripButtonRefresh.Text = "Refresh";
            this.toolStripButtonRefresh.Click += new System.EventHandler(this.toolStripButtonRefresh_Click);
            // 
            // tabPageJsonDoc
            // 
            this.tabPageJsonDoc.Controls.Add(this.toolStrip2);
            this.tabPageJsonDoc.Controls.Add(this.textBoxJsonDoc);
            this.tabPageJsonDoc.Location = new System.Drawing.Point(4, 22);
            this.tabPageJsonDoc.Name = "tabPageJsonDoc";
            this.tabPageJsonDoc.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageJsonDoc.Size = new System.Drawing.Size(665, 454);
            this.tabPageJsonDoc.TabIndex = 2;
            this.tabPageJsonDoc.Text = "Elements Doc";
            this.tabPageJsonDoc.UseVisualStyleBackColor = true;
            // 
            // textBoxJsonDoc
            // 
            this.textBoxJsonDoc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxJsonDoc.Location = new System.Drawing.Point(3, 31);
            this.textBoxJsonDoc.Multiline = true;
            this.textBoxJsonDoc.Name = "textBoxJsonDoc";
            this.textBoxJsonDoc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxJsonDoc.Size = new System.Drawing.Size(662, 420);
            this.textBoxJsonDoc.TabIndex = 2;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton2});
            this.toolStrip2.Location = new System.Drawing.Point(3, 3);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(659, 25);
            this.toolStrip2.TabIndex = 3;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "toolStripButton2";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(961, 486);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "XSD to HTML";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.tabControlDocumantion.ResumeLayout(false);
            this.tabPageDocuantion.ResumeLayout(false);
            this.tabPageOutputTabPage.ResumeLayout(false);
            this.tabPageOutputTabPage.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabPageJsonDoc.ResumeLayout(false);
            this.tabPageJsonDoc.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox textBoxOutput;
        private System.Windows.Forms.WebBrowser webBrowserHtml;
        private System.Windows.Forms.TabControl tabControlDocumantion;
        private System.Windows.Forms.TabPage tabPageDocuantion;
        private System.Windows.Forms.TabPage tabPageOutputTabPage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ListBox listBoxXsdEle;
        private System.Windows.Forms.ToolStrip toolStrip1;
        //tool Strip Button Genrate Html
        private System.Windows.Forms.ToolStripButton toolStripButtonGenrateHtml;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButtonCopy;
        private System.Windows.Forms.ToolStripButton toolStripButtonRefresh;
        private System.Windows.Forms.TabPage tabPageJsonDoc;
        private System.Windows.Forms.TextBox textBoxJsonDoc;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
    }
}

