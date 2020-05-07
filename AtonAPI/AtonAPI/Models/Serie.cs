using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtonAPI.Models
{
    public class Serie
    {
        public Serie(int idSerie, string seriesName, string seriesPhoto)
        {
            IdSerie = idSerie;
            SeriesName = seriesName;
            SeriesPhoto = seriesPhoto;
        }

        public int IdSerie { get; set; }
        public string SeriesName { get; set; }
        public string SeriesPhoto { get; set; }
    }
}