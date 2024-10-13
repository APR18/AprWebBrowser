using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AprWebBrowser
{
    public partial class HomeDialog : Form
    {
        public HomeDialog(string homeUrl)
        {
            InitializeComponent();
            homeUrlTextBox.Text = homeUrl;
        }

        public string getHomeUrl
        {
            get { return homeUrlTextBox.Text; } 
        }
        private void okayButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(homeUrlTextBox.Text))
            {
                MessageBox.Show("Please enter a valid Url");
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
