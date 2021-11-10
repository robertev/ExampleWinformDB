using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExampleWinformDB
{
    public partial class Form1 : Form
    {


        public Form1()
        {

            InitializeComponent();
            var c = this.tableAdapterManager.Connection.ConnectionString;
        }



        private void emplyoeeBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.emplyoeeBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.exampleDataSet);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'exampleDataSet.Emplyoee' table. You can move, or remove it, as needed.
           this.emplyoeeTableAdapter.Fill(this.exampleDataSet.Emplyoee);

        }
    }
}
