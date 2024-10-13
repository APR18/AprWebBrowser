using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;
using System.Drawing;

namespace AprWebBrowser
{
    public partial class Form1 : Form
    {
        private string homePageUrl = "https://www.hw.ac.uk/";
        private string currentUrl = "https://www.hw.ac.uk/";
        static readonly HttpClient client = new HttpClient();
        private List<string> favouritesList = new List<string>();
        private List<string> historyList = new List<string>();

        static Stack<string> forwardNavigationStack = new Stack<string>();
        static Stack<string> backwardNavigationStack = new Stack<string>();
        public Form1()
        {
            InitializeComponent();
            loadFavourites();
            loadHistory();
            backButton.Enabled = false;
            forwardButton.Enabled = false;
            favouriteListBox.Visible = false;
            historyListBox.Visible = false;
            modifyFavButton.Visible = false;
            deleteFavouritesButton.Visible = false;
            clearHistory.Visible = false;
            historyAndFavLabel.Visible = false;
            loadHomePage();
            searchTextBox.Text = currentUrl;
        }
        private async Task fetchHtmlCode(string url)
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
                {
                    // Above three lines can be replaced with new helper method below
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    statusCodeLabel.Text = $"Status Code: {(int)response.StatusCode}";
                    string responseBody = await response.Content.ReadAsStringAsync();

                    switch (response.StatusCode)
                    {
                        case System.Net.HttpStatusCode.OK:
                            searchResultBox.Text = responseBody.Trim();
                            break;
                        case System.Net.HttpStatusCode.BadRequest:
                            searchResultBox.Text = $"Error Code : 400 Bad request: The server could not process your request";
                            break;
                        case System.Net.HttpStatusCode.Forbidden:
                            searchResultBox.Text = $"Error Code : 403 Forbidden: You don't have access to this site";
                            break;
                        case System.Net.HttpStatusCode.NotFound:
                            searchResultBox.Text = $"Error Code : 404 Not Found : Page not found";
                            break;
                        default:
                            searchResultBox.Text = $"Please check your url and try again";
                            break;
                    }
                    if (currentUrl != url)
                    {
                        backwardNavigationStack.Push(url);
                        forwardNavigationStack.Clear();
                        updateNavigationButtons();
                    }
                    currentUrl = url;
                    AddUrlToHistoryList(currentUrl);
                    setPageTitle(responseBody.Trim());
                    

                }
                else
                {
                    MessageBox.Show("Please enter a valid url and try again!!!");
                }

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
           
        }
        private void updateNavigationButtons()
        {

        }
        private void setPageTitle(string htmlCode)
        {
            string pattern = @"<title>\s*(.+?)\s*</title>";
            Match match = Regex.Match(htmlCode, pattern);
           if (match.Success)
            {
                // MessageBox.Show(match.Groups[1].Value);
                this.Text = match.Groups[1].Value;
            }
            else
            {
                this.Text = $"Title not Found";
            }
             
        }
        private async void searchButton_Click(object sender, EventArgs e)
        {
            
            if (favouriteListBox.Visible)
            {
                favouriteListBox.Visible = false;
                favouriteButton.Location = new Point(713, 39);
                deleteFavouritesButton.Visible = false;
                modifyFavButton.Visible = false;
                historyAndFavLabel.Visible = false;
            }
            if(historyListBox.Visible)
            {
                historyAndFavLabel.Visible = false;
                clearHistory.Visible = false;
            }
            await fetchHtmlCode(searchTextBox.Text);
        }
        private void loadHomePage( )
        {

            if (File.Exists("home.txt"))
            {

                using (StreamReader readtext = new StreamReader("home.txt"))
                {
                    string homeUrl = readtext.ReadLine();
                    currentUrl = homeUrl;
                    homePageUrl = homeUrl;
                    if (homeUrl != null)
                    {
                        fetchHtmlCode(homeUrl);
                    }
                }
            }
            else
            {
                saveHomePageUrlToTextFile(homePageUrl);
                fetchHtmlCode(homePageUrl);
            }
        }
        private void refreshButton_Click(object sender, EventArgs e)
        {
            searchResultBox.Clear();
            this.Text = "";
            statusCodeLabel.Text = "Status Code: ";
            fetchHtmlCode(currentUrl);

        }
   
        private void favouriteButton_Click(object sender, EventArgs e)
        {
          
            using (FavouriteDialog dialog = new FavouriteDialog(currentUrl))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    AddToFavouritesList(dialog.getFavouriteName, dialog.getFavouriteUrl);
                }
            }
        }
        private void AddToFavouritesList(string name, string url)
        {
            favouritesList.Add($"{name} | {url}");
            updateFavouriteListBox();
            saveFavouritesToTextFile();
        }
        private void updateFavouriteListBox()
        {
            favouriteListBox.Items.Clear();
            foreach(string favourite in favouritesList)
            {
                favouriteListBox.Items.Add(favourite);
            }
        }
        private void saveFavouritesToTextFile()
        {
            using (StreamWriter writetext = new StreamWriter("favourite.txt"))
            {
                foreach(string favourite in favouritesList)
                    writetext.WriteLine(favourite);
            }
        }
        private void loadFavourites()
        {
            if (File.Exists("favourite.txt"))
            {
                favouritesList.Clear();
                using (StreamReader readtext = new StreamReader("favourite.txt"))
                {
                    string url;
                    while ((url = readtext.ReadLine()) != null)
                    {
                        favouritesList.Add(url);
                    }
                }
                updateFavouriteListBox();
            }
        }
        private void loadHistory()
        {
            if (File.Exists("history.txt"))
            {
                historyList.Clear();
                using(StreamReader readtext = new StreamReader("history.txt"))
                {
                    string url;
                    while((url = readtext.ReadLine()) != null)
                    {
                        historyList.Add(url);
                    }
                }
                updateHistoryListBox();
            }
        }
        private void favouriteListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private string extractUrl(string nameAndUrl)
        {
            string[] fullFavouriteString = nameAndUrl.Split('|');
            return fullFavouriteString.Length == 2 ? fullFavouriteString[1].Trim(): "";
        }
        private void favouritesMenu_Click(object sender, EventArgs e)
        {
            favouriteListBox.Visible = !favouriteListBox.Visible;
            favouriteButton.Location = favouriteListBox.Visible? new Point(564, 39): new Point(713, 39);
            deleteFavouritesButton.Visible = favouriteListBox.Visible;
            modifyFavButton.Visible = favouriteListBox.Visible;
        }
        private void deleteFavouritesButton_Click(object sender, EventArgs e)
        {
            if (favouriteListBox.SelectedItem!= null)
            {
                favouritesList.Remove(favouriteListBox.SelectedItem.ToString()); 
            }
            updateFavouriteListBox();
            saveFavouritesToTextFile();
        }
        private void modifyFavButton_Click(object sender, EventArgs e)
        {
            if (favouriteListBox.SelectedItem != null)
            {
                string url = extractUrl(favouriteListBox.SelectedItem.ToString());
                int index = favouritesList.IndexOf(favouriteListBox.SelectedItem.ToString());
                
                using (FavouriteDialog dialog = new FavouriteDialog(url))
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        favouritesList.Remove(favouriteListBox.SelectedItem.ToString());
                        favouritesList.Insert(index, $"{dialog.getFavouriteName} | {dialog.getFavouriteUrl}");
                    }
                }
                updateFavouriteListBox();
                saveFavouritesToTextFile();

            }
        }
        private void homeButton_Click(object sender, EventArgs e)
        {
            using(HomeDialog dialog = new HomeDialog(homePageUrl))
            {
                if(dialog.ShowDialog() == DialogResult.OK)
                {
                    homePageUrl = dialog.getHomeUrl;
                    saveHomePageUrlToTextFile(dialog.getHomeUrl);
                }
            }
        }
        private void saveHomePageUrlToTextFile(string url)
        {
            using (StreamWriter writetext = new StreamWriter("home.txt"))
            {
                writetext.WriteLine(url);
            }
        }
        private void goHomeButton_Click(object sender, EventArgs e)
        {
            searchTextBox.Text = "";
            searchResultBox.Clear();
            searchTextBox.Text = homePageUrl;
            fetchHtmlCode(homePageUrl);
        }
        private void favouriteListBox_DoubleClick(object sender, EventArgs e)
        {
            if (favouriteListBox.SelectedIndex != -1)
            {
                string url = extractUrl(favouriteListBox.SelectedItem.ToString());

                if (!string.IsNullOrEmpty(url))
                    searchTextBox.Text = url;
                    searchResultBox.Clear();
                    fetchHtmlCode(url);
            }

        }
        private void AddUrlToHistoryList(string url)
        {
            historyList.Add(url);
            saveHistoryToTextFile();
            updateHistoryListBox();
        }
        private void historyButton_Click(object sender, EventArgs e)
        {
            historyListBox.Visible = !historyListBox.Visible;
            clearHistory.Visible = historyListBox.Visible;
            historyAndFavLabel.Visible = historyListBox.Visible;
            historyAndFavLabel.Text = "History";


        }

        private void updateHistoryListBox()
        {
            historyListBox.Items.Clear();
            foreach (string history in historyList)
            {
                historyListBox.Items.Add(history);
            }
        }

        private void clearHistory_Click(object sender, EventArgs e)
        {
            historyList.Clear();
            updateHistoryListBox();
            if (File.Exists("history.txt"))
            {
                File.Delete("history.txt");
            }
           
        }

        private void saveHistoryToTextFile()
        {
            using (StreamWriter writetext = new StreamWriter("history.txt"))
            {
                foreach (string history in historyList)
                    writetext.WriteLine(history);
            }
        }

        private void historyListBox_DoubleClick(object sender, EventArgs e)
        {
            if (historyListBox.SelectedIndex != -1)
            {
                fetchHtmlCode(favouriteListBox.SelectedItem.ToString());
            }

        }

        private void backButton_Click(object sender, EventArgs e)
        {
            if (backwardNavigationStack.Count == 0)
            {
                backButton.Enabled = false;
                return;
            }
            forwardNavigationStack.Push(currentUrl);
            currentUrl = backwardNavigationStack.Pop();
            searchTextBox.Text = currentUrl;
            fetchHtmlCode(currentUrl);
        }

        private void forwardButton_Click(object sender, EventArgs e)
        {

        }
    }
}
