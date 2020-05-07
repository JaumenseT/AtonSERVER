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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Usuarios/5
        public string Get(int id)
        {
            return "value";
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
