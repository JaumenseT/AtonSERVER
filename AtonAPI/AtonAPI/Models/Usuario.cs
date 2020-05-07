using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtonAPI.Models
{
    public class Usuario
    {
        public Usuario(int id, string name, string userName, string password)
        {
            Id = id;
            Name = name;
            UserName = userName;
            Password = password;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}