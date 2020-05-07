using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace AtonAPI.Repositories {
    public class TokensRepository {
        internal static string insertarToken(int usuario) {
            for (int i = 0; i < 10; i++) {
                using (MySqlConnection con = Database.GetConnection()) {
                    con.Open();
                    MySqlCommand command = con.CreateCommand();
                    string g = Guid.NewGuid().ToString();
                    command.CommandText = "insert into tokens(UUID, user) values" +
                    "(@UUID, @user)";
                    command.Parameters.AddWithValue("UUID", g);
                    command.Parameters.AddWithValue("user", usuario);
                    command.ExecuteNonQuery();
                    return g;
                }
            }
            throw new Exception("Error al conectarse a la base de datos");
        }

        internal static int getUsuarioByToken(string token) {
            using (MySqlConnection con = Database.GetConnection()) {
                con.Open();
                MySqlCommand command = con.CreateCommand();
                command.CommandText = "select user from tokens where UUID = @UUID";
                command.Parameters.AddWithValue("UUID", token);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) {
                    return reader.GetInt32("user");
                }
            }
            return -1;
        }
    }
}