using System;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.IO;
using System.Collections.ObjectModel;

namespace biblia
{
    class Database
    {
        SQLiteConnection sqlconnection;
        const string DBName = "database.sqlite3";

        public Database()
        {
            sqlconnection = new SQLiteConnection(String.Format("Data Source={0}", DBName));
            sqlconnection.Open();
            DatabaseSetup();
        }

        public string XDEncryption(string str)
        {
            // encrypts the given string
            string result = "";
            Byte[] b = Encoding.ASCII.GetBytes(str);
            str = string.Join(" ", b.Select(byt => Convert.ToString(byt, 2).PadLeft(8, '0')));
            for(int i = 0; i < str.Length; i++)
            {
                if(str[i]%2 == 0)
                {
                    result += "X";
                }
                else
                {
                    result += "D";
                }
            }
            return result;
        }

        public void DatabaseSetup()
        {
            // database setup ;)
            if (!File.Exists(DBName))
            {
                SQLiteConnection.CreateFile(DBName);
            }
            // creates and fills users table :P
            string query = $"create table if not exists users(id integer primary key, username varchar(20) not null, passw0rd varchar(220) not null, permission bool);" +
                $"insert into users (username, passw0rd, permission) select 'admin','{XDEncryption("!QAZ2wsx!")}', true where not exists (select 1 from users where id=1);" +
                $"insert into users (username, passw0rd, permission) select 'user','{XDEncryption("glowna")}', false where not exists (select 1 from users where id=2);";
            new SQLiteCommand(query, sqlconnection).ExecuteNonQuery();
            // creates and fills books table >~<
            query = "create table if not exists books(id integer primary key, title varchar(40) not null, author varchar(40) not null, year varchar(20) not null, publisher varchar(40) not null, avaliability bool);" +
                "insert into books (id, title, author, year, publisher, avaliability) select 1, 'Być (nie)zwykłym wychowawcą', 'Anna Konarzewska', '2019', '>.<', true where not exists (select 1 from books where id=1);" +
                "insert into books (id, title, author, year, publisher, avaliability) select 2, 'Ciekawi Swiata 2', 'T. Bielecki', '2013', 'ZSL', true where not exists (select 1 from books where id=2);" +
                "insert into books (id, title, author, year, publisher, avaliability) select 3, 'Ferdydurke', 'Gombrowicz', '1974', 'MlodaPolska', true where not exists (select 1 from books where id=3);";
            new SQLiteCommand(query, sqlconnection).ExecuteNonQuery();
        }

        public LoginResult Login(string username, string password)
        {
            password = XDEncryption(password);
            string query = String.Format("select permission from users where username='{0}' and passw0rd='{1}'", username, password);
            SQLiteDataReader queryresult = new SQLiteCommand(query, sqlconnection).ExecuteReader();
            if (queryresult.HasRows)
            {
                while (queryresult.Read())
                {
                    if ((bool)queryresult["permission"] == true)
                        return new LoginResult(true, "Administrator", username);
                    else
                        return new LoginResult(true, "User", username);
                }
                return new LoginResult(false, "error", "error");
            }
            else
                return new LoginResult(false, "error" , "error");
        }

        public void AddBook(Book book)
        {
            string query = String.Format("insert into books(title, author, year, publisher, avaliability) values ('{0}', '{1}', {2}, '{3}', true);", book.Title, book.Author, book.Year, book.Publisher);
            new SQLiteCommand(query, sqlconnection).ExecuteNonQuery();
        }

        public void DeleteBook(int ID)
        {
            string query = String.Format("delete from books where id={0};", ID);
            new SQLiteCommand(query, sqlconnection).ExecuteNonQuery();
        }

        public void RentBook(int ID)
        {
            string query = String.Format("update books set avaliability=false where id={0};", ID);
            new SQLiteCommand(query, sqlconnection).ExecuteNonQuery();
        }

        public void ReturnBook(int ID)
        {
            string query = String.Format("update books set avaliability=true where id={0};", ID);
            new SQLiteCommand(query, sqlconnection).ExecuteNonQuery();
        }

        public ObservableCollection<Book> ListBooks(SearchValues values)
        {
            // returns a list of all or searched books
            ObservableCollection<Book> books = new ObservableCollection<Book>();
            string query = $"select * from books where title like '%{values.Title}%' and author like '%{values.Author}%' and year like '%{values.Year}%' and publisher like '%{values.Publisher}%';";
            SQLiteDataReader queryresult = new SQLiteCommand(query, sqlconnection).ExecuteReader();
            if (queryresult.HasRows)
            {
                while (queryresult.Read())
                {
                    Book book = new Book(
                        (string)queryresult["title"],
                        (string)queryresult["author"],
                        (string)queryresult["year"],
                        (string)queryresult["publisher"],
                        (bool)queryresult["avaliability"]
                        );
                    book.ID = (Int64)queryresult["id"];
                    books.Add(book);
                }
            }
            return books;
        }
    }
}
