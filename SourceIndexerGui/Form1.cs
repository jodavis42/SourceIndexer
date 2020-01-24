using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SourceIndexer
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }

    private void PdbPathTextBox_TextChanged(object sender, EventArgs e)
    {

    }

    private void PdbPath_Click(object sender, EventArgs e)
    {

    }

    private void RunButton_Click(object sender, EventArgs e)
    {
      var indexer = CreateSourceIndexer();
      indexer.RunSourceIndexing();
      EvaluateResults(indexer);
    }

    private void EvaluateButton_Click(object sender, EventArgs e)
    {
      var indexer = CreateSourceIndexer();
      EvaluateResults(indexer);
    }

    private void EvaluateResults(ISourceIndexer sourceIndexer)
    {
      this.EvaluatedRichTextBox.Text = this.NormalizeNewlines(sourceIndexer.EvaluateSourceIndexing());
      this.StreamRichTextBox.Text = this.NormalizeNewlines(sourceIndexer.GetSourceIndexingResults());
      this.UnindexedRichTextBox.Text = this.NormalizeNewlines(sourceIndexer.GetUnindexedList());
    }
    string NormalizeNewlines(string data)
    {
      return Regex.Replace(data, @"\r\n|\n\r|\n|\r", "\r\n");
    }

    private ISourceIndexer CreateSourceIndexer()
    {
      var indexer = new GitHubSourceIndexer();
      indexer.FullPdbPath = this.PdbPathTextBox.Text;
      indexer.SetSourceRoot(this.SourceRootTextBox.Text);
      return indexer;
    }

    private void OnDragDrop(object sender, DragEventArgs e)
    {
      string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
      TextBox textBox = (TextBox)sender;
      textBox.Text = files[0];
    }

    private void OnDragEnter(object sender, DragEventArgs e)
    {
      e.Effect = DragDropEffects.All;
    }
  }
}
