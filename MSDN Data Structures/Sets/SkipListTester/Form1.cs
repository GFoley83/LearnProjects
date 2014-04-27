using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using skmDataStructures.SkipList;

namespace SkipListTester
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox value;
		private System.Windows.Forms.Button btnInsert;
		private System.Windows.Forms.TextBox list;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label slHeight;
		private System.Windows.Forms.Button btnContains;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown opsNum;
		private System.Windows.Forms.Button btnStressTest;
		private System.Windows.Forms.SaveFileDialog saveLogFile;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox rndSeed;

		private SkipList sl = new SkipList();

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
			this.value = new System.Windows.Forms.TextBox();
			this.btnInsert = new System.Windows.Forms.Button();
			this.list = new System.Windows.Forms.TextBox();
			this.btnDelete = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.slHeight = new System.Windows.Forms.Label();
			this.btnContains = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label3 = new System.Windows.Forms.Label();
			this.opsNum = new System.Windows.Forms.NumericUpDown();
			this.btnStressTest = new System.Windows.Forms.Button();
			this.saveLogFile = new System.Windows.Forms.SaveFileDialog();
			this.label4 = new System.Windows.Forms.Label();
			this.rndSeed = new System.Windows.Forms.TextBox();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.opsNum)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(40, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Value:";
			// 
			// value
			// 
			this.value.Location = new System.Drawing.Point(64, 8);
			this.value.Name = "value";
			this.value.Size = new System.Drawing.Size(64, 20);
			this.value.TabIndex = 1;
			this.value.Text = "";
			// 
			// btnInsert
			// 
			this.btnInsert.Location = new System.Drawing.Point(160, 8);
			this.btnInsert.Name = "btnInsert";
			this.btnInsert.TabIndex = 2;
			this.btnInsert.Text = "Insert";
			this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
			// 
			// list
			// 
			this.list.Location = new System.Drawing.Point(16, 72);
			this.list.Multiline = true;
			this.list.Name = "list";
			this.list.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
			this.list.Size = new System.Drawing.Size(584, 48);
			this.list.TabIndex = 3;
			this.list.Text = "";
			this.list.WordWrap = false;
			// 
			// btnDelete
			// 
			this.btnDelete.Location = new System.Drawing.Point(248, 8);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.TabIndex = 4;
			this.btnDelete.Text = "Delete";
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 16);
			this.label2.TabIndex = 5;
			this.label2.Text = "SkipList Height: ";
			// 
			// slHeight
			// 
			this.slHeight.Location = new System.Drawing.Point(128, 48);
			this.slHeight.Name = "slHeight";
			this.slHeight.Size = new System.Drawing.Size(100, 16);
			this.slHeight.TabIndex = 6;
			// 
			// btnContains
			// 
			this.btnContains.Location = new System.Drawing.Point(336, 8);
			this.btnContains.Name = "btnContains";
			this.btnContains.TabIndex = 7;
			this.btnContains.Text = "Contains?";
			this.btnContains.Click += new System.EventHandler(this.btnContains_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.rndSeed);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.btnStressTest);
			this.groupBox1.Controls.Add(this.opsNum);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Location = new System.Drawing.Point(40, 144);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(424, 100);
			this.groupBox1.TabIndex = 8;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Stress Test SkipList";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 24);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(120, 16);
			this.label3.TabIndex = 0;
			this.label3.Text = "Number of Operations:";
			// 
			// opsNum
			// 
			this.opsNum.Increment = new System.Decimal(new int[] {
																	 10,
																	 0,
																	 0,
																	 0});
			this.opsNum.Location = new System.Drawing.Point(144, 24);
			this.opsNum.Maximum = new System.Decimal(new int[] {
																   255000,
																   0,
																   0,
																   0});
			this.opsNum.Minimum = new System.Decimal(new int[] {
																   10,
																   0,
																   0,
																   0});
			this.opsNum.Name = "opsNum";
			this.opsNum.Size = new System.Drawing.Size(96, 20);
			this.opsNum.TabIndex = 1;
			this.opsNum.Value = new System.Decimal(new int[] {
																 1024,
																 0,
																 0,
																 0});
			// 
			// btnStressTest
			// 
			this.btnStressTest.Location = new System.Drawing.Point(112, 56);
			this.btnStressTest.Name = "btnStressTest";
			this.btnStressTest.Size = new System.Drawing.Size(208, 24);
			this.btnStressTest.TabIndex = 2;
			this.btnStressTest.Text = "Commence Testing!";
			this.btnStressTest.Click += new System.EventHandler(this.btnStressTest_Click);
			// 
			// saveLogFile
			// 
			this.saveLogFile.Filter = "Log Files|*.log|All Files|*.*";
			this.saveLogFile.Title = "Log File";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(248, 24);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(80, 16);
			this.label4.TabIndex = 3;
			this.label4.Text = "Random Seed:";
			// 
			// rndSeed
			// 
			this.rndSeed.Location = new System.Drawing.Point(336, 24);
			this.rndSeed.Name = "rndSeed";
			this.rndSeed.Size = new System.Drawing.Size(64, 20);
			this.rndSeed.TabIndex = 4;
			this.rndSeed.Text = "";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(608, 253);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnContains);
			this.Controls.Add(this.slHeight);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.list);
			this.Controls.Add(this.btnInsert);
			this.Controls.Add(this.value);
			this.Controls.Add(this.label1);
			this.Name = "Form1";
			this.Text = "SkipList Tester";
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.opsNum)).EndInit();
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

		private void btnInsert_Click(object sender, System.EventArgs e)
		{
			sl.Add(value.Text);
			UpdateDisplay();
		}

		private void UpdateDisplay()
		{
			list.Text = String.Empty;
			foreach(Node n in sl)
			{
				list.Text += n.Value.ToString() + " (H=" + n.Height.ToString() + " | ";
				for (int i = n.Height - 1; i >= 0; i--)
					if (n[i] == null)
						list.Text += "NULL, ";
					else
						list.Text += n[i].Value.ToString() + ", ";

				list.Text += "); ";
			}

			slHeight.Text = sl.Height.ToString();
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			sl.Remove(value.Text);
			UpdateDisplay();
		}

		private void btnContains_Click(object sender, System.EventArgs e)
		{
			if (sl.Contains(value.Text))
				MessageBox.Show("The value " + value.Text + " is in the SkipList...");
			else
				MessageBox.Show("The value " + value.Text + " is NOT in the SkipList...");
		}

		private void btnStressTest_Click(object sender, System.EventArgs e)
		{
			// prompt the user to select a place to save the log file
			if (this.saveLogFile.ShowDialog() == DialogResult.OK)
			{
				// the user has opted to save the file, run tests!
				Random rnd;
				SkipList tempSL;
				int totalComps = 0;

				if (rndSeed.Text.Length > 0)
				{
					rnd = new Random(Convert.ToInt32(rndSeed.Text));
					tempSL = new SkipList(Convert.ToInt32(rndSeed.Text));
				}
				else
				{
					rnd = new Random();
					tempSL = new SkipList();
				}

				
				
				// start by running some inserts
				StreamWriter sw = File.CreateText(saveLogFile.FileName);
				for (int i = 0; i < this.opsNum.Value / 2; i++)
				{
					int adding = rnd.Next(Convert.ToInt32(opsNum.Value));
					sw.Write("Adding " + adding.ToString());
					
					tempSL.ResetComparisons();
					tempSL.Add(adding);
					totalComps += tempSL.Comparisons;
					sw.WriteLine(" (" + tempSL.Comparisons + " comps, " + tempSL.Count.ToString() + " items, height " + tempSL.Height.ToString() + ")");
				}

				// now, do a mix of inserts/deletes/lookups
				for (int i = 0; i < this.opsNum.Value / 2; i++)
				{
					int val = rnd.Next(Convert.ToInt32(opsNum.Value));

					switch(rnd.Next(3))
					{
						case 0:	// insert
							sw.Write("Adding " + val.ToString());
					
							tempSL.ResetComparisons();
							tempSL.Add(val);
							sw.WriteLine(" (" + tempSL.Comparisons + " comps, " + tempSL.Count.ToString() + " items, height " + tempSL.Height.ToString() + ")");
							totalComps += tempSL.Comparisons;
							break;

						case 1: // delete
							sw.Write("Removing " + val.ToString());
					
							tempSL.ResetComparisons();
							tempSL.Remove(val);
							sw.WriteLine(" (" + tempSL.Comparisons + " comps, " + tempSL.Count.ToString() + " items, height " + tempSL.Height.ToString() + ")");
							totalComps += tempSL.Comparisons;
							break;

						case 2: // lookup
							sw.Write("Contains " + val.ToString());
					
							tempSL.ResetComparisons();
							if (tempSL.Contains(val))
								sw.Write(" FOUND ");
							else
								sw.Write(" NOT FOUND ");
							
							sw.WriteLine(" (" + tempSL.Comparisons + " comps, " + tempSL.Count.ToString() + " items, height " + tempSL.Height.ToString() + ")");
							totalComps += tempSL.Comparisons;
							break;
					}
				}

				double avgComps = Convert.ToDouble(totalComps) / Convert.ToDouble(opsNum.Value);

				sw.WriteLine("");
				sw.WriteLine("TOTAL NODES IN SKIPLIST: " + tempSL.Count.ToString());
				sw.WriteLine("HEIGHT OF SKIPLIST: " + tempSL.Height.ToString());
				sw.WriteLine("TOTAL # OF COMPARISONS: " + totalComps.ToString());
				sw.WriteLine("AVERAGE # OF COMPARISONS PER OPERATION: " + avgComps.ToString());

				sw.Close();
				tempSL = null;

				MessageBox.Show("Test Complete... " + totalComps.ToString() + 
								" total comparisons with an average of " + avgComps.ToString() + 
								" comparisons per operation...");
			}
		}
	}
}
