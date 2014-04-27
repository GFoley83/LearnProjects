using System;
using System.Text;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using skmDataStructures.Graph;

namespace GraphTester
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnReload;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label lblStartCity;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox startCity;
		private System.Windows.Forms.ComboBox endCity;
		private System.Windows.Forms.Button btnShortestPath;
		private System.Windows.Forms.Button btnViewData;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		// create a Graph object
		private Graph SoCalMap = new Graph();

		// create a Hashtable for the shortest distance and paths
		private Hashtable dist = new Hashtable();
		private Hashtable route = new Hashtable();

		const string GRAPH_FILE_NAME = "data.txt";		// the filename that contains the graph data


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
			this.btnReload = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.lblStartCity = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.startCity = new System.Windows.Forms.ComboBox();
			this.endCity = new System.Windows.Forms.ComboBox();
			this.btnShortestPath = new System.Windows.Forms.Button();
			this.btnViewData = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(344, 112);
			this.label1.TabIndex = 0;
			this.label1.Text = "The graph data resides in the text file data.txt.  On each line is a city name fo" +
				"llowed by a space followed by a city name followed by a space followed by the di" +
				"stance.  If you make changes to this file, click the Reload button to reload the" +
				" data...";
			// 
			// btnReload
			// 
			this.btnReload.Location = new System.Drawing.Point(200, 128);
			this.btnReload.Name = "btnReload";
			this.btnReload.Size = new System.Drawing.Size(136, 23);
			this.btnReload.TabIndex = 1;
			this.btnReload.Text = "Reload Graph Data";
			this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btnShortestPath);
			this.groupBox1.Controls.Add(this.endCity);
			this.groupBox1.Controls.Add(this.startCity);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.lblStartCity);
			this.groupBox1.Location = new System.Drawing.Point(32, 168);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(296, 136);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Compute Shortest Distance";
			// 
			// lblStartCity
			// 
			this.lblStartCity.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblStartCity.Location = new System.Drawing.Point(8, 24);
			this.lblStartCity.Name = "lblStartCity";
			this.lblStartCity.Size = new System.Drawing.Size(88, 16);
			this.lblStartCity.TabIndex = 0;
			this.lblStartCity.Text = "Starting City:";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(8, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(88, 16);
			this.label2.TabIndex = 1;
			this.label2.Text = "Ending City:";
			// 
			// startCity
			// 
			this.startCity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.startCity.Location = new System.Drawing.Point(96, 24);
			this.startCity.MaxDropDownItems = 10;
			this.startCity.Name = "startCity";
			this.startCity.Size = new System.Drawing.Size(152, 21);
			this.startCity.TabIndex = 2;
			// 
			// endCity
			// 
			this.endCity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.endCity.Location = new System.Drawing.Point(96, 48);
			this.endCity.Name = "endCity";
			this.endCity.Size = new System.Drawing.Size(152, 21);
			this.endCity.TabIndex = 3;
			// 
			// btnShortestPath
			// 
			this.btnShortestPath.Location = new System.Drawing.Point(72, 88);
			this.btnShortestPath.Name = "btnShortestPath";
			this.btnShortestPath.Size = new System.Drawing.Size(192, 32);
			this.btnShortestPath.TabIndex = 4;
			this.btnShortestPath.Text = "Compute Shortest Route from Starting to Ending City";
			this.btnShortestPath.Click += new System.EventHandler(this.btnShortestPath_Click);
			// 
			// btnViewData
			// 
			this.btnViewData.Location = new System.Drawing.Point(24, 128);
			this.btnViewData.Name = "btnViewData";
			this.btnViewData.Size = new System.Drawing.Size(152, 23);
			this.btnViewData.TabIndex = 3;
			this.btnViewData.Text = "View/Edit Graph Data";
			this.btnViewData.Click += new System.EventHandler(this.btnViewData_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(360, 325);
			this.Controls.Add(this.btnViewData);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnReload);
			this.Controls.Add(this.label1);
			this.Name = "Form1";
			this.Text = "Graph Tester";
			this.Load += new System.EventHandler(this.Form1_Load);
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

		private void Form1_Load(object sender, System.EventArgs e)
		{
			ReloadGraphData();		// load the graph data from the file.
		}

		/// <summary>
		/// Reloads the graph data from the file GRAPH_FILE_NAME.
		/// </summary>
		/// <remarks>The data in the graph file must be in the following format:<p />
		/// CityName1 CityName2 Distance
		/// <p />
		/// For cities with spaces in them, use quotation marks, like:<p />
		/// "San Diego" Riverside 100
		/// </remarks>
		private void ReloadGraphData()
		{
			SoCalMap.Clear();
			dist.Clear();
			route.Clear();

			try
			{
				// populate the graph with the data from the file
				StreamReader sr = File.OpenText(GRAPH_FILE_NAME);

				// iterate through each line
				string line = sr.ReadLine();
				while (line != null)
				{
					// get the city names and distance					
					line = Regex.Replace(line, "\"(.*?) (.*?)\"", "$1_$2");
					
					string city1 = Regex.Replace(line, "^(.*?) (.*?) (\\d+)$", "$1").Replace('_', ' ');
					string city2 = Regex.Replace(line, "^(.*?) (.*?) (\\d+)$", "$2").Replace('_', ' ');
					int distance = Convert.ToInt32(Regex.Replace(line, "^(.*?) (.*?) (\\d+)$", "$3"));

					// add the nodes to the graph, if needed
					if (!SoCalMap.Contains(city1))
						SoCalMap.AddNode(new Node(city1, null));
					if (!SoCalMap.Contains(city2))
						SoCalMap.AddNode(new Node(city2, null));
					SoCalMap.AddUndirectedEdge(city1, city2, distance);

					line = sr.ReadLine();
				}

				sr.Close();
			}
			catch (Exception)
			{
				MessageBox.Show("There was some unexpected error in reading the file....");
			}


			// reload the combo boxes
			this.startCity.Items.Clear();
			this.endCity.Items.Clear();

			foreach(Node n in SoCalMap.Nodes)
			{
				startCity.Items.Add(n.Key);
				endCity.Items.Add(n.Key);
			}

			if (startCity.Items.Count > 0)
				startCity.SelectedIndex = 0;
			if (endCity.Items.Count > 1)
				endCity.SelectedIndex = 1;
			else if (endCity.Items.Count > 0)
				endCity.SelectedIndex = 0;
		}


		private void btnViewData_Click(object sender, System.EventArgs e)
		{
			// Run Notepad, loading the specified file.
			Process.Start("notepad.exe", GRAPH_FILE_NAME);
		}

		private void btnReload_Click(object sender, System.EventArgs e)
		{
			// Reload the graph data
			ReloadGraphData();
		}


		/// <summary>
		/// Initializes the distance and route tables used for Dijkstra's Algorithm.
		/// </summary>
		/// <param name="start">The <b>Key</b> to the source Node.</param>
		private void InitDistRouteTables(string start)
		{
			// set the initial distance and route for each city in the graph
			foreach(Node n in SoCalMap.Nodes)
			{
				dist.Add(n.Key, Int32.MaxValue);
				route.Add(n.Key, null);
			}

			// set the initial distance of start to 0
			dist[start] = 0;
		}

		/// <summary>
		/// Relaxes the edge from the Node uCity to vCity.
		/// </summary>
		/// <param name="cost">The distance between uCity and vCity.</param>
		private void Relax(Node uCity, Node vCity, int cost)
		{
			int distTouCity = (int) dist[uCity.Key];
			int distTovCity = (int) dist[vCity.Key];			

			if (distTovCity > distTouCity + cost)
			{
				// update distance and route
				dist[vCity.Key] = distTouCity + cost;
				route[vCity.Key] = uCity;
			}
		}

		/// <summary>
		/// Retrieves the Node from the passed-in NodeList that has the <i>smallest</i> value in the distance table.
		/// </summary>
		/// <remarks>This method of grabbing the smallest Node gives Dijkstra's Algorithm a running time of
		/// O(<i>n</i><sup>2</sup>), where <i>n</i> is the number of nodes in the graph.  A better approach is to
		/// use a <i>priority queue</i> data structure to store the nodes, rather than an array.  Using a priority queue
		/// will improve Dijkstra's running time to O(E lg <i>n</i>), where E is the number of edges.  This approach is
		/// preferred for sparse graphs.  For more information on this, consult the README included in the download.</remarks>
		private Node GetMin(NodeList nodes)
		{
			// find the node in nodes with the smallest distance value
			int minDist = Int32.MaxValue;
			Node minNode = null;
			foreach(Node n in nodes)
			{
				if (((int) dist[n.Key]) <= minDist)
				{
					minDist = (int) dist[n.Key];
					minNode = n;
				}
			}

			return minNode;
		}

		/// <summary>
		/// Runs Dijkstra's Algorithm to find the shortest path.
		/// </summary>
		private void btnShortestPath_Click(object sender, System.EventArgs e)
		{
			string start = this.startCity.SelectedItem.ToString();
			string end = this.endCity.SelectedItem.ToString();

            InitDistRouteTables(start);		// initialize the route & distance tables
		
			NodeList nodes = SoCalMap.Nodes;	// nodes == Q
			
			/**** START DIJKSTRA ****/
			while (nodes.Count > 0)
			{
				Node u = GetMin(nodes);		// get the minimum node
				nodes.Remove(u);			// remove it from the set Q

				// iterate through all of u's neighbors
				foreach(EdgeToNeighbor edge in u.Neighbors)
					Relax(u, edge.Neighbor, edge.Cost);		// relax each edge
			}	// repeat until Q is empty
			/**** END DIJKSTRA ****/

			// Display results
			string results = "The shortest path from " + start + " to " + end + " is " + dist[end].ToString() + " miles and goes as follows: ";
			
			Stack traceBackSteps = new Stack();
						
			Node current = new Node(end, null);
			Node prev = null;
			do
			{
				prev = current;
				current = (Node) route[current.Key];

				string temp = current.Key + " to " + prev.Key + "\r\n";
				traceBackSteps.Push(temp);
			} while (current.Key != start);

			StringBuilder sb = new StringBuilder(30 * traceBackSteps.Count);
			while (traceBackSteps.Count > 0)
				sb.Append((string) traceBackSteps.Pop());

			MessageBox.Show(results + "\r\n\r\n" + sb.ToString());

		}
	}
}
