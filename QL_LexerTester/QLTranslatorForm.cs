using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace QL_LexerTester
{
    class QLTranslatorForm : ToolStripContainer
    {
        private System.Windows.Forms.SplitContainer spltConTripleEditor;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox rteQL_LT_out_line;
        private System.Windows.Forms.RichTextBox rteQL_LT_error_line;
        //private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel1;
        private System.Windows.Forms.ToolStripPanel TopToolStripPanel1;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel1;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel1;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lb_in_line_Position;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Start;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Clear;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_SaveScriptMenu;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_SaveCodeMenu;
        private ScintillaNET.Scintilla rteQL_LT_in_line;
        private System.Windows.Forms.ToolStripComboBox toolStripWithLexemes;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_SaveScript;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_SaveScriptAs;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_SaveCode;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_SaveCodeAs;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripStatusLabel ErrorsStatusLabel;

        void IntializeComponent()
        {
            this.BottomToolStripPanel1 = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel1 = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel1 = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel1 = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel1 = new System.Windows.Forms.ToolStripContentPanel();
            this.spltConTripleEditor = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.rteQL_LT_in_line = new ScintillaNET.Scintilla();
            this.rteQL_LT_out_line = new System.Windows.Forms.RichTextBox();
            this.rteQL_LT_error_line = new System.Windows.Forms.RichTextBox();
            //this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lb_in_line_Position = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MenuItem_Start = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Clear = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_SaveScriptMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_SaveScript = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_SaveScriptAs = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_SaveCodeMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_SaveCode = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_SaveCodeAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripWithLexemes = new System.Windows.Forms.ToolStripComboBox();
            //this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.ErrorsStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.spltConTripleEditor)).BeginInit();
            this.spltConTripleEditor.Panel1.SuspendLayout();
            this.spltConTripleEditor.Panel2.SuspendLayout();
            this.spltConTripleEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rteQL_LT_in_line)).BeginInit();
            this./*toolStripContainer1.*/BottomToolStripPanel.SuspendLayout();
            this./*toolStripContainer1.*/ContentPanel.SuspendLayout();
            this./*toolStripContainer1.*/TopToolStripPanel.SuspendLayout();
            this./*toolStripContainer1.*/SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BottomToolStripPanel1
            // 
            this.BottomToolStripPanel1.Location = new System.Drawing.Point(0, 0);
            this.BottomToolStripPanel1.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel1.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BottomToolStripPanel1.Size = new System.Drawing.Size(0, 0);
            // 
            // TopToolStripPanel1
            // 
            this.TopToolStripPanel1.Location = new System.Drawing.Point(0, 0);
            this.TopToolStripPanel1.Name = "TopToolStripPanel";
            this.TopToolStripPanel1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel1.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.TopToolStripPanel1.Size = new System.Drawing.Size(0, 0);
            // 
            // RightToolStripPanel1
            // 
            this.RightToolStripPanel1.Location = new System.Drawing.Point(0, 0);
            this.RightToolStripPanel1.Name = "RightToolStripPanel";
            this.RightToolStripPanel1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel1.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.RightToolStripPanel1.Size = new System.Drawing.Size(0, 0);
            // 
            // LeftToolStripPanel1
            // 
            this.LeftToolStripPanel1.Location = new System.Drawing.Point(0, 0);
            this.LeftToolStripPanel1.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel1.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.LeftToolStripPanel1.Size = new System.Drawing.Size(0, 0);
            // 
            // ContentPanel1
            // 
            this.ContentPanel1.AutoScroll = true;
            this.ContentPanel1.Size = new System.Drawing.Size(1909, 1177);
            // 
            // spltConTripleEditor
            // 
            this.spltConTripleEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spltConTripleEditor.Location = new System.Drawing.Point(0, 0);
            this.spltConTripleEditor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.spltConTripleEditor.Name = "spltConTripleEditor";
            this.spltConTripleEditor.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spltConTripleEditor.Panel1
            // 
            this.spltConTripleEditor.Panel1.Controls.Add(this.splitContainer1);
            // 
            // spltConTripleEditor.Panel2
            // 
            this.spltConTripleEditor.Panel2.Controls.Add(this.rteQL_LT_error_line);
            this.spltConTripleEditor.Size = new System.Drawing.Size(1783, 888);
            this.spltConTripleEditor.SplitterDistance = 747;
            this.spltConTripleEditor.SplitterWidth = 5;
            this.spltConTripleEditor.TabIndex = 6;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.rteQL_LT_in_line);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.rteQL_LT_out_line);
            this.splitContainer1.Size = new System.Drawing.Size(1783, 747);
            this.splitContainer1.SplitterDistance = 354;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 6;
            // 
            // rteQL_LT_in_line
            // 
            this.rteQL_LT_in_line.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rteQL_LT_in_line.Folding.Flags = ScintillaNET.FoldFlag.LevelNumbers;
            this.rteQL_LT_in_line.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rteQL_LT_in_line.Location = new System.Drawing.Point(0, 0);
            this.rteQL_LT_in_line.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.rteQL_LT_in_line.Margins.Margin1.Width = 0;
            this.rteQL_LT_in_line.Margins.Margin2.Width = 20;
            this.rteQL_LT_in_line.Name = "rteQL_LT_in_line";
            this.rteQL_LT_in_line.Size = new System.Drawing.Size(354, 747);
            this.rteQL_LT_in_line.Styles.BraceBad.FontName = "Verdana\0";
            this.rteQL_LT_in_line.Styles.BraceBad.Size = 10F;
            this.rteQL_LT_in_line.Styles.BraceLight.FontName = "Verdana\0";
            this.rteQL_LT_in_line.Styles.BraceLight.Size = 10F;
            this.rteQL_LT_in_line.Styles.CallTip.FontName = "Tahoma\0\0";
            this.rteQL_LT_in_line.Styles.ControlChar.FontName = "Verdana\0";
            this.rteQL_LT_in_line.Styles.ControlChar.ForeColor = System.Drawing.Color.Aqua;
            this.rteQL_LT_in_line.Styles.ControlChar.Size = 10F;
            this.rteQL_LT_in_line.Styles.Default.BackColor = System.Drawing.SystemColors.Window;
            this.rteQL_LT_in_line.Styles.Default.CharacterSet = ScintillaNET.CharacterSet.Russian;
            this.rteQL_LT_in_line.Styles.Default.FontName = "Consolas";
            this.rteQL_LT_in_line.Styles.Default.Size = 8.25F;
            this.rteQL_LT_in_line.Styles.IndentGuide.FontName = "Verdana\0";
            this.rteQL_LT_in_line.Styles.IndentGuide.Size = 10F;
            this.rteQL_LT_in_line.Styles.LastPredefined.FontName = "Verdana\0";
            this.rteQL_LT_in_line.Styles.LastPredefined.Size = 10F;
            this.rteQL_LT_in_line.Styles.LineNumber.FontName = "Verdana\0";
            this.rteQL_LT_in_line.Styles.LineNumber.Size = 10F;
            this.rteQL_LT_in_line.Styles.Max.FontName = "Verdana\0";
            this.rteQL_LT_in_line.Styles.Max.Size = 10F;
            this.rteQL_LT_in_line.TabIndex = 0;
            this.rteQL_LT_in_line.SelectionChanged += new System.EventHandler(this.rteQL_LT_in_line_SelectionChanged);
            this.rteQL_LT_in_line.TextChanged += new System.EventHandler(this.rteQL_LT_in_line_TextChanged);
            // 
            // rteQL_LT_out_line
            // 
            this.rteQL_LT_out_line.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rteQL_LT_out_line.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rteQL_LT_out_line.Location = new System.Drawing.Point(0, 0);
            this.rteQL_LT_out_line.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.rteQL_LT_out_line.Name = "rteQL_LT_out_line";
            this.rteQL_LT_out_line.Size = new System.Drawing.Size(1424, 747);
            this.rteQL_LT_out_line.TabIndex = 7;
            this.rteQL_LT_out_line.Text = "";
            this.rteQL_LT_out_line.WordWrap = false;
            // 
            // rteQL_LT_error_line
            // 
            this.rteQL_LT_error_line.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rteQL_LT_error_line.Location = new System.Drawing.Point(0, 0);
            this.rteQL_LT_error_line.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rteQL_LT_error_line.Name = "rteQL_LT_error_line";
            this.rteQL_LT_error_line.Size = new System.Drawing.Size(1783, 136);
            this.rteQL_LT_error_line.TabIndex = 0;
            this.rteQL_LT_error_line.Text = "";
            this.rteQL_LT_error_line.WordWrap = false;
            // 
            // toolStripContainer1
            // 
            this./*toolStripContainer1.*/Dock = System.Windows.Forms.DockStyle.Fill;
            this./*toolStripContainer1.*/LeftToolStripPanelVisible = false;
            this./*toolStripContainer1.*/Location = new System.Drawing.Point(0, 0);
            this./*toolStripContainer1.*/Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this./*toolStripContainer1.*/Name = "toolStripContainer1";
            this./*toolStripContainer1.*/RightToolStripPanelVisible = false;
            this./*toolStripContainer1.*/Size = new System.Drawing.Size(17830, 987);
            this./*toolStripContainer1.*/TabIndex = 7;
            this./*toolStripContainer1.*/Text = "toolStripContainer1";
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this./*toolStripContainer1.*/BottomToolStripPanel.Controls.Add(this.statusStrip1);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this./*toolStripContainer1.*/ContentPanel.AutoScroll = true;
            this./*toolStripContainer1.*/ContentPanel.Controls.Add(this.spltConTripleEditor);
            this./*toolStripContainer1.*/ContentPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this./*toolStripContainer1.*/ContentPanel.Size = new System.Drawing.Size(1783, 888);
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this./*toolStripContainer1.*/TopToolStripPanel.Controls.Add(this.menuStrip1);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lb_in_line_Position, /*toolStripSplitButton1,*/ ErrorsStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1783, 46);
            this.statusStrip1.TabIndex = 0;
            // 
            // lb_in_line_Position
            // 
            this.lb_in_line_Position.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lb_in_line_Position.Name = "lb_in_line_Position";
            this.lb_in_line_Position.Size = new System.Drawing.Size(57, 41);
            this.lb_in_line_Position.Text = "0,0";
            this.lb_in_line_Position.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            //this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            //this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(32, 46);
            this.toolStripSplitButton1.Text = "";
            this.toolStripSplitButton1.DisplayStyle = ToolStripItemDisplayStyle.None;
            // 
            // ErrorsStatusLabel
            // 
            this.ErrorsStatusLabel.Name = "ErrorsStatusLabel";
            this.ErrorsStatusLabel.Size = new System.Drawing.Size(299, 43);
            this.ErrorsStatusLabel.Text = "";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_Start,
            this.MenuItem_Clear,
            this.MenuItem_SaveScriptMenu,
            this.MenuItem_SaveCodeMenu,
            this.toolStripWithLexemes});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1783, 53);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // MenuItem_Start
            // 
            this.MenuItem_Start.Name = "MenuItem_Start";
            this.MenuItem_Start.Size = new System.Drawing.Size(91, 49);
            this.MenuItem_Start.Text = "Start";
            this.MenuItem_Start.Click += new System.EventHandler(this.MenuItem_Start_Click);
            // 
            // MenuItem_Clear
            // 
            this.MenuItem_Clear.Name = "MenuItem_Clear";
            this.MenuItem_Clear.Size = new System.Drawing.Size(97, 49);
            this.MenuItem_Clear.Text = "Clear";
            this.MenuItem_Clear.Click += new System.EventHandler(this.MenuItem_Clear_Click);
            // 
            // MenuItem_SaveScriptMenu
            // 
            this.MenuItem_SaveScriptMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_SaveScript,
            this.MenuItem_SaveScriptAs});
            this.MenuItem_SaveScriptMenu.Name = "MenuItem_SaveScriptMenu";
            this.MenuItem_SaveScriptMenu.Size = new System.Drawing.Size(174, 49);
            this.MenuItem_SaveScriptMenu.Text = "Save Script";
            // 
            // MenuItem_SaveScript
            // 
            this.MenuItem_SaveScript.Name = "MenuItem_SaveScript";
            this.MenuItem_SaveScript.Size = new System.Drawing.Size(290, 46);
            this.MenuItem_SaveScript.Text = "Save Script";
            this.MenuItem_SaveScript.Click += new System.EventHandler(this.MenuItem_SaveScript_Click);
            // 
            // MenuItem_SaveScriptAs
            // 
            this.MenuItem_SaveScriptAs.Name = "MenuItem_SaveScriptAs";
            this.MenuItem_SaveScriptAs.Size = new System.Drawing.Size(290, 46);
            this.MenuItem_SaveScriptAs.Text = "Save Script as..";
            this.MenuItem_SaveScriptAs.Click += new System.EventHandler(this.MenuItem_SaveScriptAs_Click);
            // 
            // MenuItem_SaveCodeMenu
            // 
            this.MenuItem_SaveCodeMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_SaveCode,
            this.MenuItem_SaveCodeAs});
            this.MenuItem_SaveCodeMenu.Name = "MenuItem_SaveCodeMenu";
            this.MenuItem_SaveCodeMenu.Size = new System.Drawing.Size(170, 49);
            this.MenuItem_SaveCodeMenu.Text = "Save Code";
            // 
            // MenuItem_SaveCode
            // 
            this.MenuItem_SaveCode.Name = "MenuItem_SaveCode";
            this.MenuItem_SaveCode.Size = new System.Drawing.Size(286, 46);
            this.MenuItem_SaveCode.Text = "Save Code";
            this.MenuItem_SaveCode.Click += new System.EventHandler(this.MenuItem_SaveCode_Click);
            // 
            // MenuItem_SaveCodeAs
            // 
            this.MenuItem_SaveCodeAs.Name = "MenuItem_SaveCodeAs";
            this.MenuItem_SaveCodeAs.Size = new System.Drawing.Size(286, 46);
            this.MenuItem_SaveCodeAs.Text = "Save Code as..";
            this.MenuItem_SaveCodeAs.Click += new System.EventHandler(this.MenuItem_SaveCodeAs_Click);
            // 
            // toolStripWithLexemes
            // 
            this.toolStripWithLexemes.Items.AddRange(new object[] {
            "No lexemes",
            "With lexemes"});
            this.toolStripWithLexemes.Name = "toolStripWithLexemes";
            this.toolStripWithLexemes.Size = new System.Drawing.Size(121, 49);
            this.toolStripWithLexemes.Text = "No lexemes";
            // 
            // Form1
            // 
            //this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            //this.ClientSize = new System.Drawing.Size(1783, 987);
            //this.Controls.Add(this.toolStripContainer1);
            //this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            //this.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            //this.Name = "Form1";
            //this.Text = "QL New";
            this.spltConTripleEditor.Panel1.ResumeLayout(false);
            this.spltConTripleEditor.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spltConTripleEditor)).EndInit();
            this.spltConTripleEditor.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rteQL_LT_in_line)).EndInit();
            this./*toolStripContainer1.*/BottomToolStripPanel.ResumeLayout(false);
            this./*toolStripContainer1.*/BottomToolStripPanel.PerformLayout();
            this./*toolStripContainer1.*/ContentPanel.ResumeLayout(false);
            this./*toolStripContainer1.*/TopToolStripPanel.ResumeLayout(false);
            this./*toolStripContainer1.*/TopToolStripPanel.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this./*toolStripContainer1.*/ResumeLayout(false);
            this./*toolStripContainer1.*/PerformLayout();
            this.ResumeLayout(false);
        }

        List<error_record> error_line = new List<error_record>();

        int ColNum;
        int LineNum;
        string file_name;
        Int32 prevLexerLineLenght;

        FileInfo scriptFileInfo = null;
        FileStream ScriptFile = null;
        //bool scriptFileIsNew = true;
        //bool scriptFileIsMine = false;

        public EventHandler FileNameChanged;
        public EventHandler TextChanged;
        public EventHandler SelectionChanged;

        public bool WithLexemes
        {
            get { return (toolStripWithLexemes.Text == "With lexemes")? true : false; }
            set 
            { 
                if (value == true) toolStripWithLexemes.Text = "With lexemes"; 
                else toolStripWithLexemes.Text = "No lexemes"; 
            }
        }

        public bool CodeVisible
        {
            get { return this.splitContainer1.Panel2Collapsed; }
            set { this.splitContainer1.Panel2Collapsed = value; }
        }

        public bool ErrorsVisible
        {
            get { return this.spltConTripleEditor.Panel2Collapsed; }
            set { this.spltConTripleEditor.Panel2Collapsed = value; }
        }

        public bool CanUndo
        {
            get { return rteQL_LT_in_line.UndoRedo.CanUndo; }
        }

        public bool CanRedo
        {
            get { return rteQL_LT_in_line.UndoRedo.CanRedo; }
        }

        public QLTranslatorForm(string fileName, bool standingAlone = true)
            : base()
        {
            IntializeComponent();

            // Showing menu if started as an alone window or hiding if started as a nested control
            //menuStrip1.Visible = standingAlone;
            this.TopToolStripPanelVisible = false;

            this.splitContainer1.Panel2Collapsed = true;
            this.spltConTripleEditor.Panel2Collapsed = true;

            rteQL_LT_in_line.CharAdded += rteQL_LT_in_line_CharAdded;

            ColNum = 0;
            LineNum = 1;

            file_name = fileName;

            this.Disposed += QLTranslatorForm_Disposed;

            if (File.Exists(file_name) == true)
            {
                Open(file_name);
                rteQL_LT_in_line.UndoRedo.EmptyUndoBuffer();
            }
            else
            {
                New();
                rteQL_LT_in_line.UndoRedo.EmptyUndoBuffer();
            }

            // Setting comments style
            rteQL_LT_in_line.Styles[0].ForeColor = Color.DarkGreen;
            rteQL_LT_in_line.Styles[0].Font = new Font("Consolas", 12, FontStyle.Regular);

            // Setting key operators style
            rteQL_LT_in_line.Styles[1].ForeColor = Color.DarkCyan;
            rteQL_LT_in_line.Styles[1].Font = new Font("Consolas", 12, FontStyle.Bold);

            // Setting constants style
            rteQL_LT_in_line.Styles[2].ForeColor = Color.DarkBlue;
            rteQL_LT_in_line.Styles[2].Font = new Font("Consolas", 12, FontStyle.Regular);

            // Setting other style
            rteQL_LT_in_line.Styles[3].ForeColor = Color.Black;
            rteQL_LT_in_line.Styles[3].Font = new Font("Consolas", 12, FontStyle.Regular);

            // Setting left margin to make line numbers visible
            rteQL_LT_in_line.Margins[0].Width = 4;

            rteQL_LT_in_line.Indentation.SmartIndentType = ScintillaNET.SmartIndent.Simple;

        }

        void QLTranslatorForm_Disposed(object sender, EventArgs e)
        {
            if (ScriptFile != null) ScriptFile.Close();
        }

        private lexeme_record[] RefreshLexerLine()
        {
            // Getting lexer_line
            char[] editor_line = rteQL_LT_in_line.Text.ToCharArray();

            Q_Lexer lexer = new Q_Lexer(editor_line, error_line);
            int pos = 0;
            while (pos < editor_line.Length)
                lexer.lexer(pos++);

            return lexer.get_line();
        }

        private void ColorText(lexeme_record[] lexer_line/*, Int32 start, Int32 lenght*/)
        {
            rteQL_LT_in_line.GetRange().SetStyle(0);

            Int32 pos = 0;

            while (pos < lexer_line.Length)
            {
                /*if (lexer_line[pos].start >= start & lexer_line[pos].start + lexer_line[pos].lenght <= start + lenght)*/
                if (lexer_line[pos].type == 'o' &
                    (lexer_line[pos].lexeme == "<<" | lexer_line[pos].lexeme == "~" |
                     lexer_line[pos].lexeme == "||" | lexer_line[pos].lexeme == "&&" |
                     lexer_line[pos].lexeme == "{" | lexer_line[pos].lexeme == "}" |
                     lexer_line[pos].lexeme == "[" | lexer_line[pos].lexeme == "]" |
                     lexer_line[pos].lexeme == ".."))
                {
                    rteQL_LT_in_line.GetRange(lexer_line[pos].start, lexer_line[pos].start + lexer_line[pos].lenght).SetStyle(1);
                }

                else if (lexer_line[pos].type == 'n')
                {
                    if (lexer_line[pos].lexeme == "long" | lexer_line[pos].lexeme == "short" | lexer_line[pos].lexeme == "stop")
                    {
                        rteQL_LT_in_line.GetRange(lexer_line[pos].start, lexer_line[pos].start + lexer_line[pos].lenght).SetStyle(1);
                        //                            rteQL_LT_in_line.SelectionStart = lexer_line[pos].start - 1;
                        //                            rteQL_LT_in_line.SelectionLength = lexer_line[pos].lenght + 2;
                        //                            rteQL_LT_in_line.SelectionColor = Color.Blue;
                    }
                    else
                        rteQL_LT_in_line.GetRange(lexer_line[pos].start, lexer_line[pos].start + lexer_line[pos].lenght).SetStyle(3);
                }

                else if (lexer_line[pos].type == 'c')
                {
                    if (lexer_line[pos].lexeme.ElementAt<char>(0) == 's')
                    {
                        rteQL_LT_in_line.GetRange(lexer_line[pos].start - 1, lexer_line[pos].start + lexer_line[pos].lenght + 1).SetStyle(2);
                        //                            rteQL_LT_in_line.SelectionStart = lexer_line[pos].start - 1;
                        //                            rteQL_LT_in_line.SelectionLength = lexer_line[pos].lenght + 2;
                        //                            rteQL_LT_in_line.SelectionColor = Color.Blue;
                    }
                    else if (lexer_line[pos].lexeme.ElementAt<char>(0) == 'x')
                    {
                        rteQL_LT_in_line.GetRange(lexer_line[pos].start, lexer_line[pos].start + lexer_line[pos].lenght).SetStyle(2);
                        //                            rteQL_LT_in_line.SelectionStart = lexer_line[pos].start;
                        //                            rteQL_LT_in_line.SelectionLength = lexer_line[pos].lenght;
                        //                            rteQL_LT_in_line.SelectionColor = Color.Blue;
                    }
                    else if (lexer_line[pos].lexeme.ElementAt<char>(0) == 'c' | lexer_line[pos].lexeme.ElementAt<char>(0) == 'i' |
                             lexer_line[pos].lexeme.ElementAt<char>(0) == 'p' | lexer_line[pos].lexeme.ElementAt<char>(0) == '%' |
                             lexer_line[pos].lexeme.ElementAt<char>(0) == 'l' | lexer_line[pos].lexeme.ElementAt<char>(0) == 'n' |
                             lexer_line[pos].lexeme.ElementAt<char>(0) == 'S' | lexer_line[pos].lexeme.ElementAt<char>(0) == 'M' |
                             lexer_line[pos].lexeme.ElementAt<char>(0) == 'H' | lexer_line[pos].lexeme.ElementAt<char>(0) == 'D' |
                             lexer_line[pos].lexeme.ElementAt<char>(0) == 'W')
                    {
                        rteQL_LT_in_line.GetRange(lexer_line[pos].start, lexer_line[pos].start + lexer_line[pos].lenght + 1).SetStyle(2);
                        //                            rteQL_LT_in_line.SelectionStart = lexer_line[pos].start;
                        //                            rteQL_LT_in_line.SelectionLength = lexer_line[pos].lenght + 1;
                        //                            rteQL_LT_in_line.SelectionColor = Color.Blue;
                    }
                }
                else
                {
                    rteQL_LT_in_line.GetRange(lexer_line[pos].start, lexer_line[pos].start + lexer_line[pos].lenght).SetStyle(3);
                    //                        rteQL_LT_in_line.SelectionStart = lexer_line[pos].start;
                    //                        rteQL_LT_in_line.SelectionLength = lexer_line[pos].lenght;
                    //                        rteQL_LT_in_line.SelectionColor = Color.Black;
                }

                pos++;
            }

        }

        private void MenuItem_Start_Click(object sender, EventArgs e)
        {
            Compile();
        }

        public Int32 Compile()
        {
            ClearOutputs();
            //rteQL_LT_in_line.Text += "\n";
            char[] editor_line = rteQL_LT_in_line.Text.ToCharArray();

            // Getting lexer_line
            Q_Lexer lexer = new Q_Lexer(editor_line, error_line);
            int pos = 0;
            while (pos < editor_line.Length)
                lexer.lexer(pos++);
            lexeme_record[] lexer_line = lexer.get_line();

            // Printing lexer_line
            if (toolStripWithLexemes.Text == "With lexemes")
            {
                pos = 0;
                while (pos < lexer_line.Length)
                    rteQL_LT_out_line.AppendText(lexer_line[pos++].ToString() + "\n");
                rteQL_LT_out_line.AppendText("\n");
            }

            // Getting parcer_line
            Q_Parcer parcer = new Q_Parcer(lexer_line, error_line, "");
            pos = 0;
            while (pos < lexer_line.Length & error_line.Count < 25)
            {
                parcer.expression(pos);
                pos = parcer.CurrentPos;
                pos++;
            }
            parcer_record[] parcer_line = parcer.get_line();

            // Getting translated text or errors
            if (error_line.Count == 0)
            {
                Q_Gen gen = new Q_Gen(parcer_line);
                pos = 0;
                while (pos < parcer_line.Length)
                    gen.gen(pos++);
                string[] gen_line = gen.get_line();

                pos = 0;
                while (pos < gen_line.Length)
                    rteQL_LT_out_line.AppendText(gen_line[pos++]);
            }
            else
            {
                error_record[] err_line = error_line.ToArray();
                pos = 0;
                while (pos < err_line.Length)
                    rteQL_LT_error_line.AppendText(err_line[pos++].ToString() + "\n");

            }

            ErrorsStatusLabel.Text = error_line.Count.ToString() + " Errors";
            
            return error_line.Count;

        }

        private void MenuItem_Clear_Click(object sender, EventArgs e)
        {
            ClearOutputs();
        }

        public void ClearOutputs()
        {
            rteQL_LT_out_line.Clear();
            rteQL_LT_error_line.Clear();
            error_line.Clear();
        }

        private void rteQL_LT_in_line_SelectionChanged(object sender, EventArgs e)
        {
            int pos = rteQL_LT_in_line.CurrentPos;
            
            ScintillaNET.LineCollection lines = rteQL_LT_in_line.Lines;
            if (lines.Count == 0) return;

            int i = 0;

            while (pos > lines[i].EndPosition) i++;
            LineNum = lines[i].VisibleLineNumber + 1;
            ColNum = pos - lines[i].StartPosition;

            lb_in_line_Position.Text = LineNum.ToString() + ", " + ColNum.ToString();

            if (SelectionChanged != null) SelectionChanged.Invoke(sender, e);
        }

        private void MenuItem_SaveCodeAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog SFD = new SaveFileDialog();
            SFD.AddExtension = true;
            SFD.DefaultExt = "aql";
            SFD.Filter = "aql files (*.aql)|*.aql|all files (*.*)|*.*";
            if (SFD.ShowDialog() == DialogResult.OK)
                SaveCodeAs(SFD.FileName);

        }

        public void SaveCodeAs(string fileName)
        {
            rteQL_LT_out_line.SaveFile(fileName, RichTextBoxStreamType.PlainText);
        }

        private void MenuItem_SaveScriptAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog SFD = new SaveFileDialog();
            SFD.InitialDirectory = scriptFileInfo.DirectoryName;
            SFD.AddExtension = true;
            SFD.DefaultExt = "ql";
            SFD.Filter = "ql files (*.ql)|*.ql|all files (*.*)|*.*";
            if (SFD.ShowDialog() == DialogResult.OK)
            {
                SaveScriptAs(SFD.FileName);

                //rteQL_LT_in_line.SaveFile(SFD.FileName, RichTextBoxStreamType.PlainText);
            }

        }

        public void SaveScriptAs(string fileName)
        {
            if (ScriptFile != null) ScriptFile.Close();

            scriptFileInfo = new FileInfo(fileName);
            ScriptFile = scriptFileInfo.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);

            StreamWriter w = new StreamWriter(ScriptFile);
            w.Write(rteQL_LT_in_line.Text);
            w.Flush();
            //w.Close();

            //scriptFileInfo.IsReadOnly = true;
            //scriptFileIsMine = true;
            //scriptFileIsNew = false;
        }

        private void rteQL_LT_in_line_TextChanged(object sender, EventArgs e)
        {
            lexeme_record[] lexer_line = RefreshLexerLine();

            ColorText(lexer_line);
            ErrorsStatusLabel.Text = "";

            if (TextChanged != null) TextChanged.Invoke(sender, e);
        }

        private void MenuItem_SaveScript_Click(object sender, EventArgs e)
        {
            SaveScript();
        }

        public void SaveScript()
        {
            if (ScriptFile != null)
            {
                ScriptFile.SetLength(0);
                StreamWriter w = new StreamWriter(ScriptFile);
                w.Write(rteQL_LT_in_line.Text);
                w.Flush();
                //w.Close();
            }

        }

        private void MenuItem_SaveCode_Click(object sender, EventArgs e)
        {
            SaveCode();
        }

        public void SaveCode()
        {
            char[] separator = {'.'};
            string[] strings = scriptFileInfo.Name.Split(separator);

            int i = 0;
            string FileName = "";
            while (i < strings.Count<string>() - 1)
            {
                FileName += strings[i]; FileName += ".";
                i++;
            }
            FileName += "aql";

            FileInfo codeFileInfo = new FileInfo(scriptFileInfo.DirectoryName + "\\" + FileName);
            rteQL_LT_out_line.SaveFile(codeFileInfo.FullName, RichTextBoxStreamType.PlainText);
        }

        public void SaveBoth()
        {
            SaveScript();
            SaveCode(); 
        }

        public void New()
        {
            if (ScriptFile != null) ScriptFile.Close();

            scriptFileInfo = null;
            rteQL_LT_in_line.Text = "";

            //scriptFileIsMine = true;
            MenuItem_SaveScript.Enabled = MenuItem_SaveCode.Enabled = false;

            //scriptFileIsNew = true;
        }

        public bool Open(string fileName)
        {
            scriptFileInfo = new FileInfo(fileName);

            // Catching the ql file
            //if (scriptFileInfo.IsReadOnly == true)
            //{
            //    scriptFileIsMine = false;
            //    MenuItem_SaveScript.Enabled = MenuItem_SaveCode.Enabled = false;
            //}
            //else
            //{
            //    scriptFileInfo.IsReadOnly = true;
                //scriptFileIsMine = true;
                MenuItem_SaveScript.Enabled = MenuItem_SaveCode.Enabled = true;
            //}

            // Open
            ScriptFile = scriptFileInfo.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
            StreamReader r = new StreamReader(ScriptFile);
            rteQL_LT_in_line.ResetText();
            rteQL_LT_in_line.AppendText(r.ReadToEnd());
            //r.Close();

            //Text = " QL " + " " + scriptFileInfo.Name;

            rteQL_LT_in_line.TextChanged -= rteQL_LT_in_line_TextChanged;
            lexeme_record[] lex_line = RefreshLexerLine();
            ColorText(lex_line);
            rteQL_LT_in_line.TextChanged += rteQL_LT_in_line_TextChanged;

            //scriptFileIsNew = false;

            return true;
        }

        public void EditUndo()
        {
            if (rteQL_LT_in_line.UndoRedo.CanUndo) rteQL_LT_in_line.UndoRedo.Undo();
        }

        public void EditRedo()
        {
            if (rteQL_LT_in_line.UndoRedo.CanRedo) rteQL_LT_in_line.UndoRedo.Redo();
        }

        public void EditCut()
        {
            if (rteQL_LT_in_line.Clipboard.CanCut) rteQL_LT_in_line.Clipboard.Cut();
        }

        public void EditCopy()
        {
            if (rteQL_LT_in_line.Clipboard.CanCopy) rteQL_LT_in_line.Clipboard.Copy();
        }

        public void EditPaste()
        {
            if (rteQL_LT_in_line.Clipboard.CanPaste) rteQL_LT_in_line.Clipboard.Paste();
        }

        public void EditSelectAll()
        {
            rteQL_LT_in_line.Selection.SelectAll();
        }

        public void EditFind()
        {
            ScintillaNET.FindReplaceDialog dd = new ScintillaNET.FindReplaceDialog();
            dd.Scintilla = rteQL_LT_in_line;
            dd.ShowDialog();
        }

        public void EditGoto()
        {
            rteQL_LT_in_line.GoTo.ShowGoToDialog();
        }

        private void rteQL_LT_in_line_CharAdded(object sender, ScintillaNET.CharAddedEventArgs e)
        {
            //ScintillaNET.Scintilla editor = sender as ScintillaNET.Scintilla;
            int caretpos = rteQL_LT_in_line.Caret.Position;
            if (e.Ch == '{')
            {
                rteQL_LT_in_line.InsertText("}");
                rteQL_LT_in_line.Caret.Goto(caretpos);
            }
            //else if (e.Ch == '(')
            //{
            //    rteQL_LT_in_line.InsertText(")");
            //    rteQL_LT_in_line.Caret.Goto(caretpos);
            //}
            //else if (e.Ch == '[')
            //{
            //    rteQL_LT_in_line.InsertText("]");
            //    rteQL_LT_in_line.Caret.Goto(caretpos);
            //}

        }


    }
}
