using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataLayer
{

    public class Message
    {
        public ObjectId Id { get; set; }

        public string Username { get; private set; }

        public string Content { get; private set; }

        public string Location { get; private set; }

        public DateTime MessageDate { get; set; }

        public Message(string username, string content, DateTime messageDate, string location = null)
        {
            this.Username = username;
            this.Content = content;
            this.MessageDate = messageDate;
            this.Location = location;
        }

        public Message()
            : this(null, null, DateTime.Now)
        { }

    }
}