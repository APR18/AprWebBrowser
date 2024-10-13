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
    public partial class FavouriteDialog : Form
    {
        
        public FavouriteDialog(string url)
        {
            InitializeComponent();
            urlTextBox.ReadOnly = true;
            urlTextBox.Text = url;
        }

        public string getFavouriteName
        {
            get { return favouriteNameTextBox.Text; }
        }

        public string getFavouriteUrl
        {
            get { return urlTextBox.Text; }
        }

        private void okayButton_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(favouriteNameTextBox.Text))
            {
                MessageBox.Show("Please enter a valid name for your Url");
                return;
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
