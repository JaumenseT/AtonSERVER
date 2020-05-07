using AtonAPI.Models;
using AtonAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AtonAPI.Controllers
{
    public class CapituloController : ApiController
    {
        // GET: api/Capitulo
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Capitulo/5
        public Object Get(int id)
        {
            int user = Utilities.getUserFromToken(Request);

            if (user == -1) {
                return new {
                    error = "No tiene autorización",
                };
            }

            Capitulo c = CapituloRepository.GetUserCapitulo(user, id);

            if (c == null) {
                return new { };
            } else {
                return c;
            }
        }

        // POST: api/Capitulo
        public Object Post(int idCapitulo, int idSerie, int numCapitulo, int numTemporada)
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
            if (!UsuarioSeriesRepository.ExisteUsuarioSerieDatabase(user, idSerie)) {
                UsuarioSeriesRepository.Save(user, idSerie, null);
            }
            if (!CapituloRepository.ExisteCapituloDatabase(idCapitulo)) {
                CapituloRepository.Save(new Capitulo(idCapitulo, idSerie, numCapitulo, numTemporada));
            }
            UsuarioCapituloRepository.Save(user, idCapitulo);
            return "OK";
        }

        // PUT: api/Capitulo/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Capitulo/5
        public Object Delete(int idCapitulo)
        {
            int user = Utilities.getUserFromToken(Request);

            if (user == -1) {
                return new {
                    error = "No tiene autorización",
                };
            }

            UsuarioCapituloRepository.Delete(user, idCapitulo);
            return "OK";
        }
    }
}
