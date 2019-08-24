using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using Votable.Utilities;

namespace Votable.Models
{
    public class User
    {
        public static string UserFile = "CurrentUser.txt";
        public string Address { get; set; }
        public Guid UserID { get; set; }

        public User()
        {
            Address = "02134";
            UserID = Guid.NewGuid();
        }

        public User(string jsonstring)
        {
            var other = JsonConvert.DeserializeObject<User>(jsonstring);
            this.UserID = other.UserID;
            this.Address = other.Address;
        }

        public void Save()
        {
            IoC.Get<FileService>().WriteToFile(UserFile, JsonConvert.SerializeObject(this));
        }
    }
}
