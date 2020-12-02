using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTB.DVDCentral.BL.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DisplayName("User Id")]
        public string UserId { get; set; }
        public string Password { get; set; }
        public User()
        {

        }
        public User(string userid, string password)
        {
            //login
            UserId = userid;
            Password = password;
        }

        public User(int id, string userid, string firstname, string lastname, string password)
        {
            //update
            UserId = userid;
            Password = password;
            Id = id;
            LastName = lastname;
            FirstName = firstname;
        }

        public User(string userid, string firstname, string lastname, string password)
        {
            //create
            UserId = userid;
            Password = password;
            LastName = lastname;
            FirstName = firstname;
        }
    }
}
