using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using AtonAPI.Repositories;

namespace AtonAPI {
    public class Utilities {
        public static int getUserFromToken(HttpRequestMessage re) {
            var headers = re.Headers;
            int user = -1;

            if (headers.Contains("Authorization") &&
                headers.GetValues("Authorization").First().StartsWith("Bearer ")) {
                user = TokensRepository.getUsuarioByToken(headers.GetValues("Authorization").First().Substring("Bearer ".Length));
            }
            return user;
        }
    }
}