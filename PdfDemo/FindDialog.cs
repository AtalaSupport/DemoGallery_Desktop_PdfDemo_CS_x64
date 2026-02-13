using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace PdfDemo
{
	/// <summary>
	/// Summary description for FindDialog.
	/// </summary>
	public class FindDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox txtFind;
		private System.Windows.Forms.Button btnNext;
		private System.Windows.Forms.CheckBox cbMatchCase;
		private System.Windows.Forms.CheckBox cbWholeWord;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FindDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
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
			this.txtFind = new System.Windows.Forms.TextBox();
			this.btnNext = new System.Windows.Forms.Button();
			this.cbMatchCase = new System.Windows.Forms.CheckBox();
			this.cbWholeWord = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// txtFind
			// 
			this.txtFind.Location = new System.Drawing.Point(8, 9);
			this.txtFind.Name = "txtFind";
			this.txtFind.Size = new System.Drawing.Size(168, 20);
			this.txtFind.TabIndex = 0;
			this.txtFind.Text = "";
			// 
			// btnNext
			// 
			this.btnNext.Location = new System.Drawing.Point(188, 7);
			this.btnNext.Name = "btnNext";
			this.btnNext.Size = new System.Drawing.Size(64, 24);
			this.btnNext.TabIndex = 1;
			this.btnNext.Text = "Next";
			this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
			// 
			// cbMatchCase
			// 
			this.cbMatchCase.Location = new System.Drawing.Point(8, 32);
			this.cbMatchCase.Name = "cbMatchCase";
			this.cbMatchCase.Size = new System.Drawing.Size(96, 24);
			this.cbMatchCase.TabIndex = 2;
			this.cbMatchCase.Text = "Match &Case";
			// 
			// cbWholeWord
			// 
			this.cbWholeWord.Location = new System.Drawing.Point(104, 32);
			this.cbWholeWord.Name = "cbWholeWord";
			this.cbWholeWord.TabIndex = 3;
			this.cbWholeWord.Text = "&Whole Word";
			// 
			// FindDialog
			// 
			this.AcceptButton = this.btnNext;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(264, 62);
			this.Controls.Add(this.cbWholeWord);
			this.Controls.Add(this.cbMatchCase);
			this.Controls.Add(this.btnNext);
			this.Controls.Add(this.txtFind);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(272, 72);
			this.Name = "FindDialog";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Find Text";
			this.TopMost = true;
			this.ResumeLayout(false);

		}
		#endregion


		public delegate void FindNextHandler(String text, bool matchCase, bool wholeWord);
		public event FindNextHandler OnFindNext;

		private void btnNext_Click(object sender, System.EventArgs e)
		{
			OnFindNext(txtFind.Text, cbMatchCase.Checked, cbWholeWord.Checked);
		}
	}
}
