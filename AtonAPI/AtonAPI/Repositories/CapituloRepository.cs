using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using AtonAPI.Models;
using MySql.Data.MySqlClient;

namespace AtonAPI.Repositories {
    public class CapituloRepository {
        internal static void Save(Capitulo c) {
            using (MySqlConnection con = Database.GetConnection()) {
                con.Open();
                MySqlCommand command = con.CreateCommand();
                command.CommandText = "insert into capitulo(idCapitulo, idSerie, numCapitulo, numTemporada) values" +
                "(@idCapitulo, @idSerie, @numCapitulo, @numTemporada)";
                command.Parameters.AddWithValue("idCapitulo", c.IdCapitulo);
                command.Parameters.AddWithValue("idSerie", c.IdSerie);
                command.Parameters.AddWithValue("numCapitulo", c.NumCapitulo);
                command.Parameters.AddWithValue("numTemporada", c.NumTemporada);
                Debug.WriteLine("comando" + command.CommandText);
                command.ExecuteNonQuery();
            }
        }

        internal static Capitulo GetUserCapitulo(int user, int idCapitulo) {
            using (MySqlConnection con = Database.GetConnection()) {
                con.Open();
                MySqlCommand command = con.CreateCommand();
                command.CommandText = "select c.* from capitulo c join usuariocapitulo uc on c.idCapitulo = uc.idCapitulo " +
                            "where uc.idCapitulo = @idCapitulo and uc.idUsuario = @idUsuario";
                command.Parameters.AddWithValue("idCapitulo", idCapitulo);
                command.Parameters.AddWithValue("idUsuario", user);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) {
                    return new Capitulo(
                        reader.GetInt32("idCapitulo"),
                        reader.GetInt32("idSerie"),
                        reader.GetInt32("numCapitulo"),
                        reader.GetInt32("numTemporada"));
                }
            }
            return null;
        }

        internal static bool ExisteCapituloDatabase(int idCapitulo) {
            using (MySqlConnection con = Database.GetConnection()) {
                con.Open();
                MySqlCommand command = con.CreateCommand();
                command.CommandText = "select * from capitulo where idCapitulo=@idCapitulo";
                command.Parameters.AddWithValue("idCapitulo", idCapitulo);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) {
                    return true;
                }
            }
            return false;
        }
    }
}