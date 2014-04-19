using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataLayer
{
    public class User
    {
        public ObjectId Id { get; set; }

        public string Username { get; private set; }

        public string Email { get; private set; }

        public DateTime RegisterDate { get; private set; }

        public List<User> Following { get; private set; }

        public bool Verified { get; private set; }

        public string HashedPassword { get; private set; }

        public User(string username, string email, DateTime registerDate, string hashedPassword)
        {
            this.Username = username;
            this.Email = email;
            this.RegisterDate = registerDate;
            this.Following = new List<User>();
            this.Verified = false;
            this.HashedPassword = hashedPassword;
        }

        public override int GetHashCode()
        {
            return Username.GetHashCode() ^ Email.GetHashCode();
        }

        public override bool Equals(Object u)
        {
            User user = (User)u;
            return this.Username.CompareTo(user.Username) == 0;
        }
    }
}