using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AtonAPI.Models;
using AtonAPI.Repositories;
using System.Diagnostics;
using TvDbSharper;

namespace AtonAPI.Controllers
{
    public class SeriesController : ApiController
    {

        // GET: api/Series
        public Object Get()
        {
            int user = Utilities.getUserFromToken(Request);

            if (user == -1) {
                return new {
                    error = "No tiene autorización",
                };
            }

            return UsuarioSeriesRepository.getAllSeriesUsuario(user);
        }

        // GET: api/Series/5
        public Object Get(int id)
        {
            int user = Utilities.getUserFromToken(Request);

            if (user == -1) {
                return new {
                    error = "No tiene autorización",
                };
            }

            Serie s = SeriesRepository.GetUserSerie(user, id);

            if (s == null) {
                return new { };
            } else {
                return s;
            }

        }

        // POST: api/Series
        public Object Post(int idSerie)
        {
            int user = Utilities.getUserFromToken(Request);

            if (user == -1) {
                return new {
                    error = "No tiene autorización",
                };
            }

            if (!SeriesRepository.ExisteSerieDatabase(idSerie)) {
                SeriesRepository.Save(new Serie(idSerie, "", ""));
            }
            
            UsuarioSeriesRepository.Save(user, idSerie, CapituloRepository.GetEpisodioId(idSerie, 1, 1));
            return "OK";
        }

        // PUT: api/Series/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Series/5
        public Object Delete(int idSerie)
        {
            int user = Utilities.getUserFromToken(Request);

            if (user == -1) {
                return new {
                    error = "No tiene autorización",
                };
            }

            UsuarioSeriesRepository.Delete(user, idSerie);
            return "OK";
        }
    }
}
