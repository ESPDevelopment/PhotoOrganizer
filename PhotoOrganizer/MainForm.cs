using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PhotoOrganizer
{
    public partial class MainForm : Form
    {
        // Constant value for DatePictureTaken property item
        private const int PropertyTagExifDTOriginal = 0x9003;

        // Collection of specific date and time values (to ensure uniqueness)
        private Dictionary<DateTime, int> m_datesFound = null;

        // Collection of filenames retrieve from the target folder
        private Queue<string> m_fileNames = null;

        // Process counters
        private int m_filesFound = 0;
        private int m_filesCopied = 0;
        private int m_filesMoved = 0;
        private int m_filesFailed = 0;

        // Log file
        private StreamWriter m_logfile = null;

        // Callback delegates
        delegate void SetStatusCallback(string text);
        delegate void SetSourceFileCallback(string text);
        delegate void SetTargetFileCallback(string text);

        // Process parameters
        private string m_sourceFolder = "";
        private string m_targetFolder = "";
        private string m_logFilename = "";
        private bool m_copyFiles = true;
        private bool m_recurseFolders = true;

        /// <summary>
        /// Initializes the main form window
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            // Initialize form fields
            textBoxSourceFolder.Text = m_sourceFolder;
            textBoxTargetFolder.Text = m_targetFolder;
            textBoxLogFilename.Text = m_logFilename;
            checkBoxCopyFiles.Checked = m_copyFiles;
            checkBoxRecurseFolders.Checked = m_recurseFolders;

            // Hide status area
            ShowStatusArea(false);
        }

        /// <summary>
        /// Validates that all required parameters are valid.
        /// </summary>
        /// <returns>True if all required parameters are valid, otherwise False.</returns>
        private bool ParamsAreValid()
        {
            // Validate the source folder
            if (Directory.Exists(textBoxSourceFolder.Text) == false)
            {
                MessageBox.Show("The specified Source Folder does not exist.", "Photo Organizer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                buttonStart.Enabled = false;
                textBoxSourceFolder.SelectAll();
                textBoxSourceFolder.Focus();
                return false;
            }

            // Validate the target folder
            if (Directory.Exists(textBoxTargetFolder.Text) == false)
            {
                MessageBox.Show("The specified Target Folder does not exist.", "Photo Organizer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                buttonStart.Enabled = false;
                textBoxTargetFolder.SelectAll();
                textBoxTargetFolder.Focus();
                return false;
            }

            // Validate that the source and target folders are different
            if (textBoxSourceFolder.Text == textBoxTargetFolder.Text)
            {
                MessageBox.Show("Source and Target Folders must be different.", "Photo Organizer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                buttonStart.Enabled = false;
                textBoxSourceFolder.SelectAll();
                textBoxSourceFolder.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Organizes the image files by generating file names based on the date and
        /// time the picture was taken and then moving or copying them into the
        /// destination folder.
        /// </summary>
        /// <param name="worker"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool OrganizeFiles(BackgroundWorker worker, DoWorkEventArgs e)
        {
            // Initialize collection of found date/time values
            m_datesFound = new Dictionary<DateTime, int>();

            // Initialize file name queue
            m_fileNames = new Queue<string>();

            // Initialize process counters
            m_filesFound = 0;
            m_filesCopied = 0;
            m_filesMoved = 0;
            m_filesFailed = 0;

            // Create log file
            if (CreateLogFile(m_logFilename) == false)
                return false;

            // Get image filenames
            QueueFiles(m_sourceFolder, m_recurseFolders);
            m_filesFound = m_fileNames.Count;

            // Process image files
            ProcessFiles(worker, e);

            return true;
        }

        /// <summary>
        /// Create a log file.
        /// </summary>
        /// <param name="filename">Filename of the log file to be created</param>
        /// <returns></returns>
        private bool CreateLogFile(string filename)
        {
            try
            {
                // Create log file
                m_logfile = File.CreateText(filename);

                // Write header information
                m_logfile.WriteLine("{0}\tProcess started.", DateTime.Now.ToString("G"));
                m_logfile.WriteLine("{0}\tSource folder: {1}", DateTime.Now.ToString("G"), m_sourceFolder);
                m_logfile.WriteLine("{0}\tTarget folder: {1}", DateTime.Now.ToString("G"), m_targetFolder);
                m_logfile.WriteLine("{0}\tCopy files: {1}", DateTime.Now.ToString("G"), m_copyFiles);
                m_logfile.WriteLine("{0}\tRecurse folders: {1}", DateTime.Now.ToString("G"), m_recurseFolders);
                return true;
            }
            catch (Exception e1)
            {
                MessageBox.Show("Error creating log file.  Message: " + e1.Message, "Photo Organizer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        /// <summary>
        /// Retrieve image filenames and place them into the work queue for processing.
        /// </summary>
        /// <param name="sourceFolder">The source folder name</param>
        /// <param name="recurseFolders">Set to true to recurse folders, otherwise set to false</param>
        private void QueueFiles(string sourceFolder, bool recurseFolders)
        {
            // Append filenames in target folder
            string[] filenames = Directory.GetFiles(sourceFolder, "*.jpg");
            m_logfile.WriteLine("{0}\t{1} .JPG files found in folder {2}",  DateTime.Now.ToString("G"), filenames.Length, sourceFolder);
            foreach (string filename in filenames)
            {
                m_fileNames.Enqueue(filename);
                SetStatus(m_fileNames.Count.ToString());
            }

            // Recurse folders, if appropriate
            if (recurseFolders)
            {
                string[] folderEntries = Directory.GetDirectories(sourceFolder);
                m_logfile.WriteLine("{0}\t{1} folders found in folder {2}", DateTime.Now.ToString("G"), folderEntries.Length, sourceFolder);
                foreach (string folder in folderEntries)
                    QueueFiles(folder, recurseFolders);
            }
        }

        /// <summary>
        /// Reads EXIF meta data from the image file, constructs a new filename based on
        /// the date and time the picture was taken, and then moves or copies the file
        /// to a set of folders arranged by Year, then Month.
        /// </summary>
        /// <param name="worker"></param>
        /// <param name="e"></param>
        private void ProcessFiles(BackgroundWorker worker, DoWorkEventArgs e)
        {
            // Process all of the files in the queue
            while (m_fileNames.Count > 0)
            {
                string filename = m_fileNames.Dequeue();

                // Retrieve date picture taken from image file
                DateTime dateTaken = GetDatePictureTakenFromImage(filename);

                // Move or copy file
                if (dateTaken != DateTime.MinValue)
                {
                    // Construct new filename for image file
                    string newFilename = ConstructNewFilename(dateTaken);

                    // Update status text
                    SetSourceFile(filename);
                    SetTargetFile(newFilename);

                    // Move or copy file
                    CopyFile(filename, newFilename, m_copyFiles);
                } else {
                    m_filesFailed++;
                    }

                // Check for cancellation
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }

                // Report progress as a percentage of the total task
                int percentComplete = (int)((float)(m_filesFound - m_fileNames.Count) / (float)m_filesFound * 100);
                worker.ReportProgress(percentComplete);

                // Update files remaining
                SetStatus(m_fileNames.Count.ToString());
            }
        }

        /// <summary>
        /// Copies or moves the specified file to the specified filename.
        /// </summary>
        /// <param name="filename">The filename of the file to be moved or copied</param>
        /// <param name="newFilename">The destination filename</param>
        /// <param name="copy">True if the file should be copied, False if the file should be moved</param>
        private void CopyFile(string filename, string newFilename, bool copy)
        {
            // Set extension of new file to the extension of the original file
            FileInfo fileInfo = new FileInfo(filename);
            newFilename = newFilename + fileInfo.Extension;
            FileInfo newFileInfo = new FileInfo(newFilename);

            // Move or copy the image file to the new location
            try
            {
                // Create the folder, if necessary
                if (Directory.Exists(newFileInfo.DirectoryName) == false)
                {
                    Directory.CreateDirectory(newFileInfo.DirectoryName);
                    m_logfile.WriteLine("{0}\tCreated folder {1}", DateTime.Now.ToString("G"), newFileInfo.DirectoryName);
                }

                // Copy or move the file
                if (copy == true)
                {
                    fileInfo.CopyTo(newFilename);
                    m_filesCopied++;
                    m_logfile.WriteLine("{0}\tCopied file {1} to {2}", DateTime.Now.ToString("G"), filename, newFilename);
                }
                else
                {
                    fileInfo.MoveTo(newFilename);
                    m_filesMoved++;
                    m_logfile.WriteLine("{0}\tMoved file {1} to {2}", DateTime.Now.ToString("G"), filename, newFilename);
                }
            }
            catch (Exception e)
            {
                if (copy == true)
                    m_logfile.WriteLine("{0}\tERROR copying file {1} to {2}!\t{3}", DateTime.Now.ToString("G"), filename, newFilename, e.Message);
                else
                    m_logfile.WriteLine("{0}\tERROR moving file {1} to {2}!\t3", DateTime.Now.ToString("G"), filename, newFilename, e.Message);
                m_filesFailed++;
            }
            finally { }
        }

        /// <summary>
        /// Gets the date and time information from the image file using the
        /// Date Picture Taken EXIF property.
        /// </summary>
        /// <param name="filename">The name of the file to be examined</param>
        /// <returns></returns>
        private DateTime GetDatePictureTakenFromImage(string filename)
        {
            // Attempt to retrieve meta data for image files
            try
            {
                // Retrieve meta data from image file
                Image img = Image.FromFile(filename);
                PropertyItem item = img.GetPropertyItem(PropertyTagExifDTOriginal);
                string exifDateTaken = ASCIIEncoding.ASCII.GetString(item.Value);

                // Convert string to a date/time object (EXIF format is YYYY:MM:DD HH:MM:SS)
                string dateString = (exifDateTaken.Substring(0, 10)).Replace(":", "/");
                string timeString = exifDateTaken.Substring(11, 8);
                DateTime dateTaken = DateTime.ParseExact(dateString + " " + timeString, "yyyy/MM/dd HH:mm:ss", null);

                // Dispose of Image object
                img.Dispose();

                // Return dateTake
                return dateTaken;
            }
            catch (Exception e)
            {
                // Ignore error
                m_logfile.WriteLine("{0}\tERROR: File {1} is not an image file or does not contain valid EXIF properties.\t{2}", DateTime.Now.ToString("G"), filename, e.Message);
            }
            return DateTime.MinValue;
        }

        /// <summary>
        /// Constructs a new filename for the Image using the date and time
        /// information provided to the method.
        /// </summary>
        /// <param name="dateTaken">The Date/Time data from the image file</param>
        /// <returns>A fully qualified filename</returns>
        private string ConstructNewFilename(DateTime dateTaken)
        {
            // Update collection of dates found
            int count = 1;
            if (m_datesFound.ContainsKey(dateTaken))
            {
                count = m_datesFound[dateTaken] + 1;
                m_datesFound[dateTaken] = count;
            }
            else
            {
                m_datesFound.Add(dateTaken, count);
            }
            dateTaken = dateTaken.AddMilliseconds(count);

            // Construct filename
            string newFilename = textBoxTargetFolder.Text +
                "\\" +
                "Pictures " +
                dateTaken.ToString("yyyy") +
                "\\" +
                dateTaken.ToString("yyyy-MM MMMM") +
                "\\" +
                "Picture " +
                dateTaken.ToString("yyyy-MM-dd HHmmss FFF");
            return newFilename;
        }

        /// <summary>
        /// Set the status label to the specified value using
        /// a thread safe method.
        /// </summary>
        /// <param name="text">The text value to set</param>
        private void SetStatus(string text)
        {
            // Check to see if we need to invoke the callback or if
            // we can set the value directly
            if (this.labelFilesRemaining.InvokeRequired)
            {
                SetStatusCallback d = new SetStatusCallback(SetStatus);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.labelFilesRemaining.Text = text;
            }
        }

        /// <summary>
        /// Set the Source File status label to the specified value using
        /// a thread safe method.
        /// </summary>
        /// <param name="text">The text value to set</param>
        private void SetSourceFile(string text)
        {
            // Check to see if we need to invoke the callback or if
            // we can set the value directly
            if (this.labelSourceFile.InvokeRequired)
            {
                SetSourceFileCallback d = new SetSourceFileCallback(SetSourceFile);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.labelSourceFile.Text = text;
            }
        }

        /// <summary>
        /// Set the Target File status label to the specified value using
        /// a thread safe method.
        /// </summary>
        /// <param name="text">The text value to set</param>
        private void SetTargetFile(string text)
        {
            // Check to see if we need to invoke the callback or if
            // we can set the value directly
            if (this.labelTargetFile.InvokeRequired)
            {
                SetTargetFileCallback d = new SetTargetFileCallback(SetTargetFile);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.labelTargetFile.Text = text;
            }
        }

        /// <summary>
        /// Enable or disable the main form controls
        /// </summary>
        /// <param name="enabled">True to enable or False to disable the controls</param>
        private void EnableMainForm(bool enabled)
        {
            // Enable or disable all of the main form controls
            this.labelSourceFolder.Enabled = enabled;
            this.labelTargetFolder.Enabled = enabled;
            this.labelLogFilename.Enabled = enabled;
            this.textBoxSourceFolder.Enabled = enabled;
            this.textBoxTargetFolder.Enabled = enabled;
            this.textBoxLogFilename.Enabled = enabled;
            this.buttonBrowseSourceFolder.Enabled = enabled;
            this.buttonBrowseTargetFolder.Enabled = enabled;
            this.buttonBrowseLogFile.Enabled = enabled;
            this.checkBoxCopyFiles.Enabled = enabled;
            this.checkBoxRecurseFolders.Enabled = enabled;
            this.buttonStart.Enabled = enabled;
            this.buttonExit.Enabled = enabled;

            // Refresh the form
            this.Refresh();
        }

        /// <summary>
        /// Show or hide the status area
        /// </summary>
        /// <param name="show">True to show the status area or False to hide it</param>
        private void ShowStatusArea(bool show)
        {
            // Expand or contract main form to show or hide the status area (382/225) 245
            this.Height = (show == true) ? 402 : 245;
            this.Refresh();
        }

        /// <summary>
        /// Validate required parameters and begin the organization process if
        /// all required parameters are valid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonStart_Click(object sender, EventArgs e)
        {
            // Validate required parameters
            if (ParamsAreValid())
            {
                // Retrieve parameters from the form
                m_sourceFolder = textBoxSourceFolder.Text;
                m_targetFolder = textBoxTargetFolder.Text;
                m_logFilename = textBoxLogFilename.Text;
                m_copyFiles = checkBoxCopyFiles.Checked;
                m_recurseFolders = checkBoxRecurseFolders.Checked;

                // Disable main form controls
                EnableMainForm(false);

                // Show status area and enable cancel button
                this.buttonCancel.Enabled = true;
                this.buttonCancel.Visible = true;
                this.buttonOK.Enabled = false;
                this.buttonOK.Visible = false;
                this.buttonCancel.Focus();
                ShowStatusArea(true);

                // Organize photos
                backgroundWorker.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Close the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonExit_Click(object sender, EventArgs e)
        {
            // Close the application
            this.Close();
        }

        /// <summary>
        /// Cancel the current operation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            // Cancel the asynchronous operation.
            this.backgroundWorker.CancelAsync();
            this.buttonCancel.Enabled = false;
        }

        /// <summary>
        /// Hide the status area and enable the main form controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            // Hide status area and enable main form
            ShowStatusArea(false);
            EnableMainForm(true);
        }
        
        /// <summary>
        /// Opens a folder browse dialog box to allow the user to select a source folder
        /// containing the photo images to be organized.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBrowseSourceFolder_Click(object sender, EventArgs e)
        {
            // Display folder browser dialog and update the source folder text box
            // if the user selected a new folder
            DialogResult result = folderBrowserDialogSource.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBoxSourceFolder.Text = folderBrowserDialogSource.SelectedPath;
            }
        }

        /// <summary>
        /// Opens a folder browse dialog box to allow the user to select a destination folder
        /// into which the selected photo images will be organized.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBrowseDestFolder_Click(object sender, EventArgs e)
        {
            // Display folder browser dialog and update the destination folder text box
            // if the user selected a new folder
            DialogResult result = folderBrowserDialogTarget.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBoxTargetFolder.Text = folderBrowserDialogTarget.SelectedPath;
            }
        }

        /// <summary>
        /// Opens a save file dialog box to allow the user to select a filename for
        /// the log file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBrowseLogFile_Click(object sender, EventArgs e)
        {
            // Display a save file dialog and update the log filename text box
            // if the user selected a new filename
            DialogResult result = saveFileDialogLogFile.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBoxLogFilename.Text = saveFileDialogLogFile.FileName;
            }
        }

        /// <summary>
        /// Triggered by a TextChanged event with the source folder text box,
        /// checks to see if the source and target folder text boxes contain
        /// valid input.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxSourceFolder_TextChanged(object sender, EventArgs e)
        {
            buttonStart.Enabled = (textBoxSourceFolder.Text.Length > 0 && textBoxTargetFolder.Text.Length > 0);
        }

        /// <summary>
        /// Triggered by a TextChanged event with the target folder text box,
        /// checks to see if the source and target folder text boxes contain
        /// valid input.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxTargetFolder_TextChanged(object sender, EventArgs e)
        {
            buttonStart.Enabled = (textBoxSourceFolder.Text.Length > 0 && textBoxTargetFolder.Text.Length > 0);
        }

        /// <summary>
        /// Executes the background work of processing image files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // Get the BackgroundWorker that raised this event.
            BackgroundWorker worker = sender as BackgroundWorker;
            e.Result = OrganizeFiles(worker, e);
        }

        /// <summary>
        /// Responds to the ProgressChanged event by updating the progress bar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Update the progress bar
            this.progressBar.Value = e.ProgressPercentage;
        }

        /// <summary>
        /// Executes after the background worker completes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Write statistics to the log file
            m_logfile.WriteLine("{0}\tFiles found : {1}", DateTime.Now.ToString("G"), m_filesFound);
            m_logfile.WriteLine("{0}\tFiles copied: {1}", DateTime.Now.ToString("G"), m_filesCopied);
            m_logfile.WriteLine("{0}\tFiles moved : {1}", DateTime.Now.ToString("G"), m_filesMoved);
            m_logfile.WriteLine("{0}\tFiles failed: {1}", DateTime.Now.ToString("G"), m_filesFailed);

            // First, handle the case where an exception was thrown.
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled the operation.
                // Note that due to a race condition in the DoWork event handler
                // the Cancelled flag may not have been set, even though
                // CancelAsync was called.
                SetStatus("Process cancelled by user.");
                m_logfile.WriteLine("{0}\tProcess cancelled by user.", DateTime.Now.ToString("G"));
            }
            else
            {
                // Finally, handle the case where the operation succeeded.
                SetStatus("Process completed successfully.");
                m_logfile.WriteLine("{0}\tProcess completed successfully.", DateTime.Now.ToString("G"));
            }

            // Close the log file
            m_logfile.Close();

            // Disable and hide the cancel button
            this.buttonCancel.Visible = false;
            this.buttonCancel.Enabled = false;

            // Enable and show the OK button
            this.buttonOK.Enabled = true;
            this.buttonOK.Visible = true;
            this.buttonOK.Focus();
            this.Refresh();

            // Clear the file labels
            this.labelSourceFile.Text = "";
            this.labelTargetFile.Text = "";
        }

    }
}
