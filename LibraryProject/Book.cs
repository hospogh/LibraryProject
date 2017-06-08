using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace LibraryProject
{
    public class Book
    {
        //fields
        public string Name { get; set; }

        public Guid ID { get; private set; }
        public string Author { get; set; }
        public int BookAge { get; set; }
        public double Price = -1;
        public int PagesCount = -1;
        public List<BookUsage> BookHistory { get; private set; }

        //constructors
        public Book(string name, string author, int bookAge)
        {
            Name = name;
            Author = author;
            BookAge = bookAge;
            BookHistory = new List<BookUsage>();
            ID = Guid.NewGuid();
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
        public override string ToString()
        {
            return $"{Name}[{Author}]({BookAge})";
        }

        public void Use(User currentUser, DateTime endTime)
        {
            if (IsNowUsing)
            {
                MessageBox.Show($"This book {this} is use user {currentUser}");
            }
            else
            {
                BookHistory.Add(new BookUsage(currentUser, endTime));
            }
        }

        public Human CurrentHuman => BookHistory[BookHistory.Count].CurrentUser;
        public DateTime UsingEndTime => BookHistory[BookHistory.Count].EndUsingTime;
        public Boolean IsNowUsing => DateTime.Now > BookHistory[BookHistory.Count].EndUsingTime;
    }

    public class BookUsage
    {
        public User CurrentUser;
        public DateTime StartUsingTime;
        public DateTime EndUsingTime;

        public BookUsage(User currentUser, DateTime endTime)
        {
            this.CurrentUser = currentUser;
            this.EndUsingTime = endTime;
            StartUsingTime = DateTime.Now;
        }

        public BookUsage(User currentUser, DateTime startTime, DateTime endTime)
        {
            CurrentUser = currentUser;
            StartUsingTime = startTime;
            EndUsingTime = endTime;
        }
    }
}