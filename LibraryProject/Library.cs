using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace LibraryProject
{
    internal enum UserCommands
    {
        none,
        allCommands,
        bookSearch,
        bookReserve
    }

    internal enum DefaultCommands
    {
        none,
        signUp,
        signIn,
        signOut,
        searchBook
    }

    internal enum AdminCommands
    {
        none,
        allCommands,
        addAdmin,
        addBook,
        removeBookWithId,
        removeBook,
        viweBookHistory
    }


    public static class Library
    {
        public static List<Book> Books = new List<Book>();
        private static List<User> _users = new List<User>();
        private static List<Admin> _admins = new List<Admin>();

        private static void PrintCommands(Type enumType)
        {
            string _res = enumType.GetEnumNames().Aggregate("", (current, val) => current + (val + " "));
            Console.WriteLine(_res.Replace("none", ""));
        }

        private static T DetectCommand<T>(string str)
        {
            str = str.Trim().Split()[0];
            Type type = typeof(T);
            foreach (string val in type.GetEnumNames())
            {
                if (val.Equals(str))
                {
                    return (T) Enum.Parse(type, val);
                }
            }
            return (T) Enum.Parse(type, "none");
        }

        private static void CommandExecution<T>(T command)
        {
        }

        public static void Terminal()
        {
            string line = Console.ReadLine();
            Human currentHuman = null;
            Type type = typeof(DefaultCommands);
            while ()
            {
                if (currentHuman == null && DetectCommand<>(line) != DefaultCommands.none)
                {
                }
            }
        }
    }
}