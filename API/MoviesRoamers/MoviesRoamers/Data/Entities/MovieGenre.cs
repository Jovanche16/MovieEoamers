using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class MovieGenre
    {
        public int MovieId { get; set; }
       // [JsonIgnore]
        public Movie Movie { get; set; }
        public int GenreId { get; set; }
       // [JsonIgnore]
        public Genre Genre { get; set; }
    }
}
