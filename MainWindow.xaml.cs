using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;

namespace biblia
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml ( ͡o ꒳ ͡o )
    /// </summary>
    public partial class MainWindow : Window
    {
        Database db;
        string action;
        public MainWindow()
        {
            InitializeComponent();
            db = new Database();
        }

        public void SwitchUI(string toWhat, bool isAdmin)
        {
            switch (toWhat)
            {
                case "LoggedOut":
                    GridLoginOptions.Visibility = Visibility.Visible;
                    GridOptionsUser.Visibility = Visibility.Hidden;
                    GridOptionsAdmin.Visibility = Visibility.Hidden;
                    GridInputID.Visibility = Visibility.Hidden;
                    GridInputBook.Visibility = Visibility.Hidden;
                    DataGrid.Visibility = Visibility.Hidden;
                    break;
                case "LoggedIn":
                    GridLoginOptions.Visibility = Visibility.Hidden;
                    GridOptionsUser.Visibility = Visibility.Visible;
                    GridOptionsAdmin.Visibility = Visibility.Hidden;
                    if(isAdmin)
                        GridOptionsAdmin.Visibility = Visibility.Visible;
                    break;
                case "DisplayBooks":
                    GridInputID.Visibility = Visibility.Hidden;
                    GridInputBook.Visibility = Visibility.Hidden;
                    DataGrid.Visibility = Visibility.Visible;
                    break;
                case "InputBook":
                    GridInputID.Visibility = Visibility.Hidden;
                    GridInputBook.Visibility = Visibility.Visible;
                    DataGrid.Visibility = Visibility.Hidden;
                    break;
                case "InputID":
                    GridInputID.Visibility = Visibility.Visible;
                    GridInputBook.Visibility = Visibility.Hidden;
                    DataGrid.Visibility = Visibility.Hidden;
                    break;
            }
        }

        // button clicks :O
        public void ButtonLogin_Click(object sender, EventArgs e)
        {
            // checks username and password ( . Y . )
            string username = TBUsername.Text, 
                   password =  TBPassword.Text;
            LoginResult result = db.Login(username, password);
            if (result.IsLoggedIn)
            {
                LabelLoggedIn.Content = String.Format("Logged as {0}, {1}", result.Username, result.AccType);
                LabelLoggedIn.Margin = new Thickness(212, 20, 220, 0);
                TBUsername.Text = "";
                TBPassword.Text = "";
                if (result.AccType == "Administrator")
                    SwitchUI("LoggedIn", true);
                else
                    SwitchUI("LoggedIn", false);
            }
            else
                MessageBox.Show("Wrong username or password");
        }

        public void ButtonLogout_Click(object sender, EventArgs e)
        {
            // logs out ¯\_(ツ)_/¯
            SwitchUI("LoggedOut", false);
        }

        public void ButtonDisplay_Click(object sender, EventArgs e)
        {
            // displays all books :-]
            if (DataGrid.Visibility == Visibility.Hidden)
            {
                ObservableCollection<Book> books = db.ListBooks(new SearchValues("", "", "", ""));;
                if (books.Count > 0)
                {
                    SwitchUI("DisplayBooks", false);
                    DataGrid.ItemsSource = books;
                }
                else
                {
                    MessageBox.Show("No books in database");
                }
            }
            else
            {
                DataGrid.Visibility = Visibility.Hidden;
            }
                
        }

        public void ButtonSearch_Click(object sender, EventArgs e)
        {
            // opens book search options :=D
            if (GridInputBook.Visibility == Visibility.Hidden)
            {
                SwitchUI("InputBook", false);
                action = "Search";
            }
            else
                GridInputBook.Visibility = Visibility.Hidden;
        }

        public void ButtonRent_Click(object sender, EventArgs e)
        {
            // opens book rent options >:)
            if (GridInputID.Visibility == Visibility.Hidden)
            {
                SwitchUI("InputID", false);
                action = "Rent";
            }
            else
                GridInputID.Visibility = Visibility.Hidden;
        }

        public void ButtonReturn_Click(object sender, EventArgs e)
        {
            // opens book return options *<|:‑) (sw mikolaj emotka)
            if (GridInputID.Visibility == Visibility.Hidden)
            {
                SwitchUI("InputID", false);
                action = "Return";
            }
            else
                GridInputID.Visibility = Visibility.Hidden;
        }

        public void ButtonAdd_Click(object sender, EventArgs e)
        {
            // opens book add options O_o
            if (GridInputBook.Visibility == Visibility.Hidden)
            {
                SwitchUI("InputBook", false);
                action = "Add";
            }
            else
                GridInputBook.Visibility = Visibility.Hidden;
        }

        public void ButtonDelete_Click(object sender, EventArgs e)
        {
            // opens book delete options (=^··^=)
            if (GridInputID.Visibility == Visibility.Hidden)
            {
                SwitchUI("InputID", false);
                action = "Delete";
            }
            else
                GridInputID.Visibility = Visibility.Hidden;
        }

        public void ButtonInputID_Click(object sender, EventArgs e)
        {
            // submits book rent, return or delete ((d[-_-]b))
            if (Int32.TryParse(TBBookID.Text, out int n))
            {
                if (action == "Delete")
                {
                    db.DeleteBook(n);
                    MessageBox.Show("Deleted the book if u passed a correct id :^)");
                    GridInputID.Visibility = Visibility.Hidden;
                } 
                if (action == "Rent")
                {
                    db.RentBook(n);
                    MessageBox.Show("Rented the book if u passed a correct id :^)");
                    GridInputID.Visibility = Visibility.Hidden;
                }
                if (action == "Return")
                {
                    db.ReturnBook(n);
                    MessageBox.Show("Returned the book if u passed a correct id :^)");
                    GridInputID.Visibility = Visibility.Hidden;
                }
                TBBookID.Text = "";
            }
        }

        public void ButtonInputBook_Click(object sender, EventArgs e)
        {
            // submits book search or add ～°·_·°～
            if (Int32.TryParse(TBBookYear.Text, out int year) && TBBookTitle.Text.Length > 0 && TBBookAuthor.Text.Length > 0 && TBBookPublisher.Text.Length > 0 && action == "Add")
            {
                db.AddBook(new Book(TBBookTitle.Text, TBBookAuthor.Text, TBBookYear.Text, TBBookPublisher.Text, true));
                MessageBox.Show("Book added");
            }
            else if (action == "Search")
            {
                GridInputBook.Visibility = Visibility.Hidden;
                GridInputID.Visibility = Visibility.Hidden;
                if (DataGrid.Visibility == Visibility.Hidden)
                {
                    ObservableCollection<Book> books = db.ListBooks(new SearchValues(TBBookTitle.Text, TBBookAuthor.Text, TBBookYear.Text, TBBookPublisher.Text));
                    if (books.Count > 0)
                    {
                        DataGrid.ItemsSource = books;
                        DataGrid.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        MessageBox.Show("No books in database that meet given criteria");
                    }
                }
                else
                {
                    DataGrid.Visibility = Visibility.Hidden;
                }
            }
            TBBookTitle.Text = "";
            TBBookAuthor.Text = "";
            TBBookYear.Text = "";
            TBBookPublisher.Text = "";
            TBBookTitle.Text = "";
            GridInputBook.Visibility = Visibility.Hidden;
        }
    }
}
