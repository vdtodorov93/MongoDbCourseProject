using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IRepository
    {
        void AddUser(User user);

        User getUserByUsername(string username);

        void AddTweet(Message tweet);

        List<Message> GetMessagesOfUser(string username);

        List<Message> LastMessages(string username, int limit);

        List<Message> GetLastFewMessages(string username, int limit);

        List<User> GetAllUsers();

        void AddFollowing(string nameFollower, string nameFollowed);

        void StopFollowing(string nameFollower, string nameFollowed);
    }
}
