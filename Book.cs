using System;

namespace biblia
{
    public class Book
    {
        private Int64 iD;
        private string title, author, year, publisher;
        private bool avaliability;

        public Book(string title, string author, string year, string publisher, bool avaliability)
        {
            Title = title;
            Author = author;
            Year = year;
            Publisher = publisher;
            Avaliability = avaliability;
        }
        public Int64 ID { get => iD; set => iD = value; }
        public string Title { get => title; set => title = value; }
        public string Author { get => author; set => author = value; }
        public string Year { get => year; set => year = value; }
        public string Publisher { get => publisher; set => publisher = value; }
        public bool Avaliability { get => avaliability; set => avaliability = value; }
    }
}
