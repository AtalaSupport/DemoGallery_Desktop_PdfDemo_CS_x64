using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using Atalasoft.Imaging;
using Atalasoft.Imaging.Codec;
using Atalasoft.Imaging.Codec.Pdf;
using Atalasoft.Imaging.WinControls;
using Atalasoft.Annotate.UI;
using Atalasoft.Annotate;
using Atalasoft.PdfDoc;


namespace PdfDemo
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private Atalasoft.Annotate.UI.AnnotateViewer workspaceViewer1;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.StatusBar statusBar1;
		private System.Windows.Forms.MenuItem menuFile;
		private System.Windows.Forms.MenuItem menuOpen;
		private System.Windows.Forms.MenuItem menuGetInfo;
		private System.Windows.Forms.MenuItem menuExtractImages;
		private Atalasoft.Imaging.WinControls.ThumbnailView thumbnailView1;
		private System.Windows.Forms.StatusBarPanel statusBarPanel1;
		private System.Windows.Forms.ProgressBar progressBar1;
		private PdfDecoder _pdf;
		private bool _extractedImages = false;
		private System.Windows.Forms.MenuItem menuView;
		private System.Windows.Forms.MenuItem menuViewFullSize;
		private System.Windows.Forms.MenuItem menuViewFitWidth;
		private System.Windows.Forms.MenuItem menuViewBestFit;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuSave;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuPrint;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItemFind;
		private System.Windows.Forms.StatusBarPanel statusProgress;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.ComponentModel.IContainer components;

		private string _currentFile = null;
		private int _currentResolution = -1;

		// members to control text search
		private FindDialog _findDialog;
        private TabControl tabControl1;
        private TabPage tabPages;
        private TabPage tabBookmarks;
        private Splitter splitter1;
        private TreeView treeBookmarks;
		private PdfDocumentSearch _pdfDocSearch;
		
		
		public Form1()
		{

			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			try
			{
				_pdf = new PdfDecoder();
				_pdf.Resolution = 150;
                _pdf.RenderSettings = new RenderSettings() { AnnotationSettings = AnnotationRenderSettings.RenderNone };

				//add the PDFDecoder to so PDF becomes a Known image type
				RegisteredDecoders.Decoders.Insert(0, _pdf);
			}
			catch (Atalasoft.Imaging.AtalasoftLicenseException)
			{
				this.menuFile.Enabled = false;
				ShowLicenseMessage("PdfReader");
			}

			this.workspaceViewer1.ImageBorderPen = new Atalasoft.Imaging.Drawing.AtalaPen(Color.Black, 1);
			workspaceViewer1.Annotations.Layers.Add(new LayerAnnotation());			
		}

		private void ShowLicenseMessage(string product)
		{
			if (MessageBox.Show("This demo requires a " + product + " license.\r\nAn evaluation license can be requested with our activation utility.\r\n\r\nWould you like to run this utility now?", product + " License Required", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
			{
				string activationUtil = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles) + "\\Atalasoft\\DotImage ";

				// Use reflection to find out which version of DotImage we are using.
				System.Reflection.AssemblyName[] assemblies = System.Reflection.Assembly.GetExecutingAssembly().GetReferencedAssemblies();
				foreach (System.Reflection.AssemblyName name in assemblies)
				{
					if (name.Name == "Atalasoft.dotImage")
					{
						activationUtil += name.Version.ToString(2);
						break;
					}
				}

				activationUtil += "\\AtalasoftToolkitActivation.exe";

				if (System.IO.File.Exists(activationUtil))
					System.Diagnostics.Process.Start(activationUtil);
				else
					MessageBox.Show("We were unable to location the activation utility on this system.\r\nPlease run it from the start menu.", "AtalasoftToolkitActivation.exe Not Found");
			}
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.workspaceViewer1 = new Atalasoft.Annotate.UI.AnnotateViewer();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuFile = new System.Windows.Forms.MenuItem();
            this.menuOpen = new System.Windows.Forms.MenuItem();
            this.menuSave = new System.Windows.Forms.MenuItem();
            this.menuItemFind = new System.Windows.Forms.MenuItem();
            this.menuExtractImages = new System.Windows.Forms.MenuItem();
            this.menuGetInfo = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuPrint = new System.Windows.Forms.MenuItem();
            this.menuView = new System.Windows.Forms.MenuItem();
            this.menuViewFullSize = new System.Windows.Forms.MenuItem();
            this.menuViewFitWidth = new System.Windows.Forms.MenuItem();
            this.menuViewBestFit = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.statusBarPanel1 = new System.Windows.Forms.StatusBarPanel();
            this.statusProgress = new System.Windows.Forms.StatusBarPanel();
            this.thumbnailView1 = new Atalasoft.Imaging.WinControls.ThumbnailView();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPages = new System.Windows.Forms.TabPage();
            this.tabBookmarks = new System.Windows.Forms.TabPage();
            this.treeBookmarks = new System.Windows.Forms.TreeView();
            this.splitter1 = new System.Windows.Forms.Splitter();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusProgress)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPages.SuspendLayout();
            this.tabBookmarks.SuspendLayout();
            this.SuspendLayout();
            // 
            // workspaceViewer1
            // 
            this.workspaceViewer1.AntialiasDisplay = Atalasoft.Imaging.WinControls.AntialiasDisplayMode.ReductionOnly;
            this.workspaceViewer1.Asynchronous = true;
            this.workspaceViewer1.BackColor = System.Drawing.SystemColors.Control;
            this.workspaceViewer1.Centered = true;
            this.workspaceViewer1.DefaultSecurity = null;
            this.workspaceViewer1.DisplayProfile = null;
            this.workspaceViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.workspaceViewer1.Location = new System.Drawing.Point(175, 0);
            this.workspaceViewer1.Magnifier.BackColor = System.Drawing.Color.White;
            this.workspaceViewer1.Magnifier.BorderColor = System.Drawing.Color.Black;
            this.workspaceViewer1.Magnifier.Size = new System.Drawing.Size(100, 100);
            this.workspaceViewer1.MouseWheelScrolling = true;
            this.workspaceViewer1.Name = "workspaceViewer1";
            this.workspaceViewer1.OutputProfile = null;
            this.workspaceViewer1.RotationSnapInterval = 0F;
            this.workspaceViewer1.RotationSnapThreshold = 0F;
            this.workspaceViewer1.Selection = null;
            this.workspaceViewer1.Size = new System.Drawing.Size(785, 686);
            this.workspaceViewer1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            this.workspaceViewer1.TabIndex = 0;
            this.workspaceViewer1.Text = "workspaceViewer1";
            this.workspaceViewer1.ToolTip = null;
            this.workspaceViewer1.UndoManager.Levels = 0;
            this.workspaceViewer1.ImageChanged += new Atalasoft.Imaging.ImageEventHandler(this.workspaceViewer1_ChangedImage);
            this.workspaceViewer1.ProcessError += new Atalasoft.Imaging.ExceptionEventHandler(this.workspaceViewer1_ProcessError);
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuFile,
            this.menuView,
            this.menuItem1});
            // 
            // menuFile
            // 
            this.menuFile.Index = 0;
            this.menuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuOpen,
            this.menuSave,
            this.menuItemFind,
            this.menuExtractImages,
            this.menuGetInfo,
            this.menuItem7,
            this.menuPrint});
            this.menuFile.Text = "File";
            // 
            // menuOpen
            // 
            this.menuOpen.Index = 0;
            this.menuOpen.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
            this.menuOpen.Text = "Open";
            this.menuOpen.Click += new System.EventHandler(this.menuOpen_Click);
            // 
            // menuSave
            // 
            this.menuSave.Index = 1;
            this.menuSave.Text = "Save As";
            this.menuSave.Click += new System.EventHandler(this.menuSave_Click);
            // 
            // menuItemFind
            // 
            this.menuItemFind.Enabled = false;
            this.menuItemFind.Index = 2;
            this.menuItemFind.Shortcut = System.Windows.Forms.Shortcut.CtrlF;
            this.menuItemFind.Text = "Find ...";
            this.menuItemFind.Click += new System.EventHandler(this.menuItemFind_Click);
            // 
            // menuExtractImages
            // 
            this.menuExtractImages.Index = 3;
            this.menuExtractImages.Text = "Extract Images";
            this.menuExtractImages.Click += new System.EventHandler(this.menuExtractImages_Click);
            // 
            // menuGetInfo
            // 
            this.menuGetInfo.Index = 4;
            this.menuGetInfo.Text = "Get Information";
            this.menuGetInfo.Click += new System.EventHandler(this.menuGetInfo_Click);
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 5;
            this.menuItem7.Text = "-";
            // 
            // menuPrint
            // 
            this.menuPrint.Index = 6;
            this.menuPrint.Text = "Print ... ";
            this.menuPrint.Click += new System.EventHandler(this.menuPrint_Click);
            // 
            // menuView
            // 
            this.menuView.Index = 1;
            this.menuView.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuViewFullSize,
            this.menuViewFitWidth,
            this.menuViewBestFit});
            this.menuView.Text = "View";
            this.menuView.Click += new System.EventHandler(this.menuView_Click);
            // 
            // menuViewFullSize
            // 
            this.menuViewFullSize.Index = 0;
            this.menuViewFullSize.Text = "Full Size";
            this.menuViewFullSize.Click += new System.EventHandler(this.menuViewFullSize_Click);
            // 
            // menuViewFitWidth
            // 
            this.menuViewFitWidth.Index = 1;
            this.menuViewFitWidth.Text = "Fit To Width";
            this.menuViewFitWidth.Click += new System.EventHandler(this.menuViewFitWidth_Click);
            // 
            // menuViewBestFit
            // 
            this.menuViewBestFit.Index = 2;
            this.menuViewBestFit.Text = "Best Fit";
            this.menuViewBestFit.Click += new System.EventHandler(this.menuViewBestFit_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 2;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem6});
            this.menuItem1.Text = "Help";
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 0;
            this.menuItem6.Text = "About ...";
            this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "pdf";
            this.openFileDialog1.Filter = "PDF Files|*.pdf|All Files|*.*";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "pdf";
            this.saveFileDialog1.Filter = "PDF File(*.pdf)|*.pdf|TIFF File(*tif)|*.tif|JPEG File(*.jpg)|*.jpg";
            // 
            // statusBar1
            // 
            this.statusBar1.Location = new System.Drawing.Point(0, 686);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.statusBarPanel1,
            this.statusProgress});
            this.statusBar1.ShowPanels = true;
            this.statusBar1.Size = new System.Drawing.Size(960, 22);
            this.statusBar1.TabIndex = 1;
            // 
            // statusBarPanel1
            // 
            this.statusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            this.statusBarPanel1.Name = "statusBarPanel1";
            this.statusBarPanel1.Width = 643;
            // 
            // statusProgress
            // 
            this.statusProgress.Name = "statusProgress";
            this.statusProgress.Width = 300;
            // 
            // thumbnailView1
            // 
            this.thumbnailView1.BackColor = System.Drawing.Color.Gray;
            this.thumbnailView1.CaptionLines = 0;
            this.thumbnailView1.DisplayText = Atalasoft.Imaging.WinControls.ThumbViewAttribute.None;
            this.thumbnailView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.thumbnailView1.DragSelectionColor = System.Drawing.Color.Red;
            this.thumbnailView1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.thumbnailView1.HighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.thumbnailView1.HighlightTextColor = System.Drawing.SystemColors.HighlightText;
            this.thumbnailView1.LoadErrorMessage = "";
            this.thumbnailView1.LoadMethod = Atalasoft.Imaging.WinControls.ThumbLoadMethod.EntireFolder;
            this.thumbnailView1.Location = new System.Drawing.Point(3, 3);
            this.thumbnailView1.Margins = new Atalasoft.Imaging.WinControls.Margin(4, 4, 4, 4);
            this.thumbnailView1.SelectionMode = ThumbnailSelectionMode.SingleSelect;
            this.thumbnailView1.Name = "thumbnailView1";
            this.thumbnailView1.SelectionRectangleBackColor = System.Drawing.Color.Transparent;
            this.thumbnailView1.SelectionRectangleDashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.thumbnailView1.SelectionRectangleLineColor = System.Drawing.Color.Black;
            this.thumbnailView1.Size = new System.Drawing.Size(155, 654);
            this.thumbnailView1.TabIndex = 2;
            this.thumbnailView1.Text = "thumbnailView1";
            this.thumbnailView1.ThumbnailBackground = null;
            this.thumbnailView1.ThumbnailOffset = new System.Drawing.Point(0, 0);
            this.thumbnailView1.ThumbnailSize = new System.Drawing.Size(100, 100);
            this.thumbnailView1.SelectedIndexChanged += new System.EventHandler(this.thumbnailView1_SelectedIndexChanged);
            this.thumbnailView1.ThumbnailCreated += new Atalasoft.Imaging.WinControls.ThumbnailEventHandler(this.thumbnailView1_ThumbnailLoad);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(646, 688);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(300, 20);
            this.progressBar1.TabIndex = 3;
            // 
            // menuItem2
            // 
            this.menuItem2.Index = -1;
            this.menuItem2.Text = "10";
            // 
            // menuItem3
            // 
            this.menuItem3.Index = -1;
            this.menuItem3.Text = "75";
            // 
            // menuItem4
            // 
            this.menuItem4.Index = -1;
            this.menuItem4.Text = "200";
            // 
            // menuItem5
            // 
            this.menuItem5.Index = -1;
            this.menuItem5.Text = "300";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPages);
            this.tabControl1.Controls.Add(this.tabBookmarks);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(169, 686);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPages
            // 
            this.tabPages.Controls.Add(this.thumbnailView1);
            this.tabPages.Location = new System.Drawing.Point(4, 22);
            this.tabPages.Name = "tabPages";
            this.tabPages.Padding = new System.Windows.Forms.Padding(3);
            this.tabPages.Size = new System.Drawing.Size(161, 660);
            this.tabPages.TabIndex = 0;
            this.tabPages.Text = "Pages";
            this.tabPages.UseVisualStyleBackColor = true;
            // 
            // tabBookmarks
            // 
            this.tabBookmarks.Controls.Add(this.treeBookmarks);
            this.tabBookmarks.Location = new System.Drawing.Point(4, 22);
            this.tabBookmarks.Name = "tabBookmarks";
            this.tabBookmarks.Padding = new System.Windows.Forms.Padding(3);
            this.tabBookmarks.Size = new System.Drawing.Size(161, 660);
            this.tabBookmarks.TabIndex = 1;
            this.tabBookmarks.Text = "Bookmarks";
            this.tabBookmarks.UseVisualStyleBackColor = true;
            // 
            // treeBookmarks
            // 
            this.treeBookmarks.BackColor = System.Drawing.Color.Gray;
            this.treeBookmarks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeBookmarks.ForeColor = System.Drawing.Color.White;
            this.treeBookmarks.Location = new System.Drawing.Point(3, 3);
            this.treeBookmarks.Name = "treeBookmarks";
            this.treeBookmarks.Size = new System.Drawing.Size(155, 654);
            this.treeBookmarks.TabIndex = 0;
            this.treeBookmarks.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeBookmarks_AfterSelect);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(169, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(6, 686);
            this.splitter1.TabIndex = 5;
            this.splitter1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(960, 708);
            this.Controls.Add(this.workspaceViewer1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.statusBar1);
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.Text = "PDF Demo";
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusProgress)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPages.ResumeLayout(false);
            this.tabBookmarks.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void menuOpen_Click(object sender, System.EventArgs e)
		{

			if (this.openFileDialog1.ShowDialog(this) == DialogResult.OK)
			{				
				string file = this.openFileDialog1.FileName;
				int frameCount = RegisteredDecoders.GetImageInfo(file, 0).FrameCount;

				//set decoder properties
				Form frm = new Parameters("PDF DEcoder Properties", _pdf);
				if (frm.ShowDialog(this) == DialogResult.OK)
				{
					this.thumbnailView1.Items.Cancel();
					this.thumbnailView1.Items.Clear();

					//reset progress bar
					this.progressBar1.Maximum = frameCount;
					this.progressBar1.Value = 0;
					
					// Create the Thumbnail objects and pass them into the ThumbnailView all at once.
					Thumbnail[] thumbs = new Thumbnail[frameCount];
					for (int i = 0; i < frameCount; i++)
					{
						thumbs[i] = new Thumbnail(file, i, "", "");
					}
					this.thumbnailView1.Items.AddRange(thumbs);

					//open the first full size page
					this.workspaceViewer1.Open(file, 0);

					_extractedImages = false;
					_currentFile = file;
					_currentResolution = _pdf.Resolution;
					this.workspaceViewer1.Annotations.CurrentLayer.Items.Clear();
					menuItemFind.Enabled = true;

                    LoadPdfBookmarks(file);
				}
			}
		}

		private void menuGetInfo_Click(object sender, System.EventArgs e)
		{
			if (this.openFileDialog1.ShowDialog(this) == DialogResult.OK)
			{
				FileStream fs = new FileStream(this.openFileDialog1.FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
				ImageInfo info = _pdf.GetImageInfo(fs);
				PdfImageInfo pdfInfo = (PdfImageInfo)info;
				Form frm = new Parameters("PDF Image Information", info);
				frm.ShowDialog();
				frm.Dispose();
			}

		}

		private void menuExtractImages_Click(object sender, System.EventArgs e)
		{
			if (this.openFileDialog1.ShowDialog(this) == DialogResult.OK)
			{
				this.workspaceViewer1.Images.Clear();
				this.thumbnailView1.Items.Cancel();
				this.thumbnailView1.Items.Clear();
			
				FileStream fs = new FileStream(this.openFileDialog1.FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
				
				// Document objects must be disposed.  This makes sure it happens even if there
				// is an exception
				using (Document document = new Document( fs )) 
				{
					//reset progress bar, estimate one image per page
					this.progressBar1.Maximum = 0;
					this.progressBar1.Value = 0;
					

					for (int i = 0; i < document.Pages.Count; i++)
					{
					
						ExtractedImageInfo[] extractedImages = document.Pages[i].ExtractImages();
						// check progressbar
						this.progressBar1.Maximum += extractedImages.Length;
						foreach (ExtractedImageInfo info in extractedImages)
						{
							this.workspaceViewer1.Images.Add((AtalaImage)info.Image.Clone());
							this.thumbnailView1.Items.Add(info.Image, "");
						}
					}
				}
				_extractedImages = true;
				fs.Close();
			}
		}

		private void thumbnailView1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (this.thumbnailView1.SelectedItems.Count > 0)
			{
                this.workspaceViewer1.ScrollPosition = new Point(0, 0);

				if (_extractedImages)
					this.workspaceViewer1.Image = this.workspaceViewer1.Images[this.thumbnailView1.SelectedIndices[0]];
				else {
					this.workspaceViewer1.Open(this.openFileDialog1.FileName, this.thumbnailView1.SelectedIndices[0]);
					this.workspaceViewer1.Annotations.CurrentLayer.Items.Clear();
				}
			}
		}

		private void menuSave_Click(object sender, System.EventArgs e)
		{
			if (this.saveFileDialog1.ShowDialog(this) == DialogResult.OK)
			{
				ImageEncoder encoder = null;
				switch (this.saveFileDialog1.FilterIndex)
				{
					case 1:
						//for more control over saving, use the PdfCollection and PdfImage classes
						encoder = new PdfEncoder();
						break;
					case 2:
						encoder = new TiffEncoder();
						break;
					case 3:
						encoder = new JpegEncoder();
						break;
				}
				this.workspaceViewer1.Save(this.saveFileDialog1.FileName, encoder);
			}
		}

		private void workspaceViewer1_ChangedImage(object sender, Atalasoft.Imaging.ImageEventArgs e)
		{
			if (e.Image != null)
				this.statusBar1.Panels[0].Text = e.Image.ToString();
		}

		private void workspaceViewer1_ProcessError(object sender, Atalasoft.Imaging.ExceptionEventArgs e)
		{
			MessageBox.Show(this, e.Exception.ToString());
		}

		private void menuView_Click(object sender, System.EventArgs e)
		{
			switch (this.workspaceViewer1.AutoZoom)
			{
				case AutoZoomMode.FitToWidth:
					menuViewFitWidth.Checked = true;
					menuViewFullSize.Checked = false;
					menuViewBestFit.Checked = false;
					break;
				case AutoZoomMode.BestFit:
					menuViewFitWidth.Checked = false;
					menuViewFullSize.Checked = false;
					menuViewBestFit.Checked = true;
					break;
				default:
					menuViewFitWidth.Checked = false;
					menuViewFullSize.Checked = true;
					menuViewBestFit.Checked = false;
					break;
			}
		}

		private void menuViewFullSize_Click(object sender, System.EventArgs e)
		{
			this.workspaceViewer1.AutoZoom = AutoZoomMode.None;
			this.workspaceViewer1.Zoom = 1.0;
		}

		private void menuViewFitWidth_Click(object sender, System.EventArgs e)
		{
			this.workspaceViewer1.AutoZoom = AutoZoomMode.FitToWidth;
		}

		private void menuViewBestFit_Click(object sender, System.EventArgs e)
		{
			this.workspaceViewer1.AutoZoom = AutoZoomMode.BestFit;
		}

		private void menuItem6_Click(object sender, System.EventArgs e)
		{
			AtalaDemos.AboutBox.About aboutBox = new AtalaDemos.AboutBox.About("About Atalasoft DotImage PDF Demo",
				"DotImage PDF Demo");
			aboutBox.Description = @"Demonstrates how to view and save PDF files with DotImage and DotImage Pdf Reader (PdfDecoder).  Rasterizers a small thumbnail of each page in the PDF asynchronously while loading the first page in the PDF.  Demonstrates use of the ThumbnailView control.

";
			aboutBox.ShowDialog();

		}

		private void thumbnailView1_ThumbnailLoad(object sender, Atalasoft.Imaging.WinControls.ThumbnailEventArgs e)
		{
			// update the progressbar for every thumbnail load.
			this.progressBar1.Value += 1;
			if (this.progressBar1.Value == this.progressBar1.Maximum)
				this.progressBar1.Value = 0;
		}

		#region Printing

		private Document docToPrint = null;
		private Pages imagesToPrint = null; 
		private int current = 0; 

		private void menuPrint_Click(object sender, System.EventArgs e)
		{
			if (this.openFileDialog1.FileName == "") // make sure an image is loaded
				return;

			FileStream fs = new FileStream(this.openFileDialog1.FileName, FileMode.Open, FileAccess.Read, FileShare.Read); 
			docToPrint = new Document(fs);
			this.imagesToPrint = docToPrint.Pages; 
			// Use System.Drawing.Print.PrintDocument 
			PrintDocument thePrintDoc = new PrintDocument(); 
			thePrintDoc.PrintPage +=new PrintPageEventHandler(thePrintDoc_PrintPage); 
			this.current = 0; 
			thePrintDoc.Print(); 
			fs.Close(); 
		}

		private void thePrintDoc_PrintPage(object sender, PrintPageEventArgs e) 
		{ 
			e.HasMorePages = true; 
			Page p = imagesToPrint[current++]; 
			// fit to page, only when image is too large. 
			float newX = (float)(e.PageBounds.Width / p.Width); 
			float newY = (float)(e.PageBounds.Height / p.Height); 
			if (!(newX > 1 && newY > 1)) 
				e.Graphics.ScaleTransform(newX, newY); 
			// Draw pdf image onto graphics object here.
			RenderSettings rs = new RenderSettings();
			rs.AnnotationSettings = AnnotationRenderSettings.RenderAll;
			p.Draw(e.Graphics, PageBoundary.Default, rs, 500); 
			if (current >= imagesToPrint.Count) 
			{
				e.HasMorePages = false; 
				docToPrint.Dispose(); // make sure to dispose
				docToPrint = null;
			}
		}

		#endregion

		private void menuItemFind_Click(object sender, System.EventArgs e)
		{
			if (_findDialog == null && _currentFile != null) 
			{
				_pdfDocSearch = new PdfDocumentSearch(GetCurrentPage());
				_findDialog = new FindDialog();					
				_findDialog.Closed += new EventHandler(findDialog_Closed);
				_findDialog.OnFindNext += new PdfDemo.FindDialog.FindNextHandler(findDialog_OnFindNext);
				_findDialog.Show();
			}			
		}

		private void findDialog_Closed(object sender, EventArgs e)
		{
			_findDialog = null;
		}

		private int GetCurrentPage()
		{
			if (this.thumbnailView1.SelectedIndices.Length > 0) 
			{
				return this.thumbnailView1.SelectedIndices[0];
			}
			return 0;
		}

		
		/// <summary>
		/// This is called when the next button is clicked on the find dialog
		/// </summary>
		/// <param name="text">The text in the find dialog</param>
		/// <param name="matchCase">true if the search should match case</param>
		/// <param name="wholeWord">true if the search should only find whole words</param>
		private void findDialog_OnFindNext(String findText, bool matchCase, bool wholeWord)
		{
			// find and highlight the text
			bool found = false;
			if (_currentFile != null)
			{	
				PdfFindHighlighter findHighlighter = new PdfFindHighlighter(workspaceViewer1, thumbnailView1, _currentResolution);
				using (FileStream fs = new FileStream(_currentFile, FileMode.Open, FileAccess.Read, FileShare.Read)) 
				{
					found = _pdfDocSearch.FindNext(fs, findText, matchCase, wholeWord, new PdfDocumentSearch.FindTextHandler(findHighlighter.HighlightFoundText));
				}
				if (!found) 
				{
					MessageBox.Show("The specified text was not found.", "PDF Viewer Search", MessageBoxButtons.OK);
				}
			}
        }

        #region Bookmark Code

        // When a bookmark is selected, load the page and scroll to the bookmark position.
        private void treeBookmarks_AfterSelect(object sender, TreeViewEventArgs e)
        {
            PdfBookmark bm = e.Node.Tag as PdfBookmark;
            if (bm != null && bm.ClickAction.Count > 0)
            {
                PdfAction action = bm.ClickAction[0];
                if (action.ActionType == PdfActionType.GoToView)
                {
                    PdfGoToViewAction gotoView = action as PdfGoToViewAction;
                    if (gotoView != null && gotoView.Destination != null && gotoView.Destination.Page != null)
                    {
                        PdfIndexedPageReference page = gotoView.Destination.Page as PdfIndexedPageReference;
                        if (page != null)
                        {
                            if (page.PageIndex >= 0 && page.PageIndex < this.thumbnailView1.Items.Count)
                            {
                                // Selecting the thumbnail will load the page.
                                int pageIndex = GetCurrentPage();
                                if (pageIndex != page.PageIndex)
                                {
                                    this.thumbnailView1.ClearSelection();
                                    this.thumbnailView1.Items[page.PageIndex].Selected = true;
                                    this.thumbnailView1.ScrollTo(page.PageIndex);
                                }

                                /*
                                    The bookmark coordinates are in PDF page coordinates.
                                    (0,0) is the lower left corner, increases up and to the right
                                    Units are 1/72”
                                */
                                int y = (gotoView.Destination.Top.HasValue ? Convert.ToInt32((gotoView.Destination.Top.Value / 72.0) * this.workspaceViewer1.Image.Resolution.Y) : 0);
                                y = this.workspaceViewer1.Image.Height - y;
                                this.workspaceViewer1.ScrollPosition = new Point(0, -y);
                            }
                        }
                    }
                }
            }
        }

        private void LoadPdfBookmarks(string fileName)
        {
            this.treeBookmarks.Nodes.Clear();

            PdfDocument doc = new PdfDocument(fileName);
            if (doc.BookmarkTree != null)
            {
                foreach (PdfBookmark bookmark in doc.BookmarkTree.Bookmarks)
                {
                    AddBookMark(null, bookmark);
                }
            }
        }

        private void AddBookMark(TreeNode parent, PdfBookmark bookMark)
        {
            TreeNode node = new TreeNode(bookMark.Text);
            node.ForeColor = bookMark.Color;
            node.Tag = bookMark;

            if (parent == null)
                this.treeBookmarks.Nodes.Add(node);
            else
                parent.Nodes.Add(node);

            if (bookMark.Children != null)
            {
                foreach (PdfBookmark bm in bookMark.Children)
                {
                    AddBookMark(node, bm);
                }
            }
        }

        #endregion
    }
}
