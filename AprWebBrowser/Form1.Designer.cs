
namespace AprWebBrowser
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.backButton = new System.Windows.Forms.Button();
            this.forwardButton = new System.Windows.Forms.Button();
            this.searchResultBox = new System.Windows.Forms.RichTextBox();
            this.statusCodeLabel = new System.Windows.Forms.Label();
            this.refreshButton = new System.Windows.Forms.Button();
            this.favouriteButton = new System.Windows.Forms.Button();
            this.favouriteListBox = new System.Windows.Forms.ListBox();
            this.favouritesMenu = new System.Windows.Forms.Button();
            this.deleteFavouritesButton = new System.Windows.Forms.Button();
            this.modifyFavButton = new System.Windows.Forms.Button();
            this.homeButton = new System.Windows.Forms.Button();
            this.historyButton = new System.Windows.Forms.Button();
            this.goHomeButton = new System.Windows.Forms.Button();
            this.historyListBox = new System.Windows.Forms.ListBox();
            this.clearHistory = new System.Windows.Forms.Button();
            this.historyAndFavLabel = new System.Windows.Forms.Label();
            this.bulkButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // searchTextBox
            // 
            this.searchTextBox.Location = new System.Drawing.Point(175, 13);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(532, 20);
            this.searchTextBox.TabIndex = 0;
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(713, 10);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(75, 23);
            this.searchButton.TabIndex = 1;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(13, 13);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(30, 23);
            this.backButton.TabIndex = 2;
            this.backButton.Text = "<";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // forwardButton
            // 
            this.forwardButton.Location = new System.Drawing.Point(49, 13);
            this.forwardButton.Name = "forwardButton";
            this.forwardButton.Size = new System.Drawing.Size(31, 23);
            this.forwardButton.TabIndex = 3;
            this.forwardButton.Text = ">";
            this.forwardButton.UseVisualStyleBackColor = true;
            this.forwardButton.Click += new System.EventHandler(this.forwardButton_Click);
            // 
            // searchResultBox
            // 
            this.searchResultBox.Location = new System.Drawing.Point(13, 96);
            this.searchResultBox.Name = "searchResultBox";
            this.searchResultBox.Size = new System.Drawing.Size(775, 342);
            this.searchResultBox.TabIndex = 4;
            this.searchResultBox.Text = "";
            // 
            // statusCodeLabel
            // 
            this.statusCodeLabel.AutoSize = true;
            this.statusCodeLabel.Location = new System.Drawing.Point(12, 49);
            this.statusCodeLabel.Name = "statusCodeLabel";
            this.statusCodeLabel.Size = new System.Drawing.Size(65, 13);
            this.statusCodeLabel.TabIndex = 5;
            this.statusCodeLabel.Text = "Status Code";
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(86, 13);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(75, 23);
            this.refreshButton.TabIndex = 6;
            this.refreshButton.Text = "refresh";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // favouriteButton
            // 
            this.favouriteButton.Location = new System.Drawing.Point(713, 39);
            this.favouriteButton.Name = "favouriteButton";
            this.favouriteButton.Size = new System.Drawing.Size(75, 23);
            this.favouriteButton.TabIndex = 7;
            this.favouriteButton.Text = "Add Fav";
            this.favouriteButton.UseVisualStyleBackColor = true;
            this.favouriteButton.Click += new System.EventHandler(this.favouriteButton_Click);
            // 
            // favouriteListBox
            // 
            this.favouriteListBox.FormattingEnabled = true;
            this.favouriteListBox.Location = new System.Drawing.Point(564, 96);
            this.favouriteListBox.Name = "favouriteListBox";
            this.favouriteListBox.Size = new System.Drawing.Size(224, 342);
            this.favouriteListBox.TabIndex = 8;
            this.favouriteListBox.SelectedIndexChanged += new System.EventHandler(this.favouriteListBox_SelectedIndexChanged);
            this.favouriteListBox.DoubleClick += new System.EventHandler(this.favouriteListBox_DoubleClick);
            // 
            // favouritesMenu
            // 
            this.favouritesMenu.Location = new System.Drawing.Point(256, 39);
            this.favouritesMenu.Name = "favouritesMenu";
            this.favouritesMenu.Size = new System.Drawing.Size(75, 23);
            this.favouritesMenu.TabIndex = 9;
            this.favouritesMenu.Text = "Favourites";
            this.favouritesMenu.UseVisualStyleBackColor = true;
            this.favouritesMenu.Click += new System.EventHandler(this.favouritesMenu_Click);
            // 
            // deleteFavouritesButton
            // 
            this.deleteFavouritesButton.Location = new System.Drawing.Point(645, 39);
            this.deleteFavouritesButton.Name = "deleteFavouritesButton";
            this.deleteFavouritesButton.Size = new System.Drawing.Size(75, 23);
            this.deleteFavouritesButton.TabIndex = 10;
            this.deleteFavouritesButton.Text = "Del fav";
            this.deleteFavouritesButton.UseVisualStyleBackColor = true;
            this.deleteFavouritesButton.Click += new System.EventHandler(this.deleteFavouritesButton_Click);
            // 
            // modifyFavButton
            // 
            this.modifyFavButton.Location = new System.Drawing.Point(726, 39);
            this.modifyFavButton.Name = "modifyFavButton";
            this.modifyFavButton.Size = new System.Drawing.Size(62, 23);
            this.modifyFavButton.TabIndex = 11;
            this.modifyFavButton.Text = "Modify";
            this.modifyFavButton.UseVisualStyleBackColor = true;
            this.modifyFavButton.Click += new System.EventHandler(this.modifyFavButton_Click);
            // 
            // homeButton
            // 
            this.homeButton.Location = new System.Drawing.Point(418, 39);
            this.homeButton.Name = "homeButton";
            this.homeButton.Size = new System.Drawing.Size(75, 23);
            this.homeButton.TabIndex = 12;
            this.homeButton.Text = "Edit Home";
            this.homeButton.UseVisualStyleBackColor = true;
            this.homeButton.Click += new System.EventHandler(this.homeButton_Click);
            // 
            // historyButton
            // 
            this.historyButton.Location = new System.Drawing.Point(337, 39);
            this.historyButton.Name = "historyButton";
            this.historyButton.Size = new System.Drawing.Size(75, 23);
            this.historyButton.TabIndex = 13;
            this.historyButton.Text = "History";
            this.historyButton.UseVisualStyleBackColor = true;
            this.historyButton.Click += new System.EventHandler(this.historyButton_Click);
            // 
            // goHomeButton
            // 
            this.goHomeButton.Location = new System.Drawing.Point(175, 39);
            this.goHomeButton.Name = "goHomeButton";
            this.goHomeButton.Size = new System.Drawing.Size(75, 23);
            this.goHomeButton.TabIndex = 14;
            this.goHomeButton.Text = "Go Home";
            this.goHomeButton.UseVisualStyleBackColor = true;
            this.goHomeButton.Click += new System.EventHandler(this.goHomeButton_Click);
            // 
            // historyListBox
            // 
            this.historyListBox.FormattingEnabled = true;
            this.historyListBox.Location = new System.Drawing.Point(564, 96);
            this.historyListBox.Name = "historyListBox";
            this.historyListBox.Size = new System.Drawing.Size(224, 342);
            this.historyListBox.TabIndex = 15;
            this.historyListBox.DoubleClick += new System.EventHandler(this.historyListBox_DoubleClick);
            // 
            // clearHistory
            // 
            this.clearHistory.Location = new System.Drawing.Point(713, 72);
            this.clearHistory.Name = "clearHistory";
            this.clearHistory.Size = new System.Drawing.Size(75, 23);
            this.clearHistory.TabIndex = 16;
            this.clearHistory.Text = "Clear History";
            this.clearHistory.UseVisualStyleBackColor = true;
            this.clearHistory.Click += new System.EventHandler(this.clearHistory_Click);
            // 
            // historyAndFavLabel
            // 
            this.historyAndFavLabel.AutoSize = true;
            this.historyAndFavLabel.Location = new System.Drawing.Point(564, 77);
            this.historyAndFavLabel.Name = "historyAndFavLabel";
            this.historyAndFavLabel.Size = new System.Drawing.Size(66, 13);
            this.historyAndFavLabel.TabIndex = 17;
            this.historyAndFavLabel.Text = "historyOrFav";
            // 
            // bulkButton
            // 
            this.bulkButton.Location = new System.Drawing.Point(499, 39);
            this.bulkButton.Name = "bulkButton";
            this.bulkButton.Size = new System.Drawing.Size(60, 23);
            this.bulkButton.TabIndex = 18;
            this.bulkButton.Text = "Bulk";
            this.bulkButton.UseVisualStyleBackColor = true;
            this.bulkButton.Click += new System.EventHandler(this.bulkButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.bulkButton);
            this.Controls.Add(this.historyAndFavLabel);
            this.Controls.Add(this.clearHistory);
            this.Controls.Add(this.historyListBox);
            this.Controls.Add(this.goHomeButton);
            this.Controls.Add(this.historyButton);
            this.Controls.Add(this.homeButton);
            this.Controls.Add(this.modifyFavButton);
            this.Controls.Add(this.deleteFavouritesButton);
            this.Controls.Add(this.favouritesMenu);
            this.Controls.Add(this.favouriteListBox);
            this.Controls.Add(this.favouriteButton);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.statusCodeLabel);
            this.Controls.Add(this.searchResultBox);
            this.Controls.Add(this.forwardButton);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.searchTextBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Button forwardButton;
        private System.Windows.Forms.RichTextBox searchResultBox;
        private System.Windows.Forms.Label statusCodeLabel;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.Button favouriteButton;
        private System.Windows.Forms.ListBox favouriteListBox;
        private System.Windows.Forms.Button favouritesMenu;
        private System.Windows.Forms.Button deleteFavouritesButton;
        private System.Windows.Forms.Button modifyFavButton;
        private System.Windows.Forms.Button homeButton;
        private System.Windows.Forms.Button historyButton;
        private System.Windows.Forms.Button goHomeButton;
        private System.Windows.Forms.ListBox historyListBox;
        private System.Windows.Forms.Button clearHistory;
        private System.Windows.Forms.Label historyAndFavLabel;
        private System.Windows.Forms.Button bulkButton;
    }
}

