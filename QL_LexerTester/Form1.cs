using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using AQProbaCL;

namespace QL_LexerTester
{
    public partial class Form1 : Form
    {
        QLTranslatorForm toolStripContainer1 = null;
        //QMForm ParamsForm = null;

        string FileName;

        bool ScriptSaved = true;

        public Form1(string fileName, bool standingAlone = true)
        {
            toolStripContainer1 = new QLTranslatorForm(fileName);
            this.Controls.Add(this.toolStripContainer1);

            InitializeComponent();

            toolStripContainer1.CodeVisible = !btViewCode.Checked;
            toolStripContainer1.ErrorsVisible = !btViewErrors.Checked;

            //toolStripContainer1.FileNameChanged += OnFileNameChanged;
            toolStripContainer1.TextChanged += EditorTextChanged;
            toolStripContainer1.SelectionChanged += EditorSelectionChanged;

            if (File.Exists(fileName) == true)
            {
                FileName = fileName;
                Text = " QL " + " " + fileName;
                btFileSave.Enabled = true;
            }
            else
            {
                //FileName = "";
                //Text = "QL New";
                //btFileSave.Enabled = false;
                btFileNew_Click(null, null);
            }

        }

        void OnFileNameChanged(object sender, string fileName)
        {
            Text = " QL " + " " + fileName;
        }

        private void btScriptCompile_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            toolStripContainer1.Compile();
            this.Cursor = Cursors.Default;
        }

        private void btScriptParams_Click(object sender, EventArgs e)
        {
            char[] separator = { '.' };
            char[] dirSeparator = { '\\' };
            string[] dirStrings = FileName.Split(dirSeparator);
            string[] strings = dirStrings.Last<string>().Split(separator);

            int i = 0;
            string fileName = "";
            while (i < strings.Count<string>() - 1)
            {
                if (strings[i] == "script" & i == strings.Count<string>() - 2)
                {
                    fileName += "params.";
                }
                else
                {
                    fileName += strings[i]; fileName += ".";
                }

                i++;
            }
            fileName += "aqp";
            fileName = fileName.Replace('_', ' ');

            i = 0;
            string fullName = "";
            while (i < dirStrings.Count<string>() - 1)
            {
                fullName += dirStrings[i]; fullName += "\\";
                i++;
            }
            fullName += fileName;

            QMForm paramsForm = new QMForm(fullName);
            paramsForm.ShowDialog();
        }


        private void btScriptLexemes_Click(object sender, EventArgs e)
        {
            toolStripContainer1.WithLexemes = btScriptLexemes.Checked;
        }

        private void btScriptClear_Click(object sender, EventArgs e)
        {
            toolStripContainer1.ClearOutputs();
        }

        private void btFileNew_Click(object sender, EventArgs e)
        {
            toolStripContainer1.New();
            FileName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\NewScript.ql";
            
            btFileSave.Enabled = false;

            ScriptSaved = true;
        }

        private void btFileOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.AddExtension = true;
            OFD.DefaultExt = "ql";
            OFD.Filter = "ql files (*.ql)|*.ql|all files (*.*)|*.*";

            if (OFD.ShowDialog() == DialogResult.OK) toolStripContainer1.Open(OFD.FileName);

            FileName = OFD.FileName;
            Text = "QL " + OFD.FileName;
            btFileSave.Enabled = true;

            ScriptSaved = true;
        }

        private void btFileSave_Click(object sender, EventArgs e)
        {
            int errs;
            this.Cursor = Cursors.WaitCursor;
            toolStripContainer1.SaveScript();
            if ((errs = toolStripContainer1.Compile()) == 0) toolStripContainer1.SaveCode();
            else MessageBox.Show("There are " + errs.ToString() + " errors; the script has been saved", "Warning", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            this.Cursor = Cursors.Default;

            ScriptSaved = true;
        }

        private void btFileSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog SFD = new SaveFileDialog();
            SFD.InitialDirectory = new FileInfo(FileName).DirectoryName;
            SFD.AddExtension = true;
            SFD.DefaultExt = "ql";
            SFD.Filter = "ql files (*.ql)|*.ql|all files (*.*)|*.*";
            if (SFD.ShowDialog() == DialogResult.OK)
            {
                int errs;
                this.Cursor = Cursors.WaitCursor;
                toolStripContainer1.SaveScriptAs(SFD.FileName);
                if ((errs = toolStripContainer1.Compile()) == 0) toolStripContainer1.SaveCode();
                else MessageBox.Show("There are " + errs.ToString() + " errors; the script has been saved", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Text = "QL " + SFD.FileName;
                FileName = SFD.FileName;
                this.Cursor = Cursors.Default;
            }

            ScriptSaved = true;

        }

        private void btFileExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btEditUndo_Click(object sender, EventArgs e)
        {
            toolStripContainer1.EditUndo();
        }

        private void btEditRedo_Click(object sender, EventArgs e)
        {
            
            toolStripContainer1.EditRedo();
        }

        private void btEditCut_Click(object sender, EventArgs e)
        {
            toolStripContainer1.EditCut();
        }

        private void btEditCopy_Click(object sender, EventArgs e)
        {
            toolStripContainer1.EditCopy();
        }

        private void btEditPaste_Click(object sender, EventArgs e)
        {
            toolStripContainer1.EditPaste();
        }

        private void btEditSelectAll_Click(object sender, EventArgs e)
        {
            toolStripContainer1.EditSelectAll();
        }

        private void btEditFind_Click(object sender, EventArgs e)
        {
            toolStripContainer1.EditFind();
        }

        private void btEditReplace_Click(object sender, EventArgs e)
        {
            toolStripContainer1.EditFind();
        }

        private void btEditGoto_Click(object sender, EventArgs e)
        {
            toolStripContainer1.EditGoto();
        }

        private void btViewCode_Click(object sender, EventArgs e)
        {
            toolStripContainer1.CodeVisible = !toolStripContainer1.CodeVisible;
            btViewCode.Checked = !toolStripContainer1.CodeVisible;
            btlCode.Checked = !toolStripContainer1.CodeVisible;
        }

        private void btViewErrors_Click(object sender, EventArgs e)
        {
            toolStripContainer1.ErrorsVisible = !toolStripContainer1.ErrorsVisible;
            btViewErrors.Checked = !toolStripContainer1.ErrorsVisible;
            btlErrors.Checked = !toolStripContainer1.ErrorsVisible;
        }

        private void EditorTextChanged(object sender, EventArgs e)
        {
            btEditUndo.Enabled = ((ScintillaNET.Scintilla)sender).UndoRedo.CanUndo;
            btEditRedo.Enabled = ((ScintillaNET.Scintilla)sender).UndoRedo.CanRedo;
            btlEditUndo.Enabled = ((ScintillaNET.Scintilla)sender).UndoRedo.CanUndo;
            btlEditRedo.Enabled = ((ScintillaNET.Scintilla)sender).UndoRedo.CanRedo;

            ScriptSaved = false;
        }

        private void EditorSelectionChanged(object sender, EventArgs e)
        {
            btEditCut.Enabled = ((ScintillaNET.Scintilla)sender).Clipboard.CanCut;
            btEditCopy.Enabled = ((ScintillaNET.Scintilla)sender).Clipboard.CanCopy;
            btEditPaste.Enabled = ((ScintillaNET.Scintilla)sender).Clipboard.CanPaste;
            btlEditCut.Enabled = ((ScintillaNET.Scintilla)sender).Clipboard.CanCut;
            btlEditCopy.Enabled = ((ScintillaNET.Scintilla)sender).Clipboard.CanCopy;
            btlEditPaste.Enabled = ((ScintillaNET.Scintilla)sender).Clipboard.CanPaste;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ScriptSaved == false)
            {
                if (MessageBox.Show("Save changes?", "Question",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    toolStripContainer1.Compile();
                    toolStripContainer1.SaveScript();
                }
            }
        }

    }
}
