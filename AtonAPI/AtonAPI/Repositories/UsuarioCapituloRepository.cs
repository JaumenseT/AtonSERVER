using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtonAPI.Repositories {
    public class UsuarioCapituloRepository {
        internal static void Save(int idUsuario, int idCapitulo) {
            using (MySqlConnection con = Database.GetConnection()) {
                con.Open();
                MySqlCommand command = con.CreateCommand();
                command.CommandText = "insert into usuarioCapitulo(idUsuario, idCapitulo) values" +
                    "(@idUsuario, @idCapitulo)";
                command.Parameters.AddWithValue("idUsuario", idUsuario);
                command.Parameters.AddWithValue("idCapitulo", idCapitulo);
                command.ExecuteNonQuery();
            }
        }

        internal static void Delete(int idUsuario, int idCapitulo) {
            using (MySqlConnection con = Database.GetConnection()) {
                con.Open();
                MySqlCommand command = con.CreateCommand();
                command.CommandText = "delete from usuarioCapitulo where idUsuario=@idUsuario and idCapitulo=@idCapitulo";
                command.Parameters.AddWithValue("idUsuario", idUsuario);
                command.Parameters.AddWithValue("idCapitulo", idCapitulo);
                command.ExecuteNonQuery();
            }
        }

        internal static int getNumCapitulosUsuario(int idUsuario) {
            using (MySqlConnection con = Database.GetConnection()) {
                con.Open();
                MySqlCommand command = con.CreateCommand();
                command.CommandText = "select count(*) as cuenta from usuarioCapitulo where idUsuario=@idUsuario";
                command.Parameters.AddWithValue("idUsuario", idUsuario);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) {
                    return reader.GetInt32("cuenta");
                }
                return -1;
            }
        }
    }
}