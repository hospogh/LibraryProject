using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
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
        public static List<Book> Books = new List<Book>();

        private static List<Human> _users = new List<Human>();
//        private static List<Admin> _admins = new List<Admin>();

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
            return new User(name, surname, nickname, password, telephone);
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

        private static List<Book> StricktSearch(Name)

        private static Book SearchBook()
        {
        }

        public static void Terminal()
        {
            Book selectedBook = null;
            Human currentHuman = null;
            Commands command = Commands.none;
            while (command != Commands.exit)
            {
                string line = Console.ReadLine();
                command = DetectCommand(line);
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
                            Console.WriteLine("{0}: command not found", line);
                        }
                        break;
                    case Commands.searchBook:

                        break;
                    case Commands.signOut:
                        currentHuman = null;
                        selectedBook = null;
                        break;
                    case Commands.bookReserve: break;
                    case Commands.addAdmin: break;
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