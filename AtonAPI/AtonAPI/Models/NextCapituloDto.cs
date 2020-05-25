using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtonAPI.Models {
    public class NextCapituloDto {
        public NextCapituloDto(int idCapitulo, int idSerie, int numCapitulo, int numTemporada, string nameCapitulo, string seriesName, string seriesPhoto) {
            IdCapitulo = idCapitulo;
            IdSerie = idSerie;
            NumCapitulo = numCapitulo;
            NumTemporada = numTemporada;
            NameCapitulo = nameCapitulo;
            SeriesName = seriesName;
            SeriesPhoto = seriesPhoto;
        }

        public int IdCapitulo { get; set; }
        public int IdSerie { get; set; }
        public int NumCapitulo { get; set; }
        public int NumTemporada { get; set; }
        public string NameCapitulo { get; set; }
        public string SeriesName { get; set; }
        public string SeriesPhoto { get; set; }
    }
}