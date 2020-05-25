using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AtonAPI.Repositories;
using AtonAPI.Models;

namespace AtonAPI.Controllers
{
    public class UsuariosController : ApiController
    {
        // GET: api/Usuarios
        public Object Get()
        {
            int user = Utilities.getUserFromToken(Request);

            if (user == -1) {
                return new {
                    error = "No tiene autorización",
                };
            }

            return new {
                user = UsuariosRepository.GetUsuarioById(user),
                seriesVistas = UsuarioSeriesRepository.getNumSeriesUsuario(user),
                episodiosVistos = UsuarioCapituloRepository.getNumCapitulosUsuario(user),
            };
        }

        // POST: api/Usuarios
        public object Post([FromBody]Usuario u)
        {
            UsuariosRepository rep = new UsuariosRepository();
            try
            {
                rep.Save(u);
                return "OK";
            }
            catch (Exception e)
            {
                return new { error = "El usuario ya existe" };
            }
        }

        // PUT: api/Usuarios/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Usuarios/5
        public void Delete(int id)
        {
        }
    }
}
