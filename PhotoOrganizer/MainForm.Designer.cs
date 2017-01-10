namespace PhotoOrganizer
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
            this.labelSourceFolder = new System.Windows.Forms.Label();
            this.textBoxSourceFolder = new System.Windows.Forms.TextBox();
            this.buttonBrowseSourceFolder = new System.Windows.Forms.Button();
            this.labelTargetFolder = new System.Windows.Forms.Label();
            this.textBoxTargetFolder = new System.Windows.Forms.TextBox();
            this.buttonBrowseTargetFolder = new System.Windows.Forms.Button();
            this.folderBrowserDialogSource = new System.Windows.Forms.FolderBrowserDialog();
            this.folderBrowserDialogTarget = new System.Windows.Forms.FolderBrowserDialog();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonStart = new System.Windows.Forms.Button();
            this.checkBoxCopyFiles = new System.Windows.Forms.CheckBox();
            this.checkBoxRecurseFolders = new System.Windows.Forms.CheckBox();
            this.labelLogFilename = new System.Windows.Forms.Label();
            this.textBoxLogFilename = new System.Windows.Forms.TextBox();
            this.buttonBrowseLogFile = new System.Windows.Forms.Button();
            this.saveFileDialogLogFile = new System.Windows.Forms.SaveFileDialog();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.pictureBoxHeader = new System.Windows.Forms.PictureBox();
            this.panelStatus = new System.Windows.Forms.Panel();
            this.labelTargetFile = new System.Windows.Forms.Label();
            this.labelItemsRemaining = new System.Windows.Forms.Label();
            this.labelTo = new System.Windows.Forms.Label();
            this.labelFrom = new System.Windows.Forms.Label();
            this.labelFilesRemaining = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.labelSourceFile = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHeader)).BeginInit();
            this.panelStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelSourceFolder
            // 
            this.labelSourceFolder.AutoSize = true;
            this.labelSourceFolder.Location = new System.Drawing.Point(10, 75);
            this.labelSourceFolder.Name = "labelSourceFolder";
            this.labelSourceFolder.Size = new System.Drawing.Size(76, 13);
            this.labelSourceFolder.TabIndex = 0;
            this.labelSourceFolder.Text = "Source Folder:";
            // 
            // textBoxSourceFolder
            // 
            this.textBoxSourceFolder.Location = new System.Drawing.Point(110, 71);
            this.textBoxSourceFolder.Name = "textBoxSourceFolder";
            this.textBoxSourceFolder.Size = new System.Drawing.Size(365, 20);
            this.textBoxSourceFolder.TabIndex = 1;
            this.textBoxSourceFolder.TextChanged += new System.EventHandler(this.textBoxSourceFolder_TextChanged);
            // 
            // buttonBrowseSourceFolder
            // 
            this.buttonBrowseSourceFolder.Location = new System.Drawing.Point(482, 70);
            this.buttonBrowseSourceFolder.Name = "buttonBrowseSourceFolder";
            this.buttonBrowseSourceFolder.Size = new System.Drawing.Size(70, 23);
            this.buttonBrowseSourceFolder.TabIndex = 2;
            this.buttonBrowseSourceFolder.Text = "Browse...";
            this.buttonBrowseSourceFolder.UseVisualStyleBackColor = true;
            this.buttonBrowseSourceFolder.Click += new System.EventHandler(this.buttonBrowseSourceFolder_Click);
            // 
            // labelTargetFolder
            // 
            this.labelTargetFolder.AutoSize = true;
            this.labelTargetFolder.Location = new System.Drawing.Point(10, 103);
            this.labelTargetFolder.Name = "labelTargetFolder";
            this.labelTargetFolder.Size = new System.Drawing.Size(73, 13);
            this.labelTargetFolder.TabIndex = 3;
            this.labelTargetFolder.Text = "Target Folder:";
            // 
            // textBoxTargetFolder
            // 
            this.textBoxTargetFolder.Location = new System.Drawing.Point(110, 99);
            this.textBoxTargetFolder.Name = "textBoxTargetFolder";
            this.textBoxTargetFolder.Size = new System.Drawing.Size(365, 20);
            this.textBoxTargetFolder.TabIndex = 4;
            this.textBoxTargetFolder.TextChanged += new System.EventHandler(this.textBoxTargetFolder_TextChanged);
            // 
            // buttonBrowseTargetFolder
            // 
            this.buttonBrowseTargetFolder.Location = new System.Drawing.Point(482, 98);
            this.buttonBrowseTargetFolder.Name = "buttonBrowseTargetFolder";
            this.buttonBrowseTargetFolder.Size = new System.Drawing.Size(70, 23);
            this.buttonBrowseTargetFolder.TabIndex = 5;
            this.buttonBrowseTargetFolder.Text = "Browse...";
            this.buttonBrowseTargetFolder.UseVisualStyleBackColor = true;
            this.buttonBrowseTargetFolder.Click += new System.EventHandler(this.buttonBrowseDestFolder_Click);
            // 
            // folderBrowserDialogSource
            // 
            this.folderBrowserDialogSource.Description = "Select Source Folder";
            this.folderBrowserDialogSource.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.folderBrowserDialogSource.ShowNewFolderButton = false;
            // 
            // folderBrowserDialogTarget
            // 
            this.folderBrowserDialogTarget.Description = "Select folder to place organized photos";
            this.folderBrowserDialogTarget.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // buttonExit
            // 
            this.buttonExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonExit.Location = new System.Drawing.Point(477, 165);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(75, 23);
            this.buttonExit.TabIndex = 15;
            this.buttonExit.Text = "Exit";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.Enabled = false;
            this.buttonStart.Location = new System.Drawing.Point(396, 165);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 14;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // checkBoxCopyFiles
            // 
            this.checkBoxCopyFiles.AutoSize = true;
            this.checkBoxCopyFiles.Checked = true;
            this.checkBoxCopyFiles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCopyFiles.Location = new System.Drawing.Point(110, 153);
            this.checkBoxCopyFiles.Name = "checkBoxCopyFiles";
            this.checkBoxCopyFiles.Size = new System.Drawing.Size(112, 17);
            this.checkBoxCopyFiles.TabIndex = 9;
            this.checkBoxCopyFiles.Text = "Leave source files";
            this.checkBoxCopyFiles.UseVisualStyleBackColor = true;
            // 
            // checkBoxRecurseFolders
            // 
            this.checkBoxRecurseFolders.AutoSize = true;
            this.checkBoxRecurseFolders.Checked = true;
            this.checkBoxRecurseFolders.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRecurseFolders.Location = new System.Drawing.Point(110, 176);
            this.checkBoxRecurseFolders.Name = "checkBoxRecurseFolders";
            this.checkBoxRecurseFolders.Size = new System.Drawing.Size(100, 17);
            this.checkBoxRecurseFolders.TabIndex = 10;
            this.checkBoxRecurseFolders.Text = "Recurse folders";
            this.checkBoxRecurseFolders.UseVisualStyleBackColor = true;
            // 
            // labelLogFilename
            // 
            this.labelLogFilename.AutoSize = true;
            this.labelLogFilename.Location = new System.Drawing.Point(10, 131);
            this.labelLogFilename.Name = "labelLogFilename";
            this.labelLogFilename.Size = new System.Drawing.Size(47, 13);
            this.labelLogFilename.TabIndex = 6;
            this.labelLogFilename.Text = "Log File:";
            // 
            // textBoxLogFilename
            // 
            this.textBoxLogFilename.Location = new System.Drawing.Point(110, 127);
            this.textBoxLogFilename.Name = "textBoxLogFilename";
            this.textBoxLogFilename.Size = new System.Drawing.Size(365, 20);
            this.textBoxLogFilename.TabIndex = 7;
            // 
            // buttonBrowseLogFile
            // 
            this.buttonBrowseLogFile.Location = new System.Drawing.Point(482, 126);
            this.buttonBrowseLogFile.Name = "buttonBrowseLogFile";
            this.buttonBrowseLogFile.Size = new System.Drawing.Size(70, 23);
            this.buttonBrowseLogFile.TabIndex = 8;
            this.buttonBrowseLogFile.Text = "Browse...";
            this.buttonBrowseLogFile.UseVisualStyleBackColor = true;
            this.buttonBrowseLogFile.Click += new System.EventHandler(this.buttonBrowseLogFile_Click);
            // 
            // saveFileDialogLogFile
            // 
            this.saveFileDialogLogFile.DefaultExt = "txt";
            this.saveFileDialogLogFile.Filter = "Text files|*.txt";
            this.saveFileDialogLogFile.Title = "Log File";
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Enabled = false;
            this.buttonCancel.Location = new System.Drawing.Point(476, 323);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 18;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Enabled = false;
            this.buttonOK.Location = new System.Drawing.Point(476, 323);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 19;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Visible = false;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // pictureBoxHeader
            // 
            this.pictureBoxHeader.Image = global::PhotoOrganizer.Properties.Resources.Header;
            this.pictureBoxHeader.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxHeader.Name = "pictureBoxHeader";
            this.pictureBoxHeader.Size = new System.Drawing.Size(563, 60);
            this.pictureBoxHeader.TabIndex = 20;
            this.pictureBoxHeader.TabStop = false;
            // 
            // panelStatus
            // 
            this.panelStatus.BackColor = System.Drawing.SystemColors.Window;
            this.panelStatus.BackgroundImage = global::PhotoOrganizer.Properties.Resources.StatusBackground;
            this.panelStatus.Controls.Add(this.labelTargetFile);
            this.panelStatus.Controls.Add(this.labelItemsRemaining);
            this.panelStatus.Controls.Add(this.labelTo);
            this.panelStatus.Controls.Add(this.labelFrom);
            this.panelStatus.Controls.Add(this.labelFilesRemaining);
            this.panelStatus.Controls.Add(this.progressBar);
            this.panelStatus.Controls.Add(this.labelSourceFile);
            this.panelStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelStatus.Location = new System.Drawing.Point(0, 205);
            this.panelStatus.Name = "panelStatus";
            this.panelStatus.Size = new System.Drawing.Size(565, 102);
            this.panelStatus.TabIndex = 17;
            // 
            // labelTargetFile
            // 
            this.labelTargetFile.AutoEllipsis = true;
            this.labelTargetFile.Location = new System.Drawing.Point(50, 30);
            this.labelTargetFile.Name = "labelTargetFile";
            this.labelTargetFile.Size = new System.Drawing.Size(502, 15);
            this.labelTargetFile.TabIndex = 20;
            // 
            // labelItemsRemaining
            // 
            this.labelItemsRemaining.AutoSize = true;
            this.labelItemsRemaining.Location = new System.Drawing.Point(10, 50);
            this.labelItemsRemaining.Name = "labelItemsRemaining";
            this.labelItemsRemaining.Size = new System.Drawing.Size(99, 15);
            this.labelItemsRemaining.TabIndex = 19;
            this.labelItemsRemaining.Text = "Items Remaining:";
            // 
            // labelTo
            // 
            this.labelTo.AutoSize = true;
            this.labelTo.Location = new System.Drawing.Point(10, 30);
            this.labelTo.Name = "labelTo";
            this.labelTo.Size = new System.Drawing.Size(24, 15);
            this.labelTo.TabIndex = 18;
            this.labelTo.Text = "To:";
            // 
            // labelFrom
            // 
            this.labelFrom.AutoSize = true;
            this.labelFrom.Location = new System.Drawing.Point(10, 10);
            this.labelFrom.Name = "labelFrom";
            this.labelFrom.Size = new System.Drawing.Size(38, 15);
            this.labelFrom.TabIndex = 17;
            this.labelFrom.Text = "From:";
            // 
            // labelFilesRemaining
            // 
            this.labelFilesRemaining.AutoEllipsis = true;
            this.labelFilesRemaining.Location = new System.Drawing.Point(112, 50);
            this.labelFilesRemaining.Name = "labelFilesRemaining";
            this.labelFilesRemaining.Size = new System.Drawing.Size(440, 15);
            this.labelFilesRemaining.TabIndex = 11;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(10, 71);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(542, 23);
            this.progressBar.TabIndex = 16;
            // 
            // labelSourceFile
            // 
            this.labelSourceFile.AutoEllipsis = true;
            this.labelSourceFile.Location = new System.Drawing.Point(50, 10);
            this.labelSourceFile.Name = "labelSourceFile";
            this.labelSourceFile.Size = new System.Drawing.Size(502, 15);
            this.labelSourceFile.TabIndex = 12;
            // 
            // MainForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(562, 358);
            this.Controls.Add(this.pictureBoxHeader);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.panelStatus);
            this.Controls.Add(this.buttonBrowseLogFile);
            this.Controls.Add(this.textBoxLogFilename);
            this.Controls.Add(this.labelLogFilename);
            this.Controls.Add(this.checkBoxRecurseFolders);
            this.Controls.Add(this.checkBoxCopyFiles);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.buttonBrowseTargetFolder);
            this.Controls.Add(this.textBoxTargetFolder);
            this.Controls.Add(this.labelTargetFolder);
            this.Controls.Add(this.buttonBrowseSourceFolder);
            this.Controls.Add(this.textBoxSourceFolder);
            this.Controls.Add(this.labelSourceFolder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Photo Organizer";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHeader)).EndInit();
            this.panelStatus.ResumeLayout(false);
            this.panelStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelSourceFolder;
        private System.Windows.Forms.TextBox textBoxSourceFolder;
        private System.Windows.Forms.Button buttonBrowseSourceFolder;
        private System.Windows.Forms.Label labelTargetFolder;
        private System.Windows.Forms.TextBox textBoxTargetFolder;
        private System.Windows.Forms.Button buttonBrowseTargetFolder;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogSource;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogTarget;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.CheckBox checkBoxCopyFiles;
        private System.Windows.Forms.CheckBox checkBoxRecurseFolders;
        private System.Windows.Forms.Label labelLogFilename;
        private System.Windows.Forms.TextBox textBoxLogFilename;
        private System.Windows.Forms.Button buttonBrowseLogFile;
        private System.Windows.Forms.Label labelFilesRemaining;
        private System.Windows.Forms.Label labelSourceFile;
        private System.Windows.Forms.SaveFileDialog saveFileDialogLogFile;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Panel panelStatus;
        private System.Windows.Forms.Label labelTargetFile;
        private System.Windows.Forms.Label labelItemsRemaining;
        private System.Windows.Forms.Label labelTo;
        private System.Windows.Forms.Label labelFrom;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.PictureBox pictureBoxHeader;

    }
}

