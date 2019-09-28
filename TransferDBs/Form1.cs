using MongoDB.Driver;
using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TransferDBs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            string db = txtMongoCollection.Text;  
            string source = txtTableSource.Text;
            string destination = txtTableDestination.Text;

            if(isValidFields())
            {
                Transfer transfer = new Transfer(db, source, destination);
                var result = transfer.TransferRecordsToMongoDB();

                if (result)
                {
                    MessageBox.Show("Collection created!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Something wrong happened", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            

        }

        private bool isValidFields()
        {
            var result = true;
            if(string.IsNullOrEmpty(txtMongoCollection.Text))
            {
                MessageBox.Show("Please, fill the collection name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                result = false;
            }
            else if(string.IsNullOrEmpty(txtTableSource.Text))
            {
                MessageBox.Show("Please, fill the Table source", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                result = false;
            }
            else if(string.IsNullOrEmpty(txtTableDestination.Text))
            {
                MessageBox.Show("Please, fill the Table destination", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                result = false;
            }

            return result;
        }
    }
}
