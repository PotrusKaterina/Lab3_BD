using Lab3_BD.Presenter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3_BD
{
    public partial class Form1 : Form
    {
        IPresenter presenter;
        public Form1()
        {
            InitializeComponent();
            presenter = new Presenter.Presenter();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            var items = presenter.GetFullTree();
            WriteToListBox(items);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            if (int.TryParse(textBox1.Text, out int node))
            {
                var items = presenter.GetParent(node);
                WriteToListBox(items);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            if (int.TryParse(textBox2.Text, out int node))
            {
                var items = presenter.GetDaughter(node);
                WriteToListBox(items);
            }
        }

        private void WriteToListBox(IEnumerable<String> items)
        {
            foreach (String item in items)
            {
                listBox1.Items.Add(item);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            if (int.TryParse(textBox3.Text, out int node_1))
            {
                if (int.TryParse(textBox4.Text, out int node_2))
                {
                    var items = presenter.GetBranchTwoElements(node_1, node_2);
                    WriteToListBox(items);
                }
            }
        }
    }
}
