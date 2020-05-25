using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using AtonAPI.Models;
using TvDbSharper;

namespace AtonAPI.Repositories {

    public class SeriesRepository {
        internal static void Save(Serie s) {
            using (MySqlConnection con = Database.GetConnection()) {

                ITvDbClient client = new TvDbClient();
                client.AcceptedLanguage = "es";
                var task = client.Authentication.AuthenticateAsync(Secrets.apiKey);
                task.Wait();

                var task2 = client.Series.GetAsync(s.IdSerie);
                task2.Wait();
                var result = task2.Result;

                s.SeriesName = result.Data.SeriesName;
                s.SeriesPhoto = result.Data.Poster;

                con.Open();
                MySqlCommand command = con.CreateCommand();
                command.CommandText = "insert into series(idSerie, seriesName, seriesPhoto) values" +
                "(@idSerie, @seriesName, @seriesPhoto)";
                command.Parameters.AddWithValue("idSerie", s.IdSerie);
                command.Parameters.AddWithValue("seriesName", s.SeriesName);
                command.Parameters.AddWithValue("seriesPhoto", s.SeriesPhoto);
                Debug.WriteLine("comando" + command.CommandText);
                command.ExecuteNonQuery();

                int numPagina = 1;
                int numReturned;

                do {
                    var task3 = client.Series.GetEpisodesAsync(s.IdSerie, numPagina);
                    task3.Wait();
                    var episodes = task3.Result;
                    numReturned = episodes.Data.Length;
                    foreach (var e in episodes.Data) {
                        if (e.AiredEpisodeNumber != 0 && e.AiredSeason != 0) {
                            CapituloRepository.Save(new Capitulo(e.Id, e.SeriesId, e.AiredEpisodeNumber, e.AiredSeason, e.EpisodeName));
                        }
                    }
                    numPagina++;
                } while (numReturned == 100);

            }
        }

        internal static void UpdateNextCapitulo(int idSerie, int? nextCapitulo, int idUsuario) {
            using (MySqlConnection con = Database.GetConnection()) {
                con.Open();
                MySqlCommand command = con.CreateCommand();
                command.CommandText = "update usuarioseries set nextCapitulo=@nextCapitulo where " +
                    "idSerie=@idSerie and idUsuario=@idUsuario";
                command.Parameters.AddWithValue("idSerie", idSerie);
                command.Parameters.AddWithValue("nextCapitulo", nextCapitulo);
                command.Parameters.AddWithValue("idUsuario", idUsuario);
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

        internal static int NextEpisodio(int idSerie, int? numCapitulo, int? numTemporada) {
            using (MySqlConnection con = Database.GetConnection()) {
                con.Open();
                MySqlCommand command = con.CreateCommand();
                command.CommandText = "select * from capitulo where idSerie=@idSerie and numCapitulo=@numCapitulo and " +
                    "numTemporada=@numTemporada";
                command.Parameters.AddWithValue("idSerie", idSerie);
                command.Parameters.AddWithValue("numCapitulo", numCapitulo + 1);
                command.Parameters.AddWithValue("numTemporada", numTemporada);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) {
                    return reader.GetInt32("idCapitulo");
                }
                reader.Close();
                command = con.CreateCommand();
                command.CommandText = "select * from capitulo where idSerie=@idSerie and numCapitulo=1 and " +
                    "numTemporada=@numTemporada";
                command.Parameters.AddWithValue("idSerie", idSerie);
                command.Parameters.AddWithValue("numTemporada", numTemporada + 1);
                reader = command.ExecuteReader();
                while (reader.Read()) {
                    return reader.GetInt32("idCapitulo");
                }
                return 0;
            }
        }

        internal static int GetNextEpisodio(int episodioVisto, int idUsuario) {
            int nextEpisodio = 0;
            Capitulo c = CapituloRepository.GetCapituloById(episodioVisto);
            using (MySqlConnection con = Database.GetConnection()) {
                con.Open();
                MySqlCommand command = con.CreateCommand();
                command.CommandText = "select * from usuarioseries where idSerie=@idSerie and idUsuario=@idUsuario";
                command.Parameters.AddWithValue("idSerie", c.IdSerie);
                command.Parameters.AddWithValue("idUsuario", idUsuario);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) {
                    nextEpisodio = reader.GetInt32("nextCapitulo");
                }
            }
            Capitulo cNextEpisodio = null;
            if (nextEpisodio != 0) {
                cNextEpisodio = CapituloRepository.GetCapituloById(nextEpisodio);
                if (c.NumTemporada > cNextEpisodio.NumTemporada) {
                    return NextEpisodio(c.IdSerie, c.NumCapitulo, c.NumTemporada);
                } else if (c.NumTemporada == cNextEpisodio.NumTemporada && c.NumCapitulo >= cNextEpisodio.NumCapitulo) {
                    return NextEpisodio(c.IdSerie, c.NumCapitulo, c.NumTemporada);
                } else {
                    return cNextEpisodio.IdCapitulo;
                }
            } else {
                return NextEpisodio(c.IdSerie, c.NumCapitulo, c.NumTemporada);
            }
        }
    }
}