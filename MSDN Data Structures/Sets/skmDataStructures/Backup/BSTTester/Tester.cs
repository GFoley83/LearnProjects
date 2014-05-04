using skmDataStructures;
using skmDataStructures.BST;
using skmDataStructures.BinaryTree;

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace BSTTester
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton preorder;
		private System.Windows.Forms.RadioButton inorder;
		private System.Windows.Forms.RadioButton postorder;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox value;
		private System.Windows.Forms.Button btnInsert;
		private System.Windows.Forms.Button btnSearch;
		private System.Windows.Forms.Button btnDelete;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private BST bst = new BST();
		private System.Windows.Forms.TextBox traversalOutput;
		private System.Windows.Forms.Button btnClear;
		private TraversalMethods traversal = TraversalMethods.Inorder;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			/*
			// testing of BinaryTree
			BinaryTree btree = new BinaryTree();
			btree.Root = new skmDataStructures.BinaryTree.Node(1);
			btree.Root.Left = new skmDataStructures.BinaryTree.Node(2);
			btree.Root.Right = new skmDataStructures.BinaryTree.Node(3);

			btree.Root.Left.Left = new skmDataStructures.BinaryTree.Node(4);
			btree.Root.Right.Right = new skmDataStructures.BinaryTree.Node(5);

			btree.Root.Left.Left.Right = new skmDataStructures.BinaryTree.Node(6);
			btree.Root.Right.Right.Right = new skmDataStructures.BinaryTree.Node(7);

			btree.Root.Right.Right.Right.Right = new skmDataStructures.BinaryTree.Node(8);
			*/

			RefreshTraversalDisplay();
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
			this.traversalOutput = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.postorder = new System.Windows.Forms.RadioButton();
			this.inorder = new System.Windows.Forms.RadioButton();
			this.preorder = new System.Windows.Forms.RadioButton();
			this.label1 = new System.Windows.Forms.Label();
			this.value = new System.Windows.Forms.TextBox();
			this.btnInsert = new System.Windows.Forms.Button();
			this.btnSearch = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnClear = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// traversalOutput
			// 
			this.traversalOutput.Location = new System.Drawing.Point(8, 168);
			this.traversalOutput.Multiline = true;
			this.traversalOutput.Name = "traversalOutput";
			this.traversalOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.traversalOutput.Size = new System.Drawing.Size(368, 192);
			this.traversalOutput.TabIndex = 0;
			this.traversalOutput.Text = "";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.postorder);
			this.groupBox1.Controls.Add(this.inorder);
			this.groupBox1.Controls.Add(this.preorder);
			this.groupBox1.Location = new System.Drawing.Point(32, 104);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(320, 48);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Traversal Method";
			// 
			// postorder
			// 
			this.postorder.Location = new System.Drawing.Point(232, 16);
			this.postorder.Name = "postorder";
			this.postorder.TabIndex = 2;
			this.postorder.Text = "Postorder";
			this.postorder.CheckedChanged += new System.EventHandler(this.postorder_CheckedChanged);
			// 
			// inorder
			// 
			this.inorder.Checked = true;
			this.inorder.Location = new System.Drawing.Point(128, 16);
			this.inorder.Name = "inorder";
			this.inorder.TabIndex = 1;
			this.inorder.TabStop = true;
			this.inorder.Text = "Inorder";
			this.inorder.CheckedChanged += new System.EventHandler(this.inorder_CheckedChanged);
			// 
			// preorder
			// 
			this.preorder.Location = new System.Drawing.Point(16, 16);
			this.preorder.Name = "preorder";
			this.preorder.TabIndex = 0;
			this.preorder.Text = "Preorder";
			this.preorder.CheckedChanged += new System.EventHandler(this.preorder_CheckedChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(88, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96, 16);
			this.label1.TabIndex = 2;
			this.label1.Text = "String value:";
			// 
			// value
			// 
			this.value.Location = new System.Drawing.Point(184, 8);
			this.value.Name = "value";
			this.value.Size = new System.Drawing.Size(120, 20);
			this.value.TabIndex = 3;
			this.value.Text = "";
			// 
			// btnInsert
			// 
			this.btnInsert.Location = new System.Drawing.Point(16, 48);
			this.btnInsert.Name = "btnInsert";
			this.btnInsert.Size = new System.Drawing.Size(64, 32);
			this.btnInsert.TabIndex = 4;
			this.btnInsert.Text = "Insert";
			this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
			// 
			// btnSearch
			// 
			this.btnSearch.Location = new System.Drawing.Point(104, 48);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(72, 32);
			this.btnSearch.TabIndex = 5;
			this.btnSearch.Text = "Search";
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			// 
			// btnDelete
			// 
			this.btnDelete.Location = new System.Drawing.Point(200, 48);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(72, 32);
			this.btnDelete.TabIndex = 6;
			this.btnDelete.Text = "Delete";
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnClear
			// 
			this.btnClear.Location = new System.Drawing.Point(296, 48);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(72, 32);
			this.btnClear.TabIndex = 7;
			this.btnClear.Text = "Clear";
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(384, 365);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.btnSearch);
			this.Controls.Add(this.btnInsert);
			this.Controls.Add(this.value);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.traversalOutput);
			this.Name = "Form1";
			this.Text = "BST Tester";
			this.groupBox1.ResumeLayout(false);
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

		private void ShowMustProvideDataMessage()
		{
			MessageBox.Show("You must first provide a value in the String value textbox.", "Please Provide a Value", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private void RefreshTraversalDisplay()
		{
			if (preorder.Checked)
				traversalOutput.Text = bst.ToString(TraversalMethods.Preorder, ", ");
			else if (inorder.Checked)
				traversalOutput.Text = bst.ToString(TraversalMethods.Inorder, ", ");
			else if (postorder.Checked)
				traversalOutput.Text = bst.ToString(TraversalMethods.Postorder, ", ");

			traversalOutput.Text += "\r\n\r\nCount = " + bst.Count;
		}

		private void btnInsert_Click(object sender, System.EventArgs e)
		{
			if (value.Text.Length == 0)
				ShowMustProvideDataMessage();
			else
			{
				bst.Add(value.Text);
				value.Text = String.Empty;

				// refresh the traversal display
				RefreshTraversalDisplay();
			}
		}

		private void preorder_CheckedChanged(object sender, System.EventArgs e)
		{
			RefreshTraversalDisplay();
		}

		private void inorder_CheckedChanged(object sender, System.EventArgs e)
		{
			RefreshTraversalDisplay();
		}

		private void postorder_CheckedChanged(object sender, System.EventArgs e)
		{
			RefreshTraversalDisplay();
		}

		private void btnClear_Click(object sender, System.EventArgs e)
		{
			bst.Clear();
			RefreshTraversalDisplay();
		}

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			if (value.Text.Length == 0)
				ShowMustProvideDataMessage();
			else
			{
				string message = String.Empty;
				if (bst.Contains(value.Text))
					message = "String " + value.Text + " was found!";
				else
					message = "String " + value.Text + " was **NOT** found!!";

				MessageBox.Show(message, "Search Results", MessageBoxButtons.OK, MessageBoxIcon.Information);

				value.Text = String.Empty;
			}
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			if (value.Text.Length == 0)
				ShowMustProvideDataMessage();
			else
			{
				if (!bst.Contains(value.Text))
				{
					MessageBox.Show("The BST does not contain " + value.Text + "!", "Deletion Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else
				{
					bst.Delete(value.Text);
					this.RefreshTraversalDisplay();
				}

				value.Text = String.Empty;
			}		
		}
	}
}
