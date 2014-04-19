using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using MongoDB.Driver.GridFS;

namespace DataLayer
{
    public class MongoRepository : IRepository
    {
        public string ConnectionString { get; set; }
        public MongoClient Client { get; set; }
        public MongoServer Server { get; set; }
        public MongoDatabase Database { get; set; }
        public MongoCollection<User> Users { get; set; }
        public MongoCollection<Message> Tweets { get; set; }

        public MongoRepository()
        {
            this.ConnectionString = "mongodb://localhost";
            this.Client = new MongoClient(ConnectionString);
            this.Server = Client.GetServer();
            this.Database = Server.GetDatabase("Hirundo");
            this.Users = Database.GetCollection<User>("users");
            this.Tweets = Database.GetCollection<Message>("tweets");
        }

        public void AddUser(User user)
        {
            Users.Insert(user);
        }

        public User getUserByUsername(string username)
        {
            var query = Query<User>.EQ(e => e.Username, username);
            User result = Users.FindOne(query);
            return result;
        }

        public void AddTweet(Message tweet)
        {
            Tweets.Insert(tweet);
        }

        public List<Message> GetMessagesOfUser(string username)
        {
            throw new NotImplementedException();
        }

        public List<Message> GetTweetsOfUser(string username)
        {
            List<Message> result = new List<Message>();
            var query =
                from msg in Tweets.AsQueryable<Message>()
                where msg.Username == username
                select msg;
            foreach (var msg in query)
            {
                result.Add(msg);
            }
            return result;
        }

        public List<Message> LastMessages(string username, int limit)
        {
            User currUser = getUserByUsername(username);
            //var query =
            //    (from msg in Tweets.AsQueryable<Message>()
            //     where isUserFollowed(msg.Username, currUser).Equals(true)
            //     orderby msg.MessageDate
            //     select msg).Take(limit);
            //var query2 = Tweets.AsQueryable<Message>()
            //    .Where(msg => isUserFollowed(msg.Username, currUser).Equals(true))
            //    .OrderBy(msg => msg.MessageDate)
            //    .Take(limit);
            //var query3 =
            //    from msg in Tweets.AsQueryable<Message>()
            //    where isUserFollowed(msg.Username, currUser).Equals(true)
            //    orderby msg.MessageDate
            //    select msg;

            //var mongoQuery = ((MongoQueryable<Message>)query3).GetMongoQuery();
            //var cursor = Tweets.Find(mongoQuery);
            //var query4 = Query<Message>.Where(m => isUserFollowed(m.Username, currUser).Equals(true));
            //var messages = Tweets.Find(query4);
            //foreach (var msg in messages)
            //{
            //    result.Add(msg);
            //}
            //var query = Tweets.AsQueryable<Message>()
            //    .Where(msg => currUser.Following.Contains(getUserByUsername(msg.Username)).Equals(true))
            //    .OrderBy(msg => msg.MessageDate)
            //    .Take(limit);
            //var query = Query<Message>.In(m => getUserByUsername(m.Username), currUser.Following);
            //var messages = Tweets.Find(query);
            List<Message> result = new List<Message>();
            //foreach (var msg in messages)
            //{
            //    result.Add(msg);
            //}

            return result;
        }

        public List<Message> GetLastFewMessages(string username, int limit)
        {
            var query =
                from msg in Tweets.AsQueryable<Message>()
                orderby msg.MessageDate descending
                select msg;

            //List<Message> all = new List<Message>();
            //foreach (var msg in query)
            //{
            //    all.Add(msg);
            //}
            //
            
            //int counter = 0;
            //List<Message> result = new List<Message>();
            //for (int i = 0; i < all.Count && counter < limit; i++)
            //{
            //    if (isUserFollowed(all[i].Username, currUser))
            //    {
            //        result.Add(all[i]);
            //        counter++;
            //    }
            //}
            User currUser = getUserByUsername(username);
            int counter = 0;
            List<Message> result = new List<Message>();
            foreach (var msg in query)
            {
                if (isUserFollowed(msg.Username, currUser))
                {
                    result.Add(msg);
                    counter++;
                }
                if (counter >= limit)
                {
                    break;
                }
            }
            return result;
        }

        private bool isUserFollowed(string username, User currUser)
        {
            bool result = false;
            foreach (var followed in currUser.Following)
            {
                if (username.CompareTo(followed.Username) == 0)
                {
                    result = true;
                }
            }
            return result;
        }

        public List<User> GetAllUsers()
        {
            List<User> result = new List<User>();
            var cursor = Users.FindAll();
            foreach (var user in cursor)
            {
                result.Add(user);
            }

            return result;
        }

        public void AddFollowing(string nameFollower, string nameFollowed)
        {
            if (nameFollower.CompareTo(nameFollowed) != 0)
            {
                User follower = getUserByUsername(nameFollower);
                User followed = getUserByUsername(nameFollowed);
                if (follower != null && followed != null && !follower.Following.Contains(followed))
                {
                    follower.Following.Add(followed);
                    Users.Save(follower);
                }
            }
        }

        public void StopFollowing(string nameFollower, string nameFollowed)
        {
            User follower = getUserByUsername(nameFollower);
            User followed = getUserByUsername(nameFollowed);
            if (follower != null && followed != null)
            {
                follower.Following.Remove(followed);
                Users.Save(follower);
            }
        }

    }
}
