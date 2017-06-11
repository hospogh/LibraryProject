using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace LibraryProject
{
    internal enum Commands
    {
        none, // none
        signUp, // default mode
        signIn, // default mode 

        exit, // all modes
        searchBook, // all modes 
        allCommands, // all modes (last command for default mode)


        //User commands
        signOut, // user & admin modes
        selectBook, //user & admin modes
        bookReserve, // user & admin modes 
        getBookId,

        //Admin commands
        addAdmin,
        addBook,
        removeBookWithId,
        removeBook,
        viweBookHistory,
    }


    public static class Library
    {
        //Data
        public static List<Book> Books = new List<Book>();

        private static List<Human> _users = new List<Human>();

        /* Methods */
        private static Commands DetectCommand(string str)
        {
            str = str.Trim().Split()[0];
            foreach (string val in typeof(Commands).GetEnumNames())
            {
                if (val.Equals(str))
                {
                    return (Commands) Enum.Parse(typeof(Commands), str);
                }
            }
            return Commands.none;
        }

        private static void PrintCommands<T>(T currentHuman)
        {
            string _res = "";
            string[] cmds = typeof(Commands).GetEnumNames();
            int i;
            int j;
            if (currentHuman == null)
            {
                i = (int) Commands.signUp;
                j = (int) Commands.allCommands;
            }
            else
            {
                i = (int) Commands.exit;
                j = typeof(T) == typeof(User) ? (int) Commands.bookReserve : cmds.Length - 1;
            }
            while (i <= j)
            {
                _res += $"cmds[i] ";
                i++;
            }
            Console.WriteLine(_res);
        }

        private static string GetPassword()
        {
            string password = "";
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                    {
                        password = password.Substring(0, (password.Length - 1));
                        Console.Write("\b \b");
                    }
                }
            } while (key.Key != ConsoleKey.Enter);
            return Encode.Encrypt(password);
        }

        private static User SignUp()
        {
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Surname: ");
            string surname = Console.ReadLine();
            Console.Write("Nickname: ");
            string nickname = Console.ReadLine();
            Console.Write("Password: ");
            string password = GetPassword();
            Console.Write("Telephone: ");
            string telephone = Console.ReadLine();
            User usr = new User(name, surname, nickname, password, telephone);
            _users.Add(usr);
            return usr;
        }

        private static Human SignIn()
        {
            Console.Write("Nickname: ");
            string nickname = Console.ReadLine();
            Console.Write("Password: ");
            string password = GetPassword();
            foreach (var usr in _users)
            {
                if (usr.Nickname == nickname && usr.Password == password)
                {
                    return usr;
                }
            }
            MessageBox.Show("incorrect nickname or password.");
            return null;
        }

        //search
        private static Book StricktSearch(string name, string author, int? age = null)
        {
            foreach (var book in Books)
            {
                if (book.Name == name && book.Author == author && book.BookAge == age)
                {
                    return book;
                }
            }
            return null;
        }

        private static Book SearchBook()
        {
            Console.WriteLine("# strickt search");
            Console.WriteLine("book name: ");
            string name = Console.ReadLine();
            Console.WriteLine("book author: ");
            string author = Console.ReadLine();
            Console.WriteLine("book age: ");
            int? age;
            try
            {
                age = int.Parse(s: Console.ReadLine());
            }
            catch
            {
                age = null;
            }
            return StricktSearch(name, author, age);
        }

        //book reserve
        private static void BookReserve(Human human, Book selectedBook)
        {
            if (human == null)
            {
                Console.WriteLine("first sing in.");
                return;
            }
            if (selectedBook == null)
            {
                Console.WriteLine("no book selected.");
                return;
            }
            Console.WriteLine("are you want to reserve this book? (Y/n) : ");
            string readLine = Console.ReadLine();
            if (readLine != null && readLine.ToLower().Trim() == "y")
            {
                int lgt = selectedBook.BookHistory.Count;
                if (!selectedBook.IsNowUsing)
                {
                    Console.WriteLine("book is already reserved.");
                    return;
                }
                human.UserHistory.Add(new UserHistory(human, DateTime.Now,
                    DateTime.Now.Add(new TimeSpan(30, 0, 0, 0))));
                selectedBook.BookHistory.Add(new BookHistory(selectedBook, DateTime.Now,
                    DateTime.Now.Add(new TimeSpan(30, 0, 0, 0))));
            }
        }

        private static void AddAdmin()
        {
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Surname: ");
            string surname = Console.ReadLine();
            Console.Write("Nickname: ");
            string nickname = Console.ReadLine();
            Console.Write("Password: ");
            string password = GetPassword();
            Console.Write("Telephone: ");
            string telephone = Console.ReadLine();
            _users.Add(new Admin(name, surname, nickname, password, telephone));
        }

        private static void AddBook()
        {
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Author name: ");
            string author = Console.ReadLine();
            Console.Write("Age: ");
            int age = int.Parse(s: Console.ReadLine());
            Console.Write("Price: ");
            Double? price = Double.Parse(Console.ReadLine());
            Books.Add(new Book(name, author, age, price));
        }

        public static void Terminal()
        {
            Human currentHuman = null;
            Commands command = Commands.none;
            while (command != Commands.exit)
            {
                string line = Console.ReadLine();
                command = DetectCommand(line);
                Book selectedBook = null;
                switch (command)
                {
                    case Commands.exit: return;
                    case Commands.allCommands:
                        PrintCommands(currentHuman);
                        break;
                    case Commands.signUp:
                        if (currentHuman != null)
                        {
                            command = Commands.none;
                            break;
                        }
                        currentHuman = SignUp();
                        _users.Add(currentHuman);
                        break;
                    case Commands.signIn:
                        if (currentHuman == null)
                        {
                            currentHuman = SignIn();
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"{line}: command not found");
                        }
                        break;
                    case Commands.searchBook:
                        selectedBook = SearchBook();
                        Console.WriteLine(selectedBook == null ? "incorrect data." : "book is selected.");
                        break;
                    case Commands.signOut:
                        currentHuman = null;
                        selectedBook = null;
                        break;
                    case Commands.bookReserve:
                        BookReserve(currentHuman, selectedBook);
                        break;
                    case Commands.addAdmin:
                        if (currentHuman is Admin)
                        {
                            AddAdmin();
                        }
                        break;
                    case Commands.addBook: break;
                    case Commands.removeBook: break;
                    case Commands.removeBookWithId: break;
                    case Commands.viweBookHistory: break;
                }
                if (command == Commands.none)
                {
                    Console.WriteLine("No command '{0}' found.", command);
                }
            }
        }
    }
}