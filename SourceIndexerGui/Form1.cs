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

    private void EvaluateResults(SourceIndexer sourceIndexer)
    {
      var pdbPath = this.PdbPathTextBox.Text;
      var debugStreams = sourceIndexer.GetDebugStreams(pdbPath);
      if(debugStreams == null)
      {
        debugStreams = new SourceIndexerDebugStreams();
        sourceIndexer.ExtractDebugStreams(pdbPath, debugStreams);
      }
      
      this.EvaluatedRichTextBox.Text = this.NormalizeNewlines(debugStreams.EvaluatedResults);
      this.StreamRichTextBox.Text = this.NormalizeNewlines(debugStreams.RawResults);
      this.UnindexedRichTextBox.Text = this.NormalizeNewlines(debugStreams.UnindexedResults);
    }
    string NormalizeNewlines(string data)
    {
      return Regex.Replace(data, @"\r\n|\n\r|\n|\r", "\r\n");
    }

    private SourceIndexer CreateSourceIndexer()
    {
      var config = new SourceIndexerConfig();
      config.ExtractDebugStreams = true;
      config.FastMode = true;
      config.SourceRoot = this.SourceRootTextBox.Text;
      var indexer = new SourceIndexer();
      indexer.Config = config;
      indexer.PdbPaths.Add(this.PdbPathTextBox.Text);
      indexer.FrontEnd = new GitFrontEnd();
      indexer.BackEnd = BackendComboBox.SelectedItem as IBackEnd;
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

    private void Form1_Load(object sender, EventArgs e)
    {
      this.BackendComboBox.Items.Add(new CmdBackEnd());
      this.BackendComboBox.Items.Add(new GitBackEnd());
      this.BackendComboBox.Items.Add(new GitHubBackEnd());
      this.BackendComboBox.SelectedIndex = 0;
    }
  }
}
