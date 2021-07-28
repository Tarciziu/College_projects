using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain
{
    public class User :Entity
    {
        public long Id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public User(long id, string username, string password)
        {
            this.Id = id;
            this.username = username;
            this.password = password;
        }
        public User(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public override string ToString() 
        {
            return "User: " + username + "| Password: " + password;
        }
    }
}
