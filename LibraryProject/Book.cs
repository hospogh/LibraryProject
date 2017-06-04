//lines 57, 

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace LibraryProject
{
    public class Book
    {
        //fields
        public string Name;

        public string Author;
        public int BookAge;
        public double Price = -1;
        public int PagesCount = -1;
        public List<BookUsing> BookHistory { get; }

        //constructors
        public Book(string name, string author, int bookAge)
        {
            this.Name = name;
            this.Author = author;
            this.BookAge = bookAge;
            BookHistory = new List<BookUsing>();
        }

        public Book(string name, string author, int bookAge, double price) : this(name, author, bookAge)
        {
            this.Price = price;
        }

        public Book(string name, string author, int bookAge, int pagesCount) : this(name, author, bookAge)
        {
            this.PagesCount = pagesCount;
        }

        public Book(string name, string author, int bookAge, double price, int pagesCount) : this(name, author, bookAge,
            price)
        {
            this.PagesCount = pagesCount;
        }

        //Methods and Properties
        public void Use(User currentUser, DateTime endTime)
        {
            if (!IsNowUsing)
            {
                BookHistory.Add(new BookUsing(currentUser, endTime));
            }
            else
            {
                //MessageBox.Show("Book is now using{0}");//add currentUser{0}-ի կողմից
            }
        }

        public User CurrentUser => BookHistory[BookHistory.Count].CurrentUser;
        public DateTime UsingEndTime => BookHistory[BookHistory.Count].EndUsingTime;
        public Boolean IsNowUsing => DateTime.Now > BookHistory[BookHistory.Count].EndUsingTime;
    }

    public class BookUsing
    {
        public User CurrentUser;
        public DateTime StartUsingTime;
        public DateTime EndUsingTime;

        public BookUsing(User currentUser, DateTime endTime)
        {
            this.CurrentUser = currentUser;
            this.EndUsingTime = endTime;
            StartUsingTime = DateTime.Now;
        }
    }
}