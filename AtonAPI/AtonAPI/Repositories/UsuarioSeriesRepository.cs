using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace AtonAPI.Repositories {
    public class UsuarioSeriesRepository {
        internal static void Save(int idUsuario, int idSerie, int? nextCapitulo) {
            using (MySqlConnection con = Database.GetConnection()) {
                con.Open();
                MySqlCommand command = con.CreateCommand();
                command.CommandText = "insert into usuarioSeries(idUsuario, idSerie, nextCapitulo) values" +
                    "(@idUsuario, @idSerie, @nextCapitulo)";
                command.Parameters.AddWithValue("idUsuario", idUsuario);
                command.Parameters.AddWithValue("idSerie", idSerie);
                command.Parameters.AddWithValue("nextCapitulo", nextCapitulo);
                command.ExecuteNonQuery();
            }
        }

        internal static void Delete(int idUsuario, int idSerie) {
            using (MySqlConnection con = Database.GetConnection()) {
                con.Open();
                MySqlCommand command = con.CreateCommand();
                command.CommandText = "delete from usuarioSeries where idUsuario=@idUsuario and idSerie=@idSerie";
                command.Parameters.AddWithValue("idUsuario", idUsuario);
                command.Parameters.AddWithValue("idSerie", idSerie);
                command.ExecuteNonQuery();
            }
        }

        internal static bool ExisteUsuarioSerieDatabase(int idUsuario, int idSerie) {
            using (MySqlConnection con = Database.GetConnection()) {
                con.Open();
                MySqlCommand command = con.CreateCommand();
                command.CommandText = "select * from usuarioSeries where idUsuario=@idUsuario and idSerie=@idSerie";
                command.Parameters.AddWithValue("idUsuario", idUsuario);
                command.Parameters.AddWithValue("idSerie", idSerie);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) {
                    return true;
                }
            }
            return false;
        }
    }
}