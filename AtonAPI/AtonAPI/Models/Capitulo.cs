using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtonAPI.Models
{
    public class Capitulo
    {
        public Capitulo(int idCapitulo, int idSerie, int numCapitulo, int numTemporada)
        {
            IdCapitulo = idCapitulo;
            IdSerie = idSerie;
            NumCapitulo = numCapitulo;
            NumTemporada = numTemporada;
        }

        public int IdCapitulo { get; set; }
        public int IdSerie { get; set; }
        public int NumCapitulo { get; set; }
        public int NumTemporada { get; set; }
    }
}