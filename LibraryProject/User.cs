using System.ComponentModel;

namespace LibraryProject
{
    public abstract class User
    {
        public string Name;
        public string Surname;
        public string Nicknme;
        public string Password;
        public string Telephone;
    }

    public class Admin : User
    {
        public string Name;
        public string Surname;
        public string Nicknme;
        public string Password;
        public string Telephone;

        public Admin(string name, string surname, string nickname, string password)
        {
            this.Name = name;
            this.Surname = surname;
            this.Nicknme = nickname;
            this.Password = password;
        }

        public Admin(string name, string surname, string nickname, string password, string telephone) : this(name,
            surname, nickname, password)
        {
            this.Telephone = telephone;
        }
    }
    //AddBook
}