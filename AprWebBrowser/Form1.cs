using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Text;

namespace AprWebBrowser
{
    public partial class Form1 : Form
    {
        private string homePageUrl = "https://www.hw.ac.uk/";
        private string currentUrl = "https://www.hw.ac.uk/";
        static readonly HttpClient client = new HttpClient();
        private List<string> favouritesList = new List<string>();
        private List<string> historyList = new List<string>();
        private string bulkPath = "bulk.txt";
        static Stack<string> forwardNavigationStack = new Stack<string>();
        static Stack<string> backwardNavigationStack = new Stack<string>();
        public Form1()
        {
            InitializeComponent();
            loadFavourites();
            loadHistory();
            createBulkFile();
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
            // favouriteListView.View = View.Details;
            //listView1.Columns.Add("name", 100);
            //listView1.Columns.Add("url", 300);
        }



        // This method sends an http request and fetch the raw HTML code
        // parameter url is the website url
        // parameter bulkFlag indicates whether this function was called from bulk button or not
        private async Task fetchHtmlCode(string url, bool bulkFlag = false)
        {
            try
            {
                if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
                {
                   
                    HttpResponseMessage response = await client.GetAsync(url);

                    statusCodeLabel.Text = $"Status Code: {(int)response.StatusCode}";
                    string responseBody = await response.Content.ReadAsStringAsync();
                    int bytes = Encoding.UTF8.GetByteCount(responseBody);
                    if (!bulkFlag)
                    { 
                    // This switch handles different status codes and shows corresponding message in the search Result Box
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
                    updateNavigationStacks(url);
                        
                    AddUrlToHistoryList(currentUrl);
                    setPageTitle(responseBody.Trim());
                    }
                    else
                    {
                        searchResultBox.AppendText($"<{(int)response.StatusCode}>       <{bytes}>       <{url}>\n");
                    }
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
        //updates the navigation buttons according tto the navigation stacks
        private void updateNavigationButtons()
        {
            backButton.Enabled = backwardNavigationStack.Count > 0;
            forwardButton.Enabled = forwardNavigationStack.Count > 0;
        }

        // extracts the page title from the HTML code using regex
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
        // handles the search button click
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
            if (historyListBox.Visible)
            {
                historyAndFavLabel.Visible = false;
                clearHistory.Visible = false;
            }
            await fetchHtmlCode(searchTextBox.Text);
        }

        //loads the home page from the text file if it's present
        private void loadHomePage()
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
        // Refreshes the current web page
        private void refreshButton_Click(object sender, EventArgs e)
        {
            searchResultBox.Clear();
            this.Text = "";
            statusCodeLabel.Text = "Status Code: ";
            fetchHtmlCode(currentUrl);

        }

        // handles the add fav button click
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

        // adds favourites to the the favourites list
        private void AddToFavouritesList(string name, string url)
        {
            favouritesList.Add($"{name} | {url}");
            updateFavouriteListBox();
            saveFavouritesToTextFile();
        }

        // updates the UI i.e. the list containing favourites
        private void updateFavouriteListBox()
        {
            favouriteListBox.Items.Clear();
            foreach (string favourite in favouritesList)
            {
                favouriteListBox.Items.Add(favourite);
            }
        }

        // saves the favourites to the favourite text file
        private void saveFavouritesToTextFile()
        {
            using (StreamWriter writetext = new StreamWriter("favourite.txt"))
            {
                foreach (string favourite in favouritesList)
                    writetext.WriteLine(favourite);
            }
        }

        // loads favourites when the application is launched
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

        // loads the history page when the application is launched
        private void loadHistory()
        {
            if (File.Exists("history.txt"))
            {
                historyList.Clear();
                using (StreamReader readtext = new StreamReader("history.txt"))
                {
                    string url;
                    while ((url = readtext.ReadLine()) != null)
                    {
                        historyList.Add(url);
                    }
                }
                updateHistoryListBox();
            }
        }

        // not used
        private void favouriteListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        //extracts the url from the name and url string saved in favourites list
        private string extractUrl(string nameAndUrl)
        {
            string[] fullFavouriteString = nameAndUrl.Split('|');
            return fullFavouriteString.Length == 2 ? fullFavouriteString[1].Trim() : "";
        }
        // handles the favourite button click
        private void favouritesMenu_Click(object sender, EventArgs e)
        {
            historyListBox.Visible = false;
            historyAndFavLabel.Visible = !favouriteListBox.Visible;
            historyAndFavLabel.Text = "Favourites";
            clearHistory.Visible = false;
            favouriteListBox.Visible = !favouriteListBox.Visible;
            favouriteButton.Location = favouriteListBox.Visible ? new Point(1090, 35) : new Point(794, 10);
            deleteFavouritesButton.Visible = favouriteListBox.Visible;
            modifyFavButton.Visible = favouriteListBox.Visible;
        }

        // handles the delete favourite button click
        private void deleteFavouritesButton_Click(object sender, EventArgs e)
        {
            if (favouriteListBox.SelectedItem != null)
            {
                favouritesList.Remove(favouriteListBox.SelectedItem.ToString());
            }
            updateFavouriteListBox();
            saveFavouritesToTextFile();
        }
        // handles the modify favourite button click
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

        // handles the home button click
        private void homeButton_Click(object sender, EventArgs e)
        {
            using (HomeDialog dialog = new HomeDialog(homePageUrl))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    homePageUrl = dialog.getHomeUrl;
                    saveHomePageUrlToTextFile(dialog.getHomeUrl);
                }
            }
        }
        // saves the home page url to a text file
        private void saveHomePageUrlToTextFile(string url)
        {
            using (StreamWriter writetext = new StreamWriter("home.txt"))
            {
                writetext.WriteLine(url);
            }
        }
        // handls the go home button click
        private void goHomeButton_Click(object sender, EventArgs e)
        {
            searchTextBox.Text = "";
            searchResultBox.Clear();
            searchTextBox.Text = homePageUrl;
            fetchHtmlCode(homePageUrl);
        }

        //Handles double click in the favourites list box to navigate to the selected webpage
        private void favouriteListBox_DoubleClick(object sender, EventArgs e)
        {
            if (favouriteListBox.SelectedIndex != -1)
            {
                string url = extractUrl(favouriteListBox.SelectedItem.ToString());

                if (!string.IsNullOrEmpty(url))
                    searchTextBox.Text = url;
                searchResultBox.Clear();
                fetchHtmlCode(url);
                
                forwardNavigationStack.Clear();
            }

        }
        // adds url to history list
        private void AddUrlToHistoryList(string url)
        {
            string timeStamp = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
            historyList.Add($"{timeStamp} | {url}");
            saveHistoryToTextFile();
            updateHistoryListBox();
        }

        // handles the history button click
        private void historyButton_Click(object sender, EventArgs e)
        {
            favouriteListBox.Visible = false;
            deleteFavouritesButton.Visible = false;
            modifyFavButton.Visible = false;
            favouriteButton.Location = new Point(794, 10);
            historyListBox.Visible = !historyListBox.Visible;
            clearHistory.Visible = historyListBox.Visible;
            historyAndFavLabel.Visible = historyListBox.Visible;
            historyAndFavLabel.Text = "History";


        }
        // update the UI i.e. the  history list box
        private void updateHistoryListBox()
        {
            historyListBox.Items.Clear();
            foreach (string history in historyList)
            {
                historyListBox.Items.Add(history);
            }
        }
        // handles the clear history button click
        private void clearHistory_Click(object sender, EventArgs e)
        {
            historyList.Clear();
            updateHistoryListBox();
            if (File.Exists("history.txt"))
            {
                File.Delete("history.txt");
            }

        }
        // saves the history to a text file
        private void saveHistoryToTextFile()
        {
            using (StreamWriter writetext = new StreamWriter("history.txt"))
            {
                foreach (string history in historyList)
                    writetext.WriteLine(history);
            }
        }

        //Handles double click in the history list box to navigate to the selected webpage
        private void historyListBox_DoubleClick(object sender, EventArgs e)
        {
            if (historyListBox.SelectedIndex != -1)
            {
                string url = string.Empty;
                string[] selectedHistory = historyListBox.SelectedItem.ToString().Split('|');
                if (selectedHistory.Length == 2)
                {
                     url = selectedHistory[1];
                 
                fetchHtmlCode(url);
                searchTextBox.Text =url;
                forwardNavigationStack.Clear();
                }
                 
            }
        }
        // handles the back button click to navigate to previous web page
        private void backButton_Click(object sender, EventArgs e)
        {
            if (backwardNavigationStack.Count > 0)
            {
                PushToStack(forwardNavigationStack, currentUrl);
                currentUrl = backwardNavigationStack.Pop();
                fetchHtmlCode(currentUrl);
                searchTextBox.Text = currentUrl;
                updateNavigationButtons();
            }

        }
        // pushes to the given either forward or backward stack if it's not empty 
        private void PushToStack(Stack<string> stack,string url)
        {
            if (stack.Count == 0 || stack.Peek()!= url)
            {
                stack.Push(url);
            }
        }

        // updatethe navigation stack if the current url is not null
        private void updateNavigationStacks(string url)
        {
            if(currentUrl != url)
            {
                PushToStack(backwardNavigationStack, currentUrl);
                forwardNavigationStack.Clear();
            }
            currentUrl = url;
            updateNavigationButtons();
        }

        // handles the forward button click to navigate to next web page
        private void forwardButton_Click(object sender, EventArgs e)
        {
            if (forwardNavigationStack.Count > 0)
            {
                PushToStack(backwardNavigationStack, currentUrl);
                currentUrl = forwardNavigationStack.Pop();
                fetchHtmlCode(currentUrl);
                searchTextBox.Text = currentUrl;
                updateNavigationButtons();
            }

        }
        // creates a default bulk.txt file with the top five visited websites
        private void createBulkFile()
        {
            string[] urls = { "https://www.google.com", "https://www.youtube.com", "https://www.facebook.com", "https://www.wikipedia.org", "https://www.amazon.com" };
            if (!File.Exists(bulkPath))
            {
                using (StreamWriter writetext = new StreamWriter(bulkPath))
                {
                    foreach(string url in urls)
                    {
                        writetext.WriteLine(url);

                    }
                }
            }
        }
        // handles the bulk button click
        private async void bulkButton_Click(object sender, EventArgs e)
        {
            searchResultBox.Clear();
            if (File.Exists(bulkPath))
            {
                string[] urls = File.ReadAllLines(bulkPath);
                foreach (string url in urls)
                {
                    fetchHtmlCode(url, true);

                }

            }
        }
        // handles the change bulk button click
        private void changeBulkButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {

                    bulkPath = openFileDialog.FileName;
                    MessageBox.Show($"New bulk file path has been set: {bulkPath}");

                }
                else
                {
                    MessageBox.Show("Please select a folder to set a new file for Bulk Download");
                }
            }
        }

        private async void searchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (favouriteListBox.Visible)
                {
                    favouriteListBox.Visible = false;
                    favouriteButton.Location = new Point(713, 39);
                    deleteFavouritesButton.Visible = false;
                    modifyFavButton.Visible = false;
                    historyAndFavLabel.Visible = false;
                }
                if (historyListBox.Visible)
                {
                    historyAndFavLabel.Visible = false;
                    clearHistory.Visible = false;
                }
                await fetchHtmlCode(searchTextBox.Text);
            }
        }
    }
    
}
