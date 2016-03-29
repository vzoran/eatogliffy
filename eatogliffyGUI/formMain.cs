using eatogliffy.gliffy.io;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eatogliffyGUI
{
    public partial class formMain : Form
    {
        private EaManager eaManager = new EaManager();

        public formMain()
        {
            InitializeComponent();
        }

        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textSourceFile.Text = openFileDialog.FileName;
                refreshList(openFileDialog.FileName);
            }
        }

        private void buttonTargetFile_Click(object sender, EventArgs e)
        {
            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                textTargetFile.Text = saveFileDialog.FileName;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void refreshList(string sourceFile)
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                treeDiagrams.Nodes.Clear();

                List<EaObject> diagramList = eaManager
                    .openFile(sourceFile)
                    .getDiagramList();

                foreach (EaObject entry in diagramList)
                {
                    if (entry.ParentId != null)
                    {
                        TreeNode[] parentNodes = treeDiagrams.Nodes.Find(entry.ParentId, true);
                        if (parentNodes.Length > 0)
                        {
                            parentNodes[0].Nodes.Add(entry.Id, entry.Name, (entry.IsDiagram ? 1 : 0), (entry.IsDiagram ? 1 : 0));
                        }
                    }
                    else
                    {
                        treeDiagrams.Nodes.Add(entry.Id, entry.Name, (entry.IsDiagram ? 1 : 0), (entry.IsDiagram ? 1 : 0));
                    }
                }

                treeDiagrams.ExpandAll();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void formMain_Deactivate(object sender, EventArgs e)
        {
            //
        }

        private void formMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            eaManager.closeFile();

            Properties.Settings.Default.SourcePath = textSourceFile.Text;
            Properties.Settings.Default.TargetPath = textTargetFile.Text;
            Properties.Settings.Default.Save();
        }

        private void formMain_Load(object sender, EventArgs e)
        {
            textSourceFile.Text = Properties.Settings.Default.SourcePath;
            textTargetFile.Text = Properties.Settings.Default.TargetPath;
        }

        private void buttonReload_Click(object sender, EventArgs e)
        {
            refreshList(textSourceFile.Text);
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if(treeDiagrams.SelectedNode != null)
            {
                string jsonResult = eaManager.ConvertDiagram(treeDiagrams.SelectedNode.Name);
                Clipboard.SetText(jsonResult);
                System.IO.File.WriteAllText(textTargetFile.Text, jsonResult);
            }
        }
    }
}
