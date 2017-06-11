using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LibraryProject
{
    public class Book
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public int? BookAge { get; set; }
        public double? Price;
        public Guid Id { get; }
        public List<BookHistory> BookHistory { get; }

        //constructors
        public Book(string name, string author, int bookAge)
        {
            Name = name;
            Author = author;
            BookAge = bookAge;
            BookHistory = new List<BookHistory>();
            Id = Guid.NewGuid();
        }

        public Book(string name, string author, int bookAge, double price) : this(name, author, bookAge)
        {
            Price = price;
        }

        //Methods and Properties
        public override string ToString()
        {
            return $"{Name}[{Author}]({BookAge})" + "{" + $"{Price}, {Id}" + "}";
        }

        public void PrintBookHistory()
        {
            Console.WriteLine($"book:{this}\nhistory: ");
            foreach (BookHistory b in BookHistory)
            {
                Console.WriteLine(b.ToString());
            }
        }

        public DateTime UsingEndTime => BookHistory[BookHistory.Count].EndUsingTime;
        public Boolean IsNowUsing => DateTime.Now > BookHistory[BookHistory.Count].EndUsingTime;
    }

    public class BookHistory
    {
        public User CurrentUser { get; set; }
        public DateTime StartUsingTime { get; set; }
        public DateTime EndUsingTime { get; set; }

        public BookHistory(User currentUser, DateTime endTime)
        {
            CurrentUser = currentUser;
            EndUsingTime = endTime;
            StartUsingTime = DateTime.Now;
        }

        public BookHistory(User currentUser, DateTime startTime, DateTime endTime)
            : this(currentUser, endTime)
        {
            StartUsingTime = startTime;
        }

        public override string ToString()
        {
            return $"User: {CurrentUser}, at {StartUsingTime} to {EndUsingTime}.";
        }
    }
}