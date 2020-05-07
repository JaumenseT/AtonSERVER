using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace AtonAPI.Repositories
{
    public class Database
    {
        static internal MySqlConnection GetConnection()
        {
            string url = "server=localhost;database=atondb;uid=root;pwd=;";
            return new MySqlConnection(url);
        }
    }
}