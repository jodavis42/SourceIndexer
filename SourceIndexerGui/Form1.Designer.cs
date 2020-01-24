namespace SourceIndexer
{
  partial class Form1
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
      this.PdbPath = new System.Windows.Forms.Label();
      this.PdbPathTextBox = new System.Windows.Forms.TextBox();
      this.EvaluatedRichTextBox = new System.Windows.Forms.RichTextBox();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.StreamRichTextBox = new System.Windows.Forms.RichTextBox();
      this.tabPage3 = new System.Windows.Forms.TabPage();
      this.UnindexedRichTextBox = new System.Windows.Forms.RichTextBox();
      this.RunButton = new System.Windows.Forms.Button();
      this.EvaluateButton = new System.Windows.Forms.Button();
      this.panel1 = new System.Windows.Forms.Panel();
      this.label1 = new System.Windows.Forms.Label();
      this.SourceRootTextBox = new System.Windows.Forms.TextBox();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.tabPage3.SuspendLayout();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // PdbPath
      // 
      this.PdbPath.AutoSize = true;
      this.PdbPath.Location = new System.Drawing.Point(302, 29);
      this.PdbPath.Name = "PdbPath";
      this.PdbPath.Size = new System.Drawing.Size(48, 13);
      this.PdbPath.TabIndex = 0;
      this.PdbPath.Text = "PdbPath";
      this.PdbPath.Click += new System.EventHandler(this.PdbPath_Click);
      // 
      // PdbPathTextBox
      // 
      this.PdbPathTextBox.AllowDrop = true;
      this.PdbPathTextBox.Location = new System.Drawing.Point(285, 45);
      this.PdbPathTextBox.Name = "PdbPathTextBox";
      this.PdbPathTextBox.Size = new System.Drawing.Size(100, 20);
      this.PdbPathTextBox.TabIndex = 1;
      this.PdbPathTextBox.Text = "\"E:\\Code\\Repos\\AzureCrashProject\\CMake\\Generated\\VS2017_MSVC_Windows\\src\\Debug\\Pr" +
    "oject.pdb\"";
      this.PdbPathTextBox.TextChanged += new System.EventHandler(this.PdbPathTextBox_TextChanged);
      this.PdbPathTextBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnDragDrop);
      this.PdbPathTextBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnDragEnter);
      // 
      // EvaluatedRichTextBox
      // 
      this.EvaluatedRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.EvaluatedRichTextBox.Location = new System.Drawing.Point(3, 3);
      this.EvaluatedRichTextBox.Name = "EvaluatedRichTextBox";
      this.EvaluatedRichTextBox.Size = new System.Drawing.Size(786, 272);
      this.EvaluatedRichTextBox.TabIndex = 2;
      this.EvaluatedRichTextBox.Text = "";
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Controls.Add(this.tabPage2);
      this.tabControl1.Controls.Add(this.tabPage3);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl1.Location = new System.Drawing.Point(0, 100);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(800, 304);
      this.tabControl1.TabIndex = 3;
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.EvaluatedRichTextBox);
      this.tabPage1.Location = new System.Drawing.Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(792, 278);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Evaluated";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // tabPage2
      // 
      this.tabPage2.Controls.Add(this.StreamRichTextBox);
      this.tabPage2.Location = new System.Drawing.Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(792, 278);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "Stream";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // StreamRichTextBox
      // 
      this.StreamRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.StreamRichTextBox.Location = new System.Drawing.Point(3, 3);
      this.StreamRichTextBox.Name = "StreamRichTextBox";
      this.StreamRichTextBox.Size = new System.Drawing.Size(786, 272);
      this.StreamRichTextBox.TabIndex = 3;
      this.StreamRichTextBox.Text = "";
      // 
      // tabPage3
      // 
      this.tabPage3.Controls.Add(this.UnindexedRichTextBox);
      this.tabPage3.Location = new System.Drawing.Point(4, 22);
      this.tabPage3.Name = "tabPage3";
      this.tabPage3.Size = new System.Drawing.Size(792, 278);
      this.tabPage3.TabIndex = 2;
      this.tabPage3.Text = "Unindexed";
      this.tabPage3.UseVisualStyleBackColor = true;
      // 
      // UnindexedRichTextBox
      // 
      this.UnindexedRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.UnindexedRichTextBox.Location = new System.Drawing.Point(0, 0);
      this.UnindexedRichTextBox.Name = "UnindexedRichTextBox";
      this.UnindexedRichTextBox.Size = new System.Drawing.Size(792, 278);
      this.UnindexedRichTextBox.TabIndex = 3;
      this.UnindexedRichTextBox.Text = "";
      // 
      // RunButton
      // 
      this.RunButton.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.RunButton.Location = new System.Drawing.Point(0, 404);
      this.RunButton.Name = "RunButton";
      this.RunButton.Size = new System.Drawing.Size(800, 23);
      this.RunButton.TabIndex = 4;
      this.RunButton.Text = "Run";
      this.RunButton.UseVisualStyleBackColor = true;
      this.RunButton.Click += new System.EventHandler(this.RunButton_Click);
      // 
      // EvaluateButton
      // 
      this.EvaluateButton.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.EvaluateButton.Location = new System.Drawing.Point(0, 427);
      this.EvaluateButton.Name = "EvaluateButton";
      this.EvaluateButton.Size = new System.Drawing.Size(800, 23);
      this.EvaluateButton.TabIndex = 4;
      this.EvaluateButton.Text = "Evaluate";
      this.EvaluateButton.UseVisualStyleBackColor = true;
      this.EvaluateButton.Click += new System.EventHandler(this.EvaluateButton_Click);
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.label1);
      this.panel1.Controls.Add(this.PdbPath);
      this.panel1.Controls.Add(this.SourceRootTextBox);
      this.panel1.Controls.Add(this.PdbPathTextBox);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(800, 100);
      this.panel1.TabIndex = 5;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(60, 29);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(67, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Source Root";
      // 
      // SourceRootTextBox
      // 
      this.SourceRootTextBox.AllowDrop = true;
      this.SourceRootTextBox.Location = new System.Drawing.Point(43, 45);
      this.SourceRootTextBox.Name = "SourceRootTextBox";
      this.SourceRootTextBox.Size = new System.Drawing.Size(225, 20);
      this.SourceRootTextBox.TabIndex = 1;
      this.SourceRootTextBox.Text = "E:\\Code\\Repos\\AzureCrashProject";
      this.SourceRootTextBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnDragDrop);
      this.SourceRootTextBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnDragEnter);
      // 
      // Form1
      // 
      this.AllowDrop = true;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(800, 450);
      this.Controls.Add(this.tabControl1);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.RunButton);
      this.Controls.Add(this.EvaluateButton);
      this.Name = "Form1";
      this.Text = "Form1";
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.tabPage2.ResumeLayout(false);
      this.tabPage3.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label PdbPath;
    private System.Windows.Forms.TextBox PdbPathTextBox;
    private System.Windows.Forms.RichTextBox EvaluatedRichTextBox;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.Button RunButton;
    private System.Windows.Forms.Button EvaluateButton;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox SourceRootTextBox;
    private System.Windows.Forms.RichTextBox StreamRichTextBox;
    private System.Windows.Forms.TabPage tabPage3;
    private System.Windows.Forms.RichTextBox UnindexedRichTextBox;
  }
}

