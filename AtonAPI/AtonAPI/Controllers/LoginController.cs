using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AtonAPI.Models;
using AtonAPI.Repositories;

namespace AtonAPI.Controllers
{
    public class LoginController : ApiController
    {
        // GET: api/Login
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Login/5
        public Object GetByUserNamePassword(string userName, string password)
        {
            UsuariosRepository rep = new UsuariosRepository();
            Usuario u = rep.GetByUserNamePassword(userName, password);
            if (u == null)
            {
                return new {
                    error = "Usuario o contraseña incorrecta",
                };
            } else {
                return new {
                    token = TokensRepository.insertarToken(u.Id),
                };
            }
        }

        // POST: api/Login
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Login/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Login/5
        public void Delete(int id)
        {
        }
    }
}
