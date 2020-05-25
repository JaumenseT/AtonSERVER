using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using AtonAPI.Models;

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

        internal static List<Serie> getAllSeriesUsuario(int idUsuario) {
            using (MySqlConnection con = Database.GetConnection()) {
                List<Serie> seriesUsuario = new List<Serie>();
                con.Open();
                MySqlCommand command = con.CreateCommand();
                command.CommandText = "select * from usuarioSeries us join series s on us.idSerie=s.idSerie " +
                    "where idUsuario=@idUsuario order by s.seriesName";
                command.Parameters.AddWithValue("idUsuario", idUsuario);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) {
                    seriesUsuario.Add(new Serie(reader.GetInt32("idSerie"), reader.GetString("seriesName"), 
                        reader.GetString("seriesPhoto")));
                }
                return seriesUsuario;
            }
        }

        internal static int getNumSeriesUsuario(int idUsuario) {
            using (MySqlConnection con = Database.GetConnection()) {
                con.Open();
                MySqlCommand command = con.CreateCommand();
                command.CommandText = "select count(*) as cuenta from usuarioSeries where idUsuario=@idUsuario";
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