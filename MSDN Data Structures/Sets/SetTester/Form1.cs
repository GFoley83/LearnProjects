using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using skmDataStructures.Set;

namespace SetTester
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown lowerBound;
		private System.Windows.Forms.NumericUpDown upperBound;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnCreateSet;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.GroupBox setOperationsGroupBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox setContents;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnAddInteger;
		private System.Windows.Forms.Button btnComlement;
		private System.Windows.Forms.Button btnIntersect;
		private System.Windows.Forms.TextBox ints;
		private System.Windows.Forms.Button btnSetDifference;
		private System.Windows.Forms.Button btnIsSubset;
		private System.Windows.Forms.Button btnIsProperSubset;
		private System.Windows.Forms.Button btnCountVowels;
		private System.Windows.Forms.OpenFileDialog openFileDlg;

		private PascalSet pSet = null;

		public Form1()
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
			this.label1 = new System.Windows.Forms.Label();
			this.lowerBound = new System.Windows.Forms.NumericUpDown();
			this.upperBound = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.btnCreateSet = new System.Windows.Forms.Button();
			this.setOperationsGroupBox = new System.Windows.Forms.GroupBox();
			this.label3 = new System.Windows.Forms.Label();
			this.setContents = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.ints = new System.Windows.Forms.TextBox();
			this.btnAddInteger = new System.Windows.Forms.Button();
			this.btnComlement = new System.Windows.Forms.Button();
			this.btnIntersect = new System.Windows.Forms.Button();
			this.btnSetDifference = new System.Windows.Forms.Button();
			this.btnIsSubset = new System.Windows.Forms.Button();
			this.btnIsProperSubset = new System.Windows.Forms.Button();
			this.btnCountVowels = new System.Windows.Forms.Button();
			this.openFileDlg = new System.Windows.Forms.OpenFileDialog();
			((System.ComponentModel.ISupportInitialize)(this.lowerBound)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.upperBound)).BeginInit();
			this.setOperationsGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(160, 24);
			this.label1.TabIndex = 0;
			this.label1.Text = "Specify the set\'s bounds:";
			// 
			// lowerBound
			// 
			this.lowerBound.Location = new System.Drawing.Point(176, 8);
			this.lowerBound.Minimum = new System.Decimal(new int[] {
																	   100,
																	   0,
																	   0,
																	   -2147483648});
			this.lowerBound.Name = "lowerBound";
			this.lowerBound.Size = new System.Drawing.Size(48, 20);
			this.lowerBound.TabIndex = 1;
			// 
			// upperBound
			// 
			this.upperBound.Location = new System.Drawing.Point(264, 8);
			this.upperBound.Minimum = new System.Decimal(new int[] {
																	   100,
																	   0,
																	   0,
																	   -2147483648});
			this.upperBound.Name = "upperBound";
			this.upperBound.Size = new System.Drawing.Size(48, 20);
			this.upperBound.TabIndex = 2;
			this.upperBound.Value = new System.Decimal(new int[] {
																	 10,
																	 0,
																	 0,
																	 0});
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(232, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(16, 16);
			this.label2.TabIndex = 3;
			this.label2.Text = "to";
			// 
			// btnCreateSet
			// 
			this.btnCreateSet.Location = new System.Drawing.Point(336, 8);
			this.btnCreateSet.Name = "btnCreateSet";
			this.btnCreateSet.Size = new System.Drawing.Size(104, 24);
			this.btnCreateSet.TabIndex = 4;
			this.btnCreateSet.Text = "Create Set";
			this.btnCreateSet.Click += new System.EventHandler(this.btnCreateSet_Click);
			// 
			// setOperationsGroupBox
			// 
			this.setOperationsGroupBox.Controls.Add(this.btnIsProperSubset);
			this.setOperationsGroupBox.Controls.Add(this.btnIsSubset);
			this.setOperationsGroupBox.Controls.Add(this.btnSetDifference);
			this.setOperationsGroupBox.Controls.Add(this.btnIntersect);
			this.setOperationsGroupBox.Controls.Add(this.btnComlement);
			this.setOperationsGroupBox.Controls.Add(this.btnAddInteger);
			this.setOperationsGroupBox.Controls.Add(this.ints);
			this.setOperationsGroupBox.Controls.Add(this.label4);
			this.setOperationsGroupBox.Controls.Add(this.setContents);
			this.setOperationsGroupBox.Controls.Add(this.label3);
			this.setOperationsGroupBox.Enabled = false;
			this.setOperationsGroupBox.Location = new System.Drawing.Point(8, 48);
			this.setOperationsGroupBox.Name = "setOperationsGroupBox";
			this.setOperationsGroupBox.Size = new System.Drawing.Size(424, 320);
			this.setOperationsGroupBox.TabIndex = 5;
			this.setOperationsGroupBox.TabStop = false;
			this.setOperationsGroupBox.Text = "Set Operations";
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.Location = new System.Drawing.Point(8, 192);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(104, 16);
			this.label3.TabIndex = 0;
			this.label3.Text = "Set Contents:";
			// 
			// setContents
			// 
			this.setContents.Enabled = false;
			this.setContents.Location = new System.Drawing.Point(8, 208);
			this.setContents.Multiline = true;
			this.setContents.Name = "setContents";
			this.setContents.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.setContents.Size = new System.Drawing.Size(408, 96);
			this.setContents.TabIndex = 1;
			this.setContents.Text = "";
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label4.Location = new System.Drawing.Point(16, 24);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(104, 64);
			this.label4.TabIndex = 2;
			this.label4.Text = "Comma-Delimited List of Integers:";
			// 
			// ints
			// 
			this.ints.Location = new System.Drawing.Point(128, 24);
			this.ints.Multiline = true;
			this.ints.Name = "ints";
			this.ints.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.ints.Size = new System.Drawing.Size(288, 56);
			this.ints.TabIndex = 3;
			this.ints.Text = "";
			// 
			// btnAddInteger
			// 
			this.btnAddInteger.Location = new System.Drawing.Point(32, 96);
			this.btnAddInteger.Name = "btnAddInteger";
			this.btnAddInteger.Size = new System.Drawing.Size(72, 40);
			this.btnAddInteger.TabIndex = 4;
			this.btnAddInteger.Text = "Union Integers";
			this.btnAddInteger.Click += new System.EventHandler(this.btnAddInteger_Click);
			// 
			// btnComlement
			// 
			this.btnComlement.Location = new System.Drawing.Point(336, 96);
			this.btnComlement.Name = "btnComlement";
			this.btnComlement.Size = new System.Drawing.Size(80, 56);
			this.btnComlement.TabIndex = 5;
			this.btnComlement.Text = "Complement the Set";
			this.btnComlement.Click += new System.EventHandler(this.btnComlement_Click);
			// 
			// btnIntersect
			// 
			this.btnIntersect.Location = new System.Drawing.Point(120, 96);
			this.btnIntersect.Name = "btnIntersect";
			this.btnIntersect.Size = new System.Drawing.Size(72, 40);
			this.btnIntersect.TabIndex = 8;
			this.btnIntersect.Text = "Intersect Integers";
			this.btnIntersect.Click += new System.EventHandler(this.btnIntersect_Click);
			// 
			// btnSetDifference
			// 
			this.btnSetDifference.Location = new System.Drawing.Point(216, 96);
			this.btnSetDifference.Name = "btnSetDifference";
			this.btnSetDifference.Size = new System.Drawing.Size(75, 40);
			this.btnSetDifference.TabIndex = 9;
			this.btnSetDifference.Text = "Set Difference";
			this.btnSetDifference.Click += new System.EventHandler(this.btnSetDifference_Click);
			// 
			// btnIsSubset
			// 
			this.btnIsSubset.Location = new System.Drawing.Point(112, 160);
			this.btnIsSubset.Name = "btnIsSubset";
			this.btnIsSubset.TabIndex = 10;
			this.btnIsSubset.Text = "Is Subset?";
			this.btnIsSubset.Click += new System.EventHandler(this.btnIsSubset_Click);
			// 
			// btnIsProperSubset
			// 
			this.btnIsProperSubset.Location = new System.Drawing.Point(232, 152);
			this.btnIsProperSubset.Name = "btnIsProperSubset";
			this.btnIsProperSubset.Size = new System.Drawing.Size(75, 32);
			this.btnIsProperSubset.TabIndex = 11;
			this.btnIsProperSubset.Text = "Is Proper Subset?";
			this.btnIsProperSubset.Click += new System.EventHandler(this.btnIsProperSubset_Click);
			// 
			// btnCountVowels
			// 
			this.btnCountVowels.Location = new System.Drawing.Point(128, 376);
			this.btnCountVowels.Name = "btnCountVowels";
			this.btnCountVowels.Size = new System.Drawing.Size(176, 23);
			this.btnCountVowels.TabIndex = 6;
			this.btnCountVowels.Text = "Count Character Classes";
			this.btnCountVowels.Click += new System.EventHandler(this.btnCountVowels_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(448, 413);
			this.Controls.Add(this.btnCountVowels);
			this.Controls.Add(this.setOperationsGroupBox);
			this.Controls.Add(this.btnCreateSet);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.upperBound);
			this.Controls.Add(this.lowerBound);
			this.Controls.Add(this.label1);
			this.Name = "Form1";
			this.Text = "Set Tester";
			((System.ComponentModel.ISupportInitialize)(this.lowerBound)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.upperBound)).EndInit();
			this.setOperationsGroupBox.ResumeLayout(false);
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

		private void btnCreateSet_Click(object sender, System.EventArgs e)
		{
			try
			{
				pSet = new PascalSet(Convert.ToInt32(this.lowerBound.Value), Convert.ToInt32(this.upperBound.Value));
				MessageBox.Show("Set created with lower bound " + Convert.ToInt32(this.lowerBound.Value).ToString() + " to " + Convert.ToInt32(this.upperBound.Value).ToString());
				this.setOperationsGroupBox.Enabled = true;
				UpdateDisplay();
			}
			catch (ArgumentException ae)
			{
				MessageBox.Show("There was an error creating the set:\r\n" + ae.Message, "Error Creating Set", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}		
		}

		private int[] GetIntArrayFromString(string str)
		{
			string [] addStrs = str.Split(new char[] {','});
			int [] add = new int[addStrs.Length];
			for (int i = 0; i < addStrs.Length; i++)
				add[i] = Convert.ToInt32(addStrs[i].Trim());

			return add;
		}

		private void btnAddInteger_Click(object sender, System.EventArgs e)
		{
			try
			{
				int [] nums = GetIntArrayFromString(ints.Text);

				pSet = pSet.Union(nums);
				UpdateDisplay();
				ints.Text = string.Empty;
			}
			catch (Exception ex)
			{
				MessageBox.Show("There was a problem with unioning the integer list to the set:\r\n" + ex.Message);
			}
		}

		private void UpdateDisplay()
		{
			this.setContents.Text = "{ ";
			IEnumerator cursor = pSet.GetEnumerator();
			while (cursor.MoveNext())
				this.setContents.Text += cursor.Current.ToString() + ", ";

			this.setContents.Text += "}";
		}

		private void btnComlement_Click(object sender, System.EventArgs e)
		{
			pSet = pSet.Complement();
			UpdateDisplay();
		}

		private void btnIntersect_Click(object sender, System.EventArgs e)
		{
			try
			{
				int [] nums = GetIntArrayFromString(ints.Text);

				pSet = pSet.Intersection(nums);
				UpdateDisplay();
				ints.Text = String.Empty;
			}
			catch (Exception ex)
			{
				MessageBox.Show("There was a problem with intersecting the integer list with the set:\r\n" + ex.Message);
			}
		}

		private void btnSetDifference_Click(object sender, System.EventArgs e)
		{
			try
			{
				int [] nums = GetIntArrayFromString(ints.Text);

				pSet = pSet.Difference(nums);
				UpdateDisplay();
				ints.Text = String.Empty;
			}
			catch (Exception ex)
			{
				MessageBox.Show("There was a problem with set differencing the integer list with the set:\r\n" + ex.Message);
			}	
		}

		private void btnIsSubset_Click(object sender, System.EventArgs e)
		{
			try
			{
				int [] nums = GetIntArrayFromString(ints.Text);

				if (pSet.Subset(nums))
					MessageBox.Show(this.setContents.Text + " is a subset of {" + ints.Text + "}");
				else
					MessageBox.Show(this.setContents.Text + " is NOT a subset of {" + ints.Text + "}");
				
				ints.Text = String.Empty;
			}
			catch (Exception ex)
			{
				MessageBox.Show("There was a problem with checking the subset for the integer list with the set:\r\n" + ex.Message);
			}	
		}

		private void btnIsProperSubset_Click(object sender, System.EventArgs e)
		{
			try
			{
				int [] nums = GetIntArrayFromString(ints.Text);

				if (pSet.ProperSubset(nums))
					MessageBox.Show(this.setContents.Text + " is a proper subset of {" + ints.Text + "}");
				else
					MessageBox.Show(this.setContents.Text + " is NOT a proper subset of {" + ints.Text + "}");
					
				ints.Text = String.Empty;
			}
			catch (Exception ex)
			{
				MessageBox.Show("There was a problem with checking the proper subset for the integer list with the set:\r\n" + ex.Message);
			}	
	
		}

		private void btnCountVowels_Click(object sender, System.EventArgs e)
		{
			MessageBox.Show("Choose a text file to open.  Using a character set, the program will provide some stats about the file.");
			if (this.openFileDlg.ShowDialog() == DialogResult.OK)
			{
				// read in the file's contents
				StreamReader sr = File.OpenText(openFileDlg.FileName);
                string fileContents = sr.ReadToEnd().ToLower();
				sr.Close();

				PascalSet vowels = new PascalSet('a', 'z');
				vowels = vowels.Union('a','e','i','o','u');
				PascalSet consonants = vowels.Complement();

				int vCount = 0, cCount = 0, oCount = 0;
				for (int i = 0; i < fileContents.Length; i++)
				{
					char c = fileContents[i];
					if (vowels.ContainsElement(c))
						vCount++;
					else if (consonants.ContainsElement(c))
						cCount++;
					else
						oCount++;
				}

				MessageBox.Show("In the file there were " + vCount.ToString() + " vowels, " + cCount.ToString() + " consonants, and " + oCount.ToString() + " non-alphabetic characters.");
			}
		}
	}
}
