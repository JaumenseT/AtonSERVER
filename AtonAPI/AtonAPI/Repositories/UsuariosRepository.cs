using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using AtonAPI.Models;
using System.Diagnostics;

namespace AtonAPI.Repositories
{
    public class UsuariosRepository
    {
        internal void Save(Usuario u)
        {
            MySqlConnection con = Database.GetConnection();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "insert into usuarios(name, userName, password) values" +
            "(@name, @userName, md5(@password))";
            command.Parameters.AddWithValue("name", u.Name);
            command.Parameters.AddWithValue("userName", u.UserName);
            command.Parameters.AddWithValue("password", u.Password);
            Debug.WriteLine("comando" + command.CommandText);
            try
            {
                con.Open();
                command.ExecuteNonQuery();
            }
            finally 
            {
                con.Close();
            }
        }

        internal Usuario GetByUserNamePassword(string userName, string password)
        {       
            using (MySqlConnection con = Database.GetConnection()) {
                con.Open();
                MySqlCommand command = con.CreateCommand();
                command.CommandText = "select * from usuarios where username = @userName and password = md5(@password)";
                command.Parameters.AddWithValue("userName", userName);
                command.Parameters.AddWithValue("password", password);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    return new Usuario(reader.GetInt32("id"), reader.GetString("name"), reader.GetString("userName"), reader.GetString("password"));
                }
            }
            return null;
        }

        internal static Usuario GetUsuarioById(int idUsuario) {
            using (MySqlConnection con = Database.GetConnection()) {
                con.Open();
                MySqlCommand command = con.CreateCommand();
                command.CommandText = "select * from usuarios where id=@idUsuario";
                command.Parameters.AddWithValue("idUsuario", idUsuario);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) {
                    return new Usuario(reader.GetInt32("id"), reader.GetString("name"), reader.GetString("userName"), reader.GetString("password"));
                }
            }
            return null;
        }
    }
}