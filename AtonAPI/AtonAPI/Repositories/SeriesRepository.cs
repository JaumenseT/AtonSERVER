using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using AtonAPI.Models;

namespace AtonAPI.Repositories {
    public class SeriesRepository {
        internal static void Save(Serie s) {
            using (MySqlConnection con = Database.GetConnection()) {
                con.Open();
                MySqlCommand command = con.CreateCommand();
                command.CommandText = "insert into series(idSerie, seriesName, seriesPhoto) values" +
                "(@idSerie, @seriesName, @seriesPhoto)";
                command.Parameters.AddWithValue("idSerie", s.IdSerie);
                command.Parameters.AddWithValue("seriesName", s.SeriesName);
                command.Parameters.AddWithValue("seriesPhoto", s.SeriesPhoto);
                Debug.WriteLine("comando" + command.CommandText);
                command.ExecuteNonQuery();

            }
        }

        internal static Serie GetUserSerie(int user, int idSerie) {
            using (MySqlConnection con = Database.GetConnection()) {
                con.Open();
                MySqlCommand command = con.CreateCommand();
                command.CommandText = "select s.* from series s join usuarioseries us on s.idSerie = us.idSerie " +
                            "where us.idSerie = @idSerie and us.idUsuario = @idUsuario";
                command.Parameters.AddWithValue("idSerie", idSerie);
                command.Parameters.AddWithValue("idUsuario", user);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) {
                    return new Serie(reader.GetInt32("idSerie"), reader.GetString("seriesName"), reader.GetString("seriesPhoto"));
                }
            }
            return null;
        }

        internal static bool ExisteSerieDatabase(int idSerie) {
            using (MySqlConnection con = Database.GetConnection()) {
                con.Open();
                MySqlCommand command = con.CreateCommand();
                command.CommandText = "select * from series where idSerie=@idSerie";
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