using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace LibraryProject
{
    public abstract class Human
    {
        public abstract string Name { get; set; }
        public abstract string Surname { get; set; }
        public abstract string Nickname { get; set; }
        public abstract string Password { get; set; }
        public abstract string Telephone { get; set; }

        public override string ToString()
        {
            return $"Nickname(Name Surname)";
        }
    }

    public sealed class Admin : Human
    {
        // fields
        public override string Name { get; set; }

        public override string Surname { get; set; }
        public override string Nickname { get; set; }
        public override string Password { get; set; }
        public override string Telephone { get; set; }

        //constructors
        public Admin(string name, string surname, string nickname, string password)
        {
            Name = name;
            Surname = surname;
            Nickname = nickname;
            Password = password;
        }

        public Admin(string name, string surname, string nickname, string password, string telephone) : this(name,
            surname, nickname, password)
        {
            Telephone = telephone;
        }

        //methods
        public void AddBook(Book book, List<Book> allBooks)
        {
            allBooks.Add(book);
        }

        public void RemoveBook(Guid bookID, List<Book> allBooks)
        {
            foreach (Book book in allBooks)
            {
                if (book.ID != bookID) continue;
                allBooks.Remove(book);
                MessageBox.Show("Book is removed.");
                return;
            }
            MessageBox.Show("No Book with this ID.");
        }

        public void RemoveBook(String bookName, string authorName, List<Book> allBooks)
        {
            //strickt search
            foreach (Book book in allBooks)
            {
                if (book.Name != bookName || book.Author != authorName) continue;
                allBooks.Remove(book);
                MessageBox.Show("Book is removed.");
                return;
            }
            MessageBox.Show("No Book with this ID.");
        }
    }


    public sealed class User : Human
    {
        public override string Name { get; set; }
        public override string Surname { get; set; }
        public override string Nickname { get; set; }
        public override string Password { get; set; }
        public override string Telephone { get; set; }

        public List<BookUsage> UserBooksHistory { get; }

        public User(string name, string surname, string nickname, string password)
        {
            Name = name;
            Surname = surname;
            Nickname = nickname;
            Password = password;
            UserBooksHistory = new List<BookUsage>();
        }

        public User(string name, string surname, string nickname, string password, string telephone) : this(name,
            surname, nickname, password)
        {
            this.Telephone = telephone;
        }
    }
}