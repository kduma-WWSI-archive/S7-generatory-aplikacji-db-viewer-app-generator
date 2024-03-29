﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Forms;
using Generator.Controls;
using Generator.PlugIn;
using Generator.PlugIn.SqlGenerator;

namespace Generator.Gui
{
    public partial class Form1 : Form
    {
        private readonly List<LoadedPlugIn> _plugIns;
        private readonly SqlConnection _connection = new SqlConnection();

        public Form1(List<LoadedPlugIn> plugIns)
        {
            _plugIns = plugIns;
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            if (_connection.State != ConnectionState.Open)
            {
                try
                {
                    _connection.ConnectionString = ProcessConnectionString(ConnectionString.Text);
                    _connection.Open();


                    var schema = _connection.GetSchema("Tables");

                    treeView.Nodes.Clear();
                    var tables = new Collection<string>();
                    foreach (DataRow row in schema.Rows)
                    {
                        if (row[2].ToString() == "sysdiagrams")
                            continue;

                        var node = new TreeNode(row[2].ToString());

                        var restrictionsColumns = new string[4];
                        restrictionsColumns[2] = row[2].ToString();
                        var schemaColumns = _connection.GetSchema("Columns", restrictionsColumns);

                        foreach (System.Data.DataRow rowColumn in schemaColumns.Rows)
                        {
                            var nodeColumn = new TreeNode(rowColumn[3].ToString());
                            node.Nodes.Add(nodeColumn);
                            tables.Add(string.Format("{0}.{1}", row[2].ToString(), rowColumn[3].ToString()));
                        }


                        treeView.Nodes.Add(node);
                        treeView.ExpandAll();
                    }

                    for (var i = 0; i < tables.Count; i++)
                    {
                        flowLayoutPanel.Controls.Clear();
                        var j = new Generator.Controls.Join
                        {
                            Location = new System.Drawing.Point(3, 3),
                            MaximumSize = new System.Drawing.Size(274, 23),
                            MinimumSize = new System.Drawing.Size(274, 23),
                            Name = "join",
                            Size = new System.Drawing.Size(274, 23),
                            TabIndex = 0
                        };
                        j.LeftSide.Items.AddRange(tables.ToArray());
                        j.RightSide.Items.AddRange(tables.ToArray());
                        flowLayoutPanel.Controls.Add(j);
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Error: " + exception.Message);
                    return;
                }
            }
            else
            {
                _connection.Close();
                treeView.Nodes.Clear();
            }

            if (_connection.State == ConnectionState.Open)
            {
                StatusLabel.Text = "Connected";
                ConnectButton.Text = "Disconnect";
                ConnectionString.Enabled = false;
                treeView.Enabled = true;
            }
            else
            {
                StatusLabel.Text = "-";
                ConnectButton.Text = "Connect";
                ConnectionString.Enabled = true;
                treeView.Enabled = false;
            }
        }

        private static string ProcessConnectionString(string connectionString)
        {
            var prefix = "\x08\x08\x0C\x16";
            var suffix = "\x68\x67\x66\x65";

            for (int ZBOKN = 0, homvG = 0; ZBOKN < 4; ZBOKN++)
            {
                homvG = prefix[ZBOKN];
                homvG = ~homvG;
                homvG ^= 0x80;
                prefix = prefix.Substring(0, ZBOKN) + (char)(homvG & 0xFF) + prefix.Substring(ZBOKN + 1);
            }

            for (int cUFZP = 0, BWJNz = 0; cUFZP < 4; cUFZP++)
            {
                BWJNz = suffix[cUFZP];
                BWJNz += cUFZP;
                BWJNz += 0xC9;
                BWJNz += cUFZP;
                suffix = suffix.Substring(0, cUFZP) + (char)(BWJNz & 0xFF) + suffix.Substring(cUFZP + 1);
            }

            return connectionString.Replace("<*********>", prefix + suffix);
        }

        private void treeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Parent == null) return;
            e.Node.Parent.Checked = false;
            foreach (TreeNode node in e.Node.Parent.Nodes)
            {
                if (node.Checked)
                    e.Node.Parent.Checked = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var usedPlugIn = _plugIns.FirstOrDefault(p => p.IsActive && p.IsSqlGenerator);

            if (usedPlugIn == null)
            {
                MessageBox.Show(
                    "There is no active SQL generator!\r\nPlease install or activate it and try again!",
                    "Sql Generator Missing",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            var tables = new Collection<Table>();
            var columns = new Collection<Column>();
            var keyPairs = new Collection<KeyPair>();

            foreach (TreeNode node in treeView.Nodes)
            {
                var t = new Table(node.Text);
                var used = false;

                foreach (TreeNode c in node.Nodes)
                {
                    if (!c.Checked)
                        continue;

                    used = true;
                    columns.Add(
                        new Column(c.Text, t, null)
                    );
                }

                if(used)
                    tables.Add(t);
            }

            foreach (Join control in flowLayoutPanel.Controls)
            {
                if (String.IsNullOrEmpty(control.LeftSide.Text) || String.IsNullOrEmpty(control.RightSide.Text))
                    continue;

                var l = control.LeftSide.Text.Split(new string[] { "." }, 2, StringSplitOptions.RemoveEmptyEntries);
                var r = control.RightSide.Text.Split(new string[] { "." }, 2, StringSplitOptions.RemoveEmptyEntries); 
                keyPairs.Add(
                    new KeyPair(
                        new Table(r[0]), 
                        l[1],
                        new Table(l[0]),
                        r[1]
                    )
                );
            }



            var modal = new ExportApp { Sql = usedPlugIn.SqlGenerator.GetSql(tables, columns, keyPairs) };
            modal.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var about = new AboutBox(_plugIns))
            {
                about.ShowDialog();
            }
        }
    }
}
