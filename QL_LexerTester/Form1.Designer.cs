namespace QL_LexerTester
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.btFileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.btFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.btFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.btFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.btFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.btEditMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.btEditUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.btEditRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btEditCut = new System.Windows.Forms.ToolStripMenuItem();
            this.btEditCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.btEditPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.btEditSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btEditFind = new System.Windows.Forms.ToolStripMenuItem();
            this.btEditReplace = new System.Windows.Forms.ToolStripMenuItem();
            this.btEditGoto = new System.Windows.Forms.ToolStripMenuItem();
            this.btScriptMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.btScriptCompile = new System.Windows.Forms.ToolStripMenuItem();
            this.btScriptLexemes = new System.Windows.Forms.ToolStripMenuItem();
            this.btScriptClear = new System.Windows.Forms.ToolStripMenuItem();
            this.btScriptParameters = new System.Windows.Forms.ToolStripMenuItem();
            this.btViewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.btViewCode = new System.Windows.Forms.ToolStripMenuItem();
            this.btViewErrors = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btlNew = new System.Windows.Forms.ToolStripButton();
            this.btlOpen = new System.Windows.Forms.ToolStripButton();
            this.btlSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btlEditUndo = new System.Windows.Forms.ToolStripButton();
            this.btlEditRedo = new System.Windows.Forms.ToolStripButton();
            this.btlEditCut = new System.Windows.Forms.ToolStripButton();
            this.btlEditCopy = new System.Windows.Forms.ToolStripButton();
            this.btlEditPaste = new System.Windows.Forms.ToolStripButton();
            this.btlEditFind = new System.Windows.Forms.ToolStripButton();
            this.btlEditGoto = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btlCompile = new System.Windows.Forms.ToolStripButton();
            this.btlScriptParameters = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.btlCode = new System.Windows.Forms.ToolStripButton();
            this.btlErrors = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btFileMenu,
            this.btEditMenu,
            this.btScriptMenu,
            this.btViewMenu,
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1783, 49);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // btFileMenu
            // 
            this.btFileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btFileNew,
            this.btFileOpen,
            this.btFileSave,
            this.btFileSaveAs,
            this.toolStripSeparator1,
            this.btFileExit});
            this.btFileMenu.Name = "btFileMenu";
            this.btFileMenu.Size = new System.Drawing.Size(75, 45);
            this.btFileMenu.Text = "File";
            // 
            // btFileNew
            // 
            this.btFileNew.Name = "btFileNew";
            this.btFileNew.Size = new System.Drawing.Size(251, 46);
            this.btFileNew.Text = "New";
            this.btFileNew.Click += new System.EventHandler(this.btFileNew_Click);
            // 
            // btFileOpen
            // 
            this.btFileOpen.Name = "btFileOpen";
            this.btFileOpen.Size = new System.Drawing.Size(251, 46);
            this.btFileOpen.Text = "Open...";
            this.btFileOpen.Click += new System.EventHandler(this.btFileOpen_Click);
            // 
            // btFileSave
            // 
            this.btFileSave.Name = "btFileSave";
            this.btFileSave.Size = new System.Drawing.Size(251, 46);
            this.btFileSave.Text = "Save";
            this.btFileSave.Click += new System.EventHandler(this.btFileSave_Click);
            // 
            // btFileSaveAs
            // 
            this.btFileSaveAs.Name = "btFileSaveAs";
            this.btFileSaveAs.Size = new System.Drawing.Size(251, 46);
            this.btFileSaveAs.Text = "Save as...";
            this.btFileSaveAs.Click += new System.EventHandler(this.btFileSaveAs_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(248, 6);
            // 
            // btFileExit
            // 
            this.btFileExit.Name = "btFileExit";
            this.btFileExit.Size = new System.Drawing.Size(251, 46);
            this.btFileExit.Text = "Exit";
            this.btFileExit.Click += new System.EventHandler(this.btFileExit_Click);
            // 
            // btEditMenu
            // 
            this.btEditMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btEditUndo,
            this.btEditRedo,
            this.toolStripSeparator4,
            this.btEditCut,
            this.btEditCopy,
            this.btEditPaste,
            this.toolStripSeparator6,
            this.btEditSelectAll,
            this.toolStripSeparator5,
            this.btEditFind,
            this.btEditReplace,
            this.btEditGoto});
            this.btEditMenu.Name = "btEditMenu";
            this.btEditMenu.Size = new System.Drawing.Size(80, 45);
            this.btEditMenu.Text = "Edit";
            // 
            // btEditUndo
            // 
            this.btEditUndo.Enabled = false;
            this.btEditUndo.Name = "btEditUndo";
            this.btEditUndo.Size = new System.Drawing.Size(353, 46);
            this.btEditUndo.Text = "Undo Ctrl+Z";
            this.btEditUndo.Click += new System.EventHandler(this.btEditUndo_Click);
            // 
            // btEditRedo
            // 
            this.btEditRedo.Enabled = false;
            this.btEditRedo.Name = "btEditRedo";
            this.btEditRedo.Size = new System.Drawing.Size(353, 46);
            this.btEditRedo.Text = "Redo Ctrl+Y";
            this.btEditRedo.Click += new System.EventHandler(this.btEditRedo_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(350, 6);
            // 
            // btEditCut
            // 
            this.btEditCut.Name = "btEditCut";
            this.btEditCut.Size = new System.Drawing.Size(353, 46);
            this.btEditCut.Text = "Cut Ctrl+X";
            this.btEditCut.Click += new System.EventHandler(this.btEditCut_Click);
            // 
            // btEditCopy
            // 
            this.btEditCopy.Name = "btEditCopy";
            this.btEditCopy.Size = new System.Drawing.Size(353, 46);
            this.btEditCopy.Text = "Copy Ctrl+C";
            this.btEditCopy.Click += new System.EventHandler(this.btEditCopy_Click);
            // 
            // btEditPaste
            // 
            this.btEditPaste.Name = "btEditPaste";
            this.btEditPaste.Size = new System.Drawing.Size(353, 46);
            this.btEditPaste.Text = "Paste Ctrl+V";
            this.btEditPaste.Click += new System.EventHandler(this.btEditPaste_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(350, 6);
            // 
            // btEditSelectAll
            // 
            this.btEditSelectAll.Name = "btEditSelectAll";
            this.btEditSelectAll.Size = new System.Drawing.Size(353, 46);
            this.btEditSelectAll.Text = "Select All Ctrl+A";
            this.btEditSelectAll.Click += new System.EventHandler(this.btEditSelectAll_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(350, 6);
            // 
            // btEditFind
            // 
            this.btEditFind.Name = "btEditFind";
            this.btEditFind.Size = new System.Drawing.Size(353, 46);
            this.btEditFind.Text = "Find... Ctrl+F";
            this.btEditFind.Click += new System.EventHandler(this.btEditFind_Click);
            // 
            // btEditReplace
            // 
            this.btEditReplace.Name = "btEditReplace";
            this.btEditReplace.Size = new System.Drawing.Size(353, 46);
            this.btEditReplace.Text = "Replace... Ctrl+H";
            this.btEditReplace.Click += new System.EventHandler(this.btEditReplace_Click);
            // 
            // btEditGoto
            // 
            this.btEditGoto.Name = "btEditGoto";
            this.btEditGoto.Size = new System.Drawing.Size(353, 46);
            this.btEditGoto.Text = "Go to... Ctrl+G";
            this.btEditGoto.Click += new System.EventHandler(this.btEditGoto_Click);
            // 
            // btScriptMenu
            // 
            this.btScriptMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btScriptCompile,
            this.btScriptLexemes,
            this.btScriptClear,
            this.btScriptParameters});
            this.btScriptMenu.Name = "btScriptMenu";
            this.btScriptMenu.Size = new System.Drawing.Size(105, 45);
            this.btScriptMenu.Text = "Script";
            // 
            // btScriptCompile
            // 
            this.btScriptCompile.Name = "btScriptCompile";
            this.btScriptCompile.Size = new System.Drawing.Size(316, 46);
            this.btScriptCompile.Text = "Compile";
            this.btScriptCompile.Click += new System.EventHandler(this.btScriptCompile_Click);
            // 
            // btScriptLexemes
            // 
            this.btScriptLexemes.CheckOnClick = true;
            this.btScriptLexemes.Name = "btScriptLexemes";
            this.btScriptLexemes.Size = new System.Drawing.Size(316, 46);
            this.btScriptLexemes.Text = "Lexemes";
            this.btScriptLexemes.Click += new System.EventHandler(this.btScriptLexemes_Click);
            // 
            // btScriptClear
            // 
            this.btScriptClear.Name = "btScriptClear";
            this.btScriptClear.Size = new System.Drawing.Size(316, 46);
            this.btScriptClear.Text = "Clear Outputs";
            this.btScriptClear.Click += new System.EventHandler(this.btScriptClear_Click);
            // 
            // btScriptParameters
            // 
            this.btScriptParameters.Name = "btScriptParameters";
            this.btScriptParameters.Size = new System.Drawing.Size(316, 46);
            this.btScriptParameters.Text = "Parameters...";
            this.btScriptParameters.Click += new System.EventHandler(this.btScriptParams_Click);
            // 
            // btViewMenu
            // 
            this.btViewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btViewCode,
            this.btViewErrors});
            this.btViewMenu.Name = "btViewMenu";
            this.btViewMenu.Size = new System.Drawing.Size(94, 45);
            this.btViewMenu.Text = "View";
            // 
            // btViewCode
            // 
            this.btViewCode.CheckOnClick = true;
            this.btViewCode.Name = "btViewCode";
            this.btViewCode.Size = new System.Drawing.Size(209, 46);
            this.btViewCode.Text = "Code";
            this.btViewCode.Click += new System.EventHandler(this.btViewCode_Click);
            // 
            // btViewErrors
            // 
            this.btViewErrors.CheckOnClick = true;
            this.btViewErrors.Name = "btViewErrors";
            this.btViewErrors.Size = new System.Drawing.Size(209, 46);
            this.btViewErrors.Text = "Errors";
            this.btViewErrors.Click += new System.EventHandler(this.btViewErrors_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.aboutToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(92, 45);
            this.toolStripMenuItem1.Text = "Help";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(286, 46);
            this.toolStripMenuItem2.Text = "Help Topics";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(286, 46);
            this.aboutToolStripMenuItem.Text = "About...";
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.MenuBar;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btlNew,
            this.btlOpen,
            this.btlSave,
            this.toolStripSeparator2,
            this.btlEditUndo,
            this.btlEditRedo,
            this.btlEditCut,
            this.btlEditCopy,
            this.btlEditPaste,
            this.btlEditFind,
            this.btlEditGoto,
            this.toolStripSeparator3,
            this.btlCompile,
            this.btlScriptParameters,
            this.toolStripSeparator7,
            this.btlCode,
            this.btlErrors});
            this.toolStrip1.Location = new System.Drawing.Point(0, 49);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1783, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btlNew
            // 
            this.btlNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btlNew.Image = global::QL_LexerTester.Properties.Resources.NewFile_6276;
            this.btlNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btlNew.Name = "btlNew";
            this.btlNew.Size = new System.Drawing.Size(29, 29);
            this.btlNew.Text = "toolStripButton1";
            this.btlNew.ToolTipText = "New";
            this.btlNew.Click += new System.EventHandler(this.btFileNew_Click);
            // 
            // btlOpen
            // 
            this.btlOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btlOpen.Image = global::QL_LexerTester.Properties.Resources.Open_6529;
            this.btlOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btlOpen.Name = "btlOpen";
            this.btlOpen.Size = new System.Drawing.Size(29, 29);
            this.btlOpen.Text = "toolStripButton1";
            this.btlOpen.ToolTipText = "Open";
            this.btlOpen.Click += new System.EventHandler(this.btFileOpen_Click);
            // 
            // btlSave
            // 
            this.btlSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btlSave.Image = global::QL_LexerTester.Properties.Resources.Save_6530;
            this.btlSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btlSave.Name = "btlSave";
            this.btlSave.Size = new System.Drawing.Size(29, 29);
            this.btlSave.Text = "toolStripButton2";
            this.btlSave.ToolTipText = "Save";
            this.btlSave.Click += new System.EventHandler(this.btFileSave_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 32);
            // 
            // btlEditUndo
            // 
            this.btlEditUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btlEditUndo.Enabled = false;
            this.btlEditUndo.Image = global::QL_LexerTester.Properties.Resources.Undo_16x;
            this.btlEditUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btlEditUndo.Name = "btlEditUndo";
            this.btlEditUndo.Size = new System.Drawing.Size(29, 29);
            this.btlEditUndo.Text = "toolStripButton1";
            this.btlEditUndo.ToolTipText = "Undo";
            this.btlEditUndo.Click += new System.EventHandler(this.btEditUndo_Click);
            // 
            // btlEditRedo
            // 
            this.btlEditRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btlEditRedo.Enabled = false;
            this.btlEditRedo.Image = global::QL_LexerTester.Properties.Resources.Redo_16x;
            this.btlEditRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btlEditRedo.Name = "btlEditRedo";
            this.btlEditRedo.Size = new System.Drawing.Size(29, 29);
            this.btlEditRedo.Text = "Redo";
            this.btlEditRedo.Click += new System.EventHandler(this.btEditRedo_Click);
            // 
            // btlEditCut
            // 
            this.btlEditCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btlEditCut.Image = global::QL_LexerTester.Properties.Resources.Cut_6523;
            this.btlEditCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btlEditCut.Name = "btlEditCut";
            this.btlEditCut.Size = new System.Drawing.Size(29, 29);
            this.btlEditCut.Text = "Cut";
            this.btlEditCut.Click += new System.EventHandler(this.btEditCut_Click);
            // 
            // btlEditCopy
            // 
            this.btlEditCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btlEditCopy.Image = global::QL_LexerTester.Properties.Resources.Copy_6524;
            this.btlEditCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btlEditCopy.Name = "btlEditCopy";
            this.btlEditCopy.Size = new System.Drawing.Size(29, 29);
            this.btlEditCopy.Text = "Copy";
            this.btlEditCopy.Click += new System.EventHandler(this.btEditCopy_Click);
            // 
            // btlEditPaste
            // 
            this.btlEditPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btlEditPaste.Image = global::QL_LexerTester.Properties.Resources.Paste_6520;
            this.btlEditPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btlEditPaste.Name = "btlEditPaste";
            this.btlEditPaste.Size = new System.Drawing.Size(29, 29);
            this.btlEditPaste.Text = "Paste";
            this.btlEditPaste.Click += new System.EventHandler(this.btEditPaste_Click);
            // 
            // btlEditFind
            // 
            this.btlEditFind.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btlEditFind.Image = global::QL_LexerTester.Properties.Resources.Find_5650;
            this.btlEditFind.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btlEditFind.Name = "btlEditFind";
            this.btlEditFind.Size = new System.Drawing.Size(29, 29);
            this.btlEditFind.Text = "Find";
            this.btlEditFind.Click += new System.EventHandler(this.btEditFind_Click);
            // 
            // btlEditGoto
            // 
            this.btlEditGoto.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btlEditGoto.Image = global::QL_LexerTester.Properties.Resources.GotoNextRow_289_color;
            this.btlEditGoto.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btlEditGoto.Name = "btlEditGoto";
            this.btlEditGoto.Size = new System.Drawing.Size(29, 29);
            this.btlEditGoto.Text = "Go to";
            this.btlEditGoto.ToolTipText = "Go to line";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 32);
            // 
            // btlCompile
            // 
            this.btlCompile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btlCompile.Image = ((System.Drawing.Image)(resources.GetObject("btlCompile.Image")));
            this.btlCompile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btlCompile.Name = "btlCompile";
            this.btlCompile.Size = new System.Drawing.Size(23, 22);
            this.btlCompile.Text = "toolStripButton3";
            this.btlCompile.ToolTipText = "Compile";
            this.btlCompile.Click += new System.EventHandler(this.btScriptCompile_Click);
            // 
            // btlScriptParameters
            // 
            this.btlScriptParameters.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btlScriptParameters.Image = global::QL_LexerTester.Properties.Resources.ParametersInfo_2423;
            this.btlScriptParameters.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btlScriptParameters.Name = "btlScriptParameters";
            this.btlScriptParameters.Size = new System.Drawing.Size(29, 29);
            this.btlScriptParameters.Text = "Parameters";
            this.btlScriptParameters.ToolTipText = "Script Parameters";
            this.btlScriptParameters.Click += new System.EventHandler(this.btScriptParams_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 32);
            // 
            // btlCode
            // 
            this.btlCode.CheckOnClick = true;
            this.btlCode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btlCode.Image = global::QL_LexerTester.Properties.Resources.CodeCoverageResults_8592;
            this.btlCode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btlCode.Name = "btlCode";
            this.btlCode.Size = new System.Drawing.Size(29, 29);
            this.btlCode.Text = "toolStripButton1";
            this.btlCode.ToolTipText = "View Code";
            this.btlCode.Click += new System.EventHandler(this.btViewCode_Click);
            // 
            // btlErrors
            // 
            this.btlErrors.CheckOnClick = true;
            this.btlErrors.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btlErrors.Image = global::QL_LexerTester.Properties.Resources.BuildErrorList_7237;
            this.btlErrors.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btlErrors.Name = "btlErrors";
            this.btlErrors.Size = new System.Drawing.Size(29, 29);
            this.btlErrors.Text = "toolStripButton2";
            this.btlErrors.ToolTipText = "View Errors";
            this.btlErrors.Click += new System.EventHandler(this.btViewErrors_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1783, 987);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.Name = "Form1";
            this.Text = "QL New";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem btFileMenu;
        private System.Windows.Forms.ToolStripMenuItem btFileNew;
        private System.Windows.Forms.ToolStripMenuItem btFileOpen;
        private System.Windows.Forms.ToolStripMenuItem btFileSave;
        private System.Windows.Forms.ToolStripMenuItem btFileSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem btFileExit;
        private System.Windows.Forms.ToolStripMenuItem btScriptMenu;
        private System.Windows.Forms.ToolStripMenuItem btScriptCompile;
        private System.Windows.Forms.ToolStripMenuItem btScriptLexemes;
        private System.Windows.Forms.ToolStripMenuItem btViewMenu;
        private System.Windows.Forms.ToolStripMenuItem btViewCode;
        private System.Windows.Forms.ToolStripMenuItem btViewErrors;
        private System.Windows.Forms.ToolStripMenuItem btScriptClear;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btlNew;
        private System.Windows.Forms.ToolStripButton btlSave;
        private System.Windows.Forms.ToolStripButton btlCompile;
        private System.Windows.Forms.ToolStripButton btlOpen;
        private System.Windows.Forms.ToolStripButton btlCode;
        private System.Windows.Forms.ToolStripButton btlErrors;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem btEditMenu;
        private System.Windows.Forms.ToolStripMenuItem btEditFind;
        private System.Windows.Forms.ToolStripMenuItem btEditReplace;
        private System.Windows.Forms.ToolStripMenuItem btEditUndo;
        private System.Windows.Forms.ToolStripMenuItem btEditRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem btEditCut;
        private System.Windows.Forms.ToolStripMenuItem btEditCopy;
        private System.Windows.Forms.ToolStripMenuItem btEditPaste;
        private System.Windows.Forms.ToolStripMenuItem btEditSelectAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton btlEditUndo;
        private System.Windows.Forms.ToolStripButton btlEditRedo;
        private System.Windows.Forms.ToolStripButton btlEditCut;
        private System.Windows.Forms.ToolStripButton btlEditCopy;
        private System.Windows.Forms.ToolStripButton btlEditPaste;
        private System.Windows.Forms.ToolStripButton btlEditFind;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem btEditGoto;
        private System.Windows.Forms.ToolStripButton btlEditGoto;
        private System.Windows.Forms.ToolStripMenuItem btScriptParameters;
        private System.Windows.Forms.ToolStripButton btlScriptParameters;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;

        /*private System.Windows.Forms.SplitContainer spltConTripleEditor;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox rteQL_LT_out_line;
        private System.Windows.Forms.RichTextBox rteQL_LT_error_line;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
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
        private System.Windows.Forms.ToolStripMenuItem MenuItem_SaveCodeAs;*/

    }
}

